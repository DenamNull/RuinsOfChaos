using Terraria;
using Terraria.ModLoader;
using System;
using System.Linq;
using Terraria.DataStructures;
using Terraria.ID;
using System.Collections.Generic;
using RuinsOfChaos.Common;
using RuinsOfChaos;

namespace RuinsOfChaos.Common.GlobalNPCs
{
    public class XPGain : GlobalNPC
    {
        
        public override void OnKill(NPC npc)
        {
            if (!npc.friendly)
            {
                List<int> playersWhichDealtDamage = new List<int>();
                //checks for every player.
                for (int i = 0; i < Main.player.Count(); i++)
                {
                    //checks if that player dealt damage to the NPC, then adds them to the list.
                    if (npc.playerInteraction[i])
                    {
                        playersWhichDealtDamage.Add(i);
                    }
                }
                //Distributes the XP between each player and sets the levels accordingly.
                for (int j = 0; j < playersWhichDealtDamage.Count(); j++)
                {
                    double experienceAddedToEachPlayer = npc.lifeMax / playersWhichDealtDamage.Count();
                    RoCPlayer modPlayer = Main.player[playersWhichDealtDamage[j]].GetModPlayer<RoCPlayer>();
                    modPlayer.Experience += (int)Math.Floor(experienceAddedToEachPlayer);

                    if (Main.dedServ)
                    {
                        ModPacket modPacket = ModContent.GetInstance<RuinsOfChaos>().GetPacket();
                        modPacket.Write((byte)RuinsOfChaos.PacketID.SyncXP);
                        modPacket.Write(playersWhichDealtDamage[j]);
                        modPacket.Write7BitEncodedInt(Main.player[playersWhichDealtDamage[j]].GetModPlayer<RoCPlayer>().Experience);
                        modPacket.Send();
                    }
                }
                
            }
            
        }
    }
}
