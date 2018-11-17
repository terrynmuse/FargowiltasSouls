using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class ValadiumEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int lightGen;
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Valadium Enchantment");
            Tooltip.SetDefault(
@"''
Reverse gravity by pressing UP
While reversed, ranged damage is increased by 12%
While equipped, the eye will give vision of your cursors current position");
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
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ValadiumEffect(player);
        }
        
        private void ValadiumEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //set bonus
            player.gravControl = true;
            if (player.gravDir == -1f)
            {
                player.rangedDamage += 0.12f;
                player.AddBuff(thorium.BuffType("GravityDamage"), 60, true);
            }
            //eye of beholder
            lightGen++;
            if (lightGen >= 40)
            {
                for (int i = 0; i < 10; i++)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("BeholderGaze"), 0, 0f, player.whoAmI, i, 0f);
                }
                for (int j = 0; j < 10; j++)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("BeholderGaze2"), 0, 0f, player.whoAmI, j, 0f);
                }
                lightGen = 0;
            }
        }
        
        private readonly string[] items =
        {
            "ValadiumHelmet",
            "ValadiumBreastPlate",
            "ValadiumGreaves",
            "EyeofBeholder",
            "GlacialSting",
            "Obliterator",
            "ValadiumBow",
            "ValadiumStaff",
            "LodeStoneQuickDraw",
            "TommyGun"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
