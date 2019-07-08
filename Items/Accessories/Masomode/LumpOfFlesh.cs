using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class LumpOfFlesh : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lump of Flesh");
            Tooltip.SetDefault(@"'It's growing'
Grants immunity to Blackout, Obstructed, and Dazed
Increases minion damage by 16% but slightly decreases defense
Increases your max number of minions by 2
Increases your max number of sentries by 2
The pungent eyeball charges energy to fire a laser as you attack
Enemies are less likely to target you
Makes armed and magic skeletons less hostile outside the Dungeon");
            DisplayName.AddTranslation(GameCulture.Chinese, "肉团");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'它在增长'
免疫致盲,阻塞和眩晕
增加16%召唤伤害,但略微减少防御
+2最大召唤栏
+2最大哨兵栏
当你攻击时,尖刻眼球会充能来发射激光
敌人不太可能以你为目标
地牢外的装甲和魔法骷髅敌意减小");
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
            player.buffImmune[BuffID.Blackout] = true;
            player.buffImmune[BuffID.Obstructed] = true;
            player.buffImmune[BuffID.Dazed] = true;
            player.minionDamage += 0.16f;
            player.statDefense -= 6;
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
            player.maxMinions += 2;
            player.maxTurrets += 2;
            if (Soulcheck.GetValue("Pungent Eye Minion"))
                player.AddBuff(mod.BuffType("PungentEyeball"), 5);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("PungentEyeball"));
            recipe.AddIngredient(mod.ItemType("SkullCharm"));
            recipe.AddIngredient(ItemID.SpectreBar, 12);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
