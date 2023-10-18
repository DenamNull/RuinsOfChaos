using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using RuinsOfChaos.Common;

namespace RuinsOfChaos.Common.UI.BookOfKnowledgeUI
{
    public class BookMainMenu : UIState
    {
        
        static public ReLogic.Content.Asset<Texture2D> DifficultyIcon = ModContent.Request<Texture2D>("Terraria/Images/UI/WorldCreation/IconDifficultyNormal");
        public UIImageButton diffSwitchButton = new UIImageButton(DifficultyIcon);
        public UIText techText = new UIText("Skill Tree");
        public UIPanel techTree = new UIPanel();
        public UIText header = new UIText("Book of Knowledge");
        public LevelBar levelBar = new LevelBar(4);
        public UIText lvText = new UIText("LV: 0");
        public UIText guideText = new UIText("Guide");
        public UIPanel GuideButton = new UIPanel();
        public UIPanel StatsButton = new UIPanel();
        public UIText statsText = new UIText("Stats");
        public UIPanel LoreButton = new UIPanel();
        public UIText LoreText = new UIText("Lore");
        public const string BookUIPath = BookMainSystem.BookUIPath;
        public override void OnInitialize()
        {
            float panelWidth = 250f;
            float panelHeight = 300f;
            Draggable panel = new Draggable();
            panel.SetPadding(0f);
            panel.Width.Set(panelWidth, 0f);
            panel.Height.Set(panelHeight, 0f);
            panel.HAlign = panel.VAlign = 0.5f;
            Append(panel);
            
            levelBar.Width.Set(208, 0f);
            levelBar.Height.Set(16, 0f);
            levelBar.Left.Set(6, 0f);
            levelBar.Top.Set(278, 0f);
            panel.Append(levelBar);
            UIImageButton exitButton = new UIImageButton(ModContent.Request<Texture2D>("Terraria/Images/UI/SearchCancel"));
            exitButton.Top.Set(2f, 0f);
            exitButton.Left.Set(224f, 0f);
            exitButton.Height.Set(24f, 0f);
            exitButton.Width.Set(24f, 0f);
            exitButton.OnLeftClick += new MouseEvent(OnExitButtonClick);
            panel.Append(exitButton);
            diffSwitchButton.Height.Set(32f, 0f);
            diffSwitchButton.Width.Set(32f, 0f);
            diffSwitchButton.Top.Set(266f, 0f);
            diffSwitchButton.Left.Set(216f, 0f);
            diffSwitchButton.OnLeftClick += new MouseEvent(OnDiffButtonClick);
            panel.Append(diffSwitchButton);
            
            lvText.Left.Set(6, 0f);
            lvText.Top.Set(260, 0f);
            panel.Append(lvText);
            header.SetText(Language.GetText($"{BookUIPath}.Header"));
            header.HAlign = 0.5f;
            header.Top.Set(10f, 0f);
            panel.Append(header);
            
            techTree.Width.Set(190f, 0f);
            techTree.HAlign = 0.5f;
            techTree.Top.Set(40f, 0f);
            techTree.Height.Set(50f, 0f);
            techTree.SetPadding(0f);
            panel.Append(techTree);
            
            techText.HAlign = techText.VAlign = 0.5f;
            techTree.Append(techText);

            GuideButton.Width.Set(190f, 0f);
            GuideButton.Height.Set(50f, 0f);
            GuideButton.HAlign = 0.5f;
            GuideButton.Top.Set(94f, 0f);
            GuideButton.SetPadding(0f);
            panel.Append(GuideButton);
            guideText.SetText(Language.GetText($"{BookUIPath}.Guide"));
            guideText.HAlign = guideText.VAlign = 0.5f;
            GuideButton.Append(guideText);

            StatsButton.Width.Set(190f, 0f);
            StatsButton.Height.Set(50f, 0f);
            StatsButton.HAlign = 0.5f;
            StatsButton.Top.Set(148f, 0f);
            StatsButton.SetPadding(0f);
            StatsButton.OnLeftClick += new MouseEvent(OnSelectStatsClick);
            panel.Append(StatsButton);
            statsText.SetText(Language.GetText($"{BookUIPath}.Stats"));
            statsText.HAlign = statsText.VAlign = 0.5f;
            StatsButton.Append(statsText);
            LoreText.SetText(Language.GetText($"{BookUIPath}.Lore"));
            LoreButton.Width.Set(190f, 0f);
            LoreButton.Height.Set(50f, 0f);
            LoreButton.HAlign = 0.5f;
            LoreButton.Top.Set(202f, 0f);
            LoreButton.SetPadding(0f);
            panel.Append(LoreButton);

            LoreText.HAlign = LoreText.VAlign = 0.5f;
            LoreButton.Append(LoreText);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            string classText = Language.GetTextValue($"{BookUIPath}.Class");
            string classSelectedText;
            switch (Main.LocalPlayer.GetModPlayer<RoCPlayer>().SelectedClass)
            {
                case 0:
                    classSelectedText = Language.GetTextValue($"{BookUIPath}.Melee");
                    break;
                case 1:
                    classSelectedText = Language.GetTextValue($"{BookUIPath}.Ranger");
                    break;
                case 2:
                    classSelectedText = Language.GetTextValue($"{BookUIPath}.Mage");
                    break;
                case 3:
                    classSelectedText = Language.GetTextValue($"{BookUIPath}.Summoner");
                    break;
                case 4:
                    classSelectedText = Language.GetTextValue($"{BookUIPath}.Assassin");
                    break;
                default:
                    classSelectedText = Language.GetTextValue($"{BookUIPath}.None");
                    break;
            }
            lvText.SetText("LV: " + Main.LocalPlayer.GetModPlayer<RoCPlayer>().Levels + "     " + classText + ": " + classSelectedText);

            switch (Main.GameMode)
            {
                case 0:
                    DifficultyIcon = ModContent.Request<Texture2D>("Terraria/Images/UI/WorldCreation/IconDifficultyNormal");
                    break;
                case 1:
                    DifficultyIcon = ModContent.Request<Texture2D>("Terraria/Images/UI/WorldCreation/IconDifficultyExpert");
                    break;
                case 2:
                    DifficultyIcon = ModContent.Request<Texture2D>("Terraria/Images/UI/WorldCreation/IconDifficultyMaster");
                    break;
            }
            diffSwitchButton.SetImage(DifficultyIcon);
            if (diffSwitchButton.IsMouseHovering)
            {
                switch (Main.GameMode)
                {
                    case 0:
                        Main.instance.MouseText(Language.GetTextValue($"{BookUIPath}.OnClassicMode"));
                        break;
                    case 1:
                        Main.instance.MouseText(Language.GetTextValue($"{BookUIPath}.OnExpertMode"));
                        break;
                    case 2:
                        Main.instance.MouseText(Language.GetTextValue($"{BookUIPath}.OnMasterMode"));
                        break;
                }
            }
            if (!Main.LocalPlayer.GetModPlayer<RoCPlayer>().HasSelectedClass)
            {
    
                techText.SetText(Language.GetTextValue($"{BookUIPath}.SelectClass"));
                techTree.OnLeftClick += OnSelectClassClick;
                if (techTree.IsMouseHovering || techText.IsMouseHovering)
                {
                    Main.instance.MouseText(Language.GetTextValue($"{BookUIPath}.ClickToGetGear"));
                }
                
            }
            else if (Main.LocalPlayer.GetModPlayer<RoCPlayer>().HasSelectedClass)
            {
                techText.SetText(Language.GetTextValue($"{BookUIPath}.SkillTree"));
                
                if (techTree.IsMouseHovering || techText.IsMouseHovering)
                {
                    Main.instance.MouseText(Language.GetTextValue($"{BookUIPath}.ComingSoon"));
                }
            }
            if (GuideButton.IsMouseHovering || guideText.IsMouseHovering)
            {
                Main.instance.MouseText(Language.GetTextValue($"{BookUIPath}.ComingSoon"));
            }
            
            if (StatsButton.IsMouseHovering || statsText.IsMouseHovering)
            {
                Main.instance.MouseText(Language.GetTextValue($"{BookUIPath}.StatsText"));
            }
            
            if (LoreButton.IsMouseHovering || LoreText.IsMouseHovering)
            {
                Main.instance.MouseText(Language.GetTextValue($"{BookUIPath}.ComingSoon"));
            }
        }
        public void OnExitButtonClick(UIMouseEvent evt, UIElement listeningElemen)
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            ModContent.GetInstance<BookMainSystem>().ClearBook();
            ModContent.GetInstance<BookMainSystem>().LastState = (int)BookMainSystem.StateID.MainMenu;
        }
        public void OnDiffButtonClick(UIMouseEvent evt, UIElement listeningElemen)
        {
            switch(Main.GameMode)
            {
                case 0:
                    Main.GameMode = 1;
                    SoundEngine.PlaySound(SoundID.NPCHit54);
                    break;
                case 1:
                    Main.GameMode = 2;
                    SoundEngine.PlaySound(SoundID.NPCHit57);
                    Main.NewText(Language.GetTextValue($"{BookUIPath}.ReminderToHaveFun"));
                    break;
                case 2:
                    Main.GameMode = 0;
                    SoundEngine.PlaySound(SoundID.Item57);
                    break;
            }
            if (Main.dedServ)
            {
                ModPacket difficultyChange = ModContent.GetInstance<RuinsOfChaos>().GetPacket();
                difficultyChange.Write((byte)RuinsOfChaos.PacketID.DiffChange);
                difficultyChange.Write((byte)Main.GameMode);
                difficultyChange.Send();
            }
            
        }
        public void OnSelectClassClick(UIMouseEvent evt, UIElement listeningElemen)
        {
            SoundEngine.PlaySound(SoundID.MenuOpen);
            techTree.OnLeftClick -= OnSelectClassClick;
            BookMainSystem bookUI = ModContent.GetInstance<BookMainSystem>();
            bookUI.ClearBook();
            bookUI.SetBookState((int)BookMainSystem.StateID.SelectClasses);
        }
        public void OnSelectStatsClick(UIMouseEvent evt, UIElement listeningElemen)
        {
            SoundEngine.PlaySound(SoundID.MenuOpen);
            BookMainSystem bookUI = ModContent.GetInstance<BookMainSystem>();
            bookUI.ClearBook();
            bookUI.SetBookState((int)BookMainSystem.StateID.CharacterStatsView);
        }
    }
}
