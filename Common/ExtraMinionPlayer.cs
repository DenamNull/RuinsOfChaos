using Terraria;
using Terraria.ModLoader;
using RuinsOfChaos.Content.Modifiers.Summoner;
namespace RuinsOfChaos.Common
{
    public class ExtraMinionPlayer : ModPlayer
    {
        public bool RebelMinion = false;
        public bool RebelSentry = false;
        //NOTE: HAS NOT BEEN TESTED YET. DO NOT EXPECT IT TO WORK.
        public override void PostUpdateBuffs()
        {
            if (Player.HeldItem.CountsAsClass(DamageClass.Summon))
            {
                if (Player.HeldItem.sentry)
                {
                    if (Player.HeldItem.prefix == ModContent.PrefixType<Rebellious>() && Player.maxTurrets > 1 && !RebelSentry)
                    {
                        Player.maxTurrets--;
                        RebelSentry = true;
                    } else if (Player.HeldItem.prefix != ModContent.PrefixType<Rebellious>() && RebelSentry)
                    {
                        Player.maxTurrets++;
                        RebelSentry = false;
                    }
                } else
                {
                    if (Player.HeldItem.prefix == ModContent.PrefixType<Rebellious>() && Player.maxMinions > 1 && !RebelMinion)
                    {
                        Player.maxMinions--;
                        RebelMinion = true;
                    } else if (Player.HeldItem.prefix != ModContent.PrefixType<Rebellious>() && RebelMinion)
                    {
                        Player.maxMinions++;
                        RebelMinion = false;
                    }
                }
            }
            base.PostUpdateBuffs();
        }
    }
}
