using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CalamityMod;
using System;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class GodSlayerEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("God Slayer Enchantment");
            Tooltip.SetDefault(
@"'The power to slay gods resides within you...'
Attacks have a 2% chance to do no damage to you
You will survive fatal damage and will be healed 150 HP if an attack would have killed you
This effect can only occur once every 45 seconds
Taking over 80 damage in one hit will cause you to release a swarm of high-damage god killer darts
An attack that would deal 80 damage or less will have its damage reduced to 1
Your ranged critical hits have a chance to critically hit, causing 4 times the damage
You have a chance to fire a god killer shrapnel round while firing ranged weapons
Enemies will release god slayer flames and healing flames when hit with magic attacks
Taking damage will cause you to release a magical god slayer explosion
Hitting enemies will summon god slayer phantoms
Summons a god-eating mechworm to fight for you
While at full HP all of your rogue stats are boosted by 10%
If you take over 80 damage in one hit you will be given extra immunity frames
Effects of the Nebulous Core");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(43, 96, 222));
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 10000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);

            if (Soulcheck.GetValue("God Slayer Effects"))
            {
                //body
                modPlayer.godSlayerReflect = true;
                //all
                modPlayer.godSlayer = true;
                //melee
                modPlayer.godSlayerDamage = true;
                //ranged
                modPlayer.godSlayerRanged = true;
                //magic
                modPlayer.godSlayerMage = true;
                //throw
                modPlayer.godSlayerThrowing = true;
            }
            
            if (Soulcheck.GetValue("Mechworm Minion"))
            {
                //summon
                modPlayer.godSlayerSummon = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("Mechworm")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("Mechworm"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.ProjectileType("MechwormHead")] < 1)
                    {
                        int whoAmI = player.whoAmI;
                        int num = calamity.ProjectileType("MechwormHead");
                        int num2 = calamity.ProjectileType("MechwormBody");
                        int num3 = calamity.ProjectileType("MechwormBody2");
                        int num4 = calamity.ProjectileType("MechwormTail");
                        for (int i = 0; i < 1000; i++)
                        {
                            if (Main.projectile[i].active && Main.projectile[i].owner == whoAmI && (Main.projectile[i].type == num || Main.projectile[i].type == num4 || Main.projectile[i].type == num2 || Main.projectile[i].type == num3))
                            {
                                Main.projectile[i].Kill();
                            }
                        }
                        int num5 = player.maxMinions;
                        if (num5 > 10)
                        {
                            num5 = 10;
                        }
                        int num6 = (int)(35f * (player.minionDamage * 5f / 3f + player.minionDamage * 0.46f * (num5 - 1)));
                        Vector2 value = player.RotatedRelativePoint(player.MountedCenter, true);
                        Vector2 value2 = Utils.RotatedBy(Vector2.UnitX, player.fullRotation, default(Vector2));
                        Vector2 value3 = Main.MouseWorld - value;
                        float num7 = Main.mouseX + Main.screenPosition.X - value.X;
                        float num8 = Main.mouseY + Main.screenPosition.Y - value.Y;
                        if (player.gravDir == -1f)
                        {
                            num8 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - value.Y;
                        }
                        float num9 = (float)Math.Sqrt((num7 * num7 + num8 * num8));
                        if ((float.IsNaN(num7) && float.IsNaN(num8)) || (num7 == 0f && num8 == 0f))
                        {
                            num7 = player.direction;
                            num8 = 0f;
                            num9 = 10f;
                        }
                        else
                        {
                            num9 = 10f / num9;
                        }
                        num7 *= num9;
                        num8 *= num9;
                        int num10 = -1;
                        int num11 = -1;
                        for (int j = 0; j < 1000; j++)
                        {
                            if (Main.projectile[j].active && Main.projectile[j].owner == whoAmI)
                            {
                                if (num10 == -1 && Main.projectile[j].type == num)
                                {
                                    num10 = j;
                                }
                                else if (num11 == -1 && Main.projectile[j].type == num4)
                                {
                                    num11 = j;
                                }
                                if (num10 != -1 && num11 != -1)
                                {
                                    break;
                                }
                            }
                        }
                        if (num10 == -1 && num11 == -1)
                        {
                            float num12 = Vector2.Dot(value2, value3);
                            if (num12 > 0f)
                            {
                                player.ChangeDir(1);
                            }
                            else
                            {
                                player.ChangeDir(-1);
                            }
                            num7 = 0f;
                            num8 = 0f;
                            value.X = Main.mouseX + Main.screenPosition.X;
                            value.Y = Main.mouseY + Main.screenPosition.Y;
                            int num13 = Projectile.NewProjectile(value.X, value.Y, num7, num8, calamity.ProjectileType("MechwormHead"), num6, 1f, whoAmI, 0f, 0f);
                            int num14 = num13;
                            num13 = Projectile.NewProjectile(value.X, value.Y, num7, num8, calamity.ProjectileType("MechwormBody"), num6, 1f, whoAmI, num14, 0f);
                            num14 = num13;
                            num13 = Projectile.NewProjectile(value.X, value.Y, num7, num8, calamity.ProjectileType("MechwormBody2"), num6, 1f, whoAmI, num14, 0f);
                            Main.projectile[num14].localAI[1] = num13;
                            Main.projectile[num14].netUpdate = true;
                            num14 = num13;
                            num13 = Projectile.NewProjectile(value.X, value.Y, num7, num8, calamity.ProjectileType("MechwormTail"), num6, 1f, whoAmI, num14, 0f);
                            Main.projectile[num14].localAI[1] = num13;
                            Main.projectile[num14].netUpdate = true;
                        }
                        else if (num10 != -1 && num11 != -1)
                        {
                            int num15 = Projectile.NewProjectile(value.X, value.Y, num7, num8, calamity.ProjectileType("MechwormBody"), num6, 1f, whoAmI, Main.projectile[num11].ai[0], 0f);
                            int num16 = Projectile.NewProjectile(value.X, value.Y, num7, num8, calamity.ProjectileType("MechwormBody2"), num6, 1f, whoAmI, num15, 0f);
                            Main.projectile[num15].localAI[1] = num16;
                            Main.projectile[num15].ai[1] = 1f;
                            Main.projectile[num15].minionSlots = 0f;
                            Main.projectile[num15].netUpdate = true;
                            Main.projectile[num16].localAI[1] = num11;
                            Main.projectile[num16].netUpdate = true;
                            Main.projectile[num16].minionSlots = 0f;
                            Main.projectile[num16].ai[1] = 1f;
                            Main.projectile[num11].ai[0] = num16;
                            Main.projectile[num11].netUpdate = true;
                            Main.projectile[num11].ai[1] = 1f;
                        }
                    }
                }
            }
            
            if (Soulcheck.GetValue("Nebulous Core"))
            {
                //nebulous core
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
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("GodSlayerHelm"));
            recipe.AddIngredient(calamity.ItemType("GodSlayerHelmet"));
            recipe.AddIngredient(calamity.ItemType("GodSlayerVisage"));
            recipe.AddIngredient(calamity.ItemType("GodSlayerHornedHelm"));
            recipe.AddIngredient(calamity.ItemType("GodSlayerMask"));
            recipe.AddIngredient(calamity.ItemType("GodSlayerChestplate"));
            recipe.AddIngredient(calamity.ItemType("GodSlayerLeggings"));
            recipe.AddIngredient(calamity.ItemType("NebulousCore"));
            recipe.AddIngredient(calamity.ItemType("DimensionalSoulArtifact"));
            recipe.AddIngredient(calamity.ItemType("ThePack"));
            recipe.AddIngredient(calamity.ItemType("Onyxia"));
            recipe.AddIngredient(calamity.ItemType("PrimordialAncient"));
            recipe.AddIngredient(calamity.ItemType("DevilsDevastation"));
            recipe.AddIngredient(calamity.ItemType("StarfleetMK2"));

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
