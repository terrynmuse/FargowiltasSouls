using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class TurtleEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Turtle Enchantment");
            Tooltip.SetDefault(
@"'You suddenly have the urge to hide in a shell'
When standing still and not attacking, you gain the Shell Hide buff
100% of damage taken by melee attacks is reflected
Enemies are more likely to target you
Summons a pet Lizard and Turtle");
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
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.TurtleEnchant = true;
            player.thorns = 1f;
            player.turtleThorns = true;
            player.aggro += 50;
            modPlayer.AddPet("Turtle Pet", hideVisual, BuffID.PetTurtle, ProjectileID.Turtle);
            modPlayer.AddPet("Lizard Pet", hideVisual, BuffID.PetLizard, ProjectileID.PetLizard);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TurtleHelmet);
            recipe.AddIngredient(ItemID.TurtleScaleMail);
            recipe.AddIngredient(ItemID.TurtleLeggings);
            recipe.AddIngredient(ItemID.FleshKnuckles);
            recipe.AddIngredient(ItemID.NettleBurst);
            recipe.AddIngredient(ItemID.Seaweed);
            recipe.AddIngredient(ItemID.LizardEgg);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}