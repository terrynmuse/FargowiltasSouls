using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class LifeBinderEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Life Binder Enchantment");
            Tooltip.SetDefault(
@"''
Healing spells will shortly increase the healed player's maximum life by 50
Healing an ally grants them 2 life recovery for 10 seconds
Your radiant damage has a 15% chance to release a blinding flash of light
The flash heals nearby allies equal to your bonus healing and confuses enemies
Healing allies with less health than you increases their life recovery
Healing allies with more health than you increases your life recovery
Summons a pet Holy Goat");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 4;
            item.value = 120000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //life binder set bonus
            thoriumPlayer.mistSet = true;
            //aloe leaf
            thoriumPlayer.aloePlant = true;
            //goat pet
            modPlayer.BinderEnchant = true;
            modPlayer.AddPet("Holy Goat Pet", hideVisual, thorium.BuffType("HolyGoatBuff"), thorium.ProjectileType("HolyGoat"));
            thoriumPlayer.goatPet = true;
            //iridescent set bonus
            thoriumPlayer.iridescentSet = true;
            //equalizer 
            thoriumPlayer.equilibrium = true;
        }
        
        private readonly string[] items =
        {
            "AloeLeaf",
            "BloomGuard",
            "SunrayStaff",
            "MorningDew",
            "LifeFruitButterfly",
            "RichLeaf"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("DewBinderMask"));
            recipe.AddIngredient(thorium.ItemType("DewBinderBreastplate"));
            recipe.AddIngredient(thorium.ItemType("DewBinderGreaves"));
            recipe.AddIngredient(null, "IridescentEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
