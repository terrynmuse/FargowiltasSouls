using SacredTools.Items.Armor.Asthraltite;
using SacredTools.Items.Armor.Dragon;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.ModCompatibilities
{
    public class SoACompatibility : ModCompatibility
    {
        public SoACompatibility(Mod callerMod) : base(callerMod, nameof(SacredTools))
        {
        }


        protected override void AddRecipeGroups()
        {
            // Flarium
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyFlariumHelmet", 
                new RecipeGroup(() => Lang.misc[37] + " Flarium Helmet", 
                    ModContent.ItemType<FlariumCowl>(), ModContent.ItemType<FlariumHelmet>(), ModContent.ItemType<FlariumHood>(), ModContent.ItemType<FlariumCrown>(), ModContent.ItemType<FlariumMask>()));

            // Asthraltite
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAstralHelmet",
                new RecipeGroup(() => Lang.misc[37] + " Asthraltite Helmet",
                    ModContent.ItemType<AsthralMelee>(), ModContent.ItemType<AsthralRanged>(), ModContent.ItemType<AsthralMage>(), ModContent.ItemType<AsthralSummon>(), ModContent.ItemType<AsthralThrown>()));
        }
    }
}