using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class MeteorEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteor Enchantment");
            Tooltip.SetDefault(
                @"'Cosmic power builds your magical prowess'
A meteor shower initiates every few seconds while using magic weapons");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).MeteorEffect(50);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteorHelmet);
            recipe.AddIngredient(ItemID.MeteorSuit);
            recipe.AddIngredient(ItemID.MeteorLeggings);
            recipe.AddIngredient(ItemID.SpaceGun);
            recipe.AddIngredient(ItemID.StarCannon);

            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("CometCrossfire"));
                recipe.AddIngredient(ItemID.MeteorStaff);
                recipe.AddIngredient(ItemID.PlaceAbovetheClouds);
                recipe.AddIngredient(thorium.ItemType("MeteorButterfly"));
                recipe.AddIngredient(thorium.ItemType("BioPod"));
            }
            else
            {
                recipe.AddIngredient(ItemID.MeteorStaff);
                recipe.AddIngredient(ItemID.PlaceAbovetheClouds);
            }
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
