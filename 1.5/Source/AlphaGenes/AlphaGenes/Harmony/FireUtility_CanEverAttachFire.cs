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


    [HarmonyPatch(typeof(FireUtility))]
    [HarmonyPatch("CanEverAttachFire")]
    public static class AlphaGenes_FireUtility_CanEverAttachFire_Patch
    {
        [HarmonyPostfix]
        static void CantAttachFire(Thing t, ref bool __result)
        {

            Pawn pawn = t as Pawn;
            if (pawn!=null && pawn.genes.HasActiveGene(InternalDefOf.AG_HeatImmunity))
            {
                __result = false;

            }



        }
    }








}
