using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace AlphaGenes
{
        
    public class Gene_Resource_MineralEffiency : Gene
    {

        //I wanted to put this in the def but I couldnt find a good field to hijack and adding it to a defextension is effort and might confuse ppl if it was in framework one.
        public float EffiencyFactor
        {
            get
            {
                if(def.defName == "AG_MineralEfficencyPoor")
                {
                    return 3f;
                }
                else
                {
                    return 0.5f;
                }
            }
        }
        public override void ExposeData()
        {
            base.ExposeData();
            if(Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                if(Gene != null)
                {
                    Gene.cachedEffiency = this;
                }                
            }
        }

        public override void PostAdd()
        {
            base.PostAdd();
            if (Gene != null)
            {
                Gene.cachedEffiency = this;
            }
        }
        public Gene_Resource_Metal Gene
        {
            get
            {
                if (cachedGene == null)
                {
                    cachedGene = pawn.genes.GetFirstGeneOfType<Gene_Resource_Metal>();
                }
                return cachedGene;
            }
        }
        private Gene_Resource_Metal cachedGene;
    }
}
