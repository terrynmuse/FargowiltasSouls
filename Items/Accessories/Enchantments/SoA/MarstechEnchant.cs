using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;
using SacredTools;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class MarstechEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marstech Enchantment");
            Tooltip.SetDefault(
@"'Who needs magic when you have technology?'
Dealing damage charges up an energy forcefield around you that damages enemies and decays over time 
Can be instantly discharged by pressing [Ability], which will cause a shockwave to damage all nearby enemies 
Damage, range and debuff duration are increased by forcefield strength 
Has a cooldown of 1 minute");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            //set bonus
            modPlayer.marsArmor = true;
            //space junk
            modPlayer.spaceJunk = true;
        }

        private readonly string[] items =
        {
            "PhaseSlasher",
            "PlasmaDischarge",
            "ZappersInsanity",
            "Trispear",
            "LCSynth"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(soa.ItemType("MarstechHelm"));
            recipe.AddIngredient(soa.ItemType("MarstechPlate"));
            recipe.AddIngredient(soa.ItemType("MarstechLegs"));
            recipe.AddIngredient(null, "SpaceJunkEnchant");

            foreach (string i in items) recipe.AddIngredient(soa.ItemType(i));

            recipe.AddIngredient(ItemID.PaintingTheTruthIsUpThere);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
