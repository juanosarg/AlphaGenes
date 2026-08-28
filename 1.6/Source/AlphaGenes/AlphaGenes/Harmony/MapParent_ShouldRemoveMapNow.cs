using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System;
using Verse;

namespace AlphaGenes
{
    // Intercept the single choke point all map removal goes through
    [HarmonyPatch(typeof(Game), "DeinitAndRemoveMap")]
    public static class AlphaGenes_Game_DeinitAndRemoveMap_Patch
    {
        [HarmonyPrefix]
        public static bool PreventAnchoredMapRemoval(Map map)
        {
            if (map == null) return true;

            bool isAnchored = WorldComponent_PocketPlaneAnchor.Instance?.IsAnchored(map) == true;
            Log.Message($"[AlphaGenes] DeinitAndRemoveMap called for '{map}', isAnchored={isAnchored}\n{Environment.StackTrace}");

            if (isAnchored)
            {
                Log.Message($"[AlphaGenes] BLOCKED DeinitAndRemoveMap for anchored map '{map}'");
                return false;
            }
            return true;
        }
    }

    // Site covers most temporary maps (raids, quests, ancient dangers, etc.)
    [HarmonyPatch(typeof(Site), "ShouldRemoveMapNow")]
    public static class AlphaGenes_Site_ShouldRemoveMapNow_Patch
    {
        [HarmonyPostfix]
        public static void PreventAnchoredMapRemoval(MapParent __instance, ref bool __result)
        {
            if (__result && __instance.HasMap)
            {
                bool isAnchored = WorldComponent_PocketPlaneAnchor.Instance?.IsAnchored(__instance.Map) == true;
                Log.Message($"[AlphaGenes] Site.ShouldRemoveMapNow '{__instance.Map}': result was {__result}, isAnchored={isAnchored}");
                if (isAnchored)
                {
                    __result = false;
                    Log.Message($"[AlphaGenes] Blocked removal of anchored Site map '{__instance.Map}'");
                }
            }
        }
    }

    // Camp covers player-created camps
    [HarmonyPatch(typeof(Camp), "ShouldRemoveMapNow")]
    public static class AlphaGenes_Camp_ShouldRemoveMapNow_Patch
    {
        [HarmonyPostfix]
        public static void PreventAnchoredMapRemoval(MapParent __instance, ref bool __result)
        {
            if (__result && __instance.HasMap)
            {
                bool isAnchored = WorldComponent_PocketPlaneAnchor.Instance?.IsAnchored(__instance.Map) == true;
                Log.Message($"[AlphaGenes] Camp.ShouldRemoveMapNow '{__instance.Map}': result was {__result}, isAnchored={isAnchored}");
                if (isAnchored)
                {
                    __result = false;
                    Log.Message($"[AlphaGenes] Blocked removal of anchored Camp map '{__instance.Map}'");
                }
            }
        }
    }
}
