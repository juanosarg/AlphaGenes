
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace AlphaGenes
{
	public class JobDriver_UseGeneRemover : JobDriver
	{
		private int useDuration = -1;

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look(ref useDuration, "useDuration", 0);
		}

		public override void Notify_Starting()
		{
			base.Notify_Starting();
			useDuration = job.GetTarget(TargetIndex.A).Thing.TryGetComp<CompUsable>().Props.useDuration;
		}

		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return pawn.Reserve(job.targetA, job, 1, -1, null, errorOnFailed) && pawn.Reserve(job.targetB, job, 1, -1, null, errorOnFailed);
		}

		protected override IEnumerable<Toil> MakeNewToils()
		{
			job.count = 1;
			this.FailOnIncapable(PawnCapacityDefOf.Manipulation);
			yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.A);
			yield return Toils_Haul.StartCarryThing(TargetIndex.A, false, false, false);
			yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.B);


			Toil toil = Toils_General.Wait(useDuration);
			toil.WithProgressBarToilDelay(TargetIndex.B);
			toil.FailOnDespawnedNullOrForbidden(TargetIndex.B);
			toil.FailOnCannotTouch(TargetIndex.B, PathEndMode.Touch);
			if (job.targetB.IsValid)
			{
				toil.FailOnDespawnedOrNull(TargetIndex.B);

			}
			yield return toil;
			Toil use = new Toil();

			use.initAction = delegate
			{
				Pawn actor = use.actor;
				actor.CurJob.targetA.Thing.TryGetComp<CompTargetEffect_RemoveGene>().DoEffectOn(actor, job.targetB.Thing);


			};
			use.defaultCompleteMode = ToilCompleteMode.Instant;
			yield return use;
			yield break;

		}
	}
}
