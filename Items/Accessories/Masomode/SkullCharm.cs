using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class SkullCharm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skull Charm");
            Tooltip.SetDefault(@"'No longer in the zone'
Grants immunity to Dazed
Increases damage taken and dealt by 10%
Enemies are less likely to target you
Makes armed and magic skeletons less hostile outside the Dungeon");
            DisplayName.AddTranslation(GameCulture.Chinese, "头骨挂坠");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'不在这个区域了'
免疫眩晕
增加10%所受和造成的伤害
敌人不太可能以你为目标
地牢外的装甲和魔法骷髅敌意减小");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 8;
            item.value = Item.sellPrice(0, 6);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Dazed] = true;
            player.meleeDamage += 0.1f;
            player.rangedDamage += 0.1f;
            player.magicDamage += 0.1f;
            player.minionDamage += 0.1f;
            player.thrownDamage += 0.1f;
            player.endurance -= 0.1f;
            player.aggro -= 400;
            player.GetModPlayer<FargoPlayer>().SkullCharm = true;
            if (!player.ZoneDungeon)
            {
                player.npcTypeNoAggro[NPCID.SkeletonSniper] = true;
                player.npcTypeNoAggro[NPCID.SkeletonCommando] = true;
                player.npcTypeNoAggro[NPCID.TacticalSkeleton] = true;
                player.npcTypeNoAggro[NPCID.DiabolistRed] = true;
                player.npcTypeNoAggro[NPCID.DiabolistWhite] = true;
                player.npcTypeNoAggro[NPCID.Necromancer] = true;
                player.npcTypeNoAggro[NPCID.NecromancerArmored] = true;
                player.npcTypeNoAggro[NPCID.RaggedCaster] = true;
                player.npcTypeNoAggro[NPCID.RaggedCasterOpenCoat] = true;
            }
        }
    }
}
