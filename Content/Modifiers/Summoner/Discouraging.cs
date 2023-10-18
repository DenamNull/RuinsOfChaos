using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RuinsOfChaos.Content.Modifiers.Summoner
{
    public class Discouraging : SummonerWeaponModifiers
    {
        public override float PercentManaUsed => 1.1f;
        public override float DamagePercentage => 0.9f;
        public override LocalizedText DisplayName => Language.GetText($"{SummonerModifierPath}.Discouraging");
    }
}
