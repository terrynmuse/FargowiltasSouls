using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class TideHunterEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tide Hunter Enchantment");
            Tooltip.SetDefault(
@"'Not just for hunting fish'
Ranged critical strikes release a splash of foam, slowing nearby enemies
After four consecutive non-critical strikes, your next ranged attack will mini-crit for 150% damage
While standing still, defense is increased by 4 and you are immune to knockback
Brightens the area directly in front of you");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 80000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //tide hunter set bonus
            thoriumPlayer.tideHunterSet = true;
            //angler bowl
            if (!hideVisual)
            {
                if (player.direction > 0 && Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(player.Center.X + 56f, player.Center.Y - 10f, 0f, 0f, thorium.ProjectileType("AnglerLight"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                if (player.direction < 0 && Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(player.Center.X - 56f, player.Center.Y - 10f, 0f, 0f, thorium.ProjectileType("AnglerLight"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
            }
            //yew set bonus
            thoriumPlayer.yewCharging = true;
            //goblin war shield
            if (player.velocity.X == 0f)
            {
                player.statDefense += 4;
                player.noKnockback = true;
            }
        }
        
        private readonly string[] items =
        {
            "AnglerBowl",
            "BlunderBuss",
            "PearlPike",
            "HydroCannon",
            "SharkStorm",
            "MarineLauncher"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("TideHunterCap"));
            recipe.AddIngredient(thorium.ItemType("TideHunterChestpiece"));
            recipe.AddIngredient(thorium.ItemType("TideHunterLeggings"));
            recipe.AddIngredient(null, "YewWoodEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
