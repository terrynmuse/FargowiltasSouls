using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class BardSoul : ModItem
    {
        string _tooltip = null;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rhapsodist's Soul");

            if (ModLoader.GetLoadedMods().Contains("ThoriumMod"))
            {
                Tooltip.SetDefault("'Every note you produce births a new world'\n" +
                                    "40% increased symphonic damage\n" +
                                    "25% increased symphonic playing speed\n" +
                                    "20% increased symphonic critical strike chance\n" +
                                    "Your symphonic damage empowers all nearby allies with: Cold Shoulder, Spider Bite, Abomination's Blood, Vile Flames and Terrarian\n" +
                                    "Damage done against frostburnt, envenomed, ichor'd, and cursed flamed enemies is increased by 10%\n" +
                                    "Doubles the range of your empowerments effect radius\n" +
                                    "Percussion critical strikes will deal 10% more damage\n" +
                                    "Percussion critical strikes will briefly stun enemies\n" +
                                    "Your wind instrument attacks now attempt to quickly home in on enemies\n" +
                                    "If the attack already homes onto enemies, it does so far more quickly\n" +
                                    "String weapon projectiles bounce five additional times\n" +
                                    "Critical strikes caused by brass instrument attacks release a spread of energy");

                //at a later date
                // Increases inspiration regeneration by 10%
                // Increases maximum inspiration by 4					
            }
            else
            {
                Tooltip.SetDefault("'Every note you produce births a new world'\n" +
                                   "-Enable Thorium for this soul to do anything-");
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 750000;
            item.expert = true;
            item.rare = -12;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                Bard(player);
            }
        }

        private void Bard(Player player)
        {
            //general

            ThoriumMod.ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumMod.ThoriumPlayer>(_thorium);
            
            thoriumPlayer.symphonicDamage += 0.4f;
            thoriumPlayer.symphonicCrit += 20;
            thoriumPlayer.symphonicSpeed += .25f;

            //woofers
            thoriumPlayer.subwooferFrost = true;
            thoriumPlayer.subwooferVenom = true;
            thoriumPlayer.subwooferIchor = true;
            thoriumPlayer.subwooferCursed = true;
            thoriumPlayer.subwooferTerrarium = true;

            //type buffs
            thoriumPlayer.bardHomingBool = true;
            thoriumPlayer.bardHomingBonus = 5f;
            thoriumPlayer.bardMute2 = true;
            thoriumPlayer.tuner2 = true;
            thoriumPlayer.bardBounceBonus = 5;
        }

        private readonly string[] _items = 
        {
            "VenomSubwoofer",
            "FrostSubwoofer",
            "CorruptSubwoofer",
            "CrimsonSubwoofer",
            "TerrariumSubwoofer",
            "DigitalVibrationTuner",
            "EpicMouthpiece",
            "StraightMute",
            "GuitarPickClaw",
            "Triangle",
            "Ocarina",
            "Saxophone",
            "RockstarsDoubleBassBlastGuitar"
        };

        private readonly Mod _thorium = ModLoader.GetMod("ThoriumMod");

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            ModRecipe recipe = new ModRecipe(mod);

            foreach (string i in _items)
            {
                recipe.AddIngredient(_thorium.ItemType(i));
            }
            
            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
