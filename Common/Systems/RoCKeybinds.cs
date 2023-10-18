using Terraria.ModLoader;

namespace RuinsOfChaos.Common.Systems
{
    public class RoCKeybinds : ModSystem
    {
        public static ModKeybind BookOfKnowledgeKeybind { get; private set; }
        public override void Load()
        {
            BookOfKnowledgeKeybind = KeybindLoader.RegisterKeybind(Mod, "BookOfKnowledge", Microsoft.Xna.Framework.Input.Keys.B);
        }
        public override void Unload()
        {
            BookOfKnowledgeKeybind = null;
        }
    }
}
