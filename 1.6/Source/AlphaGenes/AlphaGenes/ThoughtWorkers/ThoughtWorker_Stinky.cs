
using RimWorld;
using System.Collections.Generic;
using Verse;
namespace AlphaGenes
{
    public class ThoughtWorker_Stinky : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn pawn, Pawn other)
        {
            if (!other.RaceProps.Humanlike || !RelationsUtility.PawnsKnowEachOther(pawn, other))
            {
                return false;
            }

            List<BodyPartRecord> partsWithTag = pawn.RaceProps.body.GetPartsWithDef(InternalDefOf.Nose);
            if (!partsWithTag.Any())
            {
                return false;
            }
            foreach (BodyPartRecord item in partsWithTag)
            {
                if (pawn.health.hediffSet.PartIsMissing(item))
                {
                    return false;                  
                }
            }

            if (other.genes?.HasActiveGene(InternalDefOf.AG_Stinky) == true)
            {
                return ThoughtState.ActiveAtStage(0);
            }

            return false;
        }
    }
}