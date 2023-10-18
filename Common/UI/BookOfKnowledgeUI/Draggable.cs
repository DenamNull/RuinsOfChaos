using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Terraria;
using Microsoft.Xna.Framework;

namespace RuinsOfChaos.Common.UI.BookOfKnowledgeUI
{
    public class Draggable : UIPanel
    {
        private bool isBeingDragged;
        private Vector2 newPos;
        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            
            newPos = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            isBeingDragged = true;
        }
        public override void LeftMouseUp(UIMouseEvent evt)
        {
            base.LeftMouseUp(evt);
            Vector2 endMousePos = evt.MousePosition;
            isBeingDragged = false;
            Left.Set(endMousePos.X - newPos.X, 0f);
            Top.Set(endMousePos.Y - newPos.Y, 0f);
            Recalculate();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (isBeingDragged)
            {
                Left.Set(Main.mouseX - newPos.X, 0f);
                Top.Set(Main.mouseY - newPos.Y, 0f);
                Recalculate();
            }
            var parentSpace = Parent.GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(parentSpace))
            {
                Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
                Recalculate();
            }
        }
    }
}
