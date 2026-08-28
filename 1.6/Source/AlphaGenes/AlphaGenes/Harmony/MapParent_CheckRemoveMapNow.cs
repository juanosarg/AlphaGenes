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
            if (__instance.HasMap && WorldComponent_PocketPlaneAnchor.Instance?.IsAnchored(__instance.Map) == true)
            {
                return false;
            }
            return true;
        }
    }
}
