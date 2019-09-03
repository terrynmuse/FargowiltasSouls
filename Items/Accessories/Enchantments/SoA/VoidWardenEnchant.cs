using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;
using SacredTools;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class VoidWardenEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Warden Enchantment");
            Tooltip.SetDefault(
@"'The ride never ends'
Taking damage has a chance to freeze all enemies nearby
Bosses and enemies with over 8000 HP are unaffected 
Attacking has a 5% chance to make nearby enemies take double damage
Summons a friendly Bullet Kin");
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
            modPlayer.voidDefense = true;
            modPlayer.voidOffense = true;

            //pets soon tm
        }

        private readonly string[] items =
        {
            "VoidHelm",
            "VoidChest",
            "VoidChestOffense",
            "VoidLegs",
            "Skill_FuryForged",
            "DarkRemnant",
            "DragonslayerPandolarra",
            "EdgeOfGehenna",
            "OblivionMagnum",
            "ArachnesGaze"
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
