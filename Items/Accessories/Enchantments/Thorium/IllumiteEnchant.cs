using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;

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
Every third attack will unleash an illumite missile
Effects of Pink Music Player
Summons a pet Pink Slime");
            DisplayName.AddTranslation(GameCulture.Chinese, "荧光魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'好像还不够粉'
大多数远程武器具有额外抛射物
每5颗子弹会发射荧光子弹
每4支箭会发射一串荧光能量
每3发导弹会发射荧光导弹
拥有粉色播放器的效果
召唤宠物粉红史莱姆");
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
            modPlayer.IllumiteEnchant = true;
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3LifeRegen = 2;
            //slime pet
            modPlayer.AddPet("Pink Slime Pet", hideVisual, thorium.BuffType("PinkSlimeBuff"), thorium.ProjectileType("PinkSlime"));
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
