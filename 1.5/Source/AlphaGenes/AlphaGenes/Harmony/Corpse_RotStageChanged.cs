using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AlphaGenes
{
    [HarmonyPatch(typeof(Corpse))]
    [HarmonyPatch("RotStageChanged")]
    public static class AlphaGenes_Corpse_RotStageChanged_Patch
    {

        [HarmonyPrefix]
        public static bool DisableStupidFuckingError(Corpse __instance)
        {
           
            if (__instance.InnerPawn?.health?.hediffSet?.HasHediff(InternalDefOf.AG_PlaguedClaws)==true)
            {
                
                return false;
                
            }


            return true;
        }
    }
}
