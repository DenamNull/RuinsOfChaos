using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RuinsOfChaos.Common.Systems;
using RuinsOfChaos.Content.Projectiles.Friendly.Marksman.Crossbows;

namespace RuinsOfChaos.Content.Items.Weapons.Marksman
{
    public class CopperCrossbow : CrossbowSubclassTemplate
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 9;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 0, 15, 50);
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.UseSound = RoCSoundEffect.CrossbowFire;
            Item.shoot = ModContent.ProjectileType<CopperBolt>();
            Item.shootSpeed = 11.5f;
            Item.noMelee = true;
            Item.damage = 9;
            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.knockBack = 4.25f;
            Item.DamageType = ModContent.GetInstance<AssassinClass>();
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CopperBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        
    }
}
