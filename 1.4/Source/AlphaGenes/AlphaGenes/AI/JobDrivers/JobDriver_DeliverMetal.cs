using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace AlphaGenes
{
    public class JobDriver_DeliverMetal : JobDriver
    {

        public Thing ToConsume => job.GetTarget(TargetIndex.A).Thing;
        public Pawn Deliveree => job.targetB.Pawn;
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(ToConsume, job, stackCount: job.count) && pawn.Reserve(Deliveree, job);
        }
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(TargetIndex.B);
            Toil goToPickup = Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch);
            yield return goToPickup;
            yield return Toils_Ingest.PickupIngestible(TargetIndex.A, Deliveree);
            yield return Toils_Goto.GotoCell(TargetIndex.C, PathEndMode.ClosestTouch);            
           
            yield break;
        }

    }
}
