using VanillaGenesExpanded;
using Verse;
using HarmonyLib;

namespace AlphaGenes
{
    [HarmonyPatch(typeof(PawnRenderer))]
    [HarmonyPatch("GetBodyOverlayMeshSet")]
    public static class PawnRenderer_GetBodyOverlayMeshSet
    {
        public static void Postfix(ref GraphicMeshSet __result, Pawn ___pawn)
        {
            if (!___pawn.RaceProps.Humanlike || !ModsConfig.BiotechActive)
            {
                return;
            }
            var genes = ___pawn.genes;
            var vector3 = __result.MeshAt(Rot4.North).vertices[2] * 2;
            //x and Z because trying to reverse NewPlaneMesh
            float factorX = vector3.x;
            float factorY = vector3.z;

            if (genes == null) { return; }
            foreach (var gene in genes.GenesListForReading)
            {
                var ext = gene.def.GetModExtension<GeneExtension>();
                if (ext != null && ext.bodyScaleFactor != 1f)
                {
                    factorX *= ext.bodyScaleFactor;
                    factorY *= ext.bodyScaleFactor;
                }
            }
            __result = MeshPool.GetMeshSetForWidth(factorX, factorY);


        }

    }
}
