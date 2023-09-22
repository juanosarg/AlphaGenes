
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace AlphaGenes
{
	public class HediffComp_GiveHediffLungRotAlternative : HediffComp
	{
		private HediffCompProperties_GiveHediffLungRotAlternative Props => (HediffCompProperties_GiveHediffLungRotAlternative)props;

		public override void CompPostPostAdd(DamageInfo? dinfo)
		{
		}

		public override bool CompDisallowVisible()
		{
			return true;
		}

		public override void CompPostTick(ref float severityAdjustment)
		{
			Pawn pawn = parent.pawn;
			if (!pawn.Spawned || pawn.NonHumanlikeOrWildMan() || !pawn.HasActiveGene(InternalDefOf.AG_LungRotStrength) || !(parent.Severity >= Props.minSeverity) || !pawn.Position.AnyGas(pawn.Map, GasType.RotStink) ||  !pawn.IsHashIntervalTick(Props.mtbCheckDuration) || !Rand.MTBEventOccurs(Props.mtbOverRotGasExposureCurve.Evaluate(parent.Severity), 2500f, Props.mtbCheckDuration))
			{
				return;
			}
			IEnumerable<BodyPartRecord> lungRotAffectedBodyParts = GasUtility.GetLungRotAffectedBodyParts(pawn);
			if (!lungRotAffectedBodyParts.Any())
			{
				return;
			}
			foreach (BodyPartRecord item in lungRotAffectedBodyParts)
			{
				pawn.health.AddHediff(InternalDefOf.AG_LungRotStrengthHediff, item);
			}
			
		}
	}
}