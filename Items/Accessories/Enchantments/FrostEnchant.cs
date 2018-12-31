using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class FrostEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Enchantment");
            
            string tooltip = 
@"'Let's coat the world in a deep freeze' 
Icicles will start to appear around you
When there are three, attacking will launch them towards the cursor
";

            /*if(thorium != null)
            {
                tooltip +=
@"An icy aura surrounds you, which freezes nearby enemies after a short delay
Your symphonic damage empowers all nearby allies with: Cold Shoulder
Damage done against frostburnt enemies is increased by 8% 
Doubles the range of your empowerments effect radius
";
            }*/

            tooltip += "Summons a pet Penguin and Snowman";

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
            player.GetModPlayer<FargoPlayer>(mod).FrostEffect(50, hideVisual);

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.subwooferFrost = true;
            thoriumPlayer.bardRangeBoost += 450;

            //icy set bonus
            thoriumPlayer.icySet = true;
            if (player.ownedProjectileCounts[thorium.ProjectileType("IcyAura")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("IcyAura"), 0, 0f, player.whoAmI, 0f, 0f);
            }*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostHelmet);
            recipe.AddIngredient(ItemID.FrostBreastplate);
            recipe.AddIngredient(ItemID.FrostLeggings);
            
            /*if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "IcyEnchant");
                recipe.AddIngredient(thorium.ItemType("FrostSubwoofer"));
                recipe.AddIngredient(thorium.ItemType("Glacieor"));
                recipe.AddIngredient(ItemID.IceBow);
                recipe.AddIngredient(ItemID.ColdWatersintheWhiteLand);
                recipe.AddIngredient(thorium.ItemType("FrozenButterfly"));
            }
            else
            {*/
                recipe.AddIngredient(ItemID.IceBow);
                recipe.AddIngredient(ItemID.ColdWatersintheWhiteLand);
                recipe.AddIngredient(ItemID.Fish);
            //}
            
            recipe.AddIngredient(ItemID.ToySled);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
