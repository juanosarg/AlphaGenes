
using RimWorld;
using Verse;


namespace AlphaGenes
{
	[DefOf]
	public static class InternalDefOf
	{
		public static ThingDef AG_Alphapack;
		public static ThingDef AG_Mixedpack;


		public static RulePackDef AG_NamerAlphapack;
		public static RulePackDef AG_NamerMixedpack;

		static InternalDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
		}
	}
}
