using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class DaedalusEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daedalus Enchantment");
            Tooltip.SetDefault(
@"'Icy magic envelopes you...'
You have a 33% chance to reflect projectiles back at enemies
If you reflect a projectile you are also healed for 1/5 of that projectile's damage
Getting hit causes you to emit a blast of crystal shards
You have a 10% chance to absorb physical attacks and projectiles when hit
If you absorb an attack you are healed for 1/2 of that attack's damage
A daedalus crystal floats above you to protect you
Rogue projectiles throw out crystal shards as they travel
Effects of Permafrost's Concoction");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 500000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);

            if (Soulcheck.GetValue("Daedalus Effects"))
            {
                modPlayer.daedalusReflect = true;
                modPlayer.daedalusShard = true;
                modPlayer.daedalusAbsorb = true;
                modPlayer.daedalusCrystal = true;
                modPlayer.daedalusSplit = true;
            }
            
            if (Soulcheck.GetValue("Permafrost's Concoction"))
            {
                //permafrost concoction
                modPlayer.permafrostsConcoction = true;
            }
            
            if (player.GetModPlayer<FargoPlayer>().Eternity) return;

            if (Soulcheck.GetValue("Daedalus Crystal Minion") && player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(calamity.BuffType("DaedalusCrystal")) == -1)
                {
                    player.AddBuff(calamity.BuffType("DaedalusCrystal"), 3600, true);
                }
                if (player.ownedProjectileCounts[calamity.ProjectileType("DaedalusCrystal")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("DaedalusCrystal"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("DaedalusHelm"));
            recipe.AddIngredient(calamity.ItemType("DaedalusHelmet"));
            recipe.AddIngredient(calamity.ItemType("DaedalusHat"));
            recipe.AddIngredient(calamity.ItemType("DaedalusHeadgear"));
            recipe.AddIngredient(calamity.ItemType("DaedalusVisor"));
            recipe.AddIngredient(calamity.ItemType("DaedalusBreastplate"));
            recipe.AddIngredient(calamity.ItemType("DaedalusLeggings"));
            recipe.AddIngredient(calamity.ItemType("FrostFlare"));
            recipe.AddIngredient(calamity.ItemType("PermafrostsConcoction"));
            recipe.AddIngredient(calamity.ItemType("CrystalBlade"));
            recipe.AddIngredient(calamity.ItemType("CrystalFlareStaff"));
            recipe.AddIngredient(calamity.ItemType("SlagMagnum"));
            recipe.AddIngredient(calamity.ItemType("ProporsePistol"));
            recipe.AddIngredient(calamity.ItemType("SHPC"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
