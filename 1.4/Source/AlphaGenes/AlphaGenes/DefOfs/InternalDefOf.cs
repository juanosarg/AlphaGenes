
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

		public static ThoughtDef AG_PukedRats;
		public static ThoughtDef AG_Implanted;
		public static ThoughtDef AG_Implanted_Social;

		public static QuestScriptDef AG_OpportunitySite_AbandonedBiotechLab;

		public static GeneDef AG_BrittleBones;
		public static GeneDef AG_UnsettlingAppearance;
		public static GeneDef AG_LungRotStrength;
		public static GeneDef AG_FastGestation;
		public static GeneDef AG_SlowGestation;
		public static GeneDef AG_MetalEater;
		public static GeneDef AlphaGenes_Randomizer;
        public static GeneDef AG_Thalassophobia;
		public static GeneDef AG_BloodVomit;
		public static GeneDef AG_RatVomit;
		public static GeneDef AG_Beauty_Angelic;
        public static GeneDef AlphaGenes_ExoticOrganism;
		public static GeneDef AG_Male;
		public static GeneDef AG_Female;

		public static GeneDef AG_UnstableMutation;
        public static GeneDef AG_UnstableMutationMajor;
        public static GeneDef AG_UnstableMutationCatastrophic;

        public static HediffDef AG_LungRotStrengthHediff;
		public static HediffDef AG_GeneRemovalComa;
		public static HediffDef AG_MineralCraving;
		public static HediffDef AG_MineralFueled;
		public static HediffDef AG_ReactiveArmourHediff;
		public static HediffDef AG_MineralOverdriveHediff;
		public static HediffDef AG_XenogerminationComa;

		public static RulePackDef AG_NamerAlphapack;
		public static RulePackDef AG_NamerMixedpack;

		public static JobDef AG_ConsumeMetal;
		public static JobDef AG_FeedMetal;
		public static JobDef AG_DeliverMetal;
		static InternalDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
		}
	}
}
