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
        public Dictionary<SkillDef, HediffDef> skillsToHediffs = new Dictionary<SkillDef, HediffDef> { { SkillDefOf.Melee,HediffDefOf.Abasia} };


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
                        

                        if (pawn.Faction != null && pawn.Faction != Faction.OfPlayer)
                        {
                            pawn.Faction.SetRelationDirect(Faction.OfPlayer, FactionRelationKind.Hostile);
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

            if (maxSkill.def == SkillDefOf.Melee)
            {
               

            }

        
        
        
        
        }












    }
}