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

	[HarmonyPatch(typeof(RelationsUtility))]
	[HarmonyPatch("IsDisfigured")]
	public static class AlphaGenes_RelationsUtility_IsDisfigured_Patch
	{


		[HarmonyPostfix]
		public static void RemoveDisfigurement(Pawn pawn, ref bool __result)

		{

			if (pawn?.genes?.HasGene(InternalDefOf.AG_Beauty_Angelic) == true)
			{
				__result = false;

			}



		}


	}
}
