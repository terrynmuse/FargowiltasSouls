using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class FolvEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Folv's Ancient Enchantment");
            Tooltip.SetDefault(
@"'A man of mystery...'
Projects a mystical barrier around you
While above 50% life, every seventh magic cast will unleash damaging mana bolts
While below 50% life, your defensive capabilities are increased
Magic critical strikes engulf enemies in a long lasting void flame
Effects of Mana-Charged Rocketeers and Gray Music Player");
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

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.folvSet = true;
            Lighting.AddLight(player.position, 0.03f, 0.3f, 0.5f);
            if (player.statLife >= player.statLifeMax * 0.5)
            {
                thoriumPlayer.folvBonus = true;
            }
            else
            {
                thoriumPlayer.folvBonus2 = true;
                player.lifeRegen += 2;
                thoriumPlayer.thoriumEndurance += 0.1f;
                player.noKnockback = true;
            }
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3Defense = 2;
            //malignant
            thoriumPlayer.malignantSet = true;
            //mana charge rockets
            thorium.GetItem("ManaChargedRocketeers").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("FolvHat"));
            recipe.AddIngredient(thorium.ItemType("FolvRobe"));
            recipe.AddIngredient(thorium.ItemType("FolvLegging"));
            recipe.AddIngredient(null, "MalignantEnchant");
            recipe.AddIngredient(thorium.ItemType("TunePlayerDefense"));
            recipe.AddIngredient(ItemID.UnholyTrident);
            recipe.AddIngredient(thorium.ItemType("AncientFlame"));
            recipe.AddIngredient(thorium.ItemType("AncientFrost"));
            recipe.AddIngredient(thorium.ItemType("AncientSpark"));
            recipe.AddIngredient(thorium.ItemType("AncientLight"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
