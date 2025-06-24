using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace AlphaGenes
{
    public class JobDriver_FeedPatient : JobDriver
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
            yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.ClosestTouch);            
            yield return JobDriver_ConsumeMetal.ChewMetal(Deliveree,1.5f,TargetIndex.A).FailOnCannotTouch(TargetIndex.B,PathEndMode.Touch);
            Toil finalize = ToilMaker.MakeToil("AteMetal");
            finalize.initAction = () =>
            {
                var actor = Deliveree;
                var toEat = ToConsume;
                if(actor.needs.mood != null)
                {
                    //Cant escape the table
                    if(!(actor.Position + actor.Rotation.FacingCell).HasEatSurface(actor.Map) && actor.GetPosture()== PawnPosture.Standing)
                    {
                        actor.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.AteWithoutTable);
                    }
                    Room room = actor.GetRoom(RegionType.Set_All);
                    if(room != null)
                    {
                        int scoreStageIndex = RoomStatDefOf.Impressiveness.GetScoreStageIndex(room.GetStat(RoomStatDefOf.Impressiveness));
                        if (ThoughtDefOf.AteInImpressiveDiningRoom.stages[scoreStageIndex] != null)
                        {
                            actor.needs.mood.thoughts.memories.TryGainMemory(ThoughtMaker.MakeThought(ThoughtDefOf.AteInImpressiveDiningRoom, scoreStageIndex), null);
                        }
                    }
                }
                var gene_Metal = actor.genes.GetFirstGeneOfType<Gene_Resource_Metal>();
                gene_Metal.Notify_IngestedThing(toEat,job.count);
                actor.needs.food.CurLevel = actor.needs.food.MaxLevel;//This is because food should always be full while eating metal only dropping when 0 metal. But drops extremely fast if 0
                toEat.Destroy();
            };
            finalize.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return finalize;
            yield break;
        }
       
      
    }
}
