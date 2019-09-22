using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;
using Terraria.Localization;

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
Effects of Permafrost's Concoction and Regenerator
Summons a Bear and Third Sage pet");
            DisplayName.AddTranslation(GameCulture.Chinese, "代达罗斯魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'冰霜魔法保护着你...'
33%概率将抛射物反射回敌人
反射时将回复此抛射物伤害1/5的生命值
被攻击时爆发魔晶碎片
受攻击时有10%概率吸收物理攻击和抛射物
吸收时将回复此攻击伤害1/2的生命值
代达罗斯水晶将保护你
盗贼抛射物会在飞行中会射出魔晶碎片
拥有佩码·福洛斯特之融魔台和再生器的效果");
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

            if (SoulConfig.Instance.GetValue("Daedalus Effects"))
            {
                modPlayer.daedalusReflect = true;
                modPlayer.daedalusShard = true;
                modPlayer.daedalusAbsorb = true;
                modPlayer.daedalusCrystal = true;
                modPlayer.daedalusSplit = true;
            }
            
            if (SoulConfig.Instance.GetValue("Permafrost's Concoction"))
            {
                //permafrost concoction
                modPlayer.permafrostsConcoction = true;
            }
            
            if (player.GetModPlayer<FargoPlayer>().Eternity) return;

            if (SoulConfig.Instance.GetValue("Daedalus Crystal Minion") && player.whoAmI == Main.myPlayer)
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

            //regenerator
            if (SoulConfig.Instance.GetValue("Regenator"))
                modPlayer.regenator = true;

            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>(mod);
            fargoPlayer.DaedalusEnchant = true;
            fargoPlayer.AddPet("Third Sage Pet", hideVisual, calamity.BuffType("ThirdSageBuff"), calamity.ProjectileType("ThirdSage"));
            fargoPlayer.AddPet("Bear Pet", hideVisual, calamity.BuffType("BearBuff"), calamity.ProjectileType("Bear"));
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("FargowiltasSouls:AnyDaedalusHelmet");
            recipe.AddIngredient(calamity.ItemType("DaedalusBreastplate"));
            recipe.AddIngredient(calamity.ItemType("DaedalusLeggings"));
            recipe.AddIngredient(calamity.ItemType("PermafrostsConcoction"));
            recipe.AddIngredient(calamity.ItemType("Cryophobia"));
            recipe.AddIngredient(calamity.ItemType("Roxcalibur"));
            recipe.AddIngredient(calamity.ItemType("CrystalBlade"));
            recipe.AddIngredient(calamity.ItemType("CrystalFlareStaff"));
            recipe.AddIngredient(calamity.ItemType("KelvinCatalyst"));
            recipe.AddIngredient(calamity.ItemType("SlagMagnum"));
            recipe.AddIngredient(calamity.ItemType("Arbalest"));
            recipe.AddIngredient(calamity.ItemType("SHPC"));
            recipe.AddIngredient(calamity.ItemType("IbarakiBox"));
            recipe.AddIngredient(calamity.ItemType("BearEye"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
