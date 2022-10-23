using HarmonyLib;
using RimWorld;
using Verse.Grammar;
using Verse;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection.Emit;
using System.Linq;
using System;
using Verse.AI;

namespace AlphaGenes
{
	//playing with performance fire patching this, just automatically doing a transpiler
	[HarmonyPatch(typeof(HaulAIUtility))]
	[HarmonyPatch("PawnCanAutomaticallyHaulFast")]

	public static class AlphaGenes_HaulAIUtility_PawnCanAutomaticallyHaulFast
	{
		public static MethodInfo myMethod = AccessTools.Method("AlphaGenes_HaulAIUtility_PawnCanAutomaticallyHaulFast:IsPrisonerReserved");
		public static MethodInfo isBurning = AccessTools.Method("FireUtility:IsBurning",new Type[] {typeof(Thing)});
		public static  IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
			List<CodeInstruction> codes = instructions.ToList();
			var jump = generator.DefineLabel();
			for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].Calls(isBurning))
                {					
					var index = i;									
					codes.Insert(index++, new CodeInstruction(OpCodes.Call, myMethod));
					codes.Insert(index++, new CodeInstruction(OpCodes.Brfalse_S, jump));
					codes.Insert(index++, new CodeInstruction(OpCodes.Ldc_I4_0));
					codes.Insert(index++, new CodeInstruction(OpCodes.Ret));
					var instr = new CodeInstruction(OpCodes.Ldarg_1);
					instr.labels.Add(jump);
					codes.Insert(index++, instr);
					break;						
                }				
			}
			foreach(var code in codes)
				yield return code; 
		}

		public static bool IsPrisonerReserved(Thing thing)
        {
            if (thing.def.IsMetal)
            {
				var room = thing.GetRoom();
				if(room?.IsPrisonCell ?? false)
                {
					foreach(var pawn in room.Owners)
                    {
                        if (pawn.genes.HasGene(InternalDefOf.AG_MetalEater))
                        {
							var gene = pawn.genes.GetFirstGeneOfType<Gene_Resource_Metal>();
                            if (gene.ShouldConsumeNow())
                            {
								return true;
                            }
                        }
                    }
                }
            }
			return false;
        }


	}
}
