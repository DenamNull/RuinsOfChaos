using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace RuinsOfChaos.Content.Buffs
{
    public class CurseOfSilver : ModBuff
    {
        internal int Timer = 0;
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = false;
            
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.buffTime[buffIndex] == 0)
            {
                buffIndex--;
                Timer = 0;
                return;
            }
            if (Timer++ % 20 != 0) //Should run every 1/3 second
            {
                return;
            }
            var hitInfo = npc.CalculateHitInfo(10, 1, false, 0, DamageClass.Default, true, Main.player[npc.lastInteraction].luck);
            npc.StrikeNPC(hitInfo, false, true);
            NetMessage.SendStrikeNPC(npc, hitInfo);
            int CoinAmount = Main.rand.Next(45, 1500);
            if (Main.rand.NextBool(150))
            {
                Vector2 vector = Main.rand.NextVector2Circular(1f, 1f);
                vector += npc.Center;
                int index = Item.NewItem(new EntitySource_Buff(npc, Type, buffIndex), vector, ItemID.GoldCoin, 1, true);
                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    ModPacket modPacket = ModContent.GetInstance<RuinsOfChaos>().GetPacket();
                    modPacket.Write((byte)RuinsOfChaos.PacketID.SyncGoldCoinSpawn);
                    modPacket.Write7BitEncodedInt(index);
                    modPacket.WriteVector2(vector);
                    modPacket.Send();
                }
            } else
            {
                if (npc.boss)
                {
                    CoinAmount = (int)Math.Floor((double)CoinAmount / 10);
                }
                int copperCoinNum = CoinAmount % 100;
                int silverCoinNum = (int)Math.Floor((double)CoinAmount / 100);

                int index1 = 0;
                int index2 = 0;
                Vector2 copperVector = Main.rand.NextVector2Circular(1f, 1f);
                Vector2 silverVector = Main.rand.NextVector2Circular(1f, 1f);
                copperVector += npc.Center;
                silverVector += npc.Center;
                if (silverCoinNum != 0)
                {
                    index1 = Item.NewItem(new EntitySource_Buff(npc, Type, buffIndex), copperVector, ItemID.SilverCoin, silverCoinNum, true);
                }
                if (copperCoinNum != 0)
                {
                    index2 = Item.NewItem(new EntitySource_Buff(npc, Type, buffIndex), silverVector, ItemID.CopperCoin, copperCoinNum, true);
                }
                if (Main.netMode != NetmodeID.SinglePlayer && (silverCoinNum != 0 || copperCoinNum != 0))
                {
                    ModPacket modPacket = ModContent.GetInstance<RuinsOfChaos>().GetPacket();
                    modPacket.Write((byte)RuinsOfChaos.PacketID.SyncSilverCoinSpawn); 
                    modPacket.Write7BitEncodedInt(index1);
                    modPacket.Write7BitEncodedInt(index2);
                    modPacket.Write7BitEncodedInt(CoinAmount);
                    modPacket.WriteVector2(copperVector);
                    modPacket.WriteVector2(silverVector);
                    modPacket.Send();
                }
            }
        }
    }
}
