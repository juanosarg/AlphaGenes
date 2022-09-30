using HarmonyLib;
using RimWorld;
using Verse.Grammar;
using Verse;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Verse.AI;

namespace AlphaGenes
{
   
    [HarmonyPatch(typeof(Genepack))]
    [HarmonyPatch("PostMake")]
    public static class AlphaGenes_Genepack_PostMake_Patch
    {
        [HarmonyPostfix]
        public static void ChangeForAlphaGenes(Genepack __instance, ref GeneSet ___geneSet)

        {
           if(__instance.def == InternalDefOf.AG_Alphapack)
            {
				GeneSet geneSet = new GeneSet();
				IntRange range = new IntRange(1,4);
				Rand.PushState(GenTicks.TicksGame);

				for (int j = 0; j < range.RandomInRange; j++)
				{				
						geneSet.AddGene(DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.defName.Contains("AG_")).RandomElement());				
				}
				GenerateName(geneSet);
				Rand.PopState();
				if (geneSet.Empty)
				{
					Log.Error("Generated gene pack with no genes.");
				}
				geneSet.SortGenes();
				___geneSet = geneSet;
			}


        }

		public static void GenerateName(GeneSet geneSet)
		{
			if (ModsConfig.BiotechActive && geneSet.GenesListForReading.Any())
			{
				GrammarRequest request = default(GrammarRequest);
				request.Includes.Add(InternalDefOf.AG_NamerAlphapack);
				request.Rules.Add(new Rule_String("geneWord", geneSet.Label));
				request.Rules.Add(new Rule_String("geneCountMinusOne", (geneSet.GenesListForReading.Count - 1).ToString()));
				request.Constants.Add("geneCount", geneSet.GenesListForReading.Count.ToString());

				Type typ = typeof(GeneSet);
				FieldInfo type = typ.GetField("name", BindingFlags.Instance | BindingFlags.NonPublic);
				type.SetValue(geneSet, GrammarResolver.Resolve("r_name", request, null, forceLog: false, null, null, null, capitalizeFirstSentence: false));

			}
			
		}
	}
}
