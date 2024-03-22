using RimWorld;
namespace AlphaGenes
{
	public class CompProperties_AbilityStinger : CompProperties_AbilityEffect
	{

		public bool endogenes = false;

		public CompProperties_AbilityStinger()
		{
			compClass = typeof(CompAbilityEffect_Stinger);
		}
	}
}
