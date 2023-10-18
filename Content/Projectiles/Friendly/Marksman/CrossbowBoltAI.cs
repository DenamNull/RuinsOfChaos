using Terraria.ModLoader;
using Terraria;

namespace RuinsOfChaos.Content.Projectiles.Friendly.Marksman
{
    public class CrossbowBoltAI
    {
        public static void UseCrossbowBoltAI(Projectile proj)
        {
            proj.rotation = proj.velocity.ToRotation();
            proj.ai[0] += 1f;
            if (proj.ai[0] >= 30f)
            {
                proj.velocity.Y += 0.08f;
            }
            if (proj.velocity.Y > 16f)
            {
                proj.velocity.Y = 16f;
            }
        }
    }
}
