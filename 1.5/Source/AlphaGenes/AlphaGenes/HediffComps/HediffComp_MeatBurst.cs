
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace AlphaGenes
{
    public class HediffComp_MeatBurst : HediffComp
    {
        public HediffCompProperties_MeatBurst Props => (HediffCompProperties_MeatBurst)props;

        public override void Notify_PawnDied(DamageInfo? dinfo, Hediff culprit = null)
        {
            base.Notify_PawnDied(dinfo, culprit);

            if (!ModLister.CheckAnomaly("Pawn dividing"))
            {
                return;
            }
            Pawn innerPawn = this.parent.pawn;

            Lord lord = innerPawn.GetLord();

            if (innerPawn == null)
            {
                return;
            }
            int dividePawnCount = Props.dividePawnCount;
            for (int i = 0; i < dividePawnCount; i++)
            {
                Pawn child = PawnGenerator.GeneratePawn(new PawnGenerationRequest(Props.dividePawnKindOptions.RandomElement(), innerPawn.Faction, PawnGenerationContext.NonPlayer, -1, forceGenerateNewPawn: true, allowDead: false, allowDowned: false, canGeneratePawnRelations: true, mustBeCapableOfViolence: false, 1f, forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowPregnant: false, allowFood: true, allowAddictions: true, inhabitant: false, certainlyBeenInCryptosleep: false, forceRedressWorldPawnIfFormerColonist: false, worldPawnFactionDoesntMatter: false, 0f, 0f, null, 1f, null, null, null, null, null, 0f));
                SpawnPawn(child, innerPawn, innerPawn.PositionHeld, innerPawn.MapHeld, lord);
            }
            foreach (PawnKindDef item in Props.dividePawnKindAdditionalForced)
            {
                Pawn child2 = PawnGenerator.GeneratePawn(new PawnGenerationRequest(item, innerPawn.Faction, PawnGenerationContext.NonPlayer, -1, forceGenerateNewPawn: true, allowDead: false, allowDowned: false, canGeneratePawnRelations: true, mustBeCapableOfViolence: false, 1f, forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowPregnant: false, allowFood: true, allowAddictions: true, inhabitant: false, certainlyBeenInCryptosleep: false, forceRedressWorldPawnIfFormerColonist: false, worldPawnFactionDoesntMatter: false, 0f, 0f, null, 1f, null, null, null, null, null, 0f));
                SpawnPawn(child2, innerPawn, innerPawn.PositionHeld, innerPawn.MapHeld, lord);
            }
            FleshbeastUtility.MeatSplatter(Props.divideBloodFilthCountRange.RandomInRange, innerPawn.PositionHeld, innerPawn.MapHeld, FleshbeastUtility.ExplosionSizeFor(innerPawn));
            FilthMaker.TryMakeFilth(innerPawn.PositionHeld, innerPawn.MapHeld, ThingDefOf.Filth_TwistedFlesh);
         

        }




        private void SpawnPawn(Pawn child, Pawn parent, IntVec3 position, Map map, Lord lord)
        {
            GenSpawn.Spawn(child, position, map, WipeMode.VanishOrMoveAside);
            lord?.AddPawn(child);
            CompInspectStringEmergence compInspectStringEmergence = child.TryGetComp<CompInspectStringEmergence>();
            if (compInspectStringEmergence != null)
            {
                compInspectStringEmergence.sourcePawn = parent;
            }
            FleshbeastUtility.SpawnPawnAsFlyer(child, map, position);
        }
    }
}