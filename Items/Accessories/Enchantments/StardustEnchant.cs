using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class StardustEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stardust Enchantment");
			Tooltip.SetDefault(
@"'The power of the Stand is yours' 
Double tap down to direct your guardian
When you do, you freeze time temporarily
There is a longer cooldown for this effect, a sound effect plays when it's back");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
			item.rare = 10; 
			item.value = 400000; 
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			modPlayer.StardustEnchant = true;
            modPlayer.AddPet("Stardust Guardian", BuffID.StardustGuardianMinion, ProjectileID.StardustGuardian);

            player.setStardust = true;

            if ((player.controlDown && player.releaseDown))
            {
                if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15 && modPlayer.FreezeCD == 0) 
                {
                    modPlayer.FreezeTime = true;
                    modPlayer.FreezeCD = 1800;

                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/ZaWarudo").WithVolume(1f).WithPitchVariance(.5f), player.Center);
                }   
			}
        }

        public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StardustHelmet);
			recipe.AddIngredient(ItemID.StardustBreastplate);
			recipe.AddIngredient(ItemID.StardustLeggings);
			recipe.AddIngredient(ItemID.StardustCellStaff);
			recipe.AddIngredient(ItemID.StardustDragonStaff);
			recipe.AddIngredient(ItemID.RainbowCrystalStaff);
			recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}	
}