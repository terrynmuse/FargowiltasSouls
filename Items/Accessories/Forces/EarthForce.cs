using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class EarthForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Earth");
            Tooltip.SetDefault("'Gaia's blessing shines upon you'\n" +
                                "30% increased weapon use speed\n" +
                                "Enemies will explode into cobalt shards on death\n" +
                                "Greatly increases life regeneration after striking an enemy\n" +
                                "Very small chance for an attack to gain 33% life steal\n" +
                                "Chance for a fireball to spew from a hit enemy\n" +
                                "20% chance to shoot multiple projectiles with single shot magic weapons \n" +
                                "Briefly become invulnerable after striking an enemy\n" +
                                "While a dodge is active, damage is increased by 25%\n" +
                                "While on cooldown, damage is decreased by 15%\n" +
                                "Shows the location of enemies, traps, and treasures\n" +
                                "Other effects of material enchantments");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 300000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            //miner
            player.pickSpeed -= 0.3f;
            if (Soulcheck.GetValue("Shine Buff"))
            {
                Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);
            }
            if (Soulcheck.GetValue("Spelunker Buff"))
            {
                player.findTreasure = true;
            }
            if (Soulcheck.GetValue("Hunter Buff"))
            {
                player.detectCreature = true;
            }
            if (Soulcheck.GetValue("Dangersense Buff"))
            {
                player.dangerSense = true;
            }
            //cobalt
            modPlayer.CobaltEnchant = true;

            //palladium
            player.onHitRegen = true;
            modPlayer.PalladEnchant = true;

            //mythril	
            if (Soulcheck.GetValue("Increase Use Speed"))
            {
                modPlayer.FiringSpeed += .3f;
                modPlayer.CastingSpeed += .3f;
                modPlayer.ThrowingSpeed += .3f;
                modPlayer.RadiantSpeed += .3f;
                modPlayer.SymphonicSpeed += .3f;
                modPlayer.HealingSpeed += .3f;
                modPlayer.AxeSpeed += .3f;
                modPlayer.HammerSpeed += .3f;
                modPlayer.PickSpeed += .3f;
            }

            //orichalcum
            if (Soulcheck.GetValue("Orichalcum Fireball"))
            {
                player.onHitPetal = true;
                modPlayer.OriEnchant = true;
            }
            //adamantite
            if (Soulcheck.GetValue("Splitting Projectiles"))
            {
                modPlayer.AdamantiteEnchant = true;
            }

            //titanium
            player.onHitDodge = true;
            player.kbBuff = true;

            if (player.FindBuffIndex(59) == -1)
            {
                player.magicDamage -= .15f;
                player.meleeDamage -= .15f;
                player.rangedDamage -= .15f;
                player.minionDamage -= .15f;
                player.thrownDamage -= .15f;
            }
            else
            {
                player.magicDamage += .25f;
                player.meleeDamage += .25f;
                player.rangedDamage += .25f;
                player.minionDamage += .25f;
                player.thrownDamage += .25f;
            }

            if (player.whoAmI == Main.myPlayer)
            {
                //if(Soulcheck.GetValue("Baby Face Monster Pet"))
                //{
                modPlayer.LanternPet = true;

                if (player.FindBuffIndex(152) == -1)
                {
                    if (player.ownedProjectileCounts[ProjectileID.MagicLantern] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.MagicLantern, 0, 2f, Main.myPlayer);
                    }
                }
                //}
                //else
                //{
                //	modPlayer.lanternPet = false;
                //}
            }

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MinerEnchant");
            recipe.AddIngredient(null, "CobaltEnchant");
            recipe.AddIngredient(null, "PalladiumEnchant");
            recipe.AddIngredient(null, "MythrilEnchant");
            recipe.AddIngredient(null, "OrichalcumEnchant");
            recipe.AddIngredient(null, "AdamantiteEnchant");
            recipe.AddIngredient(null, "TitaniumEnchant");


            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}


