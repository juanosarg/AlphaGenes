using System.Collections.Generic;
using System;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI.Group;



namespace AlphaGenes
{
    public class HediffComp_DeleteAfterTime : HediffComp
    {

        public HediffCompProperties_DeleteAfterTime Props => (HediffCompProperties_DeleteAfterTime)props;

        public int timer;

        public override void CompExposeData()
        {
            Scribe_Values.Look(ref timer, "timer", 0);

        }
        public override void CompPostTick(ref float severityAdjustment)
        {
            timer++;
            if (timer > Props.disappearsAfterTicks)
            {
                this.parent.pawn.health.RemoveHediff(parent);
                if (Props.justDeletePawn)
                {
                    this.parent.pawn.Destroy();
                }

                if (Props.revertToMechanoid)
                {
                    parent.pawn.GetOverseer()?.relations.RemoveDirectRelation(PawnRelationDefOf.Overseer, parent.pawn);
                    parent.pawn.SetFaction(Faction.OfMechanoids);
                    if (parent.pawn.Map != null)
                    {
                        Lord lord = null;
                        if (parent.pawn.Map.mapPawns.SpawnedPawnsInFaction(Faction.OfMechanoids).Any((Pawn p) => p != parent.pawn))
                        {
                            lord = ((Pawn)GenClosest.ClosestThing_Global(parent.pawn.Position, parent.pawn.Map.mapPawns.SpawnedPawnsInFaction(Faction.OfMechanoids),
                                99999f, (Thing p) => p != parent.pawn && ((Pawn)p).GetLord() != null, null)).GetLord();
                        }
                        if (lord == null)
                        {
                            LordJob_DefendPoint lordJob = new LordJob_DefendPoint(parent.pawn.Position, null, false, true);
                            lord = LordMaker.MakeNewLord(Faction.OfMechanoids, lordJob, parent.pawn.Map, null);
                        }
                        lord.AddPawn(parent.pawn);

                    }



                }

            }


        }

        public override void Notify_PawnDied()
        {
            if (Props.revertToMechanoid)
            {
                parent.pawn.SetFaction(Faction.OfMechanoids);
            }
        }



        public override string CompLabelInBracketsExtra
        {
            get
            {

                return (Props.disappearsAfterTicks - timer).ToStringTicksToPeriod(allowSeconds: true, shortForm: true, canUseDecimals: true, allowYears: true, true);
            }
        }


    }
}
