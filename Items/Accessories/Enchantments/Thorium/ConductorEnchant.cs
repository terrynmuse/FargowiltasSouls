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
        public int timer;
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Conductor Enchantment");
            Tooltip.SetDefault(
                @"''
Pressing the Special Ability key will summon a chorus of music playing ghosts
Every three seconds the metronome will flip between tick & tock
Tick increases your symphonic playing speed and damage
Tock decreases your symphonic playing speed and damage");
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
            
            ConductorEffect(player);
        }
        
        private void ConductorEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.conductorSet = true;
            //metronome
            timer++;
            if (timer == 180)
            {
                player.AddBuff(thorium.BuffType("MetronomeBuff"), 179, true);
            }
            if (timer == 360)
            {
                player.AddBuff(thorium.BuffType("MetronomeDebuff"), 179, true);
                timer = 0;
            }
        }
        
        private readonly string[] items =
        {
            "PowderedWig",
            "ConductorSuit",
            "ConductorLeggings",
            "Metronome",
            "BoneTrumpet",
            "MusicSheet4",
            "Clarinet",
            "Bassoon",
            "FrenchHorn",
            "Saxophone"
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
