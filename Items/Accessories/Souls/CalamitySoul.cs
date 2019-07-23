using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using CalamityMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class CalamitySoul : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public int dragonTimer = 60;
        public const int FireProjectiles = 2;
        public const float FireAngleSpread = 120f;
        public int FireCountdown;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Tyrant");
            Tooltip.SetDefault(
@"'And the land grew quiet once more...'
All armor bonuses from Omega Blue, Auric Tesla, and Demon Shade
Effects of Profaned Soul Artifact, Core of the Blood God, and Affliction
Effects of Nebulous Core, Godly Soul Artifact, and Yharim's Gift
Effects of Counter Scarf, The Community, Draedon's Heart, and The Amalgam
Effects of Heart of the Elements, The Sponge, and Dark Sun Ring");
            DisplayName.AddTranslation(GameCulture.Chinese, "暴君之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'于是大地再次恢复了平静...'
拥有蓝色欧米茄,圣金源和魔影的套装效果
拥有亵渎神物,血神核心和灾劫之尖啸的效果
拥有星云之核,圣魂神物和魔君的礼物的效果
拥有反击围巾,归一元心石,嘉登之心和聚合之脑的效果
拥有元灵之心,化绵留香石和蚀日尊戒的效果");

        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;//
            item.value = 20000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);
            //TARRAGON
            //profaned soul artifact
            if (Soulcheck.GetValue("Profaned Soul Artifact"))
            {
                modPlayer.pArtifact = true;
            }
            
            //BLOOD FLARE
            //core of the blood god
            modPlayer.coreOfTheBloodGod = true;
            modPlayer.fleshTotem = true;
            //affliction
            modPlayer.affliction = true;
            if (player.whoAmI != Main.myPlayer && player.miscCounter % 10 == 0)
            {
                int myPlayer = Main.myPlayer;
                if (Main.player[myPlayer].team == player.team && player.team != 0)
                {
                    float x = player.position.X - Main.player[myPlayer].position.X;
                    float y = player.position.Y - Main.player[myPlayer].position.Y;
                    if ((float)Math.Sqrt((x * x + y * y)) < 2800f)
                    {
                        Main.player[myPlayer].AddBuff(calamity.BuffType("Afflicted"), 20, true);
                    }
                }
            }
            //OMEGA BLUE
            mod.GetItem("OmegaBlueEnchant").UpdateAccessory(player, hideVisual);
            //GODSLAYER
            if (Soulcheck.GetValue("Nebulous Core"))
            {
                //nebulous core, rest in auric
                modPlayer.nCore = true;
                int dmg = 300;
                float kb = 3f;
                if (Main.rand.Next(15) == 0)
                {
                    int num3 = 0;
                    for (int i = 0; i < 1000; i++)
                    {
                        if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].type == calamity.ProjectileType("NebulaStar"))
                        {
                            num3++;
                        }
                    }
                    if (Main.rand.Next(15) >= num3 && num3 < 10)
                    {
                        int num4 = 50;
                        int num5 = 24;
                        int num6 = 90;
                        for (int j = 0; j < num4; j++)
                        {
                            int num7 = Main.rand.Next(200 - j * 2, 400 + j * 2);
                            Vector2 center = player.Center;
                            center.X += Main.rand.Next(-num7, num7 + 1);
                            center.Y += Main.rand.Next(-num7, num7 + 1);
                            if (!Collision.SolidCollision(center, num5, num5) && !Collision.WetCollision(center, num5, num5))
                            {
                                center.X += (num5 / 2);
                                center.Y += (num5 / 2);
                                if (Collision.CanHit(new Vector2(player.Center.X, player.position.Y), 1, 1, center, 1, 1) || Collision.CanHit(new Vector2(player.Center.X, player.position.Y - 50f), 1, 1, center, 1, 1))
                                {
                                    int num8 = (int)center.X / 16;
                                    int num9 = (int)center.Y / 16;
                                    bool flag = false;
                                    if (Main.rand.Next(3) == 0 && Main.tile[num8, num9] != null && Main.tile[num8, num9].wall > 0)
                                    {
                                        flag = true;
                                    }
                                    else
                                    {
                                        center.X -= (num6 / 2);
                                        center.Y -= (num6 / 2);
                                        if (Collision.SolidCollision(center, num6, num6))
                                        {
                                            center.X += (num6 / 2);
                                            center.Y += (num6 / 2);
                                            flag = true;
                                        }
                                    }
                                    if (flag)
                                    {
                                        for (int k = 0; k < 1000; k++)
                                        {
                                            if (Main.projectile[k].active && Main.projectile[k].owner == player.whoAmI && Main.projectile[k].type == calamity.ProjectileType("NebulaStar") && (center - Main.projectile[k].Center).Length() < 48f)
                                            {
                                                flag = false;
                                                break;
                                            }
                                        }
                                        if (flag && Main.myPlayer == player.whoAmI)
                                        {
                                            Projectile.NewProjectile(center.X, center.Y, 0f, 0f, calamity.ProjectileType("NebulaStar"), dmg, kb, player.whoAmI, 0f, 0f);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //SILVA
            if (Soulcheck.GetValue("Godly Soul Artifact"))
            {
                //godly soul artifact
                modPlayer.gArtifact = true;
            }
            if (Soulcheck.GetValue("Yharim's Gift"))
            {
                //yharims gift
                if (player.velocity.X > 0.0 || player.velocity.Y > 0.0 || player.velocity.X < -0.1 || player.velocity.Y < -0.1)
                {
                    dragonTimer--;
                    if (dragonTimer <= 0)
                    {
                        if (player.whoAmI == Main.myPlayer)
                        {
                            int p = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, calamity.ProjectileType("DragonDust"), 350, 5f, player.whoAmI, 0f, 0f);
                            Main.projectile[p].timeLeft = 60;
                        }
                        dragonTimer = 60;
                    }
                }
                else
                {
                    dragonTimer = 60;
                }
                if (player.immune && Main.rand.Next(8) == 0 && player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        float posX = player.position.X + Main.rand.Next(-400, 400);
                        float posY = player.position.Y - Main.rand.Next(500, 800);
                        Vector2 vector = new Vector2(posX, posY);
                        float num4 = player.position.X + (player.width / 2) - vector.X;
                        float num5 = player.position.Y + (player.height / 2) - vector.Y;
                        num4 += Main.rand.Next(-100, 101);
                        int num6 = 22;
                        float num7 = (float)Math.Sqrt((num4 * num4 + num5 * num5));
                        num7 = num6 / num7;
                        num4 *= num7;
                        num5 *= num7;
                        int num8 = Projectile.NewProjectile(posX, posY, num4, num5, calamity.ProjectileType("SkyFlareFriendly"), 750, 9f, player.whoAmI, 0f, 0f);
                        Main.projectile[num8].ai[1] = player.position.Y;
                        Main.projectile[num8].hostile = false;
                        Main.projectile[num8].friendly = true;
                    }
                }
            }
            
            //AURIC
            mod.GetItem("AuricEnchant").UpdateAccessory(player, hideVisual);
            //DEMONSHADE
            mod.GetItem("DemonShadeEnchant").UpdateAccessory(player, hideVisual);

            //counter scarf
            modPlayer.dodgeScarf = true;
            modPlayer.dashMod = 1;
            //the community
            modPlayer.community = true;
            //draedons heart
            modPlayer.draedonsHeart = true;
            player.buffImmune[calamity.BuffType("Horror")] = true;
            modPlayer.draedonsStressGain = true;
            //THE AMALGAM
            modPlayer.aBrain = true;
            if (Soulcheck.GetValue("Fungal Clump Minion"))
            {
                modPlayer.fungalClump = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("FungalClump")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("FungalClump"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.ProjectileType("FungalClump")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("FungalClump"), 250, 1f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            
            if (Soulcheck.GetValue("Poisonous Sea Water") && (player.velocity.X > 0.0 || player.velocity.Y > 0.0 || player.velocity.X < -0.1 || player.velocity.Y < -0.1) && player.whoAmI == Main.myPlayer)
            {
                int p = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, calamity.ProjectileType("PoisonousSeawater"), 500, 5f, player.whoAmI, 0f, 0f);
                Main.projectile[p].timeLeft = 10;
            }
            if (player.immune && Main.rand.Next(20) == 0 && player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i < 1; i++)
                {
                    float posX = player.position.X + Main.rand.Next(-400, 400);
                    float posY = player.position.Y - Main.rand.Next(500, 800);
                    Vector2 vector = new Vector2(posX, posY);
                    float num4 = player.position.X + (player.width / 2) - vector.X;
                    float num5 = player.position.Y + (player.height / 2) - vector.Y;
                    num4 += Main.rand.Next(-100, 101);
                    int num6 = 22;
                    float num7 = (float)Math.Sqrt((num4 * num4 + num5 * num5));
                    num7 = num6 / num7;
                    num4 *= num7;
                    num5 *= num7;
                    int num8 = (Main.rand.Next(2) == 0) ? calamity.ProjectileType("AuraRain") : calamity.ProjectileType("StandingFire");
                    int num9 = Projectile.NewProjectile(posX, posY, num4, num5, num8, 500, 3f, player.whoAmI, 0f, 0f);
                    Main.projectile[num9].tileCollide = false;
                }
            }

            if (FireCountdown == 0)
            {
                FireCountdown = 600;
            }
            if (FireCountdown > 0)
            {
                FireCountdown--;
                if (FireCountdown == 0 && player.whoAmI == Main.myPlayer)
                {
                    int num15 = 25;
                    float x = (Main.rand.Next(1000) - 500) + player.Center.X;
                    float y = -1000f + player.Center.Y;
                    Vector2 vector2 = new Vector2(x, y);
                    Vector2 vector3 = player.Center - vector2;
                    vector3.Normalize();
                    vector3 *= num15;
                    for (int k = 0; k < 2; k++)
                    {
                        Vector2 vector4 = vector2;
                        vector4.X = vector4.X + (k * 30) - 30f;
                        Vector2 vector5 = vector3;
                        vector5 = Utils.RotatedBy(vector3, MathHelper.ToRadians(-60f + 120f * k / 2f), default(Vector2));
                        vector5.X = vector5.X + 3f * Utils.NextFloat(Main.rand) - 1.5f;
                        int num16 = Projectile.NewProjectile(vector4.X, vector4.Y, vector5.X, vector5.Y, calamity.ProjectileType("BrimstoneHellfireballFriendly2"), 500, 5f, Main.myPlayer, 0f, 0f);
                        Main.projectile[num16].tileCollide = false;
                        Main.projectile[num16].timeLeft = 50;
                    }
                }
            }
            if (Soulcheck.GetValue("Elemental Waifus"))
            {
                //HEART OF THE ELEMENTS
                modPlayer.allWaifus = true;
                modPlayer.elementalHeart = true;
                int num = NPC.downedMoonlord ? 150 : 90;
                float num2 = 2f;
                if (player.ownedProjectileCounts[calamity.ProjectileType("BigBustyRose")] > 1 || player.ownedProjectileCounts[calamity.ProjectileType("SirenLure")] > 1 || player.ownedProjectileCounts[calamity.ProjectileType("DrewsSandyWaifu")] > 1 || player.ownedProjectileCounts[calamity.ProjectileType("SandyWaifu")] > 1 || player.ownedProjectileCounts[calamity.ProjectileType("CloudWaifu")] > 1)
                {
                    player.ClearBuff(calamity.BuffType("HotE"));
                }
                if (player.FindBuffIndex(calamity.BuffType("HotE")) == -1)
                {
                    player.AddBuff(calamity.BuffType("HotE"), 3600, true);
                }
                if (player.ownedProjectileCounts[calamity.ProjectileType("BigBustyRose")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("BigBustyRose"), (int)(num * num2), 2f, Main.myPlayer, 0f, 0f);
                }
                if (player.ownedProjectileCounts[calamity.ProjectileType("SirenLure")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("SirenLure"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                if (player.ownedProjectileCounts[calamity.ProjectileType("DrewsSandyWaifu")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("DrewsSandyWaifu"), (int)(num * num2), 2f, Main.myPlayer, 0f, 0f);
                }
                if (player.ownedProjectileCounts[calamity.ProjectileType("SandyWaifu")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("SandyWaifu"), (int)(num * num2), 2f, Main.myPlayer, 0f, 0f);
                }
                if (player.ownedProjectileCounts[calamity.ProjectileType("CloudyWaifu")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("CloudyWaifu"), (int)(num * num2), 2f, Main.myPlayer, 0f, 0f);
                }
                if (player.velocity.Y == 0f && player.grappling[0] == -1)
                {
                    int num3 = (int)player.Center.X / 16;
                    int num4 = (int)(player.position.Y + player.height - 1f) / 16;
                    if (Main.tile[num3, num4] == null)
                    {
                        Main.tile[num3, num4] = new Tile();
                    }
                    if (!Main.tile[num3, num4].active() && Main.tile[num3, num4].liquid == 0 && Main.tile[num3, num4 + 1] != null && WorldGen.SolidTile(num3, num4 + 1))
                    {
                        Main.tile[num3, num4].frameY = 0;
                        Main.tile[num3, num4].slope(0);
                        Main.tile[num3, num4].halfBrick(false);
                        if (Main.tile[num3, num4 + 1].type == 0)
                        {
                            if (Main.rand.Next(1000) == 0)
                            {
                                Main.tile[num3, num4].active(true);
                                Main.tile[num3, num4].type = 227;
                                Main.tile[num3, num4].frameX = (short)(34 * Main.rand.Next(1, 13));
                                while (Main.tile[num3, num4].frameX == 144)
                                {
                                    Main.tile[num3, num4].frameX = (short)(34 * Main.rand.Next(1, 13));
                                }
                            }
                            if (Main.netMode == 1)
                            {
                                NetMessage.SendTileSquare(-1, num3, num4, 1, 0);
                            }
                        }
                        if (Main.tile[num3, num4 + 1].type == 2)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                Main.tile[num3, num4].active(true);
                                Main.tile[num3, num4].type = 3;
                                Main.tile[num3, num4].frameX = (short)(18 * Main.rand.Next(6, 11));
                                while (Main.tile[num3, num4].frameX == 144)
                                {
                                    Main.tile[num3, num4].frameX = (short)(18 * Main.rand.Next(6, 11));
                                }
                            }
                            else
                            {
                                Main.tile[num3, num4].active(true);
                                Main.tile[num3, num4].type = 73;
                                Main.tile[num3, num4].frameX = (short)(18 * Main.rand.Next(6, 21));
                                while (Main.tile[num3, num4].frameX == 144)
                                {
                                    Main.tile[num3, num4].frameX = (short)(18 * Main.rand.Next(6, 21));
                                }
                            }
                            if (Main.netMode == 1)
                            {
                                NetMessage.SendTileSquare(-1, num3, num4, 1, 0);
                                return;
                            }
                        }
                        else if (Main.tile[num3, num4 + 1].type == 109)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                Main.tile[num3, num4].active(true);
                                Main.tile[num3, num4].type = 110;
                                Main.tile[num3, num4].frameX = (short)(18 * Main.rand.Next(4, 7));
                                while (Main.tile[num3, num4].frameX == 90)
                                {
                                    Main.tile[num3, num4].frameX = (short)(18 * Main.rand.Next(4, 7));
                                }
                            }
                            else
                            {
                                Main.tile[num3, num4].active(true);
                                Main.tile[num3, num4].type = 113;
                                Main.tile[num3, num4].frameX = (short)(18 * Main.rand.Next(2, 8));
                                while (Main.tile[num3, num4].frameX == 90)
                                {
                                    Main.tile[num3, num4].frameX = (short)(18 * Main.rand.Next(2, 8));
                                }
                            }
                            if (Main.netMode == 1)
                            {
                                NetMessage.SendTileSquare(-1, num3, num4, 1, 0);
                                return;
                            }
                        }
                        else if (Main.tile[num3, num4 + 1].type == 60)
                        {
                            Main.tile[num3, num4].active(true);
                            Main.tile[num3, num4].type = 74;
                            Main.tile[num3, num4].frameX = (short)(18 * Main.rand.Next(9, 17));
                            if (Main.netMode == 1)
                            {
                                NetMessage.SendTileSquare(-1, num3, num4, 1, 0);
                            }
                        }
                    }
                }
            }
            
            //the sponge
            modPlayer.beeResist = true;
            modPlayer.aSpark = true;
            modPlayer.gShell = true;
            modPlayer.fCarapace = true;
            modPlayer.absorber = true;
            modPlayer.aAmpoule = true;
            //dark sun ring
            modPlayer.darkSunRing = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "TarragonEnchant");
            recipe.AddIngredient(null, "BloodflareEnchant");
            recipe.AddIngredient(null, "OmegaBlueEnchant");
            recipe.AddIngredient(null, "GodSlayerEnchant");
            recipe.AddIngredient(null, "SilvaEnchant");
            recipe.AddIngredient(null, "AuricEnchant");
            recipe.AddIngredient(null, "DemonShadeEnchant");

            recipe.AddIngredient(calamity.ItemType("CounterScarf"));
            recipe.AddIngredient(calamity.ItemType("TheCommunity"));
            recipe.AddIngredient(calamity.ItemType("DraedonsHeart"));
            recipe.AddIngredient(calamity.ItemType("HeartoftheElements"));
            recipe.AddIngredient(calamity.ItemType("TheAmalgam"));
            recipe.AddIngredient(calamity.ItemType("Sponge"));
            recipe.AddIngredient(calamity.ItemType("DarkSunRing"));

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
