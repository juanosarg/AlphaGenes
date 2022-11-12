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

	[HarmonyPatch(typeof(JobDriver_Vomit))]
	[HarmonyPatch("MakeNewToils")]
	public static class AlphaGenes_JobDriver_Vomit_MakeNewToils_Patch
	{


		[HarmonyPostfix]
        public static void DamageAfterPuking(JobDriver_Vomit __instance)

        {
		
			if (__instance.pawn?.genes?.HasGene(InternalDefOf.AG_BloodVomit) == true)
			{
				for (int i = 0; i < 3; i++)
				{
					__instance.pawn.TakeDamage(new DamageInfo(DamageDefOf.Cut, 1, 1f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown));
				}

			}

			if (__instance.pawn?.genes?.HasGene(InternalDefOf.AG_RatVomit) == true)
			{
				__instance.pawn.needs?.mood?.thoughts.memories.TryGainMemory(InternalDefOf.AG_PukedRats);

			}


		}

	
	}
}
