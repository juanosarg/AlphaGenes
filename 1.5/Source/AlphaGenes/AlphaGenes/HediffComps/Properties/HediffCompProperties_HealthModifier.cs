using Verse;

namespace AlphaGenes
{
    internal class HediffCompProperties_HealthModifier : HediffCompProperties
    {

        public float healthPointToAdd = 0f;

        public HediffCompProperties_HealthModifier()
        {
            this.compClass = typeof(HediffComp_HealthModifier);
        }

        
    }
}