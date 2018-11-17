using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class BronzeEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bronze Enchantment");
            Tooltip.SetDefault(
                @"''
Thrown damage has a chance to cause a lightning bolt to strike
The damage you take is stored into your next attack.
The bonus damage is stored until it is expended
Throwing damage increases your movement speed by 1% up to 25%
Throwing damage increases your throwing speed by 0.4% up to 10%
These effects will fade after 3 seconds of not dealing throwing damage
Your symphonic damage empowers all nearby allies with: Medusa's Gaze
Damage done against petrified enemies is increased by 8%
Doubles the range of your empowerments effect radius");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 60000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            BronzeEffect(player);
        }
        
        private void BronzeEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //lightning
            thoriumPlayer.greekSet = true;
            //rebuttal
            thoriumPlayer.championShield = true;
            //sandles
            thoriumPlayer.spartanSandle = true;
            //subwoofer
            thoriumPlayer.subwooferMarble = true;
            thoriumPlayer.bardRangeBoost += 450;
        }
        
        private readonly string[] items =
        {
            "BronzeHelmet",
            "BronzeBreastplate",
            "BronzeGreaves",
            "ChampionsBarrier",
            "SpartanSandles",
            "BronzeSubwoofer",
            "ChampionBlade"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(thorium.ItemType("BronzeThrowing"), 300);
            recipe.AddIngredient(thorium.ItemType("DraculaFang"), 300);
            recipe.AddIngredient(thorium.ItemType("AncientWingButterfly"));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
