using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;




namespace AlphaGenes
{


    [HarmonyPatch(typeof(GeneUIUtility))]
    [HarmonyPatch("DrawGeneBasics")]
    public static class AlphaGenes_GeneUIUtility_DrawGeneBasics_Patch
    {

        public static readonly CachedTexture GeneBackground_Changed = new CachedTexture("UI/Icons/Genes/Gene_XenogermReimplanter");
        public static readonly CachedTexture GeneBackground_ChangedTwo = new CachedTexture("UI/Icons/Genes/Gene_Ugly");


        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {

            var loadsField = AccessTools.Field(typeof(GeneUIUtility), "GeneBackground_Endogene");

            var loadsFieldTwo = AccessTools.Field(typeof(GeneUIUtility), "GeneBackground_Xenogene");

            var codes = instructions.ToList();

            for (var i = 0; i < codes.Count; i++)
            {

                var code = codes[i];
                if (codes[i].opcode == OpCodes.Ldsfld && codes[i].LoadsField(loadsField))
                {

                    yield return new CodeInstruction(OpCodes.Ldsfld, typeof(AlphaGenes_GeneUIUtility_DrawGeneBasics_Patch).GetField("GeneBackground_Changed")).MoveLabelsFrom(codes[i]) ;
                  
                }
                else if (codes[i].opcode == OpCodes.Ldsfld && codes[i].LoadsField(loadsFieldTwo))
                {
                    yield return new CodeInstruction(OpCodes.Ldsfld, typeof(AlphaGenes_GeneUIUtility_DrawGeneBasics_Patch).GetField("GeneBackground_ChangedTwo")).MoveLabelsFrom(codes[i]);

                }

                else
                {
                    yield return code;
                }


            }


        }
    }
}