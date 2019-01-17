using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ChlorophyteEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chlorophyte Enchantment");

            string tooltip = 
@"'The jungle's essence crystallizes above you'
Summons a leaf crystal to shoot at nearby enemies
Chance to steal 4 mana with each attack
Taking damage will release a poisoning spore explosion
";

            if(thorium != null)
            {
                //bulb stuff
                //Your magic damage has a chance to poison hit enemies with a spore cloud
                //Enemies that you poison or envenom will take additional damage over time
                //When out of combat for 5 seconds, life recovery will increase up to 3 over time

                tooltip +=
@"You and nearby allies have a chance to poison enemies when attacking
Doubles the range of your empowerments effect radius
";
            }
            /*else
            {*/
                tooltip += "Allows the collection of Vine Rope from vines\n";
            //}

            tooltip +=
@"Flowers grow on the grass you walk on
All herb collection is doubled
Summons a pet Seedling";

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //crystal and pet
            modPlayer.ChloroEffect(hideVisual, 100);
            //herb double
            modPlayer.ChloroEnchant = true;
            modPlayer.FlowerBoots();
            modPlayer.JungleEffect();

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //subwoofer
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerPoison = true;
                }
            }
            /*
            //bulb set bonus
            thoriumPlayer.bulbSpore = true;
            //petal shield
            if (thoriumPlayer.outOfCombat)
            {
                timer++;
                if (timer >= 900)
                {
                    thoriumPlayer.lifeRecovery += 3;
                    timer = 900;
                    return;
                }
                if (timer >= 600)
                {
                    thoriumPlayer.lifeRecovery += 2;
                    return;
                }
                if (timer >= 300)
                {
                    thoriumPlayer.lifeRecovery++;
                    return;
                }
            }
            else
            {
                timer = 0;
            }
            //night shade petal
            thoriumPlayer.nightshadeBoost = true;*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyChloroHead");
            recipe.AddIngredient(ItemID.ChlorophytePlateMail);
            recipe.AddIngredient(ItemID.ChlorophyteGreaves);
            recipe.AddIngredient(null, "JungleEnchant");
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {
                //recipe.AddIngredient(null, "BulbEnchant");
                recipe.AddIngredient(ItemID.FlowerBoots);
                recipe.AddIngredient(ItemID.StaffofRegrowth);
                recipe.AddIngredient(ItemID.LeafBlower);
                recipe.AddIngredient(thorium.ItemType("ChlorophyteButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.FlowerBoots);
                recipe.AddIngredient(ItemID.StaffofRegrowth);
            }
            
            recipe.AddIngredient(ItemID.Seedling);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
