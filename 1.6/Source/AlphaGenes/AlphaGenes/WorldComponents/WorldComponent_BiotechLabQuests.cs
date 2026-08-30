using RimWorld;
using RimWorld.Planet;
using Verse;
using RimWorld.QuestGen;
using System.Collections.Generic;
using System.Linq;

namespace AlphaGenes
{

    public class WorldComponent_BiotechLabQuests : WorldComponent
    {
        public int tickCounter;
        public int ticksToNextQuest = 60000 * 14;

        public int forsakenTickCounter = forsakenTickInterval;
        public const int forsakenTickInterval = 10000;
     
        public WorldComponent_BiotechLabQuests(World world) : base(world)
        {
        }

        public override void FinalizeInit(bool fromLoad)
        {
            base.FinalizeInit(fromLoad);
        }

        public override void WorldComponentTick()
        {
            base.WorldComponentTick();

            if (!AlphaGenes_Mod.settings.AG_DisableQuests)
            {

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

            if (forsakenTickCounter > forsakenTickInterval)
            {
                List<Pawn> allPawns = PawnsFinder.AllMapsCaravansAndTravellingTransporters_Alive_Colonists.InRandomOrder().ToList();
                bool forgeAccepted=false;
                foreach (Pawn pawn in allPawns)
                {
                    if (pawn.genes?.HasActiveGene(InternalDefOf.AG_ForsakenKnowledge) == true)
                    {
                        if (VEF.Buildings.StaticCollectionsClass.hidden_designators.Contains(InternalDefOf.AG_ForsakenForge))
                        {
                            VEF.Buildings.StaticCollectionsClass.hidden_designators.Remove(InternalDefOf.AG_ForsakenForge);
                        }
                        forgeAccepted = true;
                        break;
                    }
                }
                if (!forgeAccepted) {
                    if (!VEF.Buildings.StaticCollectionsClass.hidden_designators.Contains(InternalDefOf.AG_ForsakenForge))
                    {
                        VEF.Buildings.StaticCollectionsClass.hidden_designators.Add(InternalDefOf.AG_ForsakenForge);
                    }
                }

                forsakenTickCounter = 0;

            }
            forsakenTickCounter++;

        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.tickCounter, nameof(this.tickCounter));
            Scribe_Values.Look(ref this.ticksToNextQuest, nameof(this.ticksToNextQuest));
        }
    }
}
