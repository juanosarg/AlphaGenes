using System.Collections.Generic;
using RimWorld;
using Verse;
using System.Linq;

namespace AlphaGenes
{
    public class CompTargetableGenepack : CompTargetable
    {

        public List<ThingDef> allowedPacks = new List<ThingDef> {InternalDefOf.AG_Alphapack,InternalDefOf.AG_Mixedpack,ThingDefOf.Genepack };

        public new CompProperties_TargetableGenepack Props
        {
            get
            {
                return (CompProperties_TargetableGenepack)this.props;
            }
        }

        protected override bool PlayerChoosesTarget => true;


        protected override TargetingParameters GetTargetingParameters()
        {



            return new TargetingParameters
            {
                canTargetPawns = false,
                canTargetItems = true,
                canTargetBuildings = false,
                mapObjectTargetsMustBeAutoAttackable = false,
                validator = (TargetInfo x) => (x.Thing is Thing && allowedPacks.Contains(x.Thing.def) )
            };
        }

        public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
        {
            yield return targetChosenByPlayer;
        }
    }
}
