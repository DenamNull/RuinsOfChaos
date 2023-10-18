using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;

namespace RuinsOfChaos.Content
{
    public class RuinsOfChaosMenu : ModMenu
    {
        public override string DisplayName => "Ruins Of Chaos";
        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("RuinsOfChaos/Assets/Textures/MainMenuUI/roc_logo");
        public override int Music => MusicLoader.GetMusicSlot("RuinsOfChaos/Assets/Music/RuinsOfChaos");
        public override void OnSelected()
        {
            SoundEngine.PlaySound(SoundID.Zombie105);
        }
        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            logoScale = 2f;
            Vector2 OffsetVector = Vector2.Zero;
            Texture2D backTexture = ModContent.Request<Texture2D>("RuinsOfChaos/Assets/Textures/MainMenuUI/RuinsOfChaosMenuBackground").Value;
            Vector2 TextureSize = backTexture.Size();
            float WidthScalar = (float)Main.ScreenSize.X / (float)backTexture.Width;
            float HeightScalar = (float)Main.ScreenSize.Y / (float)backTexture.Height;
            float OverallScalar;
            if (WidthScalar > HeightScalar)
            {
                OverallScalar = WidthScalar;
                OffsetVector.Y -= ((float)backTexture.Height * OverallScalar - (float)Main.ScreenSize.Y) * 0.5f;
            } else
            {
                OverallScalar = HeightScalar;
                OffsetVector.X -= ((float)backTexture.Width * OverallScalar - (float)Main.ScreenSize.X) * 0.5f;
            }
            spriteBatch.Draw(backTexture, OffsetVector, null, Color.White, 0f, Vector2.Zero, OverallScalar, SpriteEffects.None, 0f);
            Main.dayTime = true;
            Main.time = 27000;
            return true;
        }
        public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>("RuinsOfChaos/Assets/Textures/MainMenuUI/Empty");
        public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>("RuinsOfChaos/Assets/Textures/MainMenuUI/Empty");

        public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<RuinsOfChaosEmptyBackground>();
    }
}
