using HarmonyLib;
using RimWorld;
using Verse;

namespace AlphaGenes
{
    [HarmonyPatch(typeof(MapPawns), nameof(MapPawns.AnyPawnBlockingMapRemoval), MethodType.Getter)]
    public static class AlphaGenes_MapPawns_AnyPawnBlockingMapRemoval_Patch
    {
        [HarmonyPostfix]
        public static void PreventAnchoredMapRemoval(ref bool __result, Map ___map)
        {
            if (!__result && WorldComponent_PocketPlaneAnchor.Instance?.IsAnchored(___map) == true)
            {
                __result = true;
            }
        }
    }
}
