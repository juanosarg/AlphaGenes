
using RimWorld;
using Verse;
namespace AlphaGenes
{
    public class PawnRenderNodeWorker_OverlayFalseShambler : PawnRenderNodeWorker_Overlay
    {
        protected override PawnOverlayDrawer OverlayDrawer(Pawn pawn)
        {
            return pawn.Drawer.renderer.ShamblerScarDrawer;
        }

        public override bool ShouldListOnGraph(PawnRenderNode node, PawnDrawParms parms)
        {
            return true;
        }

        public override bool CanDrawNow(PawnRenderNode node, PawnDrawParms parms)
        {
            if (base.CanDrawNow(node, parms))
            {

                return true;
            }
            return false;
        }
    }
}