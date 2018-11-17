using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class FolvEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Folv's Ancient Enchantment");
            Tooltip.SetDefault(
@"''
Projects a mystical barrier around you
While above 50% life, every fourth magic cast will unleash damaging mana bolts
While below 50% life, your defensive capabilities are increased
Your symphonic damage will empower all nearby allies with: Defense II");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            FolvEffect(player);
        }
        
        private void FolvEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.folvSet = true;
            Lighting.AddLight(player.position, 0.03f, 0.3f, 0.5f);
            if (player.statLife >= player.statLifeMax * 0.5)
            {
                thoriumPlayer.folvBonus = true;
            }
            else
            {
                thoriumPlayer.folvBonus2 = true;
                player.lifeRegen += 2;
                thoriumPlayer.thoriumEndurance += 0.1f;
                player.noKnockback = true;
            }
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3Defense = 2;
        }
        
        private readonly string[] items =
        {
            "DemonBloodStaff",
            "Legacy",
            "AncientFrost",
            "AncientSpark",
            "AncientLight"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("FolvHat"));
            recipe.AddIngredient(thorium.ItemType("FolvRobe"));
            recipe.AddIngredient(thorium.ItemType("FolvLegging"));
            recipe.AddIngredient(thorium.ItemType("TunePlayerDefense"));
            recipe.AddIngredient(ItemID.UnholyTrident);

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
