using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class WoodForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Wood");

            Tooltip.SetDefault(
@"'Extremely rigid'
Critters have massively increased defense
When critters die, they release their souls to aid you
Every 5th attack will be accompanied by several snowballs
All grappling hooks pull you in and retract twice as fast
Any hook will periodically fire homing shots at enemies
You have an aura of Shadowflame
When you take damage, you are inflicted with Super Bleeding
Double tap down to spawn a palm tree sentry that throws nuts at enemies
You leave behind a trail of rainbows that may shrink enemies");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //wood
            modPlayer.WoodEnchant = true;
            //boreal
            modPlayer.BorealEnchant = true;
            //mahogany
            modPlayer.MahoganyEnchant = true;
            //ebon
            modPlayer.EbonEffect();
            //shade
            modPlayer.ShadeEnchant = true;
            //palm
            modPlayer.PalmEffect();
            //pearl
            modPlayer.PearlEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "WoodEnchant");
            recipe.AddIngredient(null, "BorealWoodEnchant");
            recipe.AddIngredient(null, "RichMahoganyEnchant");
            recipe.AddIngredient(null, "EbonwoodEnchant");
            recipe.AddIngredient(null, "ShadewoodEnchant");
            recipe.AddIngredient(null, "PalmWoodEnchant");
            recipe.AddIngredient(null, "PearlwoodEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}