
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
    public class CompTargetEffect_XenotypeNullifier : CompTargetEffect
    {




        public CompProperties_TargetEffect_XenotypeNullifier Props
        {
            get
            {
                return (CompProperties_TargetEffect_XenotypeNullifier)this.props;
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
                    pawn.genes?.SetXenotype(XenotypeDefOf.Baseliner);
                    pawn.health.AddHediff(InternalDefOf.AG_GeneRemovalComa);
                    user.carryTracker.DestroyCarriedThing();
                }





            }





        }





    }
}
