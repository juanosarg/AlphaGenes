using HarmonyLib;
using RimWorld.Planet;
using Verse;

namespace AlphaGenes
{
    [HarmonyPatch(typeof(MapParent), nameof(MapParent.CheckRemoveMapNow))]
    public static class Patch_MapParent_CheckRemoveMapNow
    {
        [HarmonyPrefix]
        public static bool Prefix(MapParent __instance)
        {
            if (__instance.HasMap && WorldComponent_PocketPlaneAnchor.Instance?.IsAnchored(__instance.Map) == true)
            {
                // Block CheckRemoveMapNow completely for anchored maps
                return false;
            }
            return true;
        }
    }
}