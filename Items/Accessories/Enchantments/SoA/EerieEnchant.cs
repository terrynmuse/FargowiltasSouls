using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;
using SacredTools;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class EerieEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eerie Enchantment");
            Tooltip.SetDefault(
@"'Creeper? Aw man…'
Minion damage grants increased life regeneration");
            DisplayName.AddTranslation(GameCulture.Chinese, "怪诞魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'Creeper? Aw man…'
召唤伤害增加生命回复
召唤一个发出血红色亮光的猩红符文");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 70000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            //set bonus
            modPlayer.EerieEffect = true;

            //pets soon tm
        }

        private readonly string[] items =
        {
            "EerieHelmet",
            "EerieChest",
            "EerieLegs",
            "EerieCane"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            foreach (string i in items) recipe.AddIngredient(soa.ItemType(i));

            recipe.AddIngredient(ItemID.FleshGrinder);
            recipe.AddIngredient(ItemID.DeathbringerPickaxe);
            recipe.AddIngredient(ItemID.TheRottedFork);
            recipe.AddIngredient(ItemID.Fleshcatcher);
            recipe.AddIngredient(ItemID.CrimsonCloak);
            recipe.AddIngredient(soa.ItemType("CrimsonSigil"));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
