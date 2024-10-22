using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using UnityEngine;
using Verse.AI;

namespace AlphaGenes
{
    public class WorkGiver_ForceEatMetal : WorkGiver_Scanner
    {
        //This is a bit of a wonky way to do this. Better alternative may be to use a designator but that has its own tricky/hacky issues
        //Primary purpose is so metal eaters can eat metal objects that aren't raw meterials. Like weapons.
        //Not putting that as part of the thinknode jobgiver as eating a weapon randomly would be frustating to say the least...
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForGroup(ThingRequestGroup.HaulableAlways);

        public override PathEndMode PathEndMode => PathEndMode.ClosestTouch;

        public override Danger MaxPathDanger(Pawn pawn)
        {
            return Danger.Deadly;
        }
        public override bool ShouldSkip(Pawn pawn, bool forced = false)
        {
            var gene = pawn.genes?.GetFirstGeneOfType<Gene_Resource_Metal>();
            if (gene == null) { return true; }
            return base.ShouldSkip(pawn, forced);
        }
        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!forced) { return false; }
            var gene = pawn.genes?.GetFirstGeneOfType<Gene_Resource_Metal>();
            if (gene == null) { return false; }           
            float nutrition = gene.GetResourceRestore(t);
            if(nutrition == 0) { return false; }
            return true;
        }

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!forced) { return null; }
            var gene = pawn.genes.GetFirstGeneOfType<Gene_Resource_Metal>();
            if (gene == null) { return null; }            
            float nutrition = gene.GetResourceRestore(t);
            int count = 1;
            if(t.stackCount > 1)
            {
                float massDesire = gene.MassDesired;
                float mass = t.def.statBases.First(x => x.stat == StatDefOf.Mass).value;
                count = Mathf.Min(t.stackCount,Mathf.CeilToInt(massDesire / mass));
            }
            Job job = JobMaker.MakeJob(InternalDefOf.AG_ConsumeMetal, t);
            job.count = count;
            return job;
            
        }


    }
}
