using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class FrigidGemstone : ModItem
    {   
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frigid Gemstone");
            Tooltip.SetDefault(@"'A shard of ancient magical ice'
Grants immunity to Frostburn
Your attacks summon Frostfireballs to attack your enemies");
            DisplayName.AddTranslation(GameCulture.Chinese, "寒玉");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'一块古老的魔法冰碎片'
免疫寒焰
攻击召唤霜火球攻击敌人");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 5;
            item.value = Item.sellPrice(0, 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Frostburn] = true;
            if (Soulcheck.GetValue("Frostfireballs"))
            {
                FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
                fargoPlayer.FrigidGemstone = true;
                if (fargoPlayer.FrigidGemstoneCD > 0)
                    fargoPlayer.FrigidGemstoneCD--;
            }
        }
    }
}
