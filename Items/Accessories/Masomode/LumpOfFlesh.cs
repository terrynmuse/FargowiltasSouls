using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class LumpOfFlesh : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lump of Flesh");
            Tooltip.SetDefault(@"Grants immunity to Rotting, the Tongue, and Dazed
Increases minion damage by 16% but slightly decreases defense
Increases your max number of minions by 2
Increases your max number of sentries by 2
The pungent eyeball charges energy to fire a laser as you attack
Enemies are less likely to target you
Makes armed and magic skeletons less hostile outside the Dungeon
'It's growing'");
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
            player.buffImmune[mod.BuffType("Rotting")] = true;
            player.buffImmune[BuffID.TheTongue] = true;
            player.buffImmune[BuffID.Dazed] = true;
            player.minionDamage += 0.16f;
            player.statDefense -= 6;
            player.aggro -= 400;
            player.GetModPlayer<FargoPlayer>().SkullCharm = true;
            player.GetModPlayer<FargoPlayer>().LumpOfFlesh = true;
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
            recipe.AddIngredient(ItemID.Ectoplasm, 6);
            recipe.AddIngredient(ItemID.SoulofLight, 12);
            recipe.AddIngredient(ItemID.SoulofNight, 12);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
