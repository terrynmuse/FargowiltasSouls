using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Forces.Thorium
{
    public class VanaheimForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Vanaheim");
            Tooltip.SetDefault(
@"'Holds a glimpse of the future...'
Projects a mystical barrier around you
Every seventh attack will unleash damaging mana bolts
Critical strikes engulf enemies in a long lasting void flame and unleash ivory flares
Pressing the 'Special Ability' key will summon an incredibly powerful aura around your cursor
Creating this aura costs 150 mana
Each unique empowerment you have grants you:
8% increased damage, 
3% increased movement speed
Effects of Mana-Charged Rocketeers and Ascension Statuette");
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
            //includes malignant debuff, folv bolts, white dwarf flares
            modPlayer.VanaheimForce = true;

            if (Soulcheck.GetValue("Folv's Aura"))
            {
                //folv
                thoriumPlayer.folvSet = true;
                Lighting.AddLight(player.position, 0.03f, 0.3f, 0.5f);
                thoriumPlayer.folvBonus2 = true;
            }

            if (Soulcheck.GetValue("Mana-Charged Rocketeers"))
            {
                //mana charge rockets
                player.manaRegen++;
                player.manaRegenDelay -= 2;
                if (player.statMana > 0)
                {
                    player.rocketBoots = 1;
                    if (player.rocketFrame)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            player.statMana -= 2;
                            Dust.NewDust(new Vector2(player.position.X, player.position.Y + 20f), player.width, player.height, 15, player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 100, default(Color), 1.5f);
                        }
                        player.rocketTime = 1;
                    }
                }
            }
            
            if (Soulcheck.GetValue("Celestial Aura"))
            {
                //celestial
                thoriumPlayer.celestialSet = true;
            }

            if (Soulcheck.GetValue("Ascension Statuette"))
            {
                //ascension statue
                thoriumPlayer.ascension = true;
            }
            
            //balladeer meme hell
            if (thoriumPlayer.empowerDamage > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerAttackSpeed > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerCriticalStrike > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerMovementSpeed > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerInspirationRegen > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerDamageReduction > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerManaRegen > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerMaxMana > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerLifeRegen > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerMaxLife > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerDefense > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerAmmoConsumption > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "FolvEnchant");
            recipe.AddIngredient(null, "WhiteDwarfEnchant");
            recipe.AddIngredient(null, "CelestialEnchant");
            recipe.AddIngredient(null, "BalladeerEnchant");

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
