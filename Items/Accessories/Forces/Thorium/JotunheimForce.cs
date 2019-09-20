using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces.Thorium
{
    public class JotunheimForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Jotunheim");
            Tooltip.SetDefault(
@"'A bitter cold, the power of the Jotuns...'
All armor bonuses from Depth Diver, Yew Wood, and Tide Hunter
All armor bonuses from Naga Skin, Icy, Cryo Magus, and Whispering
Effects of Sea Breeze Pendant, Bubble Magnet, and Deep Dark Subwoofer
Effects of Goblin War Shield, Agnor's Bowl, and Ice Bound Strider Hide
Summons several pets");
            DisplayName.AddTranslation(GameCulture.Chinese, "约顿海姆之力");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'彻骨严寒, 巨人的力量...'
获得水下呼吸能力
获得游泳和水下快速移动的能力
在水中时, 增加20%攻击速度
暴击释放飞溅泡沫, 缓慢附近的敌人
连续4次攻击不暴击时, 下一次远程攻击造成150%伤害
攻击将产生此次伤害值33%的冰刺攻击敌人, 并对敌人造成冻结效果
环绕的冰锥将冰冻敌人
偶尔在地面产生深渊能量触手攻击附近的敌人
最多产生6根触手, 触手的攻击将会为你偷取1点生命和法力
拥有海洋通行证, 泡泡磁铁和渊暗音箱的效果
拥有哥布林战盾, 琵琶鱼球碗和遁蛛契约的效果
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

            //bubble magnet
            thoriumPlayer.bubbleMagnet = true;
            modPlayer.DepthEnchant = true;
            modPlayer.AddPet("Jellyfish Pet", hideVisual, thorium.BuffType("JellyPet"), thorium.ProjectileType("JellyfishPet"));

            //tide hunter
            modPlayer.TideHunterEnchant = true;
            //angler bowl
            thorium.GetItem("AnglerBowl").UpdateAccessory(player, hideVisual);
            //yew wood
            modPlayer.YewEnchant = true;
            
            //strider hide
            thoriumPlayer.frostBonusDamage = true;
            //pets
            modPlayer.IcyEnchant = true;
            modPlayer.AddPet("Penguin Pet", hideVisual, BuffID.BabyPenguin, ProjectileID.Penguin);
            modPlayer.AddPet("Owl Pet", hideVisual, thorium.BuffType("SnowyOwlBuff"), thorium.ProjectileType("SnowyOwlPet"));

            if (SoulConfig.Instance.GetValue("Icy Barrier"))
            {
                //icy set bonus
                thoriumPlayer.icySet = true;
                if (player.ownedProjectileCounts[thorium.ProjectileType("IcyAura")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("IcyAura"), 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            //cryo
            modPlayer.CryoEnchant = true;
            
            if (SoulConfig.Instance.GetValue("Whispering Tentacles"))
            {
                mod.GetItem("WhisperingEnchant").UpdateAccessory(player, hideVisual);
            }

            if (modPlayer.ThoriumSoul) return;

            //water bonuses
            if (player.breath <= player.breathMax + 2)
            {
                player.breath = player.breathMax + 3;
            }
            //sea breeze pendant
            player.accFlipper = true;
            if (player.wet || thoriumPlayer.drownedDoubloon)
            {
                player.AddBuff(thorium.BuffType("AquaticAptitude"), 60, true);
                player.GetModPlayer<FargoPlayer>().AllDamageUp(.1f);
                modPlayer.AttackSpeed *= 1.2f;
            }
            //quicker in water
            player.ignoreWater = true;
            if (player.wet)
            {
                player.moveSpeed += 0.15f;
            }

            //depth woofer
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerGouge = true;
                }
            }

            //goblin war shield
            thorium.GetItem("GoblinWarshield").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "DepthDiverEnchant");
            recipe.AddIngredient(null, "TideHunterEnchant");
            recipe.AddIngredient(null, "NagaSkinEnchant");
            recipe.AddIngredient(null, "CryoMagusEnchant");
            recipe.AddIngredient(null, "WhisperingEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
