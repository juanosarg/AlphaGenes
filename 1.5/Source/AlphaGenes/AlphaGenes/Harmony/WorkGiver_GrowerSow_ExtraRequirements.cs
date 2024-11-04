
using HarmonyLib;
using RimWorld;
using Verse;
using System;
using System.Collections.Generic;


namespace AlphaGenes
{
    [HarmonyPatch(typeof(WorkGiver_GrowerSow), "ExtraRequirements")]
    internal class AlphaGenes_WorkGiver_GrowerSow_ExtraRequirements_Postfix
    {

        [HarmonyPostfix]
        private static void PostFix(ref bool __result, IPlantToGrowSettable settable, Pawn pawn)
        {
          
            Zone_Growing zone_Growing = settable as Zone_Growing;
            IntVec3 c;
            if (zone_Growing != null)
            {              
                c = zone_Growing.Cells[0];
            }
            else
            {
                c = ((Thing)settable).Position;
            }

            ThingDef wantedPlantDef = WorkGiver_Grower.CalculateWantedPlantDef(c, pawn.Map);
          
            if (wantedPlantDef == InternalDefOf.AG_Septimum && !pawn.HasActiveGene(InternalDefOf.AG_ForsakenKnowledge))
            {
              
                __result = false;
            }

        }
    }
}