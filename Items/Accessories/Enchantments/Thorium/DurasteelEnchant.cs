using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using System;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class DurasteelEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Durasteel Enchantment");
            Tooltip.SetDefault(
@"'Masterfully forged by the Blacksmith'
12% damage reduction
Grants the ability to dash into the enemy
Right Click to guard with your shield
Effects of the Ogre Sandals, Spiked Bracers, and Greedy Magnet");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 80000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //durasteel set bonus
            thoriumPlayer.thoriumEndurance += 0.12f;
            //ogre sandals
            if (player.velocity.Y > 0f && thoriumPlayer.falling < 120)
            {
                thoriumPlayer.falling += 3;
            }
            if (player.velocity.Y < 0f)
            {
                thoriumPlayer.falling = 0;
            }
            if (player.velocity.Y == 0f && Collision.SolidCollision(player.position, player.width, player.height + 4) && thoriumPlayer.falling > 50)
            {
                if (thoriumPlayer.falling >= 100)
                {
                    Main.PlaySound(SoundID.Item70, player.position);
                    Main.PlaySound(SoundID.Item69, player.position);
                    float num = 16f;
                    int num2 = 0;
                    while (num2 < num)
                    {
                        Vector2 vector = Vector2.UnitX * 0f;
                        vector += -Utils.RotatedBy(Vector2.UnitY, (num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(20f, 5f);
                        vector = Utils.RotatedBy(vector, Utils.ToRotation(player.velocity), default(Vector2));
                        int num3 = Dust.NewDust(player.Center, 0, 0, 0, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num3].scale = 1.35f;
                        Main.dust[num3].noGravity = true;
                        Main.dust[num3].position = player.Center + vector;
                        Dust dust = Main.dust[num3];
                        dust.position.Y = dust.position.Y + 12f;
                        Main.dust[num3].velocity = player.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                        int num4 = num2;
                        num2 = num4 + 1;
                    }
                }
                Main.PlaySound(SoundID.Item69, player.position);
                float num5 = 6f + 0.05f * thoriumPlayer.falling;
                int num6 = (int)(50.0 + thoriumPlayer.falling * 0.25);
                Projectile.NewProjectile(player.Center.X, player.Center.Y + 8f, 5f + thoriumPlayer.falling * 0.035f, 0f, thorium.ProjectileType("CrashSurge"), num6, num5, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(player.Center.X, player.Center.Y + 8f, -5f - thoriumPlayer.falling * 0.035f, 0f, thorium.ProjectileType("CrashSurge"), num6, num5, Main.myPlayer, 0f, 0f);
                thoriumPlayer.falling = 0;
            }
            //greedy magnet
            for (int i = 0; i < 400; i++)
            {
                if (Main.item[i].active && Main.item[i].noGrabDelay == 0 && Vector2.Distance(player.Center, Main.item[i].position) < 700f)
                {
                    Main.item[i].beingGrabbed = true;
                    float num = 10f;
                    Vector2 vector = new Vector2(Main.item[i].position.X + (Main.item[i].width / 2), Main.item[i].position.Y + (Main.item[i].height / 2));
                    float num2 = player.Center.X - vector.X;
                    float num3 = player.Center.Y - vector.Y;
                    float num4 = (float)Math.Sqrt((num2 * num2 + num3 * num3));
                    num4 = num / num4;
                    num2 *= num4;
                    num3 *= num4;
                    int num5 = 5;
                    Main.item[i].velocity.X = (Main.item[i].velocity.X * (num5 - 1) + num2) / num5;
                    Main.item[i].velocity.Y = (Main.item[i].velocity.Y * (num5 - 1) + num3) / num5;
                }
            }
            //EoC Shield
            player.dash = 2;
            //spiked bracers
            player.thorns += 0.25f;
            //iron shield raise
            player.GetModPlayer<FargoPlayer>(mod).IronEffect();
        }
        
        private readonly string[] items =
        {
            "OgreSandal",
            "GreedyMagnet",
            "DurasteelRepeater",
            "SpudBomber",
            "ThiefDagger",
            "SeaMine"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(thorium.ItemType("DurasteelHelmet"));
            recipe.AddIngredient(thorium.ItemType("DurasteelChestplate"));
            recipe.AddIngredient(thorium.ItemType("DurasteelGreaves"));
            recipe.AddIngredient(null, "DarksteelEnchant");
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
