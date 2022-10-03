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

		private static readonly AlphaGeneCount[] AlphaGeneCountProbabilities;
		private static readonly MixedGeneCount[] MixedGeneCountProbabilities;

		static AlphaGenes_Genepack_PostMake_Patch(){

			AlphaGeneCount[] array = new AlphaGeneCount[6];
			AlphaGeneCount geneCount = new AlphaGeneCount
			{
				alphaCount = 1,
				chance = 0.66f
			};
			array[0] = geneCount;
			geneCount = new AlphaGeneCount
			{
				alphaCount = 2,
				chance = 0.2f
			};
			array[1] = geneCount;
			geneCount = new AlphaGeneCount
			{
				alphaCount = 3,
				chance = 0.05f
			};
			array[2] = geneCount;
			geneCount = new AlphaGeneCount
			{
				alphaCount = 4,
				chance = 0.02f
			};
			array[3] = geneCount;
			geneCount = new AlphaGeneCount
			{
				alphaCount = 1,
				architeCount = 1,
				chance = 0.05f
			};
			array[4] = geneCount;
			geneCount = new AlphaGeneCount
			{
				alphaCount = 2,
				architeCount = 1,
				chance = 0.02f
			};
			array[5] = geneCount;
			AlphaGeneCountProbabilities = array;


			MixedGeneCount[] array2 = new MixedGeneCount[6];
			MixedGeneCount geneCount2 = new MixedGeneCount
			{
				alphaCount = 1,
				nonArchiteCount = 1,
				chance = 0.66f
			};
			array2[0] = geneCount2;
			geneCount2 = new MixedGeneCount
			{
				alphaCount = 2,
				nonArchiteCount = 1,
				chance = 0.1f
			};
			array2[1] = geneCount2;
			geneCount2 = new MixedGeneCount
			{
				alphaCount = 1,
				nonArchiteCount = 2,
				chance = 0.1f
			};
			array2[2] = geneCount2;
			geneCount2 = new MixedGeneCount
			{
				alphaCount = 2,
				nonArchiteCount = 2,
				chance = 0.05f
			};
			array2[3] = geneCount2;
			geneCount2 = new MixedGeneCount
			{
				alphaCount = 3,
				nonArchiteCount = 1,
				chance = 0.02f
			};
			array2[4] = geneCount2;
			geneCount2 = new MixedGeneCount
			{
				alphaCount = 1,
				nonArchiteCount = 1,
				architeCount = 1,
				chance = 0.07f
			};
			array2[5] = geneCount2;
			MixedGeneCountProbabilities = array2;

		}


		[HarmonyPostfix]
        public static void ChangeForAlphaGenes(Genepack __instance, ref GeneSet ___geneSet)

        {
           if(__instance.def == InternalDefOf.AG_Alphapack)
            {
				GeneSet geneSet = new GeneSet();
				
				Rand.PushState(GenTicks.TicksGame);
				AlphaGeneCount geneCount = AlphaGeneCountProbabilities.RandomElementByWeight((AlphaGeneCount x) => x.chance);
				for (int i = 0; i < geneCount.architeCount; i++)
				{
					if (DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.biostatArc > 0 && geneSet.CanAddGeneDuringGeneration(x)).TryRandomElementByWeight((GeneDef x) => x.selectionWeight, out GeneDef result))
					{
						geneSet.AddGene(result);
					}
				}
				for (int j = 0; j < geneCount.alphaCount; j++)
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
				
				Rand.PushState(GenTicks.TicksGame);
				MixedGeneCount geneCount = MixedGeneCountProbabilities.RandomElementByWeight((MixedGeneCount x) => x.chance);
				for (int i = 0; i < geneCount.architeCount; i++)
				{
					if (DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.biostatArc > 0 && geneSet.CanAddGeneDuringGeneration(x)).TryRandomElementByWeight((GeneDef x) => x.selectionWeight, out GeneDef result))
					{
						geneSet.AddGene(result);
					}
				}
				for (int j = 0; j < geneCount.nonArchiteCount; j++)
				{
					if (DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.biostatArc == 0 && geneSet.CanAddGeneDuringGeneration(x)).TryRandomElementByWeight((GeneDef x) => x.selectionWeight, out var result1))
					{
						geneSet.AddGene(result1);
					}
				}

				for (int j = 0; j < geneCount.alphaCount; j++)
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

		private struct AlphaGeneCount
		{			

			public int alphaCount;

			public int architeCount;

			public float chance;
		}

		private struct MixedGeneCount
		{
			public int nonArchiteCount;

			public int alphaCount;

			public int architeCount;

			public float chance;
		}
	}
}
