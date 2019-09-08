using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class DemonBloodEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Blood Enchantment");
            Tooltip.SetDefault(
@"'Infused with Corrupt Blood'
Dealing damage will grant you a 'Blood Charge'
At maximum charges, your next attack will deal 2x damage and heal you for 20% of the damage dealt
Consecutive attacks against enemies might drop flesh, which grants bonus life and damage
Effects of Vampire Gland, Demon Blood Badge, and Vile Flail-Core
Effects of Blood Demon's Subwoofer and Yellow Music Player
Summons a pet Flying Blister");
            DisplayName.AddTranslation(GameCulture.Chinese, "魔血魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'注满了腐化之血'
攻击提供'蓄血'Buff
充能完毕时, 你的下一次攻击会造成双倍伤害, 并将伤害的20%转化为治疗
连续攻击敌人时概率掉落肉, 拾取肉会获得额外生命并增加伤害
拥有吸血鬼试剂, 魔血徽章和邪恶链锤核心的效果
拥有血魔音箱和黄色播放器的效果
召唤宠物泡泡虫");
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
            //demon blood effect
            modPlayer.DemonBloodEnchant = true;
            //demon blood badge
            thoriumPlayer.CrimsonBadge = true;
            //vile core
            thoriumPlayer.vileCore = true;
            //subwoofer
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerIchor = true;
                }
            }
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3CriticalStrike = 2;
            //flesh set bonus
            thoriumPlayer.Symbiotic = true;
            //vampire gland
            thoriumPlayer.vampireGland = true;
            //blister pet
            modPlayer.AddPet("Blister Pet", hideVisual, thorium.BuffType("BlisterBuff"), thorium.ProjectileType("BlisterPet"));
            thoriumPlayer.blisterPet = true;
        }
        
        private readonly string[] items =
        {
            "DemonRageBadge",
            "VileCore",
            "CrimsonSubwoofer",
            "TunePlayerCritChance",
            "DarkContagionBook"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("DemonBloodHelmet"));
            recipe.AddIngredient(thorium.ItemType("DemonBloodBreastPlate"));
            recipe.AddIngredient(thorium.ItemType("DemonBloodGreaves"));
            recipe.AddIngredient(null, "FleshEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(thorium.ItemType("FesteringBalloon"), 300);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
