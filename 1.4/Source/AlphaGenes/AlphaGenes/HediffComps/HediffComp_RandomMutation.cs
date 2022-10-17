
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace AlphaGenes
{
	public class HediffComp_RandomMutation : HediffComp
	{
		private HediffCompProperties_RandomMutation Props => (HediffCompProperties_RandomMutation)props;

		public GeneDef gene;

        public override void CompExposeData()
        {
            base.CompExposeData();
			Scribe_Defs.Look(ref this.gene, nameof(this.gene));
		}

        public override void CompPostPostAdd(DamageInfo? dinfo)
		{
			base.CompPostPostAdd(dinfo);
			
			gene = DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.exclusionTags?.Contains("AG_OnlyOnCharacterCreation") == false).RandomElement();
			this.parent.pawn.genes?.AddGene(gene,true);
			Messages.Message("AG_RandomGeneGained".Translate(gene.LabelCap), this.parent.pawn, MessageTypeDefOf.PositiveEvent, true);
		}

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();
            if (this.parent.pawn.genes?.GetGene(gene) != null)
            {
				this.parent.pawn.genes?.RemoveGene(this.parent.pawn.genes.GetGene(gene));
			}
			
		}




    }
}