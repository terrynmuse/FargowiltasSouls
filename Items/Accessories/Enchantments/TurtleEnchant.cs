using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

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
Enemies may explode into needles on death
Summons a pet Lizard and Turtle"); //shell hide no happen with SoE
            DisplayName.AddTranslation(GameCulture.Chinese, "乌龟魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'你突然有一种想躲进壳里的冲动'
当站立不动且不攻击时,获得缩壳Buff
缩壳能阻挡所有抛射物,但是增加接触伤害
反弹100%接触伤害
敌人死亡时爆成针
召唤一只宠物蜥蜴和宠物海龟");
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
            recipe.AddIngredient(ItemID.ChlorophytePartisan);

            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.Seedler);
                recipe.AddIngredient(thorium.ItemType("AbsintheFury"));
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
