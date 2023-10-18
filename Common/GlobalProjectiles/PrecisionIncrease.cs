using Terraria;
using Terraria.ModLoader;
using RuinsOfChaos;
using RuinsOfChaos.Content;
using Terraria.DataStructures;
using System.Linq;

namespace RuinsOfChaos.Common.GlobalProjectiles
{
    public class PrecisionIncrease : GlobalProjectile
    {
        
        public bool hasHitEnemy = false;
        public int precIncr;
        public int precDecr;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.CountsAsClass<AssassinClass>(); //Only works if class is Assassin
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player myPlayer = Main.player[projectile.owner];
            RoCPlayer myModPlayer = myPlayer.GetModPlayer<RoCPlayer>();
            PrecisionIncrease modProj = projectile.GetGlobalProjectile<PrecisionIncrease>();
            if (projectile.CountsAsClass<AssassinClass>() && projectile.owner != 255 && myModPlayer.Precision < myModPlayer.PrecisionCap && !target.friendly && !modProj.hasHitEnemy)
            {
                int diff = myModPlayer.PrecisionCap - myModPlayer.Precision;
                if (diff < modProj.precIncr)
                {
                    myModPlayer.Precision += diff;
                }
                else
                {
                    myModPlayer.Precision += modProj.precIncr;
                }
            }
            modProj.hasHitEnemy = true;
            SyncPrecision(myModPlayer);
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            
            Player myPlayer = Main.player[projectile.owner];
            RoCPlayer myModPlayer = myPlayer.GetModPlayer<RoCPlayer>();
            PrecisionIncrease modProj = projectile.GetGlobalProjectile<PrecisionIncrease>();
            modProj.hasHitEnemy = true;
            if (projectile.CountsAsClass<AssassinClass>() && projectile.owner != 255 && myModPlayer.Precision < myModPlayer.PrecisionCap && target != myPlayer)
            {
                int diff = myModPlayer.PrecisionCap - myModPlayer.Precision;
                if (diff < modProj.precIncr)
                {
                    myModPlayer.Precision += diff;
                }
                else
                {
                    myModPlayer.Precision += modProj.precIncr;
                }
            }
            SyncPrecision(myModPlayer);
            base.OnHitPlayer(projectile, target, info);
        }
        public override void OnKill(Projectile projectile, int timeLeft)
        {
            RoCPlayer myModPlayer = Main.player[projectile.owner].GetModPlayer<RoCPlayer>();
            PrecisionIncrease modProj = projectile.GetGlobalProjectile<PrecisionIncrease>();
            if (projectile.CountsAsClass<AssassinClass>() && projectile.owner != 255 && myModPlayer.Precision != 0 && !modProj.hasHitEnemy)
            {
                if (myModPlayer.Precision < modProj.precDecr)
                {
                    myModPlayer.Precision = 0;
                } else
                {
                    myModPlayer.Precision -= modProj.precDecr;
                }
            }
            SyncPrecision(myModPlayer);
            base.OnKill(projectile, timeLeft);
        }
        public void SyncPrecision(RoCPlayer modPlayer)
        {
            if (Main.dedServ) {
                ModPacket modPacket = ModContent.GetInstance<RuinsOfChaos>().GetPacket();
                modPacket.Write((byte)RuinsOfChaos.PacketID.SyncPrecision);
                modPacket.Write((byte)modPlayer.Player.whoAmI);
                modPacket.Write((byte)modPlayer.Precision);
                modPacket.Send();
            }
        }
        public override bool InstancePerEntity => true;

    }
}
