using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class JungleEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Enchantment");
            Tooltip.SetDefault(
@"'The wrath of the jungle dwells within'
Chance to steal 5 mana with each attack
Taking damage will release a poisoning spore explosion
Spore damage scales with magic damage");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Soulcheck.GetValue("Spore Explosion"))
            {
                player.GetModPlayer<FargoPlayer>(mod).JungleEnchant = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.JungleHat);
            recipe.AddIngredient(ItemID.JungleShirt);
            recipe.AddIngredient(ItemID.JunglePants);
            recipe.AddIngredient(ItemID.JungleRose);
            recipe.AddIngredient(ItemID.DoNotStepontheGrass);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}