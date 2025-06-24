
using Verse;
using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using System.Linq;



namespace AlphaGenes
{
    class CompTeratogenicHediff : CompAbilityEffect
    {

        new public CompProperties_TeratogenicHediff Props
        {
            get
            {
                return (CompProperties_TeratogenicHediff)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn pawn = parent.pawn;
            Pawn targetPawn;
            if (Props.targetOther)
            {
                targetPawn = (Pawn)target;
            }
            else
            {
                targetPawn = parent.pawn;
            }

            HediffDef hediffDef = HediffDefOf.Carcinoma;
            List<Hediff> carcinomas = new List<Hediff>();


            foreach (Hediff hediffToCheck in pawn.health.hediffSet.hediffs)
            {
                if (hediffToCheck.def == hediffDef)
                {
                    carcinomas.Add(hediffToCheck);

                }

            }
            int cancerCount = carcinomas.Count();

            if (cancerCount > 0)
            {
                targetPawn.health.AddHediff(Props.hediff);
                Hediff newHediff = targetPawn.health.hediffSet.GetFirstHediffOfDef(Props.hediff);
                newHediff.Severity = 0.1f * cancerCount;
                newHediff.TryGetComp<HediffComp_Disappears>().ticksToDisappear = 60000 * cancerCount;
            }

            foreach (Hediff carcinoma in carcinomas)
            {
                pawn.health.RemoveHediff(carcinoma);

            }






        }






    }
}
