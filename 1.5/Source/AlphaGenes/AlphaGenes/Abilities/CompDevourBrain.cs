using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace AlphaGenes
{
    public class CompDevourBrain : CompAbilityEffect
    {
        public Dictionary<SkillDef, HediffDef> skillsToHediffs = new Dictionary<SkillDef, HediffDef> { { SkillDefOf.Shooting, InternalDefOf.AG_DevouredShooting },
            { SkillDefOf.Melee,InternalDefOf.AG_DevouredMelee},{ SkillDefOf.Construction,InternalDefOf.AG_DevouredConstruction},
        { SkillDefOf.Mining,InternalDefOf.AG_DevouredMining},{ SkillDefOf.Cooking,InternalDefOf.AG_DevouredCooking},
        { SkillDefOf.Plants,InternalDefOf.AG_DevouredPlants},{ SkillDefOf.Animals,InternalDefOf.AG_DevouredAnimals},
        { SkillDefOf.Crafting,InternalDefOf.AG_DevouredCrafting},{ SkillDefOf.Artistic,InternalDefOf.AG_DevouredArtistic},
        { SkillDefOf.Medicine,InternalDefOf.AG_DevouredMedical},{ SkillDefOf.Social,InternalDefOf.AG_DevouredSocial}
        ,{ SkillDefOf.Intellectual,InternalDefOf.AG_DevouredIntellectual}};


        private new CompProperties_DevourBrain Props => (CompProperties_DevourBrain)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            
            base.Apply(target, dest);
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {

                


                if (pawn.Downed || pawn.InBed()) {


                    BodyPartRecord brain = pawn.health.hediffSet.GetBrain();
                    if (brain != null)
                    {

                        SkillRecord maxSkill = pawn.skills.skills.MaxBy(element => (element.levelInt));
                        ApplyNeededHediff(maxSkill, parent.pawn);
                        

                        Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, pawn, brain);
                        

                        if (pawn.Faction != null && pawn.Faction != Faction.OfPlayer && !pawn.Faction.HostileTo(parent.pawn.Faction))
                        {
                            Faction.OfPlayer.TryAffectGoodwillWith(pawn.Faction, -50, canSendMessage: true, canSendHostilityLetter: true, HistoryEventDefOf.UsedHarmfulAbility);

                          
                        }

                       
                       

                        pawn.health.AddHediff(hediff, brain);

                    }
                    else
                    {
                        Messages.Message("AG_PawnWithoutBrain".Translate(pawn), pawn, MessageTypeDefOf.RejectInput, historical: false);


                    }



                }
                else
                {
                    Messages.Message("AG_DownedOrLyingInBed".Translate(pawn), pawn, MessageTypeDefOf.RejectInput, historical: false);


                }


                

            }
        }

        public void ApplyNeededHediff(SkillRecord maxSkill, Pawn pawn) {

            if (maxSkill != null) {

                foreach(HediffDef hediffdef in skillsToHediffs.Values)
                {
                    if (pawn.health.hediffSet.HasHediff(hediffdef))
                    {
                        pawn.health.RemoveHediff(pawn.health.hediffSet.GetFirstHediffOfDef(hediffdef));
                    }
                }


                HediffDef hediffDef = skillsToHediffs[maxSkill.def];
                pawn.health.AddHediff(hediffDef);

            }






        }












    }
}