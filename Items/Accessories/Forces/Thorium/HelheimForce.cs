using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces.Thorium
{
    public class HelheimForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Helheim");
            Tooltip.SetDefault(
@"'From the halls of Hel, a vision of the end...'
Killing enemies or continually damaging bosses generates soul wisps
After generating 5 wisps, they are instantly consumed to heal you for 10 life
Your boots vibrate at an unreal frequency, increasing movement speed significantly
While moving, your damage and critical strike chance are increased
Your attacks have a chance to unleash an explosion of Dragon's Flame
Dealing damage will grant you a 'Blood Charge'
At maximum charges, your next attack will deal 2x damage and heal you for 20% of the damage dealt
Consecutive attacks against enemies might drop flesh, which grants bonus life and damage
While above 75% maximum life, you become unstable
Enemies that attack friendly NPCs are marked as Villains
You deal 50% bonus damage to Villains
Your plague gas will linger in the air twice as long and your plague reactions will deal 20% more damage
Killing an enemy will release a soul fragment
Effects of Inner Flame, Crash Boots, Dragon Talon Necklace, and Grim Subwoofer
Effects of Vampire Gland, Demon Blood Badge, and Blood Demon's Subwoofer 
Effects of Shade Band, Lich's Gaze, and Plague Lord's Flask
Summons several pets");
            DisplayName.AddTranslation(GameCulture.Chinese, "海姆冥界之力");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'从海姆冥界的大厅起始, 一派终末的景象...'
杀死敌人或持续攻击Boss会产生灵魂碎片
集齐5个后, 它们会立即被消耗, 治疗10点生命
你的靴子以不真实的频率振动着, 显著提高移动速度
移动时增加伤害和暴击率
攻击有概率释放龙焰爆炸
攻击提供'蓄血'Buff
充能完毕时, 你的下一次攻击会造成双倍伤害, 并将伤害的20%转化为治疗
连续攻击敌人时概率掉落肉, 拾取肉会获得额外生命并增加伤害
生命值高于75%时变得不稳定
攻击友善NPC的敌人将被标记为恶棍
对恶棍造成50%额外伤害
瓦斯持续时间翻倍, 瓦斯反应多造成20%伤害
杀死敌人会释放灵魂碎片
拥有心灵之火, 震地靴, 龙爪项链和恐惧音箱的效果
拥有吸血鬼试剂, 魔血纹章和血魔音箱的效果
拥有暗影护符, 巫妖之视和瘟疫之主药剂瓶的效果
召唤数个宠物");
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
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            //spirit trapper
            modPlayer.SpiritTrapperEnchant = true;

            //inner flame
            thoriumPlayer.spiritFlame = true;

            if (Soulcheck.GetValue("Dread Speed"))
            {
                //dread
                player.moveSpeed += 0.8f;
                player.maxRunSpeed += 10f;
                player.runAcceleration += 0.05f;
                if (player.velocity.X > 0f || player.velocity.X < 0f)
                {
                    modPlayer.AllDamageUp(.25f);
                    modPlayer.AllCritUp(20);

                    for (int i = 0; i < 2; i++)
                    {
                        int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 65, 0f, 0f, 0, default(Color), 1.75f);
                        int num2 = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 75, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num].noGravity = true;
                        Main.dust[num2].noGravity = true;
                        Main.dust[num].noLight = true;
                        Main.dust[num2].noLight = true;
                    }
                }
            }

            //crash boots
            thorium.GetItem("CrashBoots").UpdateAccessory(player, hideVisual);
            player.moveSpeed -= 0.15f;
            player.maxRunSpeed -= 1f;
            //woofers
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerCursed = true;
                    thoriumPlayer.empowerIchor = true;
                }
            }
            if (Soulcheck.GetValue("Dragon Flames"))
            {
                //dragon 
                thoriumPlayer.dragonSet = true;
            }
            //dragon tooth necklace
            player.armorPenetration += 15;
            //wyvern pet
            modPlayer.AddPet("Wyvern Pet", hideVisual, thorium.BuffType("WyvernPetBuff"), thorium.ProjectileType("WyvernPet"));
            thoriumPlayer.wyvernPet = true;

            //demon blood effect
            modPlayer.DemonBloodEnchant = true;
            //demon blood badge
            thoriumPlayer.CrimsonBadge = true;
            if (Soulcheck.GetValue("Flesh Drops"))
            {
                //flesh set bonus
                thoriumPlayer.Symbiotic = true;
            }
            if (Soulcheck.GetValue("Vampire Gland"))
            {
                //vampire gland
                thoriumPlayer.vampireGland = true;
            }
            //blister pet
            modPlayer.AddPet("Blister Pet", hideVisual, thorium.BuffType("BlisterBuff"), thorium.ProjectileType("BlisterPet"));
            thoriumPlayer.blisterPet = true;

            if (Soulcheck.GetValue("Harbinger Overcharge"))
            {
                //harbinger
                if (player.statLife > (int)(player.statLifeMax2 * 0.75))
                {
                    thoriumPlayer.overCharge = true;
                    modPlayer.AllDamageUp(.5f);
                }
            }
            //villain damage 
            modPlayer.KnightEnchant = true;
            //shade band
            thoriumPlayer.shadeBand = true;
            //pet
            modPlayer.AddPet("Moogle Pet", hideVisual, thorium.BuffType("LilMogBuff"), thorium.ProjectileType("LilMog"));
            modPlayer.KnightEnchant = true;

            //plague doctor
            thoriumPlayer.plagueSet = true;
            //lich gaze
            thoriumPlayer.lichGaze = true;
            modPlayer.PlagueAcc = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "SpiritTrapperEnchant");
            recipe.AddIngredient(null, "DreadEnchant");
            recipe.AddIngredient(null, "DemonBloodEnchant");
            recipe.AddIngredient(null, "HarbingerEnchant");
            recipe.AddIngredient(null, "PlagueDoctorEnchant");

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
