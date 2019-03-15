using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class LodestoneEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public bool effect;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lodestone Enchantment");
            Tooltip.SetDefault(
@"''
Damage reduction is increased by 10% at every 25% segment of life
Maximum damage reduction is reached at 30% while below 50% life
Effects of Astro-Beetle Husk");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            if (Soulcheck.GetValue("Lodestone Resistance"))
            {
                //set bonus
                thoriumPlayer.orbital = true;
                thoriumPlayer.orbitalRotation3 = Utils.RotatedBy(thoriumPlayer.orbitalRotation3, -0.05000000074505806, default(Vector2));
                if (player.statLife > player.statLifeMax * 0.75)
                {
                    thoriumPlayer.thoriumEndurance += 0.1f;
                    thoriumPlayer.lodestoneStage = 1;
                }
                if (player.statLife <= player.statLifeMax * 0.75 && player.statLife > player.statLifeMax * 0.5)
                {
                    thoriumPlayer.thoriumEndurance += 0.2f;
                    thoriumPlayer.lodestoneStage = 2;
                }
                if (player.statLife <= player.statLifeMax * 0.5)
                {
                    thoriumPlayer.thoriumEndurance += 0.3f;
                    thoriumPlayer.lodestoneStage = 3;
                }
            }
            
            //astro beetle husk
            if (thoriumPlayer.shieldHealth > 0)
            {
                player.statDefense += 5;
            }
            if (thoriumPlayer.outOfCombat)
            {
                thoriumPlayer.astroBeetle = true;
                if (!effect)
                {
                    float num = 25f;
                    int num2 = 0;
                    while (num2 < num)
                    {
                        Vector2 vector = Vector2.UnitX * 0f;
                        vector += -Utils.RotatedBy(Vector2.UnitY, (num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(20f, 5f);
                        vector = Utils.RotatedBy(vector, 0.0, default(Vector2));
                        int num3 = Dust.NewDust(player.Center, 0, 0, 173, 0f, 0f, 0, default(Color), 1.15f);
                        Main.dust[num3].noGravity = true;
                        Main.dust[num3].position = player.Center + vector;
                        Main.dust[num3].velocity = player.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                        int num4 = num2;
                        num2 = num4 + 1;
                    }
                    effect = true;
                    return;
                }
            }
            else
            {
                effect = false;
            }
        }
        
        private readonly string[] items =
        {
            "LodeStoneFaceGuard",
            "LodeStoneChestGaurd",
            "LodeStoneShinGaurds",
            "AstroBeetleHusk",
            "StoneSledge",
            "TheJuggernaut",
            "LodeStoneClaymore",
            "LodeStoneMace",
            "LodeStoneStaff",
            "ValadiumSpear"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
