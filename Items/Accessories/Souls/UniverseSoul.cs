using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class UniverseSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        private readonly Mod dbzMod = ModLoader.GetMod("DBZMOD");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Universe");

            string tooltip =
@"'The heavens themselves bow to you'
66% increased all damage
50% increased use speed for all weapons
50% increased shoot speed
25% increased all critical chance
Crits deal 5x damage
All weapons have double knockback
Increases your maximum mana by 300
";
            string tooltip_ch =
@"'诸天也向你俯首'
增加66%所有伤害
增加50%所有武器使用速度
增加50%射击速度
增加25%所有暴击率
暴击造成5倍伤害
所有武器双倍击退
增加300最大法力值
";

            if (thorium != null)
            {
                tooltip += "Increases maximum inspiration by 30\n";
                tooltip_ch += "增加30最大灵感值\n";
            }

            tooltip += 
@"Increases your max number of minions by 8
Increases your max number of sentries by 4
All attacks inflict Flames of the Universe
Effects of the Fire Gauntlet and Yoyo Bag
Effects of Sniper Scope, Celestial Cuffs and Mana Flower";

            tooltip_ch +=
@"+8最大召唤栏
+4最大哨兵栏
所有攻击造成宇宙之火效果
拥有烈火手套和悠悠球袋的效果
拥有狙击镜, 星体手铐和魔力花的效果";

            if (thorium != null)
            {
                tooltip += @"
Effects of Phylactery, Crystal Scorpion, and Yuma's Pendant
Effects of Guide to Expert Throwing - Volume III, Mermaid's Canteen, and Deadman's Patch
Effects of Support Sash, Saving Grace, Soul Guard, Archdemon's Curse, Archangel's Heart, and Medical Bag
Effects of Epic Mouthpiece, Straight Mute, Digital Tuner, and Guitar Pick Claw";
                
                tooltip_ch += @"
拥有拥有魂匣, 魔晶蝎和云码垂饰的效果
拥有投手大师指导:卷三, 美人鱼水壶和亡者眼罩的效果
拥有支援腰带, 救世恩典, 灵魂庇佑, 大恶魔之咒, 圣天使之心和医疗包的效果
拥有史诗吹口, 金属弱音器, 数码调谐器和吉他拨片的效果";
            }
            
            if (calamity != null)
            {
                tooltip += "\nEffects of Elemental Gauntlet, Elemental Quiver, Ethereal Talisman, Statis' Belt of Curses, and Nanotech";
                
                tooltip_ch += "\n拥有元素之握, 元素箭袋, 空灵护符, 斯塔提斯的诅咒系带和纳米技术的效果";
            }
            
            if(dbzMod != null)
            {
                tooltip += "\nEffects of Zenkai Charm and Aspera Crystallite";
                
                tooltip_ch += "\n拥有全开符咒和原始晶粒的效果";
            }

            Tooltip.SetDefault(tooltip);

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(12, 5));
            DisplayName.AddTranslation(GameCulture.Chinese, "寰宇之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 5000000;
            item.rare = -12;
            item.expert = true;

            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            modPlayer.AllDamageUp(.66f);
            modPlayer.AllCritUp(25);
            //use speed, velocity, debuffs, crit dmg, mana up, double knockback
            modPlayer.UniverseEffect = true;
            
            if (SoulConfig.Instance.GetValue("Universe Attack Speed"))
            {
                modPlayer.AttackSpeed *= 1.5f;
            }

            player.maxMinions += 8;
            player.maxTurrets += 4;

            //accessorys
            player.counterWeight = 556 + Main.rand.Next(6);
            player.yoyoGlove = true;
            player.yoyoString = true;
            if (SoulConfig.Instance.GetValue("Sniper Scope"))
            {
                player.scope = true;
            }
            player.manaFlower = true;
            player.manaMagnet = true;
            player.magicCuffs = true;

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);

            if (Fargowiltas.Instance.CalamityLoaded) Calamity(player);

            if (Fargowiltas.Instance.DBZMODLoaded) DBT(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //phylactery
            if (!thoriumPlayer.lichPrevent)
            {
                player.AddBuff(thorium.BuffType("LichActive"), 60, true);
            }
            //crystal scorpion
            if (SoulConfig.Instance.GetValue("Crystal Scorpion"))
            {
                thoriumPlayer.crystalScorpion = true;
            }
            //yumas pendant
            if (SoulConfig.Instance.GetValue("Yuma's Pendant"))
            {
                thoriumPlayer.yuma = true;
            }

            //THROWING
            thoriumPlayer.throwGuide2 = true;
            //dead mans patch
            thoriumPlayer.deadEyeBool = true;
            //mermaid canteen
            thoriumPlayer.throwerExhaustionMax += 1125;
            thoriumPlayer.canteenCadet = true;

            //HEALER
            thoriumPlayer.radiantBoost += 0.4f;
            thoriumPlayer.radiantSpeed -= 0.25f;
            thoriumPlayer.healingSpeed += 0.25f;
            thoriumPlayer.radiantCrit += 20;
            //support stash
            thoriumPlayer.supportSash = true;
            thoriumPlayer.quickBelt = true;
            //saving grace
            thoriumPlayer.crossHeal = true;
            thoriumPlayer.healBloom = true;
            //soul guard
            thoriumPlayer.graveGoods = true;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && player2 != player && Vector2.Distance(player2.Center, player.Center) < 400f)
                {
                    player2.AddBuff(thorium.BuffType("AegisAura"), 30, false);
                }
            }
            //archdemon's curse
            thoriumPlayer.darkAura = true;
            //archangels heart
            thoriumPlayer.healBonus += 5;
            //medical bag
            thoriumPlayer.medicalAcc = true;
            //head mirror arrow 
            if (SoulConfig.Instance.GetValue("Head Mirror"))
            {
                float num = 0f;
                int num2 = player.whoAmI;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && Main.player[i] != player && !Main.player[i].dead && (Main.player[i].statLifeMax2 - Main.player[i].statLife) > num)
                    {
                        num = (Main.player[i].statLifeMax2 - Main.player[i].statLife);
                        num2 = i;
                    }
                }
                if (player.ownedProjectileCounts[thorium.ProjectileType("HealerSymbol")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("HealerSymbol"), 0, 0f, player.whoAmI, 0f, 0f);
                }
                for (int j = 0; j < 1000; j++)
                {
                    Projectile projectile = Main.projectile[j];
                    if (projectile.active && projectile.owner == player.whoAmI && projectile.type == thorium.ProjectileType("HealerSymbol"))
                    {
                        projectile.timeLeft = 2;
                        projectile.ai[1] = num2;
                    }
                }
            }
            //BARD
            thoriumPlayer.symphonicDamage += 0.3f;
            thoriumPlayer.symphonicSpeed += .2f;
            thoriumPlayer.symphonicCrit += 15;
            thoriumPlayer.bardResourceMax2 += 30;
            //epic mouthpiece
            thoriumPlayer.accWindHoming = true;
            thoriumPlayer.bardHomingBonus = 5f;
            //straight mute
            thoriumPlayer.accBrassMute2 = true;
            //digital tuner
            thoriumPlayer.accPercussionTuner2 = true;
            //guitar pick claw
            thoriumPlayer.bardBounceBonus = 5;
        }

        private void Calamity(Player player)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            //melee
            modPlayer.eGauntlet = true;
            //removing the extra boosts it adds because meme calamity
            player.meleeDamage -= .15f;
            player.meleeSpeed -= .15f;
            player.meleeCrit -= 5;

            if (SoulConfig.Instance.GetValue("Elemental Quiver"))
            {
                //range
                modPlayer.eQuiver = true;
            }
            
            //magic
            modPlayer.eTalisman = true;
            //summon
            modPlayer.statisBeltOfCurses = true;
            modPlayer.shadowMinions = true;
            modPlayer.tearMinions = true;
            //throw
            modPlayer.nanotech = true;
        }

        private void DBT(Player player)
        {
            DBZMOD.MyPlayer dbtPlayer = player.GetModPlayer<DBZMOD.MyPlayer>();

            dbtPlayer.chargeMoveSpeed = Math.Max(dbtPlayer.chargeMoveSpeed, 2f);
            dbtPlayer.kiKbAddition += 0.4f;
            dbtPlayer.kiDrainMulti -= 0.5f;
            dbtPlayer.kiMaxMult += 0.4f;
            dbtPlayer.kiRegen += 5;
            dbtPlayer.orbGrabRange += 6;
            dbtPlayer.orbHealAmount += 150;
            dbtPlayer.chargeLimitAdd += 8;
            dbtPlayer.flightSpeedAdd += 0.6f;
            dbtPlayer.flightUsageAdd += 3;
            dbtPlayer.zenkaiCharm = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GladiatorsSoul");
            recipe.AddIngredient(null, "SnipersSoul");
            recipe.AddIngredient(null, "ArchWizardsSoul");
            recipe.AddIngredient(null, "ConjuristsSoul");
            recipe.AddIngredient(null, "OlympiansSoul");

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "GuardianAngelsSoul");
                recipe.AddIngredient(null, "BardSoul");
                recipe.AddIngredient(thorium.ItemType("TheRing"));              
            }

            if (Fargowiltas.Instance.DBZMODLoaded)
            {
                recipe.AddIngredient(null, "KiSoul");
            }

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
