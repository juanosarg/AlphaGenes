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
    public class CompTeleportItemsPocketPlane : CompAbilityEffect
    {

        private new CompProperties_TeleportItemsPocketPlane Props => (CompProperties_TeleportItemsPocketPlane)props;


        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            base.Apply(target, dest);

            Pawn pawn = this.parent.pawn;

            if (pawn.abilities?.GetAbility(InternalDefOf.AG_PocketPlaneAbility) != null)
            {
                Ability ability = pawn.abilities?.GetAbility(InternalDefOf.AG_PocketPlaneAbility);
                CompPocketPlane comp = ability.comps.First() as CompPocketPlane;
                if (comp.pocketMap != null)
                {
                    if (comp.pocketMap == pawn.Map)
                    {
                        Messages.Message("AG_CantTeleportItemsOutOfPocketPlane".Translate(pawn.LabelCap), pawn, MessageTypeDefOf.RejectInput, historical: false);
                    }
                    else
                    {
                        if (!target.HasThing || target.Thing as Pawn != null )
                        {
                            return;
                        }
                        parent.AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(target.Thing, pawn.Map), target.Thing.Position, 60);
                        SoundDefOf.Psycast_Skip_Entry.PlayOneShot(new TargetInfo(target.Cell, parent.pawn.Map));
                        Thing thingToSend = target.Thing;
                        thingToSend.DeSpawn();
                        GenSpawn.Spawn(thingToSend, comp.pocketMap.Center, comp.pocketMap);
                        
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