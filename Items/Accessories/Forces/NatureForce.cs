using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class NatureForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Nature");
            Tooltip.SetDefault(
@"'Tapped into every secret of the wilds'
Greatly increases life regen
Hearts heal for 1.5x as much
Allows the collection of Vine Rope from vines
Chance to steal 5 mana with each attack
Taking damage will release a poisoning spore explosion
Nearby enemies are ignited
When you die, you violently explode dealing massive damage to surrounding enemies
Icicles will start to appear around you
When there are three, using any weapon will launch them towards the cursor, Chilling and Frostburning enemies
Allows the ability to walk on water
Summons a leaf crystal to shoot at nearby enemies
Flowers grow on the grass you walk on and all herb collection is doubled
Not moving puts you in stealth, while in stealth crits deal 4x damage and spores spawn on enemies
Summons a Baby Face Monster, Crimson Heart, Baby Penguin, Snowman, Seedling, and Baby Truffle");
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
            player.crimsonRegen = true;
            //increase heart heal
            modPlayer.CrimsonEnchant = true;
            player.cordage = true;
            modPlayer.JungleEnchant = true;
            modPlayer.InfernoEffect(20);
            //explode on death
            modPlayer.MoltenEnchant = true;
            player.waterWalk = true;
            //icicles
            modPlayer.FrostEnchant = true;
            modPlayer.FrostEffect(80);
            modPlayer.AddMinion("Leaf Crystal", mod.ProjectileType("Chlorofuck"), 100, 10f);
            modPlayer.FlowerBoots();
            //herb double
            modPlayer.ChloroEnchant = true;
            //stealth and spores
            modPlayer.ShroomiteEffect();
            modPlayer.AddPet("Baby Face Monster Pet", hideVisual, BuffID.BabyFaceMonster, ProjectileID.BabyFaceMonster);
            modPlayer.AddPet("Crimson Heart Pet", hideVisual, BuffID.CrimsonHeart, ProjectileID.CrimsonHeart);
            modPlayer.AddPet("Baby Penguin Pet", hideVisual, BuffID.BabyPenguin, ProjectileID.Penguin);
            modPlayer.AddPet("Baby Snowman Pet", hideVisual, BuffID.BabySnowman, ProjectileID.BabySnowman);
            modPlayer.AddPet("Seedling Pet", hideVisual, BuffID.PetSapling, ProjectileID.Sapling);
            modPlayer.AddPet("Truffle Pet", hideVisual, BuffID.BabyTruffle, ProjectileID.Truffle);
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
            {
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            }
            else
            {
                recipe.AddTile(TileID.LunarCraftingStation);
            }

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}