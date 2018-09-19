using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class SpiritForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Spirit");
            Tooltip.SetDefault(
                @"'The strength of your spirit amazes even the Mutant'
If you reach zero HP you cheat death, returning with 20 HP
For a few seconds after reviving, you are immune to all damage and spawn bones everywhere
Double tap down to call an ancient storm to the cursor location
Any projectiles shot through your storm gain double pierce and 50% damage
You are immune to the Mighty Wind debuff
You gain a shield that can reflect projectiles
Summons an Enchanted Sword familiar that scales with minion damage
Attacks will inflict a random debuff
All damage can spawn damaging orbs and healing orbs
Summons a pet Baby Dino, Magical Fairy, Tiki Spirit, and Wisp");
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
            modPlayer.SpiritForce = true;
            modPlayer.FossilEffect(20, hideVisual);
            modPlayer.ForbiddenEffect();
            modPlayer.HallowEffect(hideVisual, 80);
            modPlayer.TikiEnchant = true;
            modPlayer.SpectreEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FossilEnchant");
            recipe.AddIngredient(null, "ForbiddenEnchant");
            recipe.AddIngredient(null, "HallowEnchant");
            recipe.AddIngredient(null, "TikiEnchant");
            recipe.AddIngredient(null, "SpectreEnchant");

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}