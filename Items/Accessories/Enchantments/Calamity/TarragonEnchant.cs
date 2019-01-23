using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class TarragonEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("CalamityMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tarragon Enchantment");
            Tooltip.SetDefault(
@"'Braelor's undying might flows through you...'

Increased heart pickup range.
Enemies have a higher chance to drop hearts on death.
Taking damage gives you a 25% chance to get the Tarra Life buff for 6 seconds.
Pressing Y reduces contact damage by 75% for 10 seconds.
Ranged projectiles have a 13% chance to split into 3 life energies on impact, dealing 33% of the projectile's original damage.
Every ranged crit gives a small ranged damage boost. Stacks up to 10%.
On every 5th crit, the player fires 10 leaf projectiles. The leaves deal 20% of the attack that summoned them.
Magic projectiles have a 50% chance to heal the player every 1.5 seconds. The projectile heals you for 2% of the projectile's damage.
50% more summon damage.
+2 max minions.
+10% summon damage while at full health.
Summons a life aura around the player that deals 300 damage per frame.
After every 25th throwing crit, the player becomes invincible for 5 seconds. This occurs once every 30 seconds.
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

            //Tarragon leggings, breastplate, all Tarragon helmets, Dark Sun Ring, 100 Tarra throwing darts, biofusillade, nettlevine greatbow, verdant, spyker, mistlestorm, badge of bravery, divine retribution, and lifehunt scythe.

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
