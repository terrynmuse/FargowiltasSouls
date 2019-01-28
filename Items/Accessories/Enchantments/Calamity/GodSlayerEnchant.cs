using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class GodSlayerEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("CalamityMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("God Slayer Enchantment");
            Tooltip.SetDefault(
@"'The power to slay gods resides within you...'

If you are killed, you revive with 150 hp. Cooldown is 45 seconds, and while this ability is on cooldown, you gain 10% increased damage.
Taking over 80 damage in one hit releases a swarm of godslayer darts that deal 900 damage each.
Taking 80 or less damage in a single hit reduces it to 1.
Enemies that touch you take 2.5x their contact damage.
Ranged crits have a 1 in (100 - ranged crit chance) to deal 4 times as much damage.
When firing ranged weapons, the weapon has a 5% chance to fire a god-killer shrapnel round that deals 210% of the projectile that triggered it. It explodes into shrapnel that deals 30% of the round's damage.
Enemies release godslayer and healing flames upon being hit by a magic attack. The godslayer flames deal 50% more damage of the projectile that triggered it, and the healing flames heal you for 6% of the projectile's damage.
Taking damage releases a godslayer explosion, dealing 1200 damage.
Summons a mechworm. 
Hitting enemies summons godslayer phantoms.
If the wearer takes more than 80 damage in one hit, they get extra invincibility frames.
Grants immunity to fire blocks and knockback.
+40 max life.
Grants a dash that can ram enemies.
Press N to cut your speed but increase damage and crit chance by 10%, and defense by 25
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

            //Godslayer leggings, chestplate, all Godslayer helmets, Excelsus, Deathwind, Eradidator, Deathhail staff, Staff of the Mechworm, Cleansing blaze, Essence Flayer, the Enforcer, Executioner's blade, Stream Gouge, Soul Piercer, Magnetic Meltdown, Asgardian Aegis and Cosmic Discharge. 

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
