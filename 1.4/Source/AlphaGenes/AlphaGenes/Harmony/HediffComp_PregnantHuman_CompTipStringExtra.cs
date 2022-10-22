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

	[HarmonyPatch(typeof(HediffComp_PregnantHuman))]
	[HarmonyPatch("CompTipStringExtra", MethodType.Getter)]
	public static class AlphaGenes_HediffComp_PregnantHuman_CompTipStringExtra_Patch
	{



		[HarmonyPostfix]
		public static void AddGeneMultiplierExplanation(HediffWithComps ___parent,ref string __result)

		{

			if (___parent.pawn?.genes?.HasGene(InternalDefOf.AG_SlowGestation) == true)
			{
				__result = __result + "\n"+"AG_PregnancySlower".Translate();
			}
			else if (___parent.pawn?.genes?.HasGene(InternalDefOf.AG_FastGestation) == true)
			{
				__result = __result + "\n" + "AG_PregnancyFaster".Translate();
			}

		}


	}
}
