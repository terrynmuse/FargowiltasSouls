using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class NatureForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Nature");

            /*string tooltip =
@"'Tapped into every secret of the wilds'
Summons a leaf crystal to shoot at nearby enemies
Flowers grow on the grass you walk on
All herb collection is doubled
Summons a pet Seedling
Greatly increases life regen
Hearts heal for 1.5x as much
Summons a Baby Face Monster and a Crimson Heart
Icicles will start to appear around you
When there are three, attacking will launch them towards the cursor
Chance to steal 4 mana with each attack
Taking damage will release a poisoning spore explosion
Nearby enemies are ignited
When you die, you violently explode dealing massive damage to surrounding enemies
Not moving puts you in stealth
While in stealth crits deal 4x damage
Summons a pet Baby Truffle";

            if (thorium != null)
            {
                tooltip +=
@"Your symphonic damage empowers all nearby allies with: Cold Shoulder
Damage done against frostburnt enemies is increased by 8% 
Doubles the range of your empowerments effect radius
Your symphonic damage empowers all nearby allies with: Jungle's Nibble
Damage done against poisoned enemies is increased by 8%
Doubles the range of your empowerments effect radius";
            }

            tooltip += "Summons a pet Penguin and Snowman";

            Tooltip.SetDefault(tooltip);*/
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //crystal and pet
            modPlayer.ChloroEffect(hideVisual, 100);
            //herb double
            modPlayer.ChloroEnchant = true;
            modPlayer.FlowerBoots();
            //regen, hearts heal more, pets
            modPlayer.CrimsonEffect(hideVisual);
            //icicles, pets
            modPlayer.FrostEffect(75, hideVisual);
            //mana steal, spore, cordage
            modPlayer.JungleEffect();
            //inferno and explode
            modPlayer.MoltenEffect(20);
            //stealth, crits, pet
            modPlayer.ShroomiteEffect(hideVisual);

            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            thoriumPlayer.subwooferFrost = true;
            thoriumPlayer.bardRangeBoost += 450;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CrimsonEnchant");
            recipe.AddIngredient(null, "JungleEnchant");
            recipe.AddIngredient(null, "MoltenEnchant");
            recipe.AddIngredient(null, "FrostEnchant");
            recipe.AddIngredient(null, "ChlorophyteEnchant");
            recipe.AddIngredient(null, "ShroomiteEnchant");

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}