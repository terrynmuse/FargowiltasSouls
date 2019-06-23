using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class CelestialRune : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Rune");
            Tooltip.SetDefault(@"'A fallen enemy's spells, repurposed'
Grants immunity to Marked for Death and Hexed
You may periodically fire additional attacks depending on weapon type
Taking damage creates a friendly Ancient Vision to attack enemies"); 
            DisplayName.AddTranslation(GameCulture.Chinese, "天界符文");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'堕落的敌人的咒语,被改换用途'
免疫死亡标记和着魔
根据武器类型定期发动额外的攻击
受伤时创造一个友好的远古幻象来攻击敌人");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 9;
            item.value = Item.sellPrice(0, 7);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("MarkedforDeath")] = true;
            player.buffImmune[mod.BuffType("Hexed")] = true;
            player.GetModPlayer<FargoPlayer>().CelestialRune = true;
            player.GetModPlayer<FargoPlayer>().AdditionalAttacks = true;
        }
    }
}
