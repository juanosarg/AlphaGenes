using RimWorld;
using Verse;

namespace AlphaGenes
{


    public class CompProperties_TeratogenicHealing : CompProperties_AbilityEffect
    {

        public bool targetOther=false;


        public CompProperties_TeratogenicHealing()
        {
            this.compClass = typeof(CompTeratogenicHealing);
        }
    }
}