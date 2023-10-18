using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using RuinsOfChaos.Content.Modifiers.Summoner;

namespace RuinsOfChaos.Common.GlobalProjectiles
{
    public class SentryModifierProjEffects : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.sentry;
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is IEntitySource_WithStatsFromItem fromItem && fromItem.Item.prefix == ModContent.PrefixType<Defiant>())
            {
                projectile.timeLeft = 1200;
            }
        }
    }
}
