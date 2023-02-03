using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AlphaGenes
{
    public class CompProperties_AbilityOcularConversion : CompProperties_AbilityEffect
    {

        public List<string> plantList = new List<string>();

        public CompProperties_AbilityOcularConversion()
        {
            compClass = typeof(CompAbilityOcularConversion);
        }
    }
}
