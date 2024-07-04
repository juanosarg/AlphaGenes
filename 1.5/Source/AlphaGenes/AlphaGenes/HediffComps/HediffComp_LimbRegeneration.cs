
using Verse;
using RimWorld;
using System.Collections.Generic;
using System.Linq;

namespace AlphaGenes
{
    class HediffComp_LimbRegeneration : HediffComp
    {

        public static List<BodyPartDef> validParts = new List<BodyPartDef>() { InternalDefOf.Torso, InternalDefOf.Shoulder, InternalDefOf.Arm, InternalDefOf.Hand, 
            InternalDefOf.Finger, InternalDefOf.Toe, InternalDefOf.Ear, InternalDefOf.Head, InternalDefOf.Nose, InternalDefOf.Neck, InternalDefOf.Leg, 
            InternalDefOf.Foot, InternalDefOf.Tongue
    };

        public HediffCompProperties_LimbRegeneration Props
        {
            get
            {
                return (HediffCompProperties_LimbRegeneration)this.props;
            }
        }
        public int tickCounter = 0;
        public const int initialRate = 60000;
        public int rate = initialRate;


        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterLimbRegen", 0, false);
            Scribe_Values.Look<int>(ref this.rate, "rate", 0, false);


        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            tickCounter++;

            if (tickCounter >= rate)
            {
                Pawn pawn = parent.pawn;

                if (pawn.health != null)
                {
                    List<Hediff_Injury> injuries = GetInjuries(pawn);

                    if (injuries.Count > 0)
                    {


                        Hediff_Injury injury = injuries.RandomElement();
                        injury.Severity = injury.Severity - Props.healAmount;


                    }
                    else
                    {
                        BodyPartRecord bodyPartRecord = FindFirstMissingBodyPart(pawn);
                        if (bodyPartRecord != null)
                        {

                            pawn.health.RestorePart(bodyPartRecord);
                            int num = (int)pawn.health.hediffSet.GetPartHealth(bodyPartRecord) - 1;
                            DamageInfo damageInfo = new DamageInfo(DamageDefOf.Cut, (float)num, 999f, -1f, null, bodyPartRecord, null, DamageInfo.SourceCategory.ThingOrUnknown, null, true, true);
                            damageInfo.SetAllowDamagePropagation(false);
                            pawn.TakeDamage(damageInfo);



                        }

                    }
                }
                rate = Props.rateInTicks.RandomInRange;
                tickCounter = 0;
            }

        }

        public List<Hediff_Injury> GetInjuries(Pawn pawn)
        {
            List<Hediff_Injury> injuries = new List<Hediff_Injury>();
            for (int i = 0; i < pawn.health.hediffSet.hediffs.Count; i++)
            {
                Hediff_Injury hediff_Injury = pawn.health.hediffSet.hediffs[i] as Hediff_Injury;
                if (hediff_Injury != null && validParts.Contains(hediff_Injury.Part.def))
                {
                    injuries.Add(hediff_Injury);
                }

            }
            return injuries;
        }

        public static BodyPartRecord FindFirstMissingBodyPart(Pawn pawn)
        {
            BodyPartRecord bodyPartRecord = null;
            foreach (Hediff_MissingPart missingPartsCommonAncestor in pawn.health.hediffSet.GetMissingPartsCommonAncestors())
            {
                if (!pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(missingPartsCommonAncestor.Part) && (bodyPartRecord == null || missingPartsCommonAncestor.Part.coverageAbsWithChildren > bodyPartRecord.coverageAbsWithChildren) && validParts.Contains(missingPartsCommonAncestor.Part.def))
                {
                    bodyPartRecord = missingPartsCommonAncestor.Part;
                }
            }
            return bodyPartRecord;
        }


    }
}
