using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class EarthForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Earth");
            Tooltip.SetDefault(
                @"'Gaia's blessing shines upon you'
25% chance for your projectiles to explode into shards
Greatly increases life regeneration after striking an enemy 
Small chance for an attack to gain 33% life steal
30% increased weapon use speed
Flower petals will cause extra damage to your target 
Attacks may spawn fireballs to rotate around you
Every 8th projectile you shoot will split into 3
Any secondary projectiles may also split
Any damage you take while at full HP is reduced by 90%
Briefly become invulnerable after striking an enemy when below 50% HP
Increases all knockback");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.EarthForce = true;
            modPlayer.CobaltEnchant = true;
            modPlayer.PalladiumEffect();
            modPlayer.MythrilEnchant = true;
            modPlayer.OrichalcumEffect();
            modPlayer.AdamantiteEnchant = true;
            modPlayer.TitaniumEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CobaltEnchant");
            recipe.AddIngredient(null, "PalladiumEnchant");
            recipe.AddIngredient(null, "MythrilEnchant");
            recipe.AddIngredient(null, "OrichalcumEnchant");
            recipe.AddIngredient(null, "AdamantiteEnchant");
            recipe.AddIngredient(null, "TitaniumEnchant");

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}