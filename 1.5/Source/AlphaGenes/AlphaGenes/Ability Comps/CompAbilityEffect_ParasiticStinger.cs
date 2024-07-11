using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace AlphaGenes
{
    public class CompAbilityEffect_ParasiticStinger : CompAbilityEffect
    {
        private static readonly CachedTexture ReimplantIcon = new CachedTexture("UI/Icons/Genes/AG_ParasiticStinger");

        private new CompProperties_AbilityParasiticStinger Props => (CompProperties_AbilityParasiticStinger)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (!ModLister.CheckBiotech("xenogerm reimplantation"))
            {
                return;
            }
            base.Apply(target, dest);
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {
                HealthUtility.DamageUntilDowned(pawn);

                pawn.health.AddHediff(Props.hediffDef);

                Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(Props.hediffDef);

                List<GeneDef> genesToPass = new List<GeneDef>();

                if (Props.endogenes) {
                    foreach (Gene gene in parent.pawn.genes.Endogenes)
                    {
                        genesToPass.Add(gene.def);
                    }
                } else {
                    foreach (Gene gene in parent.pawn.genes.Xenogenes)
                    {
                        genesToPass.Add(gene.def);
                    }
                }
                

                HediffComp_Parasites comp = hediff.TryGetComp<HediffComp_Parasites>();

                comp.motherGenes = genesToPass;
                comp.motherDef = this.parent.pawn.kindDef;
                comp.mother = parent.pawn;
                comp.motherFaction = parent.pawn.Faction;
                comp.endogenes = Props.endogenes;

                FleckMaker.AttachedOverlay(pawn, FleckDefOf.FlashHollow, new Vector3(0f, 0f, 0.26f));

                pawn.needs?.mood?.thoughts?.memories?.TryGainMemory((Thought_Memory)ThoughtMaker.MakeThought(InternalDefOf.AG_Parasite), parent.pawn);

                pawn.needs?.mood?.thoughts?.memories?.TryGainMemory((Thought_Memory)ThoughtMaker.MakeThought(InternalDefOf.AG_Parasite_Social), parent.pawn);

                for (int i = 0; i < 20; i++)
                {
                    IntVec3 c;
                    CellFinder.TryFindRandomReachableCellNearPosition(pawn.Position, pawn.Position, pawn.Map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);
                    FilthMaker.TryMakeFilth(c, pawn.Map, ThingDefOf.Filth_Blood);

                }
                if (pawn.Faction != null && pawn.Faction != Faction.OfPlayer)
                {

                    pawn.Faction.SetRelationDirect(Faction.OfPlayer, FactionRelationKind.Hostile);
                }

            }
        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            Pawn pawn = target.Pawn;
            if (pawn == null)
            {
                return base.Valid(target, throwMessages);
            }

            if (pawn.HostileTo(parent.pawn))
            {
                if (throwMessages)
                {
                    Messages.Message("AG_CannotBeUsedAsWeapon".Translate(pawn), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;
            }

            if (pawn.HasActiveGene(GeneDefOf.Deathless))
            {
                if (throwMessages)
                {
                    Messages.Message("AG_CannotBeUsedOnDeathless".Translate(pawn), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;
            }



            return base.Valid(target, throwMessages);
        }



       

       



    }
}