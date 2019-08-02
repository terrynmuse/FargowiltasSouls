using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Forces.Thorium
{
    public class JotunheimForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Jotunheim");
            Tooltip.SetDefault(
@"'A bitter cold, the power of the Jotuns...'
Allows you to breathe underwater
Grants the ability to swim and quicker movement in water
Critical strikes release a splash of foam, slowing nearby enemies
After four consecutive non-critical strikes, your next attack will mini-crit for 150% damage
Damage will duplicate itself for 33% of the damage and apply the Frozen debuff to hit enemies
An icy aura surrounds you, which freezes nearby enemies after a short delay
You occasionally birth a tentacle of abyssal energy that attacks nearby enemies
You can have up to six tentacles and their damage saps 1 life & mana from the hit enemy
Effects of Sea Breeze Pendant, Bubble Magnet, and Deep Dark Subwoofer
Effects of Goblin War Shield, Agnor's Bowl, and Ice Bound Strider Hide
Summons several pets");
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
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //tide hunter foam, yew crits, cryo duplicate
            modPlayer.JotunheimForce = true;

            //water bonuses
            if (player.breath <= player.breathMax + 2)
            {
                player.breath = player.breathMax + 3;
            }
            //sea breeze pendant
            player.accFlipper = true;
            if (player.wet || thoriumPlayer.drownedDoubloon)
            {
                player.AddBuff(thorium.BuffType("AquaticAptitude"), 60, true);
                player.GetModPlayer<FargoPlayer>().AllDamageUp(.1f);
            }
            //bubble magnet
            thoriumPlayer.bubbleMagnet = true;
            //quicker in water
            player.ignoreWater = true;
            if (player.wet)
            {
                player.moveSpeed += 0.15f;
            }

            //depth woofer
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerGouge = true;
                }
            }
            modPlayer.DepthEnchant = true;
            modPlayer.AddPet("Jellyfish Pet", hideVisual, thorium.BuffType("JellyPet"), thorium.ProjectileType("JellyfishPet"));

            //tide hunter
            //angler bowl
            if (!hideVisual)
            {
                if (player.direction > 0 && Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(player.Center.X + 56f, player.Center.Y - 10f, 0f, 0f, thorium.ProjectileType("AnglerLight"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                if (player.direction < 0 && Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(player.Center.X - 56f, player.Center.Y - 10f, 0f, 0f, thorium.ProjectileType("AnglerLight"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
            }
            //goblin war shield
            if (player.velocity.X == 0f)
            {
                player.statDefense += 4;
                player.noKnockback = true;
            }

            //strider hide
            thoriumPlayer.frostBonusDamage = true;
            //pets
            modPlayer.IcyEnchant = true;
            modPlayer.AddPet("Penguin Pet", hideVisual, BuffID.BabyPenguin, ProjectileID.Penguin);
            modPlayer.AddPet("Owl Pet", hideVisual, thorium.BuffType("SnowyOwlBuff"), thorium.ProjectileType("SnowyOwlPet"));

            if (Soulcheck.GetValue("Icy Barrier"))
            {
                //icy set bonus
                thoriumPlayer.icySet = true;
                if (player.ownedProjectileCounts[thorium.ProjectileType("IcyAura")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("IcyAura"), 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            
            if (Soulcheck.GetValue("Whispering Tentacles"))
            {
                thoriumPlayer.whisperingSet = true;
                if (player.ownedProjectileCounts[thorium.ProjectileType("WhisperingTentacle")] + player.ownedProjectileCounts[thorium.ProjectileType("WhisperingTentacle2")] < 6 && player.ownedProjectileCounts[thorium.ProjectileType("WhisperingTentacleSpawn")] < 1)
                {
                    timer++;
                    if (timer > 30)
                    {
                        Projectile.NewProjectile(player.Center.X + (float)Main.rand.Next(-300, 300), player.Center.Y, 0f, 0f, thorium.ProjectileType("WhisperingTentacleSpawn"), 50, 0f, player.whoAmI, 0f, 0f);
                        timer = 0;
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "DepthDiverEnchant");
            recipe.AddIngredient(null, "TideHunterEnchant");
            recipe.AddIngredient(null, "NagaSkinEnchant");
            recipe.AddIngredient(null, "CryoMagusEnchant");
            recipe.AddIngredient(null, "WhisperingEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
