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
                if (hediff?.Part?.def == __instance && HediffUtility.TryGetComp<HediffComp_HealthModifier>(hediff) != null)
                {
                    __result += HediffUtility.TryGetComp<HediffComp_HealthModifier>(hediff).Props.healthPointToAdd;
                }
            }
        }
    }
}