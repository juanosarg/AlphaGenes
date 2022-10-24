
using RimWorld;
using Verse;


namespace AlphaGenes
{
	[DefOf]
	public static class InternalDefOf
	{
		public static ThingDef AG_Alphapack;
		public static ThingDef AG_Mixedpack;
		public static PawnKindDef Rat;
		public static PawnKindDef Wolf_Timber;
		public static PawnKindDef Bear_Polar;

		public static QuestScriptDef AG_OpportunitySite_AbandonedBiotechLab;

		public static GeneDef AG_BrittleBones;
		public static GeneDef AG_UnsettlingAppearance;
		public static GeneDef AG_LungRotStrength;
		public static GeneDef AG_FastGestation;
		public static GeneDef AG_SlowGestation;
		public static GeneDef AG_MetalEater;

		public static HediffDef AG_LungRotStrengthHediff;
		public static HediffDef AG_GeneRemovalComa;
		public static HediffDef AG_MineralCraving;
		public static HediffDef AG_MineralFueled;
		public static HediffDef AG_ReactiveArmourHediff;

		public static RulePackDef AG_NamerAlphapack;
		public static RulePackDef AG_NamerMixedpack;

		public static JobDef AG_ConsumeMetal;
		public static JobDef AG_FeedMetal;
		static InternalDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
		}
	}
}
