using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace AlphaGenes
{
    public class WorkGiver_AdministerMetal : WorkGiver_Scanner
    {
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForGroup(ThingRequestGroup.Pawn);

        public override PathEndMode PathEndMode => PathEndMode.ClosestTouch;

        public override Danger MaxPathDanger(Pawn pawn)
        {
            return Danger.Deadly;
        }
        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            Pawn patient = t as Pawn;
            if (patient == null || patient == pawn) { return false; }
            var gene = patient.genes?.GetFirstGeneOfType<Gene_Resource_Metal>();
            if(gene == null) { return false; }
            if(gene.ValuePercent >= 0.95f) { return false; }
            if(!forced && gene.ValuePercent >=0.25f) { return false; }
            if(!FeedPatientUtility.ShouldBeFed(patient)) { return false; }
            if (!pawn.CanReserve(t)) { return false; }
            float mass = gene.MassDesired;
            var candidate = JobGiver_GetMetalicResource.GetMetalsForMass(pawn, mass, out var count);
            if (candidate == null) 
            {
                JobFailReason.Is("NoIngredient".Translate(ThingDefOf.Steel));
                return false;                 
            }
            return true;
        }
        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            Pawn patient = t as Pawn;
            var gene = patient.genes?.GetFirstGeneOfType<Gene_Resource_Metal>();
            float mass = gene.MassDesired;
            var candidate = JobGiver_GetMetalicResource.GetMetalsForMass(pawn, mass, out var count);
            if(candidate != null)
            {
                var job = JobMaker.MakeJob(InternalDefOf.AG_FeedMetal,candidate,patient);
                job.count = count;
                return job;
            }            
            return null;
        }
    }
}
