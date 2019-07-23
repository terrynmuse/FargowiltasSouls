using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FargowiltasSouls.Items.Accessories.Forces.Thorium
{
    public class AsgardForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Asgard");
            Tooltip.SetDefault(
@"'What's this about Ragnarok?'
Produces a floating globule every half second
Every globule increases defense and makes your next attack a mini-crit
Attacks have a 20% chance to unleash aquatic homing daggers all around you
Attacks have a 10% chance to duplicate and become increased by 15%
Attacks have a 5% chance to instantly kill the enemy
Attacks will heavily burn and damage all adjacent enemies
Pressing the 'Special Ability' key will:
envelop you within an impervious bubble,
unleash an echo of Slag Fury's power,
place you within the Dream and bend the very fabric of reality,
grant you infinite inspiration and increased symphonic damage and playing speed,
overload all nearby allies with every empowerment III for 15 seconds
Summons a pet Maid");
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
            //includes tide turner daggers, assassin duplicate and insta kill, pyro burst
            modPlayer.AsgardForce = true;

            //tide turner
            if (Soulcheck.GetValue("Tide Turner Globules"))
            {
                //floating globs and defense
                thoriumPlayer.tideHelmet = true;
                if (thoriumPlayer.tideOrb < 8)
                {
                    timer++;
                    if (timer > 30)
                    {
                        float num = 30f;
                        int num2 = 0;
                        while (num2 < num)
                        {
                            Vector2 vector = Vector2.UnitX * 0f;
                            vector += -Utils.RotatedBy(Vector2.UnitY, (num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(25f, 25f);
                            vector = Utils.RotatedBy(vector, Utils.ToRotation(player.velocity), default(Vector2));
                            int num3 = Dust.NewDust(player.Center, 0, 0, 113, 0f, 0f, 0, default(Color), 1f);
                            Main.dust[num3].scale = 1.6f;
                            Main.dust[num3].noGravity = true;
                            Main.dust[num3].position = player.Center + vector;
                            Main.dust[num3].velocity = player.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                            int num4 = num2;
                            num2 = num4 + 1;
                        }
                        thoriumPlayer.tideOrb++;
                        timer = 0;
                    }
                }
            }
            
            //set bonus damage to healing hot key
            thoriumPlayer.tideSet = true;

            //pyro summon bonus
            thoriumPlayer.napalmSet = true;

            //dream weaver
            //all allies invuln hot key
            thoriumPlayer.dreamHoodSet = true;
            //enemies slowed and take more dmg hot key
            thoriumPlayer.dreamSet = true;
            //maid pet
            modPlayer.AddPet("Maid Pet", hideVisual, thorium.BuffType("MaidBuff"), thorium.ProjectileType("Maid1"));
            modPlayer.DreamEnchant = true;

            //rhapsodist
            //hotkey buff allies 
            thoriumPlayer.rallySet = true;
            //hotkey buff self
            thoriumPlayer.soloistSet = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "TideTurnerEnchant");
            recipe.AddIngredient(null, "AssassinEnchant");
            recipe.AddIngredient(null, "PyromancerEnchant");
            recipe.AddIngredient(null, "DreamWeaverEnchant");
            recipe.AddIngredient(null, "RhapsodistEnchant");
            recipe.AddIngredient(thorium.ItemType("BowofLight"));

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
