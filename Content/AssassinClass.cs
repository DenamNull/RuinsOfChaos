using Terraria;
using Terraria.ModLoader;
using System;
using RuinsOfChaos.Common;

namespace RuinsOfChaos.Content
{
    public class AssassinClass : DamageClass
    {
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Generic)
            {
                return StatInheritanceData.Full;
            }
            return StatInheritanceData.None;
        }
        
        public override bool UseStandardCritCalcs => false;

        public override bool ShowStatTooltipLine(Player player, string lineName)
        {
            if (lineName == "CritChance")
            {
                return false;
            }
            return base.ShowStatTooltipLine(player, lineName);
        }
    }
}
