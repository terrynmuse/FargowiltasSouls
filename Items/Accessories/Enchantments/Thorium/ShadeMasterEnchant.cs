using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class ShadeMasterEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shade Master Enchantment");
            Tooltip.SetDefault(
@"'Live in the shadows, and strike with precision'
Striking an enemy with any throwing weapon will trigger 'Shadow Dance'
Additonally, while Shadow Dance is active you deal 15% more throwing damage
Throw a smoke bomb to teleport to it
Standing nearby smoke gives you the First Strike buff
Summons a pet Black Cat");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            //set bonus
            thoriumPlayer.shadeSet = true;
            if (thoriumPlayer.shadeTele)
            {
                player.thrownDamage += 0.15f;
            }
            //ninja, smoke bombs, pet
            modPlayer.NinjaEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(thorium.ItemType("ShadeMasterMask"));
            recipe.AddIngredient(thorium.ItemType("ShadeMasterGarb"));
            recipe.AddIngredient(thorium.ItemType("ShadeMasterTreads"));
            recipe.AddIngredient(null, "NinjaEnchant");
            recipe.AddIngredient(thorium.ItemType(""));
            recipe.AddIngredient(thorium.ItemType("ClockWorkBomb"), 300);
            recipe.AddIngredient(thorium.ItemType("BugenkaiShuriken"), 300);
            recipe.AddIngredient(thorium.ItemType("ShadeKunai"), 300);
            recipe.AddIngredient(thorium.ItemType("ShadeDagger"));
            recipe.AddIngredient(thorium.ItemType("Soulslasher"), 300);
            recipe.AddIngredient(thorium.ItemType("LihzahrdKukri"), 300);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
