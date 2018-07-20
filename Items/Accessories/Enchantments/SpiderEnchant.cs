using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class SpiderEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spider Enchantment");
            Tooltip.SetDefault("'Arachniphobia is punishable by arachnid induced death' \n" +
                                "10% increased minion damage \n" +
                                "Summon damage causes venom\n" +
                                "When an enemy dies, it may drop spider eggs\n" +
                                "Summons a pet Spider");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 4;
            item.value = 30000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.SpiderEnchant = true;

            player.minionDamage += 0.1f;

            //pet
            if (player.whoAmI == Main.myPlayer)
            {
                if (Soulcheck.GetValue("Spider Pet"))
                {
                    modPlayer.SpiderPet = true;

                    if (player.FindBuffIndex(81) == -1)
                    {
                        if (player.ownedProjectileCounts[ProjectileID.Spider] < 1)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.Spider, 0, 2f, Main.myPlayer);
                        }
                    }
                }
                else
                {
                    modPlayer.SpiderPet = false;
                }
            }


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpiderMask);
            recipe.AddIngredient(ItemID.SpiderBreastplate);
            recipe.AddIngredient(ItemID.SpiderGreaves);
            recipe.AddIngredient(ItemID.SpiderStaff);
            recipe.AddIngredient(ItemID.QueenSpiderStaff);
            recipe.AddIngredient(ItemID.SpiderEgg);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

