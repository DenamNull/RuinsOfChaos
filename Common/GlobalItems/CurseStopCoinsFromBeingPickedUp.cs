using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using RuinsOfChaos.Content.Buffs;

namespace RuinsOfChaos.Common.GlobalItems
{
    public class CurseStopCoinsFromBeingPickedUp : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (entity.IsACoin)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public override void OnSpawn(Item item, IEntitySource source)
        {
            if (source is EntitySource_Buff buffSource && buffSource.BuffId == ModContent.BuffType<CurseOfSilver>())
            {
                item.timeLeftInWhichTheItemCannotBeTakenByEnemies = 3600;
            }
        }
    }
}
