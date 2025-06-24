
using RimWorld;
using System;
using Verse;
using RimWorld.Planet;
using System.Collections.Generic;
using AlphaGenes;

namespace AlphaGenes
{
    public class CompNightlingAffinity : CompAbilityEffect
    {

        public new CompProperties_NightlingAffinity Props => (CompProperties_NightlingAffinity)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn animal = target.Pawn;
            if (animal != null)
            {
                InteractionWorker_RecruitAttempt.DoRecruit(this.parent.pawn, animal);
                DebugActionsUtility.DustPuffFrom(animal);
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
            if (pawn.def != InternalDefOf.AA_Nightling)
            {
                if (throwMessages)
                {
                    Messages.Message("AM_OnlyNightlings".Translate(), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;
            }
                       
            if (pawn.Faction != null)
            {
                if (throwMessages)
                {
                    Messages.Message("AM_OnlyWildNightlings".Translate(), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;
            }

            if (pawn.Dead)
            {
                if (throwMessages)
                {
                    Messages.Message("AM_NightlingDead".Translate(), parent.pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;
            }

            return true;


        }




    }
}