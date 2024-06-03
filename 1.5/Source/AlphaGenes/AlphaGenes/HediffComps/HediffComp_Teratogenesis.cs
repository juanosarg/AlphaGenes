
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using static HarmonyLib.Code;


namespace AlphaGenes
{
    public class HediffComp_Teratogenesis : HediffComp
    {
        private HediffCompProperties_Teratogenesis Props => (HediffCompProperties_Teratogenesis)props;

        public int tickCounter =0;
        public int tickInterval;

        public override void CompExposeData()
        {
            base.CompExposeData();
          
            Scribe_Values.Look(ref this.tickCounter, nameof(this.tickCounter));

        }

        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            base.CompPostPostAdd(dinfo);
            tickInterval = Props.period.min;

        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            tickCounter++;
            if (tickCounter > tickInterval && parent.pawn.Map!=null)
            {

                if(this.parent.pawn.DevelopmentalStage == DevelopmentalStage.Adult)
                {
                    HediffDef hediffDef = HediffDefOf.Carcinoma;
                    if (parent.pawn.health.hediffSet.GetHediffCount(hediffDef) < 10)
                    {
                        IEnumerable<BodyPartRecord> source = parent.pawn.health.hediffSet.GetNotMissingParts();
                        source = source.Where((BodyPartRecord p) => p.def.alive);

                        source = source.Where((BodyPartRecord p) => !parent.pawn.health.hediffSet.HasHediff(hediffDef, p) && !parent.pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(p));
                        if (source.Any())
                        {
                            BodyPartRecord partRecord = source.RandomElementByWeight((BodyPartRecord x) => x.coverageAbs);
                            Hediff hediff = HediffMaker.MakeHediff(hediffDef, parent.pawn, partRecord);
                            parent.pawn.health.AddHediff(hediff);

                        }
                    }
                }
               
                
                
               

                tickInterval = Props.period.RandomInRange;
                tickCounter = 0;
            }


        }


       




    }
}