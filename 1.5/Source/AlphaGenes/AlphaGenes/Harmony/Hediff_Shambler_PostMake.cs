using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;
using VanillaGenesExpanded;

namespace AlphaGenes
{


    [HarmonyPatch(typeof(Hediff_Shambler))]
    [HarmonyPatch("PostMake")]
    public static class AlphaGenes_Hediff_Shambler_PostMake_Patch
    {
        [HarmonyPostfix]
        static void AddRandomGenes(Hediff_Shambler __instance)
        {

            if (AlphaGenes_Mod.settings.AG_RandomGenesToShamblers && __instance.pawn.genes!=null)
            {
                if(__instance.pawn.genes.GenesListForReading.Where(x => x.def.geneClass == typeof(Gene_Shambler) && x.def != InternalDefOf.AG_Shambler_Plagued).Count() <= 0)
                {
                    List<GeneDef> genes = DefDatabase<GeneDef>.AllDefsListForReading.Where(x => x.geneClass == typeof(Gene_Shambler) && x != InternalDefOf.AG_Shambler_DeadlifeBelcher).ToList();
                    if (__instance.pawn.genes.HasActiveGene(InternalDefOf.AG_Shambler_Plagued))
                    {
                        genes.Remove(InternalDefOf.AG_Shambler_Plagued);
                    }
                    IntRange numberToRemoveGasProducers = new IntRange(1, 10);
                    if (numberToRemoveGasProducers.RandomInRange != 5)
                    {
                        genes.Remove(InternalDefOf.AG_Shambler_Toxic);
                        genes.Remove(InternalDefOf.AG_Shambler_Stinker);
                        genes.Remove(InternalDefOf.AG_Shambler_Polluter);
                    }


                    IntRange numberToAdd = new IntRange(1, 3);
                    List<GeneDef> genesToAdd = genes.InRandomOrder().Take(numberToAdd.RandomInRange).ToList();
                    foreach (GeneDef geneToAdd in genesToAdd)
                    {
                        __instance.pawn.genes.AddGene(geneToAdd, true);

                    }
                }


                
            }


        }
    }








}
