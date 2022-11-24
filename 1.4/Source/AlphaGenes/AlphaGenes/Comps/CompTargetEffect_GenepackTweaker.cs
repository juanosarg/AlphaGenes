
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
    public class CompTargetEffect_GenepackTweaker : CompTargetEffect
    {




        public CompProperties_TargetEffect_GenepackTweaker Props
        {
            get
            {
                return (CompProperties_TargetEffect_GenepackTweaker)this.props;
            }
        }





        public override void DoEffectOn(Pawn user, Thing target)
        {


            if (user.IsColonistPlayerControlled && user.CanReserveAndReach(target, PathEndMode.Touch, Danger.Deadly))
            {

                Genepack genepack = target as Genepack;
                if(genepack != null)
                {
                    if (genepack.GeneSet.GenesListForReading.Count > 3)
                    {
                        Messages.Message("AG_AlreadyFourGenes".Translate(), MessageTypeDefOf.RejectInput, true);

                    }
                    else
                    {

                        GeneDef geneToCreate = DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.exclusionTags?.Contains("AG_OnlyOnCharacterCreation") == false && x.prerequisite == null &&x.biostatArc==0).RandomElement();
                        genepack.GeneSet.AddGene(geneToCreate);

                        user.carryTracker.DestroyCarriedThing();

                    }
                }



                    





            }





        }





    }
}
