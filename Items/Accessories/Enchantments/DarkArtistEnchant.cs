using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.NPCs;

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
Your weapon's projectiles occasionally shoot from the shadows of where you used to be
Greatly enhances Flameburst effectiveness
";

            if(thorium != null)
            {
                tooltip += "Effects of Dark Effigy\n";
            }

            tooltip += "Summons a pet Flickerwick";

            Tooltip.SetDefault(tooltip); 
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
            player.setApprenticeT2 = true;
            player.setApprenticeT3 = true;
            player.GetModPlayer<FargoPlayer>(mod).DarkArtistEffect(hideVisual);

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            //dark effigy
            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");

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
            recipe.AddIngredient(ItemID.ApprenticeScarf);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("Effigy"));
                recipe.AddIngredient(ItemID.ShadowFlameHexDoll);
                recipe.AddIngredient(thorium.ItemType("WhisperingDagger"));
                recipe.AddIngredient(ItemID.DD2FlameburstTowerT2Popper);
                recipe.AddIngredient(ItemID.DD2FlameburstTowerT3Popper);
            }
            else
            {
                recipe.AddIngredient(ItemID.ShadowFlameHexDoll);
                recipe.AddIngredient(ItemID.DD2FlameburstTowerT3Popper);
            }
            
            recipe.AddIngredient(ItemID.DD2PetGhost);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
