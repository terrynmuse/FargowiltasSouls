using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces.Thorium
{
    public class VanaheimForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Vanaheim");
            Tooltip.SetDefault(
@"'Holds a glimpse of the future...'
All armor bonuses from Malignant, Folv, and White Dwarf
All armor bonuses from Celestial and Balladeer
Effects of Mana-Charged Rocketeers and Ascension Statuette");
            DisplayName.AddTranslation(GameCulture.Chinese, "华纳海姆之力");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'对未来的一瞥...'
神秘屏障环绕周身
每7次攻击会释放魔法箭
暴击释放长时间的宇宙星光和虚空之焰吞没敌人
按下'特殊能力'键将在光标处召唤无比强大的光环
召唤光环消耗150法力
每拥有一种咒音, 获得以下增益:
增加8%伤害
增加3%移动速度
拥有魔力充能火箭靴和飞升雕像效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            //folv
            modPlayer.MalignantEnchant = true;
            modPlayer.FolvEnchant = true;
            if (SoulConfig.Instance.GetValue("Folv's Aura"))
            {
                thoriumPlayer.folvSet = true;
                Lighting.AddLight(player.position, 0.03f, 0.3f, 0.5f);
                thoriumPlayer.folvBonus2 = true;
            }
            if (SoulConfig.Instance.GetValue("Mana-Charged Rocketeers"))
            {
                //mana charge rockets
                thorium.GetItem("ManaChargedRocketeers").UpdateAccessory(player, hideVisual);
            }

            //white dwarf
            modPlayer.WhiteDwarfEnchant = true;
            
            if (SoulConfig.Instance.GetValue("Celestial Aura"))
            {
                //celestial
                thoriumPlayer.celestialSet = true;
            }

            if (modPlayer.ThoriumSoul) return;

            if (SoulConfig.Instance.GetValue("Ascension Statuette"))
            {
                //ascension statue
                thoriumPlayer.ascension = true;
            }
            
            //balladeer meme hell
            if (thoriumPlayer.empowerDamage > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerAttackSpeed > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerCriticalStrike > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerMovementSpeed > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerInspirationRegen > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerDamageReduction > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerManaRegen > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerMaxMana > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerLifeRegen > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerMaxLife > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerDefense > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
            if (thoriumPlayer.empowerAmmoConsumption > 0)
            {
                modPlayer.AllDamageUp(.08f);
                player.moveSpeed += 0.03f;
            }
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "FolvEnchant");
            recipe.AddIngredient(null, "WhiteDwarfEnchant");
            recipe.AddIngredient(null, "CelestialEnchant");
            recipe.AddIngredient(null, "BalladeerEnchant");

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
