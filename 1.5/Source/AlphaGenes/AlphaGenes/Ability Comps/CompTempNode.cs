using System;
using Verse;
using RimWorld;
using AlphaGenes;
using static RimWorld.MechClusterSketch;

namespace AlphaGenes
{
    public class CompTempNode : CompAbilityEffect
    {
        public new CompProperties_TempNode Props
        {
            get
            {
                return (CompProperties_TempNode)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            IntVec3 loc = target.Cell;
            Map map = this.parent.pawn.Map;

            if (parent.pawn.mechanitor == null)
            {
                Messages.Message("AM_OnlyMechanitorCanUse".Translate(), parent.pawn, MessageTypeDefOf.RejectInput, historical: false);
                return;
            }

            if (loc.InBounds(map) && loc.GetEdifice(map) == null)
            {

                ThingDef newThing = InternalDefOf.AG_TemporaryBandNode;
                Thing node = GenSpawn.Spawn(newThing, loc, map, WipeMode.Vanish);
                node.SetFaction(Faction.OfPlayer);
                parent.pawn.health.AddHediff(InternalDefOf.AG_TempNodeEffect);
                CompTempNodeBuilding comp = node.TryGetComp<CompTempNodeBuilding>();
                if (comp != null)
                {
                    comp.pawn = parent.pawn;
                }
            }
            else
            {
                Messages.Message("AG_CantSpawnThere".Translate(), null, MessageTypeDefOf.RejectInput, historical: false);
                
            }
            base.Apply(target, dest);

        }
    }
}
