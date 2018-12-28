using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ForbiddenEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forbidden Enchantment");

            string tooltip =
@"'Walk like an Egyptian'
Double tap down to call an ancient storm to the cursor location
Any projectiles shot through your storm gain double pierce and 50% damage
";

            /*if(thorium != null)
            {
                tooltip +=
                    @"Your healing streak fuels an ancient crucible every 100 life up to 500 total
                    The crucible increases your radiant damage by 5% per 100 life in the crucible";
            }*/

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).ForbiddenEffect();

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            thoriumPlayer.karmicHolder = true;
            if (thoriumPlayer.healStreak >= 0 && player.ownedProjectileCounts[thorium.ProjectileType("KarmicHolderPro")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("KarmicHolderPro"), 0, 0f, player.whoAmI, 0f, 0f);
            }*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AncientBattleArmorHat);
            recipe.AddIngredient(ItemID.AncientBattleArmorShirt);
            recipe.AddIngredient(ItemID.AncientBattleArmorPants);
            
            /*if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.FlyingCarpet);
                recipe.AddIngredient(thorium.ItemType("KarmicHolder"));
                recipe.AddIngredient(thorium.ItemType("WhisperRa"));
                recipe.AddIngredient(ItemID.SpiritFlame);
                recipe.AddIngredient(ItemID.BookStaff);
            }
            else
            {*/
                recipe.AddIngredient(ItemID.SpiritFlame);
                recipe.AddIngredient(ItemID.BookStaff);
            //}
            
            recipe.AddIngredient(ItemID.Scorpion);
            recipe.AddIngredient(ItemID.SecretoftheSands);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
