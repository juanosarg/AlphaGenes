using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace AlphaGenes
{
    public class GameComponent_RaisePawns : GameComponent
    {



        public int tickCounter = 0;
        public int tickInterval = 2000;
        public HashSet<Corpse> pawnsToRise_backup = new HashSet<Corpse>();
       


        public GameComponent_RaisePawns(Game game) : base()
        {

        }

        public override void ExposeData()
        {
            base.ExposeData();

            // Need to expose corpses as exposing pawns will result with null/empty collection
            if (Scribe.mode == LoadSaveMode.Saving)
            {
                pawnsToRise_backup = StaticCollectionsClass.pawnsToRise.Select(x => x.Corpse).ToHashSet();
            }
            Scribe_Collections.Look(ref pawnsToRise_backup, "pawnsToRise_backup", LookMode.Reference);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                StaticCollectionsClass.pawnsToRise = pawnsToRise_backup.Select(x => x.InnerPawn).ToHashSet();
                pawnsToRise_backup = null;
            }

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
