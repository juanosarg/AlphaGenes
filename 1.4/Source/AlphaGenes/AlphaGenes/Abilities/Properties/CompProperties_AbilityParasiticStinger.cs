using RimWorld;
using Verse;

namespace AlphaGenes
{
    public class CompProperties_AbilityParasiticStinger : CompProperties_AbilityEffect
    {

        public HediffDef hediffDef;


        public CompProperties_AbilityParasiticStinger()
        {
            compClass = typeof(CompAbilityEffect_ParasiticStinger);
        }
    }
}
