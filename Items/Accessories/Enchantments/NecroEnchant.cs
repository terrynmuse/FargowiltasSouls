using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class NecroEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necro Enchantment");
            Tooltip.SetDefault(
@"'Welcome to the bone zone' 
A Dungeon Guardian will occasionally annihilate a foe when struck by any attack
Summons a pet Skeletron Head");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).NecroEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NecroHelmet);
            recipe.AddIngredient(ItemID.NecroBreastplate);
            recipe.AddIngredient(ItemID.NecroGreaves);
            recipe.AddIngredient(ItemID.BoneSword);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("Slugger"));
                recipe.AddIngredient(thorium.ItemType("MarrowScepter"));
                recipe.AddIngredient(thorium.ItemType("BoneFlayerTail"));
                recipe.AddIngredient(ItemID.TheGuardiansGaze);
                recipe.AddIngredient(thorium.ItemType("BoneButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.TheGuardiansGaze);
            }
            
            recipe.AddIngredient(ItemID.BoneKey);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
