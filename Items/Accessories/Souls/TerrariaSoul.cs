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

Summons fireballs, icicles, a leaf crystal, Hallowed sword and shield, Beetles, and lots of pets
Toggle vanity to remove all Pets, Right Click to Guard
Double tap down to call an ancient storm, toggle stealth, spawn a portal, and direct your guardian
Gold Key to be encased in Gold, Freeze Key to freeze time for 5 seconds, minions spew scythes
Solar shield allows you to dash, Dash into any walls, to teleport through them
Attacks may spawn lightning, flower petals, spectre orbs, a Dungeon Guardian, or buff boosters
Attacks cause increased life regen, shadow dodge, meteor showers, reduced enemy knockback immunity
Critical chance is set to 25%, Crit to increase it by 5%, At 100% every 10th attack gains 4% life steal
Getting hit drops your crit back down, releases a spore explosion and reflects damage
One attack gains 5% life steal every second, capped at 5 HP
Projectiles may split or shatter, Stars heal twice as much,
Nearby enemies are ignited, You leave behind a trail of fire when you walk
Most other effects of material Forces
When you die, you explode and revive with 200 HP




Critters have massively increased defense
Killing critters no longer inflicts Guilty
When critters die, they release their souls to aid you
Every 5th attack will be accompanied by several snowballs
All grappling hooks pull you in and retract twice as fast
Any hook will periodically fire homing shots at enemies
You have a large aura of Shadowflame
When you take damage, you are inflicted with Super Bleeding
Double tap down to spawn a palm tree sentry that throws nuts at enemies
You leave behind a trail of rainbows that may shrink enemies

Attacks have a chance to shock enemies with lightning
Sets your critical strike chance to 10%
Every crit will increase it by 5%
Getting hit drops your crit back down
Allows the player to dash into the enemy
Right Click to guard with your shield
You attract items from a larger range
150% increased sword size
100% increased projectile size
Grants immunity to fire, fall damage, and lava

25% chance for your projectiles to explode into shards
20% increased weapon use speed
Greatly increases life regeneration after striking an enemy 
One attack gains 10% life steal every 4 seconds, capped at 8 HP
Flower petals will cause extra damage to your target 
Spawns 6 fireballs to rotate around you
Every 8th projectile you shoot will split into 3
Briefly become invulnerable after striking an enemy

Greatly increases life regen
Nearby enemies are ignited
When you die, you violently explode dealing massive damage
Icicles will start to appear around you
Taking damage will release a poisoning spore explosion
Summons a leaf crystal to shoot at nearby enemies
All herb collection is doubled
Not moving puts you in stealth
While in stealth, crits deal 4x damage
Effects of Flower Boots

You leave behind a trail of fire when you walk
Eating Pumpkin Pie heals you to full HP
100% of contact damage is reflected
Enemies may explode into needles on death
50% chance for any friendly bee to become a Mega Bee
15% chance for minions to crit
When standing still and not attacking, you gain the Shell Hide buff
Beetles protect you from damage

If you reach zero HP you cheat death, returning with 100 HP
For a few seconds after reviving, you are immune to all damage and spawn bones
Double tap down to call an ancient storm to the cursor location
Summons an Enchanted Sword familiar
You gain a shield that can reflect projectiles
Attacks will inflict Infested
Infested deals increasing damage over time
Damage has a chance to spawn damaging orbs

Your attacks may inflict Darkness on enemies
A Dungeon Guardian will occasionally annihilate a foe when struck
All of your minions may occasionally spew massive scythes everywhere
Throw a smoke bomb to teleport to it and gain the First Strike Buff
Using the Rod of Discord will also grant this buff
Dash into any walls, to teleport through them to the next opening
While attacking, Flameburst shots manifest themselves from your shadows
Greatly enhances Flameburst and Lightning Aura effectiveness
Effects of Master Ninja Gear

Your attacks inflict Midas and Super Bleed
Press the Gold hotkey to be encased in a Golden Shell
20% chance for enemies to drop 8x loot
Spears will rain down on struck enemies 
Your attacks deal increasing damage to low HP enemies
All attacks will slowly remove enemy knockback immunity
Greatly enhances Ballista and Explosive Traps effectiveness
Effects of Greedy Ring, Celestial Shell, and Shiny Stone

A meteor shower initiates every few seconds while attacking
Solar shield allows you to dash through enemies
Attacks may inflict the Solar Flare debuff
Double tap down to toggle stealth, reducing chance for enemies to target you but slowing movement
You also spawn a vortex to draw in and massively damage enemies when you enter stealth
Hurting enemies has a chance to spawn buff boosters
Double tap down to direct your guardian
Press the Freeze Key to freeze time for 5 seconds
Summons a pet Companion Cube
";
            string tooltip_ch =
@"'真·泰拉之主'
";

            if (thorium == null)
            {
                tooltip +=
@"";
                tooltip_ch +=
@"
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
@"";
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
@"";
            tooltip_ch +=
@"
点燃附近敌人,在身后留下火焰路径
材料魂的绝大部分效果
死亡时,爆炸并且回复200生命";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "泰拉之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);

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
            mod.GetItem("WoodForce").UpdateAccessory(player, hideVisual);
            //TERRA
            mod.GetItem("TerraForce").UpdateAccessory(player, hideVisual);
            //EARTH
            mod.GetItem("EarthForce").UpdateAccessory(player, hideVisual);
            //NATURE
            mod.GetItem("NatureForce").UpdateAccessory(player, hideVisual);
            //LIFE
            mod.GetItem("LifeForce").UpdateAccessory(player, hideVisual);
            //SPIRIT
            mod.GetItem("SpiritForce").UpdateAccessory(player, hideVisual);
            //SHADOW
            mod.GetItem("ShadowForce").UpdateAccessory(player, hideVisual);
            //WILL
            mod.GetItem("WillForce").UpdateAccessory(player, hideVisual);
            //COSMOS
            mod.GetItem("CosmoForce").UpdateAccessory(player, hideVisual);

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player, hideVisual);

            //if (Fargowiltas.Instance.CalamityLoaded)
             //   mod.GetItem("CalamityForce").UpdateAccessory(player, hideVisual);
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

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            /*if (Fargowiltas.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(null, "CalamityForce");
                recipe.AddTile(ModLoader.GetMod("CalamityMod"), "DraedonsForge");
            }*/

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}