
using System.Collections.Generic;
using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaGenes
{

    public class ThingSetMaker_FissionMouse : ThingSetMaker
    {
        protected override void Generate(ThingSetMakerParams parms, List<Thing> outThings)
        {

            PawnKindDef mouse = PawnKindDef.Named("AA_FissionMouse");

            PawnGenerationRequest request = new PawnGenerationRequest(mouse, Faction.OfPlayer, PawnGenerationContext.NonPlayer);
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

