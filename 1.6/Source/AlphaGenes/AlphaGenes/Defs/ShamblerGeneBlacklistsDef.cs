
using System;
using System.Collections.Generic;
using RimWorld;
using Verse;


namespace AlphaGenes
{
    public class ShamblerGeneBlacklistsDef : Def
    {
        public List<ShamblerGeneLists> shamblerGenelists;
       
    }

    public class ShamblerGeneLists
    {       
        public PawnKindDef pawnKindDef;
        public float geneChance = 0.33f;
        public IntRange genesToAdd = new IntRange(1, 1);
        public List<GeneDef> shamblerGeneBlacklist;
    }
}
