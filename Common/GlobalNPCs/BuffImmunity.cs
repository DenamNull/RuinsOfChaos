using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RuinsOfChaos.Content.Buffs;

namespace RuinsOfChaos.Common.GlobalNPCs
{
    public class BuffImmunity : GlobalNPC
    {
        public override void SetDefaults(NPC entity)
        {
            if (entity.buffImmune[BuffID.Midas])
            {
                entity.buffImmune[ModContent.GetInstance<CurseOfSilver>().Type] = true;
            }
            
        }
    }
}
