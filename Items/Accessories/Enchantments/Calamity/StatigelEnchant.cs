using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class StatigelEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("CalamityMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Statigel Enchantment");
            Tooltip.SetDefault(
@"'Statis’ mystical power surrounds you…'
When you take over 100 damage in one hit you become immune to damage for an extended period of time
Grants an extra jump and increased jump height
Summons a mini slime god to fight for you, the type depends on what world evil you have");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            CalamityPlayer calamityPlayer = player.GetModPlayer<CalamityPlayer>(calamity);
            calamityPlayer.statigelSet = true;
            player.doubleJumpSail = true;
            player.jumpBoost = true;
            //summon
            calamityPlayer.slimeGod = true;
            modPlayer.AddMinion("Slime God Minion", calamity.ProjectileType("SlimeGod"), 33, 0);
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("StatigelHelm"));
            recipe.AddIngredient(calamity.ItemType("StatigelHeadgear"));
            recipe.AddIngredient(calamity.ItemType("StatigelCap"));
            recipe.AddIngredient(calamity.ItemType("StatigelHood"));
            recipe.AddIngredient(calamity.ItemType("StatigelMask"));
            recipe.AddIngredient(calamity.ItemType("StatigelArmor"));
            recipe.AddIngredient(calamity.ItemType("StatigelGreaves"));
            recipe.AddIngredient(calamity.ItemType("ManaOverloader"));
            recipe.AddIngredient(calamity.ItemType("OverloadedBlaster"));
            recipe.AddIngredient(calamity.ItemType("Waraxe"));
            recipe.AddIngredient(calamity.ItemType("MarkedMagnum"));
            recipe.AddIngredient(calamity.ItemType("HeartRapier"));
            recipe.AddIngredient(calamity.ItemType("CursedDagger"));
            recipe.AddIngredient(calamity.ItemType("IchorSpear"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
