using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class CosmoForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Cosmos");
            Tooltip.SetDefault(
                @"'Been around since the Big Bang'
A meteor shower initiates every few seconds while using any weapon
Solar shield allows you to dash through enemies
Attacks inflict the Solar Flare debuff
Double tap down to toggle stealth, reducing chance for enemies to target you but slowing movement
You also spawn a vortex to draw in and massively damage enemies when you enter stealth
Attacks rarely spawn a vortex to draw in and massively damage enemies
Hurting enemies has a chance to spawn buff boosters
Maintain maxed buff boosters for 5 seconds to gain obscene attack speed
Double tap down to direct your guardian
Press the Freeze Key to freeze time for 5 seconds
Summons a Companion Cube Pet and a suspicious looking eye to provide light");
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
            modPlayer.CosmoForce = true;
            modPlayer.MeteorEffect(75);
            modPlayer.SolarEffect();
            modPlayer.VortexEffect(hideVisual);
            modPlayer.NebulaEffect();
            modPlayer.StardustEffect();
            modPlayer.AddPet("Suspicious Eye Pet", hideVisual, BuffID.SuspiciousTentacle, ProjectileID.SuspiciousTentacle);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MeteorEnchant");
            recipe.AddIngredient(null, "SolarEnchant");
            recipe.AddIngredient(null, "VortexEnchant");
            recipe.AddIngredient(null, "NebulaEnchant");
            recipe.AddIngredient(null, "StardustEnchant");

            //bow of light
            recipe.AddIngredient(ItemID.SuspiciousLookingTentacle);

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}