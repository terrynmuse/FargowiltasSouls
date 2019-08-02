using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Items.CalamityCustomThrowingDamage;
using System;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class AuricEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Auric Tesla Enchantment");
            Tooltip.SetDefault(
@"'Your strength rivals that of the Jungle Tyrant...'
All effects from Tarragon, Bloodflare, Godslayer and Silva armor
Not moving boosts all damage and critical strike chance
Attacks have a 2% chance to do no damage to you
You will freeze enemies near you when you are struck
All attacks spawn healing auric orbs
You have a magic carpet");
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

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);

            if (Soulcheck.GetValue("Auric Tesla Effects"))
            {
                //legs
                player.carpet = true;
                //body
                modPlayer.fBarrier = true;
                modPlayer.godSlayerReflect = true;
                //all heads
                modPlayer.tarraSet = true;
                modPlayer.bloodflareSet = true;
                modPlayer.godSlayer = true;
                modPlayer.silvaSet = true;
                modPlayer.auricSet = true;
                modPlayer.auricBoost = true;
                player.lavaImmune = true;
                player.ignoreWater = true;
                //melee head
                modPlayer.tarraMelee = true;
                modPlayer.bloodflareMelee = true;
                modPlayer.godSlayerDamage = true;
                modPlayer.silvaMelee = true;
                //range head
                modPlayer.tarraRanged = true;
                modPlayer.bloodflareRanged = true;
                modPlayer.godSlayerRanged = true;
                modPlayer.silvaRanged = true;
                //magic head
                modPlayer.tarraMage = true;
                modPlayer.bloodflareMage = true;
                modPlayer.godSlayerMage = true;
                modPlayer.silvaMage = true;
                //throw head
                modPlayer.tarraThrowing = true;
                modPlayer.bloodflareThrowing = true;
                modPlayer.godSlayerThrowing = true;
                modPlayer.silvaThrowing = true;
            }

            //summon head
            modPlayer.tarraSummon = true;
            if (Soulcheck.GetValue("Polterghast Mines"))
            {
                modPlayer.bloodflareSummon = true;
            }

            if (player.whoAmI == Main.myPlayer)
            {
                if (Soulcheck.GetValue("Silva Crystal Minion"))
                {
                    modPlayer.silvaSummon = true;
                    if (player.FindBuffIndex(calamity.BuffType("SilvaCrystal")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("SilvaCrystal"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.ProjectileType("SilvaCrystal")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("SilvaCrystal"), (int)(3000.0 * (double)player.minionDamage), 0f, Main.myPlayer, 0f, 0f);
                    }
                }

                if (Soulcheck.GetValue("Mechworm Minion"))
                {
                    modPlayer.godSlayerSummon = true;
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
                            return;
                        }
                        if (num10 != -1 && num11 != -1)
                        {
                            int num15 = Projectile.NewProjectile(value.X, value.Y, num7, num8, calamity.ProjectileType("MechwormBody"), num6, 1f, whoAmI, Main.projectile[num11].ai[0], 0f);
                            int num16 = Projectile.NewProjectile(value.X, value.Y, num7, num8, calamity.ProjectileType("MechwormBody2"), num6, 1f, whoAmI, (float)num15, 0f);
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
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("AuricTeslaHelm"));
            recipe.AddIngredient(calamity.ItemType("AuricTeslaHoodedFacemask"));
            recipe.AddIngredient(calamity.ItemType("AuricTeslaWireHemmedVisage"));
            recipe.AddIngredient(calamity.ItemType("AuricTeslaSpaceHelmet"));
            recipe.AddIngredient(calamity.ItemType("AuricTeslaPlumedHelm"));
            recipe.AddIngredient(calamity.ItemType("AuricTeslaBodyArmor"));
            recipe.AddIngredient(calamity.ItemType("AuricTeslaCuisses"));
            recipe.AddIngredient(calamity.ItemType("DraedonsExoblade"));
            recipe.AddIngredient(calamity.ItemType("ArkoftheCosmos"));
            recipe.AddIngredient(calamity.ItemType("Drataliornus"));
            recipe.AddIngredient(calamity.ItemType("Photoviscerator"));
            recipe.AddIngredient(calamity.ItemType("VividClarity"));
            recipe.AddIngredient(calamity.ItemType("CosmicImmaterializer"));
            recipe.AddIngredient(calamity.ItemType("Celestus"));

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
