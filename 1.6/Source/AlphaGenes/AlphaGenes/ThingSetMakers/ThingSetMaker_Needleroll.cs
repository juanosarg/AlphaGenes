
using System.Collections.Generic;
using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaGenes
{

    public class ThingSetMaker_Needleroll : ThingSetMaker
    {
        protected override void Generate(ThingSetMakerParams parms, List<Thing> outThings)
        {

            PawnKindDef cactus = PawnKindDef.Named("AA_Needleroll");

            PawnGenerationRequest request = new PawnGenerationRequest(cactus, Faction.OfPlayer, PawnGenerationContext.NonPlayer);
            Pawn pawn = PawnGenerator.GeneratePawn(request);
            Pawn pawn2 = PawnGenerator.GeneratePawn(request);
            Pawn pawn3 = PawnGenerator.GeneratePawn(request);
            outThings.Add(pawn);
            outThings.Add(pawn2);
            outThings.Add(pawn3);



        }

        protected override IEnumerable<ThingDef> AllGeneratableThingsDebugSub(ThingSetMakerParams parms)
        {
            throw new NotImplementedException();
        }



    }
}

