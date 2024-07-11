
using Verse;
using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using System.Linq;




namespace AlphaGenes
{
    class CompDestroyEyes : CompAbilityEffect
    {

        new public CompProperties_DestroyEyes Props
        {
            get
            {
                return (CompProperties_DestroyEyes)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn pawn = target.Thing as Pawn;
            List<BodyPartRecord> eyes = (from c in pawn.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null)
                                         where c.def == BodyPartDefOf.Eye
                                         select c).ToList();


            if (eyes != null && eyes.Count > 0)
            {
                foreach (BodyPartRecord eye in eyes)
                {
                    DamageInfo damageInfo = new DamageInfo(DamageDefOf.Burn, 1000, 999f, -1f, this.parent.pawn, eye, null, DamageInfo.SourceCategory.ThingOrUnknown, null, true, true);
                    damageInfo.SetAllowDamagePropagation(false);
                    pawn.TakeDamage(damageInfo);
                }


            }

        }






    }
}
