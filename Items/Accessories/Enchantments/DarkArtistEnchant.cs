using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.NPCs;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class DarkArtistEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Artist Enchantment");

            string tooltip =
@"'The shadows hold more than they seem'
While attacking, Flameburst shots manifest themselves from your shadows
Greatly enhances Flameburst effectiveness
";
            string tooltip_ch =
@"'阴影比看起来更多'
攻击时,焰爆炮塔的射击会从你的阴影中显现出来
大大增强焰爆炮塔能力
";

            if(thorium != null)
            {
                tooltip += "Effects of Dark Effigy\n";
                tooltip_ch += "拥有阴影雕塑的效果\n";
            }

            tooltip += "Summons a pet Flickerwick";
            tooltip_ch += "召唤一个闪烁烛芯";

            Tooltip.SetDefault(tooltip); 
            DisplayName.AddTranslation(GameCulture.Chinese, "暗黑艺术家魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).DarkArtistEffect(hideVisual);

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            //dark effigy
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && (npc.shadowFlame || npc.GetGlobalNPC<ThoriumGlobalNPC>().lightLament) && npc.DistanceSQ(player.Center) < 1000000f)
                {
                    thoriumPlayer.effigy++;
                }
            }
            if (thoriumPlayer.effigy > 0)
            {
                player.AddBuff(thorium.BuffType("EffigyRegen"), 2, true);
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ApprenticeAltHead);
            recipe.AddIngredient(ItemID.ApprenticeAltShirt);
            recipe.AddIngredient(ItemID.ApprenticeAltPants);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("Effigy"));
                recipe.AddIngredient(ItemID.DD2FlameburstTowerT3Popper);
                recipe.AddIngredient(thorium.ItemType("DarkMageStaff"));
                recipe.AddIngredient(ItemID.ShadowFlameHexDoll);
                recipe.AddIngredient(ItemID.InfernoFork);
            }
            else
            {
                recipe.AddIngredient(ItemID.DD2FlameburstTowerT3Popper);
                recipe.AddIngredient(ItemID.ShadowFlameHexDoll);
                recipe.AddIngredient(ItemID.InfernoFork);
            }
            
            recipe.AddIngredient(ItemID.DD2PetGhost);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
