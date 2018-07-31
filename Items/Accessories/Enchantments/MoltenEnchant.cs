using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class MoltenEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molten Enchantment");
            Tooltip.SetDefault(
@"'They shall know the fury of hell.' 
Nearby enemies are ignited
When you die, you violently explode dealing massive damage to surrounding enemies");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //explode on death
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.MoltenEnchant = true;

            if (Soulcheck.GetValue("Inferno Buff"))
            {
                player.inferno = true;
                Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.65f, 0.4f, 0.1f);
                int num = 24;
                float num2 = 200f;
                bool flag = player.infernoCounter % 60 == 0;
                int damage = (int)(10 * player.meleeDamage);
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int l = 0; l < 200; l++)
                    {
                        NPC nPc = Main.npc[l];
                        if (nPc.active && !nPc.friendly && nPc.damage > 0 && !nPc.dontTakeDamage && !nPc.buffImmune[num] && Vector2.Distance(player.Center, nPc.Center) <= num2)
                        {
                            if (nPc.FindBuffIndex(num) == -1)
                            {
                                nPc.AddBuff(num, 120);
                            }
                            if (flag)
                            {
                                player.ApplyDamageToNPC(nPc, damage, 0f, 0, false);
                            }
                        }
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MoltenHelmet);
            recipe.AddIngredient(ItemID.MoltenBreastplate);
            recipe.AddIngredient(ItemID.MoltenGreaves);
            recipe.AddIngredient(ItemID.FieryGreatsword);
            recipe.AddIngredient(ItemID.Sunfury);
            recipe.AddIngredient(ItemID.DemonsEye);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}