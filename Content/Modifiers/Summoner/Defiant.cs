using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RuinsOfChaos.Content.Modifiers.Summoner
{
    public class Defiant : SummonerWeaponModifiers
    {
        public override float PercentManaUsed => 1.5f;
        public override float DamagePercentage => 0.8f;
        public override bool CanDespawn => true;
        public override LocalizedText DisplayName => Language.GetText($"{SummonerModifierPath}.Defiant");
        public override bool CanRoll(Item item)
        {
            return base.CanRoll(item) && item != Main.reforgeItem;
        }
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 0.4f;
        }

    }
}
