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
Attacks rarely spawn a vortex to draw in and massively damage enemies
Hurting enemies has a chance to spawn buff boosters
Once you get to the last tier with each booster type, your attack speed hits obscene levels
Double tap down to direct your guardian
When you do, you freeze time temporarily
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
            //meteor shower
            modPlayer.MeteorEffect(75, 2);
            modPlayer.SolarShield();
            //solar flare debuff
            modPlayer.SolarEnchant = true;
            //portal spawn
            modPlayer.VortexEnchant = true;
            //stealth memes
            modPlayer.VortexEffect();
            //boosters and meme speed
            modPlayer.NebulaEffect();
            //minion and freeze time
            modPlayer.StardustEnchant = true;
            modPlayer.StardustEffect();
            modPlayer.AddPet("Companion Cube Pet", hideVisual, BuffID.CompanionCube, ProjectileID.CompanionCube);
            modPlayer.AddPet("Suspicious Looking Eye Pet", hideVisual, BuffID.SuspiciousTentacle, ProjectileID.SuspiciousTentacle);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MeteorEnchant");
            recipe.AddIngredient(null, "SolarEnchant");
            recipe.AddIngredient(null, "VortexEnchant");
            recipe.AddIngredient(null, "NebulaEnchant");
            recipe.AddIngredient(null, "StardustEnchant");
            recipe.AddIngredient(ItemID.SuspiciousLookingTentacle);

            if(Fargowiltas.Instance.FargosLoaded)
            {
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            }
            else
            {
                recipe.AddTile(TileID.LunarCraftingStation);
            }

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}