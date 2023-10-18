using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RuinsOfChaos.Common.Systems;
using RuinsOfChaos.Content.Projectiles.Friendly.Marksman.Crossbows;

namespace RuinsOfChaos.Content.Items.Weapons.Marksman
{
    public class IronCrossbow : CrossbowSubclassTemplate
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 9;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 0, 55, 50);
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.UseSound = RoCSoundEffect.CrossbowFire;
            Item.shoot = ModContent.ProjectileType<IronBolt>();
            Item.shootSpeed = 11.75f;
            Item.noMelee = true;
            Item.damage = 15;
            Item.useTime = 44;
            Item.useAnimation = 44;
            Item.knockBack = 4.5f;
            Item.DamageType = ModContent.GetInstance<AssassinClass>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
