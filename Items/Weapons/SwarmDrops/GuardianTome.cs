using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class GuardianTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
            Tooltip.SetDefault("'It's their turn to run'");
        }

        public override void SetDefaults()
        {
            item.damage = 999;
            item.magic = true;
            item.width = 24;
            item.height = 28;
            item.useTime = 50;
            item.useAnimation = 50;
            item.useStyle = 5;
            item.useTurn = true;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 70);
            item.rare = 11; //
            item.mana = 100; //           
            item.UseSound = SoundID.Item21; //
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("DungeonGuardian");
            item.shootSpeed = 18f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.BoneKey, 100);
            recipe.AddIngredient(mod.ItemType("Sadism"), 15);

            recipe.AddTile(mod, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}