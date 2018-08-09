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
            item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Soulcheck.GetValue("Inferno Buff"))
            {
                player.inferno = true;
                Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.65f, 0.4f, 0.1f);
                int buff = BuffID.OnFire;
                float distance = 200f;
                bool doDmg = player.infernoCounter % 60 == 0;
                int damage = (int)(10 * player.meleeDamage);

                if (player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && !npc.friendly && npc.damage > 0 && !npc.dontTakeDamage && !npc.buffImmune[buff] && Vector2.Distance(player.Center, npc.Center) <= distance)
                        {
                            if (npc.FindBuffIndex(buff) == -1)
                            {
                                npc.AddBuff(buff, 120);
                            }
                            if (doDmg)
                            {
                                player.ApplyDamageToNPC(npc, damage, 0f, 0, false);
                            }
                        }
                    }
                }
            }

            //explode on death
            player.GetModPlayer<FargoPlayer>(mod).MoltenEnchant = true;
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