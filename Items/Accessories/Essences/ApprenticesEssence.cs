using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Essences
{
    public class ApprenticesEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apprentice's Essence");
            Tooltip.SetDefault("'This is only the beginning..' \n18% increased magic damage \nIncreases your maximum mana by 40 \nReduces mana usage by 5% \nAutomatically use mana potions when needed");
            if (Fargowiltas.Instance.CalamityLoaded)
            {
                Tooltip.SetDefault("'This is only the beginning..' \n18% increased magic damage \nIncreases your maximum mana by 40 \nReduces mana usage by 5% \nAutomatically use mana potions when needed \nGrants the effects of the Mana Overloader");
            }
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

            player.manaCost -= .05f;
            player.magicDamage += .18f;
            player.statManaMax2 += 40;
            player.manaFlower = true;

            //mana overloader
            if (Fargowiltas.Instance.CalamityLoaded)
            {
                if (player.statMana < player.statManaMax2 * 0.15f)
                {
                    player.ghostHeal = true;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                //just thorium
                recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("AzureGauntlet"));
                recipe.AddIngredient(ItemID.WizardHat);
                recipe.AddIngredient(ItemID.ManaFlower);
                recipe.AddIngredient(ItemID.SorcererEmblem);
                recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("MagikStaffAmber"));
                recipe.AddRecipeGroup("FargowiltasSouls:AnySilverStaff");
                recipe.AddRecipeGroup("FargowiltasSouls:AnyGoldStaff");
                recipe.AddIngredient(ItemID.SpaceGun);
                recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("DetachedUFOBlaster"));
                recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("MagicConch"));
                recipe.AddIngredient(ItemID.BeeGun);
                recipe.AddIngredient(ItemID.BookofSkulls);
                recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("GaussSpark"));
                recipe.AddIngredient(ItemID.Flamelash);
            }
            else
            {
                //no others
                recipe.AddIngredient(ItemID.WizardHat);
                recipe.AddIngredient(ItemID.ManaFlower);
                recipe.AddIngredient(ItemID.SorcererEmblem);
                recipe.AddIngredient(ItemID.WandofSparking);
                recipe.AddRecipeGroup("FargowiltasSouls:AnySilverStaff");
                recipe.AddRecipeGroup("FargowiltasSouls:AnyGoldStaff");
                recipe.AddRecipeGroup("FargowiltasSouls:AnyEvilMagic");
                recipe.AddIngredient(ItemID.AmberStaff);
                recipe.AddIngredient(ItemID.SpaceGun);
                recipe.AddIngredient(ItemID.WaterBolt);
                recipe.AddIngredient(ItemID.BeeGun);
                recipe.AddIngredient(ItemID.BookofSkulls);
                recipe.AddIngredient(ItemID.MagicMissile);
                recipe.AddIngredient(ItemID.Flamelash);
            }

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}