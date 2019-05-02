using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class SupremeDeathbringerFairy : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supreme Deathbringer Fairy");
            Tooltip.SetDefault(@"'Supremacy not necessarily guaranteed'
Grants immunity to Slimed, Berserked, and Infested
Increases damage by 10% and armor penetration by 10
While dashing or running quickly you will create a trail of blood scythes
Your attacks inflict Poisoned
Bees and Hornets become friendly");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 5;
            item.value = Item.sellPrice(0, 4);
            item.defense = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();

            //slimy shield
            player.buffImmune[BuffID.Slimed] = true;
            if (Soulcheck.GetValue("Slimy Shield Effects"))
            {
                player.maxFallSpeed *= 2f;
                player.GetModPlayer<FargoPlayer>().SlimyShield = true;
            }

            //agitating lens
            player.buffImmune[mod.BuffType("Berserked")] = true;
            fargoPlayer.AllDamageUp(.10f);
            fargoPlayer.AgitatingLens = true;

            //queen stinger
            player.buffImmune[mod.BuffType("Infested")] = true;
            player.armorPenetration += 10;
            player.npcTypeNoAggro[210] = true;
            player.npcTypeNoAggro[211] = true;
            player.npcTypeNoAggro[42] = true;
            player.npcTypeNoAggro[176] = true;
            player.npcTypeNoAggro[231] = true;
            player.npcTypeNoAggro[232] = true;
            player.npcTypeNoAggro[233] = true;
            player.npcTypeNoAggro[234] = true;
            player.npcTypeNoAggro[235] = true;
            fargoPlayer.QueenStinger = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("AgitatingLens"));
            recipe.AddIngredient(mod.ItemType("QueenStinger"));
            recipe.AddIngredient(mod.ItemType("SlimyShield"));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
