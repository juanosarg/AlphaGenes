using HarmonyLib;
using RimWorld;
using Verse.Grammar;
using Verse;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection.Emit;
using System.Linq;
using System;
using Verse.AI;
using VFECore.Abilities;
using VFECore.OptionalFeatures;



namespace AlphaGenes
{


    [HarmonyPatch(typeof(Thing))]
    [HarmonyPatch("Ingested")]
    public static class AlphaGenes_Thing_Ingested_Patch
    {

        public static Dictionary<string, string> meatGenesAndMeatString = new Dictionary<string, string> { { "AG_MagicMushroomFlesh", "AB_PsychotropicFungus" },
            { "AG_AerofleetFlesh", "AA_AerofleetMeat" },{ "AG_PlantFlesh", "AA_CactusMeat" },{ "AG_OcularFlesh", "AA_OcularJellyMeat" }
        ,{ "AG_WasteFlesh", "VAEWaste_ToxicMeat" } ,{ "AG_JellyFlesh", "InsectJelly" },{ "AG_ChocolateFlesh", "Chocolate" },{ "AG_TwistedFlesh", "Meat_Twisted" }
        ,{ "AG_RoyalJellyFlesh", "VFEI2_RoyalInsectJelly" }};

    

        [HarmonyPostfix]
        public static void AddGeneticMeatThought(Pawn ingester, Thing __instance)
        {
            if (ingester?.RaceProps.Humanlike == true)
            {
                string geneString = ingester.ReturnGenePawnHasFromList(meatGenesAndMeatString.Keys.ToList());
              
                if (geneString != "")
                {
                    if (meatGenesAndMeatString[geneString] == __instance.def.defName)
                    {
                  

                        ingester.mindState.lastHumanMeatIngestedTick = Find.TickManager.TicksGame;
                        Find.HistoryEventsManager.RecordEvent(new HistoryEvent(HistoryEventDefOf.AteHumanMeat, ingester.Named(HistoryEventArgsNames.Doer)), canApplySelfTookThoughts: true);

                        Find.HistoryEventsManager.RecordEvent(new HistoryEvent(HistoryEventDefOf.AteHumanMeatDirect, ingester.Named(HistoryEventArgsNames.Doer)), canApplySelfTookThoughts: true);

                    }
                }

            }

        }
    }

    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("ThoughtsFromIngesting")]
    public static class AlphaGenes_FoodUtility_ThoughtsFromIngesting_Patch
    {



        [HarmonyPostfix]
        public static void DisableThoughtsFromVenomGland(Pawn ingester, Thing foodSource, ThingDef foodDef, ref List<FoodUtility.ThoughtFromIngesting> __result)
        {


            if (ingester.health?.hediffSet?.HasHediff(InternalDefOf.AG_VFEI_VenomGland) == true)
            {
                __result.Clear();
            }
        }
    }

    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("AddThoughtsFromIdeo")]
    public static class AlphaGenes_FoodUtility_AddThoughtsFromIdeo_Patch
    {

        [HarmonyPrefix]
        public static bool DisableNonCannibalFoodThought(HistoryEventDef eventDef, Pawn ingester, ThingDef foodDef)
        {
            string geneString = ingester.ReturnGenePawnHasFromList(AlphaGenes_Thing_Ingested_Patch.meatGenesAndMeatString.Keys.ToList());
            if (geneString != "")
            {
                if (AlphaGenes_Thing_Ingested_Patch.meatGenesAndMeatString[geneString] == foodDef.defName && eventDef == HistoryEventDefOf.AteNonCannibalFood)
                {
                    return false;
                }
            }


            return true;
        }
    }

}