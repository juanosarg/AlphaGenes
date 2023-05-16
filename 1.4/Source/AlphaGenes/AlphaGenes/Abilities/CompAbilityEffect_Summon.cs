



using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

using AnimalBehaviours;
using static RimWorld.MechClusterSketch;


namespace AlphaGenes
{
    [StaticConstructorOnStartup]
    public class CompAbilityEffect_Summon : CompAbilityEffect
    {

        private new CompProperties_AbilitySummon Props => (CompProperties_AbilitySummon)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            Pawn pawn = parent.pawn;
            List<Gene> genes = pawn.genes?.GenesListForReading;
            PawnKindDef pawnToMake = null;

            if (genes != null)
            {
                foreach (Gene gene in genes)
                {
                    if (gene.def.defName.Contains("AlphaGenes_Animal") && gene.Active)
                    {
                        SummoningGeneDefExtension extension = gene.def.GetModExtension<SummoningGeneDefExtension>();
                        if (extension != null)
                        {
                            pawnToMake = extension.pawn;
                            break;
                        }

                    }

                }
            }
            if (pawnToMake != null)
            {
                if (Props.isMechanoid)
                {
                    if (parent.pawn.mechanitor == null)
                    {
                        Messages.Message("AM_OnlyMechanitorCanUse".Translate(pawnToMake.LabelCap), parent.pawn, MessageTypeDefOf.RejectInput, historical: false);
                        return;
                    }

                    int num = parent.pawn.mechanitor.TotalBandwidth - parent.pawn.mechanitor.UsedBandwidth;
                    float statValue = pawnToMake.race.GetStatValueAbstract(StatDefOf.BandwidthCost);
                    if ((float)num < statValue)
                    {
                        Messages.Message("AM_NotEnoughBandwidth".Translate(pawnToMake.LabelCap), parent.pawn, MessageTypeDefOf.RejectInput, historical: false);
                        return;
                    }
                    Pawn pawnCreated = PawnGenerator.GeneratePawn(pawnToMake, pawn.Faction);
                    GenSpawn.Spawn(pawnCreated, target.Cell, pawn.Map, Rot4.South);

                    pawnCreated.GetOverseer()?.relations.RemoveDirectRelation(PawnRelationDefOf.Overseer, pawnCreated);
                    parent.pawn.relations.AddDirectRelation(PawnRelationDefOf.Overseer, pawnCreated);
                    pawnCreated.health.AddHediff(InternalDefOf.AG_TemporarySummonMech);




                }
                else
                {
                    Pawn pawnCreated = PawnGenerator.GeneratePawn(pawnToMake, pawn.Faction);
                    GenSpawn.Spawn(pawnCreated, target.Cell, pawn.Map, Rot4.South);
                    pawnCreated.mindState.mentalStateHandler.TryStartMentalState(InternalDefOf.AG_SelectiveManhunter, null, true);
                    pawnCreated.health.AddHediff(InternalDefOf.AG_TemporarySummon);
                }



            }



        }


    }
}
