using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using UnityEngine;
using Verse.AI;

namespace AlphaGenes
{
    public class WorkGiver_Warden_DeliverMetal : WorkGiver_Warden
    {
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForGroup(ThingRequestGroup.Pawn);

        public override PathEndMode PathEndMode => PathEndMode.ClosestTouch;

        public override Danger MaxPathDanger(Pawn pawn)
        {
            return Danger.Deadly;
        }
        
        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!ShouldTakeCareOfPrisoner(pawn, t, forced))
            {
                return null;
            }
            Pawn prisoner = t as Pawn;
            if (prisoner == null || prisoner == pawn) { return null; }
            if(!prisoner.guest.CanBeBroughtFood || !prisoner.Position.IsInPrisonCell(prisoner.Map)) { return null; }
            var gene = prisoner.genes?.GetFirstGeneOfType<Gene_Resource_Metal>();
            if (gene == null) { return null; }
            if (gene.ValuePercent >= 0.95f) { return null; }
            if (!forced && !gene.ShouldConsumeNow()) { return null; }
            if (WardenFeedUtility.ShouldBeFed(prisoner)) { return null; }
            if(MetalAlreadyDelivered(prisoner)) { return null; }
            float mass = gene.MassDesired;
            var candidate = JobGiver_GetMetalicResource.GetMetalsForMass(pawn, mass, out var count);            
            IntVec3 dest = RCellFinder.SpotToStandDuringJob(prisoner);
            if (!pawn.CanReserve(candidate) || !pawn.CanReserve(dest) || !pawn.CanReserve(prisoner)) { return null; }
            if (candidate != null)
            {
                var job = JobMaker.MakeJob(InternalDefOf.AG_DeliverMetal, candidate, dest,prisoner);
                job.count = count;
                return job;
            }            
            return null;
        }
        private bool MetalAlreadyDelivered(Pawn prisoner)
        {
            var gene = prisoner.genes?.GetFirstGeneOfType<Gene_Resource_Metal>();
            float mass = gene.MassDesired;
            var room = prisoner.GetRoom();
            if (prisoner.carryTracker.CarriedThing?.def.IsMetal ?? false) { return true; }
            if (prisoner.inventory.innerContainer.Any(x => x.def.IsMetal)) { return true; }
            float mvForMass = 99999f;
            int count = 1;
            Thing candidate = null;      
            //making a quicker version of this to avoid looping listerthings twice
            foreach (var thing in room.ContainedAndAdjacentThings.Where(x=>x.def.IsMetal))
            {
                var tmpMass = thing.def.statBases.First(x => x.stat == StatDefOf.Mass).value;
                if (tmpMass * thing.stackCount >= mass)
                {
                    int tmpCount = Mathf.CeilToInt(mass / tmpMass);
                    var tmpMV = thing.def.statBases.First(x => x.stat == StatDefOf.MarketValue).value * tmpCount;
                    if (tmpMV < mvForMass)
                    {
                        candidate = thing;
                        mvForMass = tmpMV;
                        count = tmpCount;
                    }
                }
            }            
            return candidate != null;
        }

    }
}
