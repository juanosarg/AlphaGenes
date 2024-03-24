using Verse;

namespace AlphaGenes
{
    internal class HediffComp_HealthModifier : HediffComp
    {
        public HediffCompProperties_HealthModifier Props
        {
            get
            {
                return (HediffCompProperties_HealthModifier)this.props;
            }
        }
    }
}