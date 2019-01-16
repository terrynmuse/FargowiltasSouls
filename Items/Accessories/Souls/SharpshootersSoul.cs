using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Neck)]
    public class SharpshootersSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharpshooter's Soul");
            Tooltip.SetDefault(
@"'Ready, aim, fire'
30% increased range damage
20% increased firing speed
15% increased ranged critical chance
Sniper Scope Effects");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 1000000;
            item.rare = 11;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(188, 253, 68));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //attack speed
            player.GetModPlayer<FargoPlayer>(mod).RangedSoul = true;
            player.rangedDamage += .3f;
            player.rangedCrit += 15;
            //sniper scope
            player.scope = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "SnipersEssence");

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(ItemID.MagicQuiver);
                recipe.AddIngredient(thorium.ItemType("VegaPhaser"));
                recipe.AddIngredient(thorium.ItemType("Scorn"));
                recipe.AddIngredient(thorium.ItemType("SpineBuster"));
                recipe.AddIngredient(thorium.ItemType("DestroyersRage"));
                recipe.AddIngredient(thorium.ItemType("TerraBow"));
                recipe.AddIngredient(ItemID.PiranhaGun);
                recipe.AddIngredient(thorium.ItemType("LaunchJumper"));
                recipe.AddIngredient(thorium.ItemType("NovaRifle"));
                recipe.AddIngredient(ItemID.Tsunami);
                recipe.AddIngredient(ItemID.StakeLauncher);
                recipe.AddIngredient(ItemID.EldMelter);
                recipe.AddIngredient(ItemID.FireworksLauncher);
            }
            else
            {
                recipe.AddIngredient(ItemID.MagicQuiver);
                recipe.AddIngredient(ItemID.SniperScope);
                recipe.AddIngredient(ItemID.DartPistol);
                recipe.AddIngredient(ItemID.Megashark);
                recipe.AddIngredient(ItemID.PulseBow);
                recipe.AddIngredient(ItemID.NailGun);
                recipe.AddIngredient(ItemID.PiranhaGun);
                recipe.AddIngredient(ItemID.SniperRifle);
                recipe.AddIngredient(ItemID.Tsunami);
                recipe.AddIngredient(ItemID.StakeLauncher);
                recipe.AddIngredient(ItemID.EldMelter);
                recipe.AddIngredient(ItemID.Xenopopper);
                recipe.AddIngredient(ItemID.FireworksLauncher);
            }

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
