
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
    public class CompTargetEffect_ArchoXenogenefier : CompTargetEffect
    {

        public CompProperties_TargetEffect_ArchoXenogenefier Props
        {
            get
            {
                return (CompProperties_TargetEffect_ArchoXenogenefier)this.props;
            }
        }
        public override void DoEffectOn(Pawn user, Thing target)
        {
            if (user.IsColonistPlayerControlled && user.CanReserveAndReach(target, PathEndMode.Touch, Danger.Deadly))
            {
                
                Pawn pawn = target as Pawn;
                if (pawn.genes?.Endogenes.Count > 0)
                {
                    Dialog_EndogeneToXenogene window = new Dialog_EndogeneToXenogene(pawn, this.parent);
                    Find.WindowStack.Add(window);
                }
                else
                {
                    Messages.Message("AG_NoEndogenes".Translate(), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                

            }
        }
    }
}
