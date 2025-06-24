using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AlphaGenes
{
    [HarmonyPatch(typeof(FoodUtility), "NutritionForEater", MethodType.Normal)]
    internal class AlphaGenes_FoodUtility_NutritionForEater_Postfix
    {
        [HarmonyPostfix]
        private static void PostFix(Pawn eater, Thing food, ref float __result)
        {
            if (eater?.health?.hediffSet?.HasHediff(InternalDefOf.AG_VFEI_PredatorStomach) == true)
            {
                if (!food.def.IsRawHumanFood())
                {
                    __result = __result * 1.1f;
                }
            }

        }
    }
}