using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using Verse.Sound;

namespace AlphaGenes
{
    public class CompDestroyPocketPlane : CompAbilityEffect
    {

        private new CompProperties_DestroyPocketPlane Props => (CompProperties_DestroyPocketPlane)props;


      

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            base.Apply(target, dest);

            Pawn pawn = this.parent.pawn;

            if (pawn.abilities?.AllAbilitiesForReading?.Where(x => x.def.GetModExtension<AbilityExtension>()?.isPocketPlaneAbility==true)?.First() != null)
            {
                Ability ability = pawn.abilities.AllAbilitiesForReading.Where(x => x.def.GetModExtension<AbilityExtension>()?.isPocketPlaneAbility == true).First();
                CompPocketPlane comp = ability.comps.First() as CompPocketPlane;
                if(comp.pocketMap != null)
                {
                    if (comp.pocketMap == pawn.Map)
                    {
                        Messages.Message("AG_PawnCantCollapsePocket".Translate(pawn.LabelCap), pawn, MessageTypeDefOf.RejectInput, historical: false);
                    }
                    else
                    {

                        PocketMapUtility.DestroyPocketMap(comp.pocketMap);
                        comp.pocketMap = null;
                    }

                }
                else
                {
                    Messages.Message("AG_PocketPlaneMapNotActive".Translate(), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
            }

        }







    }
}