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
    public class Ability_ReactiveArmour : Ability
    {
        public bool active;
        
        public Ability_ReactiveArmour(Pawn pawn) : base(pawn)
        {
        }

        public Ability_ReactiveArmour(Pawn pawn,AbilityDef def) : base(pawn,def)
        {
            this.def = def;
            this.pawn = pawn;
        }
        public override IEnumerable<Command> GetGizmos()
        {
            if (active)
            {
                yield return new CommandAbilityToggle(pawn, this)
                {
                    defaultLabel = "AG_ReativeArmourOff".Translate(),
                    toggleAction = () =>
                    {
                        active = false;
                        Activate(pawn, LocalTargetInfo.Invalid);
                    },
                    isActive = () => active,
                    icon = ContentFinder<Texture2D>.Get("UI/Abilities/AG_ReactiveArmourOn"), //**Todo make the great sarg draw :P
                };
            }
            else
            {
                yield return new CommandAbilityToggle(pawn, this)
                {
                    defaultLabel = "AG_ReativeArmourOn".Translate(),
                    toggleAction = () =>
                    {
                        active = true;
                        Activate(pawn, LocalTargetInfo.Invalid);
                    },
                    isActive = () => active,
                    icon = ContentFinder<Texture2D>.Get("UI/Abilities/AG_ReactiveArmourOff"),
                };
            }

        }
        public override bool Activate(LocalTargetInfo target, LocalTargetInfo dest)
        {
            bool result = base.Activate(target, dest);
            ApplyHediff(target, dest);
            return result;
        }
        public override void AbilityTick()
        {
            base.AbilityTick();
            if (active)
            {
                if (Gene.Value <= 0 || pawn.Downed || !pawn.Drafted)
                {
                    active = false;
                }
                else if(pawn.IsHashIntervalTick(120)) //Every 2 IRL seconds at 60 tps
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
                    var hediff = HediffMaker.MakeHediff(InternalDefOf.AG_ReactiveArmourHediff, pawn) as Hediff_ReactiveArmour;
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
    //Based on VFEP siege
    [StaticConstructorOnStartup]
    public class CommandAbilityToggle : Command_Toggle
    {
        public static readonly Texture2D CooldownTex =
            SolidColorMaterials.NewSolidColorTexture(new Color(1f, 1f, 1f, 0.1f));

        public Ability ability;

        public Pawn pawn;

        public CommandAbilityToggle(Pawn pawn, Ability ability)
        {
            
            var reason = "";
            this.pawn = pawn;
            this.ability = ability;
            defaultLabel = ability.def.LabelCap;
            defaultDesc = ability.def.description;
            icon = ability.def.uiIcon;
            disabled = ability.GizmoDisabled(out reason);
            disabledReason = reason.Colorize(Color.red);
            Order = 10f;
            
        }

        protected override GizmoResult GizmoOnGUIInt(Rect butRect, GizmoRenderParms parms)
        {
            var result = base.GizmoOnGUIInt(butRect, parms);
            int cooldown = ability.def.cooldownTicksRange.min;
            if (disabled && cooldown > Find.TickManager.TicksGame)
            {
                GUI.DrawTexture(butRect.RightPartPixels(butRect.width * ((float)(ability.CooldownTicksRemaining) / cooldown)),CooldownTex);
            }               
                    
            return result;
        }
    }


}
