using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
    public class OlympiansSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olympian's Soul");
            Tooltip.SetDefault(
@"'Strike with deadly precision'
30% increased throwing damage
20% increased throwing speed
15% increased throwing critical chance and velocity");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 1000000;
            item.rare = -12;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).ThrowSoul = true;

            player.thrownDamage += 0.3f;
            player.thrownCrit += 15;
            player.thrownVelocity += 0.15f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "SlingersEssence");

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                


            }
            else
            {
                recipe.AddIngredient(ItemID.Chik);
                recipe.AddIngredient(ItemID.MagicDagger);
                recipe.AddIngredient(ItemID.Bananarang, 5);
                recipe.AddIngredient(ItemID.Amarok);
                recipe.AddIngredient(ItemID.ShadowFlameKnife);
                recipe.AddIngredient(ItemID.FlyingKnife);
                recipe.AddIngredient(ItemID.LightDisc, 5);
                recipe.AddIngredient(ItemID.FlowerPow);
                recipe.AddIngredient(ItemID.ToxicFlask);
                recipe.AddIngredient(ItemID.VampireKnives);
                recipe.AddIngredient(ItemID.PaladinsHammer);
                recipe.AddIngredient(ItemID.PossessedHatchet);
                recipe.AddIngredient(ItemID.Terrarian);
            }


            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}