using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class NatureForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;
        public bool allowJump = true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Nature");

            string tooltip =
@"'Tapped into every secret of the wilds'
";
            string tooltip_ch =
@"'挖掘了野外的每一个秘密'
";

            if (thorium == null)
            {
                tooltip +=
@"Greatly increases life regen
Hearts heal for 1.5x as much
Nearby enemies are ignited
When you die, you violently explode dealing massive damage
Icicles will start to appear around you
When there are three, attacking will launch them towards the cursor
Taking damage will release a poisoning spore explosion
Summons a leaf crystal to shoot at nearby enemies
Not moving puts you in stealth
While in stealth, crits deal 4x damage
Summons several pets";
                tooltip_ch +=
@"极大增加生命恢复速度
心获得1.5倍治疗量
点燃附近敌人
死亡时剧烈爆炸,造成大量伤害
冰柱将出现在你周围
当存在3枚时,攻击会将它们向光标位置发射
受到伤害会释放出有毒的孢子爆炸
召唤一个叶绿水晶向射击附近的敌人
站立不动时潜行
潜行时,暴击造成4倍伤害
召唤数个宠物";
            }
            else
            {
                tooltip +=
@"Greatly increases life regen and Hearts heal for 1.5x as much
Nearby enemies are ignited
When you die, you violently explode dealing massive damage
Attack speed is increased by 5% at every 25% segment of life
Enemies that you set on fire or singe will take additional damage over time
Icicles will start to appear around you
When there are three, attacking will launch them towards the cursor
Taking and dealing damage will release a poisoning spore explosion
Summons a leaf crystal to shoot at nearby enemies
Not moving puts you in stealth, While in stealth, crits deal 4x damage
Attacks may inflict Fungal Growth
Effects of Night Shade Petal, Sub-Zero Subwoofer, and Toxic Subwoofer 
Effects of Spring Steps and Slag Stompers
Summons several pets";
                tooltip_ch +=
@"极大增加生命恢复速度,心获得1.5倍治疗量
点燃附近敌人
死亡时剧烈爆炸,造成大量伤害
生命值每下降25%,增加5%攻击速度
随着时间的推移,被你点燃或烧伤的敌人会受到额外的伤害
冰柱将出现在你周围
当存在3枚时,攻击会将它们向光标位置发射
受到伤害会释放出有毒的孢子爆炸
召唤一个叶绿水晶向射击附近的敌人
站立不动时潜行,潜行时,暴击造成4倍伤害
攻击造成真菌寄生效果
拥有影缀花,零度音箱和剧毒音箱的效果
拥有弹簧鞋和熔渣重踏的效果
召唤数个宠物";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "自然之力");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
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
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //bulb, cryo effect
            modPlayer.NatureForce = true;

            //regen, hearts heal more, pets
            modPlayer.CrimsonEffect(hideVisual);

            //inferno and explode
            modPlayer.MoltenEffect(25);
            //icicles, pets
            modPlayer.FrostEffect(75, hideVisual);
            //spores
            modPlayer.JungleEffect();
            //crystal and pet
            modPlayer.ChloroEffect(hideVisual, 100);
            //stealth, crits, pet
            modPlayer.ShroomiteEffect(hideVisual);

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player, hideVisual); ;
        }

        private void Thorium(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //subwoofer
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerPoison = true;
                    thoriumPlayer.empowerFrost = true;
                }
            }
            //night shade petal
            thoriumPlayer.nightshadeBoost = true;

            thoriumPlayer.orbital = true;
            thoriumPlayer.orbitalRotation3 = Utils.RotatedBy(thoriumPlayer.orbitalRotation3, -0.075000002980232239, default(Vector2));
            //making divers code less of a meme :scuseme:
            if (player.statLife > player.statLifeMax * 0.75)
            {
                thoriumPlayer.berserkStage = 1;
            }
            else if (player.statLife > player.statLifeMax * 0.5)
            {
                modPlayer.AttackSpeed *= 1.05f;
                thoriumPlayer.berserkStage = 2;
            }
            else if (player.statLife > player.statLifeMax * 0.25)
            {
                modPlayer.AttackSpeed *= 1.1f;
                thoriumPlayer.berserkStage = 3;
            }
            else
            {
                modPlayer.AttackSpeed *= 1.15f;
                thoriumPlayer.berserkStage = 4;
            }

            //magma
            thoriumPlayer.magmaSet = true;
            //spring steps
            player.extraFall += 10;
            if (player.velocity.Y < 0f && allowJump)
            {
                allowJump = false;
                thoriumPlayer.jumps++;
            }
            if (player.velocity.Y > 0f || player.sliding || player.justJumped)
            {
                allowJump = true;
            }
            if (thoriumPlayer.jumps == 0)
            {
                player.jumpSpeedBoost += 5f;
            }
            if (thoriumPlayer.jumps == 1)
            {
                player.jumpSpeedBoost += 1f;
            }
            if (thoriumPlayer.jumps == 2)
            {
                player.jumpSpeedBoost += 1.75f;
            }
            if (thoriumPlayer.jumps >= 3)
            {
                float num = 16f;
                int num2 = 0;
                while (num2 < num)
                {
                    Vector2 vector = Vector2.UnitX * 0f;
                    vector += -Utils.RotatedBy(Vector2.UnitY, (num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(5f, 20f);
                    vector = Utils.RotatedBy(vector, Utils.ToRotation(player.velocity), default(Vector2));
                    int num3 = Dust.NewDust(player.Center, 0, 0, 127, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num3].scale = 1.35f;
                    Main.dust[num3].noGravity = true;
                    Main.dust[num3].position = player.Center + vector;
                    Dust dust = Main.dust[num3];
                    dust.position.Y = dust.position.Y + 12f;
                    Main.dust[num3].velocity = player.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                    int num4 = num2;
                    num2 = num4 + 1;
                }
                Main.PlaySound(SoundID.Item74, player.position);
                thoriumPlayer.jumps = 0;
            }
            if (Soulcheck.GetValue("Slag Stompers"))
            {
                //slag stompers
                timer++;
                if (timer > 20)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0.1f * Main.rand.Next(-25, 25), 2f, thorium.ProjectileType("SlagPro"), 20, 1f, Main.myPlayer, 0f, 0f);
                    timer = 0;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "CrimsonEnchant");
            
            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "FrostEnchant");
                recipe.AddIngredient(null, "ChlorophyteEnchant");
                recipe.AddIngredient(null, "ShroomiteEnchant");
                recipe.AddIngredient(null, "BerserkerEnchant"); 
            }
            else
            {
                recipe.AddIngredient(null, "MoltenEnchant");
                recipe.AddIngredient(null, "FrostEnchant");
                recipe.AddIngredient(null, "ChlorophyteEnchant");
                recipe.AddIngredient(null, "ShroomiteEnchant");
            }

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}