using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.NPCs;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class ShadowForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Force");

            string tooltip = @"'Dark, Darker, Yet Darker'
Your attacks may inflict Darkness on enemies
A Dungeon Guardian will occasionally annihilate a foe when struck
All of your minions may occasionally spew massive scythes everywhere
Throw a smoke bomb to teleport to it
Standing nearby smoke gives you the First Strike buff
Dash into any walls, to teleport through them to the next opening
While attacking, Flameburst shots manifest themselves from your shadows
Greatly enhances Flameburst effectiveness
";

            if (thorium == null)
            {
                tooltip +=
@"Effects of Master Ninja Gear
Summons several pets";
            }
            else
            {
                tooltip +=
@"Effects of Master Ninja Gear and Dark Effigy
Summons several pets";
            }

            Tooltip.SetDefault(tooltip);
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
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //warlock, shade, plague accessory effect for all
            modPlayer.ShadowForce = true;
            //shoot from where you were meme, pet
            modPlayer.DarkArtistEffect(hideVisual);
            //DG meme, pet
            modPlayer.NecroEffect(hideVisual);
            //darkness debuff, pets
            modPlayer.ShadowEffect(hideVisual);
            //ninja gear
            player.blackBelt = true;
            player.spikedBoots = 2;
            player.dash = 1;
            //tele thru walls, pet
            modPlayer.ShinobiEffect(hideVisual);
            //smoke bomb nonsense, pet
            modPlayer.NinjaEffect(hideVisual);
            //scythe doom, pets
            modPlayer.SpookyEffect(hideVisual);

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
            
            if(!Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "ShadowEnchant");
            }

            recipe.AddIngredient(null, "NecroEnchant");
            recipe.AddIngredient(null, "SpookyEnchant");
            recipe.AddIngredient(null, "ShinobiEnchant");
            recipe.AddIngredient(null, "DarkArtistEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}