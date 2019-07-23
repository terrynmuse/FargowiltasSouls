using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Essences
{
    public class HealerEssence : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crusader's Essence");
            Tooltip.SetDefault(
@"''This is only the beginning..''
18% increased radiant damage
5% increased healing and radiant casting speed
5% increased radiant critical strike chance");
            DisplayName.AddTranslation(GameCulture.Chinese, "十字军精华");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"''这才刚刚开始..''
增加18%光辉伤害
增加5%治疗和光辉施法速度
增加5%光辉暴击率");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 4;
            item.value = 150000; 
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            HealEffect(player);
        }
        
        private void HealEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.radiantBoost += 0.18f;
            thoriumPlayer.radiantSpeed -= 0.05f;
            thoriumPlayer.healingSpeed += 0.05f;
            thoriumPlayer.radiantCrit += 5;
        }
        
        private readonly string[] items =
        {
            "ClericEmblem",
            "HeartWand",
            "LifeQuartzClaymore",
            "FeatherBarrierRod",
            "TulipStaff",
            "LargePopcorn",
            "BatScythe",
            "DivineLotus",
            "GraniteEnergyStaff",
            "SentinelWand",
            "LifeDisperser",
            "RedeemerStaff",
            "DeepStaff",
            "StarRod"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
