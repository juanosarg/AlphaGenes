using System.Collections.Generic;
using RimWorld;
using Verse;


namespace AlphaGenes
{
    internal class HediffComp_NoBleedHeartAttack : HediffComp
    {
        private HediffCompProperties_NoBleedHeartAttack Props
        {
            get
            {
                return (HediffCompProperties_NoBleedHeartAttack)this.props;
            }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Find.TickManager.TicksGame % 500 == 0)
            {
                if (this.Pawn.health.hediffSet.BleedRateTotal > 0)
                {
                    foreach (Hediff h in this.Pawn.health.hediffSet.hediffs)
                    {
                        if (h.Bleeding)
                        {
                            h.Tended(0.3f, 0.3f);
                        }
                    }

                    if (Rand.RangeInclusive(0, 100) < 5)
                    {
                        List<BodyPartDef> bodyPartDefs = new List<BodyPartDef>
                        {
                            BodyPartDefOf.Heart
                        };
                        HediffGiverUtility.TryApply(this.Pawn, InternalDefOf.HeartAttack, bodyPartDefs, true);
                    }
                }
            }
        }
    }
}