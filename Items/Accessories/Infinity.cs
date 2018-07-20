using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ID.ItemID;

namespace FargowiltasSouls.Items.Accessories
{
    public class Infinity : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Relic");
            Tooltip.SetDefault("'Is it really worth it?'\n" +
                                "You consume no ammo, mana, or consumables \n" +
                                "There is of course a catch (^:");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //nothing used son
            player.GetModPlayer<FargoPlayer>(mod).Infinity = true;
            player.manaCost -= 1f;

            //the price
            player.AddBuff(mod.BuffType("InfinityDebuff"), 7200, false);

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(EndlessQuiver);
            recipe.AddIngredient(HolyArrow, 999);
            recipe.AddIngredient(VenomArrow, 999);
            recipe.AddIngredient(EndlessMusketPouch);
            recipe.AddIngredient(PartyBullet, 999);
            recipe.AddIngredient(ChlorophyteBullet, 999);
            recipe.AddIngredient(RocketIII, 999);
            recipe.AddIngredient(CrystalDart, 999);
            recipe.AddIngredient(ManaCrystal, 10);
            recipe.AddIngredient(LifeCrystal, 10);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

