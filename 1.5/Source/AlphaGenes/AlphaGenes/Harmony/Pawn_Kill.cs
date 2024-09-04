using HarmonyLib;
using RimWorld;
using Verse.Grammar;
using Verse;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Verse.AI;

namespace AlphaGenes
{

    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("Kill")]
    public static class AlphaGenes_Pawn_Kill_Patch
    {

    

        [HarmonyPostfix]
        public static void RemovePocketPlanes(Pawn __instance)

        {
            if(__instance.Dead && __instance.abilities != null){

                List<Ability> listAbilities = __instance.abilities?.AllAbilitiesForReading?.Where(x => x.def.GetModExtension<AbilityExtension>()?.isPocketPlaneAbility == true).ToList();
                if (listAbilities.Any() )
                {
                    foreach(Ability ability in listAbilities )
                    {
                        CompPocketPlane comp = ability.comps.First() as CompPocketPlane;
                        if (comp.pocketMap != null)
                        {
                           

                                PocketMapUtility.DestroyPocketMap(comp.pocketMap);
                                comp.pocketMap = null;
                            

                        }
                    }
                }
            
            } 

        }

    


    }
}
