using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Essences
{
    public class BardEssence : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bard Essence");
            Tooltip.SetDefault(
@"''This is only the beginning..''
18% increased symphonic damage
5% increased symphonic playing speed
5% increased symphonic critical strike chance");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 4;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            BardEffect(player);
        }
        
        private void BardEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.symphonicDamage += 0.18f;
            thoriumPlayer.symphonicSpeed += .05f;
            thoriumPlayer.symphonicCrit += 5;
        }
        
        private readonly string[] items =
        {
            "BardEmblem",
            "AntlionMaraca",
            "SeashellCastanets",
            "Didgeridoo",
            "BagPipe",
            "YewWoodLute",
            "ForestOcarina",
            "AquamarineWineGlass",
            "SonarCannon",
            "MusicSheetCongas",
            "GraniteBoombox",
            "BronzeTuningFork",
            "HotHorn",
            "SongofIceandFire"
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
