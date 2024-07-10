using RimWorld;
using Verse;

namespace AlphaGenes
{

    public class CompProperties_DestroyPocketPlane : CompProperties_AbilityEffect
    {

        public CompProperties_DestroyPocketPlane()
        {
            this.compClass = typeof(CompDestroyPocketPlane);
        }
    }
}