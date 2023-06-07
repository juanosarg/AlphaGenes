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
    public class CompGeneSyphon : CompAbilityEffect
    {
       

        private new CompProperties_GeneSyphon Props => (CompProperties_GeneSyphon)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            base.Apply(target, dest);
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {


                if (pawn.Downed || pawn.InBed())
                {

                    List<Gene> listOfGenes = pawn.genes?.GenesListForReading;
                    if(listOfGenes.Count> 0)
                    {
                        Gene gene = listOfGenes.RandomElement();
                        if (gene != null)
                        {
                            parent.pawn.genes?.AddGene(gene.def, true);
                        }
                    }
                    

                    if (!pawn.IsSlaveOfColony)
                    {
                        Faction homeFaction = pawn.HomeFaction;
                        if (parent.pawn.Faction == Faction.OfPlayer && homeFaction != null && !homeFaction.HostileTo(parent.pawn.Faction))
                        {
                            Faction.OfPlayer.TryAffectGoodwillWith(homeFaction, -50, canSendMessage: true, canSendHostilityLetter: true, HistoryEventDefOf.UsedHarmfulAbility);
                        }
                    }
                    for (int i = 0; i < 20; i++)
                    {
                        IntVec3 c;
                        CellFinder.TryFindRandomReachableCellNear(pawn.Position, pawn.Map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);
                        FilthMaker.TryMakeFilth(c, pawn.Map, ThingDefOf.Filth_Blood);

                    }

                    pawn.Kill(null);


                }
                else
                {
                    Messages.Message("AG_DownedOrLyingInBed".Translate(pawn), pawn, MessageTypeDefOf.RejectInput, historical: false);


                }




            }
        }

        












    }
}