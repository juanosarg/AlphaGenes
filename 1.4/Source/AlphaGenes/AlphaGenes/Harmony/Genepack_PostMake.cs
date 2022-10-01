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
				GenerateName(geneSet, InternalDefOf.AG_NamerAlphapack);
				Rand.PopState();
				if (geneSet.Empty)
				{
					Log.Error("Generated gene pack with no genes.");
				}
				geneSet.SortGenes();
				___geneSet = geneSet;
			}else if (__instance.def == InternalDefOf.AG_Mixedpack)
			{
				GeneSet geneSet = new GeneSet();
				IntRange rangeNormalGenes = new IntRange(1, 3);
				Rand.PushState(GenTicks.TicksGame);
				int normalGenes = rangeNormalGenes.RandomInRange;
				IntRange rangeAlphaGenes = new IntRange(1, 4 - normalGenes);
				Rand.PushState(GenTicks.TicksGame);
				int alphaGenes = rangeAlphaGenes.RandomInRange;

				for (int j = 0; j < normalGenes; j++)
				{
					if (DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.biostatArc == 0 && geneSet.CanAddGeneDuringGeneration(x)).TryRandomElementByWeight((GeneDef x) => x.selectionWeight, out var result1))
					{
						geneSet.AddGene(result1);
					}
				}

				for (int j = 0; j < alphaGenes; j++)
				{
					geneSet.AddGene(DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.defName.Contains("AG_")).RandomElement());
				}
				GenerateName(geneSet, InternalDefOf.AG_NamerMixedpack);
				Rand.PopState();
				if (geneSet.Empty)
				{
					Log.Error("Generated gene pack with no genes.");
				}
				geneSet.SortGenes();
				___geneSet = geneSet;
			}


		}

		public static void GenerateName(GeneSet geneSet, RulePackDef rule)
		{
			if (ModsConfig.BiotechActive && geneSet.GenesListForReading.Any())
			{
				GrammarRequest request = default(GrammarRequest);
				request.Includes.Add(rule);
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
