
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;
namespace AlphaGenes
{
    public class CompAbilityEffect_SelfSkip : CompAbilityEffect_Teleport
    {

        public new CompProperties_AbilityTeleport Props => (CompProperties_AbilityTeleport)props;

        public override string ExtraLabelMouseAttachment(LocalTargetInfo target)
        {
            return CanSkipTarget(target).Reason;
        }

        private AcceptanceReport CanSkipTarget(LocalTargetInfo target)
        {
            Pawn pawn;
            if ((pawn = target.Thing as Pawn) != null)
            {
               
                
                if (parent.pawn != pawn)
                {
                    return "AG_NeedToTargetCaster".Translate();
                }
            }
            return true;
        }

        public override bool Valid(LocalTargetInfo target, bool showMessages = true)
        {
            Pawn pawn;
            if ((pawn = target.Thing as Pawn) != null)
            {


                if (parent.pawn != pawn)
                {
                    Messages.Message("AG_NeedToTargetCaster".Translate(), pawn, MessageTypeDefOf.RejectInput, historical: false);
                    return false;
                }
            }
            return true;

        }


        }
}