using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace AlphaGenes
{
    [StaticConstructorOnStartup]
    public static class StaticStartup
    {
        static StaticStartup()
        {
            if (ModsConfig.IsActive("erdelf.HumanoidAlienRaces_steam") || ModsConfig.IsActive("erdelf.HumanoidAlienRaces"))
            {
                HARLoaded = true;
            }
        }

        public static bool HARLoaded;

    }
}
