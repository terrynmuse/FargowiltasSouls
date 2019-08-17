using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class EskimoEnchant : ModItem
    {
    public override bool Autoload(ref string name)
        {
            return false;
        }
        
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eskimo Enchantment");
            Tooltip.SetDefault(
@"''
goes into frost enchant
You can walk on water and when you do, it freezes and creates spikes
");
            DisplayName.AddTranslation(GameCulture.Chinese, "爱斯基摩魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"''
变为霜冻魔石
可以水上行走,如此做时,水会结冰并产生尖刺
");
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
            /*
             * if(player.walkingOnWater)
{
	Create Ice Rod Projectile right below you
}

NearbyEffects:

if(modPlayer.EskimoEnchant && tile.type == IceRodBlock)
{
	Create spikes
}
             */
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.PinkEskimoHood);
            recipe.AddIngredient(ItemID.PinkEskimoCoat);
            recipe.AddIngredient(ItemID.PinkEskimoPants);
            //recipe.AddIngredient(ItemID.IceRod);
            recipe.AddIngredient(ItemID.FrostMinnow);
            recipe.AddIngredient(ItemID.AtlanticCod);
            recipe.AddIngredient(ItemID.MarshmallowonaStick);
            

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
