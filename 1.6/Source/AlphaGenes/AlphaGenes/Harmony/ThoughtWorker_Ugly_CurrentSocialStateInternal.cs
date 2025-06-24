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

    [HarmonyPatch(typeof(ThoughtWorker_Ugly))]
    [HarmonyPatch("CurrentSocialStateInternal")]
    public static class AlphaGenes_ThoughtWorker_Ugly_CurrentSocialStateInternal_Patch
    {


        [HarmonyPostfix]
        public static void NoUgliness(Pawn pawn, ref ThoughtState __result)

        {

            if (pawn?.HasActiveGene(InternalDefOf.AG_Teratophilia) == true)
            {
                __result = false;

            }

           


        }


    }
}
