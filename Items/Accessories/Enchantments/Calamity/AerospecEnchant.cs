using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class AerospecEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aerospec Enchantment");
            Tooltip.SetDefault(
@"'The sky comes to your aid…'
You fall quicker and are immune to fall damage
Taking over 25 damage in one hit causes several homing feathers to fall
Summons a Valkyrie minion to protect you
Effects of Gladiator's Locket and Unstable Prism");
            DisplayName.AddTranslation(GameCulture.Chinese, "天蓝魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"天空向你施以援手...
下落得更快,免疫坠落伤害
一次性承受超过25点伤害时,会落下出数根追踪羽毛
召唤天武神来保护你
拥有角斗士链坠和不稳定棱镜的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);
            modPlayer.aeroSet = true;
            player.noFallDmg = true;

            if (player.GetModPlayer<FargoPlayer>().Eternity) return;

            if (Soulcheck.GetValue("Valkyrie Minion"))
            {
                modPlayer.valkyrie = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("Valkyrie")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("Valkyrie"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.ProjectileType("Valkyrie")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("Valkyrie"), 25, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            calamity.GetItem("GladiatorsLocket").UpdateAccessory(player, hideVisual);
            calamity.GetItem("UnstablePrism").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("AerospecHelm"));
            recipe.AddIngredient(calamity.ItemType("AerospecHood"));
            recipe.AddIngredient(calamity.ItemType("AerospecHat"));
            recipe.AddIngredient(calamity.ItemType("AerospecHelmet"));
            recipe.AddIngredient(calamity.ItemType("AerospecHeadgear"));
            recipe.AddIngredient(calamity.ItemType("AerospecBreastplate"));
            recipe.AddIngredient(calamity.ItemType("AerospecLeggings"));
            recipe.AddIngredient(calamity.ItemType("GladiatorsLocket"));
            recipe.AddIngredient(calamity.ItemType("UnstablePrism"));
            recipe.AddIngredient(calamity.ItemType("Galeforce"));
            recipe.AddIngredient(calamity.ItemType("StormSurge"));
            recipe.AddIngredient(calamity.ItemType("SkyGlaze"));
            recipe.AddIngredient(calamity.ItemType("PerfectDark"));
            recipe.AddIngredient(calamity.ItemType("SausageMaker"));
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
