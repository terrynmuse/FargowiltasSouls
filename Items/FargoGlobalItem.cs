using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

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
                    break;
                case ItemID.TwinsBossBag:
                    player.QuickSpawnItem(mod.ItemType("TwinBoomerangs"));
                    break;
                case ItemID.SkeletronPrimeBossBag:
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
            if (item.type == ItemID.PumpkinPie && player.HasBuff(BuffID.PotionSickness)) return false;

            if (item.magic && player.GetModPlayer<FargoPlayer>().ReverseManaFlow) return false;

            return true;
        }

        public override bool UseItem(Item item, Player player)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();

            if (item.type == ItemID.PumpkinPie && modPlayer.PumpkinEnchant && !modPlayer.TerrariaSoul)
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

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            //thorium//

            /*if (modPlayer.ShadowForce && item.damage >= 1 && Main.rand.Next(5) == 0)
            {
                float num9 = 0.25f;
                float num10 = (float)Math.Sqrt((speedX * speedX + speedY * speedY));
                double num11 = Math.Atan2(speedX, speedY);
                double num12 = num11 + (0.25f * num9);
                double num13 = num11 - (0.25f * num9);
                float num14 = Utils.NextFloat(Main.rand) * 0.2f + 0.95f;
                Projectile.NewProjectile(position.X, position.Y, num10 * num14 * (float)Math.Sin(num12), num10 * num14 * (float)Math.Cos(num12), thorium.ProjectileType("BlightDagger"), damage, knockBack, player.whoAmI, 0f, 0f);
                Projectile.NewProjectile(position.X, position.Y, num10 * num14 * (float)Math.Sin(num13), num10 * num14 * (float)Math.Cos(num13), thorium.ProjectileType("BlightDagger"), damage, knockBack, player.whoAmI, 0f, 0f);
            }*/

            return true;
        }
    }
}