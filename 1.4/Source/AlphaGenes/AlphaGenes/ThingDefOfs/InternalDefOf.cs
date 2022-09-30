
using RimWorld;
using Verse;


namespace AlphaGenes
{
	[DefOf]
	public static class InternalDefOf
	{
		public static ThingDef AG_Alphapack;

		public static RulePackDef AG_NamerAlphapack;

		static InternalDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
		}
	}
}
