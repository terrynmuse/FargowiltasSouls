using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Forces.Thorium
{
    public class SvartalfheimForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Svartalfheim");
            Tooltip.SetDefault(
@"'Behold the craftsmanship of the Dark Elves...'
10% increased damage and damage reduction
Immune to intense heat
Attacks have a chance to cause a lightning bolt to strike
Grants the ability to dash into the enemy
Right Click to guard with your shield
A meteor shower initiates every few seconds while attacking
Moving around generates up to 5 static rings, then a bubble of energy will protect you from one attack
Effects of Eye of the Storm, Energized Subwoofer, and Spartan's Subwoofer
Effects of Champion's Rebuttal, Incandescent Spark, and Spiked Bracers
Effects of the Greedy Magnet, Mask of the Crystal Eye, and Abyssal Shell
Summons a pet Omega, I.F.O., and Bio-Feeder");
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
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //includes bronze lightning
            modPlayer.SvartalfheimForce = true;
            //granite
            player.fireWalk = true;
            player.lavaImmune = true;
            player.buffImmune[24] = true;

            if (Soulcheck.GetValue("Eye of the Storm"))
            {
                //eye of the storm
                thorium.GetItem("EyeoftheStorm").UpdateAccessory(player, hideVisual);
            }
            
            //woofers
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerGranite = true;
                    thoriumPlayer.empowerMarble = true;
                }
            }

            //bronze
            //rebuttal
            thoriumPlayer.championShield = true;

            //durasteel
            mod.GetItem("DurasteelEnchant").UpdateAccessory(player, hideVisual);
            thoriumPlayer.thoriumEndurance -= 0.02f; //meme way to make it 10%

            //titan
            modPlayer.AllDamageUp(.1f);
            //crystal eye mask
            thoriumPlayer.critDamage += 0.1f;
            //abyssal shell
            thoriumPlayer.AbyssalShell = true;

            //conduit
            mod.GetItem("ConduitEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "GraniteEnchant");
            recipe.AddIngredient(null, "BronzeEnchant");
            recipe.AddIngredient(null, "DurasteelEnchant");
            recipe.AddIngredient(null, "TitanEnchant");
            recipe.AddIngredient(null, "ConduitEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
