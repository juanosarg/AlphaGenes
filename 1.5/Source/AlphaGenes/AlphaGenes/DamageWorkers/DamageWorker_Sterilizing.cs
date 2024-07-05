
using RimWorld;
using Verse;

namespace AlphaGenes
{
    public class DamageWorker_Sterilizing : DamageWorker
    {
      

        public override DamageResult Apply(DamageInfo dinfo, Thing victim)
        {
            DamageResult result = new DamageResult();
            Blight blight = victim as Blight;
           
            if (blight != null && !blight.Destroyed)
            {
                base.Apply(dinfo, victim);
                blight.Destroy();
                
            }
            Pawn pawn = victim as Pawn;
            if (pawn != null)
            {
                Hediff hediff = HediffMaker.MakeHediff(dinfo.Def.hediff, pawn);
                hediff.Severity = dinfo.Amount;
                pawn.health.AddHediff(hediff, null, dinfo);
            }
            return result;
        }
    }
}