using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class SpiritForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Spirit");

            string tooltip =
@"'Ascend from this mortal realm'
If you reach zero HP you cheat death, returning with 100 HP and spawning bones
Double tap down to call an ancient storm to the cursor location
You gain a shield that can reflect projectiles
Attacks will inflict Infested
Damage has a chance to spawn damaging orbs
If you crit, you might also get a healing orb
";

            if (thorium != null)
            {
                tooltip +=
@"Killing enemies or continually damaging bosses generates soul wisps
After generating 5 wisps, they are instantly consumed to heal you for 10 life
";
            }

            tooltip += 
@"Summons an Enchanted Sword familiar
Summons several pets";

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
            //spectre works for all, spirit trapper works for all
            modPlayer.SpiritForce = true;
            //revive, bone zone, pet
            modPlayer.FossilEffect(25, hideVisual);
            //storm
            modPlayer.ForbiddenEffect();
            //sword, shield, pet
            modPlayer.HallowEffect(hideVisual, 100);
            //infested debuff, pet
            modPlayer.TikiEffect(hideVisual);
            //pet
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

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}