using Terraria.ModLoader;

namespace FargowiltasSouls.ModCompatibilities
{
    public abstract class ModCompatibility
    {
        protected ModCompatibility(Mod callerMod, string modName)
        {
            CallerMod = callerMod;

            ModName = modName;
        }


        public virtual ModCompatibility TryLoad() => (ModInstance = ModLoader.GetMod(ModName)) == null ? null : this;

        
        public virtual void AddRecipes() { }

        public virtual void AddRecipeGroups() { }


        public Mod CallerMod { get; }

        public string ModName { get; }
        public Mod ModInstance { get; private set; }
    }
}