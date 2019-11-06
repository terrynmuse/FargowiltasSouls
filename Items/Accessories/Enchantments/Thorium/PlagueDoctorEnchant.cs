using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class PlagueDoctorEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plague Doctor Enchantment");
            Tooltip.SetDefault(
@"'What nasty concoction could you be brewing?'
Your plague gas will linger in the air twice as long and your plague reactions will deal 20% more damage
Killing an enemy will release a soul fragment
Touching a soul fragment greatly increases your movement and throwing speed briefly
Effects of Lich's Gaze and Plague Lord's Flask");
            DisplayName.AddTranslation(GameCulture.Chinese, "瘟疫医生魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'你都能酿出什么恶心的药剂呢?'
瓦斯持续时间翻倍, 瓦斯反应多造成20%伤害
杀死敌人会释放灵魂碎片
触摸灵魂碎片会在短时间内极大增加移动速度和投掷速度
拥有瘟疫之主药剂瓶和巫妖之视效果");
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
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            thoriumPlayer.plagueSet = true;
            //plague lords fask
            modPlayer.PlagueAcc = true;
            //lich set bonus
            modPlayer.LichEnchant = true;
            //lich gaze
            thoriumPlayer.lichGaze = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(thorium.ItemType("PlagueDoctersMask"));
            recipe.AddIngredient(thorium.ItemType("PlagueDoctersGarb"));
            recipe.AddIngredient(thorium.ItemType("PlagueDoctersLeggings"));
            recipe.AddIngredient(null, "LichEnchant");
            recipe.AddIngredient(thorium.ItemType("PlagueLordFlask"));
            recipe.AddIngredient(thorium.ItemType("CombustionFlask"), 300);
            recipe.AddIngredient(thorium.ItemType("NitrogenVial"), 300);
            recipe.AddIngredient(thorium.ItemType("CorrosionBeaker"), 300);
            recipe.AddIngredient(thorium.ItemType("FrostPlagueStaff"));
            recipe.AddIngredient(ItemID.ToxicFlask);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
