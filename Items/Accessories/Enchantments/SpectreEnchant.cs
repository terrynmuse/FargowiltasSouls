using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class SpectreEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spectre Enchantment");

            string tooltip = 
@"'Their lifeforce will be their undoing'
Damage has a chance to spawn damaging orbs
If you crit, you might also get a healing orb
";
            string tooltip_ch =
@"'他们的生命力将毁灭自己'
魔法伤害有机会产生伤害法球
暴击会造成治疗球爆发
";

            if(thorium != null)
            {
                tooltip +=
@"Killing enemies or continually damaging bosses generates soul wisps
After generating 5 wisps, they are instantly consumed to heal you for 10 life
Effects of Ghastly Carapace and Inner Flame
";
                tooltip_ch +=
@"杀死敌人或持续攻击Boss会产生灵魂碎片
在产生5个后,它们会立即被消耗,治疗10点生命
拥有惊魂甲壳和心灵之火的效果
";
            }

            tooltip += "Summons a pet Wisp";
            tooltip_ch += "召唤一个瓶中精灵";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "幽魂魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).SpectreEffect(hideVisual);

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            if (Soulcheck.GetValue("Ghastly Carapace"))
            {
                //ghastly carapace
                if (!thoriumPlayer.lifePrevent)
                {
                    player.AddBuff(thorium.BuffType("GhastlySoul"), 60, true);
                }
            }

            if(Soulcheck.GetValue("Spirit Trapper Wisps"))
            {
                //spirit trapper set bonus
                thoriumPlayer.spiritTrapper = true;
            }
            
            //inner flame
            thoriumPlayer.spiritFlame = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.SpectreMask);
                recipe.AddIngredient(ItemID.SpectreHood);
                recipe.AddIngredient(ItemID.SpectreRobe);
                recipe.AddIngredient(ItemID.SpectrePants);
                recipe.AddIngredient(null, "SpiritTrapperEnchant");
                recipe.AddIngredient(thorium.ItemType("GhastlyCarapace"));
                recipe.AddIngredient(ItemID.Keybrand);
                recipe.AddIngredient(ItemID.MagicalHarp);
                recipe.AddIngredient(ItemID.SpectreStaff);
            }
            else
            {
                recipe.AddRecipeGroup("FargowiltasSouls:AnySpectreHead");
                recipe.AddIngredient(ItemID.SpectreRobe);
                recipe.AddIngredient(ItemID.SpectrePants);
                recipe.AddIngredient(ItemID.Keybrand);
                recipe.AddIngredient(ItemID.SpectreStaff);
                recipe.AddIngredient(ItemID.UnholyTrident);
            }
            
            recipe.AddIngredient(ItemID.WispinaBottle);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
