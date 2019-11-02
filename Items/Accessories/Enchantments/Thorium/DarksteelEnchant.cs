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
50% damage reduction at Full HP
Nothing will stop your movement 
Double tap to dash
Grants immunity to shambler chain-balls
Effects of Spiked Bracer");
            DisplayName.AddTranslation(GameCulture.Chinese, "暗金魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'轻巧而耐用'
满血时增加50%伤害减免
没有什么能阻止你的移动
获得冲刺能力
免疫蹒跚者的链球效果
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
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //darksteel bonuses
            player.noKnockback = true;
            player.iceSkate = true;
            player.dash = 1;
            //steel effect
            if (player.statLife == player.statLifeMax2)
            {
                player.endurance += .5f;
            }
            //spiked bracers
            player.thorns += 0.35f;
            //ball n chain
            thoriumPlayer.ballnChain = true;
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
