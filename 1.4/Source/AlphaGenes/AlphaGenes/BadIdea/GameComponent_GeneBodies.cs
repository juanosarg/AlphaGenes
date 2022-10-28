using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AlphaGenes
{
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("RaceProps", MethodType.Getter)]
    public static class Pawn_RaceProps_Get_Patch
    {
        public static void Postfix(Pawn __instance)
        {
            if (!__instance.def.race.Humanlike) { return; }
            if (Current.ProgramState != ProgramState.Playing) { return; }
            var body = GeneBodyCache.GetGeneBodyFor(__instance);
            __instance.def.race.body = body.BodyDef;//Why yes this is changing the body def for all humans every single time anyone asks for raceprops for any reasons how oculd that be bad
        }
    }
    public static class GeneBodyCache
    {
        static GameComponent_GeneBodies component;
        public static GeneBody GetGeneBodyFor(Pawn pawn)
        {
            if(component == null)
            {
                component = Current.Game.GetComponent<GameComponent_GeneBodies>();
            }           
            if (component == null) { return null; }
            if (!component.cacheBody.TryGetValue(pawn, out var body))
            {
                body = new(pawn);
                body.GenerateBodyDef();
                component.cacheBody.Add(pawn, body);
            }
            return body;
        }
    }
    public class GameComponent_GeneBodies : GameComponent
    {
        public Dictionary<Pawn, GeneBody> cacheBody = new();
        private List<Pawn> tmpPawn = new();
        private List<GeneBody> tmpGeneBody = new();
        public GameComponent_GeneBodies(Game game)
        {

        }
        public override void StartedNewGame()
        {
            base.StartedNewGame();
            foreach(var pawn in Find.WorldPawns.AllPawnsAlive)
            {
                if (!cacheBody.ContainsKey(pawn))
                {
                    GeneBody body = new(pawn);
                    body.GenerateBodyDef();
                    cacheBody.Add(pawn, body);
                }
            }
        }

        public override void LoadedGame()
        {
            base.LoadedGame();
            foreach (var pawn in Find.WorldPawns.AllPawnsAlive)
            {
                if (!cacheBody.ContainsKey(pawn))
                {
                    GeneBody body = new(pawn);
                    body.GenerateBodyDef();
                    cacheBody.Add(pawn, body);
                }
            }
        }
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref cacheBody, "cacheBody", LookMode.Reference, LookMode.Deep,ref tmpPawn, ref tmpGeneBody);
        }

    }
}
