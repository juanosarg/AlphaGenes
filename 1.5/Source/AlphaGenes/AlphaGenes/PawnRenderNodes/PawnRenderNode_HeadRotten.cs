
using RimWorld;
using UnityEngine;
using Verse;
namespace AlphaGenes
{
    public class PawnRenderNode_HeadRotten : PawnRenderNode
    {
        public PawnRenderNode_HeadRotten(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree)
            : base(pawn, props, tree)
        {
        }

      

        public override Graphic GraphicFor(Pawn pawn)
        {
            if (!pawn.health.hediffSet.HasHead)
            {
                return null;
            }
            Graphic baseGraphic = pawn.story?.headType?.GetGraphic(pawn, ColorFor(pawn));

            return baseGraphic.GetColoredVersion(ShaderDatabase.Cutout, MutantUtility.GetShamblerColor(baseGraphic.Color), MutantUtility.GetShamblerColor(baseGraphic.ColorTwo));

             
        }
    }
}