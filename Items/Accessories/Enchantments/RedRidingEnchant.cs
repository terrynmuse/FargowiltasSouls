using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class RedRidingEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Riding Enchantment");
            Tooltip.SetDefault(
@"'Big Bad Red Riding Hood'
During a Full Moon, attacks may cause enemies to Super Bleed
Your attacks deal increasing damage to low HP enemies
Greatly enhances Explosive Traps effectiveness
Celestial Shell effects
Summons a pet Puppy");
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
            player.setHuntressT2 = true;
            player.setHuntressT3 = true;
            //celestial shell
            player.accMerman = true;
            player.wolfAcc = true;

            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }

            player.GetModPlayer<FargoPlayer>(mod).RedRidingEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HuntressAltHead);
            recipe.AddIngredient(ItemID.HuntressAltShirt);
            recipe.AddIngredient(ItemID.HuntressAltPants);
            recipe.AddIngredient(ItemID.HuntressBuckler);
            recipe.AddIngredient(ItemID.CelestialShell);
            
            /*if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("EvisceratingClaw"), 300);
                recipe.AddIngredient(thorium.ItemType("LadyLight"));
                recipe.AddIngredient(ItemID.DD2ExplosiveTrapT2Popper);
                recipe.AddIngredient(ItemID.DD2ExplosiveTrapT3Popper);
            }
            else
            {*/
                recipe.AddIngredient(ItemID.DD2ExplosiveTrapT3Popper);
            //}
            
            recipe.AddIngredient(ItemID.DogWhistle);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
