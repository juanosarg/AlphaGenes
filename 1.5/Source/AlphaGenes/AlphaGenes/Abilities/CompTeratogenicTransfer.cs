
using Verse;
using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using System.Linq;



namespace AlphaGenes
{
    class CompTeratogenicTransfer : CompAbilityEffect
    {

        new public CompProperties_TeratogenicTransfer Props
        {
            get
            {
                return (CompProperties_TeratogenicTransfer)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn pawn = parent.pawn;
            Pawn targetPawn = (Pawn)target;

            HediffDef hediffDef = HediffDefOf.Carcinoma;
            List<Hediff> carcinomas = new List<Hediff>();


            foreach (Hediff hediffToCheck in pawn.health.hediffSet.hediffs)
            {
                if (hediffToCheck.def == hediffDef)
                {
                    carcinomas.Add(hediffToCheck);



                    IEnumerable<BodyPartRecord> source = targetPawn.health.hediffSet.GetNotMissingParts();
                    source = source.Where((BodyPartRecord p) => p.def.alive);

                    source = source.Where((BodyPartRecord p) => !targetPawn.health.hediffSet.HasHediff(hediffDef, p) && !targetPawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(p));
                    if (source.Any())
                    {
                        BodyPartRecord partRecord = source.RandomElementByWeight((BodyPartRecord x) => x.coverageAbs);
                        Hediff hediff = HediffMaker.MakeHediff(hediffDef, targetPawn, partRecord);
                        targetPawn.health.AddHediff(hediff);

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
