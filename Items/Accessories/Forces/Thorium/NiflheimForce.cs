using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces.Thorium
{
    public class NiflheimForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Niflheim");
            Tooltip.SetDefault(
@"'A world of mist, a sign of the dead...'
10% increased inspiration regeneration
Your symphonic empowerments will last an additional 5 seconds
Symphonic critical strikes cause the attack's empowerment to ascend to a fourth level of intensity
Inspiration notes that drop are twice as potent and increase your symphonic damage briefly
Pressing the 'Special Ability' key will cycle you through four states
It will also summon a chorus of music playing ghosts
Effects of Ring of Unity, Mix Tape and Devil's Subwoofer
Effects of Auto Tuner, Concert Tickets, and Metronome
Effects of Red, Brown, and Purple Music Players");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            //crier
            thoriumPlayer.bardResourceRecharge += 10;

            //noble
            mod.GetItem("NobleEnchant").UpdateAccessory(player, hideVisual);

            //cyber punk
            mod.GetItem("CyberPunkEnchant").UpdateAccessory(player, hideVisual);

            //ornate
            mod.GetItem("OrnateEnchant").UpdateAccessory(player, hideVisual);

            //conductor
            mod.GetItem("ConductorEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "CrierEnchant");
            recipe.AddIngredient(null, "NobleEnchant");
            recipe.AddIngredient(null, "CyberPunkEnchant");
            recipe.AddIngredient(null, "OrnateEnchant");
            recipe.AddIngredient(null, "ConductorEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
