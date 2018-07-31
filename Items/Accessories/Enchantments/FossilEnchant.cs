using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class FossilEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fossil Enchantment");
			Tooltip.SetDefault(
@"'Beyond a forgotten age'
If you reach zero HP you cheat death, returning with 20 HP
For a few seconds after reviving, you are immune to all damage and spawn bones everywhere
Summons a pet Baby Dino");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;			
			ItemID.Sets.ItemNoGravity[item.type] = true;
			item.rare = 1; 
			item.value = 20000; 
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			modPlayer.FossilEnchant = true;
            modPlayer.AddPet("Baby Dino Pet", BuffID.BabyDinosaur, ProjectileID.BabyDino);

            if(modPlayer.FossilBones)
            {
                
            }
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FossilHelm);
            recipe.AddIngredient(ItemID.FossilShirt);
			recipe.AddIngredient(ItemID.FossilPants);
			recipe.AddIngredient(ItemID.BoneJavelin, 100);
			recipe.AddIngredient(ItemID.AntlionMandible);
			recipe.AddIngredient(ItemID.AmberMosquito);
			recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}