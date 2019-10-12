using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class CosmoForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Cosmos");

            string tooltip =
@"'Been around since the Big Bang'
";
            string tooltip_ch =
@"'自宇宙大爆炸以来就一直存在'
";

            tooltip +=
@"A meteor shower initiates every few seconds while attacking
Solar shield allows you to dash through enemies
Attacks may inflict the Solar Flare debuff
Double tap down to toggle stealth, reducing chance for enemies to target you but slowing movement
You also spawn a vortex to draw in and massively damage enemies when you enter stealth
Hurting enemies has a chance to spawn buff boosters
Double tap down to direct your empowered guardian
Press the Freeze Key to freeze time for 5 seconds
There is a 60 second cooldown for this effect, a sound effect plays when it's back
Summons a pet Companion Cube";

            tooltip_ch +=
@"攻击时,每隔几秒就会爆发一次流星雨
日耀护盾允许你向敌人冲刺
攻击概率造成耀斑效果
双击'下'键切换潜行,减少敌人攻击你的概率, 但减少移动速度
进入潜行时, 生成一个漩涡, 聚拢敌人并造成大量伤害
杀死敌人有概率产生增益效果
双击'下'键控制你的强化替身
按下时间冻结热键时停5秒
该能力有60秒的冷却时间, 冷却结束时会播放音效
召唤一个伙伴方块";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "宇宙之力");
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
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            //meme speed, solar flare, white dwarf flames, tide turner daggers, pyro bursts, assassin insta kill
            modPlayer.CosmoForce = true;

            //meteor shower
            modPlayer.MeteorEffect(75);
            //solar shields
            modPlayer.SolarEffect();
            //flare debuff
            modPlayer.SolarEnchant = true;
            //stealth, voids, pet
            modPlayer.VortexEffect(hideVisual);
            //boosters and meme speed
            modPlayer.NebulaEffect();
            //guardian and time freeze
            modPlayer.StardustEffect();
            modPlayer.AddPet("Suspicious Eye Pet", hideVisual, BuffID.SuspiciousTentacle, ProjectileID.SuspiciousTentacle);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "MeteorEnchant");
            recipe.AddIngredient(null, "SolarEnchant");
            recipe.AddIngredient(null, "VortexEnchant");
            recipe.AddIngredient(null, "NebulaEnchant");
            recipe.AddIngredient(null, "StardustEnchant");
            recipe.AddIngredient(ItemID.SuspiciousLookingTentacle);

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}