
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace AlphaGenes
{
	public class HediffComp_SeverityFromMinerals : HediffComp
	{
		private HediffCompProperties_SeverityFromMinerals Props => (HediffCompProperties_SeverityFromMinerals)props;

        public override bool CompShouldRemove => Pawn.genes?.GetFirstGeneOfType<Gene_Resource_Metal>() == null;

        public override void CompPostPostAdd(DamageInfo? dinfo)
		{
		}

		public override bool CompDisallowVisible()
		{
			return true;
		}
		private Gene_Resource_Metal gene
        {
            get
            {
				if(cacheGene == null)
                {
					cacheGene = Pawn.genes?.GetFirstGeneOfType<Gene_Resource_Metal>();
				}
				return cacheGene;
            }
        }
		public override void CompPostTick(ref float severityAdjustment)
		{			
			base.CompPostTick(ref severityAdjustment);
			severityAdjustment += ((gene.Value > 0f ? Props.severityPerHourHemogen : Props.severityPerHourEmpty) / 2500f);
		}
		private Gene_Resource_Metal cacheGene;
	}
}