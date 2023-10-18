using Terraria.Localization;

namespace RuinsOfChaos.Content.Modifiers.Summoner
{
   public class Rebellious : SummonerWeaponModifiers
    {
        public override float DamagePercentage => 0.95f;
        public override LocalizedText DisplayName => Language.GetText($"{SummonerModifierPath}.Rebellious");
        public override bool? GrantsExtraSummon => false;
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 0.45f;
        }
    }
}
