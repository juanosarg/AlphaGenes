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

	[HarmonyPatch(typeof(Pawn_HealthTracker))]
	[HarmonyPatch("MakeDowned")]
	public static class AlphaGenes_Pawn_HealthTracker_MakeDowned_Patch
	{

	

		[HarmonyPostfix]
        public static void BreakSomeBones(Pawn ___pawn)

        {

            if (___pawn?.DevelopmentalStage==DevelopmentalStage.Adult && ___pawn?.HasActiveGene(InternalDefOf.AG_BrittleBones)==true)
            {
				
				for(int i = 0; i < 3; i++)
				{
					___pawn.TakeDamage(new DamageInfo(DamageDefOf.Blunt, 10, 0f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown));
				}

			}

		}

		
	}
}
