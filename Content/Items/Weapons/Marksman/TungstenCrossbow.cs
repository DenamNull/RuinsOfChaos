using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RuinsOfChaos.Common.Systems;
using RuinsOfChaos.Content.Projectiles.Friendly.Marksman.Crossbows;

namespace RuinsOfChaos.Content.Items.Weapons.Marksman
{
    public class TungstenCrossbow : CrossbowSubclassTemplate
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 9;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 1, 25, 55);
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.UseSound = RoCSoundEffect.CrossbowFire;
            Item.shoot = ModContent.ProjectileType<TungstenBolt>();
            Item.shootSpeed = 12f;
            Item.noMelee = true;
            Item.damage = 23;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.knockBack = 4.75f;
            Item.DamageType = ModContent.GetInstance<AssassinClass>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TungstenBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
