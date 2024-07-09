using System;
using Verse;
using System.Collections.Generic;
using AnimalBehaviours;

namespace AlphaGenes
{
    public class HediffCompProperties_RaiseShambler : HediffCompProperties
    {

        public float severityToTurn = 0.8f;

        public HediffCompProperties_RaiseShambler()
        {
            this.compClass = typeof(HediffComp_RaiseShambler);
        }
    }
}
