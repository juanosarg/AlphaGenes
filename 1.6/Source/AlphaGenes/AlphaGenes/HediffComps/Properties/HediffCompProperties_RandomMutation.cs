
using Verse;
namespace AlphaGenes
{
	public class HediffCompProperties_RandomMutation : HediffCompProperties
	{
		public int numberOfGenes = 1;
		public bool recurrent = false;
		public int period = 60000;

		public HediffCompProperties_RandomMutation()
		{
			compClass = typeof(HediffComp_RandomMutation);
		}
	}
}
