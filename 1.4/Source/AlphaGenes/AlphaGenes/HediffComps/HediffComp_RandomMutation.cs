
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using static HarmonyLib.Code;


namespace AlphaGenes
{
	public class HediffComp_RandomMutation : HediffComp
	{
		private HediffCompProperties_RandomMutation Props => (HediffCompProperties_RandomMutation)props;

		public List<GeneDef> genes = new List<GeneDef>();

        public List<GeneDef> blacklist = new List<GeneDef>() { InternalDefOf.AG_UnstableMutation,InternalDefOf.AG_UnstableMutationMajor,InternalDefOf.AG_UnstableMutationCatastrophic,
		InternalDefOf.AlphaGenes_Randomizer,InternalDefOf.AlphaGenes_ExoticOrganism, InternalDefOf.AG_Sensitivity_Lethal,InternalDefOf.AG_Teratogenesis,
		InternalDefOf.AG_MinorAnimalSummon_Randomizer,InternalDefOf.AG_AnimalSummon_Randomizer,InternalDefOf.AG_MajorAnimalSummon_Randomizer};

        public bool Active = false;

        public override void CompExposeData()
        {
            base.CompExposeData();
			Scribe_Collections.Look(ref this.genes, nameof(this.genes));
			Scribe_Values.Look(ref this.Active, nameof(this.Active));

		}

		public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (!Active && this.parent.pawn.Map!=null) {
				Active = true;
				genes.Clear();
				List<string> geneNamesToDisplay = new List<string>();
				for (int i = 0; i < Props.numberOfGenes; i++)
				{
					GeneDef gene = DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.exclusionTags?.Contains("AG_OnlyOnCharacterCreation") == false &&
					x.prerequisite==null && x.biostatArc == 0 && !x.defName.Contains("VREHT_") && !x.defName.Contains("VREA_") && !x.defName.Contains("AlphaGenes_") && 
					!x.defName.Contains("AG_InnatePsylink") && !blacklist.Contains(x)).RandomElement();
					genes.Add(gene);
					geneNamesToDisplay.Add(gene.LabelCap);
					this.parent.pawn.genes?.AddGene(gene, true);

				}
				if (parent.pawn.Faction == Faction.OfPlayer && !AlphaGenes_Mod.settings.AG_DisableMutationsMessage)
				{
					Messages.Message("AG_RandomGenesGained".Translate(parent.pawn.LabelShortCap, geneNamesToDisplay.ToCommaList()), this.parent.pawn, MessageTypeDefOf.PositiveEvent, true);
				}
			}

            if (Props.recurrent)
            {
                if (this.parent.pawn.IsHashIntervalTick(Props.period)) {

                    if (!genes.NullOrEmpty()) {
						for (int i = 0; i < Props.numberOfGenes; i++)
						{
							if (this.parent.pawn.genes?.GetGene(genes[i]) != null)
							{
								this.parent.pawn.genes?.RemoveGene(this.parent.pawn.genes.GetGene(genes[i]));
							}

						}
						genes.Clear();
					}
					

					Active = false;

				}
            }
			

		}
		

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();

			for (int i = 0; i < genes.Count; i++)
			{
				if (this.parent.pawn.genes?.GetGene(genes[i]) != null)
				{
					this.parent.pawn.genes?.RemoveGene(this.parent.pawn.genes.GetGene(genes[i]));
				}

			}
			Active = false;
			genes.Clear();




		}




    }
}