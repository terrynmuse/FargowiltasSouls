using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class BulbEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bulb Enchantment");
            Tooltip.SetDefault(
@"'Has a surprisingly sweet aroma'
Your magic damage has a chance to poison hit enemies with a spore cloud
Effects of Night Shade Petal and Petal Shield");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 60000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //bulb set bonus
            thoriumPlayer.bulbSpore = true;
            //petal shield
            thorium.GetItem("PetalShield").UpdateAccessory(player, hideVisual);
            player.statDefense -= 2;
            //night shade petal
            thoriumPlayer.nightshadeBoost = true;
        }
        
        private readonly string[] items =
        {
            "BulbHood",
            "BulbChestplate",
            "BulbLeggings",
            "PetalShield",
            "NightShadePetal",
            "BloomingBlade",
            "CreepingVineStaff"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(ItemID.Sunflower);
            recipe.AddIngredient(ItemID.SkyBlueFlower);
            recipe.AddIngredient(ItemID.YellowMarigold);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
