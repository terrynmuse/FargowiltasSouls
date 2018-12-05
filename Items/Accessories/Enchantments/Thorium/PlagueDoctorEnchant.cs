using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class PlagueDoctorEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plague Doctor Enchantment");
            Tooltip.SetDefault(
@"'What nasty concoction could you be brewing?'
Plague doctor vials have a 40% chance to become retrievable upon shattering
Using a throwing item has a 20% chance to unleash two Blight Daggers
Blight Daggers home in on enemies and apply a highly contagious disease
Throwing damage will decrease enemy defense
Pressing the Special Ability key will trigger True Strikes
Needs to recharge for 2 minutes after being activated");
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

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.plagueSet = true;
            //plague lords fask
            thoriumPlayer.blightAcc = true;
            //lich set bonus
            thoriumPlayer.lichSet = true;
            //lich gaze
            thoriumPlayer.lichGaze = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(thorium.ItemType("PlagueDoctersMask"));
            recipe.AddIngredient(thorium.ItemType("PlagueDoctersGarb"));
            recipe.AddIngredient(thorium.ItemType("PlagueDoctersLeggings"));
            recipe.AddIngredient(null, "LichEnchant");
            recipe.AddIngredient(thorium.ItemType("PlagueLordsFlask"));
            recipe.AddIngredient(thorium.ItemType("CombustionFlask"), 300);
            recipe.AddIngredient(thorium.ItemType("NitrogenVial"), 300);
            recipe.AddIngredient(thorium.ItemType("CorrosionBeaker"), 300);
            recipe.AddIngredient(thorium.ItemType("FrostPlagueStaff"));
            recipe.AddIngredient(ItemID.ToxicFlask);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
