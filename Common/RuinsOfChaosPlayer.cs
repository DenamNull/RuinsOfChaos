using RuinsOfChaos.Content.Items;
using RuinsOfChaos.Content;
using System;
using System.Collections.Generic;
using Terraria;
using RuinsOfChaos.Common.UI.BookOfKnowledgeUI;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using RuinsOfChaos.Common.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.Audio;

namespace RuinsOfChaos.Common
{
    public class RoCPlayer : ModPlayer
    {
        private int _experience;
        /// <summary>
        /// Experience Points of this player.
        /// The resource behind the RPG-like mechanics of the mod.
        /// This can be edited freely, and is used in determining the levels of this player.
        /// <see cref="Levels"/> are automatically set upon any change to the experience.
        /// </summary>
        public int Experience
        {
            get => _experience;
            set {
                _experience = value;
                Levels = CalculateLevels(_experience);
            }
        }
        /// <summary>
        /// Levels are a stat which governs most of the RPG elements of this mod, such as buffs when the player levels up.
        /// Levels are automatically set upon assigning a value to <see cref="Experience"/>.
        /// </summary>
        /// <remarks>
        /// This cannot be without <see cref="Experience"/> being changed
        /// </remarks>
        public int Levels { get; private set; }
        /// <summary>
        /// Whether a the player has selected a damage class from the Book of Knowledge.
        /// Can only be set when <see cref="SelectedClass"/> has been changed.
        /// </summary>
        public bool HasSelectedClass { get; private set; }
        private int _selectedClass;
        /// <summary>The selected damage class a player has chosen.
        /// <list> 0 for Melee class </list>
        /// <list> 1 for Ranger class </list>
        /// <list> 2 for Mage class </list>
        /// <list> 3 for Summoner class </list>
        /// <list> 4 for Assassin class </list>
        /// </summary>
        /// <remarks>Negative values mean a class has not been chosen yet.</remarks>
        public int SelectedClass
        {
            get => _selectedClass;
            set
            {
                _selectedClass = value;
                HasSelectedClass = _selectedClass != -1;
            }
        }
        /// <summary>
        /// The amount of Precision a player has.
        /// Used to influence the damage of Assassin Class weapons.
        /// </summary>
        public int Precision;
        /// <summary>
        /// The maximum amount of Precision a player gets upon creation.
        /// </summary>
        public const int IntialPrecisionMax = 200;
        /// <summary>
        /// The maximum amount of Precision a player will have by default.
        /// Should only be changed for permanent buffs.
        /// If you want to set temporary buffs to the maximum, you should change <see cref="PrecisionCap"/> instead.
        /// </summary>
        public int DefaultPrecisionMax { get; protected set; }
        /// <summary>
        /// The maximum amount of Precision a player can have.
        /// Can be free modified or changed, however things might break if it goes below <see cref="DefaultPrecisionMax"/>.
        /// You should access this if you want to change the max by accessories, buffs, or armor.
        /// </summary>
        public int PrecisionCap;
        /// <summary>
        /// Sets default values upon player creation.
        /// </summary>
        RoCPlayer()
        {
            Experience = 0;
            SelectedClass = -1;
            Precision = 0;
            DefaultPrecisionMax = IntialPrecisionMax;
            PrecisionCap = DefaultPrecisionMax;
        }
        /// <summary>
        /// Calculates the level a player should be at when they reach the specified amount of Experience Points.
        /// Does not set the actual level of the player this function is run on.
        /// </summary>
        /// <param name="xp">Amount of Experience</param>
        /// <returns>The level a player will reach when they have <paramref name="xp"/> Experience</returns>
        public static int CalculateLevels(int xp)
        {
            return (int)Math.Floor(Math.Cbrt(xp / 500) + 1);
        }
        /// <summary>
        /// Checks if a player will gain a new level at <paramref name="xp"/> Experience Points.
        /// </summary>
        /// <param name="xp"></param>
        /// <returns><c>true</c> if the XP corresponds to a new level.</returns>
        public bool IsNewLevel(int xp)
        {
            return CalculateLevels(xp) != Levels;
        }
        /// <summary>
        /// Gets the XP that a player must be at least to be at a certain level.
        /// Inverse of <seealso cref="CalculateLevels(int)"/>.
        /// </summary>
        /// <param name="lvl"></param>
        /// <returns></returns>
        public static int GetXPRequiredForLevel(int lvl)
        {
            if (lvl > 1)
            {
                return 500 * (int)Math.Pow((lvl - 1), 3);
            }
            else
            {
                return 0;
            }
        }
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            return new[]
            {
                new Item(ModContent.ItemType<BookOfKnowledge>())
            };
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (item.CountsAsClass<AssassinClass>())
            {
                float damageScalar = 1.75f * ((float)Precision / PrecisionCap);
                damage += damageScalar;
            } 
        }
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket modPacket = ModContent.GetInstance<RuinsOfChaos>().GetPacket();
            modPacket.Write((byte)RuinsOfChaos.PacketID.SyncPrecisionPlayer);
            modPacket.Write((byte)Player.whoAmI);
            modPacket.Write7BitEncodedInt(PrecisionCap);
            modPacket.Write7BitEncodedInt(Precision);
            modPacket.Send(toWho, fromWho);
        }
        public override void ResetEffects()
        {
            PrecisionCap = DefaultPrecisionMax;
            if (Precision > PrecisionCap)
            {
                Precision = PrecisionCap;
            }
        }
        public override void UpdateDead()
        {
            Precision = 0;
            PrecisionCap = DefaultPrecisionMax;
        }
        public override void PreUpdate()
        {
            if (Precision > PrecisionCap)
            {
                Precision = PrecisionCap;
            }
        }
        public override void Initialize()
        {
            Precision = 0;
            DefaultPrecisionMax = IntialPrecisionMax;
        }
        public override void CopyClientState(ModPlayer targetCopy)
        {
            RoCPlayer clone = (RoCPlayer)targetCopy;
            clone.PrecisionCap = PrecisionCap;
            clone.Precision = Precision;
        }
        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            RoCPlayer clone = (RoCPlayer)clientPlayer;

            if (clone.PrecisionCap != PrecisionCap || clone.Precision != Precision) {
                SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag[nameof(Experience)] = Experience;
            tag[nameof(SelectedClass)] = SelectedClass;
            tag[nameof(DefaultPrecisionMax)] = DefaultPrecisionMax;
        }
        public override void LoadData(TagCompound tag)
        {
            Experience = tag.GetInt(nameof(Experience));
            SelectedClass = tag.GetInt(nameof(SelectedClass));
            DefaultPrecisionMax = tag.GetInt(nameof(DefaultPrecisionMax));
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (RoCKeybinds.BookOfKnowledgeKeybind.JustPressed)
            {
                foreach (Item i in Player.inventory)
                {
                    if (i.type == ModContent.ItemType<BookOfKnowledge>())
                    {
                        BookMainSystem bookUI = ModContent.GetInstance<BookMainSystem>();
                        if (bookUI.KnowledgeBookUI.CurrentState == null)
                        {
                            SoundEngine.PlaySound(SoundID.MenuOpen);
                            bookUI.SetBookState(bookUI.LastState);
                        } else
                        {
                            SoundEngine.PlaySound(SoundID.MenuClose);
                            bookUI.LastState = (int)bookUI.GetStateID();
                            bookUI.ClearBook();
                        }
                    }
                }
            }
        }
    }
}
