using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class GeodeEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Geode Enchantment");
            Tooltip.SetDefault(
@"'Made from the most luxurious of materials'
50% increased mining speed
Shows the location of enemies, traps, and treasures
Light is emitted from the player
Summons a pet Magic Lantern, Inspiring Lantern, and Lock Box");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 4;
            item.value = 120000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            thoriumPlayer.geodeShine = true;
            Lighting.AddLight(player.position, 1.2f, 0.8f, 1.2f);
            //pets
            modPlayer.AddPet("Inspiring Lantern Pet", hideVisual, thorium.BuffType("SupportLanternBuff"), thorium.ProjectileType("SupportLantern"));
            modPlayer.AddPet("Lock Box Pet", hideVisual, thorium.BuffType("LockBoxBuff"), thorium.ProjectileType("LockBoxPet"));
            //mining speed, spelunker, dangersense, light, hunter, pet
            modPlayer.MinerEffect(hideVisual, .5f);
        }
        
        private readonly string[] items =
        {
            "CrystalineCharm",
            "EnchantedPickaxe",
            "GnomePick",
            "Lantern",
            "SupportLanternItem",
            "JonesLockBox"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("GeodeHelmet"));
            recipe.AddIngredient(thorium.ItemType("GeodeChestplate"));
            recipe.AddIngredient(thorium.ItemType("GeodeGreaves"));
            recipe.AddIngredient(null, "MinerEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
