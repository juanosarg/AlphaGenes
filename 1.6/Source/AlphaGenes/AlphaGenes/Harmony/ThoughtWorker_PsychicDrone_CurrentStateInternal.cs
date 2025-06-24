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

    [HarmonyPatch(typeof(ThoughtWorker_PsychicDrone))]
    [HarmonyPatch("CurrentStateInternal")]
    public static class AlphaGenes_ThoughtWorker_PsychicDrone_CurrentStateInternal_Patch
    {


        [HarmonyPostfix]
        public static void InvertPsychicDrone(Pawn p, ref ThoughtState __result)

        {

            if (p?.HasActiveGene(InternalDefOf.AG_PsychicRemapping) == true)
            {


                if (__result.StageIndex == 0) { }


                switch (__result.StageIndex)
                {
                    case 0:
                        __result = ThoughtState.ActiveAtStage(4);
                        break;
                    case 1:
                        __result = ThoughtState.ActiveAtStage(0);
                        break;
                    case 2:
                        __result = ThoughtState.ActiveAtStage(0);
                        break;
                    case 3:
                        __result = ThoughtState.ActiveAtStage(0);
                        break;
                    case 4:
                        __result = ThoughtState.ActiveAtStage(0);
                        break;


                      
                };

            }



        }


    }
}
