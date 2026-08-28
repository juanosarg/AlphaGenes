using HarmonyLib;
using RimWorld.Planet;
using Verse;

namespace AlphaGenes
{
    [HarmonyPatch(typeof(MapParent), nameof(MapParent.CheckRemoveMapNow))]
    public static class AlphaGenes_MapParent_CheckRemoveMapNow_Patch
    {
        [HarmonyPrefix]
        public static bool PreventAnchoredMapRemoval(MapParent __instance)
        {
            if (!__instance.HasMap)
                return true;

            var anchor = WorldComponent_PocketPlaneAnchor.Instance;
            if (anchor == null)
            {
                Log.Warning("[AlphaGenes] CheckRemoveMapNow: WorldComponent_PocketPlaneAnchor.Instance is NULL");
                return true;
            }

            bool isAnchored = anchor.IsAnchored(__instance.Map);
            Log.Message($"[AlphaGenes] CheckRemoveMapNow on '{__instance.Map.ToString()}' (label: {__instance.Label}): isAnchored={isAnchored}, anchoredCount={anchor.AnchoredCount}");

            if (isAnchored)
            {
                Log.Message($"[AlphaGenes] Blocking removal of anchored map '{__instance.Map}'");
                return false;
            }
            return true;
        }
    }
}
