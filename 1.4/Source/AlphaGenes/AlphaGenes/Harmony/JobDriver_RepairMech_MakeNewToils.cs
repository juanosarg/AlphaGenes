using AlphaGenes;
using HarmonyLib;
using RimWorld;
using System;
using Verse;

[HarmonyPatch(typeof(JobDriver_RepairMech))]
[HarmonyPatch("MakeNewToils")]

public static class AlphaGenes_JobDriver_RepairMech_MakeNewToils_Patch
{

    public static Pawn pawn;

    [HarmonyPrefix]

    public static void StorePawn(JobDriver_RepairMech __instance)
    {
        if (__instance.pawn?.genes?.HasGene(InternalDefOf.AG_ExpertMechRepair) == true)
        {
            pawn = __instance.pawn;
        }
        else pawn = null;



    }
}


[HarmonyPatch(typeof(MechRepairUtility))]
[HarmonyPatch("RepairTick")]

public class AlphaGenes_MechRepairUtility_RepairTick_Patch
{


    [HarmonyPostfix]

    public static void AddHediff(Pawn mech)
    {
        if (AlphaGenes_JobDriver_RepairMech_MakeNewToils_Patch.pawn?.genes?.HasGene(InternalDefOf.AG_ExpertMechRepair) == true)
        {
            if (mech.health?.hediffSet?.HasHediff(InternalDefOf.AG_MechRepairBoost) == false)
            {
                mech.health?.AddHediff(InternalDefOf.AG_MechRepairBoost);
            }


        }

    }
}