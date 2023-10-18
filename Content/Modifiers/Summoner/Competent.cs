using Terraria;
using Terraria.Localization;

namespace RuinsOfChaos.Content.Modifiers.Summoner
{
    public class Competent : SummonerWeaponModifiers
    {
        public override float DamagePercentage => 1.05f;
        public override float PercentManaUsed => 0.85f;
        public override int DefensePenetration => 2;
        public override LocalizedText DisplayName => Language.GetText($"{SummonerModifierPath}.Competent");
    }
}
