
using System;
using System.Collections.Generic;
using RimWorld;
using Verse;


namespace AlphaGenes
{
    public class AnimalGeneTemplateDef : Def
    {

        public Type geneClass = typeof(Gene);

        public List<string> customEffectDescriptions;

        public int biostatCpx;

        public int biostatMet;

        public float minAgeActive;

        public GeneCategoryDef displayCategory;

        public int displayOrderOffset;

        public float selectionWeight = 0f;

        public float maxBodySize=999f;

        public float minBodySize = 0;

        public AbilityDef ability;


        [MustTranslate]
        public string labelShortAdj;

        [NoTranslate]
        public string iconPath;

        [NoTranslate]
        public string exclusionTagPrefix;

        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string item in base.ConfigErrors())
            {
                yield return item;
            }
            if (!typeof(Gene).IsAssignableFrom(geneClass))
            {
                yield return "geneClass is not Gene or child thereof.";
            }
        }
    }
}
