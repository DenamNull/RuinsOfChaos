using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RuinsOfChaos.Content.Items;
using Terraria.Localization;

namespace RuinsOfChaos.Common.GlobalNPCs
{
    public class GuideNewText : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.Guide;
        }
        public override void OnChatButtonClicked(NPC npc, bool firstButton)
        {
            base.OnChatButtonClicked(npc, firstButton);
            bool isBookInInv() {
                foreach (Item i in Main.LocalPlayer.inventory)
                {
                    if (i.type == ModContent.ItemType<BookOfKnowledge>())
                    {
                        return true;
                    }
                }
                return false;
            };
            if (Main.helpText == 217 && isBookInInv()) {
                Main.npcChatText = Language.GetTextValue("Mods.RuinsOfChaos.GuideMessages.BookHelp");
            }
        }
    }
}
