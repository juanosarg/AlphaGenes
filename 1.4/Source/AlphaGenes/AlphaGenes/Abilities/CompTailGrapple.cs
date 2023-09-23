using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace AlphaGenes
{
    public class CompTailGrapple : CompAbilityEffect
    {
        
        private new CompProperties_TailGrapple Props => (CompProperties_TailGrapple)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {
                if (pawn.BodySize > 2.5)
                {
                    Messages.Message("AG_TargetTooBig".Translate(pawn), pawn, MessageTypeDefOf.RejectInput, historical: false);

                }
                else { pawn.stances.stunner.StunFor(600, parent.pawn, addBattleLog: false, showMote: true); }
                
            }
        }
    }
}