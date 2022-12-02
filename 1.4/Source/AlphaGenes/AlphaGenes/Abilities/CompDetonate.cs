
using Verse;
using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;




namespace AlphaGenes
{
    class CompDetonate : CompAbilityEffect
    {

        new public CompProperties_Detonate Props
        {
            get
            {
                return (CompProperties_Detonate)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Detonate();
        }

        public void Detonate()
        {
            List<Thing> ignoredThings = new List<Thing>();
            if (!Props.damageUser)
            {
                ignoredThings.Add(this.parent.pawn);
            }

            GenExplosion.DoExplosion(parent.pawn.Position, parent.pawn.Map, Props.radius, Props.damageType, parent.pawn, Props.damageAmount, Props.damagePenetration, Props.soundCreated, null, null, null,
                null, 0f, 1, GasType.BlindSmoke, false, null, 0f, 1, Props.chanceToStartFire, false, null, ignoredThings);



        }




    }
}
