
using Verse;
namespace AlphaGenes
{
	public class HediffCompProperties_GiveHediffLungRotAlternative : HediffCompProperties_GiveHediff
	{
		public SimpleCurve mtbOverRotGasExposureCurve;

		public float minSeverity = 0.5f;

		public int mtbCheckDuration = 60;

		public HediffCompProperties_GiveHediffLungRotAlternative()
		{
			compClass = typeof(HediffComp_GiveHediffLungRotAlternative);
		}
	}
}
