
using RimWorld;
using Verse;
using Verse.AI;

namespace AlphaGenes
{
    public class MentalState_Thalassophobia : MentalState
    {
        private int lastWaterSeenTick = -1;

        protected override bool CanEndBeforeMaxDurationNow => false;

        public override RandomSocialMode SocialModeMax()
        {
            return RandomSocialMode.Off;
        }

        public override void MentalStateTick(int delta)
        {
            base.MentalStateTick(delta);
            if (pawn.IsHashIntervalTick(30, delta))
            {
                if (lastWaterSeenTick < 0 || ThoughtWorker_Thalassophobia.NearWater(pawn))
                {
                    lastWaterSeenTick = Find.TickManager.TicksGame;
                }
                if (lastWaterSeenTick >= 0 && Find.TickManager.TicksGame >= lastWaterSeenTick + def.minTicksBeforeRecovery)
                {
                    RecoverFromState();
                }
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref lastWaterSeenTick, "lastWaterSeenTick", -1);
        }

        public override TaggedString GetBeginLetterText()
        {
            if (def.beginLetter.NullOrEmpty())
            {
                return null;
            }
            if (AlphaGenes_Mod.settings.AG_DisableThalassophobiaMessage)
            {
                return null;
            }
            return def.beginLetter.Formatted(pawn.NameShortColored, pawn.Named("PAWN")).AdjustedFor(pawn).Resolve()
                .CapitalizeFirst();
        }
    }
}
