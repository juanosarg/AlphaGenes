using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace AlphaGenes
{
    public class CompAbilityEffect_Stinger : CompAbilityEffect
    {
        private static readonly CachedTexture ReimplantIcon = new CachedTexture("UI/Icons/Genes/AG_InsectStinger");


        new public CompProperties_AbilityStinger Props
        {
            get
            {
                return (CompProperties_AbilityStinger)this.props;
            }
        }

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
                ReimplantXenogerm(parent.pawn, pawn);
                FleckMaker.AttachedOverlay(pawn, FleckDefOf.FlashHollow, new Vector3(0f, 0f, 0.26f));

                pawn.needs?.mood?.thoughts?.memories?.TryGainMemory((Thought_Memory)ThoughtMaker.MakeThought(InternalDefOf.AG_Implanted), parent.pawn);

                pawn.needs?.mood?.thoughts?.memories?.TryGainMemory((Thought_Memory)ThoughtMaker.MakeThought(InternalDefOf.AG_Implanted_Social), parent.pawn);

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

            if (pawn.genes.Xenotype == parent.pawn.genes.Xenotype && parent.pawn.genes.Xenotype != XenotypeDefOf.Baseliner)
            {
                if (throwMessages)
                {
                    Messages.Message("MessageCannotUseOnSameXenotype".Translate(pawn), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;
            }

            return base.Valid(target, throwMessages);
        }





        public void ReimplantXenogerm(Pawn caster, Pawn recipient)
        {

            QuestUtility.SendQuestTargetSignals(caster.questTags, "XenogermReimplanted", caster.Named("SUBJECT"));
            recipient.genes.SetXenotype(caster.genes.Xenotype);
            recipient.genes.xenotypeName = caster.genes.xenotypeName;
            recipient.genes.xenotypeName = caster.genes.xenotypeName;
            recipient.genes.iconDef = caster.genes.iconDef;

            if (!Props.endogenes)
            {
                recipient.genes.ClearXenogenes();
                foreach (Gene xenogene in caster.genes.Xenogenes)
                {
                    recipient.genes.AddGene(xenogene.def, xenogene: true);
                }            
                recipient.health.AddHediff(InternalDefOf.AG_XenogerminationComa);
            }
            else
            {
                foreach (Gene endogene in caster.genes.Endogenes)
                {
                    recipient.genes.AddGene(endogene.def, xenogene: false);
                }                
                recipient.health.AddHediff(InternalDefOf.AG_EndoimplantationComa);
            }
            if (!caster.genes.Xenotype.soundDefOnImplant.NullOrUndefined())
            {
                caster.genes.Xenotype.soundDefOnImplant.PlayOneShot(SoundInfo.InMap(recipient));
            }
            UpdateXenogermReplication(recipient);


        }

        public static void UpdateXenogermReplication(Pawn pawn)
        {

            Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.XenogermReplicating);
            if (firstHediffOfDef != null)
            {
                pawn.health.RemoveHediff(firstHediffOfDef);
            }
            pawn.health.AddHediff(HediffDefOf.XenogermReplicating);

        }




    }
}