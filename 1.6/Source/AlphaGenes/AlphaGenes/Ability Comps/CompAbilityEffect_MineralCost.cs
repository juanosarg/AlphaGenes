using RimWorld;
using Verse;

namespace AlphaGenes
{

    public class CompAbilityEffect_MineralCost : CompAbilityEffect
    {
        public new CompProperties_AbilityMineralCost Props => (CompProperties_AbilityMineralCost)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);

            Gene_Resource_Metal gene = parent.pawn.genes?.GetFirstGeneOfType<Gene_Resource_Metal>();
            if (gene != null)
            {
                gene.Value -= Props.mineralCost;
               
            }

        }

        public override bool GizmoDisabled(out string reason)
        {
            Gene_Resource_Metal gene = parent.pawn.genes?.GetFirstGeneOfType<Gene_Resource_Metal>();
            if (gene == null)
            {
                reason = "AG_AbilityDisabledNoMineralGene".Translate(parent.pawn);
                return true;
            }
            if (gene.Value < Props.mineralCost)
            {
                reason = "AG_AbilityDisabledNoMineral".Translate(parent.pawn);
                return true;
            }
            reason = null;
            return false;
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            if ((parent.pawn.genes?.GetFirstGeneOfType<Gene_Resource_Metal>()).Value < Props.mineralCost)
            {
                return false;
            }
            return true;
        }
    }
}
