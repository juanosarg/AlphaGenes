
using RimWorld;
using Verse;

namespace AlphaGenes
{
    public class DamageWorker_BansheeScream : DamageWorker
    {


        public override DamageResult Apply(DamageInfo dinfo, Thing victim)
        {
            DamageResult result = new DamageResult();

            Pawn pawn = victim as Pawn;
            if (pawn != null && pawn != dinfo.Instigator)
            {
                DamageInfo dinfo3 = dinfo;
                dinfo3.Def = DamageDefOf.Stun;
                dinfo3.SetAmount(3);
                pawn.TakeDamage(dinfo3);
                Hediff hediff = HediffMaker.MakeHediff(dinfo.Def.hediff, pawn);
                hediff.Severity = dinfo.Amount;
                pawn.health.AddHediff(hediff, null, dinfo);
                base.Apply(dinfo, victim);
            }

            
            return result;
        }
    }
}