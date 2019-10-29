using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class HeartoftheMasochist : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart of the Masochist");
            Tooltip.SetDefault(@"'Suffering no longer hurts, mostly'
Grants immunity to Living Wasteland, Frozen, Oozed, Withered Weapon, and Withered Armor
Grants immunity to Feral Bite, Mutant Nibble, Flipped, Unstable, Distorted, and Chaos State
Grants immunity to Wet, Electrified, Moon Leech, Nullification Curse, and water debuffs
Increases damage, critical strike chance, and damage reduction by 10%, 
Increases flight time by 100%
You may periodically fire additional attacks depending on weapon type
Your critical strikes inflict Rotting and Betsy's Curse
Press C to become a fireball and perform a short invincible dash
Grants effects of Wet debuff while riding Cute Fishron and gravity control
Summons a friendly super Flocko, Mini Saucer, and true eyes of Cthulhu");
            DisplayName.AddTranslation(GameCulture.Chinese, "受虐者之心");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'大多数情况下已经不用受苦了'
免疫人形废土,冻结,渗入,枯萎武器和枯萎盔甲
免疫野性咬噬,突变啃啄,翻转,不稳定,扭曲和混沌
免疫潮湿,带电,月之血蛭,无效诅咒和由水造成的Debuff
增加10%伤害,暴击率伤害减免
增加100%飞行时间
根据武器类型定期发动额外的攻击
暴击造成贝特希的诅咒
骑乘超可爱猪鲨时获得潮湿状态,能够控制重力
召唤一个友善的超级圣诞雪灵,迷你飞碟和真·克苏鲁之眼");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 11;
            item.value = Item.sellPrice(0, 9);
            item.defense = 10;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            fargoPlayer.AllDamageUp(.1f);
            fargoPlayer.AllCritUp(10);
            player.endurance += 0.1f;

            //pumpking's cape
            player.buffImmune[mod.BuffType("LivingWasteland")] = true;
            fargoPlayer.PumpkingsCape = true;
            fargoPlayer.AdditionalAttacks = true;

            //ice queen's crown
            player.buffImmune[BuffID.Frozen] = true;
            if (SoulConfig.Instance.GetValue("Flocko Minion"))
                player.AddBuff(mod.BuffType("SuperFlocko"), 2);

            //saucer control console
            player.buffImmune[BuffID.Electrified] = true;
            if (SoulConfig.Instance.GetValue("Saucer Minion"))
                player.AddBuff(mod.BuffType("SaucerMinion"), 2);

            //betsy's heart
            player.buffImmune[BuffID.OgreSpit] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            fargoPlayer.BetsysHeart = true;

            //mutant antibodies
            player.buffImmune[BuffID.Wet] = true;
            player.buffImmune[BuffID.Rabies] = true;
            player.buffImmune[mod.BuffType("MutantNibble")] = true;
            fargoPlayer.MutantAntibodies = true;
            if (player.mount.Active && player.mount.Type == MountID.CuteFishron)
                player.dripping = true;

            //galactic globe
            player.buffImmune[mod.BuffType("Flipped")] = true;
            player.buffImmune[mod.BuffType("FlippedHallow")] = true;
            player.buffImmune[mod.BuffType("Unstable")] = true;
            //player.buffImmune[mod.BuffType("CurseoftheMoon")] = true;
            player.buffImmune[BuffID.VortexDebuff] = true;
            player.buffImmune[BuffID.ChaosState] = true;
            if (SoulConfig.Instance.GetValue("Gravity Control"))
                player.gravControl = true;
            if (SoulConfig.Instance.GetValue("True Eyes Minion"))
                player.AddBuff(mod.BuffType("TrueEyes"), 2);
            fargoPlayer.GravityGlobeEX = true;
            fargoPlayer.wingTimeModifier += 1f;

            //heart of maso
            player.buffImmune[BuffID.MoonLeech] = true;
            player.buffImmune[mod.BuffType("NullificationCurse")] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("PumpkingsCape"));
            recipe.AddIngredient(mod.ItemType("IceQueensCrown"));
            recipe.AddIngredient(mod.ItemType("SaucerControlConsole"));
            recipe.AddIngredient(mod.ItemType("BetsysHeart"));
            recipe.AddIngredient(mod.ItemType("MutantAntibodies"));
            recipe.AddIngredient(mod.ItemType("GalacticGlobe"));
            recipe.AddIngredient(mod.ItemType("LunarCrystal"), 30);
            recipe.AddIngredient(ItemID.LunarBar, 15);

            recipe.AddTile(mod, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
