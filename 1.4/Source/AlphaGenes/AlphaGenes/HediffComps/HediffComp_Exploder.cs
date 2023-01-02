
using Verse;
using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;



namespace AlphaGenes
{
    public class HediffComp_Exploder : HediffComp
    {


        public HediffCompProperties_Exploder Props
        {
            get
            {
                return (HediffCompProperties_Exploder)this.props;
            }
        }



        public override void Notify_PawnDied()
        {
            List<Thing> ignoredThings = new List<Thing>();
            if (!Props.damageUser)
            {
                ignoredThings.Add(this.parent.pawn);
            }
            GenExplosion.DoExplosion(this.parent.pawn.Corpse.Position, this.parent.pawn.Corpse.Map, Props.radius, Props.damageType, parent.pawn, Props.damageAmount, Props.damagePenetration, Props.soundCreated, null, null, null,
                           Props.thingCreated, Props.thingCreatedChance, 1, GasType.BlindSmoke, false, null, 0f, 1, Props.chanceToStartFire, false, null, ignoredThings);


        }




    }
}
