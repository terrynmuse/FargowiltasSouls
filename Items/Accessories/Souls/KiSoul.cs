using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class KiSoul : ModItem
    {
        string _tooltip = null;

        public override bool Autoload(ref string name)
        {
            return false;
        }

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
25% increased cast speed
+5 Charge limit for all beams
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
            item.rare = 11;
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
            
            //dbtPlayer.KiDamage += 0.35f;
            //dbtPlayer.KiCrit += 20f;
            //dbtPlayer.KiSpeedAddition += 0.25f;
            dbtPlayer.KiKbAddition += 0.3f;
            dbtPlayer.KiDrainMulti -= 0.4f;
            dbtPlayer.KiMax = (int)(dbtPlayer.KiMax * 1.3);
            dbtPlayer.KiRegen += 4;
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
            "LargeTurtleShell",
            "WornGloves",
            "NimbusWhistle",
            "KaioFragment4",
            "KiFragment5",
            "NebulaTotem"
        };

        private readonly Mod _dbzmod = ModLoader.GetMod("DBZMOD");

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.DBTLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            foreach (string i in _items)
            {
                recipe.AddIngredient(_dbzmod.ItemType(i));
            }
            
            recipe.AddIngredient(_dbzmod.ItemType("RadiantKiCrystal"), 250);
            
            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
