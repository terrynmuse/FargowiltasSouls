using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ShroomiteEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Enchantment");
            Tooltip.SetDefault(
@"'Made with real shrooms!'
Not moving puts you in stealth
While in stealth crits deal 4x damage and spores spawn on enemies
Summons a pet Baby Truffle");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.ShroomiteEffect();
            modPlayer.AddPet("Truffle Pet", hideVisual, BuffID.BabyTruffle, ProjectileID.Truffle);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyShroomHead");
            recipe.AddIngredient(ItemID.ShroomiteBreastplate);
            recipe.AddIngredient(ItemID.ShroomiteLeggings);
            recipe.AddIngredient(ItemID.MushroomSpear);
            recipe.AddIngredient(ItemID.Hammush);
            recipe.AddIngredient(ItemID.Uzi);
            recipe.AddIngredient(ItemID.StrangeGlowingMushroom);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}