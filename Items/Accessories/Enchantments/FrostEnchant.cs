using FargowiltasSouls.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class FrostEnchant : ModItem
    {
        int icicleCD = 0;
        Projectile[] icicles = new Projectile[3];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Enchantment");
            Tooltip.SetDefault(
@"'Let's coat the world in a deep freeze' 
Icicles will start to appear around you
When there are three, using any weapon will launch them towards the cursor, Chilling enemies
Allows the ability to walk on water
Summons a baby penguin and snowman");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.FrostEnchant = true;
            player.waterWalk = true;
            modPlayer.AddPet("Baby Penguin Pet", BuffID.BabyPenguin, ProjectileID.Penguin);
            modPlayer.AddPet("Baby Snowman Pet", BuffID.BabySnowman, ProjectileID.BabySnowman);
            
            if(icicleCD == 0 && modPlayer.IcicleCount < 3)
            {
                Projectile p = Projectile.NewProjectileDirect(player.Center, Vector2.Zero, ProjectileID.Blizzard, 0, 0, player.whoAmI);
                p.GetGlobalProjectile<FargoGlobalProjectile>().Rotate = true;
                p.width = 10;
                p.height = 10;

                icicles[modPlayer.IcicleCount] = p;
                modPlayer.IcicleCount++;
                icicleCD = 120;
            }

            if(icicleCD != 0)
            {
                icicleCD--;
            }

            if(modPlayer.IcicleCount == 3 && player.controlUseItem && player.HeldItem.damage > 0)
            {
                for(int i = 0; i < icicles.Length; i++)
                {
                    Vector2 vel = (Main.MouseWorld - icicles[i].Center).SafeNormalize(-Vector2.UnitY) * 5;

                    Projectile.NewProjectile(icicles[i].Center, vel, icicles[i].type, 50, 1f, player.whoAmI);
                    icicles[i].Kill();
                }

                modPlayer.IcicleCount = 0;
                icicleCD = 300;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostHelmet);
            recipe.AddIngredient(ItemID.FrostBreastplate);
            recipe.AddIngredient(ItemID.FrostLeggings);
            recipe.AddIngredient(ItemID.IceBow);
            recipe.AddIngredient(ItemID.Fish);
            recipe.AddIngredient(ItemID.ToySled);
            recipe.AddIngredient(ItemID.ColdWatersintheWhiteLand);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}