using System;
using System.Collections.Generic;
using System.Linq;

using Verse;
using RimWorld;
using VEF.Genes;
using HarmonyLib;

namespace AlphaGenes
{

     


    [HarmonyPatch(typeof(GeneDefGenerator))]
    [HarmonyPatch("ImpliedGeneDefs")]

    public static class AlphaGenes_GeneDefGenerator_ImpliedGeneDefs_Patch
    {
        [HarmonyPostfix]
        public static IEnumerable<GeneDef> Postfix(IEnumerable<GeneDef> values)
        {
            List<GeneDef> resultingList = values.ToList();


            List<string> blackListedAnimals = new List<string>();
            List<BlackListedAnimalsDef> allBlackListedAnimals = DefDatabase<BlackListedAnimalsDef>.AllDefsListForReading;
            foreach (BlackListedAnimalsDef individualList in allBlackListedAnimals)
            {
                blackListedAnimals.AddRange(individualList.blackListedAnimals);
            }

            

            foreach (AnimalGeneTemplateDef template in DefDatabase<AnimalGeneTemplateDef>.AllDefsListForReading)
            {

                List<PawnKindDef> listOfAnimals = DefDatabase<PawnKindDef>.AllDefsListForReading.Where(element => (element?.RaceProps!=null && 
                element.RaceProps.Animal && !element.RaceProps.Dryad && element.RaceProps.baseBodySize<=template.maxBodySize &&
                element.RaceProps.baseBodySize > template.minBodySize &&
                !blackListedAnimals.Contains(element.defName)&&!element.defName.Contains("GR_") && !element.defName.Contains("WMH_"))).ToList();

                foreach (PawnKindDef animal in listOfAnimals)
                {
                    resultingList.Add(GetFromTemplate(template, animal, animal.index));
                }


            }



            return resultingList;




        }

        public static GeneDef GetFromTemplate(AnimalGeneTemplateDef template, PawnKindDef def, int displayOrderBase)
        {
            

            GeneDef geneDef = new GeneDef
            {
                defName = template.defName + "_" + def.defName,
                geneClass = template.geneClass,
                label = template.label.Formatted(def.label),
                iconPath = def.lifeStages.Last().bodyGraphicData.texPath+"_east",
                description = template.description.Formatted(def.LabelCap),
                labelShortAdj = template.labelShortAdj.Formatted(def.label),
                selectionWeight = template.selectionWeight,
                biostatCpx = template.biostatCpx,
                biostatMet = template.biostatMet,
                displayCategory = template.displayCategory,
                displayOrderInCategory = displayOrderBase + template.displayOrderOffset,
                minAgeActive = template.minAgeActive,
                customEffectDescriptions = template.customEffectDescriptions,
                modContentPack = template.modContentPack,
                modExtensions = new List<DefModExtension> {
                    new GeneExtension {
                        backgroundPathEndogenes = "UI/Icons/Genes/AG_AnimalSummonEndogenes",
                        backgroundPathXenogenes = "UI/Icons/Genes/AG_AnimalSummonXenogenes",
                        hideGene = !AlphaGenes_Mod.settings.AG_ShowAllAnimalSummonGenes,
                    },
                    new SummoningGeneDefExtension {
                        pawn = def
                    }
                },
               
                abilities = new List<AbilityDef> { template.ability },
                descriptionHyperlinks = new List<DefHyperlink> {  new DefHyperlink { def= template.ability } }
            };

           
            if (!template.exclusionTagPrefix.NullOrEmpty())
            {
                geneDef.exclusionTags = new List<string> { template.exclusionTagPrefix };
            }

            return geneDef;

        }





    }

}
