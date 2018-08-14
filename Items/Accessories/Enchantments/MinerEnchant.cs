using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class MinerEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Miner Enchantment");
            Tooltip.SetDefault(
@"'The planet trembles with each swing of your pick'
50% increased mining speed
Shows the location of enemies, traps, and treasures
You emit an aura of light
Summons a magic lantern");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            player.pickSpeed -= 0.5f;

            if (Soulcheck.GetValue("Spelunker Buff"))
            {
                player.findTreasure = true;
            }

            if (Soulcheck.GetValue("Hunter Buff"))
            {
                player.detectCreature = true;
            }

            if (Soulcheck.GetValue("Dangersense Buff"))
            {
                player.dangerSense = true;
            }

            if (Soulcheck.GetValue("Shine Buff"))
            {
                Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);
            }

            modPlayer.MinerEnchant = true;
            modPlayer.AddPet("Magic Lantern Pet", hideVisual, BuffID.MagicLantern, ProjectileID.MagicLantern);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MiningHelmet);
            recipe.AddIngredient(ItemID.MiningShirt);
            recipe.AddIngredient(ItemID.MiningPants);
            recipe.AddIngredient(ItemID.BonePickaxe);
            recipe.AddIngredient(ItemID.MoltenPickaxe);
            recipe.AddIngredient(ItemID.MagicLantern);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}