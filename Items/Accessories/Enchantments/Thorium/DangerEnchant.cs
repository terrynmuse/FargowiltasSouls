using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class DangerEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Danger Enchantment");
            Tooltip.SetDefault(
@"'Let's get dangerous...'
While in combat, your life recovery is increased by 2
You are immune to most damage-inflicting debuffs");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 60000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            if (!thoriumPlayer.outOfCombat)
            {
                thoriumPlayer.lifeRecovery += 2;
            }

            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Venom] = true;
        }
        
        private readonly string[] items =
        {
            "DangerHelmet",
            "DangerMail",
            "DangerGreaves",
            "TrackerBlade",
            "DangerDevestator",
            "DangerDaikatana",
            "DangerDoomerang",
            "DangerDuelShot" //really diver
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(thorium.ItemType("DangerDagger"), 300);
            recipe.AddIngredient(ItemID.Rally);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
