
using RimWorld;
using Verse;
namespace AlphaGenes
{
	public class ThoughtWorker_UnsettlingAppearance : ThoughtWorker
	{
		protected override ThoughtState CurrentSocialStateInternal(Pawn pawn, Pawn other)
		{
			if (!other.RaceProps.Humanlike || !RelationsUtility.PawnsKnowEachOther(pawn, other))
			{
				return false;
			}
			
			if (PawnUtility.IsBiologicallyOrArtificiallyBlind(pawn))
			{
				return false;
			}

            if (other.genes?.HasGene(InternalDefOf.AG_UnsettlingAppearance) == true && pawn.genes?.HasGene(InternalDefOf.AG_UnsettlingAppearance) == false)
            {
				return ThoughtState.ActiveAtStage(0);
			}
			
			return false;
		}
	}
}