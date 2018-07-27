using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class HallowEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Enchantment");
            Tooltip.SetDefault(
@"'Hallowed be your sword and shield'
You gain a shield that can reflect projectiles
Summons an Enchanted Sword familiar");
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
            EffectAdd(player, hideVisual, mod);
        }

        public static void EffectAdd(Player player, bool hideVisual, Mod mod)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.HallowEnchant = true;
            modPlayer.AddMinion("Enchanted Sword Familiar", mod.ProjectileType("HallowSword"), 80, 0f);
            modPlayer.AddMinion("Hallowed Shield", mod.ProjectileType("HallowShield"), 0, 0f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyHallowHead");
            recipe.AddIngredient(ItemID.HallowedPlateMail);
            recipe.AddIngredient(ItemID.HallowedGreaves);
            recipe.AddIngredient(ItemID.Excalibur);
            recipe.AddIngredient(null, "SilverEnchant");
            recipe.AddIngredient(ItemID.TheLandofDeceivingLooks);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}