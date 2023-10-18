using Terraria.Audio;
using Terraria.ModLoader;

namespace RuinsOfChaos.Common.Systems
{
    public class RoCSoundEffect : ModSystem
    {
        public static readonly SoundStyle CrossbowFire;
        static RoCSoundEffect()
        {
            CrossbowFire = new SoundStyle("RuinsOfChaos/Assets/Sounds/Items/CrossbowFire");
        }
    }
}
