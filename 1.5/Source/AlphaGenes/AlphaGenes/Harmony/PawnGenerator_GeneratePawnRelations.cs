using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Reflection.Emit;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Verse.AI;

namespace AlphaGenes
{

    [HarmonyPatch(typeof(PawnGenerator))]
    [HarmonyPatch("GeneratePawnRelations")]
    public static class AlphaGenes_PawnGenerator_GeneratePawnRelations_Patch
    {
        [HarmonyPrefix]
        public static bool DisableRelations(Pawn pawn)

        {



            if (pawn.HasActiveGene(InternalDefOf.AG_Male) || pawn.HasActiveGene(InternalDefOf.AG_Female))
            {

                return false;
            }
            else return true;


        }
    }
}
