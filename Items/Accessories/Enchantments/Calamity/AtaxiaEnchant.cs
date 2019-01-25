using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class AtaxiaEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("CalamityMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ataxia Enchantment");
            Tooltip.SetDefault(
@"'Not be confused with Ataraxia Enchantment'

Permanent Inferno potion effect.
You have a 20% chance to emit a blazing explosion on hit. 
+20 max life and mana.
Melee attacks spawn Chaos Flames.
You have a 50% chance to fire a homing chaos flare when firing a ranged weapon.
Magic attacks summon damaging and healing flame orbs on hit.
Summons a floating Chaos Spirit.
Throwing attacks have a 10% chance to unleash a volley of flames.
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

            //Ataxia armor, all Ataxia helmets, Hellfire Flamberge, Exsanguination Lance, Chaoswarped Slashaxe, Flame Scythe and Greatbow of Turmoil.

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
