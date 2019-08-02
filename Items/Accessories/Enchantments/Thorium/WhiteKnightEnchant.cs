using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class WhiteKnightEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Knight Enchantment");
            Tooltip.SetDefault(
@"'Protect e-girls at all costs'
Magical attacks have a 33% chance to recover some mana
Effects of Shade Band
Summons a Moogle pet");
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
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.whiteKnightSet = true;
            //shade band
            thoriumPlayer.shadeBand = true;
            //pet
            modPlayer.AddPet("Moogle Pet", hideVisual, thorium.BuffType("LilMogBuff"), thorium.ProjectileType("LilMog"));
            modPlayer.KnightEnchant = true;
        }
        
        private readonly string[] items =
        {
            "WhiteKnightMask",
            "WhiteKnightTabard",
            "WhiteKnightLeggings",
            "ShadeBand",
            "PrismiteStaff",
            "VileSpitter",
            "FrostFang"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(ItemID.SkyFracture);
            recipe.AddIngredient(thorium.ItemType("SnowWhite"));
            recipe.AddIngredient(thorium.ItemType("DelectableNut"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}