using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RuinsOfChaos.Common.Systems;
using RuinsOfChaos.Content.Projectiles.Friendly.Marksman.Crossbows;

namespace RuinsOfChaos.Content.Items.Weapons.Marksman
{
    public class PlatinumCrossbow : CrossbowSubclassTemplate
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 19;
            Item.height = 11;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 5, 5, 75);
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.UseSound = RoCSoundEffect.CrossbowFire;
            Item.shoot = ModContent.ProjectileType<PlatinumBolt>();
            Item.shootSpeed = 12.25f;
            Item.noMelee = true;
            Item.damage = 28;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.knockBack = 5f;
            Item.DamageType = ModContent.GetInstance<AssassinClass>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PlatinumBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
