using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class TitaniumEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Enchantment");
            Tooltip.SetDefault(
@"'Hit me with your best shot' 
Any damage you take while at full HP is reduced by 90%
Briefly become invulnerable after striking an enemy when below 50% HP
Increases all knockback");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.kbBuff = true;

            if(player.statLife == player.statLifeMax2)
            {
                player.endurance = .9f;
            }
            else if(player.statLife < player.statLifeMax2 / 2)
            {
                player.onHitDodge = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyTitaHead");
            recipe.AddIngredient(ItemID.TitaniumBreastplate);
            recipe.AddIngredient(ItemID.TitaniumLeggings);
            recipe.AddIngredient(ItemID.TitaniumSword);
            recipe.AddIngredient(ItemID.SlapHand);
            recipe.AddIngredient(ItemID.Anchor);
            recipe.AddIngredient(ItemID.MonkStaffT1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}