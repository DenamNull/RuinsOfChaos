using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using RuinsOfChaos.Common.UI.idk;

namespace RuinsOfChaos.Common.Commands
{
    public class Suicide : ModCommand
    {
        public static LocalizedText DeathMessage = Language.GetText("Mods.RuinsOfChaos.DeathMessages.FromSuicideCommand");
        public static LocalizedText DescriptionText = Language.GetText("Mods.RuinsOfChaos.Commands.Suicide.HelpText");
        public override CommandType Type => CommandType.Chat;
        public override string Command => "suicide";
        public override void Action(CommandCaller caller, string input, string[] args)
        {  
            SoundEngine.PlaySound(SoundID.Thunder);
            caller.Player.Hurt(PlayerDeathReason.ByCustomReason(DeathMessage.Format(caller.Player.name)), 1000000, 0, false);
        }
        public override string Description => DescriptionText.Value;
    }
}
