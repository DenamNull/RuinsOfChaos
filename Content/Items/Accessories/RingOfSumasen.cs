using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace RuinsOfChaos.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class RingOfSumasen : ModItem
    {
        public static readonly int PercentDamage = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(PercentDamage);
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 12;
            Item.height = 9;
            Item.value = Item.buyPrice(0, 1, 5, 0);
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 5;
            player.GetDamage(DamageClass.Generic) += 0.05f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 5);
            recipe.AddIngredient(ItemID.CrimtaneBar, 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
