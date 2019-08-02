using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class ReaverEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reaver Enchantment");
            Tooltip.SetDefault(
@"'A thorny death awaits your enemies...'
Melee projectiles explode on hit
While using a ranged weapon you have a 10% chance to fire a powerful rocket
Your magic projectiles emit a burst of spore gas on enemy hits
Summons a reaver orb that emits spore gas when enemies are near
You emit a cloud of spores when you are hit
Rage activates when you are damaged");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 400000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);

            if (Soulcheck.GetValue("Reaver Effects"))
            {
                //melee
                modPlayer.reaverBlast = true;
                //range
                modPlayer.reaverDoubleTap = true;
                //magic
                modPlayer.reaverBurst = true;
                //throw
                modPlayer.reaverSpore = true;
            }
            
            if (player.GetModPlayer<FargoPlayer>().Eternity) return;

            if (Soulcheck.GetValue("Reaver Orb Minion"))
            {
                //summon
                modPlayer.reaverOrb = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("ReaverOrb")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("ReaverOrb"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.ProjectileType("ReaverOrb")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("ReaverOrb"), 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("ReaverHelm"));
            recipe.AddIngredient(calamity.ItemType("ReaverVisage"));
            recipe.AddIngredient(calamity.ItemType("ReaverMask"));
            recipe.AddIngredient(calamity.ItemType("ReaverHelmet"));
            recipe.AddIngredient(calamity.ItemType("ReaverCap"));
            recipe.AddIngredient(calamity.ItemType("ReaverScaleMail"));
            recipe.AddIngredient(calamity.ItemType("ReaverCuisses"));
            recipe.AddIngredient(calamity.ItemType("Animosity"));
            recipe.AddIngredient(calamity.ItemType("Tumbleweed"));
            recipe.AddIngredient(calamity.ItemType("Leviatitan"));
            recipe.AddIngredient(calamity.ItemType("Keelhaul"));
            recipe.AddIngredient(calamity.ItemType("Triploon"));
            recipe.AddIngredient(calamity.ItemType("Aeries"));
            recipe.AddIngredient(calamity.ItemType("MagnaStriker"));
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
