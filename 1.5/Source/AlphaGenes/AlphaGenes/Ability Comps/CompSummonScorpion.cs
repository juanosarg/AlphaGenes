using AlphaGenes;
using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;

using Verse;
using Verse.AI;
using Verse.Sound;

namespace AlphaGenes
{
    public class CompSummonScorpion : CompAbilityEffect
    {



        private new CompProperties_SummonScorpion Props => (CompProperties_SummonScorpion)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            base.Apply(target, dest);



            Pawn pawnCreated = PawnGenerator.GeneratePawn(PawnKindDef.Named("AG_OcularSlinger"), parent.pawn.Faction);
          
            Pawn pawn = (Pawn)GenSpawn.Spawn(pawnCreated, target.Cell, parent.pawn.Map, Rot4.South);
           



        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {

            int scorpionNumber = Current.Game.World.GetComponent<ScorpionCounter_WorldComponent>().totalScorpions;
            int ocularGeneHolders = StaticCollectionsClass.ocular_gene_colonists_inWorld;


            if (scorpionNumber >= ocularGeneHolders)
            {

                if (throwMessages)
                {
                    Messages.Message("VRE_OneScorpionPerColonist".Translate(), parent.pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;


            }



            return base.Valid(target, throwMessages);
        }








    }
}