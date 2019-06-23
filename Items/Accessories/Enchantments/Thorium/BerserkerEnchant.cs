using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class BerserkerEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
       
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berserker Enchantment");
            Tooltip.SetDefault(
@"'I'd rather fight for my life than live it'
Damage is increased by 15% at every 25% segment of life
Fire surrounds your armour and melee weapons
Enemies that you set on fire or singe will take additional damage over time
Nearby enemies are ignited
When you die, you violently explode dealing massive damage
Effects of Spring Steps and Slag Stompers
Effects of Molten Spear Tip and Orange Music Player");
            DisplayName.AddTranslation(GameCulture.Chinese, "狂战士魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'我更愿意为自己的生命而战斗, 而不只是为活而活'
生命值每下降25%, 增加15%伤害
随着时间的推移,被你点燃或烧伤的敌人会受到额外的伤害
点燃附近敌人
死亡时剧烈爆炸,造成大量伤害
拥有弹簧鞋和熔渣重踏的效果
拥有炽热枪尖和橙色播放器的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.orbital = true;
            thoriumPlayer.orbitalRotation3 = Utils.RotatedBy(thoriumPlayer.orbitalRotation3, -0.075000002980232239, default(Vector2));
            //making divers code less of a meme :scuseme:
            if (player.statLife > player.statLifeMax * 0.75)
            {
                modPlayer.AllDamageUp(.15f);
                thoriumPlayer.berserkStage = 1;
            }
            else if (player.statLife > player.statLifeMax * 0.5)
            {
                modPlayer.AllDamageUp(.3f);
                thoriumPlayer.berserkStage = 2;
            }
            else if (player.statLife > player.statLifeMax * 0.25)
            {
                modPlayer.AllDamageUp(.45f);
                thoriumPlayer.berserkStage = 3;
            }
            else
            {
                modPlayer.AllDamageUp(.6f);
                thoriumPlayer.berserkStage = 4;
            }
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3AttackSpeed = 2;
            //magma
            mod.GetItem("MagmaEnchant").UpdateAccessory(player, hideVisual);
            //molten
            modPlayer.MoltenEffect(15);
        }
        
        private readonly string[] items =
        {
            "TunePlayerAttackSpeed",
            "SurtrsSword",
            "ThermogenicImpaler",
            "WyvernSlayer"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("BerserkerMask"));
            recipe.AddIngredient(thorium.ItemType("BerserkerBreastplate"));
            recipe.AddIngredient(thorium.ItemType("BerserkerGreaves"));
            recipe.AddIngredient(null, "MagmaEnchant");
            recipe.AddIngredient(null, "MoltenEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(ItemID.BreakerBlade);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
