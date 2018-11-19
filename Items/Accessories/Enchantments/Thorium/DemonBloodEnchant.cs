using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class DemonBloodEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Blood Enchantment");
            Tooltip.SetDefault(
@"'Infused with Corrupt Blood'
Maximum life increased by 100
Getting hit will trigger 'Sanguine', increasing defensive abilities briefly.
Flail weapons have a chance to release rolling spike balls on hit that apply ichor to damaged enemies.
Your symphonic damage empowers all nearby allies with: Abomination's Blood
Damage done against ichor'd enemies is increased by 5%
Doubles the range of your empowerments effect radius
Your symphonic damage will empower all nearby allies with: Critical Strike II
Damage against enemies has a 10% chance to drop flesh, which grants bonus life and damage when picked up
Greatly increases life regen
Hearts heal for 1.5x as much
Your damage will have a 10% chance to cause an eruption of blood
This blood can be picked up by players to heal themselves for 15% of the damage you dealt
Healing amount cannot exceed 15 life and picking up blood causes bleeding for 5 seconds
Summons a pet Flying Blister, Face Monster, and Crimson Heart");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.demonbloodSet = true;
            player.statLifeMax2 += 100;
            //demon blood badge
            thoriumPlayer.CrimsonBadge = true;
            //vile core
            thoriumPlayer.vileCore = true;
            //subwoofer
            thoriumPlayer.subwooferIchor = true;
            thoriumPlayer.bardRangeBoost += 450;
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3CriticalStrike = 2;
            //flesh set bonus
            thoriumPlayer.Symbiotic = true;
            //vampire gland
            thoriumPlayer.vampireGland = true;
            //blister pet
            modPlayer.AddPet("Blister Pet", hideVisual, thorium.BuffType("BlisterBuff"), thorium.ProjectileType("BlisterPet"));
            thoriumPlayer.blisterPet = true;
            //crimson regen, pets
            modPlayer.CrimsonEffect(hideVisual);
        }
        
        private readonly string[] items =
        {
            "DemonRageBadge",
            "VileCore",
            "CrimsonSubwoofer",
            "TunePlayerCritChance",
            "DemonBloodStaff",
            "DarkContagionBook",
            "IchorButterfly"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("DemonBloodHelmet"));
            recipe.AddIngredient(thorium.ItemType("DemonBloodBreastPlate"));
            recipe.AddIngredient(thorium.ItemType("DemonBloodGreaves"));
            recipe.AddIngredient(null, "FleshEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
