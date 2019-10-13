using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class EarthForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Earth");

            Tooltip.SetDefault(
@"'Gaia's blessing shines upon you'
25% chance for your projectiles to explode into shards
20% increased weapon use speed
Greatly increases life regeneration after striking an enemy 
One attack gains 10% life steal every 4 seconds, capped at 8 HP
Flower petals will cause extra damage to your target 
Spawns 6 fireballs to rotate around you
Every 8th projectile you shoot will split into 3
Any secondary projectiles may also split
Briefly become invulnerable after striking an enemy");

            DisplayName.AddTranslation(GameCulture.Chinese, "大地之力");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'盖亚的祝福照耀着你'
你的抛射物有25%概率爆炸成碎片
增加25%武器使用速度
攻击敌人后大大增加生命回复
一次攻击获得每秒5%的生命窃取,上限为5点
花瓣对你的目标造成额外伤害
召唤6个环绕你的火球
第8个抛射物将会分裂成3个
分裂出的抛射物同样可以分裂
满血状态下所受到的伤害将减少90%在攻击敌人后的瞬间无敌");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            //mythril
            if (SoulConfig.Instance.GetValue("Mythril Weapon Speed") && !modPlayer.TerrariaSoul)
                modPlayer.AttackSpeed *= 1.2f;
            //shards
            modPlayer.CobaltEnchant = true;
            //regen on hit, heals
            modPlayer.PalladiumEffect();
            //fireballs and petals
            modPlayer.OrichalcumEffect();
            //split
            modPlayer.AdamantiteEnchant = true;
            //shadow dodge
            modPlayer.TitaniumEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "CobaltEnchant");
            recipe.AddIngredient(null, "PalladiumEnchant");
            recipe.AddIngredient(null, "MythrilEnchant");
            recipe.AddIngredient(null, "OrichalcumEnchant");
            recipe.AddIngredient(null, "AdamantiteEnchant");
            recipe.AddIngredient(null, "TitaniumEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}