using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
	public class Blender : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Weapons/BossDrops/Dicer";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Blender");
			Tooltip.SetDefault("'The reward for slaughtering many..'");
			
			ItemID.Sets.Yoyo[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.useStyle = 5;
			item.width = 24;
			item.height = 24;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("BlenderProj");
			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 2.5f;
			item.damage = 124;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = 10;
		}

		public override void AddRecipes()
		{
            if (Fargowiltas.Instance.FargosLoaded)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "Dicer");
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("EnergizerPlant"));
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
		}
	}
}
