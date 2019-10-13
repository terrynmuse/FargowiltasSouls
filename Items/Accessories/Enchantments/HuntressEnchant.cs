using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class HuntressEnchant : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Enchantment");
            Tooltip.SetDefault(
@"'Who needs to aim?'
Every 8th projectile you shoot will split into 3
Any secondary projectiles may also split");
            DisplayName.AddTranslation(GameCulture.Chinese, "精金魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'谁需要瞄准?'
第8个抛射物将会分裂成3个
分裂出的抛射物同样可以分裂");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().AdamantiteEnchant = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyAdamHead");
            recipe.AddIngredient(ItemID.AdamantiteBreastplate);
            recipe.AddIngredient(ItemID.AdamantiteLeggings);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(ItemID.AdamantiteGlaive);
                recipe.AddIngredient(thorium.ItemType("AdamantiteStaff"));
                recipe.AddIngredient(thorium.ItemType("DynastyWarFan"));
                recipe.AddIngredient(thorium.ItemType("Scorn"));
                recipe.AddIngredient(thorium.ItemType("OgreSnotGun"));
            }
            else
            {
                recipe.AddIngredient(ItemID.DarkLance);
                recipe.AddIngredient(ItemID.AdamantiteGlaive);
            }

            recipe.AddIngredient(ItemID.Shotgun);
            recipe.AddIngredient(ItemID.VenomStaff);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
