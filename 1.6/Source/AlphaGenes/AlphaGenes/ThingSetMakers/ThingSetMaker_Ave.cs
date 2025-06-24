
using System.Collections.Generic;
using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaGenes
{

    public class ThingSetMaker_Ave : ThingSetMaker
    {
        protected override void Generate(ThingSetMakerParams parms, List<Thing> outThings)
        {

            PawnKindDef ave = PawnKindDef.Named("AA_MeadowAve");

            PawnGenerationRequest request = new PawnGenerationRequest(ave, Faction.OfPlayer, PawnGenerationContext.NonPlayer);
            Pawn pawn = PawnGenerator.GeneratePawn(request);
           
            outThings.Add(pawn);
         


        }

        protected override IEnumerable<ThingDef> AllGeneratableThingsDebugSub(ThingSetMakerParams parms)
        {
            throw new NotImplementedException();
        }



    }
}

