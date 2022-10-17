
using Verse;
using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;




namespace AlphaGenes
{
    class CompGiveHediff : CompAbilityEffect
    {

        new public CompProperties_GiveHediff Props
        {
            get
            {
                return (CompProperties_GiveHediff)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            parent.pawn.health.AddHediff(Props.hediffDef);
        }






    }
}
