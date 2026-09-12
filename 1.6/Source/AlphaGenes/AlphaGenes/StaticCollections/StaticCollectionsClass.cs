using Verse;
using System.Collections.Generic;
using System.Linq;

namespace AlphaGenes
{
    [StaticConstructorOnStartup]
    public static class StaticCollectionsClass
    {

        // Number of colonists with the Ocular Affinity gene currently on the world. Unused now
        public static int ocular_gene_colonists_inWorld;

        public static Dictionary<Pawn, int> colonist_and_random_mood = new Dictionary<Pawn, int>();
        public static Dictionary<HediffDef, float> hediffs_and_health_modifiers = new Dictionary<HediffDef, float>();
        public static HashSet<Pawn> pawnsToRise = new HashSet<Pawn>();
        public static HashSet<ShamblerGeneLists> shamblerGeneLists = new HashSet<ShamblerGeneLists>();

        static StaticCollectionsClass()
        {

            List<HediffDef> allHediffs = DefDatabase<HediffDef>.AllDefsListForReading.Where(x => x.HasModExtension<HealthModifierExtension>()).ToList();
            foreach (HediffDef hediff in allHediffs)
            {
                HealthModifierExtension extension = hediff.GetModExtension<HealthModifierExtension>();
                hediffs_and_health_modifiers.Add(hediff, extension.healthPointToAdd);
            }

            HashSet<ShamblerGeneBlacklistsDef> allShamblerlists = DefDatabase<ShamblerGeneBlacklistsDef>.AllDefsListForReading.ToHashSet();
            foreach (ShamblerGeneBlacklistsDef individualList in allShamblerlists)
            {
                foreach (ShamblerGeneLists list in individualList.shamblerGenelists)
                {
                    shamblerGeneLists.Add(list);
                }
            }
        }

        public static void AddColonistAndRandomMood(Pawn pawn, int mood)
        {
            if (pawn != null)
            {

                colonist_and_random_mood[pawn] = mood;
            }
        }
    }
}
