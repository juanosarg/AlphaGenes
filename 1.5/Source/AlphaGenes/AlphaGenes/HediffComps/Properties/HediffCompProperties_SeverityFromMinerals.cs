
using Verse;
namespace AlphaGenes
{
	public class HediffCompProperties_SeverityFromMinerals : HediffCompProperties
	{
		public float severityPerHourEmpty;
		public float severityPerHourHemogen;
		public HediffCompProperties_SeverityFromMinerals()
		{
			compClass = typeof(HediffComp_SeverityFromMinerals);
		}
	}
}
