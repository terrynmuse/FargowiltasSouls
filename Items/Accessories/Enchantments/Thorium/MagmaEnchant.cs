using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class MagmaEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public bool allowJump = true;
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magma Enchantment");
            Tooltip.SetDefault(
@"'Bursting with heat'
Fire surrounds your armour and melee weapons
Enemies that you set on fire or singe will take additional damage over time
Effects of Spring Steps, Slag Stompers, and Molten Spear Tip");
            DisplayName.AddTranslation(GameCulture.Chinese, "熔岩魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'充斥热能'
火焰环绕着你的盔甲和近战武器
随着时间的推移,被你点燃或烧伤的敌人会受到额外的伤害
拥有弹簧鞋, 熔渣重踏和炽热枪尖的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 60000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //set bonus
            player.magmaStone = true;
            thoriumPlayer.magmaSet = true;
            //spring steps
            thorium.GetItem("SpringSteps").UpdateAccessory(player, hideVisual);

            if (Soulcheck.GetValue("Slag Stompers"))
            {
                //slag stompers
                timer++;
                if (timer > 20)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0.1f * Main.rand.Next(-25, 25), 2f, thorium.ProjectileType("SlagPro"), 20, 1f, Main.myPlayer, 0f, 0f);
                    timer = 0;
                }
            }
            //molten spear tip
            thoriumPlayer.spearFlame = true;
        }
        
        private readonly string[] items =
        {
            "MagmaHelmet",
            "MagmaChestGuard",
            "MagmaGreaves",
            "SpringSteps",
            "SlagStompers",
            "MoltenSpearTip",
            "MagmaShiv",
            "MagmaPolearm",
            "MagmaticRicochet",
            "MagmaFlail"
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
