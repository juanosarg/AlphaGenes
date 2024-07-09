using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;


namespace AlphaGenes
{
    public class GameComponent_RaisePawns : GameComponent
    {



        public int tickCounter = 0;
        public int tickInterval = 2000;
       


        public GameComponent_RaisePawns(Game game) : base()
        {

        }

       

      

        public override void GameComponentTick()
        {


            tickCounter++;
            if ((tickCounter > tickInterval))
            {

               if(StaticCollectionsClass.pawnsToRise.Count > 0)
                {

                    List<Pawn> pawnsToRemove = new List<Pawn>();
                    foreach(Pawn pawn in StaticCollectionsClass.pawnsToRise)
                    {
                        if (pawn.Corpse?.Spawned == true && pawn.genes != null)
                        {
                            pawn.genes.AddGene(InternalDefOf.AG_Shambler_Plagued, true);
                        }
                        if (MutantUtility.CanResurrectAsShambler(pawn.Corpse, ignoreIndoors: true))
                        {
                            MutantUtility.ResurrectAsShambler(pawn);
                        }
                        pawnsToRemove.Add(pawn);
                        

                    }
                    if(pawnsToRemove.Count > 0)
                    {
                        foreach(Pawn pawn in pawnsToRemove)
                        {
                            StaticCollectionsClass.pawnsToRise.Remove(pawn);

                        }
                    }
                    
                }





                tickCounter = 0;
            }



        }


    }


}
