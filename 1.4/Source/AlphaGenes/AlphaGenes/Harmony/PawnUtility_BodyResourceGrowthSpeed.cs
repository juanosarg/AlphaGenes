using HarmonyLib;
using RimWorld;
using Verse.Grammar;
using Verse;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Verse.AI;

namespace AlphaGenes
{

	[HarmonyPatch(typeof(PawnUtility))]
	[HarmonyPatch("BodyResourceGrowthSpeed")]
	public static class AlphaGenes_PawnUtility_BodyResourceGrowthSpeed_Patch
	{



		[HarmonyPostfix]
		public static void MultiplyPregnancy(ref float __result, Pawn pawn)

		{

			if (pawn?.genes?.HasGene(InternalDefOf.AG_SlowGestation) == true)
			{
				__result = __result * 0.75f;
			}else if (pawn?.genes?.HasGene(InternalDefOf.AG_FastGestation) == true)
			{
				__result = __result * 1.25f;
			}

		}


	}
}
