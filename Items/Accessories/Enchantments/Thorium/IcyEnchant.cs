using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class IcyEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icy Enchantment");
            Tooltip.SetDefault(
@"'Cold to the touch'
An icy aura surrounds you, which freezes nearby enemies after a short delay
Effects of Frostburn Pouch
Summons a pet Penguin");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 40000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //set bonus
            thoriumPlayer.icySet = true;
            if (player.ownedProjectileCounts[thorium.ProjectileType("IcyAura")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("IcyAura"), 0, 0f, player.whoAmI, 0f, 0f);
            }

            //frostburn pouch
            thoriumPlayer.frostburnPouch = true;

            modPlayer.IcyEnchant = true;
            modPlayer.AddPet("Penguin Pet", hideVisual, BuffID.BabyPenguin, ProjectileID.Penguin);
        }
        
        private readonly string[] items =
        {
            "IcyBandana",
            "IcyMail",
            "IcyGreaves",
            "FrostburnPouch",
            "FrostFireKatana",
            "FrostFury",
            "Blizzard"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(thorium.ItemType("IcyCaltrop"), 300);
            recipe.AddIngredient(ItemID.IceBoomerang);
            recipe.AddIngredient(ItemID.Fish);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
