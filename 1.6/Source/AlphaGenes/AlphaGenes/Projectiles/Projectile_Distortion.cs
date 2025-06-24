
using Verse;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using Verse.Sound;
using UnityEngine;
using System.Collections;

namespace AlphaGenes
{
    public class Projectile_Distortion : Projectile_Liquid
    {



        protected override void Tick()
        {
            base.Tick();
            if (this.IsHashIntervalTick(10))
            {
                try
                {
                    FleckMaker.AttachedOverlay(this, FleckDefOf.PsycastAreaEffect, Vector3.zero, 3f, -1f);
                }
                catch (Exception) { }

            }
        }
    }
}