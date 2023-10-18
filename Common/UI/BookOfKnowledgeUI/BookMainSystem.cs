using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria.Localization;


namespace RuinsOfChaos.Common.UI.BookOfKnowledgeUI
{
    [Autoload(Side = ModSide.Client)]
    public class BookMainSystem : ModSystem
    {
        internal UserInterface KnowledgeBookUI;
        internal BookMainMenu BookMainUI;
        internal BookSelectClass selectClassUI;
        internal StatsView statsUI;
        /// <summary>
        /// The StateID of the last state that was used before being closed by the Book of Knowledge Item.
        /// </summary>
        public int LastState = 0;
        public const string BookUIPath = "Mods.RuinsOfChaos.UI.BookOfKnowledge";
        
        public enum StateID
        {
            MainMenu,
            SelectClasses,
            CharacterStatsView
        }

        public override void Load()
        {
            KnowledgeBookUI = new UserInterface();
            BookMainUI = new BookMainMenu();
            selectClassUI = new BookSelectClass();
            statsUI = new StatsView();
            BookMainUI.Activate();
            selectClassUI.Activate();
            statsUI.Activate();
        }
        /// <summary>
        /// Gives the StateID that the Book of Knowledge is currently set to.
        /// </summary>
        /// <returns></returns>
        public StateID? GetStateID()
        {
            if (KnowledgeBookUI?.CurrentState == BookMainUI)
            {
                return StateID.MainMenu;
            } else if (KnowledgeBookUI?.CurrentState == selectClassUI)
            {
                return StateID.SelectClasses;
            } else if (KnowledgeBookUI?.CurrentState == statsUI)
            {
                return StateID.CharacterStatsView;
            } else
            {
                return null;
            }
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (KnowledgeBookUI?.CurrentState != null)
            {
                KnowledgeBookUI.Update(gameTime);
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "RuinsOfChaos: Book of Knowledge",
                    delegate
                    {
                        if (KnowledgeBookUI?.CurrentState != null)
                        {
                            KnowledgeBookUI.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }
        /// <summary>
        /// Sets to a UIState corresponding to the state ID.
        /// </summary>
        /// <param name="stateID"></param>
        internal void SetBookState(int stateID)
        {
            switch (stateID)
            {
                case (int)StateID.MainMenu:
                    KnowledgeBookUI?.SetState(BookMainUI);
                    break;
                case (int)StateID.SelectClasses:
                    KnowledgeBookUI?.SetState(selectClassUI);
                    break;
                case (int)StateID.CharacterStatsView:
                    KnowledgeBookUI?.SetState(statsUI);
                    break;
                default:
                    Logging.PublicLogger.WarnFormat($"RuinsOfChaos: There is no state corresponding to the StateID: {0}", stateID);
                    break;
            }
        }
        /// <summary>
        /// Closes any Book of Knowledge UI.
        /// </summary>
        internal void ClearBook()
        {
            KnowledgeBookUI?.SetState(null);
        }
    }
}
