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
    public class SyranForce : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Syran");
            Tooltip.SetDefault(
@"'Dragon Rage empowers you, and encourages you to go on'
All armor bonuses from Void Warden, Vulcan Reaper, and Flarium
All armor bonuses from Asthraltite
Effects of Ring of the Fallen, Memento Mori, and Arcanum of the Caster
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

            //void warden
            modPlayer.voidDefense = true;
            modPlayer.voidOffense = true;

            //vulcan reaper
            player.buffImmune[soa.BuffType("SerpentWrath")] = true;
            player.buffImmune[soa.BuffType("ObsidianCurse")] = true;

            //flarium
            modPlayer.DragonSetEffect = true;

            //exitum
            //soon tm

            //asthraltite
            modPlayer.AstralSet = true;
            //ring of the fallen
            ModLoader.GetMod("SacredTools").GetItem("AsthralRing").UpdateAccessory(player, hideVisual);
            //memento mori
            ModLoader.GetMod("SacredTools").GetItem("MementoMori").UpdateAccessory(player, hideVisual);
            //arcanum of the caster
            ModLoader.GetMod("SacredTools").GetItem("CasterArcanum").UpdateAccessory(player, hideVisual);

            //pets soon tm
        }


        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "VoidWardenEnchant");
            recipe.AddIngredient(null, "VulcanReaperEnchant");
            recipe.AddIngredient(null, "FlariumEnchant");
            recipe.AddIngredient(null, "ExitumLuxEnchant");
            recipe.AddIngredient(null, "AsthraltiteEnchant");

            recipe.AddTile(mod, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
