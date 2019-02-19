using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class BeeEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bee Enchantment");

            string tooltip = 
@"'According to all known laws of aviation, there is no way a bee should be able to fly'
Increases the strength of friendly bees
Bees ignore most enemy defense
";

            if(thorium != null)
            {
                tooltip += "Effects of Bee Booties\n";
            }

            tooltip += "Summons a pet Baby Hornet";

            Tooltip.SetDefault(tooltip);
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
            player.GetModPlayer<FargoPlayer>(mod).BeeEffect(hideVisual);
            
            if(Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            //bee booties
            if ((player.velocity.X > 1f && player.velocity.X > 0f) || (player.velocity.X < 1f && player.velocity.X < 0f))
            {
                timer++;
                if (timer > 45)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("BeeSummonSpawn"), 0, 0f, player.whoAmI, 0f, 0f);
                    timer = 0;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeeHeadgear);
            recipe.AddIngredient(ItemID.BeeBreastplate);
            recipe.AddIngredient(ItemID.BeeGreaves);
            recipe.AddIngredient(ItemID.HiveBackpack);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("BeeBoots"));
                recipe.AddIngredient(ItemID.BeeKeeper);
                recipe.AddIngredient(ItemID.BeeGun);
                recipe.AddIngredient(thorium.ItemType("HoneyRecorder"));
                recipe.AddIngredient(thorium.ItemType("SweetWingButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.BeeGun);
            }
            
            recipe.AddIngredient(ItemID.Nectar);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
