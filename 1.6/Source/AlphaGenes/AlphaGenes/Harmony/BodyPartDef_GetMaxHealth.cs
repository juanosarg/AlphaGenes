using HarmonyLib;
using UnityEngine;
using Verse;

namespace AlphaGenes
{
    [HarmonyPatch(typeof(BodyPartDef), "GetMaxHealth", MethodType.Normal)]
    internal class AlphaGenes_BodyPartDef_GetMaxHealth
    {
        [HarmonyPostfix]
        private static void PostFix(BodyPartDef __instance, ref float __result, Pawn pawn)
        {
            foreach (Hediff hediff in pawn.health.hediffSet.hediffs)
            {
                if (hediff?.Part?.def == __instance &&  StaticCollectionsClass.hediffs_and_health_modifiers.ContainsKey(hediff.def))
                {
                    __result += StaticCollectionsClass.hediffs_and_health_modifiers[hediff.def];
                }
            }
        }
    }
}