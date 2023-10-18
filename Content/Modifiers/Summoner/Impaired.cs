using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RuinsOfChaos.Content.Modifiers.Summoner
{
    public class Impaired : SummonerWeaponModifiers
    {
        public override float PercentManaUsed => 1.35f;
        public override float DamagePercentage => 0.85f;
        public override LocalizedText DisplayName => Language.GetText($"{SummonerModifierPath}.Impaired");
    }
}
