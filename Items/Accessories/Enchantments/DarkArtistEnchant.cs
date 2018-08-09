using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class DarkArtistEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Artist Enchantment");
            Tooltip.SetDefault(
@"'The shadows hold more than they seem'
Greatly enhances Flameburst effectiveness
Magic weapons occasionally shoot from the shadows of where you used to be
Summons a flickerwick to provide light");
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
            player.setApprenticeT2 = true;
            player.setApprenticeT3 = true;
            //shadow shoot meme
            modPlayer.DarkEnchant = true;
            modPlayer.AddPet("Flickerwick Pet", BuffID.PetDD2Ghost, ProjectileID.DD2PetGhost);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeAltHead);
            recipe.AddIngredient(ItemID.ApprenticeAltShirt);
            recipe.AddIngredient(ItemID.ApprenticeAltPants);
            recipe.AddIngredient(ItemID.ApprenticeScarf);
            recipe.AddIngredient(ItemID.DD2FlameburstTowerT3Popper);
            recipe.AddIngredient(ItemID.ShadowFlameHexDoll);
            recipe.AddIngredient(ItemID.DD2PetGhost);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}