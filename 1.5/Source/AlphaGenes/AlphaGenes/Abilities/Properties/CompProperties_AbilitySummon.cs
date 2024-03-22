using RimWorld;
namespace AlphaGenes
{
    public class CompProperties_AbilitySummon : CompProperties_AbilityEffect
    {
        public bool isMechanoid = false;

        public CompProperties_AbilitySummon()
        {
            compClass = typeof(CompAbilityEffect_Summon);
        }
    }
}
