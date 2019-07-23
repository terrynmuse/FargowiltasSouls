using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class FeralFurEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Feral-Fur Enchantment");
            Tooltip.SetDefault(
@"'Let your inner animal out'
Melee critical strikes grant Alpha's Roar, briefly increasing the damage of your summoned minions");

            //sacrificial dagger on all hits?
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

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //feral set bonus
            thoriumPlayer.alphaRage = true;
        }
        
        private readonly string[] items =
        {
            "FeralSkinHead",
            "FeralSkinChest",
            "FeralSkinLegs",
            "Dagger",
            "Bellerose",
            "SacrificialDagger",
            "MeteorBarrier",
            "CrimsonSummon",
            "BloodCellStaff",
            "BackStabber"
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
