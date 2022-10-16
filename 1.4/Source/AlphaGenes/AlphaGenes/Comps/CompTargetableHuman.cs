using System.Collections.Generic;
using RimWorld;
using Verse;
using System.Linq;

namespace AlphaGenes
{
    public class CompTargetableHuman : CompTargetable
    {

        public new CompProperties_TargetableHuman Props
        {
            get
            {
                return (CompProperties_TargetableHuman)this.props;
            }
        }

        protected override bool PlayerChoosesTarget => true;


        protected override TargetingParameters GetTargetingParameters()
        {



            return new TargetingParameters
            {
                canTargetPawns = true,
                canTargetItems = false,
                canTargetBuildings = false,
                mapObjectTargetsMustBeAutoAttackable = false,
                validator = (TargetInfo x) => (x.Thing is Pawn && x.Thing.Faction == Faction.OfPlayer &&(x.Thing as Pawn).RaceProps.Humanlike)
            };
        }

        public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
        {
            yield return targetChosenByPlayer;
        }
    }
}
