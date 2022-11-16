
using RimWorld;
using Verse;
using Verse.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.Sound;


namespace AlphaGenes
{
	public class CompTargetEffect_RemoveGene : CompTargetEffect
	{


		

		public CompProperties_TargetEffect_RemoveGene Props
		{
			get
			{
				return (CompProperties_TargetEffect_RemoveGene)this.props;
			}
		}

		

	

		public override void DoEffectOn(Pawn user, Thing target)
		{


			if (user.IsColonistPlayerControlled && user.CanReserveAndReach(target, PathEndMode.Touch, Danger.Deadly))
			{
				Pawn pawn = target as Pawn;
				for (int i = 0; i < 20; i++)
				{
					IntVec3 c;
					CellFinder.TryFindRandomReachableCellNear(pawn.Position, pawn.Map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);
					FilthMaker.TryMakeFilth(c, pawn.Map, ThingDefOf.Filth_Blood);

				}

				Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.AG_GeneRemovalComa);

				if (hediff==null && pawn.genes.GenesListForReading.Count > 0)
                {
					pawn.genes.RemoveGene(pawn.genes.GenesListForReading.RandomElement());
					user.carryTracker.DestroyCarriedThing();
					if (AlphaGenes_Mod.settings.AG_GeneRemovalComa)
					{
						pawn.health.AddHediff(InternalDefOf.AG_GeneRemovalComa);
					}
					
				}
				



				
			}





		}

		



	}
}
