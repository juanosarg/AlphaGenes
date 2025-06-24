using RimWorld;
using Verse;
using RimWorld.Planet;


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


        public override void FinalizeInit(bool fromLoad)
        {

            StaticCollectionsClass.ocular_gene_colonists_inWorld = ocular_gene_colonists_inWorld_backup;


            base.FinalizeInit(fromLoad);

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
                foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravellingTransporters_Alive_Colonists)
                {
                    if (DefDatabase<GeneDef>.GetNamedSilentFail("AG_OcularAffinity")!=null&&pawn.HasActiveGene(DefDatabase<GeneDef>.GetNamedSilentFail("AG_OcularAffinity")))
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
