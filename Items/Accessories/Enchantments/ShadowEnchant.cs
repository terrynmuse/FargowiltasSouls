using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ShadowEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Enchantment");
            Tooltip.SetDefault(
@"'You feel your body slip into the deepest shadows'
Attacks have a chance to inflict cursed flame
Summons a Baby Eater of Souls and a Shadow Orb");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.ShadowEnchant = true;

            modPlayer.AddPet("Baby Eater Pet", BuffID.BabyEater, ProjectileID.BabyEater);
            modPlayer.AddPet("Shadow Orb Pet", BuffID.ShadowOrb, ProjectileID.ShadowOrb);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ShadowHelmet);
            recipe.AddIngredient(ItemID.ShadowScalemail);
            recipe.AddIngredient(ItemID.ShadowGreaves);
            recipe.AddIngredient(ItemID.LightlessChasms);
            recipe.AddIngredient(ItemID.EatersBone);
            recipe.AddIngredient(ItemID.ShadowOrb);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}