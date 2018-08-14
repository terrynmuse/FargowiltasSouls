using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class FrostEnchant : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Enchantment");
            Tooltip.SetDefault(
@"'Let's coat the world in a deep freeze' 
Icicles will start to appear around you
When there are three, using any weapon will launch them towards the cursor, Chilling and Frostburning enemies
Allows the ability to walk on water
Summons a baby penguin and snowman");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            player.waterWalk = true;
            //icicles
            modPlayer.FrostEnchant = true;
            modPlayer.FrostEffect(50);
            modPlayer.AddPet("Baby Penguin Pet", hideVisual, BuffID.BabyPenguin, ProjectileID.Penguin);
            modPlayer.AddPet("Baby Snowman Pet", hideVisual, BuffID.BabySnowman, ProjectileID.BabySnowman);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostHelmet);
            recipe.AddIngredient(ItemID.FrostBreastplate);
            recipe.AddIngredient(ItemID.FrostLeggings);
            recipe.AddIngredient(ItemID.IceBow);
            recipe.AddIngredient(ItemID.Fish);
            recipe.AddIngredient(ItemID.ToySled);
            recipe.AddIngredient(ItemID.ColdWatersintheWhiteLand);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}