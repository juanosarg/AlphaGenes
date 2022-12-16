
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;




namespace AlphaGenes
{

    public class Gene_RandomAnimalSummon : Gene
    {
        public List<GeneDef> genes = new List<GeneDef>();

        public override void PostAdd()
        {
            base.PostAdd();


            genes = DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.defName.Contains("AlphaGenes_AnimalSummon")).ToList();
            if(genes.Count > 0)
            {
                pawn.genes.AddGene(genes.RandomElement(), true);
                
            }
            pawn.genes.RemoveGene(this);






        }



    }
}
