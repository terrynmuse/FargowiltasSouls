using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class GraniteEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Granite Enchantment");
            Tooltip.SetDefault(
@"'Defensively energized'
Immune to intense heat and enemy knockback, but your movement speed is slowed down greatly
While equipped, energy will slowly seep out and attack nearby enemies
Your symphonic damage empowers nearby ally attacks with energy overflow
Damage done against overflowed enemies is increased by 8%
Doubles the range of your empowerments effect radius");
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
            
            GraniteEffect(player);
        }
        
        private void GraniteEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //set bonus
            player.fireWalk = true;
            player.lavaImmune = true;
            player.buffImmune[24] = true;
            player.noKnockback = true;
            player.moveSpeed -= 0.5f;
            player.maxRunSpeed = 4f;
            //eye of the storm
            timer++;
            if (timer > 60)
            {
                if (player.direction > 0)
                {
                    Projectile.NewProjectile(player.Center.X + 14f, player.Center.Y - 20f, Main.rand.Next(-5, 5), Main.rand.Next(-5, -1), thorium.ProjectileType("StormHome"), 25, 0f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X + 14f, player.Center.Y - 20f, Main.rand.Next(-5, 5), Main.rand.Next(-5, -1), thorium.ProjectileType("StormHome"), 25, 0f, player.whoAmI, 0f, 0f);
                    timer = 0;
                }
                if (player.direction < 0)
                {
                    Projectile.NewProjectile(player.Center.X - 14f, player.Center.Y - 20f, Main.rand.Next(-5, 5), Main.rand.Next(-5, -1), thorium.ProjectileType("StormHome"), 25, 0f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X - 14f, player.Center.Y - 20f, Main.rand.Next(-5, 5), Main.rand.Next(-5, -1), thorium.ProjectileType("StormHome"), 25, 0f, player.whoAmI, 0f, 0f);
                    timer = 0;
                }
            }
            //granite woofer
            thoriumPlayer.subwooferGranite = true;
            thoriumPlayer.bardRangeBoost += 450;
        }
        
        private readonly string[] items =
        {
            "EyeoftheStorm",
            "GraniteSubwoofer",
            "GraniteSaber",
            "EnergyProjector",
            "BoulderProbe",
            "EnergyWingButterfly"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("GraniteHelmet"));
            recipe.AddIngredient(thorium.ItemType("GraniteChestGuard"));
            recipe.AddIngredient(thorium.ItemType("GraniteGreaves"));
            recipe.AddIngredient(ItemID.NightVisionHelmet);

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
