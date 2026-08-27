using HarmonyLib;
using RimWorld;
using Verse;

namespace AlphaGenes
{
    [HarmonyPatch(typeof(MapPawns))]
    [HarmonyPatch("AnyPawnBlockingMapRemoval", MethodType.Getter)]
    public static class AlphaGenes_MapPawns_AnyPawnBlockingMapRemoval_Patch
    {
        [HarmonyPostfix]
        public static void PreventAnchoredMapRemoval(MapPawns __instance, ref bool __result)
        {
            if (!__result && WorldComponent_PocketPlaneAnchor.Instance?.IsAnchored(__instance.map) == true)
            {
                __result = true;
            }
        }
    }
}
