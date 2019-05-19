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
";

            if(thorium != null)
            {
                tooltip += "50% of the damage you take is staggered over the next 10 seconds\n";
            }

            tooltip +=
@"Throw a smoke bomb to teleport to it and gain the First Strike Buff
Greatly enhances Lightning Aura effectiveness
Effects of Master Ninja Gear
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
            //ninja gear
            player.blackBelt = true;
            player.spikedBoots = 2;
            player.dash = 1;
            //tele thru wall
            modPlayer.ShinobiEffect(hideVisual);
            //ninja, smoke bombs, pet
            modPlayer.NinjaEffect(hideVisual);

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //set bonus
            thoriumPlayer.shadeSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MonkAltHead);
            recipe.AddIngredient(ItemID.MonkAltShirt);
            recipe.AddIngredient(ItemID.MonkAltPants);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "ShadeMasterEnchant");
                recipe.AddIngredient(ItemID.MasterNinjaGear);
                recipe.AddIngredient(ItemID.MonkBelt);
                recipe.AddIngredient(thorium.ItemType("ShadeKusarigama"));
                recipe.AddIngredient(ItemID.DD2LightningAuraT3Popper);
                recipe.AddIngredient(ItemID.DeadlySphereStaff);
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
