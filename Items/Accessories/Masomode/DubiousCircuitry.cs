using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class DubiousCircuitry : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dubious Circuitry");
            Tooltip.SetDefault(@"'Malware probably not included'
Grants immunity to Cursed Inferno, Ichor, Lightning Rod, Defenseless, Stunned, and knockback
Your attacks inflict Cursed Inferno and Ichor
Your attacks have a small chance to inflict Lightning Rod
Two friendly probes fight by your side
Reduces damage taken by 6%");
            DisplayName.AddTranslation(GameCulture.Chinese, "可疑电路");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'里面也许没有恶意软件'
免疫诅咒地狱,脓液,避雷针,毫无防御,昏迷和击退
攻击造成诅咒地狱和脓液效果
攻击小概率造成避雷针效果
召唤2个友善的探测器为你而战
减少6%所受伤害");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 7;
            item.value = Item.sellPrice(0, 5);
            item.defense = 6;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[mod.BuffType("Defenseless")] = true;
            player.buffImmune[mod.BuffType("Stunned")] = true;
            player.buffImmune[mod.BuffType("LightningRod")] = true;
            player.GetModPlayer<FargoPlayer>().FusedLens = true;
            player.GetModPlayer<FargoPlayer>().GroundStick = true;
            if (SoulConfig.Instance.GetValue("Probes Minion"))
                player.AddBuff(mod.BuffType("Probes"), 2);
            player.endurance += 0.06f;
            player.noKnockback = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("FusedLens"));
            recipe.AddIngredient(mod.ItemType("GroundStick"));
            recipe.AddIngredient(mod.ItemType("ReinforcedPlating"));
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
