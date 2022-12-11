using RimWorld;
using Verse;
using Verse.Sound;
using System;
using AnimalBehaviours;
using System.Collections.Generic;
using RimWorld.Planet;

namespace AlphaGenes
{
    public class HediffComp_Parasites : HediffComp
    {
        public List<GeneDef> motherGenes = new List<GeneDef>();

        public HediffCompProperties_Parasites Props
        {
            get
            {
                return (HediffCompProperties_Parasites)this.props;
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Collections.Look(ref this.motherGenes, nameof(this.motherGenes), LookMode.Def);
        }
       

        public override void Notify_PawnDied()
        {
            //base.Notify_PawnDied();
            float severityToTurn = Props.severityToTurn;

            Map map = this.parent.pawn.Corpse.Map;
            if (map != null && this.parent.Severity > severityToTurn)
            {

                Hatch();

                for (int i = 0; i < 20; i++)
                {
                    IntVec3 c;
                    CellFinder.TryFindRandomReachableCellNear(this.parent.pawn.Corpse.Position, map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);

                    FilthMaker.TryMakeFilth(c, this.parent.pawn.Corpse.Map, ThingDefOf.Filth_Blood);

                }


                SoundDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(this.parent.pawn.Corpse.Position, map, false));
                this.parent.pawn.Corpse.Destroy();

            }

        }

        public void Hatch()
        {
            try
            {
                PawnGenerationRequest request = new PawnGenerationRequest(hatcheeParent.kindDef, hatcheeFaction, PawnGenerationContext.NonPlayer, -1, forceGenerateNewPawn: false, allowDead: false, allowDowned: true, canGeneratePawnRelations: true, mustBeCapableOfViolence: false, 1f, forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowPregnant: false, allowFood: true, allowAddictions: true, inhabitant: false, certainlyBeenInCryptosleep: false, forceRedressWorldPawnIfFormerColonist: false, worldPawnFactionDoesntMatter: false, 0f, 0f, null, 1f, null, null, null, null, null, null, null, null, null, null, null, null, forceNoIdeo: false, forceNoBackstory: false, forbidAnyTitle: false, forceDead: false, null, null, null, null, null, 0f, DevelopmentalStage.Newborn);
                for (int i = 0; i < parent.stackCount; i++)
                {
                    Pawn pawn = PawnGenerator.GeneratePawn(request);
                    if (PawnUtility.TrySpawnHatchedOrBornPawn(pawn, parent))
                    {
                        if (pawn != null)
                        {
                            if (hatcheeParent != null)
                            {
                                if (pawn.playerSettings != null && hatcheeParent.playerSettings != null && hatcheeParent.Faction == hatcheeFaction)
                                {
                                    pawn.playerSettings.AreaRestriction = hatcheeParent.playerSettings.AreaRestriction;
                                }
                                if (pawn.RaceProps.IsFlesh)
                                {
                                    pawn.relations.AddDirectRelation(PawnRelationDefOf.Parent, hatcheeParent);
                                }
                            }
                            if (otherParent != null && (hatcheeParent == null || hatcheeParent.gender != otherParent.gender) && pawn.RaceProps.IsFlesh)
                            {
                                pawn.relations.AddDirectRelation(PawnRelationDefOf.Parent, otherParent);
                            }
                        }
                        if (parent.Spawned)
                        {
                            FilthMaker.TryMakeFilth(parent.Position, parent.Map, ThingDefOf.Filth_AmnioticFluid);
                        }

                        Find.LetterStack.ReceiveLetter("VGE_EggHatchedLabel".Translate(pawn.NameShortColored), "VGE_EggHatched".Translate(pawn.NameShortColored), LetterDefOf.PositiveEvent, (TargetInfo)pawn);


                        if (maleDominant)
                        {
                            if (fatherGenes?.Count > 0)
                            {
                                foreach (GeneDef gene in fatherGenes)
                                {
                                    pawn.genes.AddGene(gene, false);
                                }

                            }
                        }
                        else if (femaleDominant)
                        {
                            if (motherGenes?.Count > 0)
                            {
                                foreach (GeneDef gene in motherGenes)
                                {
                                    pawn.genes.AddGene(gene, false);
                                }

                            }
                        }
                        else
                        {
                            System.Random rand = new System.Random();
                            List<GeneDef> genesToAdd = new List<GeneDef>();
                            foreach (GeneDef gene in motherGenes)
                            {
                                if (fatherGenes.Contains(gene))
                                {
                                    genesToAdd.Add(gene);
                                }
                                else
                                {
                                    if (rand.NextDouble() > 0.5f)
                                    {
                                        genesToAdd.Add(gene);
                                    }
                                }
                            }
                            foreach (GeneDef gene in fatherGenes)
                            {
                                if (!motherGenes.Contains(gene))
                                {
                                    if (rand.NextDouble() > 0.5f)
                                    {
                                        if (!genesToAdd.Contains(gene)) { genesToAdd.Add(gene); }
                                    }
                                }
                            }
                            foreach (GeneDef gene in genesToAdd)
                            {
                                pawn.genes.AddGene(gene, false);
                            }

                        }



                    }
                    else
                    {
                        Find.WorldPawns.PassToWorld(pawn, PawnDiscardDecideMode.Discard);
                    }
                }
            }
            finally
            {
                parent.Destroy();
            }
        }



    }
}
