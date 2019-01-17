using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class EarthForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;
        public bool effect;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Earth");

            string tooltip = "'Gaia's blessing shines upon you'\n";

            //if(thorium == null)
            //{
                tooltip +=
@"25% chance for your projectiles to explode into shards
Greatly increases life regeneration after striking an enemy 
One attack gains 5% life steal every second, capped at 5 HP
Flower petals will cause extra damage to your target 
Spawns 3 fireballs to rotate around you
Every 8th projectile you shoot will split into 3
Any secondary projectiles may also split
Any damage you take while at full HP is reduced by 90%
Briefly become invulnerable after striking an enemy";
            /*}
            else
            {
                tooltip +=
@"25% chance for your projectiles to explode into shards
Greatly increases life regeneration after striking an enemy 
One attack gains 33% life steal every 10 seconds, capped at 100 HP
Flower petals will cause extra damage to your target 
Spawns 3 fireballs to rotate around you
Every 8th projectile you shoot will split into 3
Any secondary projectiles may also split
Any damage you take while at full HP is reduced by 90%
Briefly become invulnerable after striking an enemy
Pressing the 'Encase' key will place you in an impenetrable shell
When out of combat for 5 seconds, your next attack will generate a 25 life shield
The energy of Terraria seeks to protect you
Summons a pet Pink Slime";
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
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //shards
            modPlayer.CobaltEnchant = true;
            //regen on hit, heals
            modPlayer.PalladiumEffect();
            //fireballs and petals
            modPlayer.OrichalcumEffect();
            //split
            modPlayer.AdamantiteEnchant = true;
            //shadow dodge, full hp resistance
            modPlayer.TitaniumEffect();

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //abyssal shell
            thoriumPlayer.AbyssalShell = true;
            //astro beetle husk
            if (thoriumPlayer.outOfCombat)
            {
                thoriumPlayer.astroBeetle = true;
                if (!effect)
                {
                    float num = 25f;
                    int num2 = 0;
                    while (num2 < num)
                    {
                        Vector2 vector = Vector2.UnitX * 0f;
                        vector += -Utils.RotatedBy(Vector2.UnitY, (num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(20f, 5f);
                        vector = Utils.RotatedBy(vector, 0.0, default(Vector2));
                        int num3 = Dust.NewDust(player.Center, 0, 0, 173, 0f, 0f, 0, default(Color), 1.15f);
                        Main.dust[num3].noGravity = true;
                        Main.dust[num3].position = player.Center + vector;
                        Main.dust[num3].velocity = player.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                        int num4 = num2;
                        num2 = num4 + 1;
                    }
                    effect = true;
                    return;
                }
            }
            else
            {
                effect = false;
            }
            //slime pet
            modPlayer.AddPet("Pink Slime Pet", hideVisual, thorium.BuffType("PinkSlimeBuff"), thorium.ProjectileType("PinkSlime"));
            modPlayer.IllumiteEnchant = true;
            //terrarium set bonus
            timer++;
            if (timer > 60)
            {
                Projectile.NewProjectile(player.Center.X + 14f, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraRed"), 50, 0f, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(player.Center.X + 9f, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraOrange"), 50, 0f, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(player.Center.X + 4f, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraYellow"), 50, 0f, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(player.Center.X, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraGreen"), 50, 0f, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(player.Center.X - 4f, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraBlue"), 50, 0f, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(player.Center.X - 9f, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraIndigo"), 50, 0f, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(player.Center.X - 14f, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraPurple"), 50, 0f, Main.myPlayer, 0f, 0f);
                timer = 0;
            }*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            /*if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "CobaltEnchant");
                recipe.AddIngredient(null, "PalladiumEnchant");
                recipe.AddIngredient(null, "MythrilEnchant");
                recipe.AddIngredient(null, "OrichalcumEnchant");
                recipe.AddIngredient(null, "AdamantiteEnchant");
                recipe.AddIngredient(null, "TitanEnchant");
                recipe.AddIngredient(null, "ValadiumEnchant");
                recipe.AddIngredient(null, "LodestoneEnchant");
                recipe.AddIngredient(null, "IllumiteEnchant");
                recipe.AddIngredient(null, "TerrariumEnchant");
            }
            else
            {*/
                recipe.AddIngredient(null, "CobaltEnchant");
                recipe.AddIngredient(null, "PalladiumEnchant");
                recipe.AddIngredient(null, "MythrilEnchant");
                recipe.AddIngredient(null, "OrichalcumEnchant");
                recipe.AddIngredient(null, "AdamantiteEnchant");
                recipe.AddIngredient(null, "TitaniumEnchant");
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