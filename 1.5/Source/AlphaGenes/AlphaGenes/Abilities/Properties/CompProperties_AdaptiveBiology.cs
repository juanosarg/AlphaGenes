using RimWorld;
namespace AlphaGenes
{
    public class CompProperties_AdaptiveBiology : CompProperties_AbilityEffect
    {
        public int numberOfTraits = 1;

        public CompProperties_AdaptiveBiology()
        {
            compClass = typeof(CompAbilityEffect_AdaptiveBiology);
        }
    }
}
