
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



		public static GeneDef AG_BrittleBones;
		public static GeneDef AG_UnsettlingAppearance;
		public static GeneDef AG_LungRotStrength;

		public static HediffDef AG_LungRotStrengthHediff;

		public static RulePackDef AG_NamerAlphapack;
		public static RulePackDef AG_NamerMixedpack;

		static InternalDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
		}
	}
}
