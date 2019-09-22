using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class CyclonicFin : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cyclonic Fin");
            Tooltip.SetDefault(@"'The wind is howling'
Grants immunity to Curse of the Moon and Oceanic Maul
Your attacks inflict Curse of the Moon and Oceanic Maul
Spectral Fishron periodically manifests to support your critical hits
Spectral Fishron also inflicts Mutant Nibble, disabling enemy healing
Upgrades Cute Fishron to Cute Fishron EX");
            DisplayName.AddTranslation(GameCulture.Chinese, "飓风鳍");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'风在咆哮'
免疫月之诅咒和海洋重击
攻击造成月之诅咒和海洋重击
暴击时显现幽灵猪鲨
幽灵猪鲨会造成突变啃啄,阻止敌人再生
将超可爱猪鲨升级为超可爱猪鲨 EX");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 11;
            item.value = Item.sellPrice(0, 17);
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(Main.DiscoR, 51, 255 - (int)(Main.DiscoR * 0.4));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("OceanicMaul")] = true;
            player.buffImmune[mod.BuffType("CurseoftheMoon")] = true;
            player.GetModPlayer<FargoPlayer>().CyclonicFin = true;
            if (player.GetModPlayer<FargoPlayer>().CyclonicFinCD > 0)
                player.GetModPlayer<FargoPlayer>().CyclonicFinCD--;
            if (player.mount.Active && player.mount.Type == MountID.CuteFishron)
            {
                if (player.ownedProjectileCounts[mod.ProjectileType("CuteFishronRitual")] < 1 && player.whoAmI == Main.myPlayer)
                    Projectile.NewProjectile(player.MountedCenter, Vector2.Zero, mod.ProjectileType("CuteFishronRitual"), 0, 0f, Main.myPlayer);
                player.MountFishronSpecialCounter = 300;
                player.meleeDamage += 0.15f;
                player.rangedDamage += 0.15f;
                player.magicDamage += 0.15f;
                player.minionDamage += 0.15f;
                player.thrownDamage += 0.15f;
                player.meleeCrit += 30;
                player.rangedCrit += 30;
                player.magicCrit += 30;
                player.thrownCrit += 30;
                player.statDefense += 30;
                player.lifeRegen += 3;
                player.lifeRegenCount += 3;
                player.lifeRegenTime += 3;
                if (player.controlLeft == player.controlRight)
                {
                    if (player.velocity.X != 0)
                        player.velocity.X -= player.mount.Acceleration * Math.Sign(player.velocity.X);
                    if (player.velocity.X != 0)
                        player.velocity.X -= player.mount.Acceleration * Math.Sign(player.velocity.X);
                }
                else if (player.controlLeft)
                {
                    player.velocity.X -= player.mount.Acceleration * 4f;
                    if (player.velocity.X < -16f)
                        player.velocity.X = -16f;
                    if (!player.controlUseItem)
                        player.direction = -1;
                }
                else if (player.controlRight)
                {
                    player.velocity.X += player.mount.Acceleration * 4f;
                    if (player.velocity.X > 16f)
                        player.velocity.X = 16f;
                    if (!player.controlUseItem)
                        player.direction = 1;
                }
                if (player.controlUp == player.controlDown)
                {
                    if (player.velocity.Y != 0)
                        player.velocity.Y -= player.mount.Acceleration * Math.Sign(player.velocity.Y);
                    if (player.velocity.Y != 0)
                        player.velocity.Y -= player.mount.Acceleration * Math.Sign(player.velocity.Y);
                }
                else if (player.controlUp)
                {
                    player.velocity.Y -= player.mount.Acceleration * 4f;
                    if (player.velocity.Y < -16f)
                        player.velocity.Y = -16f;
                }
                else if (player.controlDown)
                {
                    player.velocity.Y += player.mount.Acceleration * 4f;
                    if (player.velocity.Y > 16f)
                        player.velocity.Y = 16f;
                }
            }
        }
    }
}
