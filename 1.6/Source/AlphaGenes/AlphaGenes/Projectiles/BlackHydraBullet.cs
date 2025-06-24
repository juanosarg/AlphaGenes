using RimWorld;
using UnityEngine;
using Verse;

namespace AlphaGenes
{
    public class BlackHydraBullet : Bullet
    {
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            base.Impact(hitThing, false);
            Map map = hitThing?.Map;
            if (this.def != null && hitThing != null && map != null)
            {
                Vector2 vector = Rand.UnitVector2 * Rand.Range(0f, 4f);
                IntVec2 toIntVec = hitThing.Position.ToIntVec2;
                IntVec3 forcedStrikeLoc = new IntVec3((int)vector.x + toIntVec.x, 0, (int)vector.y + toIntVec.z);
                map.weatherManager.eventHandler.AddEvent(new WeatherEvent_LightningStrike(map, forcedStrikeLoc));
            }
        }
    }
}
