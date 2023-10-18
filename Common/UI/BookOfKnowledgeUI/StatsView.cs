using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;

namespace RuinsOfChaos.Common.UI.BookOfKnowledgeUI
{
    public class StatsView : UIState
    {
        public const string BookLocalizationPath = BookMainSystem.BookUIPath;
        internal UIText header = new UIText("Character Stats");
        internal Draggable panel = new Draggable();
        internal UIImageButton backButton = new UIImageButton(ModContent.Request<Texture2D>("Terraria/Images/UI/Bestiary/Button_Back"));
        internal UIImageButton exitButton = new UIImageButton(ModContent.Request<Texture2D>("Terraria/Images/UI/SearchCancel"));
        public override void OnInitialize()
        {
            panel.Width.Set(500, 0f);
            panel.Height.Set(200, 0f);
            panel.HAlign = panel.VAlign = 0.5f;
            panel.SetPadding(0f);
            Append(panel);
            backButton.Width.Set(22f, 0f);
            backButton.Height.Set(22f, 0f);
            backButton.Left.Set(4f, 0f);
            backButton.Top.Set(4f, 0f);
            backButton.OnLeftClick += new MouseEvent(OnBackClick);
            header.SetText(Language.GetText($"{BookLocalizationPath}.StatsHeader"));
            header.HAlign = 0.5f;
            header.Top.Set(10f, 0f);
            panel.Append(backButton);
            panel.Append(header);
            exitButton.Width.Set(22f, 0f);
            exitButton.Height.Set(22f, 0f);
            exitButton.Left.Set(474f, 0f);
            exitButton.Top.Set(2f, 0f);
            exitButton.OnLeftClick += new MouseEvent(OnExitClick);
            panel.Append(exitButton);
            //Not Done... Needs to actually have the stats lol
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public void OnBackClick(UIMouseEvent evt, UIElement listeningElemen)
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            BookMainSystem bookUI = ModContent.GetInstance<BookMainSystem>();
            bookUI.ClearBook();
            bookUI.SetBookState((int)BookMainSystem.StateID.MainMenu);
        }
        public void OnExitClick(UIMouseEvent evt, UIElement listeningElemen)
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            BookMainSystem bookUI = ModContent.GetInstance<BookMainSystem>();
            bookUI.LastState = (int)BookMainSystem.StateID.CharacterStatsView;
            bookUI.ClearBook();
        }
    }
}
