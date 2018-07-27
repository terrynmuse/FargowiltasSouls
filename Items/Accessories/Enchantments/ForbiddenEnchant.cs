using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ForbiddenEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forbidden Enchantment");
            Tooltip.SetDefault(
@"'Walk like an Egyptian'
Double tap down to call an ancient storm to the cursor location
Any projectiles shot through your storm gain double pierce and 50% damage
You are immune to the Mighty Wind debuff");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 80000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.ForbiddenEnchant = true;
            EffectAdd(player, hideVisual, mod);
        }

        public static void EffectAdd(Player player, bool hideVisual, Mod mod)
        {
            player.buffImmune[BuffID.WindPushed] = true; 

            if (Soulcheck.GetValue("Forbidden Storm"))
            {
                player.setForbidden = true;
                player.UpdateForbiddenSetLock();
                Lighting.AddLight(player.Center, 0.8f, 0.7f, 0.2f);
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AncientBattleArmorHat);
            recipe.AddIngredient(ItemID.AncientBattleArmorShirt);
            recipe.AddIngredient(ItemID.AncientBattleArmorPants);
            recipe.AddIngredient(ItemID.BookStaff);
            recipe.AddIngredient(ItemID.SpiritFlame);
            recipe.AddIngredient(ItemID.SecretoftheSands);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}







