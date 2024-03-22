using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;


namespace AlphaGenes
{
    public class Hediff_MineralFueled : HediffWithComps
    {
        public override bool ShouldRemove => pawn.genes?.GetFirstGeneOfType<Gene_Resource_Metal>() == null;

        public override bool Visible => false;

        public override void Tick()
        {
            if(gene.Value <= 0 && severityInt != 0)
            {
                Severity = 0f;
            }
            else if(severityInt == 0f && !pawn.health.hediffSet.HasHediff(InternalDefOf.AG_MineralCraving))
            {
                Severity = 1f;
            }
        }
        private Gene_Resource_Metal gene
        {
            get
            {
                if (cacheGene == null)
                {
                    cacheGene = pawn.genes?.GetFirstGeneOfType<Gene_Resource_Metal>();
                }
                return cacheGene;
            }
        }
        private Gene_Resource_Metal cacheGene;
    }
}
