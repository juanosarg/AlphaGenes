
using RimWorld;
using System;
using Verse;
using RimWorld.Planet;
using System.Collections.Generic;
using AlphaGenes;

namespace AlphaGenes
{
    public class CompBandwidthLoop : CompAbilityEffect
    {



        public new CompProperties_BandwidthLoop Props => (CompProperties_BandwidthLoop)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn mech = target.Pawn;
            if (mech != null)
            {
                if (parent.pawn.mechanitor == null)
                {
                    Messages.Message("AM_OnlyMechanitorCanUse".Translate(mech.LabelCap), parent.pawn, MessageTypeDefOf.RejectInput, historical: false);
                    return;
                }
                if (!mech.IsColonyMech)
                {
                    Messages.Message("AG_OnlyOnFriendlyMechs".Translate(mech.LabelCap), mech, MessageTypeDefOf.RejectInput, historical: false);
                    return;
                }

                float statValue = mech.GetStatValue(StatDefOf.BandwidthCost);
                GenExplosion.DoExplosion(mech.Position, mech.Map, statValue, DamageDefOf.Bomb, parent.pawn);
                mech.Kill(null);

            }


        }




        public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
        {
            return Valid(target);
        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            Pawn pawn = target.Pawn;
            if (pawn == null)
            {
                return false;
            }
            if (!pawn.RaceProps.IsMechanoid)
            {
                if (throwMessages)
                {
                    Messages.Message("AM_CantUseOnNonMechs".Translate(), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;
            }

                   
            if (!MechanitorUtility.IsMechanitor(parent.pawn))
            {
                if (throwMessages)
                {
                    Messages.Message("AM_OnlyMechanitorCanUse".Translate(), parent.pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;
            }

            return true;


        }




    }
}