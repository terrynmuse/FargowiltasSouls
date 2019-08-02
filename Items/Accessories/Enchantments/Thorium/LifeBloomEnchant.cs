using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class LifeBloomEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Life Bloom Enchantment");
            Tooltip.SetDefault(
@"'You are one with nature'
Minion attacks have a 33% chance to heal you lightly
Summons a living wood sapling and its attacks will home in on enemies
Effects of Flawless Chrysalis and Guide to Plant Fiber Cordage");
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
            //set bonus
            thoriumPlayer.lifeBloom = true;
            //chrysalis
            thoriumPlayer.cocoonAcc = true;
            //living wood set bonus
            thoriumPlayer.livingWood = true;
            //free boi
            modPlayer.LivingWoodEnchant = true;
            modPlayer.AddMinion("Sapling Minion", thorium.ProjectileType("MinionSapling"), 25, 2f);
            //vine rope thing
            player.cordage = true;
        }
        
        private readonly string[] items =
        {
            "Chrysalis",
            "HiveMind",
            "ButterflyStaff5",
            "HoneyBlade",
            "MushymenStaff",
            "OdinsEye"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("LifeBloomMask"));
            recipe.AddIngredient(thorium.ItemType("LifeBloomMail"));
            recipe.AddIngredient(thorium.ItemType("LifeBloomLeggings"));
            recipe.AddIngredient(null, "LivingWoodEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
