
using RimWorld;
using Verse;
using Verse.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.Sound;
using AlphaMemes;


namespace AlphaGenes
{
    public class CompTargetEffect_ChooseAndRemoveGene : CompTargetEffect
    {

        public CompProperties_TargetEffect_ChooseAndRemoveGene Props
        {
            get
            {
                return (CompProperties_TargetEffect_ChooseAndRemoveGene)this.props;
            }
        }

        public override void DoEffectOn(Pawn user, Thing target)
        {
            if (user.IsColonistPlayerControlled && user.CanReserveAndReach(target, PathEndMode.Touch, Danger.Deadly))
            {
                Pawn pawn = target as Pawn;
                if (pawn.genes?.GenesListForReading.Count > 0)
                {
                    Dialog_ChooseAndRemoveGene window = new Dialog_ChooseAndRemoveGene(pawn, this.parent);
                    Find.WindowStack.Add(window);
                }

                else
                {
                    Messages.Message("AG_NoGenesAtAll".Translate(), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }

            }

        }

    }
}
