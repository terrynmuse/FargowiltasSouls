using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class WarlockEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warlock Enchantment");
            Tooltip.SetDefault(
@"'Better than a wizard'
Radiant critical strikes will generate up to 15 shadow wisps
Pressing the 'Special Ability' key will unleash every stored shadow wisp towards your cursor's position
Effects of Demon Tongue
Summons a Li'l Devil to attack enemies");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 4;
            item.value = 120000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            if (Soulcheck.GetValue("Warlock Wisps"))
            {
                //set bonus
                thoriumPlayer.warlockSet = true;
            }
            
            //demon tongue
            thoriumPlayer.darkAura = true;
            thoriumPlayer.radiantLifeCost = 2;
            //lil devil
            modPlayer.WarlockEnchant = true;
            modPlayer.AddMinion("Li'l Devil Minion", thorium.ProjectileType("Devil"), 20, 2f);
        }
        
        private readonly string[] items =
        {
            "DemonTongue",
            "Omen",
            "ShadowStaff",
            "BloodRage",
            "NecroticStaff",
            "DevilStaff"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("WarlockHood"));
            recipe.AddIngredient(thorium.ItemType("WarlockGarb"));
            recipe.AddIngredient(thorium.ItemType("WarlockLeggings"));
            recipe.AddIngredient(null, "EbonEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
