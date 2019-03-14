using System;
using System.Collections.Generic;
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
                item.type != 184) grabRange += 250;
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
                    player.QuickSpawnItem(mod.ItemType("QueenStinger"));
                    break;
                /*case ItemID.DestroyerBossBag:
                    player.QuickSpawnItem(mod.ItemType("Probe"));
                    break;*/
                case ItemID.TwinsBossBag:
                    player.QuickSpawnItem(mod.ItemType("TwinRangs"));
                    break;
                /*case ItemID.SkeletronPrimeBossBag:
                    player.QuickSpawnItem(mod.ItemType("PrimeStaff"));
                    break;*/
                case ItemID.PlanteraBossBag:
                    player.QuickSpawnItem(mod.ItemType("Dicer"));
                    break;
                //case ItemID.GolemBossBag:
                    //player.QuickSpawnItem(mod.ItemType("GolemStaff"));
                    //break;
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
                    case ItemID.Heart:
                    case ItemID.CandyApple:
                    case ItemID.CandyCane:
                        player.HealEffect(40);
                        player.statLife += 40;
                        return false;
                    case ItemID.Star:
                        player.ManaEffect(200);
                        player.statMana += 200;
                        return false;
                }
            }
            else if (p.CrimsonEnchant && !p.NatureForce && (item.type == ItemID.Heart || item.type == ItemID.CandyApple || item.type == ItemID.CandyCane))
            {
                player.HealEffect(30);
                player.statLife += 30;
                return false;
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
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " self destructed."), (item.mana * 4) + item.damage, 0);
                player.immune = false;
            }

            if (modPlayer.Infinity && !modPlayer.Eternity && (item.useAmmo != AmmoID.None || item.mana > 0 || item.consumable))
            {
                modPlayer.InfinityCounter++;

                if (modPlayer.InfinityCounter >= 4)
                {
                    modPlayer.InfinityHurt();
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
            if (modPlayer.VanaheimForce)
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
    }
}