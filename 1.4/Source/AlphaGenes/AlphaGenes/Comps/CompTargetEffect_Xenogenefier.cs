
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
    public class CompTargetEffect_Xenogenefier : CompTargetEffect
    {




        public CompProperties_TargetEffect_Xenogenefier Props
        {
            get
            {
                return (CompProperties_TargetEffect_Xenogenefier)this.props;
            }
        }





        public override void DoEffectOn(Pawn user, Thing target)
        {


            if (user.IsColonistPlayerControlled && user.CanReserveAndReach(target, PathEndMode.Touch, Danger.Deadly))
            {
                Pawn pawn = target as Pawn;


                Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.AG_GeneRemovalComa);

                if (hediff == null && pawn.genes.Endogenes.Count > 0)
                {
                    Gene geneToTransform = pawn.genes.Endogenes.RandomElement();
                    pawn.genes.Endogenes.Remove(geneToTransform);
                    pawn.genes.Xenogenes.Add(geneToTransform);
                    pawn.health.AddHediff(InternalDefOf.AG_GeneRemovalComa);
                    user.carryTracker.DestroyCarriedThing();
                }





            }





        }





    }
}
