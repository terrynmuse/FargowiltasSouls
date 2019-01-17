using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ShinobiEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shinobi Infiltrator Enchantment");

            string tooltip = 
@"'Village Hidden in the Wall'
Dash into any walls, to teleport through them to the next opening
Effects of the Master Ninja Gear
";

            /*if(thorium != null)
            {
                tooltip +=
@"Striking an enemy with any throwing weapon will trigger 'Shadow Dance'
Additonally, while Shadow Dance is active you deal 15% more throwing damage";
            }*/

            tooltip +=
@"Throw a smoke bomb to teleport to it
Standing nearby smoke gives you the First Strike buff
Greatly enhances Lightning Aura effectiveness
Summons a pet Gato and Black Cat";

            Tooltip.SetDefault(tooltip); 
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
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            player.setMonkT2 = true;
            player.setMonkT3 = true;
            //ninja gear
            player.blackBelt = true;
            player.spikedBoots = 2;
            player.dash = 1;
            //tele thru wall
            modPlayer.ShinobiEffect(hideVisual);
            //ninja, smoke bombs, pet
            modPlayer.NinjaEffect(hideVisual);

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            //set bonus
            thoriumPlayer.shadeSet = true;
            if (thoriumPlayer.shadeTele)
            {
                player.thrownDamage += 0.15f;
            }*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MonkAltHead);
            recipe.AddIngredient(ItemID.MonkAltShirt);
            recipe.AddIngredient(ItemID.MonkAltPants);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "NinjaEnchant");
                //recipe.AddIngredient(null, "ShadeMasterEnchant");
                recipe.AddIngredient(ItemID.MasterNinjaGear);
                recipe.AddIngredient(ItemID.MonkBelt);
                recipe.AddIngredient(ItemID.DD2LightningAuraT2Popper);
                recipe.AddIngredient(ItemID.DD2LightningAuraT3Popper);
                recipe.AddIngredient(thorium.ItemType("TotalityButterfly"));
            }
            else
            {
                recipe.AddIngredient(null, "NinjaEnchant");
                recipe.AddIngredient(ItemID.MasterNinjaGear);
                recipe.AddIngredient(ItemID.MonkBelt);
            }
            
            recipe.AddIngredient(ItemID.DD2PetGato);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
