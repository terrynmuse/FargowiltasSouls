using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Forces.Thorium
{
    public class HelheimForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Helheim");
            Tooltip.SetDefault(
@"'From the halls of Hel, a vision of the end...'
Your boots vibrate at an unreal frequency, increasing movement speed significantly
While moving, all damage is increased
Your attacks have a chance to unleash an explosion of Dragon's Flame
Consecutive attacks against enemies might drop flesh, which grants bonus life and damage
While above 75% maximum life, you become unstable
Your plague gas will linger in the air twice as long and your plague reactions will deal 20% more damage
Killing an enemy will release a soul fragment
Effects of Crash Boots, Dragon Talon Necklace, and Grim Subwoofer
Effects of Vampire Gland, Demon Blood Badge, and Blood Demon's Subwoofer 
Effects of Shade Band, Lich's Gaze, and Plague Lord's Flask
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
            //plague lord flask effect
            modPlayer.HelheimForce = true;

            if (Soulcheck.GetValue("Dread Speed"))
            {
                //dread
                player.moveSpeed += 0.8f;
                player.maxRunSpeed += 10f;
                player.runAcceleration += 0.05f;
                if (player.velocity.X > 0f || player.velocity.X < 0f)
                {
                    modPlayer.AllDamageUp(.25f);

                    for (int i = 0; i < 2; i++)
                    {
                        int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 65, 0f, 0f, 0, default(Color), 1.75f);
                        int num2 = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 75, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num].noGravity = true;
                        Main.dust[num2].noGravity = true;
                        Main.dust[num].noLight = true;
                        Main.dust[num2].noLight = true;
                    }
                }
            }

            //crash boots
            thorium.GetItem("CrashBoots").UpdateAccessory(player, hideVisual);
            player.moveSpeed -= 0.15f;
            player.maxRunSpeed -= 1f;
            //woofers
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerCursed = true;
                    thoriumPlayer.empowerIchor = true;
                }
            }
            if (Soulcheck.GetValue("Dragon Flames"))
            {
                //dragon 
                thoriumPlayer.dragonSet = true;
            }
            //dragon tooth necklace
            player.armorPenetration += 15;
            //wyvern pet
            modPlayer.AddPet("Wyvern Pet", hideVisual, thorium.BuffType("WyvernPetBuff"), thorium.ProjectileType("WyvernPet"));
            thoriumPlayer.wyvernPet = true;

            //demon blood
            thoriumPlayer.demonbloodSet = true;
            //demon blood badge
            thoriumPlayer.CrimsonBadge = true;
            if (Soulcheck.GetValue("Flesh Drops"))
            {
                //flesh set bonus
                thoriumPlayer.Symbiotic = true;
            }
            if (Soulcheck.GetValue("Vampire Gland"))
            {
                //vampire gland
                thoriumPlayer.vampireGland = true;
            }
            //blister pet
            modPlayer.AddPet("Blister Pet", hideVisual, thorium.BuffType("BlisterBuff"), thorium.ProjectileType("BlisterPet"));
            thoriumPlayer.blisterPet = true;

            if (Soulcheck.GetValue("Harbinger Overcharge"))
            {
                //harbinger
                if (player.statLife > (int)(player.statLifeMax2 * 0.75))
                {
                    thoriumPlayer.overCharge = true;
                    modPlayer.AllDamageUp(.5f);
                }
            }
            //shade band
            thoriumPlayer.shadeBand = true;
            //pet
            modPlayer.AddPet("Moogle Pet", hideVisual, thorium.BuffType("LilMogBuff"), thorium.ProjectileType("LilMog"));
            modPlayer.KnightEnchant = true;

            //plague doctor
            thoriumPlayer.plagueSet = true;
            //lich gaze
            thoriumPlayer.lichGaze = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "DreadEnchant");
            recipe.AddIngredient(null, "DemonBloodEnchant");
            recipe.AddIngredient(null, "HarbingerEnchant");
            recipe.AddIngredient(null, "PlagueDoctorEnchant");

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
