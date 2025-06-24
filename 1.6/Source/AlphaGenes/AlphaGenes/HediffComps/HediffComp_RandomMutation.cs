
using System;
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

        public List<GeneDef> blacklist = new List<GeneDef>();

		public List<string> defnameStrings = new List<string>();

        public bool Active = false;

        public override void CompExposeData()
        {
            base.CompExposeData();
			Scribe_Collections.Look(ref this.genes, nameof(this.genes));
			Scribe_Collections.Look(ref this.blacklist, nameof(this.blacklist));
            Scribe_Collections.Look(ref this.defnameStrings, nameof(this.defnameStrings));
            Scribe_Values.Look(ref this.Active, nameof(this.Active));
            CompPostMake();

        }

        public override void CompPostMake()
        {
            base.CompPostMake();

            blacklist?.Clear();
            defnameStrings?.Clear();

            List<WretchBlacklistDef> allWretchBlacklistedGenes = DefDatabase<WretchBlacklistDef>.AllDefsListForReading;
			foreach (WretchBlacklistDef individualList in allWretchBlacklistedGenes)
			{
				if (!individualList.blackListedGenes.NullOrEmpty())
				{
                    blacklist.AddRange(individualList.blackListedGenes);
                }
                if (!individualList.blackListedDefNameStrings.NullOrEmpty())
                {
                    defnameStrings.AddRange(individualList.blackListedDefNameStrings);
                }
                

            }
		}


        public override IEnumerable<Gizmo> CompGetGizmos()
        {
            if (DebugSettings.ShowDevGizmos)
            {

                yield return new Command_Action
                {
                    defaultLabel = "Force mutation",
                    defaultDesc = "Use this to force a mutation on this pawn. For testing purposes.",
                    action = delegate
                    {
                        if (!genes.NullOrEmpty())
                        {
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
                    },

                };
            }

        }
       

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (!Active && this.parent.pawn.Map!=null) {
				Active = true;
				genes?.Clear();
				List<string> geneNamesToDisplay = new List<string>();
				for (int i = 0; i < Props.numberOfGenes; i++)
				{
					GeneDef gene = DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.exclusionTags?.Contains("AG_OnlyOnCharacterCreation") == false &&
					x.prerequisite==null && x.biostatArc == 0 && x.modContentPack?.PackageId != "vanillaracesexpanded.insector" && !defnameStrings.Any(s => x.defName.Contains(s))
                    && !blacklist.Contains(x)).RandomElement();
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