using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CalamityMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class TarragonEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tarragon Enchantment");
            Tooltip.SetDefault(
@"'Braelor's undying might flows through you...'
Increased heart pickup range
Enemies have a chance to drop extra hearts on death
You have a 25% chance to regen health quickly when you take damage
Press Y to cloak yourself in life energy that heavily reduces enemy contact damage for 10 seconds
Ranged critical strikes will cause an explosion of leaves
Ranged projectiles have a chance to split into life energy on death
On every 5th critical strike you will fire a leaf storm
Magic projectiles have a 50% chance to heal you on enemy hits
At full health you gain +2 max minions and 10% increased minion damage
Summons a life aura around you that damages nearby enemies
After every 25 rogue critical hits you will gain 5 seconds of damage immunity
While under the effects of a debuff you gain 10% increased rogue damage
Effects of the Profaned Soul Artifact");
            DisplayName.AddTranslation(GameCulture.Chinese, "龙蒿魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'布拉洛的不死之力从你身上流过...'
增加心拾取范围
敌人死后有概率掉落额外的心
被攻击时有25%概率快速回复生命
按'Y'键将自己笼罩在生命能量中, 极大减少敌人基础伤害, 持续10秒
远程暴击引发飞叶爆炸
远程抛射物杀死敌人后有概率分裂生命光束
每5次魔法暴击后引发叶绿风暴
魔法抛射物击中敌人时有50%的概率治愈玩家
满血时, +2最大召唤栏, 增加10%召唤伤害
召唤生命之环伤害附近的敌人
投掷暴击25次后, 获得5秒的无敌时间
Debuff状态下, 增加10%盗贼伤害
拥有渎魂神物的效果");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(0, 255, 200));
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 3000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);

            if (Soulcheck.GetValue("Tarragon Effects"))
            {
                modPlayer.tarraSet = true;
                //melee
                modPlayer.tarraMelee = true;
                //range
                modPlayer.tarraRanged = true;
                //magic
                modPlayer.tarraMage = true;
                //summon
                modPlayer.tarraSummon = true;
                //throw
                modPlayer.tarraThrowing = true;
            }
            
            if (Soulcheck.GetValue("Profaned Soul Artifact"))
            {
                //profaned soul artifact
                modPlayer.pArtifact = true;
            }
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("TarragonHelm"));
            recipe.AddIngredient(calamity.ItemType("TarragonVisage"));
            recipe.AddIngredient(calamity.ItemType("TarragonMask"));
            recipe.AddIngredient(calamity.ItemType("TarragonHornedHelm"));
            recipe.AddIngredient(calamity.ItemType("TarragonHelmet"));
            recipe.AddIngredient(calamity.ItemType("TarragonBreastplate"));
            recipe.AddIngredient(calamity.ItemType("TarragonLeggings"));
            recipe.AddIngredient(calamity.ItemType("ProfanedSoulArtifact"));
            recipe.AddIngredient(calamity.ItemType("AquaticDissolution"));
            recipe.AddIngredient(calamity.ItemType("TrueTyrantYharimsUltisword"));
            recipe.AddIngredient(calamity.ItemType("Spyker"));
            recipe.AddIngredient(calamity.ItemType("DivineRetribution"));
            recipe.AddIngredient(calamity.ItemType("HandheldTank"));
            recipe.AddIngredient(calamity.ItemType("Mistlestorm"));

            //fuse helmets, add Thunderstorm

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
