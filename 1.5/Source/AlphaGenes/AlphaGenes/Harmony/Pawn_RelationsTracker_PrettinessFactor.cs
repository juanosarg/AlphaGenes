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

    [HarmonyPatch(typeof(Pawn_RelationsTracker))]
    [HarmonyPatch("PrettinessFactor")]
    public static class AlphaGenes_Pawn_RelationsTracker_PrettinessFactor_Patch
    {


        [HarmonyPostfix]
        public static void NoUgliness(Pawn otherPawn, Pawn ___pawn,ref float __result)

        {

            if (___pawn?.HasActiveGene(InternalDefOf.AG_Teratophilia) == true)
            {
                float num = 0f;
                if (otherPawn.RaceProps.Humanlike)
                {
                    num = otherPawn.GetStatValue(StatDefOf.PawnBeauty);
                }
                if (num < 0f)
                {
                    __result=1f;
                }

            }




        }


    }
}
