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
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warlock Enchantment");
            Tooltip.SetDefault(
@"''
Radiant critical strikes will generate up to 15 shadow wisps
Corrupts your radiant powers
Pressing the 'Special Ability' key will unleash every stored shadow wisp towards your cursor's position
Halves radiant life costs but not its life transferring effect
Summons a li'l devil to attack enemies");
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
            
           WarlockEffect(player);
        }
        
        private void WarlockEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //set bonus
            thoriumPlayer.warlockSet = true;
            //demon tongue
            thoriumPlayer.darkAura = true;
            thoriumPlayer.radiantLifeCost = 2;
            //lil devil
            thoriumPlayer.devilMinion = true;
        }
        
        private readonly string[] items =
        {
            "WarlockHood",
            "WarlockGarb",
            "WarlockLeggings",
            "DemonTongue",
            "Omen",
            "DamageStaff",
            "ShadowStaff",
            "BloodRage",
            "NecroticStaff",
            "DevilStaff"
        };

        public override void AddRecipes()
        {
            //ebon 

            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
