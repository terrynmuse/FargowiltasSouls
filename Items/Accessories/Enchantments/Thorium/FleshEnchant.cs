using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class FleshEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flesh Enchantment");
            Tooltip.SetDefault(
@"'Symbiotically attached to your body'
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
            item.rare = 4;
            item.value = 120000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
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
            "VampireGland",
            "GrimFlayer",
            "FleshMace",
            "BloodBelcher",
            "HungerStaff",
            "BlisterSack"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("FleshMask"));
            recipe.AddIngredient(thorium.ItemType("FleshBody"));
            recipe.AddIngredient(thorium.ItemType("FleshLegs"));
            recipe.AddIngredient(null, "CrimsonEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
