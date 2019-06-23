using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class SpookyEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spooky Enchantment");
            Tooltip.SetDefault(
@"'Melting souls since 1902'
All of your minions may occasionally spew massive scythes everywhere
Summons a pet Cursed Sapling and Eyeball Spring");
            DisplayName.AddTranslation(GameCulture.Chinese, "阴森魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"自1902年以来融化的灵魂
召唤物偶尔会发射巨大镰刀
召唤一只万圣小树妖和弹簧眼球");
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
            player.GetModPlayer<FargoPlayer>(mod).SpookyEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpookyHelmet);
            recipe.AddIngredient(ItemID.SpookyBreastplate);
            recipe.AddIngredient(ItemID.SpookyLeggings);
            recipe.AddIngredient(ItemID.DeathSickle);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("BeholderStaff"));
                recipe.AddIngredient(thorium.ItemType("CryptWand"));
                recipe.AddIngredient(ItemID.ButchersChainsaw);
                recipe.AddIngredient(thorium.ItemType("PaganGrasp"));
            }
            else
            {
                recipe.AddIngredient(ItemID.ButchersChainsaw);
            }
            
            recipe.AddIngredient(ItemID.CursedSapling);
            recipe.AddIngredient(ItemID.EyeSpring);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
