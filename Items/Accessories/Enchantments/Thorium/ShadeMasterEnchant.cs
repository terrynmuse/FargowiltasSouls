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
        private readonly Mod fargos = ModLoader.GetMod("Fargowiltas");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shade Master Enchantment");
            Tooltip.SetDefault(
@"'Live in the shadows, and strike with precision'
50% of the damage you take is staggered over the next 10 seconds
Throw a smoke bomb to teleport to it and gain the First Strike Buff
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
            recipe.AddIngredient(thorium.ItemType("ClockWorkBomb"), 300);
            recipe.AddIngredient(fargos != null ? fargos.ItemType("KrakenThrown") : ItemID.Kraken);
            recipe.AddIngredient(thorium.ItemType("BugenkaiShuriken"), 300);
            recipe.AddIngredient(thorium.ItemType("ShadeKunai"), 300);
            recipe.AddIngredient(thorium.ItemType("Soulslasher"), 300);
            recipe.AddIngredient(thorium.ItemType("LihzahrdKukri"), 300);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
