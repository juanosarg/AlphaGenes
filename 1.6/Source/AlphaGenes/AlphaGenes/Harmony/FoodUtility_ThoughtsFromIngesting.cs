using HarmonyLib;
using RimWorld;
using Verse.Grammar;
using Verse;
using System.Reflection;
using System.Collections.Generic;


namespace AlphaGenes
{


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

}