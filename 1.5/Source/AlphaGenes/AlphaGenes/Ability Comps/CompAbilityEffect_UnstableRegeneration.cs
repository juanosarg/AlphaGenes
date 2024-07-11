using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace AlphaGenes
{
    public class CompAbilityEffect_UnstableRegeneration : CompAbilityEffect
    {
      

        new public CompProperties_UnstableRegeneration Props
        {
            get
            {
                return (CompProperties_UnstableRegeneration)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            
            base.Apply(target, dest);
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {
                HealthUtility.HealNonPermanentInjuriesAndRestoreLegs(pawn);
              
                FleckMaker.AttachedOverlay(pawn, FleckDefOf.FlashHollow, new Vector3(0f, 0f, 0.26f));

                pawn.ageTracker.AgeBiologicalTicks += 3600000 * 7;
                float maxAge = pawn.RaceProps.lifeExpectancy * pawn.GetStatValue(StatDefOf.LifespanFactor);

                if (pawn.ageTracker.AgeBiologicalYears > maxAge)
                {
                    pawn.Kill(null);
                }



            }
        }

        





       



    }
}