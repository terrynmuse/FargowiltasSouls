using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class ChaliceoftheMoon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chalice of the Moon");
            Tooltip.SetDefault(@"'The moon smiles'
Grants immunity to Venom, Ivy Venom, Burning, Fused, Marked for Death, and Hexed
Grants immunity to Atrophied, Jammed, Reverse Mana Flow, and Antisocial
Increases life regeneration
Press down in the air to fastfall
Fastfall will create a fiery eruption on impact after falling a certain distance
You periodically fire additional attacks depending on weapon type
You erupt into spiky balls and Ancient Visions when injured
Summons a friendly Cultist and plant to fight at your side");
            DisplayName.AddTranslation(GameCulture.Chinese, "月之杯");
            Tooltip.AddTranslation(GameCulture.Chinese, @"月亮的微笑
免疫毒液,燃烧,导火线,死亡标记和着迷
免疫萎缩,卡壳,反魔力流和反社交
增加生命回复
在空中按'下'键快速下落
在一定高度使用快速下落, 会在撞击地面时产生猛烈的火焰喷发
根据武器类型定期发动额外的攻击
受伤时爆发尖钉球和远古幻象攻击敌人
召唤友善的邪教徒和植物为你而战");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 9;
            item.value = Item.sellPrice(0, 7);
            item.defense = 8;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();

            //magical bulb
            player.lifeRegen += 2;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[mod.BuffType("IvyVenom")] = true;
            if (Soulcheck.GetValue("Plantera Minion"))
                player.AddBuff(mod.BuffType("PlanterasChild"), 2);

            //lihzahrd treasure
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[mod.BuffType("Fused")] = true;
            fargoPlayer.LihzahrdTreasureBox = true;

            //celestial rune
            player.buffImmune[mod.BuffType("MarkedforDeath")] = true;
            player.buffImmune[mod.BuffType("Hexed")] = true;
            fargoPlayer.CelestialRune = true;
            fargoPlayer.AdditionalAttacks = true;

            //chalice
            player.buffImmune[mod.BuffType("Atrophied")] = true;
            player.buffImmune[mod.BuffType("Jammed")] = true;
            player.buffImmune[mod.BuffType("ReverseManaFlow")] = true;
            player.buffImmune[mod.BuffType("Antisocial")] = true;
            fargoPlayer.MoonChalice = true;

            if (Soulcheck.GetValue("Cultist Minion"))
                player.AddBuff(mod.BuffType("LunarCultist"), 2);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("MagicalBulb"));
            recipe.AddIngredient(mod.ItemType("LihzahrdTreasureBox"));
            recipe.AddIngredient(mod.ItemType("CelestialRune"));
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ItemID.FragmentVortex, 10);
            recipe.AddIngredient(ItemID.FragmentNebula, 10);
            recipe.AddIngredient(ItemID.FragmentStardust, 10);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
