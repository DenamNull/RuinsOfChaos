using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RuinsOfChaos.Common.UI.BookOfKnowledgeUI;
using Terraria.Audio;
using Terraria.Localization;
using RuinsOfChaos.Common.Systems;
using System.Collections.Generic;

namespace RuinsOfChaos.Content.Items
{
    public class BookOfKnowledge : ModItem
    {
        public static LocalizedText bookKeybindText = Language.GetText("Mods.RuinsOfChaos.CommonItemTooltip.BookKeybindText");
        public static LocalizedText noKeybindText = Language.GetText("Mods.RuinsOfChaos.CommonItemTooltip.HasNoKeybindSelected");
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.useTime = 1;
            Item.useStyle = ItemUseStyleID.HiddenAnimation;
            Item.useAnimation = ItemUseStyleID.None;
            Item.holdStyle = ItemHoldStyleID.HoldLamp;
            
        }
        public override bool? UseItem(Player player)
        {
            if (ModContent.GetInstance<BookMainSystem>().KnowledgeBookUI.CurrentState == null && player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.MenuOpen);
                BookMainSystem bookUI = ModContent.GetInstance<BookMainSystem>();
                bookUI.SetBookState(bookUI.LastState);
                return true;
            } else if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.MenuClose);
                BookMainSystem bookUI = ModContent.GetInstance<BookMainSystem>();
                bookUI.LastState = (int)bookUI.GetStateID();
                bookUI.ClearBook();
                return true;
            }

            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string keybindText;
            if (RoCKeybinds.BookOfKnowledgeKeybind == null)
            {
                keybindText = bookKeybindText.Format(noKeybindText);
            } else
            {
                keybindText = bookKeybindText.Format(RoCKeybinds.BookOfKnowledgeKeybind.GetAssignedKeys()[0]);
            }
            TooltipLine keybindLine = new TooltipLine(Mod, "BookKeybindTooltip", keybindText);
            tooltips.Add(keybindLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Book, 5);
            recipe.AddTile(TileID.Bookcases);
            recipe.AddCondition(Condition.InUnderworld);
            recipe.Register();
        }
    }
}
