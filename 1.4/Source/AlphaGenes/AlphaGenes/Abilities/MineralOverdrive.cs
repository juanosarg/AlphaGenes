using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
using RimWorld.Planet;
using HarmonyLib;

namespace AlphaGenes
{
    public class Ability_MineralOverdrive : Ability
    {
        public bool active;
        
        public Ability_MineralOverdrive(Pawn pawn) : base(pawn)
        {
        }

        public Ability_MineralOverdrive(Pawn pawn,AbilityDef def) : base(pawn,def)
        {
            this.def = def;
            this.pawn = pawn;
        }
        public override IEnumerable<Command> GetGizmos()
        {
            if (!pawn.Drafted)
            {
                if (active)
                {
                    yield return new CommandAbilityToggle(pawn, this)
                    {
                        defaultLabel = "AG_OverdriveOff".Translate(),
                        toggleAction = () =>
                        {
                            active = false;
                            Activate(pawn, LocalTargetInfo.Invalid);
                        },
                        isActive = () => active,
                        icon = ContentFinder<Texture2D>.Get("UI/Abilities/AG_MineralOverdrive"),
                    };
                }
                else
                {
                    yield return new CommandAbilityToggle(pawn, this)
                    {
                        defaultLabel = "AG_OverdriveOn".Translate(),
                        toggleAction = () =>
                        {
                            active = true;
                            Activate(pawn, LocalTargetInfo.Invalid);
                        },
                        isActive = () => active,
                        icon = ContentFinder<Texture2D>.Get("UI/Abilities/AG_MineralOverdrive"),
                    };
                }
            }

        }
        public override bool Activate(LocalTargetInfo target, LocalTargetInfo dest)
        {
            bool result = base.Activate(target, dest);
            ApplyHediff(target, dest);
            if (active)
            {
                StartCooldown(300);//main cooldown doesnt start until deactived
            }
            return result;
        }
        public override void AbilityTick()
        {
            base.AbilityTick();
            if (active)
            {
                if (Gene.Value <= 0.15f || pawn.Downed || pawn.Drafted)
                {
                    active = false;
                }
                else if(pawn.IsHashIntervalTick(300)) //full minerals would last 12 hours
                {
                    Gene.Value += -0.01f;
                }
            }
        }
        //ability devs making apply effect not apply unless you have effect comps. I hate you.
        public void ApplyHediff(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target.Thing is Pawn pawn)
            {
                if (active) //removal should be handled by the hediff
                {
                    var hediff = HediffMaker.MakeHediff(InternalDefOf.AG_MineralOverdriveHediff, pawn) as Hediff_MineralOverdrive;
                    hediff.ability = this;
                    hediff.gene = Gene;
                    pawn.health.AddHediff(hediff);
                }
                else
                {
                    var hediff = pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.AG_ReactiveArmourHediff);
                    if(hediff != null)
                    {
                        pawn.health.RemoveHediff(hediff);
                    }                    
                }
            }
        }
        public Gene_Resource_Metal Gene
        {
            get
            {
                if(cachedGene == null)
                {
                    cachedGene = pawn.genes.GetFirstGeneOfType<Gene_Resource_Metal>();
                }
                return cachedGene;
            }
        }
        private Gene_Resource_Metal cachedGene;
    }



}
