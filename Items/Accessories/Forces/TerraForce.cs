using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using System;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class TerraForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Force");
            string tooltip = "''\n";

            //if (thorium == null)
            //{
                tooltip +=
@"Attacks have a chance to shock enemies with lightning
Sets your critical strike chance to 10%
Every crit will increase it by 5%
Getting hit drops your crit back down
Allows the player to dash into the enemy
Right Click to guard with your shield
You attract items from a larger range
Attacks may inflict enemies with Lead Poisoning
Grants immunity to fire and lava";
                
            /*}
            else
            {
                tooltip +=
                @"
Sets your critical strike chance to 10%
Every crit will increase it by 5%
Getting hit drops your crit back down
Attacks may inflict enemies with Lead Poisoning





Attacks have a chance to shock enemies with lightning
Allows the player to dash into the enemy
Right Click to guard with your shield
You attract items from a larger range
Grants immunity to fire blocks and lava





While in combat, you generate a 25 life shield
12% damage reduction
Landing on solid ground releases a powerful shockwave
The damage, knockback, and range of the shock wave is increased by the fall distance
Grants the ability to dash into the enemy, knockback immunity and Ice Skates effect
Right Click to guard with your shield
Magnetizes all loose items on the screen
50% of the damage you take is also dealt to the attacker
";
            }*/

            

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
            item.shieldSlot = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //crit effect improved
            modPlayer.TerraForce = true;
            //lightning
            modPlayer.CopperEnchant = true;
            //crits
            modPlayer.TinEffect();
            //EoC Shield
            player.dash = 2;
            //shield
            modPlayer.IronEffect();
            //lead poison
            modPlayer.LeadEnchant = true;
            //lava immune (obsidian)
            player.fireWalk = true;
            player.lavaImmune = true;

            //if (!Fargowiltas.Instance.ThoriumLoaded)
            //{
            //magnet
            if (Soulcheck.GetValue("Iron Magnet"))
            {
                modPlayer.IronEnchant = true;
            }
            return;
            //}

            /*ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");






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
            //darksteel bonuses
            player.noKnockback = true;
            player.iceSkate = true;
            //EoC Shield
            player.dash = 2;
            //spiked bracers
            player.thorns += 0.5f;*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            /*if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "TinEnchant");
                recipe.AddIngredient(null, "LeadEnchant");
                recipe.AddIngredient(null, "TungstenEnchant");
                recipe.AddIngredient(null, "DangerEnchant");
                recipe.AddIngredient(null, "BronzeEnchant");
                recipe.AddIngredient(null, "GraniteEnchant");
                recipe.AddIngredient(null, "DurasteelEnchant");
            }
            else
            {*/
                recipe.AddIngredient(null, "CopperEnchant");
                recipe.AddIngredient(null, "TinEnchant");
                recipe.AddIngredient(null, "IronEnchant");
                recipe.AddIngredient(null, "LeadEnchant");
                recipe.AddIngredient(null, "TungstenEnchant");
                recipe.AddIngredient(null, "ObsidianEnchant");
            //}

            

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}