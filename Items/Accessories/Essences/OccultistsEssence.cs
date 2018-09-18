using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Essences
{
    public class OccultistsEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Occultist's Essence");
            Tooltip.SetDefault(
                @"'This is only the beginning..'
18% increased summon damage
Increases your max number of minions by 1
Increases your max number of sentries by 1");
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
            player.minionDamage += 0.18f;
            player.maxMinions += 1;
            player.maxTurrets += 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                //just thorium
                recipe.AddIngredient(ItemID.SummonerEmblem);
                recipe.AddIngredient(ItemID.SlimeStaff);
                recipe.AddIngredient(ItemID.HornetStaff);
                recipe.AddIngredient(ItemID.ImpStaff);
                recipe.AddIngredient(ItemID.DD2BallistraTowerT1Popper);
                recipe.AddIngredient(ItemID.DD2ExplosiveTrapT1Popper);
                recipe.AddIngredient(ItemID.DD2FlameburstTowerT1Popper);
                recipe.AddIngredient(ItemID.DD2LightningAuraT1Popper);
            }
            else
            {
                //no others
                recipe.AddIngredient(ItemID.SummonerEmblem);
                recipe.AddIngredient(ItemID.SlimeStaff);
                recipe.AddIngredient(ItemID.HornetStaff);
                recipe.AddIngredient(ItemID.ImpStaff);
                recipe.AddIngredient(ItemID.DD2BallistraTowerT1Popper);
                recipe.AddIngredient(ItemID.DD2ExplosiveTrapT1Popper);
                recipe.AddIngredient(ItemID.DD2FlameburstTowerT1Popper);
                recipe.AddIngredient(ItemID.DD2LightningAuraT1Popper);
            }

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}