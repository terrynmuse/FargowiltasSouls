using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThoriumMod;
using System;
using System.Linq;
using CalamityMod;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class CalamitySoul : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public const int FireProjectiles = 2;
        public const float FireAngleSpread = 120f;
        public int FireCountdown;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("CalamityMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Tyrant");
            Tooltip.SetDefault(
@"''
Shade rains down when you are hit
You will confuse nearby enemies when you are struck
Drops brimstone fireballs from the sky occasionally
Brimstone fire rains down while invincibility is active\nImmunity to lava
Summons a fungal clump to fight for you
You leave behind poisonous seawater as you move
If you are damaged while submerged in liquid you will gain a damaging aura for a short time

You grow flowers on the grass beneath you, chance to grow very random dye plants on grassless dirt
Summons all waifus to protect you
Toggling the visibility of this accessory also toggles the waifus on and off
");

        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;//
            item.value = 100000000;//
            item.shieldSlot = 5;//
            item.defense = 50;//
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);
            mod.GetItem("AuricEnchant").UpdateAccessory(player, hideVisual);
            mod.GetItem("DemonShadeEnchant").UpdateAccessory(player, hideVisual);
            //THE AMALGAM
            modPlayer.aBrain = true;
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
            player.ignoreWater = true;
            player.lavaImmune = true;

            if ((player.velocity.X > 0.0 || player.velocity.Y > 0.0 || player.velocity.X < -0.1 || player.velocity.Y < -0.1) && player.whoAmI == Main.myPlayer)
            {
                int num = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, calamity.ProjectileType("PoisonousSeawater"), 500, 5f, player.whoAmI, 0f, 0f);
                Main.projectile[num].timeLeft = 10;
            }
            if (player.immune && Main.rand.Next(20) == 0 && player.whoAmI == Main.myPlayer)
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
                    int num8 = (Main.rand.Next(2) == 0) ? calamity.ProjectileType("AuraRain") : calamity.ProjectileType("StandingFire");
                    int num9 = Projectile.NewProjectile(num2, num3, num4, num5, num8, 500, 3f, player.whoAmI, 0f, 0f);
                    Main.projectile[num9].tileCollide = false;
                }
            }
            int num10 = 0;
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0f, 0.5f, 1.25f);
            int num11 = 70;
            float num12 = 200f;
            bool flag = num10 % 60 == 0;
            int num13 = 80;
            int num14 = Main.rand.Next(5);
            if (player.whoAmI == Main.myPlayer && num14 == 0 && player.immune && Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
            {
                for (int j = 0; j < 200; j++)
                {
                    NPC npc = Main.npc[j];
                    if (npc.active && !npc.friendly && npc.damage > 0 && !npc.dontTakeDamage && !npc.buffImmune[num11] && Vector2.Distance(player.Center, npc.Center) <= num12)
                    {
                        if (npc.FindBuffIndex(num11) == -1)
                        {
                            npc.AddBuff(num11, 300, false);
                        }
                        if (flag)
                        {
                            npc.StrikeNPC(num13, 0f, 0, false, false, false);
                            if (Main.netMode != 0)
                            {
                                NetMessage.SendData(28, -1, -1, null, j, num13, 0f, 0f, 0, 0, 0);
                            }
                        }
                    }
                }
            }
            num10++;
            if (num10 >= 180)
            {
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
            //HEART OF THE ELEMENTS
            modPlayer.allWaifus = !hideVisual;
            modPlayer.elementalHeart = true;
            if (!hideVisual)
            {
                int num = NPC.downedMoonlord ? 150 : 90;
                float num2 = CalamityWorld.downedDoG ? 2f : 1f;
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
            }
            else
            {
                if (player.ownedProjectileCounts[calamity.ProjectileType("BigBustyRose")] > 0 || player.ownedProjectileCounts[calamity.ProjectileType("SirenLure")] > 0 || player.ownedProjectileCounts[calamity.ProjectileType("DrewsSandyWaifu")] > 0 || player.ownedProjectileCounts[calamity.ProjectileType("SandyWaifu")] > 0 || player.ownedProjectileCounts[calamity.ProjectileType("CloudWaifu")] > 0)
                {
                    player.ClearBuff(calamity.BuffType("HotE"));
                }
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


        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);


            recipe.AddIngredient(null, "AuricEnchant");
            recipe.AddIngredient(null, "DemonShadeEnchant");

            recipe.AddIngredient(calamity.ItemType("TheAmalgam"));
            recipe.AddIngredient(calamity.ItemType("HeartoftheElements"));


            recipe.AddIngredient(calamity.ItemType("TheAmalgam"));
            recipe.AddIngredient(calamity.ItemType("TheAmalgam"));
            recipe.AddIngredient(calamity.ItemType("TheAmalgam"));

            /*
             * 





MOAB
celestial tracers
the community




Rampart of Deities
The Sponge
Asgardian Aegis
Ethereal Talisman
Elemental Gauntlet
Elemental Quiver
Statis' Belt of Curses
Nanotech
Supreme Bait Tackle Box Fishing Station
             * */

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
