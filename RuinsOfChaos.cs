using System.IO;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using RuinsOfChaos.Common;
using RuinsOfChaos.Common.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using ReLogic.Content;
namespace RuinsOfChaos
{
	public class RuinsOfChaos : Mod
	{
        public enum PacketID
        {
            SyncXP,
            DiffChange,
            MeleeSelected,
            RangerSelected,
            MageSelected,
            SummonerSelected,
            AssassinSelected,
            SyncPrecisionPlayer,
            SyncPrecision,
            SyncAssassinWeaponFields,
            SyncGoldCoinSpawn,
            SyncSilverCoinSpawn
        }
        public override void Load()
        {
            BackgroundTextureLoader.AddBackgroundTexture(this, "RuinsOfChaos/Assets/Textures/MainMenuUI/Empty");
            base.Load();
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            PacketID packetType = (PacketID)reader.ReadByte();
            switch (packetType)
            {
                case PacketID.SyncXP:
                    Player thisPlayer = Main.player[reader.ReadByte()];
                    thisPlayer.GetModPlayer<RoCPlayer>().Experience = reader.Read7BitEncodedInt();
                    break;
                case PacketID.DiffChange:
                    Main.GameMode = reader.ReadByte();
                    break;
                case PacketID.MeleeSelected:
                    int meleePlayer = reader.ReadByte();
                    Main.player[meleePlayer].GetModPlayer<RoCPlayer>().SelectedClass = 0;
                    break;
                case PacketID.RangerSelected:
                    int rangerPlayer = reader.ReadByte();
                    Main.player[rangerPlayer].GetModPlayer<RoCPlayer>().SelectedClass = 1;
                    break;
                case PacketID.MageSelected:
                    int magePlayer = reader.ReadByte();
                    Main.player[magePlayer].GetModPlayer<RoCPlayer>().SelectedClass = 2;
                    break;
                case PacketID.SummonerSelected:
                    int summonerPlayer = reader.ReadByte();
                    Main.player[summonerPlayer].GetModPlayer<RoCPlayer>().SelectedClass = 3;
                    break;
                case PacketID.AssassinSelected:
                    int assassinPlayer = reader.ReadByte();
                    Main.player[assassinPlayer].GetModPlayer<RoCPlayer>().SelectedClass = 4;
                    break;
                case PacketID.SyncPrecisionPlayer:
                    byte PrecPlayer = reader.ReadByte();
                    Main.player[PrecPlayer].GetModPlayer<RoCPlayer>().PrecisionCap = reader.Read7BitEncodedInt();
                    Main.player[PrecPlayer].GetModPlayer<RoCPlayer>().Precision = reader.Read7BitEncodedInt();
                    if (Main.netMode == NetmodeID.Server)
                    {
                        Main.player[PrecPlayer].GetModPlayer<RoCPlayer>().SyncPlayer(-1, whoAmI, false);
                    }
                    break;
                case PacketID.SyncPrecision:
                    byte SyncPlayer = reader.ReadByte();
                    Main.player[SyncPlayer].GetModPlayer<RoCPlayer>().Precision = reader.ReadByte();
                    break;
                case PacketID.SyncAssassinWeaponFields:
                    byte assassinProjectile = reader.ReadByte();
                    PrecisionIncrease modProj = Main.projectile[assassinProjectile].GetGlobalProjectile<PrecisionIncrease>();
                    modProj.precIncr = reader.ReadByte();
                    modProj.precDecr = reader.ReadByte();
                    break;
                case PacketID.SyncGoldCoinSpawn:
                    int index = reader.Read7BitEncodedInt();
                    Vector2 vector = reader.ReadVector2();
                    Main.item[index].type = ItemID.GoldCoin;
                    Main.item[index].position = vector;
                    Main.item[index].timeLeftInWhichTheItemCannotBeTakenByEnemies = 3600;
                    break;
                case PacketID.SyncSilverCoinSpawn:
                    int silverCoinIndex = reader.Read7BitEncodedInt();
                    int copperCoinIndex = reader.Read7BitEncodedInt();
                    int coinNum = reader.Read7BitEncodedInt();
                    int copperCoinNum = coinNum % 100;
                    int silverCoinNum = (int)Math.Floor((double)coinNum / 100);
                    Vector2 copperVector = reader.ReadVector2();
                    Vector2 silverVector = reader.ReadVector2();
                    if (silverCoinNum == 0 && copperCoinNum == 0)
                    {
                        Console.WriteLine("What the fuck did you do?");
                        break;
                    }
                    if (silverCoinNum != 0)
                    {
                        Main.item[silverCoinIndex].type = ItemID.SilverCoin;
                        Main.item[silverCoinIndex].stack = silverCoinNum;
                        Main.item[silverCoinIndex].position = silverVector;
                        Main.item[silverCoinIndex].timeLeftInWhichTheItemCannotBeTakenByEnemies = 3600;
                    }
                    if (copperCoinNum != 0)
                    {
                        Main.item[copperCoinIndex].type = ItemID.CopperCoin;
                        Main.item[copperCoinIndex].stack = copperCoinNum;
                        Main.item[copperCoinIndex].position = copperVector;
                        Main.item[copperCoinIndex].timeLeftInWhichTheItemCannotBeTakenByEnemies = 3600;
                    } 
                    break;
                default:
                    Logger.WarnFormat($"RuinsOfChaos: Unknown message type: {0}", packetType);
                    break;
            }
        }
    }
}