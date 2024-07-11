
using Verse;
using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;




namespace AlphaGenes
{
    class CompTeratogenicHealing : CompAbilityEffect
    {

        new public CompProperties_TeratogenicHealing Props
        {
            get
            {
                return (CompProperties_TeratogenicHealing)this.props;
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
                if(hediffToCheck.def == hediffDef)
                {
                    carcinomas.Add(hediffToCheck);

                    List<Hediff_Injury> injuries = new List<Hediff_Injury>();
                    foreach (Hediff possibleInjuryHediff in targetPawn.health.hediffSet.hediffs)
                    {
                        if(possibleInjuryHediff as Hediff_Injury != null)
                        {
                            injuries.Add((Hediff_Injury)possibleInjuryHediff);
                        }
                    }
                    if(injuries.Count > 0)
                    {
                        injuries.RandomElement().Severity -= 20f;
                    }


                }

            }
            foreach (Hediff carcinoma in carcinomas)
            { 
                pawn.health.RemoveHediff(carcinoma);
            
            }






            }






    }
}
