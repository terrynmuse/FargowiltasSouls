using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class NinjaEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ninja Enchantment");
            Tooltip.SetDefault(
@"'They are already dead'
Throw a smoke bomb to teleport to it
Standing nearby smoke gives you the First Strike buff
Effects of the Master Ninja Gear
Summons a pet Black cat");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.NinjaEnchant = true;
            modPlayer.AddPet("Black Cat Pet", BuffID.BlackCat, ProjectileID.BlackCat);

            //ninja gear
            player.blackBelt = true;
            player.spikedBoots = 2;
            player.dash = 1;  
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NinjaHood);
            recipe.AddIngredient(ItemID.NinjaShirt);
            recipe.AddIngredient(ItemID.NinjaPants);
            recipe.AddIngredient(ItemID.MasterNinjaGear);
            recipe.AddIngredient(ItemID.SmokeBomb, 50);
            recipe.AddIngredient(ItemID.UnluckyYarn);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}