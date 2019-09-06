using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Terraria.Localization;
using System;
using SacredTools;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Forces.SoA
{
    public class SoranForce : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Soran");
            Tooltip.SetDefault(
@"'The true power of the Soraniti'
All armor bonuses from Blazing Brute, Cosmic Commander, and Nebulous Apprentice
All armor bonuses from Stellar Priest and Fallen Prince
Effects of Nuba's Blessing, Novaniel's Resolve, and Celestial Ring
Summons several pets");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            //blazing brute
            modPlayer.SolariusArmor = true;
            //cosmic commander
            modPlayer.VoxaArmor = true;
            //nebulous apprentice
            modPlayer.NubaArmor = true;
            //nubas blessing
            modPlayer.NubaBlessing = true;
            //stellar priest
            modPlayer.DustiteArmor = true;
            if (player.ownedProjectileCounts[soa.ProjectileType("StellarGuardian")] == 0 && !player.dead)
            {
                Projectile.NewProjectile(player.Center, Vector2.Zero, soa.ProjectileType("StellarGuardian"), (int)(1000f * player.minionDamage), 0f, player.whoAmI, 0f, 0f);
            }
            //fallen prince
            modPlayer.NovanielArmor = true;
            //novaniels resolve
            ModLoader.GetMod("SacredTools").GetItem("NovanielResolve").UpdateAccessory(player, hideVisual);
            //celestial ring
            ModLoader.GetMod("SacredTools").GetItem("LunarRing").UpdateAccessory(player, hideVisual);

            //pets soon tm
        }


        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "BlazingBruteEnchant");
            recipe.AddIngredient(null, "CosmicCommanderEnchant");
            recipe.AddIngredient(null, "NebulousApprenticeEnchant");
            recipe.AddIngredient(null, "StellarPriestEnchant");
            recipe.AddIngredient(null, "FallenPrinceEnchant");
            recipe.AddIngredient(null, "LunarRing");
            recipe.AddIngredient(null, "TrueMoonEdgedPandolarra");

            recipe.AddTile(mod, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
