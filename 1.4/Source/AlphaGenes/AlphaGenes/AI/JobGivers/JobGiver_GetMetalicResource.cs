using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace AlphaGenes
{
    public class JobGiver_GetMetalicResource : ThinkNode_JobGiver
    {
        //In hindsight it may have been easier to patch metals to have ingestible properities then reinvent the whole wheel. But this is much better for mod compatiblity, plus I dont have to use xpath!
        public override float GetPriority(Pawn pawn)
        {
            if (!ModsConfig.BiotechActive)
            {
                return 0f;
            }
            Pawn_GeneTracker genes = pawn.genes;
            if(genes!=null && genes.GetFirstGeneOfType<Gene_Resource_Metal>() == null)
            {
                return 0f;
            }
            return 9.1f;
        }
        protected override Job TryGiveJob(Pawn pawn)
        {
            if (!ModsConfig.BiotechActive) { return null; }
            var gene = pawn.genes.GetFirstGeneOfType<Gene_Resource_Metal>();
            if(gene == null) { return null; }
            if(!gene.ShouldConsumeNow()) { return null; }
            float mass = gene.MassDesired;
            Thing metalToTake = GetMetalsForMass(pawn, mass, out var count); //Issue here is it will only satsify it if there is enough material to eat to target. Which means rationing has to be done via player with target value
            if(metalToTake == null) { return null; }
            Job job = JobMaker.MakeJob(InternalDefOf.AG_ConsumeMetal, metalToTake);
            job.count = count;
            return job;
        }
        public static Thing GetMetalsForMass(Pawn pawn, float mass, out int count)
        {
            //Issue with this is its only things in stockpile. If it becomes an issue and performance is not a concern swap to a listerthing haulable
            count = 0;
            //Due to variance in how this is used making it based on haulable. Hopefully perfomence doesn't get murdered
            var metalsCount = pawn.Map.listerThings.ThingsInGroup(ThingRequestGroup.HaulableAlways).Where(x => x.def.IsMetal);
            if (!metalsCount.Any()) return null;
            float mvForMass = 99999f;            
            ThingDef candidate = null;
            //Choose the cheapest metals
            foreach(var thing in metalsCount)
            {
                var tmpMass = thing.def.statBases.First(x => x.stat == StatDefOf.Mass).value;
                if (tmpMass * thing.stackCount >= mass)
                {
                    int tmpCount = Mathf.CeilToInt(mass / tmpMass)+1;
                    var tmpMV = thing.def.statBases.First(x => x.stat == StatDefOf.MarketValue).value * tmpCount;
                    if(tmpMV < mvForMass )
                    {
                        candidate = thing.def;
                        mvForMass = tmpMV;
                        count=tmpCount;
                    }
                }
            }
            if(candidate ==null) return null;
            var locCount = count;
            return GenClosest.ClosestThing_Global_Reachable(pawn.Position, pawn.Map, pawn.Map.listerThings.ThingsOfDef(candidate), PathEndMode.Touch, TraverseParms.For(pawn), validator: (Thing t) =>
            {
                return pawn.CanReserve(t, stackCount: locCount) && !t.IsForbidden(pawn);
            });
        }
    }
}
