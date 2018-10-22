using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Essences
{
    public class HealerEssence : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Healer Essence");
            Tooltip.SetDefault(
                @"''
");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true; //
            item.rare = 2; //
            item.value = 60000; //
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            HealEffect(player);
        }
        
        private void HealEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            
            
        }
        
        private readonly string[] items =
        {
            "ClericEmblem",
            "RottenCod - corrupt fishing",
            "BrainCoral",
            "lifes gift - jungle spores",
            "bat scythe - viscount",
            "feather barrier rod - avian",
            "energy manipulator - granite boss",
            "deep staff - scarlet chest",
            "life quartz claymore - heart crystal",
            "war forger - sold by blackmsith",
            "star rod - star scouter",
            "divine lotus - travel merch after skele",
            "sentinels wand - sold by heal guy",
            "MarrowScepter"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
