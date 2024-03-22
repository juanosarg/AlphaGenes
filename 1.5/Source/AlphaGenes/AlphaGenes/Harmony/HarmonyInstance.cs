using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Reflection.Emit;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Verse.AI;
using RimWorld.Planet;



namespace AlphaGenes
{

    public class AlphaGenes : Mod
    {
        public AlphaGenes(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("com.alphagenes");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

}
















