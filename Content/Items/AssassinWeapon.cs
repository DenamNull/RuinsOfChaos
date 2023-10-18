using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using RuinsOfChaos.Common.GlobalProjectiles;

namespace RuinsOfChaos.Content.Items
{
    public abstract class AssassinWeapon : ModItem
    {
        public int PrecisionIncrement = 10;
        public int PrecisionDecriment = 50;
        public int Subclass = 0;
        public override void SetDefaults()
        {
            Item.DamageType = ModContent.GetInstance<AssassinClass>();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltip1 = new TooltipLine(Mod, "AssassinInfoShift", Language.GetTextValue("Mods.RuinsOfChaos.CommonItemTooltip.AssassinClassInfoMessage"));
            tooltips.Add(tooltip1);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback);
                PrecisionIncrease modProj = proj.GetGlobalProjectile<PrecisionIncrease>();
                modProj.precIncr = PrecisionIncrement;
                modProj.precDecr = PrecisionDecriment;
            }
            return false;
        }
    }
    public abstract class CrossbowSubclassTemplate : AssassinWeapon
    {
        public override void SetDefaults()
        {
            Subclass = 1;
            PrecisionIncrement = 15;
            PrecisionDecriment = 75;
        }
    }
    public abstract class TrackerSubclassTemplate : AssassinWeapon
    {
        public override void SetDefaults()
        {
            Subclass = 2;
            PrecisionIncrement = 10;
            PrecisionDecriment = 5;
        }
    }
    public abstract class ThrowingKnivesSubclassTemplate : AssassinWeapon
    {
        public override void SetDefaults()
        {
            Subclass = 3;
            PrecisionIncrement = 10;
            PrecisionDecriment = 45;
        }
    }
    public abstract class SniperRiflesSubclassTemplate : AssassinWeapon
    {
        public override void SetDefaults()
        {
            Subclass = 4;
            PrecisionIncrement = 45;
            PrecisionDecriment = 100;
        }
    }
}
