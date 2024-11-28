using HarmonyLib;
using Mono.Cecil.Cil;
using RimWorld;
using RimWorld.QuestGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using Verse.AI;
using static HarmonyLib.Code;

namespace AlphaGenes
{
  /*  [HarmonyPatch(typeof(Building_VoidMonolith), "CanActivate")]
    public static class AlphaGenesBuilding_VoidMonolith_CanActivate_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var codes = codeInstructions.ToList();
            var check = AccessTools.Method(typeof(AlphaGenesBuilding_VoidMonolith_CanActivate_Patch), "GetNumber");
            var fieldLoaded = AccessTools.Field(typeof(MonolithLevelDef), "entityCountCompletionRequired");


            for (var i = 0; i < codes.Count; i++)
            {

                if (codes[i].opcode == OpCodes.Ldfld && codes[i].OperandIs(fieldLoaded))
                {

                    yield return new CodeInstruction(OpCodes.Call, check);
                   
                }else yield return codes[i];
            }
        }


        public static int GetNumber(MonolithLevelDef level)
        {
            return 100;
        }





    }

    [HarmonyPatch(typeof(QuestPart_MonolithPart), "UpdateDescription")]
    public static class AlphaGenesBuilding_QuestPart_MonolithPart_UpdateDescription_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var codes = codeInstructions.ToList();
            var check = AccessTools.Method(typeof(AlphaGenesBuilding_VoidMonolith_CanActivate_Patch), "GetNumber");
            var fieldLoaded = AccessTools.Field(typeof(MonolithLevelDef), "entityCountCompletionRequired");


            for (var i = 0; i < codes.Count; i++)
            {

                if (codes[i].opcode == OpCodes.Ldfld && codes[i].OperandIs(fieldLoaded))
                {

                    yield return new CodeInstruction(OpCodes.Call, check);

                }
                else yield return codes[i];
            }
        }


    




    }
  */
}
