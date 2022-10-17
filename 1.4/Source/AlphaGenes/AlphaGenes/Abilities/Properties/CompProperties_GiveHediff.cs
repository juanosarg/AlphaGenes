using RimWorld;
using Verse;

namespace AlphaGenes
{


    public class CompProperties_GiveHediff : CompProperties_AbilityEffect
    {

        public HediffDef hediffDef;


        public CompProperties_GiveHediff()
        {
            this.compClass = typeof(CompGiveHediff);
        }
    }
}