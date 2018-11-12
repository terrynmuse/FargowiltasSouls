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
Every 8th projectile you shoot will split into 3
Any secondary projectiles may also split
25% chance for your projectiles to explode into shards
30% increased weapon use speed
Flower petals will cause extra damage to your target 
Spawns 3 fireballs to rotate around you
Greatly increases life regeneration after striking an enemy 
One attack gains 33% life steal every 10 seconds, capped at 100 HP
Any damage you take while at full HP is reduced by 90%
Briefly become invulnerable after striking an enemy");
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
            //split
            modPlayer.AdamantiteEnchant = true;
            //shards
            modPlayer.CobaltEnchant = true;
            //mythril
            modPlayer.AttackSpeed *= 1.3f;
            //fireballs and petals
            modPlayer.OrichalcumEffect();
            //regen on hit, heals
            modPlayer.PalladiumEffect();
            //shadow dodge, full hp resistance
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