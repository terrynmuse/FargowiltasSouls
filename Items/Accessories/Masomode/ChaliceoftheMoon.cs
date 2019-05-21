using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class ChaliceoftheMoon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chalice of the Moon");
            Tooltip.SetDefault(@"'The moon smiles upon you'
Increases life regeneration
Grants immunity to Venom, Burning, Fused, Marked for Death, and Hexed
Grants immunity to Atrophied, Jammed, Reverse Mana Flow, and Antisocial
You periodically fire additional attacks depending on weapon type
You erupt into Ancient Visions when injured
Summons a friendly Cultist and plant to fight at your side");
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
