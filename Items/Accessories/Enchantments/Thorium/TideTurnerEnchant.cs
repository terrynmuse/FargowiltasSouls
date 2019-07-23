using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class TideTurnerEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tide Turner Enchantment");
            Tooltip.SetDefault(
@"'Become as unstoppable as the tides, Unleash aquatic wrath upon your foes'
Pressing the 'Special Ability' key will envelop you within an impervious bubble
While the bubble is active, all damage taken is converted into healing
Produces a floating globule every half second
Every globule increases defense and makes your next thrown attack a mini-crit
Throwing damage overflows hit enemies with energy
Throwing damage has a 20% chance to unleash aquatic homing daggers all around you");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 400000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(255, 128, 0));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //floating globs and defense
            thoriumPlayer.tideHelmet = true;
            if (thoriumPlayer.tideOrb < 8)
            {
                timer++;
                if (timer > 30)
                {
                    float num = 30f;
                    int num2 = 0;
                    while (num2 < num)
                    {
                        Vector2 vector = Vector2.UnitX * 0f;
                        vector += -Utils.RotatedBy(Vector2.UnitY, (num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(25f, 25f);
                        vector = Utils.RotatedBy(vector, Utils.ToRotation(player.velocity), default(Vector2));
                        int num3 = Dust.NewDust(player.Center, 0, 0, 113, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num3].scale = 1.6f;
                        Main.dust[num3].noGravity = true;
                        Main.dust[num3].position = player.Center + vector;
                        Main.dust[num3].velocity = player.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                        int num4 = num2;
                        num2 = num4 + 1;
                    }
                    thoriumPlayer.tideOrb++;
                    timer = 0;
                }
            }
            //set bonus damage to healing hot key
            thoriumPlayer.tideSet = true;
            //thrown applys granite surge
            thoriumPlayer.tideOverflow = true;
            //unleash daggers
            thoriumPlayer.tideThrow = true;
        }
        
        private readonly string[] items =
        {
            "TideTurnerHelmet",
            "TideTurnersGaze",
            "TideTurnerBreastplate",
            "TideTurnerGreaves",
            "PoseidonCharge", 
            "MantisPunch",
            "QuakeGauntlet",
            "OceansJudgment",
            "Trefork",
            "TerrariansKnife"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
