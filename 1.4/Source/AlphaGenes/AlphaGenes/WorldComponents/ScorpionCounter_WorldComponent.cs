using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
using RimWorld.Planet;
using AlphaGenes;
using VanillaGenesExpanded;

namespace AlphaGenes
{
    public class ScorpionCounter_WorldComponent : WorldComponent
    {

      
        public int totalScorpions = 0;
        public int tickCounter = 0;
        public int tickInterval = 1000;
        public int ocular_gene_colonists_inWorld_backup = 0;
        public ScorpionCounter_WorldComponent(World world) : base(world)
        {

        }


        public override void FinalizeInit()
        {

            StaticCollectionsClass.ocular_gene_colonists_inWorld = ocular_gene_colonists_inWorld_backup;


            base.FinalizeInit();

        }


        public override void ExposeData()
        {
            Scribe_Values.Look<int>(ref this.ocular_gene_colonists_inWorld_backup, "ocular_gene_colonists_inWorld_backup", 0, true);
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterOcular", 0, true);
          

            base.ExposeData();
        }

        public override void WorldComponentTick()
        {
            base.WorldComponentTick();


            tickCounter++;
            if ((tickCounter > tickInterval))
            {
                ocular_gene_colonists_inWorld_backup = 0;
                foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                {
                    if (DefDatabase<GeneDef>.GetNamedSilentFail("AG_OcularAffinity")!=null&&pawn.genes?.HasGene(DefDatabase<GeneDef>.GetNamedSilentFail("AG_OcularAffinity")) == true)
                    {
                        ocular_gene_colonists_inWorld_backup++;
                    }

                }
                StaticCollectionsClass.ocular_gene_colonists_inWorld = ocular_gene_colonists_inWorld_backup;



                tickCounter = 0;
            }



        }




    }


}
