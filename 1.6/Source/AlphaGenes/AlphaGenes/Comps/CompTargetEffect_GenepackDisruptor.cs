
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
    public class CompTargetEffect_GenepackDisruptor : CompTargetEffect
    {




        public CompProperties_TargetEffect_GenepackDisruptor Props
        {
            get
            {
                return (CompProperties_TargetEffect_GenepackDisruptor)this.props;
            }
        }





        public override void DoEffectOn(Pawn user, Thing target)
        {


            if (user.IsColonistPlayerControlled && user.CanReserveAndReach(target, PathEndMode.Touch, Danger.Deadly))
            {

                Genepack genepack = target as Genepack;
                if (genepack != null)
                {
                    if (genepack.GeneSet.GenesListForReading.Count <2)
                    {
                        Messages.Message("AG_NeedsMoreThanTwoGenes".Translate(), MessageTypeDefOf.RejectInput, true);

                    }
                    else
                    {

                        genepack.GeneSet.Debug_RemoveGene(genepack.GeneSet.GenesListForReading.RandomElement());

                        user.carryTracker.DestroyCarriedThing();

                    }
                }









            }





        }





    }
}
