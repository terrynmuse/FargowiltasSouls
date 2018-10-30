using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class DreamWeaverEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dream Weaver Enchantment");
            Tooltip.SetDefault(
                @"''
Pressing the Special Ability key will activate The Dream, causing all of the player's heals to cure all debuffs and give 1 second of invulnerability to their heal target for 10 seconds
Pressing the Special Ability key will activate Distorted Reality, slowing down all enemies, making them take more damage, and significantly increasing the attack speed of all allies for 15 seconds
");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 6; //blood orange
            item.value = 400000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            DreamEffect(player);
        }
        
        private void DreamEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            
            
        }
        
        private readonly string[] items =
        {
            "DreamWeaversHelmet",
            "DreamWeaversHood",
            "DreamWeaversTabard",
            "DreamWeaversTreads",
            "DragonHeartWand",
            "SnackLantern",
            "ChristmasCheer",
            "MoleculeStabilizer",
            "DreamCatcher",
            "SimpleBroom"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            //celestial
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
