
using RimWorld;
using Verse;
namespace AlphaGenes
{
	public class ThoughtWorker_BloodsuckerDistrust : ThoughtWorker
	{
		protected override ThoughtState CurrentSocialStateInternal(Pawn pawn, Pawn other)
		{
			if (!other.RaceProps.Humanlike || !RelationsUtility.PawnsKnowEachOther(pawn, other))
			{
				return false;
			}

			if (other.genes?.GetGene(GeneDefOf.Hemogenic)?.Active != true)
			{
				return false;
			}

			if (pawn.genes?.GetGene(DefDatabase<GeneDef>.GetNamedSilentFail("AG_BloodsuckerDistrust"))?.Active != true)
			{
				return false;
			}

			if (pawn.genes?.Xenotype != other.genes?.Xenotype)

			{
				return ThoughtState.ActiveAtStage(0);
			}

			return false;
		}
	}
}