using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using CalamityMod.CalPlayer;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class StatigelEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Statigel Enchantment");
            Tooltip.SetDefault(
@"'Statis’ mystical power surrounds you…'
When you take over 100 damage in one hit you become immune to damage for an extended period of time
Grants an extra jump and increased jump height
Summons a mini slime god to fight for you, the type depends on what world evil you have
Effects of Counter Scarf and Fungal Symbiote");
            DisplayName.AddTranslation(GameCulture.Chinese, "斯塔提斯魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'斯塔提斯的神秘力量环绕着你...'
一次性承受超过100点伤害时, 加长无敌时间
能够多跳跃一次, 增加跳跃高度
召唤迷你史莱姆之神为你而战, 种类视世界而定
拥有反击围巾和真菌共生体的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            CalamityPlayer calamityPlayer = player.GetModPlayer<CalamityPlayer>();
            calamityPlayer.statigelSet = true;
            player.doubleJumpSail = true;
            player.jumpBoost = true;

            if (SoulConfig.Instance.GetValue("Slime God Minion"))
            {
                //summon
                calamityPlayer.slimeGod = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("SlimeGod")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("SlimeGod"), 3600, true);
                    }
                    if (WorldGen.crimson && player.ownedProjectileCounts[calamity.ProjectileType("SlimeGodAlt")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("SlimeGodAlt"), 33, 0f, Main.myPlayer, 0f, 0f);
                        return;
                    }
                    if (!WorldGen.crimson && player.ownedProjectileCounts[calamity.ProjectileType("SlimeGod")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("SlimeGod"), 33, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            calamity.GetItem("FungalSymbiote").UpdateAccessory(player, hideVisual);

            //counter scarf
            calamityPlayer.dodgeScarf = true;
            calamityPlayer.dashMod = 1;

           /* modPlayer.StatigelEnchant = true;
            //modPlayer.AddPet("Perforator Pet", hideVisual, calamity.BuffType("BloodBound"), calamity.ProjectileType("PerforaMini"));

            if (player.FindBuffIndex(calamity.BuffType("BloodBound")) == -1 && player.ownedProjectileCounts[calamity.ProjectileType("PerforaMini")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("PerforaMini"), 0, 0f, player.whoAmI);
            }*/
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("FargowiltasSouls:AnyStatigelHelmet");
            recipe.AddIngredient(calamity.ItemType("StatigelArmor"));
            recipe.AddIngredient(calamity.ItemType("StatigelGreaves"));
            recipe.AddIngredient(calamity.ItemType("CounterScarf"));
            recipe.AddIngredient(calamity.ItemType("ManaOverloader"));
            recipe.AddIngredient(calamity.ItemType("FungalSymbiote"));
            recipe.AddIngredient(calamity.ItemType("Carnage"));
            recipe.AddIngredient(calamity.ItemType("Waraxe"));
            recipe.AddIngredient(calamity.ItemType("ClothiersWrath"));
            recipe.AddIngredient(calamity.ItemType("BloodyVein"));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
