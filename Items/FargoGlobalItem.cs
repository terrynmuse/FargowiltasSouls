using System;
using System.Collections.Generic;
using FargowiltasSouls.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items;

namespace FargowiltasSouls.Items
{
    public class FargoGlobalItem : GlobalItem
    {
        private static Mod thorium = ModLoader.GetMod("ThoriumMod");

        /*public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.Stinger)
            {
                item.ammo = item.type;
            }
        }*/

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.DogWhistle)
            {
                TooltipLine line = new TooltipLine(mod, "fun", "Shoutout to Browny and Paca");
                tooltips.Add(line);
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (player.manaCost <= 0f) player.manaCost = 0f;
        }

        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            FargoPlayer p = (FargoPlayer) player.GetModPlayer(mod, "FargoPlayer");
            //ignore money, hearts, mana stars
            if (p.IronEnchant && item.type != 71 && item.type != 72 && item.type != 73 && item.type != 74 && item.type != 54 && item.type != 1734 && item.type != 1735 &&
                item.type != 184 && item.type != ItemID.CandyCane && item.type != ItemID.SugarPlum) grabRange += 250;
        }

        public override void PickAmmo(Item item, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            FargoPlayer modPlayer = (FargoPlayer) player.GetModPlayer(mod, "FargoPlayer");

            if (modPlayer.Jammed) type = ProjectileID.ConfettiGun;
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            FargoPlayer p = player.GetModPlayer<FargoPlayer>(mod);

            if (p.Infinity && item.createTile == -1 && item.type != ItemID.LifeFruit) return false;

            if (p.BuilderMode && (item.createTile != -1 || item.createWall != -1)) return false;
            return true;
        }

        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (Main.rand.Next(10) != 0) return;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (arg)
            {
                case ItemID.KingSlimeBossBag:
                    player.QuickSpawnItem(mod.ItemType("SlimeKingsSlasher"));
                    break;
                case ItemID.EyeOfCthulhuBossBag:
                    player.QuickSpawnItem(mod.ItemType("EyeFlail"));
                    break;
                case ItemID.EaterOfWorldsBossBag:
                    player.QuickSpawnItem(mod.ItemType("EaterStaff"));
                    break;
                case ItemID.BrainOfCthulhuBossBag:
                    player.QuickSpawnItem(mod.ItemType("BrainStaff"));
                    break;
                case ItemID.SkeletronBossBag:
                    player.QuickSpawnItem(mod.ItemType("Bonezone"));
                    break;
                case ItemID.QueenBeeBossBag:
                    player.QuickSpawnItem(mod.ItemType("HiveStaff"));
                    break;
                case ItemID.DestroyerBossBag:
                    player.QuickSpawnItem(mod.ItemType("DestroyerGun"));
                    break;
                case ItemID.TwinsBossBag:
                    player.QuickSpawnItem(mod.ItemType("TwinRangs"));
                    break;
                /*case ItemID.SkeletronPrimeBossBag:
                    player.QuickSpawnItem(mod.ItemType("DarkStarCannon"));
                    break;*/
                case ItemID.PlanteraBossBag:
                    player.QuickSpawnItem(mod.ItemType("Dicer"));
                    break;
                case ItemID.GolemBossBag:
                    player.QuickSpawnItem(mod.ItemType("GolemTome"));
                    break;
                case ItemID.FishronBossBag:
                    player.QuickSpawnItem(mod.ItemType("FishStick"));
                    break;
            }
        }

        public override bool OnPickup(Item item, Player player)
        {
            FargoPlayer p = player.GetModPlayer<FargoPlayer>(mod);

            if (p.ChloroEnchant && item.stack == 1 && (item.type == ItemID.Daybloom || item.type == ItemID.Blinkroot || item.type == ItemID.Deathweed || item.type == ItemID.Fireblossom ||
                                                       item.type == ItemID.Moonglow || item.type == ItemID.Shiverthorn || item.type == ItemID.Waterleaf || item.type == ItemID.Mushroom ||
                                                       item.type == ItemID.VileMushroom || item.type == ItemID.ViciousMushroom || item.type == ItemID.GlowingMushroom)) item.stack = 2;

            if (p.TerrariaSoul)
            {
                switch (item.type)
                {
                    case ItemID.Star:
                        player.ManaEffect(200);
                        player.statMana += 200;
                        return false;
                }
            }

            return true;
        }

        public override void GetWeaponKnockback(Item item, Player player, ref float knockback)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();

            if (modPlayer.UniverseEffect || modPlayer.Eternity) knockback *= 2;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();

            if (item.type == ItemID.PumpkinPie && player.statLife != player.statLifeMax2 && player.HasBuff(BuffID.PotionSickness)) return false;

            if (item.magic && player.GetModPlayer<FargoPlayer>().ReverseManaFlow)
            {
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " was destroyed by their own magic."), (item.mana * 4) + item.damage, 0);
                player.immune = false;
                player.immuneTime = 0;
            }

            if (modPlayer.Infinity && !modPlayer.Eternity && (item.useAmmo != AmmoID.None || item.mana > 0 || item.consumable))
            {
                modPlayer.InfinityCounter++;

                if (modPlayer.InfinityCounter >= 4)
                {
                    modPlayer.InfinityHurt();
                }
            }

            //non weapons and weapons with no ammo begone
            if (item.damage <= 0 || !player.HasAmmo(item, true) || (item.mana > 0 && player.statMana < item.mana)) return true;

            if (modPlayer.BorealEnchant && ++modPlayer.BorealCount >= 4)
            {
                modPlayer.BorealCount = 0;
                if (Soulcheck.GetValue("Boreal Snowball Support"))
                {
                    Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * item.shootSpeed * .75f;
                    int p = Projectile.NewProjectile(player.Center, velocity, ProjectileID.SnowBallFriendly, (int)(item.damage * .5f), 1, Main.myPlayer);
                    if (p != 1000 && (player.ZoneSnow || modPlayer.WoodForce))
                        FargoGlobalProjectile.SplitProj(Main.projectile[p], 5);
                }
            }

            if (modPlayer.AdditionalAttacks && modPlayer.AdditionalAttacksTimer <= 0)
            {
                modPlayer.AdditionalAttacksTimer = 60;

                Vector2 position = player.Center;
                Vector2 velocity = Vector2.Normalize(Main.MouseWorld - position);

                if (modPlayer.CelestialRune && Soulcheck.GetValue("Celestial Rune Support"))
                {
                    if (item.melee) //fireball
                    {
                        Main.PlaySound(SoundID.Item34, position);
                        for (int i = 0; i < 3; i++)
                        {
                            Projectile.NewProjectile(position, velocity.RotatedByRandom(Math.PI / 6) * Main.rand.NextFloat(6f, 10f),
                                mod.ProjectileType("CelestialRuneFireball"), (int)(50f * player.meleeDamage), 9f, player.whoAmI);
                        }
                    }
                    if (item.ranged) //lightning
                    {
                        float ai1 = Main.rand.Next(100);
                        Vector2 vel = Vector2.Normalize(velocity.RotatedByRandom(Math.PI / 4)) * 7f;
                        Projectile.NewProjectile(position, vel, mod.ProjectileType("CelestialRuneLightningArc"),
                            (int)(50f * player.rangedDamage), 1f, player.whoAmI, velocity.ToRotation(), ai1);
                    }
                    if (item.magic) //ice mist
                    {
                        Projectile.NewProjectile(position, velocity * 4.25f, mod.ProjectileType("CelestialRuneIceMist"), (int)(50f * player.magicDamage), 4f, player.whoAmI);
                    }
                    if (item.thrown) //ancient vision
                    {
                        Projectile.NewProjectile(position, velocity * 16f, mod.ProjectileType("CelestialRuneAncientVision"), (int)(50f * player.thrownDamage), 0, player.whoAmI);
                    }
                }

                if (modPlayer.PumpkingsCape && Soulcheck.GetValue("Pumpking's Cape Support"))
                {
                    if (item.melee) //flaming jack
                    {
                        float distance = 2000f;
                        int target = -1;
                        for (int i = 0; i < 200; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].CanBeChasedBy())
                            {
                                float newDist = Main.npc[i].Distance(player.Center);
                                if (newDist < distance)
                                {
                                    distance = newDist;
                                    target = i;
                                }
                            }
                        }
                        if (target != -1)
                            Projectile.NewProjectile(position, velocity * 8f, ProjectileID.FlamingJack, (int)(75f * player.meleeDamage), 7.5f, player.whoAmI, target, 0f);
                    }
                    if (item.ranged) //jack o lantern
                    {
                        Projectile.NewProjectile(position, velocity * 11f, ProjectileID.JackOLantern, (int)(95f * player.rangedDamage), 8f, player.whoAmI);
                    }
                    if (item.magic) //bat scepter
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 newVel = velocity * 10f;
                            newVel.X += Main.rand.Next(-35, 36) * 0.02f;
                            newVel.Y += Main.rand.Next(-35, 36) * 0.02f;
                            Projectile.NewProjectile(position, newVel, ProjectileID.Bat, (int)(45f * player.magicDamage), 3f, player.whoAmI);
                        }
                    }
                }
            }

            if (Fargowiltas.Instance.ThoriumLoaded) ThoriumCanUse(player, item);

            return true;
        }

        private void ThoriumCanUse(Player player, Item item)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            if (Soulcheck.GetValue("Illumite Rocket"))
            {
                //illumite effect
                if (modPlayer.MidgardForce)
                {
                    thoriumPlayer.rocketsFired++;
                    if (thoriumPlayer.rocketsFired >= 3)
                    {
                        Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * item.shootSpeed * 1.5f;

                        Projectile.NewProjectile(player.Center, velocity, thorium.ProjectileType("IllumiteMissile"), item.damage, item.knockBack, player.whoAmI, 0f, 0f);
                        thoriumPlayer.rocketsFired = 0;
                        Main.PlaySound(SoundID.Item14, player.position);
                    }
                }
            }

            if (Soulcheck.GetValue("Plague Lord's Flask"))
            {
                //plague flask
                if (modPlayer.HelheimForce && item.damage >= 1 && Main.rand.Next(5) == 0)
                {
                    Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * item.shootSpeed;

                    float num9 = 0.25f;
                    float num10 = (float)Math.Sqrt((velocity.X * velocity.X + velocity.Y * velocity.Y));
                    double num11 = Math.Atan2(velocity.X, velocity.Y);
                    double num12 = num11 + (0.25f * num9);
                    double num13 = num11 - (0.25f * num9);
                    float num14 = Utils.NextFloat(Main.rand) * 0.2f + 0.95f;
                    Projectile.NewProjectile(player.Center, new Vector2(num10 * num14 * (float)Math.Sin(num12), num10 * num14 * (float)Math.Cos(num12)), thorium.ProjectileType("BlightDagger"), item.damage, (int)item.knockBack, player.whoAmI);
                    Projectile.NewProjectile(player.Center, new Vector2(num10 * num14 * (float)Math.Sin(num13), num10 * num14 * (float)Math.Cos(num13)), thorium.ProjectileType("BlightDagger"), item.damage, (int)item.knockBack, player.whoAmI);
                }
            }
            
            //folv effect
            if (modPlayer.VanaheimForce && Soulcheck.GetValue("Folv's Bolts"))
            {
                thoriumPlayer.magicCast++;
                if (thoriumPlayer.magicCast >= 7)
                {
                    Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * item.shootSpeed;

                    Vector2 value;
                    value.X = Main.MouseWorld.X;
                    value.Y = Main.MouseWorld.Y;
                    Vector2 value2 = value - player.Center;
                    float num = 10f;
                    float num2 = (float)Math.Sqrt((value2.X * value2.X + value2.Y * value2.Y));
                    if (num2 > num)
                    {
                        num2 = num / num2;
                    }
                    value2 *= num2;
                    float num3 = 0.25f;
                    double num4 = Math.Atan2((double)velocity.X, (double)velocity.Y);
                    double num5 = num4 + (double)(0.25f * num3);
                    double num6 = num4 - (double)(0.25f * num3);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, num * (float)Math.Sin(num5), num * (float)Math.Cos(num5), thorium.ProjectileType("OGBassBoosterPro2"), (int)(30f * (1f + 0.02f * (float)item.useAnimation)), 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, num * (float)Math.Sin(num6), num * (float)Math.Cos(num6), thorium.ProjectileType("OGBassBoosterPro2"), (int)(30f * (1f + 0.02f * (float)item.useAnimation)), 2f, player.whoAmI, 0f, 0f);
                    Main.PlaySound(SoundID.Item82, player.position);

                    thoriumPlayer.magicCast = 0;
                }
            }
        }

        public override bool UseItem(Item item, Player player)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();

            if (item.type == ItemID.PumpkinPie && player.statLife != player.statLifeMax2 && modPlayer.PumpkinEnchant && !modPlayer.TerrariaSoul)
            {
                int heal = player.statLifeMax2 - player.statLife;
                player.HealEffect(heal);
                player.statLife += heal;
                player.AddBuff(BuffID.PotionSickness, 10800);
            }

            if (modPlayer.UniverseEffect && item.damage > 0) item.shootSpeed *= 1.5f;

            if (modPlayer.Eternity && item.damage > 0) item.shootSpeed *= 2f;

            return false;
        }

        public override bool NewPreReforge(Item item)
        {
            if (Main.player[item.owner].GetModPlayer<FargoPlayer>().SecurityWallet)
            {
                switch(item.prefix)
                {
                    case PrefixID.Warding:  if (Soulcheck.GetValue("Warding"))  return false; break;
                    case PrefixID.Violent:  if (Soulcheck.GetValue("Violent"))  return false; break;
                    case PrefixID.Quick:    if (Soulcheck.GetValue("Quick"))    return false; break;
                    case PrefixID.Lucky:    if (Soulcheck.GetValue("Lucky"))    return false; break;
                    case PrefixID.Menacing: if (Soulcheck.GetValue("Menacing")) return false; break;
                    case PrefixID.Legendary:if (Soulcheck.GetValue("Legendary"))return false; break;
                    case PrefixID.Unreal:   if (Soulcheck.GetValue("Unreal"))   return false; break;
                    case PrefixID.Mythical: if (Soulcheck.GetValue("Mythical")) return false; break;
                    case PrefixID.Godly:    if (Soulcheck.GetValue("Godly"))    return false; break;
                    case PrefixID.Demonic:  if (Soulcheck.GetValue("Demonic"))  return false; break;
                    case PrefixID.Ruthless: if (Soulcheck.GetValue("Ruthless")) return false; break;
                    case PrefixID.Light:    if (Soulcheck.GetValue("Light"))    return false; break;
                    case PrefixID.Deadly:   if (Soulcheck.GetValue("Deadly"))   return false; break;
                    case PrefixID.Rapid:    if (Soulcheck.GetValue("Rapid"))    return false; break;
                    default: break;
                }
            }
            return true;
        }
    }
}