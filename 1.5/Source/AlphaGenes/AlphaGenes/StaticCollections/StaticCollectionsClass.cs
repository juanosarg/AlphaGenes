
using Verse;
using System;
using RimWorld;
using System.Collections.Generic;
using System.Linq;


namespace AlphaGenes
{

    public static class StaticCollectionsClass
    {

       
        // Number of colonists with the Ocular Affinity gene currently on the world 
        public static int ocular_gene_colonists_inWorld;

        public static Dictionary<Pawn, int> colonist_and_random_mood = new Dictionary<Pawn, int>();

        public static void AddColonistAndRandomMood(Pawn pawn, int mood)
        {
            if (pawn != null)
            {

                colonist_and_random_mood[pawn] = mood;
            }


        }



    }
}
