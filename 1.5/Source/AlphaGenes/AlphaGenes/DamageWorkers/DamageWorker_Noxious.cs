
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace AlphaGenes
{
    public class DamageWorker_Noxious : DamageWorker
    {


        public override DamageResult Apply(DamageInfo dinfo, Thing victim)
        {
            DamageResult result = new DamageResult();
           
            Pawn pawn = victim as Pawn;
            if (pawn != null)
            {
                Hediff hediff = HediffMaker.MakeHediff(dinfo.Def.hediff, pawn);
                hediff.Severity = dinfo.Amount;
                pawn.health.AddHediff(hediff, null, dinfo);
                pawn.TakeDamage(new DamageInfo(DamageDefOf.Blunt, 3, 0f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown));

                List<DamageDefAdditionalHediff> additionalHediffs = dinfo.Def.additionalHediffs;
                for (int i = 0; i < additionalHediffs.Count; i++)
                {
                    DamageDefAdditionalHediff damageDefAdditionalHediff = additionalHediffs[i];
                    if (damageDefAdditionalHediff.hediff == null)
                    {
                        continue;
                    }
                    float num = dinfo.Amount * damageDefAdditionalHediff.severityPerDamageDealt;
                    if (damageDefAdditionalHediff.victimSeverityScalingByInvBodySize)
                    {
                        num *= 1f / pawn.BodySize;
                    }
                    if (damageDefAdditionalHediff.victimSeverityScaling != null)
                    {
                        num *= (damageDefAdditionalHediff.inverseStatScaling ? Mathf.Max(1f - pawn.GetStatValue(damageDefAdditionalHediff.victimSeverityScaling), 0f) : pawn.GetStatValue(damageDefAdditionalHediff.victimSeverityScaling));
                    }
                   
                    if (num >= 0f)
                    {
                        Hediff hediff2 = HediffMaker.MakeHediff(damageDefAdditionalHediff.hediff, pawn);
                        hediff2.Severity = num;
                        pawn.health.AddHediff(hediff2, null, dinfo);
                       
                    }
                }



            }
            return result;
        }
    }
}