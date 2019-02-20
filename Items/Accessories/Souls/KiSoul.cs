using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class KiSoul : ModItem
    {
        private readonly Mod dbzMod = ModLoader.GetMod("DBZMOD");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("DBZMOD");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiritualist's Soul");

            Tooltip.SetDefault(
@"'The world's spirit resonates within you.'
35% increased ki damage
40% reduced ki usage
20% increased ki critical strike chance
30% increased ki knockback
Massively increased speed while charging
Drastically increased flight speed
Drastically reduced flight ki usage
+5 Charge limit for all beams
Zenkai charm effects
Drasctically increases the range of ki orb pickups
Increased ki orb heal rate
Drastically increased ki regen
30% increased max Ki");
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
            if (!Fargowiltas.Instance.DBTLoaded) return;

            //general
            DBZMOD.MyPlayer dbtPlayer = player.GetModPlayer<DBZMOD.MyPlayer>(dbzMod);

            dbtPlayer.kiDamage += 0.35f;
            dbtPlayer.kiCrit += 20;
            dbtPlayer.chargeMoveSpeed = Math.Max(dbtPlayer.chargeMoveSpeed, 3f);
            dbtPlayer.kiKbAddition += 0.3f;
            dbtPlayer.kiDrainMulti -= 0.4f;
            dbtPlayer.kiMaxMult += 0.3f;
            dbtPlayer.kiRegen += 4;
            dbtPlayer.orbGrabRange += 6;
            dbtPlayer.orbHealAmount += 100;
            dbtPlayer.chargeLimitAdd += 5;
            dbtPlayer.flightSpeedAdd += 0.5f;
            dbtPlayer.flightUsageAdd += 2;
            dbtPlayer.zenkaiCharm = true;
        }

        private readonly string[] _items = 
        {
            "CrystalliteAlleviate",
            "BlackDiamondShell",
            "BlackBlitz",
            "BuldariumSigmite",
            "CandyLaser",
            "InfuserRainbow",
            "EarthenArcanium",
            "FinalShine",
            "KaioCrystal",
            "MajinNucleus",
            "SpiritCharm",
            "SuperSpiritBomb",
            "ScouterT6",
            "ZenkaiCharm"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.DBTLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            foreach (string i in _items)
            {
                recipe.AddIngredient(dbzMod.ItemType(i));
            }
            
            //recipe.AddIngredient(_dbzmod.ItemType("RadiantKiCrystal"), 250);
            
            recipe.AddTile(mod, "CrucibleCosmosSheet");
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
