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


    [HarmonyPatch(typeof(GenRecipe))]
    [HarmonyPatch("PostProcessProduct")]
    public static class AlphaGenes_GenRecipe_PostProcessProduct_Patch
    {
        [HarmonyPostfix]
        static void IncreaseQualityByOne(Thing product, RecipeDef recipeDef, Pawn worker)
        {


            if (worker?.health?.hediffSet?.HasHediff(InternalDefOf.AG_DevouredArtistic) == true)
            {

                if (product?.HasThingCategory(ThingCategoryDefOf.BuildingsArt) == true )
                {
                    CompQuality compQuality = product?.TryGetComp<CompQuality>();
                    if (compQuality != null)
                    {
                        if (recipeDef?.workSkill == null)
                        {
                            Log.Error(recipeDef + " needs workSkill because it creates a product with a quality.");
                        }
                        if (compQuality.Quality != QualityCategory.Legendary)
                        {
                            compQuality.SetQuality(compQuality.Quality + 1, ArtGenerationContext.Colony);

                        }

                    }
                }

                

            }
          








        }
    }








}
