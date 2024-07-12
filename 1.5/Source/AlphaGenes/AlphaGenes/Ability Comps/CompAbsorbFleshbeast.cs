
using Verse;
using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using System.Linq;




namespace AlphaGenes
{
    class CompAbsorbFleshbeast : CompAbilityEffect
    {

        new public CompProperties_AbsorbFleshbeast Props
        {
            get
            {
                return (CompProperties_AbsorbFleshbeast)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn pawn = target.Thing as Pawn;

            if (!FleshbeastUtility.IsFleshBeast(pawn.kindDef))
            {
                Messages.Message("AG_OnlyOnFleshbeasts".Translate(pawn.LabelCap), pawn, MessageTypeDefOf.RejectInput, historical: false);


            }else if (pawn.RaceProps.baseBodySize > 4.5f)
            {
                Messages.Message("AG_FleshbeastTooBig".Translate(pawn.LabelCap), pawn, MessageTypeDefOf.RejectInput, historical: false);


            }
            else
            {
                FleshbeastUtility.MeatSplatter(2, pawn.PositionHeld, pawn.MapHeld, FleshbeastUtility.ExplosionSizeFor(pawn));
                FilthMaker.TryMakeFilth(pawn.PositionHeld, pawn.MapHeld, ThingDefOf.Filth_TwistedFlesh);
                Hediff hediff = this.parent.pawn.health.AddHediff(InternalDefOf.AG_FleshPower);
                hediff.Severity = 1;

                for(int i= 0; i < 10; i++)
                {
                    List<Hediff_Injury> resultHediffs = new List<Hediff_Injury>();
                    this.parent.pawn.health.hediffSet.GetHediffs(ref resultHediffs, (Hediff_Injury x) => x.CanHealNaturally() || x.CanHealFromTending());
                    if (resultHediffs.TryRandomElement(out var result))
                    {
                        result.Heal(10f);
                    }
                }
                

                pawn.Destroy();

            }

        }






    }
}
