
using RimWorld;
using Verse;
namespace AlphaGenes
{
    public static class Utils 
    {
        public static bool HasActiveGene(this Pawn pawn, GeneDef geneDef)
        {
            if (pawn.genes is null) return false;
            return pawn.genes.GetGene(geneDef)?.Active ?? false;
        }
    }
}