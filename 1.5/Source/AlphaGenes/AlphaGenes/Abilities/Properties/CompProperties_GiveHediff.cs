using RimWorld;
using Verse;

namespace AlphaGenes
{


    public class CompProperties_GiveHediff : CompProperties_AbilityEffect
    {

        public HediffDef hediffDef;
        public bool applyToCaster = true;
        public bool applyToRadius = false;


        public CompProperties_GiveHediff()
        {
            this.compClass = typeof(CompGiveHediff);
        }
    }
}