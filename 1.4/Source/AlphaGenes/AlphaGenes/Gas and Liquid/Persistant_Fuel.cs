using RimWorld;
using Verse;

namespace AlphaGenes
{

	public class Persistant_Fuel : Filth
	{
		private int spawnTick;

		public int dissapearsIn;

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look(ref spawnTick, "spawnTick", 0);
			Scribe_Values.Look(ref dissapearsIn, "dissapearsIn", 0);
		}

		public override void SpawnSetup(Map map, bool respawningAfterLoad)
		{
			base.SpawnSetup(map, respawningAfterLoad);
			spawnTick = Find.TickManager.TicksGame;
			dissapearsIn = (int)this.def.filth.disappearsInDays.RandomInRange * GenDate.TicksPerDay;
		}

		public void Refill()
		{
			spawnTick = Find.TickManager.TicksGame;
		}

		public override void Tick()
		{
			if (spawnTick + dissapearsIn < Find.TickManager.TicksGame)
			{
				Destroy();
			}
		}

		public override void ThickenFilth()
		{
			base.ThickenFilth();
			Refill();
		}
	}


}
