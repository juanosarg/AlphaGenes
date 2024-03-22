

using AlphaGenes;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AlphaGenes
{

    [HarmonyPatch(typeof(MeditationFocusTypeAvailabilityCache), "PawnCanUseInt")]
    public static class AlphaGenes_MeditationFocusTypeAvailabilityCache_PawnCanUse_Patch
    {
        public static void Postfix(Pawn p, MeditationFocusDef type, ref bool __result)
        {
            if (type == MeditationFocusDefOf.Natural)
            {
                
                    if (DefDatabase<GeneDef>.GetNamedSilentFail("AG_OcularAffinity") != null && p.HasActiveGene(DefDatabase<GeneDef>.GetNamedSilentFail("AG_OcularAffinity")))
                    {
                        __result = true;
                    }

               

            }


        }
    }
}