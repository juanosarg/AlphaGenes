using RimWorld;
using RimWorld.Planet;
using Verse;
using RimWorld.QuestGen;

namespace AlphaGenes
{



    public class WorldComponent_BiotechLabQuests : WorldComponent
    {
        public int tickCounter;
        public int ticksToNextQuest = 60000 * 15;


        public WorldComponent_BiotechLabQuests(World world) : base(world)
        {
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();



        }

        public override void WorldComponentTick()
        {
            base.WorldComponentTick();



            if (tickCounter > ticksToNextQuest)
            {

                Slate slate = new Slate();
                Quest quest = QuestUtility.GenerateQuestAndMakeAvailable(InternalDefOf.AG_OpportunitySite_AbandonedBiotechLab, slate);

                QuestUtility.SendLetterQuestAvailable(quest);
                ticksToNextQuest = (int)(60000 * Rand.RangeInclusive(15, 30) * AlphaGenes_Mod.settings.AG_QuestRate);
                tickCounter = 0;




            }
            tickCounter++;





        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.tickCounter, nameof(this.tickCounter));
            Scribe_Values.Look(ref this.ticksToNextQuest, nameof(this.ticksToNextQuest));
        }
    }
}
