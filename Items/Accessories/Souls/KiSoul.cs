using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class KiSoul : ModItem
    {
        string _tooltip = null;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiritualist's Soul");

            if (ModLoader.GetLoadedMods().Contains("DBZMOD"))
            {
                Tooltip.SetDefault(
@"'The world's spirit resonates within you.'
35% increased ki damage
40% reduced ki usage
20% increased ki critical strike chance
30% increased ki knockback
+5 Charge limit for all beams
Increased ki cast speed
Drasctically increases the range of ki orb pickups
Increased ki orb heal rate
Drastically increased ki regen
30% increased max Ki");

                //at a later date
                // Increases beam charge speed by 25%		
            }
            else
            {
                Tooltip.SetDefault("'The world's spirit resonates within you.'\n" +
                                   "-Enable Dragon Ball Terraria for this soul to do anything-");
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 1000000;
            item.expert = true;
            item.rare = -12;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Fargowiltas.Instance.DBTLoaded)
            {
                Ki(player);
            }
        }

        private void Ki(Player player)
        {
            //general

            DBZMOD.MyPlayer dbtPlayer = player.GetModPlayer<DBZMOD.MyPlayer>(_dbzmod);
            
            dbtPlayer.KiDamage += 0.35f;
            dbtPlayer.KiCrit += 20;
            dbtPlayer.KiSpeedAddition += 4;
            dbtPlayer.KiKbAddition += 0.3;
            dbtPlayer.KiDrainMulti -= 0.3;
            dbtPlayer.KiMax *= 1.3;
            dbtPlayer.KiRegen = 4;
            dbtPlayer.OrbGrabRange += 6;
            dbtPlayer.OrbHealAmount += 100;
            dbtPlayer.ChargeLimitAdd += 5;

        }

        private readonly string[] _items = 
        {
            "SpiritBomb",
            "DragonGemNecklace",
            "SenzuBag",
            "ScouterT6",
            "SpiritualEmblem",
            "TurtleShell",
            "VegetaGloves",
            "NimbusWhistle",
            "KaioFragment4",
            "KiFragment5"
        };

        private readonly Mod _dbzmod = ModLoader.GetMod("DBZMOD");

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            ModRecipe recipe = new ModRecipe(mod);

            foreach (string i in _items)
            {
                recipe.AddIngredient(_dbzmod.ItemType(i));
                recipe.AddIngredient(_dbzmod.ItemType("RadiantKiCrystal", 250));
            }
            
            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
