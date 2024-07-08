
using RimWorld;
using Verse;

namespace AlphaGenes
{
    public class DamageWorker_Freezing : DamageWorker_Frostbite
    {


        public override DamageResult Apply(DamageInfo dinfo, Thing victim)
        {
            base.Apply(dinfo, victim);
            DamageResult result = new DamageResult();

            Fire fire = victim as Fire;
            if (fire == null || fire.Destroyed)
            {
                Thing thing = victim?.GetAttachment(ThingDefOf.Fire);
                if (thing != null)
                {
                    fire = (Fire)thing;
                }
            }
            if (fire != null && !fire.Destroyed)
            {
                base.Apply(dinfo, victim);
                fire.fireSize -= dinfo.Amount * 0.01f;
                if (fire.fireSize < 0.1f)
                {
                    fire.Destroy();
                }
            }
            Pawn pawn = victim as Pawn;
            if (pawn != null)
            {
                Hediff hediff = HediffMaker.MakeHediff(InternalDefOf.AG_FreezingBreath, pawn);
                hediff.Severity = dinfo.Amount;
                pawn.health.AddHediff(hediff, null, dinfo);
            }
            return result;
        }
    }
}