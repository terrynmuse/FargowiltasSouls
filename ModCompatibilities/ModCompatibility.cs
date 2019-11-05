using System;
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


        public void TryAddRecipes()
        {
            try
            {
                AddRecipes();
            }
            catch (Exception e)
            {
                CallerMod.Logger.Error($"Error while adding recipes from `{ModInstance.Name}` for mod `{CallerMod.Name}`.", e);
            }
        }

        protected virtual void AddRecipes() { }


        public void TryAddRecipeGroups()
        {
            try
            {
                AddRecipeGroups();
            }
            catch (Exception e)
            {
                CallerMod.Logger.Error($"Error while adding recipe groups from `{ModInstance.Name}` for mod `{CallerMod.Name}`.", e);
            }
        }

        protected virtual void AddRecipeGroups() { }


        public Mod CallerMod { get; }

        public string ModName { get; }
        public Mod ModInstance { get; private set; }
    }
}