
using AlphaGenes;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AlphaGenes
{
    [HarmonyPatch(typeof(MeditationFocusDef), nameof(MeditationFocusDef.EnablingThingsExplanation))]
    public static class AlphaGenes_MeditationFocusDef_EnablingThingsExplanation_Patch
    {
        public static void Postfix(Pawn pawn, MeditationFocusDef __instance, ref string __result)
        {
            if (__instance == MeditationFocusDefOf.Natural)
            {
               
                    if (DefDatabase<GeneDef>.GetNamedSilentFail("AG_OcularAffinity") != null && pawn.genes?.HasGene(DefDatabase<GeneDef>.GetNamedSilentFail("AG_OcularAffinity"))==true)
                    {
                        __result += "\n  - " + "VRE_UnlockedByGeneGau".Translate() + ".";
                    }

                
            }




        }
    }
}