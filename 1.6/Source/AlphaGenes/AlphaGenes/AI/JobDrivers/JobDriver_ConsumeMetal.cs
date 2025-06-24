using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace AlphaGenes
{
    public class JobDriver_ConsumeMetal : JobDriver
    {
        public Thing ToConsume => job.GetTarget(TargetIndex.A).Thing;
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(ToConsume, job, stackCount: job.count);
        }
        protected override IEnumerable<Toil> MakeNewToils()
        {
            Toil chew = ToilMaker.MakeToil("ChewMetal");
            chew.initAction = () =>
            {
                //Fairly sure I need this cause closure
                Thing toEat = ToConsume;
                Pawn actor = pawn;
                actor.jobs.curDriver.ticksLeftThisToil = Mathf.RoundToInt(baseConsumeTicks * durationMultiplier(actor));
                if (toEat.Spawned)
                {
                    toEat.Map.physicalInteractionReservationManager.Reserve(actor, actor.CurJob, toEat);
                }
            };
            chew.tickAction = () =>
            {
                Thing toEat = ToConsume;
                if (toEat.Spawned)
                {
                    chew.actor.rotationTracker.FaceCell(toEat.Position);
                }
                if(TargetIndex.B != TargetIndex.None && chew.actor.CurJob.GetTarget(TargetIndex.B).IsValid)
                {
                    chew.actor.rotationTracker.FaceCell(chew.actor.CurJob.GetTarget(TargetIndex.B).Cell);
                }
                chew.actor.GainComfortFromCellIfPossible(1);                
            };
            chew.WithProgressBar(TargetIndex.A, () =>
            {
                Thing toEat = ToConsume;
                if(toEat == null) { return 1f; }
                return 1f - chew.actor.jobs.curDriver.ticksLeftThisToil / Mathf.Round(baseConsumeTicks * durationMultiplier(chew.actor));
            });
            chew.defaultCompleteMode = ToilCompleteMode.Delay;
            chew.handlingFacing = true;
            chew.FailOnDestroyedOrNull(TargetIndex.A);
            chew.AddFinishAction(() =>
            {
                var actor = chew.actor;
                if (actor.CurJob == null)
                {
                    return;
                }
                Thing toEat = ToConsume;
                if(toEat == null)
                {
                    return;
                }
                if (actor.Map.physicalInteractionReservationManager.IsReservedBy(actor, toEat))
                {
                    actor.Map.physicalInteractionReservationManager.Release(actor, actor.CurJob, toEat);
                }
            });
            chew.WithEffect(DefDatabase<EffecterDef>.GetNamed("EatVegetarian"), TargetIndex.A);
            chew.PlaySustainerOrSound(DefDatabase<SoundDef>.GetNamed("Meal_Eat"));
            Toil goToPickup = Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch);
            yield return goToPickup;
            yield return Toils_Ingest.PickupIngestible(TargetIndex.A, pawn);//Pretty sure I can get away with not rewriting this. Though pretty sure haul would do same thing
            yield return CarryIngestibleToChewSpot(pawn, TargetIndex.A); 
            yield return Toils_Ingest.FindAdjacentEatSurface(TargetIndex.B, TargetIndex.A);
            yield return chew;
            Toil finalize = ToilMaker.MakeToil("AteMetal");
            finalize.initAction = () =>
            {
                var actor = finalize.actor;
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
        public static Toil ChewMetal(Pawn pawn, float durationMulti, TargetIndex ingestibleInd)
        {
            
            Toil chew = ToilMaker.MakeToil("ChewMetal");
            Thing ToConsume = chew.actor.CurJob.GetTarget(ingestibleInd).Thing;
            chew.initAction = () =>
            {
                //Fairly sure I need this cause closure
                Thing toEat = ToConsume;
                Pawn actor = chew.actor;
                actor.jobs.curDriver.ticksLeftThisToil = Mathf.RoundToInt(baseConsumeTicks * durationMulti);
                if (toEat.Spawned)
                {
                    toEat.Map.physicalInteractionReservationManager.Reserve(pawn, actor.CurJob, toEat);
                }
            };
            chew.tickAction = () =>
            {
                Thing toEat = ToConsume;
                if(pawn != chew.actor)
                {
                    chew.actor.rotationTracker.FaceCell(pawn.Position);
                }
                if (toEat.Spawned)
                {
                    chew.actor.rotationTracker.FaceCell(toEat.Position);
                }
                if (TargetIndex.B != TargetIndex.None && chew.actor.CurJob.GetTarget(TargetIndex.B).IsValid)
                {
                    chew.actor.rotationTracker.FaceCell(chew.actor.CurJob.GetTarget(TargetIndex.B).Cell);
                }
                chew.actor.GainComfortFromCellIfPossible(1);
            };
            chew.WithProgressBar(TargetIndex.A, () =>
            {
                Thing toEat = ToConsume;
                if (toEat == null) { return 1f; }
                return 1f - chew.actor.jobs.curDriver.ticksLeftThisToil / Mathf.Round(baseConsumeTicks * durationMulti);
            });
            chew.defaultCompleteMode = ToilCompleteMode.Delay;
            chew.handlingFacing = true;
            chew.FailOnDestroyedOrNull(TargetIndex.A);
            chew.AddFinishAction(() =>
            {
                var actor = pawn;
                if (actor.CurJob == null)
                {
                    return;
                }
                Thing toEat = ToConsume;
                if (toEat == null)
                {
                    return;
                }
                if (actor.Map.physicalInteractionReservationManager.IsReservedBy(actor, toEat))
                {
                    actor.Map.physicalInteractionReservationManager.Release(actor, actor.CurJob, toEat);
                }
            });
            chew.WithEffect(DefDatabase<EffecterDef>.GetNamed("EatVegetarian"), TargetIndex.A);
            chew.PlaySustainerOrSound(DefDatabase<SoundDef>.GetNamed("Meal_Eat"));
            return chew;
        }
        public override bool ModifyCarriedThingDrawPos(ref Vector3 drawPos, ref bool flip)
        {
            var tableCell = job.GetTarget(TargetIndex.B).Cell;
            if (pawn.pather.Moving)
            {
                return false;
            }
            if(pawn.carryTracker.CarriedThing == null)
            {
                return false;
            }
            if(tableCell.IsValid && tableCell.AdjacentToCardinal(pawn.Position) && tableCell.HasEatSurface(pawn.Map))
            {
                drawPos = new Vector3(tableCell.x + 0.5f, drawPos.y, tableCell.z + 0.5f);
                return true;
            }
            return base.ModifyCarriedThingDrawPos(ref drawPos, ref flip);
        }
        //I'm passed doing it clean. Copy paste from DnSpy with slight change
        private Toil CarryIngestibleToChewSpot(Pawn pawn, TargetIndex ingestibleInd)
        {
            Toil toil = ToilMaker.MakeToil("CarryIngestibleToChewSpot");
            toil.initAction = delegate ()
            {
                Pawn actor = toil.actor;
                IntVec3 intVec = IntVec3.Invalid;
                Thing thing = null;
                Thing thing2 = actor.CurJob.GetTarget(ingestibleInd).Thing;
                Predicate<Thing> baseChairValidator = delegate (Thing t)
                {
                    if (t.def.building == null || !t.def.building.isSittable)
                    {
                        return false;
                    }
                    IntVec3 a;
                    if (!Toils_Ingest.TryFindFreeSittingSpotOnThing(t, actor, out a))
                    {
                        return false;
                    }
                    if (t.IsForbidden(pawn))
                    {
                        return false;
                    }
                    if (!actor.CanReserve(t, 1, -1, null, false))
                    {
                        return false;
                    }
                    if (!t.IsSociallyProper(actor))
                    {
                        return false;
                    }
                    if (t.IsBurning())
                    {
                        return false;
                    }
                    if (t.HostileTo(pawn))
                    {
                        return false;
                    }
                    bool result = false;
                    for (int i = 0; i < 4; i++)
                    {
                        Building edifice = (a + GenAdj.CardinalDirections[i]).GetEdifice(t.Map);
                        if (edifice != null && edifice.def.surfaceType == SurfaceType.Eat)
                        {
                            result = true;
                            break;
                        }
                    }
                    return result;
                };
                //Change here to remove ingestible and set it to the default 32
                thing = GenClosest.ClosestThingReachable(actor.Position, actor.Map, ThingRequest.ForGroup(ThingRequestGroup.BuildingArtificial), PathEndMode.OnCell, TraverseParms.For(actor, Danger.Deadly, TraverseMode.ByPawn, false, false, false), 32f, (Thing t) => baseChairValidator(t) && t.Position.GetDangerFor(pawn, t.Map) == Danger.None, null, 0, -1, false, RegionType.Set_Passable, false);
                
                if (thing == null)
                {
                    intVec = RCellFinder.SpotToChewStandingNear(actor, actor.CurJob.GetTarget(ingestibleInd).Thing, (IntVec3 c) => actor.CanReserveSittableOrSpot(c, false));
                    Danger chewSpotDanger = intVec.GetDangerFor(pawn, actor.Map);
                    if (chewSpotDanger != Danger.None)
                    {
                        thing = GenClosest.ClosestThingReachable(actor.Position, actor.Map, ThingRequest.ForGroup(ThingRequestGroup.BuildingArtificial), PathEndMode.OnCell, TraverseParms.For(actor, Danger.Deadly, TraverseMode.ByPawn, false, false, false), 32f, (Thing t) => baseChairValidator(t) && t.Position.GetDangerFor(pawn, t.Map) <= chewSpotDanger, null, 0, -1, false, RegionType.Set_Passable, false);
                    }
                }
                if (thing != null && !Toils_Ingest.TryFindFreeSittingSpotOnThing(thing, actor, out intVec))
                {
                    Log.Error("Could not find sitting spot on chewing chair! This is not supposed to happen - we looked for a free spot in a previous check!");
                }
                actor.ReserveSittableOrSpot(intVec, actor.CurJob, true);
                actor.Map.pawnDestinationReservationManager.Reserve(actor, actor.CurJob, intVec);
                actor.pather.StartPath(intVec, PathEndMode.OnCell);
            };
            toil.defaultCompleteMode = ToilCompleteMode.PatherArrival;
            return toil;
        }
        private float durationMultiplier(Pawn pawn)
        {
            return 1f / pawn.GetStatValue(StatDefOf.EatingSpeed);
        }
        private const int baseConsumeTicks = 500;
    }
}
