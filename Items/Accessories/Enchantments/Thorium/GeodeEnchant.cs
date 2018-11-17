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
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Geode Enchantment");
            Tooltip.SetDefault(
@"'Made from the most luxurious of materials'
Light is emitted from the player
Can detect ore and treasures
Summons a magic lantern, that releases a constant aura of regeneration
Summons a money spitting treasure chest
Every 10, 500 & 10,000 damage dealt will cause the chest to spit out a corresponding coin");
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
            
            GeodeEffect(player);
        }
        
        private void GeodeEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.geodeShine = true;
            Lighting.AddLight(player.position, 1.2f, 0.8f, 1.2f);
            //set bonus
            player.findTreasure = true;
            //lantern pet
            thoriumPlayer.lanternPet = true;
            //chest pet
            thoriumPlayer.LockBoxPet = true;
        }
        
        private readonly string[] items =
        {
            "GeodeHelmet",
            "GeodeChestplate",
            "GeodeGreaves",
            "EnchantedPickaxe",
            "GeodePickaxe",
            "DragonDrill",
            "FleshDrill",
            "Lantern",
            "SupportLanternItem",
            "JonesLockBox"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
