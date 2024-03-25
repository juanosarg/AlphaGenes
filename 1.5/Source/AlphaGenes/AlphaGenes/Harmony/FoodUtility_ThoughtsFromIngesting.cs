using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AlphaGenes
{


    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("ThoughtsFromIngesting")]
    public static class AlphaGenes_FoodUtility_ThoughtsFromIngesting_Patch
    {

        public static Dictionary<string, string> meatGenesAndMeatString = new Dictionary<string, string> { { "AG_MagicMushroomFlesh", "AB_PsychotropicFungus" },
            { "AG_AerofleetFlesh", "AA_AerofleetMeat" },{ "AG_PlantFlesh", "AA_CactusMeat" },{ "AG_OcularFlesh", "AA_OcularJellyMeat" }
        ,{ "AG_WasteFlesh", "VAEWaste_ToxicMeat" } ,{ "AG_JellyFlesh", "InsectJelly" },{ "AG_ChocolateFlesh", "Chocolate" }};


        [HarmonyPostfix]
        public static void AddGeneticMeatThought(Pawn ingester, Thing foodSource, ThingDef foodDef, ref List<FoodUtility.ThoughtFromIngesting> __result)
        {
            string geneString = ingester.ReturnGenePawnHasFromList(meatGenesAndMeatString.Keys.ToList());
            if(geneString != "") {
                if (meatGenesAndMeatString[geneString] == foodDef.defName)
                {
                    ingester.mindState.lastHumanMeatIngestedTick = Find.TickManager.TicksGame;
                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(HistoryEventDefOf.AteHumanMeat, ingester.Named(HistoryEventArgsNames.Doer)), canApplySelfTookThoughts: true);
                }
            }
            
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
            string geneString = ingester.ReturnGenePawnHasFromList(AlphaGenes_FoodUtility_ThoughtsFromIngesting_Patch.meatGenesAndMeatString.Keys.ToList());
            if (geneString != "")
            {
                if (AlphaGenes_FoodUtility_ThoughtsFromIngesting_Patch.meatGenesAndMeatString[geneString] == foodDef.defName && eventDef == HistoryEventDefOf.AteNonCannibalFood)
                {
                    return false;
                }
            }
            
            
            return true;
        }
    }

}