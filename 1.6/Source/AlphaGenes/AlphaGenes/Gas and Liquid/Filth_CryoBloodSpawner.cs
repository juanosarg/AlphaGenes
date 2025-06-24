using RimWorld;
using System;
using Verse;

namespace AlphaGenes
{

    public class Filth_CryoBloodSpawner : Filth
    {




        protected override void Tick()
        {
            Thing thing = ThingMaker.MakeThing(InternalDefOf.AG_Filth_CryoBloodPermanent, null);
            GenPlace.TryPlaceThing(thing, this.Position, this.Map, ThingPlaceMode.Near, null, null, default(Rot4));
            Destroy();

        }


    }


}
