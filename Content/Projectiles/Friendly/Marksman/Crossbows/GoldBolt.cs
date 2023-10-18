using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using RuinsOfChaos.Common;
using RuinsOfChaos.Content.Buffs;

namespace RuinsOfChaos.Content.Projectiles.Friendly.Marksman.Crossbows
{
    public class GoldBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 10;
            Projectile.penetrate = 5;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = ModContent.GetInstance<AssassinClass>();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig);
            for (int i = 0; i < 6; i++)
            {
                Vector2 dustPos = Projectile.Center + Main.rand.NextVector2Circular(1f, 1f);
                Dust.NewDustPerfect(dustPos, DustID.Gold);
            }
            Projectile.Kill();
            return base.OnTileCollide(oldVelocity);
        }
        public override void AI()
        {
            CrossbowBoltAI.UseCrossbowBoltAI(Projectile);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            RoCPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<RoCPlayer>();
            if (modPlayer.PrecisionCap == modPlayer.Precision)
            {
                target.AddBuff(ModContent.BuffType<CurseOfSilver>(), 200);
            }
        }
    }
}
