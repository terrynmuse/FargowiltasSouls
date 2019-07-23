using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CalamityMod;
using System;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class SilvaEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public int dragonTimer = 60;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silva Enchantment");
            Tooltip.SetDefault(
@"'Boundless life energy cascades from you...'
You are immune to almost all debuffs
Reduces all damage taken by 5%, this is calculated separately from damage reduction
All projectiles spawn healing leaf orbs on enemy hits
Max run speed and acceleration boosted by 5%
If you are reduced to 0 HP you will not die from any further damage for 10 seconds
If you get reduced to 0 HP again while this effect is active you will lose 100 max life
This effect only triggers once per life
Your max life will return to normal if you die
True melee strikes have a 25% chance to do five times damage
Melee projectiles have a 25% chance to stun enemies for a very brief moment
Increases your rate of fire with all ranged weapons
Magic projectiles have a 10% chance to cause a massive explosion on enemy hits
Summons an ancient leaf prism to blast your enemies with life energy
Rogue weapons have a faster throwing rate while you are above 90% life
Effects of the Godly Soul Artifact and Yharim's Gift");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(108, 45, 199));
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
            item.value = 20000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);

            if (Soulcheck.GetValue("Silva Effects"))
            {
                modPlayer.silvaSet = true;
                //melee
                modPlayer.silvaMelee = true;
                //range
                modPlayer.silvaRanged = true;
                //magic
                modPlayer.silvaMage = true;
                //throw
                modPlayer.silvaThrowing = true;
            }
            

            if (Soulcheck.GetValue("Silva Crystal Minion"))
            {
                //summon
                modPlayer.silvaSummon = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("SilvaCrystal")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("SilvaCrystal"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.ProjectileType("SilvaCrystal")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("SilvaCrystal"), (int)(1500.0 * (double)player.minionDamage), 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

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
                            int num = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, calamity.ProjectileType("DragonDust"), 350, 5f, player.whoAmI, 0f, 0f);
                            Main.projectile[num].timeLeft = 60;
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
                        float num2 = player.position.X + Main.rand.Next(-400, 400);
                        float num3 = player.position.Y - Main.rand.Next(500, 800);
                        Vector2 vector = new Vector2(num2, num3);
                        float num4 = player.position.X + (player.width / 2) - vector.X;
                        float num5 = player.position.Y + (player.height / 2) - vector.Y;
                        num4 += Main.rand.Next(-100, 101);
                        int num6 = 22;
                        float num7 = (float)Math.Sqrt((num4 * num4 + num5 * num5));
                        num7 = num6 / num7;
                        num4 *= num7;
                        num5 *= num7;
                        int num8 = Projectile.NewProjectile(num2, num3, num4, num5, calamity.ProjectileType("SkyFlareFriendly"), 750, 9f, player.whoAmI, 0f, 0f);
                        Main.projectile[num8].ai[1] = player.position.Y;
                        Main.projectile[num8].hostile = false;
                        Main.projectile[num8].friendly = true;
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("SilvaHelm"));
            recipe.AddIngredient(calamity.ItemType("SilvaHornedHelm"));
            recipe.AddIngredient(calamity.ItemType("SilvaMaskedCap"));
            recipe.AddIngredient(calamity.ItemType("SilvaHelmet"));
            recipe.AddIngredient(calamity.ItemType("SilvaMask"));
            recipe.AddIngredient(calamity.ItemType("SilvaArmor"));
            recipe.AddIngredient(calamity.ItemType("SilvaLeggings"));
            recipe.AddIngredient(calamity.ItemType("GodlySoulArtifact"));
            recipe.AddIngredient(calamity.ItemType("YharimsGift"));
            recipe.AddIngredient(calamity.ItemType("LightGodsBrilliance"));
            recipe.AddIngredient(calamity.ItemType("AlphaRay"));
            recipe.AddIngredient(calamity.ItemType("ScourgeoftheCosmos"));
            recipe.AddIngredient(calamity.ItemType("Climax"));
            recipe.AddIngredient(calamity.ItemType("YharimsCrystal"));

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
