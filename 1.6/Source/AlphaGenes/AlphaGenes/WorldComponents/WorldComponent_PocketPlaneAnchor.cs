using RimWorld.Planet;
using System.Collections.Generic;
using Verse;

namespace AlphaGenes
{
    public class WorldComponent_PocketPlaneAnchor : WorldComponent
    {
        private List<Map> anchoredMaps = new List<Map>();

        public WorldComponent_PocketPlaneAnchor(World world) : base(world) { }

        public static WorldComponent_PocketPlaneAnchor Instance =>
            Find.World.GetComponent<WorldComponent_PocketPlaneAnchor>();

        public void Anchor(Map map)
        {
            if (map != null && !anchoredMaps.Contains(map))
                anchoredMaps.Add(map);
        }

        public void Release(Map map)
        {
            anchoredMaps.Remove(map);
        }

        public bool IsAnchored(Map map) => anchoredMaps.Contains(map);

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref anchoredMaps, "anchoredMaps", LookMode.Reference);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
                anchoredMaps ??= new List<Map>();
        }
    }
}
