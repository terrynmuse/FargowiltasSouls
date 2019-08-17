using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class DarksteelEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darksteel Enchantment");
            Tooltip.SetDefault(
@"'Light yet durable'
8% damage reduction
Nothing will stop your movement 
Double tap to dash
Effects of Spiked Bracer");
            DisplayName.AddTranslation(GameCulture.Chinese, "暗金魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'轻巧而耐用'
增加8%伤害减免
没有什么能阻止你的移动
获得冲刺能力
拥有尖刺索的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 80000;
            item.shieldSlot = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //darksteel bonuses
            player.noKnockback = true;
            player.iceSkate = true;
            player.dash = 1;
            //steel set bonus
            thoriumPlayer.thoriumEndurance += 0.08f;
            //spiked bracers
            player.thorns += 0.35f;
        }
        
        private readonly string[] items =
        {
            "BallnChain",
            "eeDarksteelMace",
            "eeSoulSiphon",
            "ManHacker",
            "DarksteelHelmetStand",
            "GrayDPaintingItem",
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("hDarksteelFaceGuard"));
            recipe.AddIngredient(thorium.ItemType("iDarksteelBreastPlate"));
            recipe.AddIngredient(thorium.ItemType("jDarksteelGreaves"));
            recipe.AddIngredient(null, "SteelEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
