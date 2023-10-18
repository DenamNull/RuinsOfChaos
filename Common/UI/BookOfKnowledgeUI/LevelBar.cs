using Terraria.UI;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Audio;
using Terraria.ID;
using System;
using Terraria.Localization;

namespace RuinsOfChaos.Common.UI.BookOfKnowledgeUI
{
    public class LevelBar : UIElement
    {
        public int BorderThickness;
        public Rectangle dimen;
        public const string BookUIPath = BookMainSystem.BookUIPath;
        public LevelBar(int borderThickness)
        {
            BorderThickness = borderThickness;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            
            Color BorderColor = new Color(15, 15, 15);
            
            dimen = GetOuterDimensions().ToRectangle();
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(dimen.X, dimen.Y, dimen.Width, BorderThickness), BorderColor);
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(dimen.X, dimen.Y, BorderThickness, dimen.Height), BorderColor);
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(dimen.X, dimen.Y + dimen.Height - BorderThickness, dimen.Width, BorderThickness), BorderColor);
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(dimen.X + dimen.Width - BorderThickness, dimen.Y, BorderThickness, dimen.Height), BorderColor);
            
            base.Draw(spriteBatch);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            Color ProgressColor = new Color(50, 255, 130);
            dimen = GetOuterDimensions().ToRectangle();
            var modPlayer = Main.LocalPlayer.GetModPlayer<RoCPlayer>();
            int XPNeeded = RoCPlayer.GetXPRequiredForLevel(modPlayer.Levels + 1) - RoCPlayer.GetXPRequiredForLevel(modPlayer.Levels);
            int XPHas = modPlayer.Experience - RoCPlayer.GetXPRequiredForLevel(modPlayer.Levels);
            float LevelProgress = (float)XPHas / XPNeeded;
            LevelProgress = Utils.Clamp(LevelProgress, 0f, 1f);
            int progressWidth = (int)Math.Round(LevelProgress * (dimen.Width - (2 * BorderThickness)));
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(dimen.X + BorderThickness, dimen.Y + BorderThickness, progressWidth, dimen.Height - (2 * BorderThickness)), ProgressColor);
        }
        public override void Update(GameTime gameTime)
        {
            if (IsMouseHovering)
            {
                var modPlayer = Main.LocalPlayer.GetModPlayer<RoCPlayer>();
                int XPNeeded = RoCPlayer.GetXPRequiredForLevel(modPlayer.Levels + 1) - RoCPlayer.GetXPRequiredForLevel(modPlayer.Levels);
                int XPHas = modPlayer.Experience - RoCPlayer.GetXPRequiredForLevel(modPlayer.Levels);
                LocalizedText text = Language.GetOrRegister($"{BookUIPath}.LevelText");
                string LevelText = text.Format(modPlayer.Levels, XPHas, XPNeeded);
                Main.instance.MouseText(LevelText);
            }
        }
        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);
            SoundEngine.PlaySound(SoundID.MenuTick);
        }
    }
}
