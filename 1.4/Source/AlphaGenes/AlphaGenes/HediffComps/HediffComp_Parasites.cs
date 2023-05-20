using RimWorld;
using Verse;
using Verse.Sound;
using System;
using AnimalBehaviours;
using System.Collections.Generic;
using RimWorld.Planet;
using System.Linq;

namespace AlphaGenes
{
    public class HediffComp_Parasites : HediffComp
    {
        public List<GeneDef> motherGenes = new List<GeneDef>();
        public Pawn mother;
        public Faction motherFaction;
        public PawnKindDef motherDef;
        public bool endogenes = false;


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
            Scribe_References.Look(ref this.mother, nameof(this.mother));
            Scribe_References.Look(ref this.motherFaction, nameof(this.motherFaction));
            Scribe_Defs.Look(ref this.motherDef, nameof(this.motherDef));
            Scribe_Values.Look(ref this.endogenes, nameof(this.endogenes));



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
                //this.parent.pawn.Corpse.Destroy();

            }

        }

        public void Hatch()
        {
            try
            {
                PawnGenerationRequest request;
                if (mother != null && !mother.Dead)
                {
                    request = new PawnGenerationRequest(mother.kindDef, mother.Faction, PawnGenerationContext.NonPlayer, -1, forceGenerateNewPawn: false, allowDead: false, allowDowned: true, canGeneratePawnRelations: true, mustBeCapableOfViolence: false, 1f, forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowPregnant: false, allowFood: true, allowAddictions: true, inhabitant: false, certainlyBeenInCryptosleep: false, forceRedressWorldPawnIfFormerColonist: false, worldPawnFactionDoesntMatter: false, 0f, 0f, null, 1f, null, null, null, null, null, null, null, null, null, null, null, null, forceNoIdeo: false, forceNoBackstory: false, forbidAnyTitle: false, forceDead: false, null, null, null, null, null, 0f, DevelopmentalStage.Newborn);

                }
                else
                {
                    request = new PawnGenerationRequest(motherDef, motherFaction, PawnGenerationContext.NonPlayer, -1, forceGenerateNewPawn: false, allowDead: false, allowDowned: true, canGeneratePawnRelations: true, mustBeCapableOfViolence: false, 1f, forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowPregnant: false, allowFood: true, allowAddictions: true, inhabitant: false, certainlyBeenInCryptosleep: false, forceRedressWorldPawnIfFormerColonist: false, worldPawnFactionDoesntMatter: false, 0f, 0f, null, 1f, null, null, null, null, null, null, null, null, null, null, null, null, forceNoIdeo: false, forceNoBackstory: false, forbidAnyTitle: false, forceDead: false, null, null, null, null, null, 0f, DevelopmentalStage.Newborn);

                }


                Pawn pawn = PawnGenerator.GeneratePawn(request);
                if (PawnUtility.TrySpawnHatchedOrBornPawn(pawn, this.parent.pawn.Corpse))
                {
                    if (pawn != null)
                    {
                        if (mother != null)
                        {
                            if (pawn.playerSettings != null && mother.playerSettings != null)
                            {
                                pawn.playerSettings.AreaRestriction = mother.playerSettings.AreaRestriction;
                            }
                            if (pawn.RaceProps.IsFlesh)
                            {
                                pawn.relations.AddDirectRelation(PawnRelationDefOf.Parent, mother);
                            }
                        }

                    }


                    Find.LetterStack.ReceiveLetter("AG_ParasitesHatchedLabel".Translate(pawn.NameShortColored), "AG_ParasitesHatched".Translate(pawn.NameShortColored), LetterDefOf.PositiveEvent, (TargetInfo)pawn);

                    System.Random rand = new System.Random();
                    List<GeneDef> genesToAdd = new List<GeneDef>();
                    foreach (Gene gene in this.parent.pawn.genes?.GenesListForReading)
                    {
                        if (rand.NextDouble() > 0.75f)
                        {
                            genesToAdd.Add(gene.def);
                        }
                    }

                    foreach (GeneDef gene in genesToAdd)
                    {
                        pawn.genes.AddGene(gene, !endogenes);
                    }
                    foreach (GeneDef gene in motherGenes)
                    {
                        if (!pawn.genes.HasGene(gene)) { pawn.genes.AddGene(gene, !endogenes);}
                        
                    }





                }
                else
                {
                    Find.WorldPawns.PassToWorld(pawn, PawnDiscardDecideMode.Discard);
                }




            }
            catch (Exception) { }



        }
    }
}
    

