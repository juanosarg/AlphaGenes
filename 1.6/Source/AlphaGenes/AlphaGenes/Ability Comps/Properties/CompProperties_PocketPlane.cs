using RimWorld;
using Verse;

namespace AlphaGenes
{

    public class CompProperties_PocketPlane : CompProperties_AbilityEffect
    {

        public int x;
        public int z;
        public MapGeneratorDef map;

        public CompProperties_PocketPlane()
        {
            this.compClass = typeof(CompPocketPlane);
        }
    }
}