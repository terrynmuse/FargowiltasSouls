using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class BronzeEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bronze Enchantment");
            Tooltip.SetDefault(
                @"''
Attacks have a chance to shock enemies with chain lightning
Thrown damage has a chance to cause a lightning bolt to strike
Throwing damage increases your movement speed by 1% up to 25%
Throwing damage increases your throwing speed by 0.4% up to 10%
These effects will fade after 3 seconds of not dealing throwing damage
The damage you take is stored into your next attack.
The bonus damage is stored until it is expended
Your symphonic damage empowers all nearby allies with: Medusa's Gaze
Damage done against petrified enemies is increased by 8%
Doubles the range of your empowerments effect radius
While in combat, you generate a 10 life shield");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 60000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //lightning
            thoriumPlayer.greekSet = true;
            //rebuttal
            thoriumPlayer.championShield = true;
            //sandles
            thoriumPlayer.spartanSandle = true;
            //subwoofer
            thoriumPlayer.subwooferMarble = true;
            thoriumPlayer.bardRangeBoost += 450;
            //copper enchant
            player.GetModPlayer<FargoPlayer>(mod).CopperEnchant = true;
            //copper buckler
            thoriumPlayer.metallurgyShield = true;
            if (!thoriumPlayer.outOfCombat)
            {
                timer++;
                if (timer >= 30)
                {
                    int num = 10;
                    if (thoriumPlayer.shieldHealth < num)
                    {
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 255, 255), 1, false, true);
                        thoriumPlayer.shieldHealth++;
                    }
                    timer = 0;
                    return;
                }
            }
            else
            {
                timer = 0;
            }
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("BronzeHelmet"));
            recipe.AddIngredient(thorium.ItemType("BronzeBreastplate"));
            recipe.AddIngredient(thorium.ItemType("BronzeGreaves"));
            recipe.AddIngredient(null, "CopperEnchant");
            recipe.AddIngredient(thorium.ItemType("ChampionsBarrier"));
            recipe.AddIngredient(thorium.ItemType("SpartanSandles"));
            recipe.AddIngredient(thorium.ItemType("BronzeSubwoofer"));
            recipe.AddIngredient(thorium.ItemType("ChampionBlade"));
            recipe.AddIngredient(thorium.ItemType("BronzeThrowing"), 300);
            recipe.AddIngredient(thorium.ItemType("AncientWingButterfly"));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
