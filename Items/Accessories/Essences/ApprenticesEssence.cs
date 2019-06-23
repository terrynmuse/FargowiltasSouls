using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Essences
{
    public class ApprenticesEssence : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apprentice's Essence");
            Tooltip.SetDefault(
@"'This is only the beginning..'
18% increased magic damage
5% increased magic crit
Increases your maximum mana by 50");
            DisplayName.AddTranslation(GameCulture.Chinese, "学徒精华");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'这才刚刚开始..'
增加18%魔法伤害
增加5%魔法暴击率
增加50最大法力值");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 150000;
            item.rare = 4;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage += .18f;
            player.magicCrit += 5;
            player.statManaMax2 += 50;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                //just thorium
                recipe.AddIngredient(ItemID.SorcererEmblem);
                recipe.AddIngredient(thorium.ItemType("GraveBuster"));
                recipe.AddIngredient(thorium.ItemType("ThoriumStaff"));
                recipe.AddIngredient(ItemID.Vilethorn);
                recipe.AddIngredient(ItemID.CrimsonRod);
                recipe.AddIngredient(thorium.ItemType("DetachedUFOBlaster"));
                recipe.AddIngredient(ItemID.WaterBolt);
                recipe.AddIngredient(ItemID.BookofSkulls);
                recipe.AddIngredient(ItemID.MagicMissile);
                recipe.AddIngredient(ItemID.Flamelash);
                recipe.AddIngredient(thorium.ItemType("SpineBreaker"));
                recipe.AddIngredient(ItemID.DemonScythe);
                recipe.AddIngredient(thorium.ItemType("MagikStaff"));
            }
            else
            {
                recipe.AddIngredient(ItemID.SorcererEmblem);
                recipe.AddIngredient(ItemID.WandofSparking);
                recipe.AddIngredient(ItemID.Vilethorn);
                recipe.AddIngredient(ItemID.CrimsonRod);
                recipe.AddIngredient(ItemID.WaterBolt);
                recipe.AddIngredient(ItemID.BookofSkulls);
                recipe.AddIngredient(ItemID.MagicMissile);
                recipe.AddIngredient(ItemID.Flamelash);
                recipe.AddIngredient(ItemID.DemonScythe);
            }

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
