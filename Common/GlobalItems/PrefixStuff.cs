using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
using System.Collections.Generic;
using RuinsOfChaos.Content.Modifiers.Summoner;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace RuinsOfChaos.Common.GlobalItems
{
    public class PrefixStuff : GlobalItem
    {
        public enum SummonerPrefixes
        {
            Defiant,
            Rebellious,
            Impaired,
            Discouraging,
            Competent,
            Size
        }
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.CountsAsClass(DamageClass.Summon);
        }
        public override int ChoosePrefix(Item item, UnifiedRandom rand)
        {
            int randomPrefix = rand.Next((int)SummonerPrefixes.Size + PrefixLoader.GetPrefixesInCategory(PrefixCategory.Magic).Count + PrefixLoader.GetPrefixesInCategory(PrefixCategory.AnyWeapon).Count - 1);
            switch ((SummonerPrefixes)randomPrefix)
            {
                case SummonerPrefixes.Defiant:
                    return ModContent.PrefixType<Defiant>();
                case SummonerPrefixes.Rebellious:
                    return ModContent.PrefixType<Rebellious>();
                case SummonerPrefixes.Impaired:
                    return ModContent.PrefixType<Impaired>();
                case SummonerPrefixes.Discouraging:
                    return ModContent.PrefixType<Discouraging>();
                case SummonerPrefixes.Competent:
                    return ModContent.PrefixType<Competent>();
                default:
                    return base.ChoosePrefix(item, rand);
            }
        }
        
    }
}
