
using RimWorld;
using Verse;
using Verse.AI;

namespace AlphaGenes
{
    public class MentalBreakWorker_Thalassophobia : MentalBreakWorker
    {
        public override bool BreakCanOccur(Pawn pawn)
        {
            if (!pawn.Spawned)
            {
                return false;
            }
            if (!base.BreakCanOccur(pawn))
            {
                return false;
            }
            return ThoughtWorker_Thalassophobia.NearWater(pawn);
        }
    }
}
