using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AlphaGenes
{


    public class CompProperties_RandomItemSpawner : CompProperties
    {
        public List<ThingDef> items;

        public CompProperties_RandomItemSpawner()
        {
            this.compClass = typeof(CompRandomItemSpawner);
        }
    }
}
