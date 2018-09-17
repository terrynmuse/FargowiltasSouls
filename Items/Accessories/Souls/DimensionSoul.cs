using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Wings)]
    public class DimensionSoul : ModItem
    {
        private readonly Mod _calamity = ModLoader.GetMod("CalamityMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Dimensions");


            Tooltip.SetDefault("'The dimensions of Terraria are at your fingertips'"
                               + "\nDoes various things");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 18));
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
            item.defense = 12;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.value = 1500000;
            item.rare = -12;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).DimensionSoul = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "ColossusSoul");
            recipe.AddIngredient(null, "SupersonicSoul");
            recipe.AddIngredient(null, "FlightMasterySoul");
            recipe.AddIngredient(null, "WorldShaperSoul");
            recipe.AddIngredient(null, "TrawlerSoul");

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
