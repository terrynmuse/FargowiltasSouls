using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class VictideEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Victide Enchantment");
            Tooltip.SetDefault(
@"'The former seas have energized you…'
When using any weapon you have a 10% chance to throw a returning seashell projectile
This seashell does true damage and does not benefit from any damage class
Summons a sea urchin to protect you
Effects of Deep Diver, The Transformer, and Luxor's Gift");
            DisplayName.AddTranslation(GameCulture.Chinese, "胜潮魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'前海给予你力量...'
攻击时有10%的概率抛出一个贝壳回旋镖
贝壳回旋镖造成真实伤害但不受任何增幅
召唤一个海胆保护你
拥有深潜者, 变压器和祖玛的礼物的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 80000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);
            //all
            modPlayer.victideSet = true;

            if (player.GetModPlayer<FargoPlayer>().Eternity) return;

            if (SoulConfig.Instance.GetValue("Victide Sea Urchin"))
            {
                //summon
                modPlayer.urchin = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("Urchin")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("Urchin"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.ProjectileType("Urchin")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("Urchin"), 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            calamity.GetItem("DeepDiver").UpdateAccessory(player, hideVisual);
            calamity.GetItem("TheTransformer").UpdateAccessory(player, hideVisual);
            if (SoulConfig.Instance.GetValue("Luxor's Gift"))
                calamity.GetItem("LuxorsGift").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("FargowiltasSouls:AnyVictideHelmet");
            recipe.AddIngredient(calamity.ItemType("VictideBreastplate"));
            recipe.AddIngredient(calamity.ItemType("VictideLeggings"));
            recipe.AddIngredient(calamity.ItemType("DeepDiver"));
            recipe.AddIngredient(calamity.ItemType("TheTransformer"));
            recipe.AddIngredient(calamity.ItemType("LuxorsGift"));
            recipe.AddIngredient(calamity.ItemType("RedtideSword"));
            recipe.AddIngredient(calamity.ItemType("DuneHopper"));
            recipe.AddIngredient(calamity.ItemType("TeardropCleaver"));
            recipe.AddIngredient(calamity.ItemType("MycelialClaws"));
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
