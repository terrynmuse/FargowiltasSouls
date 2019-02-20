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

            string tooltip = "'Gaia's blessing shines upon you'\n";

                tooltip +=
@"25% chance for your projectiles to explode into shards
Greatly increases life regeneration after striking an enemy 
One attack gains 5% life steal every second, capped at 5 HP
Flower petals will cause extra damage to your target 
Spawns 3 fireballs to rotate around you
Every 8th projectile you shoot will split into 3
Any secondary projectiles may also split";

            if (thorium == null)
            {
                tooltip += @"
Any damage you take while at full HP is reduced by 90%
Briefly become invulnerable after striking an enemy";
            }
            
            Tooltip.SetDefault(tooltip);
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
            //shards
            modPlayer.CobaltEnchant = true;
            //regen on hit, heals
            modPlayer.PalladiumEffect();
            //fireballs and petals
            modPlayer.OrichalcumEffect();
            //split
            modPlayer.AdamantiteEnchant = true;

            //shadow dodge, full hp resistance
            if (!Fargowiltas.Instance.ThoriumLoaded)
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

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "TitaniumEnchant");
            }

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}