using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class IllumiteEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Illumite Enchantment");
            Tooltip.SetDefault(
@"'As if you weren't pink enough'
Most of your ranged weapons take on special properties
Every 5 bullets fired will unleash a multi-hit illumite bullet
Every 4 arrows fired will unleash a barrage of illumite energy
Every 3 rockets fired will unleash an illumite missile
Effects of Pink Music Player
Summons a pet Pink Slime");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.illumiteSet = true;
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3LifeRegen = 2;
            //slime pet
            modPlayer.AddPet("Pink Slime Pet", hideVisual, thorium.BuffType("PinkSlimeBuff"), thorium.ProjectileType("PinkSlime"));
            modPlayer.IllumiteEnchant = true;
        }
        
        private readonly string[] items =
        {
            "IllumiteMask",
            "IllumiteChestplate",
            "IllumiteGreaves",
            "TunePlayerLifeRegen",
            "CupidString",
            "ShusWrath",
            "HandCannon",
            "IllumiteBlaster",
            "IllumiteBarrage",
            "PinkSludge"
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
