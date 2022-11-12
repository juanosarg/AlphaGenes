using RimWorld;
using Verse;

namespace AlphaGenes
{

	public class Rat_Spawning_Filth : Filth
	{
		

		

		public override void Tick()
		{
			PawnGenerationRequest request = new PawnGenerationRequest(InternalDefOf.Rat, null, PawnGenerationContext.NonPlayer, -1, false, true, false, false, true, 1f, false, false, true, true, false, false);
			Pawn pawn = PawnGenerator.GeneratePawn(request);
			GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(this.Position, this.Map, 3, null), this.Map, WipeMode.Vanish);
			Destroy();
			
		}

		
	}


}
