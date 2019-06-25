using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class RedRidingEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Riding Enchantment");
            Tooltip.SetDefault(
@"'Big Bad Red Riding Hood'
During a Full Moon, attacks may cause enemies to Super Bleed
Your attacks deal increasing damage to low HP enemies
Greatly enhances Explosive Traps effectiveness
Effects of Celestial Shell
Summons a pet Puppy");
            DisplayName.AddTranslation(GameCulture.Chinese, "红色游侠魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'大坏红帽'
满月时,攻击概率造成大出血
对低血量的敌人伤害增加
大幅加强爆炸陷阱能力
拥有天界贝壳的效果
召唤一只小狗");
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
            //celestial shell
            player.accMerman = true;
            player.wolfAcc = true;

            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }

            player.GetModPlayer<FargoPlayer>(mod).RedRidingEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HuntressAltHead);
            recipe.AddIngredient(ItemID.HuntressAltShirt);
            recipe.AddIngredient(ItemID.HuntressAltPants);
            recipe.AddIngredient(ItemID.HuntressBuckler);
            recipe.AddIngredient(ItemID.CelestialShell);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(thorium.ItemType("BloodyHighClaws"));
                recipe.AddIngredient(thorium.ItemType("LadyLight"));
                recipe.AddIngredient(ItemID.DD2PhoenixBow);
                recipe.AddIngredient(ItemID.DD2ExplosiveTrapT3Popper);
            }
            else
            {
                recipe.AddIngredient(ItemID.DD2PhoenixBow);
            }
            
            recipe.AddIngredient(ItemID.DogWhistle);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
