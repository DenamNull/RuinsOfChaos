using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using RuinsOfChaos.Content;
using System;

namespace RuinsOfChaos.Common.UI.Precision
{
    internal class PrecisionBar : UIState
    {
        private UIElement area;
        private UIImage frame;
        private Color Gradient1;
        private Color Gradient2;

        public static int widthOffset = -10;
        public static int heightOffset = -8;
        public static int XOffset = 10;
        public static int YOffset = 4;

        public override void OnInitialize()
        {
            
            area = new UIElement();
            area.Width.Set(102, 0f);
            area.Height.Set(20, 0f);
            area.HAlign = 0.5f;
            area.Top.Set((float)Math.Floor((float)(Main.screenHeight / 2)) - 56, 0f);

            frame = new UIImage(ModContent.Request<Texture2D>("RuinsOfChaos/Common/UI/Precision/PrecisionFrame"));
            frame.Width.Set(102, 0f);
            frame.Height.Set(20, 0f);
            frame.HAlign = frame.VAlign = 0.5f;
            frame.SetPadding(0f);

            Gradient1 = new Color(132, 0, 0);
            Gradient2 = new Color(15, 15, 15);

            area.Append(frame);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Main.LocalPlayer.HeldItem.CountsAsClass<AssassinClass>())
            {
                return;
            }
            base.Draw(spriteBatch);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            float PrecQuot = (float)Main.LocalPlayer.GetModPlayer<RoCPlayer>().Precision / (float)Main.LocalPlayer.GetModPlayer<RoCPlayer>().PrecisionCap;
            PrecQuot = Utils.Clamp(PrecQuot, 0f, 1f);

            Rectangle hitbox = frame.GetDimensions().ToRectangle();
            hitbox.X += 10;
            hitbox.Y += 6;
            hitbox.Width -= 20;
            hitbox.Height -= 12;
            
            int RedSize = (int)(Math.Round(hitbox.Width * PrecQuot));
            Rectangle RedRectangle = new Rectangle(hitbox.X, hitbox.Y, RedSize, hitbox.Height);
            Rectangle BlackRectangle = new Rectangle(hitbox.X + RedSize, hitbox.Y, hitbox.Width - RedSize, hitbox.Height);
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, RedRectangle, Gradient1);
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, BlackRectangle, Gradient2);
        }
        public override void Update(GameTime gameTime)
        {
            if (!Main.LocalPlayer.HeldItem.CountsAsClass<AssassinClass>())
            {
                return;
            }
            RoCPlayer modPlayer = Main.LocalPlayer.GetModPlayer<RoCPlayer>();
            area.Top.Set((float)Math.Floor((float)(Main.screenHeight / 2)) - 56, 0f);
            LocalizedText text = Language.GetOrRegister("Mods.RuinsOfChaos.UI.PrecisionBarText");
            string PrecisionText = text.Format(modPlayer.Precision, modPlayer.PrecisionCap);
            if (frame.IsMouseHovering || area.IsMouseHovering)
            {
                Main.instance.MouseText(PrecisionText);
            }
            base.Update(gameTime);
        }
    }
    [Autoload(Side = ModSide.Client)]
    internal class PrecisionUISystem : ModSystem
    {
        private UserInterface PrecisionUI;
        internal PrecisionBar PrecisionBarUI;
        public override void Load()
        {
            PrecisionUI = new UserInterface();
            PrecisionBarUI = new PrecisionBar();
            PrecisionUI.SetState(PrecisionBarUI);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            PrecisionUI?.Update(gameTime);

        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "RuinsOfChaos: PrecisionMeter",
                    delegate
                    {
                        PrecisionUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }
    }
}
