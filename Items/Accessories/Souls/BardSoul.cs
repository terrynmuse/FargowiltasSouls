using System.Linq;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class BardSoul : ModItem
    {
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
        private string _tooltip = null;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rhapsodist's Soul");

            if (ModLoader.GetLoadedMods().Contains("ThoriumMod"))
                Tooltip.SetDefault(
                    @"'Every note you produce births a new world'
40% increased symphonic damage
25% increased symphonic playing speed
20% increased symphonic critical strike chance
Your symphonic damage empowers all nearby allies with: Cold Shoulder, Spider Bite, Abomination's Blood, Vile Flames and Terrarian
Damage done against frostburnt, envenomed, ichor'd, and cursed flamed enemies is increased by 10%
Doubles the range of your empowerments effect radius
Percussion critical strikes will deal 10% more damage
Percussion critical strikes will briefly stun enemies
Your wind instrument attacks now attempt to quickly home in on enemies
If the attack already homes onto enemies, it does so far more quickly
String weapon projectiles bounce five additional times
Critical strikes caused by brass instrument attacks release a spread of energy");
            else
                Tooltip.SetDefault("'Every note you produce births a new world'\n" +
                                   "-Enable Thorium for this soul to do anything-");
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
            if (Fargowiltas.Instance.ThoriumLoaded) Bard(player);
        }

        private void Bard(Player player)
        {
            //general

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(_thorium);

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

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            ModRecipe recipe = new ModRecipe(mod);

            foreach (string i in _items) recipe.AddIngredient(_thorium.ItemType(i));

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
