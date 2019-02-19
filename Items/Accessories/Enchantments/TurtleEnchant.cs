using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class TurtleEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Turtle Enchantment");
            Tooltip.SetDefault(
@"'You suddenly have the urge to hide in a shell'
When standing still and not attacking, you gain the Shell Hide buff
Shell Hide protects you from all projectiles, but increases contact damage
100% of contact damage is reflected
Enemies will explode into needles on death
Summons a pet Lizard and Turtle"); //shell hide no happen with SoE
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
            modPlayer.CactusEffect();
            modPlayer.TurtleEffect(hideVisual);
            player.thorns = 1f;
            player.turtleThorns = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TurtleHelmet);
            recipe.AddIngredient(ItemID.TurtleScaleMail);
            recipe.AddIngredient(ItemID.TurtleLeggings);
            recipe.AddIngredient(null, "CactusEnchant");
            recipe.AddIngredient(ItemID.FleshKnuckles);

            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("AbsintheFury"));
                recipe.AddIngredient(ItemID.ChlorophytePartisan);
                recipe.AddIngredient(thorium.ItemType("TurtleDrum"));
            }
            
            recipe.AddIngredient(ItemID.Seaweed);
            recipe.AddIngredient(ItemID.LizardEgg);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
