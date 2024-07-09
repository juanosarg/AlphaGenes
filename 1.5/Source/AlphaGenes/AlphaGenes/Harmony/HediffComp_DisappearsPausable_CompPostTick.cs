using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;
using AlphaGenes;

namespace AlphaGenes
{


    [HarmonyPatch(typeof(HediffComp_DisappearsPausable))]
    [HarmonyPatch("CompPostTick")]
    public static class AlphaGenes_HediffComp_DisappearsPausable_CompPostTick_Patch
    {
        [HarmonyPostfix]
        static void DetectGene(HediffComp_DisappearsPausable __instance)
        {

            if (__instance.parent.pawn.IsHashIntervalTick(100) && __instance.parent.pawn.genes?.HasActiveGene(InternalDefOf.AG_Shambler_Timeless)==true)
            {
                __instance.ticksToDisappear += 80;
            }


        }
    }








}
