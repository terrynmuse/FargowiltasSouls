using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Wings)]
    public class FlightMasterySoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flight Mastery Soul");
            Tooltip.SetDefault(
@"'Ascend'
Acts as wings
Allows for very long lasting flight
Releases bees when damaged");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 750000;
            item.expert = true;
            item.rare = -12;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            player.ignoreWater = true;
            player.wingTimeMax = 2000;

            //honey
            player.noFallDmg = true;
            player.jumpBoost = true;
            player.bee = true;

        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 18f;
            acceleration *= 3.5f;
        }

        /*public override void WingUpdate(Player player, bool inUse)
    {
    	if (inUse)
    		Dust.NewDust(player.position, player.width, player.height, 107, 0, 0, 0, Color.Green);
    	base.WingUpdate(player, inUse);
    }*/   //add when you have actual wings

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AngelWings);
            recipe.AddIngredient(ItemID.HarpyWings);
            recipe.AddIngredient(ItemID.BoneWings);
            recipe.AddIngredient(ItemID.LeafWings);
            recipe.AddIngredient(ItemID.FrozenWings);
            recipe.AddIngredient(ItemID.FlameWings);
            recipe.AddIngredient(ItemID.TatteredFairyWings);
            recipe.AddIngredient(ItemID.FestiveWings);
            recipe.AddIngredient(ItemID.BetsyWings);
            recipe.AddIngredient(ItemID.FishronWings);
            recipe.AddIngredient(ItemID.WingsStardust);
            recipe.AddIngredient(ItemID.WingsVortex);
            recipe.AddIngredient(ItemID.WingsNebula);
            recipe.AddIngredient(ItemID.WingsSolar);

            recipe.AddTile(TileID.LunarCraftingStation);
            //wings.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}