using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using RuinsOfChaos.Content.Modifiers.Summoner;
using Terraria.ID;

namespace RuinsOfChaos.Common.GlobalProjectiles
{
    public class MinionModifierProjEffects : GlobalProjectile
    {
        public bool WasSummonedByDefiant = false;
        public int ItemSummonedFrom;
        public int Timer = 0;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is IEntitySource_WithStatsFromItem fromItem && fromItem.Item.prefix == ModContent.PrefixType<Defiant>())
            {
                WasSummonedByDefiant = true;
                ItemSummonedFrom = fromItem.Item.type;
            }
        }
        public override void AI(Projectile projectile)
        {
            Timer++;
            if (WasSummonedByDefiant && Timer > 600)
            {
                Main.player[projectile.owner].ClearBuff(ContentSamples.ItemsByType[ItemSummonedFrom].buffType);
            }

            base.AI(projectile);
        }
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.minion;
        }
        public override bool InstancePerEntity => true;
    }
}
