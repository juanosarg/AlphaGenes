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

    [HarmonyPatch(typeof(Pawn_MechanitorTracker))]
    [HarmonyPatch("CanCommandTo")]
    public static class AlphaGenes_Pawn_MechanitorTracker_CanCommandTo_Patch
    {



        [HarmonyPostfix]
        public static void ModifyRange(ref bool __result, LocalTargetInfo target, Pawn_MechanitorTracker __instance,Pawn ___pawn)

        {

            if (___pawn.health?.hediffSet?.HasHediff(InternalDefOf.AG_IncreasedCommandRange) == true) { 
            
                __result = (float)___pawn.Position.DistanceToSquared(target.Cell) < 1225f;

            }
            if (___pawn.health?.hediffSet?.HasHediff(InternalDefOf.AG_DecreasedCommandRange) == true)
            {

                __result = (float)___pawn.Position.DistanceToSquared(target.Cell) < 225f;

            }


        }


    }
}
