using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using RuinsOfChaos.Common;

namespace RuinsOfChaos.Content.Projectiles.Friendly.Marksman.Crossbows
{
    public class SilverBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 6;
            Projectile.penetrate = 4;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.DamageType = ModContent.GetInstance<AssassinClass>();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig);
            for (int i = 0; i < 6; i++)
            {
                Vector2 dustPos = Projectile.Center + Main.rand.NextVector2Circular(1f, 1f);
                Dust.NewDustPerfect(dustPos, DustID.Silver);
            }
            Projectile.Kill();
            return base.OnTileCollide(oldVelocity);
        }
        public override void AI()
        {
            CrossbowBoltAI.UseCrossbowBoltAI(Projectile);
        }
    }
}
