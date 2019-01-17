using System.Linq;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class BardSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bard's Soul");

            Tooltip.SetDefault(
@"'Every note you produce births a new world'
30% increased symphonic damage
20% increased symphonic playing speed
15% increased symphonic critical strike chance
Increases maximum inspiration by 20
Percussion critical strikes will deal 10% more damage
Percussion critical strikes will briefly stun enemies
Your wind instrument attacks now attempt to quickly home in on enemies
If the attack already homes onto enemies, it does so far more quickly
String weapon projectiles bounce five additional times
Critical strikes caused by brass instrument attacks release a spread of energy");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 1000000;
            item.rare = 11;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(230, 248, 34));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            //general
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.symphonicDamage += 0.3f;
            thoriumPlayer.symphonicSpeed += .2f;
            thoriumPlayer.symphonicCrit += 15;
            thoriumPlayer.bardResourceMax2 += 20;
            //epic mouthpiece
            thoriumPlayer.bardHomingBool = true;
            thoriumPlayer.bardHomingBonus = 5f;
            //straight mute
            thoriumPlayer.bardMute2 = true;
            //digital tuner
            thoriumPlayer.tuner2 = true;
            //guitar pick claw
            thoriumPlayer.bardBounceBonus = 5;
        }
        
        private readonly string[] _items =
        {
            "DigitalVibrationTuner",
            "EpicMouthpiece",
            "GuitarPickClaw",
            "StraightMute",
            "BandKit",
            "FishBone",
            "PrimesRoar",
            "EskimoBanjo",
            "SoundSagesLament",
            "ChronoOcarina",
            "TheMaw",
            "SonicAmplifier",
            "TheSet"   
        };
        
        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            //recipe.AddIngredient(null, "BardEssence");

            foreach (string i in _items) recipe.AddIngredient(thorium.ItemType(i));

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
