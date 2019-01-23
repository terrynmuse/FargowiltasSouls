using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class StatigelEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("CalamityMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Statigel Enchantment");
            Tooltip.SetDefault(
@"'Statis’ mystical power surrounds you…'

Taking over 100 damage in a single hit makes you invincible for an extended time.
Increased jump height.
Grants you an extra jump.
15% increased damage.
+1 max minions.
Summons 2 baby slime gods, one for each world evil.
+17% increased movement speed.
");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;//
            item.value = 400000;//
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;


        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            //Statigel armor, all Statigel helmets, Geletic Blade, Goobow, Corroslime Staff, Crimslime Staff, Lunarian Bow and The God’s Gambit.

            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));

            recipe.AddTile(TileID.LunarCraftingStation);//
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
