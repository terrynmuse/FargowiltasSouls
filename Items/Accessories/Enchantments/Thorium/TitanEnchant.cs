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
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titan Enchantment");
            Tooltip.SetDefault(
@"'Infused with primordial energy'
15% increased damage
Effects of Mask of the Crystal Eye, Abyssal Shell, and Cyan Music Player");
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

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //set bonus
            player.GetModPlayer<FargoPlayer>().AllDamageUp(.15f);
            //crystal eye mask
            thoriumPlayer.critDamage += 0.1f;
            //abyssal shell
            thoriumPlayer.AbyssalShell = true;
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3DamageReduction = 2;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("TitanHeadgear"));
            recipe.AddIngredient(thorium.ItemType("TitanHelmet"));
            recipe.AddIngredient(thorium.ItemType("TitanMask"));
            recipe.AddIngredient(thorium.ItemType("TitanBreastplate"));
            recipe.AddIngredient(thorium.ItemType("TitanGreaves"));
            recipe.AddIngredient(thorium.ItemType("CrystalEyeMask"));
            recipe.AddIngredient(thorium.ItemType("AbyssalShell"));
            recipe.AddIngredient(thorium.ItemType("TunePlayerDamageReduction"));
            recipe.AddIngredient(thorium.ItemType("TitanBoomerang"));
            recipe.AddIngredient(thorium.ItemType("Executioner"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
