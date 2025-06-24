using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;


namespace AlphaGenes
{
    public class GameComponent_RandomMood : GameComponent
    {



        public int tickCounter = 0;
        public int tickInterval = 60000;
        public Dictionary<Pawn, int> colonist_and_random_mood_backup = new Dictionary<Pawn, int>();
        List<Pawn> list2;
        List<int> list3;


        public GameComponent_RandomMood(Game game) : base()
        {

        }

        public override void FinalizeInit()
        {
            StaticCollectionsClass.colonist_and_random_mood = colonist_and_random_mood_backup;

            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(ref colonist_and_random_mood_backup, "colonist_and_random_mood_backup", LookMode.Reference, LookMode.Value, ref list2, ref list3);
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterRandomMood", 0, true);

        }

        public override void GameComponentTick()
        {


            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                colonist_and_random_mood_backup = StaticCollectionsClass.colonist_and_random_mood;

                foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravellingTransporters_Alive_Colonists.InRandomOrder())
                {

                    if (pawn.HasActiveGene(InternalDefOf.AG_VolatileMood)) {

                        int randomMood = Rand.RangeSeeded(0, 2, Current.Game.tickManager.TicksAbs + PawnsFinder.AllMapsCaravansAndTravellingTransporters_Alive_Colonists.IndexOf(pawn));
                        StaticCollectionsClass.AddColonistAndRandomMood(pawn, randomMood);

                    }
                    


                }





                tickCounter = 0;
            }



        }


    }


}
