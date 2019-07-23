using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class EarthForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Earth");

            Tooltip.SetDefault(
@"'Gaia's blessing shines upon you'
25% chance for your projectiles to explode into shards
20% increased weapon use speed
Greatly increases life regeneration after striking an enemy 
One attack gains 10% life steal every 4 seconds, capped at 8 HP
Flower petals will cause extra damage to your target 
Spawns 3 fireballs to rotate around you
Every 8th projectile you shoot will split into 3
Any secondary projectiles may also split
Briefly become invulnerable after striking an enemy");
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
            //mythril
            if (Soulcheck.GetValue("Mythril Speedup"))
                modPlayer.AttackSpeed *= 1.2f;
            //shards
            modPlayer.CobaltEnchant = true;
            //regen on hit, heals
            modPlayer.PalladiumEffect();
            //fireballs and petals
            modPlayer.OrichalcumEffect();
            //split
            modPlayer.AdamantiteEnchant = true;
            //shadow dodge
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

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}