



using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

using AnimalBehaviours;


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
                foreach(Gene gene in genes)
                {
                    if (gene.def.defName.Contains("AlphaGenes_Animal")&& gene.Active)
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
                Pawn pawnCreated = PawnGenerator.GeneratePawn(pawnToMake, pawn.Faction);
                GenSpawn.Spawn(pawnCreated, target.Cell, pawn.Map, Rot4.South);
                pawnCreated.mindState.mentalStateHandler.TryStartMentalState(InternalDefOf.AG_SelectiveManhunter, null, true);
                pawnCreated.health.AddHediff(InternalDefOf.AG_TemporarySummon);
               
            }

               
           
        }

        
    }
}
