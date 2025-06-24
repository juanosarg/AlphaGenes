using RimWorld;
using Verse;

namespace AlphaGenes
{


    public class CompProperties_TeratogenicHediff : CompProperties_AbilityEffect
    {

        public HediffDef hediff;
        public bool targetOther = false;

        public CompProperties_TeratogenicHediff()
        {
            this.compClass = typeof(CompTeratogenicHediff);
        }
    }
}