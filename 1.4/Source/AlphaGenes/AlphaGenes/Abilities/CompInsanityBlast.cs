using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace AlphaGenes
{
    public class CompInsanityBlast : CompAbilityEffect
    {
       
        private new CompProperties_InsanityBlast Props => (CompProperties_InsanityBlast)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            
            base.Apply(target, dest);
            Pawn pawn = target.Pawn;

            if (!pawn.Dead)
            {
                System.Random rand = new System.Random();
                float psychicSensitivity = pawn.GetStatValue(StatDefOf.PsychicSensitivity);
                bool applyShock = false;
                if (psychicSensitivity > 1)
                {
                    applyShock = true;

                }
                else
                {
                    if(rand.NextDouble()< psychicSensitivity)
                    {
                        applyShock = true;
                    }
                    else
                    {
                        Messages.Message("AG_ResistedInsanityBlast".Translate(pawn), pawn, MessageTypeDefOf.RejectInput, historical: false);
                    }


                }

                if (applyShock)
                {
                    pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, forceWake: true);

                }
                

            }

        }

       



      




    }
}