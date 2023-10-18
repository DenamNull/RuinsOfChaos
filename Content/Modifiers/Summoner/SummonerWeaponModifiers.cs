using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.Localization;
using System;

namespace RuinsOfChaos.Content.Modifiers.Summoner
{
    public abstract class SummonerWeaponModifiers : ModPrefix
    {
        public const string SummonerModifierPath = "Mods.RuinsOfChaos.Prefixes.Summoner";
        public virtual float PercentManaUsed => 1f;
        public virtual float DamagePercentage => 1f;
        /// <summary>
        /// If true, summons 1 extra minion. If false, summons 1 less minion. Null does nothing.
        /// </summary>
        public virtual bool? GrantsExtraSummon => null;
        public virtual int DefensePenetration => 0;
        public virtual bool CanDespawn => false;
        public static LocalizedText ArmorPenetrationText = Language.GetText($"{SummonerModifierPath}.DefensePenetration");
        public static LocalizedText MinionsCanDespawnText = Language.GetText($"{SummonerModifierPath}.MinionsCanDespawn");
        public static LocalizedText SentriesCanDespawnText = Language.GetText($"{SummonerModifierPath}.SentriesCanDespawn");
        public static LocalizedText ExtraMinionText = Language.GetText($"{SummonerModifierPath}.ExtraMinion");
        public static LocalizedText ExtraSentryText = Language.GetText($"{SummonerModifierPath}.ExtraSentry");
        public static LocalizedText LessMinionsText = Language.GetText($"{SummonerModifierPath}.LessMinions");
        public static LocalizedText LessSentriesText = Language.GetText($"{SummonerModifierPath}.LessSentries");
        public override PrefixCategory Category => PrefixCategory.Custom;
        public override bool CanRoll(Item item)
        {
            return base.CanRoll(item) && item.CountsAsClass(DamageClass.Summon);
        }
        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult *= DamagePercentage;
            manaMult *= PercentManaUsed;
        }
        public override void ModifyValue(ref float valueMult)
        {
            if (DamagePercentage >= 0f)
            {
                valueMult *= (DamagePercentage + (10f * (float)Math.Round(Math.Log(DamagePercentage), 2)));
            } else
            {
                valueMult *= (DamagePercentage / 1.15f);
            }
        }
        public override void Apply(Item item)
        {
            item.ArmorPenetration += DefensePenetration;
            
        }
        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            List<TooltipLine> ReforgeTooltips = new List<TooltipLine>();
            if (DefensePenetration != 0)
            {
                TooltipLine defPenTooltip = new TooltipLine(Mod, "PrefixSummonerDefPen", ArmorPenetrationText.Format(DefensePenetration));
                defPenTooltip.IsModifier = true;
                ReforgeTooltips.Add(defPenTooltip);
            }
            if (CanDespawn)
            {
                if (item.sentry)
                {
                    TooltipLine sentryDespawnTooltip = new TooltipLine(Mod, "PrefixSummonerDespawn", SentriesCanDespawnText.Value);
                    sentryDespawnTooltip.IsModifier = true;
                    sentryDespawnTooltip.IsModifierBad = true;
                    ReforgeTooltips.Add(sentryDespawnTooltip);
                } else
                {
                    TooltipLine minionDespawnTooltip = new TooltipLine(Mod, "PrefixSummonerDespawn", MinionsCanDespawnText.Value);
                    minionDespawnTooltip.IsModifier = true;
                    minionDespawnTooltip.IsModifierBad = true;
                    ReforgeTooltips.Add(minionDespawnTooltip);
                }
            }
            if (GrantsExtraSummon == true)
            {
                if (item.sentry)
                {
                    TooltipLine extraSentryTooltip = new TooltipLine(Mod, "PrefixSummonerExtraSummon", ExtraSentryText.Value);
                    extraSentryTooltip.IsModifier = true;
                    ReforgeTooltips.Add(extraSentryTooltip);
                } else
                {
                    TooltipLine extraMinionTooltip = new TooltipLine(Mod, "PrefixSummonerExtraSummon", ExtraMinionText.Value);
                    extraMinionTooltip.IsModifier = true;
                    ReforgeTooltips.Add(extraMinionTooltip);
                }
            } else if (GrantsExtraSummon == false)
            {
                if (item.sentry)
                {
                    TooltipLine lessSentriesTooltip = new TooltipLine(Mod, "PrefixSummonerLessSummons", LessSentriesText.Value);
                    lessSentriesTooltip.IsModifier = true;
                    lessSentriesTooltip.IsModifierBad = true;
                    ReforgeTooltips.Add(lessSentriesTooltip);
                } else
                {
                    TooltipLine lessMinionsTooltip = new TooltipLine(Mod, "PrefixSummonerLessSummons", LessMinionsText.Value);
                    lessMinionsTooltip.IsModifier = true;
                    lessMinionsTooltip.IsModifierBad = true;
                    ReforgeTooltips.Add(lessMinionsTooltip);
                }
            }
            return ReforgeTooltips;
        }
    }
}
