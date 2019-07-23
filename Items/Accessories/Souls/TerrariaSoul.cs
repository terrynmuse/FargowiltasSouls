using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class TerrariaSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;
        public bool allowJump = true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Terraria");

            string tooltip = 
@"'A true master of Terraria'
";
            string tooltip_ch =
@"'真·泰拉之主'
";

            if (thorium == null)
            {
                tooltip +=
@"Summons fireballs, icicles, a leaf crystal, Hallowed sword and shield, Beetles, and lots of pets
Toggle vanity to remove all Pets, Right Click to Guard
Double tap down to call an ancient storm, toggle stealth, spawn a portal, and direct your guardian
Gold Key to be encased in a Gold, Freeze Key to freeze time for 5 seconds, minions spew scythes 
Solar shield allows you to dash, Dash into any walls, to teleport through them
Attacks may spawn lightning, flower petals, spectre orbs, a Dungeon Guardian, or buff boosters
Attacks cause increased life regen, shadow dodge, meteor showers, reduced enemy knockback immunity
Critical chance is set to 25%, Crit to increase it by 5%, At 100% every 10th attack gains 4% life steal
Getting hit drops your crit back down, releases a spore explosion and reflects damage
One attack gains 5% life steal every second, capped at 5 HP
Projectiles may split or shatter, Stars heal twice as much";
                tooltip_ch +=
@"召唤火球,冰柱,叶绿水晶,神圣剑盾,甲虫和许多宠物
切换可见度以移除所有宠物,右键用盾防御
双击'下'键召唤远古风暴,切换潜行,生成一个传送门,指挥你的替身
按下金身热键,使自己被包裹在一个黄金壳中,按下时间冻结热键时停5秒,召唤物发出镰刀
日耀护盾允许你双击冲刺,遇到墙壁自动穿透
攻击可以产生闪电,花瓣,幽灵球,地牢守卫者,或者增益
攻击增加生命回复,暗影闪避,流星雨,降低敌人的击退免疫
暴击率设为25%,每次暴击增加5%,达到100%时,每10次攻击附带4%生命偷取
被击中会降低暴击率,并释放出孢子爆炸,并反弹伤害
攻击获得每秒5%的生命窃取,上限为5点
抛射物可能会分裂或散开,心和星星的回复加倍";
            }
            else
            {
                tooltip +=
@"Summons fireballs, icicles, a leaf crystal, Hallowed sword and shield, Beetles, and lots of pets
Toggle vanity to remove all Pets
Double tap down to call an ancient storm, toggle stealth, spawn a portal, and direct your guardian
Gold Key to be encased in a Gold, Freeze Key to freeze time for 5 seconds, minions spew scythes 
Solar shield allows you to dash, Dash into any walls, to teleport through them
Attacks may spawn flower petals, spectre orbs, a Dungeon Guardian, or buff boosters
Attacks cause increased life regen and reduced enemy knockback immunity
Critical chance is set to 25%, Crit to increase it by 5%, At 100% every 10th attack gains 4% life steal
Getting hit drops your crit back down, releases a spore explosion and reflects damage
One attack gains 5% life steal every second, capped at 5 HP
Projectiles may split or shatter, Stars heal twice as much";
                tooltip_ch +=
@"召唤火球,冰柱,叶绿水晶,神圣剑盾,甲虫和许多宠物
切换可见度以移除所有宠物
双击'下'键召唤远古风暴,切换潜行,生成一个传送门,指挥你的替身
按下金身热键,使自己被包裹在一个黄金壳中,按下时间冻结热键时停5秒,召唤物发出镰刀
日耀护盾允许你双击冲刺,遇到墙壁自动穿透
攻击可以产生花瓣,幽灵球,地牢守卫者,或者增益
攻击增加生命回复,降低敌人的击退免疫
暴击率设为25%,每次暴击增加5%,达到100%时,每10次攻击附带4%生命偷取
被击中会降低暴击率,并释放出孢子爆炸,并反弹伤害
攻击获得每秒5%的生命窃取,上限为5点
抛射物可能会分裂或散开,心的回复加倍";
            }

            tooltip += 
@"
Nearby enemies are ignited, You leave behind a trail of fire when you walk
Most other effects of material Forces
When you die, you explode and revive with 200 HP";
            tooltip_ch +=
@"
点燃附近敌人,在身后留下火焰路径
材料魂的绝大部分效果
死亡时,爆炸并且回复200生命";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "泰拉瑞亚之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);

            /*
             -not listed
             Throw a smoke bomb to teleport to it
             Your attacks inflict Midas
            20% chance for enemies to drop 8x loot
            Effects of Hive Pack, Flower Boots, Master Ninja Gear, Celestial Shell, Shiny Stone, and Greedy Ring
            Your weapon's projectiles occasionally shoot from the shadows of where you used to be
            Enemies will explode into needles on death  
            
            THORIUM
            Effects of Proof of Avarice
            Killing enemies or continually damaging bosses generates soul wisps
            After generating 5 wisps, they are instantly consumed to heal you for 10 life

            Attack speed is increased by 5% at every 25% segment of life
            Effects of Spring Steps and Slag Stompers


             * */

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 24));
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.value = 5000000;
            item.shieldSlot = 5;

            item.rare = -12;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //includes revive, both spectres, adamantite, and star heal
            modPlayer.TerrariaSoul = true;

            //WOOD
            modPlayer.WoodForce = true;
            //wood
            modPlayer.WoodEnchant = true;
            //boreal
            modPlayer.BorealEnchant = true;
            //mahogany
            modPlayer.MahoganyEnchant = true;
            //ebon
            modPlayer.EbonEffect();
            //shade
            modPlayer.ShadeEnchant = true;
            //palm
            modPlayer.PalmEffect();
            //pearl
            modPlayer.PearlEffect();

            //TERRA
            modPlayer.TerraForce = true; //crit effect improved

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                modPlayer.CopperEnchant = true; //lightning
                player.dash = 2;
                if (Soulcheck.GetValue("Iron Shield"))
                {
                    modPlayer.IronEffect();
                }
                if (Soulcheck.GetValue("Iron Magnet"))
                {
                    modPlayer.IronEnchant = true;
                }
            }

            modPlayer.TinEffect(); //crits
            //tungsten
            modPlayer.TungstenEnchant = true;
            //obsidian
            player.fireWalk = true;
            player.lavaImmune = true;

            //EARTH
            modPlayer.CobaltEnchant = true; //shards
            modPlayer.PalladiumEffect(); //regen on hit, heals
            modPlayer.OrichalcumEffect(); //fireballs and petals
            modPlayer.AdamantiteEnchant = true; //split

            if (!Fargowiltas.Instance.ThoriumLoaded)
                modPlayer.TitaniumEffect(); //shadow dodge, full hp resistance

            //NATURE
            modPlayer.NatureForce = true;

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                modPlayer.CrimsonEffect(hideVisual); //regen, pets
            }

            modPlayer.MoltenEffect(25); //inferno and explode
            modPlayer.FrostEffect(75, hideVisual); //icicles, pets
            modPlayer.JungleEffect(); //spores
            modPlayer.ChloroEffect(hideVisual, 100); //crystal and pet
            modPlayer.ShroomiteEffect(hideVisual); //pet

            //LIFE
            modPlayer.LifeForce = true; 
            modPlayer.BeeEffect(hideVisual); //bees ignore defense, super bees, pet
            modPlayer.SpiderEffect(hideVisual); //pet
            modPlayer.BeetleEffect(); //defense beetle bois
            modPlayer.PumpkinEffect(50, hideVisual); //flame trail, pie heal, pet
            modPlayer.TurtleEffect(hideVisual); //thorns, pets
            player.thorns = 1f;
            player.turtleThorns = true;
            modPlayer.CactusEffect(); //needle spray

            //SPIRIT
            modPlayer.SpiritForce = true; //spectre works for all, spirit trapper works for all
            modPlayer.FossilEffect(40, hideVisual); //revive, bone zone, pet
            modPlayer.ForbiddenEffect(); //storm
            modPlayer.HallowEffect(hideVisual, 100); //sword, shield, pet
            modPlayer.TikiEffect(hideVisual); //pet
            modPlayer.SpectreEffect(hideVisual); //pet

            //SHADOW
            modPlayer.ShadowForce = true; 
            modPlayer.DarkArtistEffect(hideVisual); //shoot from where you were meme, pet
            modPlayer.NecroEffect(hideVisual); //DG meme, pet
            modPlayer.ShadowEffect(hideVisual); //pets
            player.blackBelt = true;
            player.spikedBoots = 2;
            player.dash = 1;
            modPlayer.ShinobiEffect(hideVisual); //tele thru walls, pet
            modPlayer.NinjaEffect(hideVisual); //smoke bomb nonsense, pet
            modPlayer.SpookyEffect(hideVisual); //scythe doom, pets

            //WILL
            modPlayer.WillForce = true; //knockback remove for all
            modPlayer.GoldEffect(hideVisual); //midas, greedy ring, pet, zhonyas
            modPlayer.PlatinumEnchant = true; //loot multiply
            modPlayer.GladiatorEffect(hideVisual); //javelins, pet
            modPlayer.RedRidingEffect(hideVisual); //pet
            player.accMerman = true;
            player.wolfAcc = true;
            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }
            modPlayer.ValhallaEffect(hideVisual); //knockback remove
            player.shinyStone = true;

            //COSMOS
            modPlayer.CosmoForce = true; 

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                //meteor shower
                modPlayer.MeteorEffect(75);
            }

            modPlayer.SolarEffect(); //solar shields
            modPlayer.VortexEffect(hideVisual); //stealth, voids, pet
            modPlayer.NebulaEffect(); //boosters
            modPlayer.StardustEffect(); //guardian and time freeze
            modPlayer.AddPet("Suspicious Eye Pet", hideVisual, BuffID.SuspiciousTentacle, ProjectileID.SuspiciousTentacle);

            if(Fargowiltas.Instance.ThoriumLoaded)

            if (Fargowiltas.Instance.CalamityLoaded)
                mod.GetItem("CalamityForce").UpdateAccessory(player, hideVisual);
        }

        private void Thorium(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            //NATURE

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
            //slag stompers
            timer++;
            if (timer > 20)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0.1f * Main.rand.Next(-25, 25), 2f, thorium.ProjectileType("SlagPro"), 20, 1f, Main.myPlayer, 0f, 0f);
                timer = 0;
            }

            //WILL
            if (Soulcheck.GetValue("Proof of Avarice"))
            {
                //proof of avarice
                thoriumPlayer.avarice2 = true;
            }
            modPlayer.AddPet("Coin Bag Pet", hideVisual, thorium.BuffType("DrachmaBuff"), thorium.ProjectileType("DrachmaBag"));
            modPlayer.AddPet("Glitter Pet", hideVisual, thorium.BuffType("ShineDust"), thorium.ProjectileType("ShinyPet"));

            //COSMOS

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WoodForce");
            recipe.AddIngredient(null, "TerraForce");
            recipe.AddIngredient(null, "EarthForce");
            recipe.AddIngredient(null, "NatureForce");
            recipe.AddIngredient(null, "LifeForce");
            recipe.AddIngredient(null, "SpiritForce");
            recipe.AddIngredient(null, "ShadowForce");
            recipe.AddIngredient(null, "WillForce");
            recipe.AddIngredient(null, "CosmoForce");

            if (Fargowiltas.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(null, "CalamityForce");
                recipe.AddTile(ModLoader.GetMod("CalamityMod"), "DraedonsForge");
            }
            else
            {
                recipe.AddTile(mod, "CrucibleCosmosSheet");
            }
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}