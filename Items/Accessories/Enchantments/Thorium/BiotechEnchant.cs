using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class BiotechEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Biotech Enchantment");
            Tooltip.SetDefault(
@"'Anyways, that's how I lost my medical license'
A biotech probe will assist you in healing your allies
Heals ally life equal to your bonus healing");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 6;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            if (Soulcheck.GetValue("Biotech Probe"))
            {
                ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
                thoriumPlayer.essenceSet = true;
                if (player.ownedProjectileCounts[thorium.ProjectileType("LifeEssence")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("LifeEssence"), 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
        }
        
        private readonly string[] items =
        {
            "LifeWeaverHood",
            "LifeWeaverGarment",
            "LifeWeaverLeggings",
            "LustrousBaton",
            "NullZoneStaff",
            "VoidPlanter",
            "LifeEssenceApparatus",
            "PocketGaurdian", //....
            "LifePulseStaff",
            "BarrierGenerator"
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
