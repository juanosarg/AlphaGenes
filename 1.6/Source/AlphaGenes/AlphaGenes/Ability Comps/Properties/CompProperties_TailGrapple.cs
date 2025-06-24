using RimWorld;
using Verse;

namespace AlphaGenes
{


    public class CompProperties_TailGrapple : CompProperties_AbilityEffect
    {
        public float maxBodySize = 2.5f;
     

        public CompProperties_TailGrapple()
        {
            this.compClass = typeof(CompTailGrapple);
        }
    }
}