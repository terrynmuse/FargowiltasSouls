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

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lodestone Enchantment");
            Tooltip.SetDefault(
@"'Sturdy'
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
            thoriumPlayer.metalShieldMax = 25;
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
