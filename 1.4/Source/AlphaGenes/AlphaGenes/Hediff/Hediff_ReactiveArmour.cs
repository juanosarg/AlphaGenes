using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using UnityEngine;


namespace AlphaGenes
{
    public class Hediff_ReactiveArmour : HediffWithComps
    {
        public override bool ShouldRemove => gene.Value <= 0 || !ability.active;

        public override bool Visible => true;

        public override void Notify_PawnPostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            base.Notify_PawnPostApplyDamage(dinfo, totalDamageDealt);
            if (!dinfo.IgnoreArmor && (dinfo.Def.armorCategory.armorRatingStat == StatDefOf.ArmorRating_Blunt || dinfo.Def.armorCategory.armorRatingStat == StatDefOf.ArmorRating_Sharp))
            {
                float drain = Mathf.Clamp(((totalDamageDealt / 5f)/100f), 0.002f, 0.03f); //So a bunch of low damage attacks doesnt drain insane amounts fast
                gene.Value -= drain;
            }
        }
        //Stages of defense are based on gene value
        public override void Tick()
        {
            base.Tick();
            Severity = gene.Value;
        }
        public Gene_Resource_Metal gene
        {
            get
            {
                if (cacheGene == null)
                {
                    cacheGene = pawn.genes?.GetFirstGeneOfType<Gene_Resource_Metal>();
                }
                return cacheGene;
            }
            set => cacheGene = value;
        }
        public Ability_ReactiveArmour ability;
        private Gene_Resource_Metal cacheGene;
    }
}
