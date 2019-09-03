using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;
using SacredTools;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class CosmicCommanderEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Commander Enchantment");
            Tooltip.SetDefault(
@"'Make Soran great again'
Pressing [Ability] puts you in 'Sniper State' 
Your damage is upped in this state however you are frozen in place and have reduced defense 
State is toggled upon button press and has a cooldown of 5 seconds after switching
Summons Chibi Voxa to follow you around");
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
            modPlayer.VoxaArmor = true;

            //pet soon tm
        }

        private readonly string[] items =
        {
            "VortexCommanderHat",
            "VortexCommanderSuit",
            "VortexCommanderGreaves",
            "DolphinGun",
            "LightningRifle",
            "PGMUltimaRatioHecateII",
            "FlariumRifle",
            "AsthralBow",
            "AsthralGun",
            "GlowingSock"
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
