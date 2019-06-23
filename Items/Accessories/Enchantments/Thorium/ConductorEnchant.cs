using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class ConductorEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Conductor Enchantment");
            Tooltip.SetDefault(
@"'I'll be Bach'
Pressing the Special Ability key will summon a chorus of music playing ghosts
Inspiration notes that drop are twice as potent and increase your symphonic damage briefly
Effects of Metronome and Purple Music Player");
            DisplayName.AddTranslation(GameCulture.Chinese, "指挥魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'我就是现代巴赫'
按下'特殊能力'键召唤亡灵合唱团
掉落的灵感音符双倍强度, 短暂增加音波伤害
拥有节拍器和粉色播放器的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.conductorSet = true;

            if (Soulcheck.GetValue("Metronome"))
            {
                thorium.GetItem("Metronome").UpdateAccessory(player, hideVisual);
            }
            
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3MaxMana = 2;
            //marching band set 
            thoriumPlayer.empoweredNotes = true;
        }
        
        private readonly string[] items =
        {
            "Metronome",
            "TunePlayerMaxMana",
            "BoneTrumpet",
            "Clarinet",
            "FrenchHorn",
            "Saxophone"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("PowderedWig"));
            recipe.AddIngredient(thorium.ItemType("ConductorSuit"));
            recipe.AddIngredient(thorium.ItemType("ConductorLeggings"));
            recipe.AddIngredient(null, "MarchingBandEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
