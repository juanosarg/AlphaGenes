using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RimWorld;
using Verse.AI.Group;
using Verse;
using RimWorld.QuestGen;
using LudeonTK;

namespace AlphaGenes
{
	public static class GenerateBiotechLabQuest
	{


		[DebugAction("Alpha Genes", "Generate Biotech lab quest", false, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
		private static void GenerateLabQuest()
		{

			Slate slate = new Slate();
			Quest quest = QuestUtility.GenerateQuestAndMakeAvailable(InternalDefOf.AG_OpportunitySite_AbandonedBiotechLab, slate);

			QuestUtility.SendLetterQuestAvailable(quest);
		}




	}
}

