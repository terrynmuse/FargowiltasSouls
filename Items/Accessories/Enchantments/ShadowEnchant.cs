using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ShadowEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Enchantment");
            Tooltip.SetDefault(
@"'You feel your body slip into the deepest of shadows'
Your attacks may inflict Darkness on enemies
Darkened enemies occasionally fire shadowflame tentacles at other enemies
Summons a pet Eater of Souls and Shadow Orb");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).ShadowEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ShadowHelmet);
            recipe.AddIngredient(ItemID.ShadowScalemail);
            recipe.AddIngredient(ItemID.ShadowGreaves);
            recipe.AddIngredient(ItemID.WarAxeoftheNight);
            recipe.AddIngredient(ItemID.PurpleClubberfish);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.BallOHurt);
                recipe.AddIngredient(thorium.ItemType("CorruptButterfly"));
            }

            
            recipe.AddIngredient(ItemID.EatersBone);
            recipe.AddIngredient(ItemID.ShadowOrb);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
