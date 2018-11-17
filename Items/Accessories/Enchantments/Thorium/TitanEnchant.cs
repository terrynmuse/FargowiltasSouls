using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class TitanEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titan Enchantment");
            Tooltip.SetDefault(
@"'Infused with primordial energy'
Damage increased by 10%
Critical strikes deal 10% more damage
Pressing the 'Encase' key will place you in an impenetrable shell
While encased, you can't use items or health potions, life regeneration is heavily reduced, and damage is nearly nullified
Leaving the shell will greatly lower your speed, damage reduction and damage briefly
Leaving the shell will prohibit the use of the shell again for 20 seconds
Your symphonic damage will empower all nearby allies with: Damage Reduction II");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 6;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            TitanEffect(player);
        }
        
        private void TitanEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //set bonus
            player.meleeDamage += 0.1f;
            player.thrownDamage += 0.1f;
            player.rangedDamage += 0.1f;
            player.magicDamage += 0.1f;
            player.minionDamage += 0.1f;
            thoriumPlayer.radiantBoost += 0.1f;
            thoriumPlayer.symphonicDamage += 0.1f;
            //crystal eye mask
            thoriumPlayer.critDamage += 0.1f;
            //abyssal shell
            thoriumPlayer.AbyssalShell = true;
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3DamageReduction = 2;
        }
        
        private readonly string[] items =
        {
            "TitanHeadgear",
            "TitanHelmet",
            "TitanMask",
            "TitanBreastplate",
            "TitanGreaves",
            "CrystalEyeMask",
            "AbyssalShell",
            "TunePlayerDamageReduction",
            "Executioner",
            "KineticKnife"
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
