using HarmonyLib;
using Mono.Cecil.Cil;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using Verse.AI;
using static HarmonyLib.Code;

namespace AlphaGenes
{
    [HarmonyPatch(typeof(WealthWatcher), "ForceRecount")]
    public static class AlphaGenes_WealthWatcher_ForceRecount_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var codes = codeInstructions.ToList();
            var check = AccessTools.Method(typeof(AlphaGenes_WealthWatcher_ForceRecount_Patch), "GeneNumberModifier");
            var field = AccessTools.Field(typeof(WealthWatcher), "wealthPawns");
 

           

            for (var i = 0; i < codes.Count; i++)
            {
                yield return codes[i];
                if (i > 0 && codes[i].opcode == OpCodes.Stloc_S && codes[i].operand is LocalBuilder lb && lb.LocalIndex == 5 && 
                    codes[i - 1].opcode == OpCodes.Callvirt)
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_S,4);
                    yield return new CodeInstruction(OpCodes.Call, check);                 
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 5);
                    yield return new CodeInstruction(OpCodes.Mul);
                    yield return new CodeInstruction(OpCodes.Stloc_S, 5);
                }
            }
        }
       

        public static float GeneNumberModifier(Pawn pawn)
        {
            if (pawn.HasActiveGene(InternalDefOf.AG_NoRaidPresence))
            {
                return 0f;
            }else if (pawn.HasActiveGene(InternalDefOf.AG_ReducedRaidPresence))
            {
                return 0.5f;
            }
            else if (pawn.HasActiveGene(InternalDefOf.AG_DangerousRaidPresence))
            {
                return 1.5f;
            }
            else if (pawn.HasActiveGene(InternalDefOf.AG_DeadlyRaidPresence))
            {
                return 4f;
            }
            return 1f;
        }


        


    }
}
