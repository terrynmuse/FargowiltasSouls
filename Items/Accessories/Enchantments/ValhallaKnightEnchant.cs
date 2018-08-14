using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ValhallaKnightEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Valhalla Knight Enchantment");
            Tooltip.SetDefault(
@"'Valhalla calls'
Greatly enhances Ballista effectiveness
You ignore enemy knockback immunity with your close range melee weapons
No enemy is ever safe from you
Shiny Stone effects
Summons a pet Dragon");
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
            player.setSquireT2 = true;
            player.setSquireT3 = true;
            modPlayer.ValhallaEnchant = true;
            player.shinyStone = true;
            modPlayer.AddPet("Dragon Pet", hideVisual, BuffID.PetDD2Dragon, ProjectileID.DD2PetDragon);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SquireAltHead);
            recipe.AddIngredient(ItemID.SquireAltShirt);
            recipe.AddIngredient(ItemID.SquireAltPants);
            recipe.AddIngredient(ItemID.SquireShield);
            recipe.AddIngredient(ItemID.ShinyStone);
            recipe.AddIngredient(ItemID.DD2BallistraTowerT3Popper);
            recipe.AddIngredient(ItemID.DD2PetDragon);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}