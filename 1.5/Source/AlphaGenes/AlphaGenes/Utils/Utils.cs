
using RimWorld;
using System.Collections.Generic;
using Verse;
namespace AlphaGenes
{
    public static class Utils 
    {
        public static bool HasActiveGene(this Pawn pawn, GeneDef geneDef)
        {
            if (geneDef is null) return false;
            if (pawn.genes is null) return false;
            return pawn.genes.GetGene(geneDef)?.Active ?? false;
        }

        public static string ReturnGenePawnHasFromList(this Pawn pawn, List<string> geneDefs)
        {
            if (pawn.genes is null) return "";

            foreach (string stringGeneDef in geneDefs) {
            
                GeneDef geneDef = DefDatabase<GeneDef>.GetNamedSilentFail(stringGeneDef);             
                    if (pawn.HasActiveGene(geneDef))
                    {
                  
                    return geneDef.defName;
                    }               
            }
            return "";        
        }

    }
}