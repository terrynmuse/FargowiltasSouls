using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class CrimsonEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crimson Enchantment");
			Tooltip.SetDefault(
@"'The blood of your enemy is your rebirth'
Greatly increases life regen
Hearts heal for 1.5x as much
Attacks have a chance to inflict ichor
Summons a Baby Face Monster and a Crimson Heart");
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
            player.crimsonRegen = true;

            modPlayer.CrimsonEnchant = true;
            modPlayer.AddPet("Baby Face Monster Pet", BuffID.BabyFaceMonster, ProjectileID.BabyFaceMonster);
            modPlayer.AddPet("Crimson Heart Pet", BuffID.CrimsonHeart, ProjectileID.CrimsonHeart);
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimsonHelmet);
			recipe.AddIngredient(ItemID.CrimsonScalemail);
			recipe.AddIngredient(ItemID.CrimsonGreaves);
			recipe.AddIngredient(ItemID.DeadlandComesAlive);
			recipe.AddIngredient(ItemID.BoneRattle);
			recipe.AddIngredient(ItemID.CrimsonHeart);
			recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}
		
	





