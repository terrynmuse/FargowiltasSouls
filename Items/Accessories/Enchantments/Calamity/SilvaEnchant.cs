using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class SilvaEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("CalamityMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silva Enchantment");
            Tooltip.SetDefault(
@"'Boundless life energy cascades from you...'


You are immune to just about every debuff.
Reduces all damage by 5%, calculated separately from DR.
Run speed increased by 5%.
+80 max life.
All projectiles spawn healing leaf orbs on hit.
If you are reduced to 0 health, you cannot die for 10 seconds. This happens once per life.
If you are reduced to 0 health while you are invulnerable, you lose 100 max hp, until you die and respawn.
After the Silva Invulnerability, you take half as much contact damage, ranged damage increases by 50%, magic weapons gain 10% increased damage and crit chance, you gain +2 minions and 15% increased minion damage, and throwing weapons gain 20% more damage.
True Melee strikes have a chance to quintuple their damage.
Melee projectiles have a 25% chance to inflict Silva Stun.
All ranged weapons with a use time of above 4 get a 10% fire rate increase.
All magic projectiles have a 10% chance to create a massive explosion.
Faster throwing rate if above 90% life.
Summons a Silva Crystal above your head to protect you.
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

            //Silva leggings, armor, all Silva helmets, Alpha Ray, Voltaic Climax, Empyrean Knives, Scourge of the Cosmos, Greatsword of Blah, 25 Yharon Soul Fragments, 50 Darksun Fragments, and 15 Effulgent Feathers.


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
