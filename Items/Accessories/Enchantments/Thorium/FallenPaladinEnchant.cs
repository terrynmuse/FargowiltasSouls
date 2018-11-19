using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class FallenPaladinEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fallen Paladin Enchantment");
            Tooltip.SetDefault(
@"'Silently, they walk the dungeon halls'
Taking damage heals nearby allies equal to 15% of the damage taken
Successfully healing an ally with a healing spell will replenish 4 life
Taking fatal damage unleashes your inner spirit
Your inner spirit will constantly release beams of healing energy towards your cursor");
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
            
            PaladinEffect(player);
        }
        
        private void PaladinEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //set bonus
            thoriumPlayer.fallenPaladinSet = true;
            //wyne
            thoriumPlayer.Wynebgwrthucher = true;
            //ascension statue
            thoriumPlayer.ascension = true;
        }
        
        private readonly string[] items =
        {
            "FallenPaladinFacegaurd",
            "FallenPaladinCuirass",
            "FallenPaladinGreaves",
            "Wynebgwrthucher",
            "AscensionStatuette",
            "TwilightStaff",
            "HolyHammer",
            "BulwarkStaff",
            "SpiritFireWand",
            "PillPopper"
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
