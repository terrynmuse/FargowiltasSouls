using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;
using SacredTools;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class StellarPriestEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellar Priest Enchantment");
            Tooltip.SetDefault(
@"'Stand proud'
Summons a Stellar Guardian behind you that attacks enemies
Summons a Dark Star to shine Darkness upon you");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 350000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            //set bonus
            modPlayer.DustiteArmor = true;
            if (player.ownedProjectileCounts[soa.ProjectileType("StellarGuardian")] == 0 && !player.dead)
            {
                Projectile.NewProjectile(player.Center, Vector2.Zero, soa.ProjectileType("StellarGuardian"), (int)(1000f * player.minionDamage), 0f, player.whoAmI, 0f, 0f);
            }

            //pet soon tm
        }

        private readonly string[] items =
        {
            "StellarPriestHead",
            "StellarPriestChest",
            "StellarPriestLegs",
            "StarScourge",
            "GalaxyScepter",
            "LunarCrystalStaff",
            "OblivionRod",
            "FlariumSceptre",
            "AsthralDroneStaff",
            "DarkStar"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            foreach (string i in items) recipe.AddIngredient(soa.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
