using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class BloodflareEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("CalamityMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodflare Enchantment");
            Tooltip.SetDefault(
@"'The souls of the fallen are at your disposal...'



Enemies have a 50% chance to drop a mana star and a heart on hit.
Enemies killed during a Blood Moon have a very high chance of dropping a blood orb.
Greatly increased life regen.
True Melee Strikes heal you.
After striking an enemy 15 times, you will enter a blood frenzy for 5 seconds. During this time, you gain 25% increased damage/crit chance, and contact damage is halved.
Press Y to unleash lost souls.
Ranged and Magic projectiles often fire ghostly bolts.
For every magic crit, you gain a small magic damage boost. Stacks to 15%.
Being over 80% life boosts your defense by 30 and your throwing critical strike chance by 5%.
Being below 80% life will boost your throwing damage by 15%. Every throwing critical strike will heal you.
Summons Polterghast's mines to orbit you.
Striking an enemy that is under 20% health will trigger a bloodsplosion that drops hearts.
Gives the effects of the Core of the Blood God.
Enemies close to you will have their life drained");
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

            //Bloodflare cuisses, body armor, all Bloodflare helmets, the Mutilator, the Core of the Blood God, the Lacerator, the Sanguine Flare, the Viscera, the Claret Cannon, and the Arterial Assault

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
