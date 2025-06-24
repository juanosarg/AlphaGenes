using Verse;

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
