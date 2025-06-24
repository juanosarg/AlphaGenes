
using RimWorld;
using Verse;
using Verse.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.Sound;


namespace AlphaGenes
{
    public class CompTargetEffect_XenotypeInjector : CompTargetEffect
    {

        public List<string> blackListedXenotypes = new List<string>() { "VREA_AndroidBasic", "VREA_AndroidAwakened" };


        public CompProperties_TargetEffect_XenotypeInjector Props
        {
            get
            {
                return (CompProperties_TargetEffect_XenotypeInjector)this.props;
            }
        }





        public override void DoEffectOn(Pawn user, Thing target)
        {


            if (user.IsColonistPlayerControlled && user.CanReserveAndReach(target, PathEndMode.Touch, Danger.Deadly))
            {
                Pawn pawn = target as Pawn;


                Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.AG_GeneRemovalComa);

                if (hediff == null)
                {
                    if(pawn.genes?.GenesListForReading.Count > 0) {
                        foreach (Gene gene in pawn.genes?.GenesListForReading)
                        {
                            pawn.genes?.RemoveGene(gene);

                        }



                    }
                    
 

                    XenotypeDef xenotype = DefDatabase<XenotypeDef>.AllDefs.Where((XenotypeDef x) => x != XenotypeDefOf.Baseliner && x != pawn.genes.Xenotype
                    &&!blackListedXenotypes.Contains(x.defName)).RandomElement();
                    pawn.genes?.SetXenotype(xenotype);


                    user.carryTracker.DestroyCarriedThing();

                    pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(InternalDefOf.AG_UsedXenotypeInjector);

                    if (AlphaGenes_Mod.settings.AG_GeneRemovalComa)
                    {
                        pawn.health.AddHediff(InternalDefOf.AG_GeneRemovalComa);
                    }

                }





            }





        }





    }
}
