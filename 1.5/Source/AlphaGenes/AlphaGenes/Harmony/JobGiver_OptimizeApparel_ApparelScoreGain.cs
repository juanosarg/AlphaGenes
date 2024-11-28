
using HarmonyLib;
using RimWorld;
using Verse;
using System;
using System.Collections.Generic;


namespace AlphaGenes
{
    [HarmonyPatch(typeof(JobGiver_OptimizeApparel), "ApparelScoreGain")]
    internal class AlphaGenes_JobGiver_OptimizeApparel_ApparelScoreGain_Postfix
    {


        [HarmonyPostfix]
        private static void PostFix(ref float  __result, Pawn pawn, Apparel ap)
        {
            if (AlphaGenes_EquipmentUtility_CanEquip_Postfix.blockedWeapons.Contains(ap.def) && !pawn.HasActiveGene(InternalDefOf.AG_ForsakenKnowledge))
            {
                __result = -1000;
               
            }


        }
    }
}