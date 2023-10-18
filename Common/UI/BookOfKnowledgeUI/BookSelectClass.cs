using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.Audio;
using Terraria.ID;
using Terraria.DataStructures;
using RuinsOfChaos.Content.Items.Accessories;
using Microsoft.Xna.Framework;
using RuinsOfChaos.Content.Items.Weapons.Marksman;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;

namespace RuinsOfChaos.Common.UI.BookOfKnowledgeUI
{
    public class BookSelectClass : UIState
    {
        internal EntitySource_Misc Source = new EntitySource_Misc("ClassSelected");
       
        public UIPanel meleeButton = new UIPanel();
        public UIText meleeText = new UIText("Melee");
        public UIPanel rangerButton = new UIPanel();
        public UIText rangerText = new UIText("Ranger");
        public UIPanel mageButton = new UIPanel();
        public UIText mageText = new UIText("Mage");
        public UIPanel summonerButton = new UIPanel();
        public UIText summonerText = new UIText("Summoner");
        public UIPanel assassinButton = new UIPanel();
        public UIText assassinText = new UIText("Assassin");
        public UIText header = new UIText("Select a Class");
        public const string BookUIPath = BookMainSystem.BookUIPath;
        public override void OnInitialize()
        {
            Draggable panel = new Draggable();
            panel.SetPadding(0f);
            panel.Height.Set(104f, 0f);
            panel.Width.Set(560f, 0f);
            panel.HAlign = panel.VAlign = 0.5f;
            Append(panel);
            header.Top.Set(10f, 0f);
            header.HAlign = 0.5f;
            panel.Append(header);
            
            meleeButton.Width.Set(100f, 0f);
            meleeButton.Height.Set(50f, 0f);
            meleeButton.Left.Set(10f, 0f);
            meleeButton.Top.Set(45f, 0f);
            meleeButton.SetPadding(0f);
            meleeButton.OnLeftClick += new MouseEvent(OnMeleeButtonClick);
            
            meleeText.VAlign = meleeText.HAlign = 0.5f;
            panel.Append(meleeButton);
            meleeButton.Append(meleeText);
            
            rangerButton.Width.Set(100f, 0f);
            rangerButton.Height.Set(50f, 0f);
            rangerButton.Left.Set(120f, 0f);
            rangerButton.Top.Set(45f, 0f);
            rangerButton.SetPadding(0f);
            rangerButton.OnLeftClick += new MouseEvent(OnRangerButtonClick);
            
            rangerText.VAlign = rangerText.HAlign = 0.5f;
            panel.Append(rangerButton);
            rangerButton.Append(rangerText);
            
            mageButton.Width.Set(100f, 0f);
            mageButton.Height.Set(50f, 0f);
            mageButton.Left.Set(230f, 0f);
            mageButton.Top.Set(45f, 0f);
            mageButton.SetPadding(0f);
            mageButton.OnLeftClick += new MouseEvent(OnMageButtonClick);
           
            mageText.VAlign = mageText.HAlign = 0.5f;
            panel.Append(mageButton);
            mageButton.Append(mageText);
            
            summonerButton.Width.Set(100f, 0f);
            summonerButton.Height.Set(50f, 0f);
            summonerButton.Left.Set(340f, 0f);
            summonerButton.Top.Set(45f, 0f);
            summonerButton.SetPadding(0f);
            summonerButton.OnLeftClick += new MouseEvent(OnSummonerButtonClick);
            
            summonerText.VAlign = summonerText.HAlign = 0.5f;
            panel.Append(summonerButton);
            summonerButton.Append(summonerText);

            assassinButton.Width.Set(100f, 0f);
            assassinButton.Height.Set(50f, 0f);
            assassinButton.Left.Set(450f, 0f);
            assassinButton.Top.Set(45f, 0f);
            assassinButton.SetPadding(0f);
            assassinButton.OnLeftClick += new MouseEvent(OnAssassinButtonClick);

            assassinText.VAlign = assassinText.HAlign = 0.5f;
            panel.Append(assassinButton);
            assassinButton.Append(assassinText);

            meleeText.SetText(Language.GetText($"{BookUIPath}.Melee"));
            rangerText.SetText(Language.GetText($"{BookUIPath}.Ranger"));
            mageText.SetText(Language.GetText($"{BookUIPath}.Mage"));
            summonerText.SetText(Language.GetText($"{BookUIPath}.Summoner"));
            assassinText.SetText(Language.GetText($"{BookUIPath}.Assassin"));
            header.SetText(Language.GetText($"{BookUIPath}.SelectClassHeader"));
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (meleeButton.IsMouseHovering || meleeText.IsMouseHovering)
            {
                Main.instance.MouseText(Language.GetTextValue($"{BookUIPath}.MeleeText"));
            }
            if (rangerButton.IsMouseHovering || rangerText.IsMouseHovering)
            {
                Main.instance.MouseText(Language.GetTextValue($"{BookUIPath}.RangerText"));
            }
            if (mageButton.IsMouseHovering || mageText.IsMouseHovering)
            {
                Main.instance.MouseText(Language.GetTextValue($"{BookUIPath}.MageText"));
            }
            if (summonerButton.IsMouseHovering || summonerText.IsMouseHovering)
            {
                Main.instance.MouseText(Language.GetTextValue($"{BookUIPath}.SummonerText"));
            }
            if (assassinButton.IsMouseHovering || assassinText.IsMouseHovering)
            {
                Main.instance.MouseText(Language.GetTextValue($"{BookUIPath}.AssassinText"));
            }
        }
        
        public void OnMeleeButtonClick(UIMouseEvent evt, UIElement listeningElemen)
        {
            Main.LocalPlayer.GetModPlayer<RoCPlayer>().SelectedClass = 0;
            if (Main.dedServ)
            {
                ModPacket meleeSelected = ModContent.GetInstance<RuinsOfChaos>().GetPacket();
                meleeSelected.Write((byte)RuinsOfChaos.PacketID.MeleeSelected);
                meleeSelected.Write((byte)Main.myPlayer);
                meleeSelected.Send();
            }
            Main.LocalPlayer.QuickSpawnItem(Source, ItemID.CopperHammer);
            Main.LocalPlayer.QuickSpawnItem(Source, ItemID.CopperBroadsword);
            Main.LocalPlayer.QuickSpawnItem(Source, ItemID.LifeCrystal);
            Main.LocalPlayer.QuickSpawnItem(Source, ModContent.ItemType<RingOfSumasen>());
            SoundEngine.PlaySound(SoundID.MenuOpen);
            BookMainSystem bookUI = ModContent.GetInstance<BookMainSystem>();
            bookUI.ClearBook();
            bookUI.SetBookState((int)BookMainSystem.StateID.MainMenu);
        }
        public void OnRangerButtonClick(UIMouseEvent evt, UIElement listeningElemen)
        {
            Main.LocalPlayer.GetModPlayer<RoCPlayer>().SelectedClass = 1;
            Main.LocalPlayer.QuickSpawnItem(Source, ItemID.CopperBow);
            Main.LocalPlayer.QuickSpawnItem(Source, ItemID.CopperHammer);
            Main.LocalPlayer.QuickSpawnItem(Source, ModContent.ItemType<RingOfSumasen>());
            Main.LocalPlayer.QuickSpawnItem(Source, ItemID.WoodenArrow, 100);
            if (Main.dedServ)
            {
                ModPacket rangerSelected = ModContent.GetInstance<RuinsOfChaos>().GetPacket();
                rangerSelected.Write((byte)RuinsOfChaos.PacketID.RangerSelected);
                rangerSelected.Write((byte)Main.myPlayer);
                rangerSelected.Send();
            }
            SoundEngine.PlaySound(SoundID.MenuOpen);
            BookMainSystem bookUI = ModContent.GetInstance<BookMainSystem>();
            bookUI.ClearBook();
            bookUI.SetBookState((int)BookMainSystem.StateID.MainMenu);
        }
        public void OnMageButtonClick(UIMouseEvent evt, UIElement listeningElemen)
        {
            Main.LocalPlayer.GetModPlayer<RoCPlayer>().SelectedClass = 2;
            Main.LocalPlayer.QuickSpawnItem(Source, ItemID.AmethystStaff);
            Main.LocalPlayer.QuickSpawnItem(Source, ItemID.CopperHammer);
            Main.LocalPlayer.QuickSpawnItem(Source, ItemID.ManaCrystal);
            Main.LocalPlayer.QuickSpawnItem(Source, ModContent.ItemType<RingOfSumasen>());
            if (Main.dedServ)
            {
                ModPacket mageSelected = ModContent.GetInstance<RuinsOfChaos>().GetPacket();
                mageSelected.Write((byte)RuinsOfChaos.PacketID.MageSelected);
                mageSelected.Write((byte)Main.myPlayer);
                mageSelected.Send();
            }
            SoundEngine.PlaySound(SoundID.MenuOpen);
            BookMainSystem bookUI = ModContent.GetInstance<BookMainSystem>();
            bookUI.ClearBook();
            bookUI.SetBookState((int)BookMainSystem.StateID.MainMenu);
        }
        public void OnSummonerButtonClick(UIMouseEvent evt, UIElement listeningElemen)
        {
            Main.LocalPlayer.GetModPlayer<RoCPlayer>().SelectedClass = 3;
            Main.LocalPlayer.QuickSpawnItem(Source, ItemID.BabyBirdStaff);
            Main.LocalPlayer.QuickSpawnItem(Source, ItemID.CopperHammer);
            Main.LocalPlayer.QuickSpawnItem(Source, ItemID.ManaCrystal);
            Main.LocalPlayer.QuickSpawnItem(Source, ModContent.ItemType<RingOfSumasen>());
            if (Main.dedServ)
            {
                ModPacket summonerSelected = ModContent.GetInstance<RuinsOfChaos>().GetPacket();
                summonerSelected.Write((byte)RuinsOfChaos.PacketID.SummonerSelected);
                summonerSelected.Write((byte)Main.myPlayer);
                summonerSelected.Send();
            }
            SoundEngine.PlaySound(SoundID.MenuOpen);
            BookMainSystem bookUI = ModContent.GetInstance<BookMainSystem>();
            bookUI.ClearBook();
            bookUI.SetBookState((int)BookMainSystem.StateID.MainMenu);
        }
        public void OnAssassinButtonClick(UIMouseEvent evt, UIElement listeningElemen)
        {
            Main.LocalPlayer.GetModPlayer<RoCPlayer>().SelectedClass = 4;
            Main.LocalPlayer.QuickSpawnItem(Source, ModContent.ItemType<CopperCrossbow>());
            Main.LocalPlayer.QuickSpawnItem(Source, ItemID.CopperHammer);
            Main.LocalPlayer.QuickSpawnItem(Source, ModContent.ItemType<RingOfSumasen>());
            if (Main.dedServ)
            {
                ModPacket assassinSelected = ModContent.GetInstance<RuinsOfChaos>().GetPacket();
                assassinSelected.Write((byte)RuinsOfChaos.PacketID.AssassinSelected);
                assassinSelected.Write((byte)Main.myPlayer);
                assassinSelected.Send();
            }
            SoundEngine.PlaySound(SoundID.MenuOpen);
            BookMainSystem bookUI = ModContent.GetInstance<BookMainSystem>();
            bookUI.ClearBook();
            bookUI.SetBookState((int)BookMainSystem.StateID.MainMenu);
        }
    }
}
