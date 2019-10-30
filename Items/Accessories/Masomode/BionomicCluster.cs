using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class BionomicCluster : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bionomic Cluster");
            Tooltip.SetDefault(@"'The amalgamate born of a thousand common enemies'
Grants immunity to Frostburn, Shadowflame, Squeaky Toy, Guilty, Mighty Wind, and Suffocation
Grants immunity to Flames of the Universe, Clipped Wings, Crippled, Webbed, and Purified
Grants immunity to Lovestruck, Stinky, Midas, cactus damage, and enemies that steal items
Your attacks can inflict Clipped Wings, spawn Frostfireballs, and produce hearts
You have autofire, improved night vision, and faster respawn when no boss is alive
Automatically use mana potions when needed and gives modifier protection
Attacks have a chance to squeak and deal 1 damage to you
You erupt into Shadowflame tentacles when injured
Certain enemies will drop potions when defeated
Summons a friendly rainbow slime");
            DisplayName.AddTranslation(GameCulture.Chinese, "生态集群");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'由上千普通敌人融合而成'
免疫寒焰,暗影烈焰,吱吱响的玩具,内疚,强风和窒息
免疫宇宙之火,剪除羽翼,残疾,织网和净化
免疫热恋,恶臭,点金手和偷取物品的敌人
攻击造成剪除羽翼,发射霜火球,并且产生心
一键连发,提高夜视能力,
没有Boss存活时,重生速度加快
在需要时自动使用魔力药水,并给予词缀保护
敌人攻击概率无效,只造成1点伤害
受伤时爆发暗影烈焰触须
召唤一个友善的彩虹史莱姆");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 8;
            item.value = Item.sellPrice(0, 6);
            item.defense = 6;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();

            //concentrated rainbow matter
            player.buffImmune[mod.BuffType("FlamesoftheUniverse")] = true;
            if (SoulConfig.Instance.GetValue("Rainbow Slime Minion"))
                player.AddBuff(mod.BuffType("RainbowSlime"), 2);

            //dragon fang
            player.buffImmune[mod.BuffType("ClippedWings")] = true;
            player.buffImmune[mod.BuffType("Crippled")] = true;
            if (SoulConfig.Instance.GetValue("Inflict Clipped Wings"))
                fargoPlayer.DragonFang = true;

            //frigid gemstone
            player.buffImmune[BuffID.Frostburn] = true;
            if (SoulConfig.Instance.GetValue("Frostfireballs"))
            {
                fargoPlayer.FrigidGemstone = true;
                if (fargoPlayer.FrigidGemstoneCD > 0)
                    fargoPlayer.FrigidGemstoneCD--;
            }

            //wretched pouch
            player.buffImmune[BuffID.ShadowFlame] = true;
            player.buffImmune[mod.BuffType("Shadowflame")] = true;
            player.GetModPlayer<FargoPlayer>().WretchedPouch = true;

            //sands of time
            player.buffImmune[BuffID.WindPushed] = true;
            fargoPlayer.SandsofTime = true;

            //squeaky toy
            player.buffImmune[mod.BuffType("SqueakyToy")] = true;
            player.buffImmune[mod.BuffType("Guilty")] = true;
            fargoPlayer.SqueakyAcc = true;

            //tribal charm
            player.buffImmune[BuffID.Webbed] = true;
            player.buffImmune[mod.BuffType("Purified")] = true;
            fargoPlayer.TribalCharm = true;

            //mystic skull
            player.buffImmune[BuffID.Suffocation] = true;
            player.manaFlower = true;

            //security wallet
            player.buffImmune[mod.BuffType("Midas")] = true;
            fargoPlayer.SecurityWallet = true;

            //carrot
            player.nightVision = true;

            //nymph's perfume
            player.buffImmune[BuffID.Lovestruck] = true;
            player.buffImmune[mod.BuffType("Lovestruck")] = true;
            player.buffImmune[BuffID.Stinky] = true;
            if (SoulConfig.Instance.GetValue("Attacks Spawn Hearts"))
            {
                fargoPlayer.NymphsPerfume = true;
                if (fargoPlayer.NymphsPerfumeCD > 0)
                    fargoPlayer.NymphsPerfumeCD--;
            }

            //tim's concoction
            if (SoulConfig.Instance.GetValue("Tim's Concoction"))
                player.GetModPlayer<FargoPlayer>().TimsConcoction = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("ConcentratedRainbowMatter"));
            recipe.AddIngredient(mod.ItemType("DragonFang"));
            recipe.AddIngredient(mod.ItemType("FrigidGemstone"));
            recipe.AddIngredient(mod.ItemType("SandsofTime"));
            recipe.AddIngredient(mod.ItemType("SqueakyToy"));
            recipe.AddIngredient(mod.ItemType("TribalCharm"));
            recipe.AddIngredient(mod.ItemType("MysticSkull"));
            recipe.AddIngredient(mod.ItemType("SecurityWallet"));
            recipe.AddIngredient(mod.ItemType("OrdinaryCarrot"));
            recipe.AddIngredient(mod.ItemType("WretchedPouch"));
            recipe.AddIngredient(mod.ItemType("NymphsPerfume"));
            recipe.AddIngredient(mod.ItemType("TimsConcoction"));
            recipe.AddIngredient(ItemID.SoulofLight, 20);
            recipe.AddIngredient(ItemID.SoulofNight, 20);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
