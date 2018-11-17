using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class ShadowForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Force");
            /*string tooltip =
@"''
Greatly enhances Flameburst effectiveness
Your weapon's projectiles occasionally shoot from the shadows of where you used to be
A Dungeon Guardian will occasionally annihilate a foe when struck by any attack
Summons a Baby Skeletron Head

Throw a smoke bomb to teleport to it
Standing nearby smoke gives you the First Strike buff
Summons a pet Black cat

Your attacks may inflict Darkness on enemies
Summons a Baby Eater of Souls and a Shadow Orb
Greatly enhances Lightning Aura effectiveness
Effects of the Master Ninja Gear
Dash into any walls, to teleport through them to the next opening
Summons a pet Gato
All of your minions may occasionally spew massive scythes everywhere
Summons a Cursed Sapling and an eyeball spring
";

            if (thorium != null)
            {
                tooltip +=
                    @"Corrupts your radiant powers
                    Enemies afflicted with shadowflame or light curse increase your life regeneration +
                    ";
            }

            tooltip += "Summons a Flickerwick to provide light";

            Tooltip.SetDefault(tooltip);*/
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //
            modPlayer.ShadowForce = true;
            player.setApprenticeT2 = true;
            player.setApprenticeT3 = true;
            //shoot from where you were meme, pet
            modPlayer.DarkArtistEffect(hideVisual);
            //DG meme, pet
            modPlayer.NecroEffect(hideVisual);
            //smoke bomb nonsense, pet
            modPlayer.NinjaEffect(hideVisual);
            //darkness debuff, pets
            modPlayer.ShadowEffect(hideVisual);
            player.setMonkT2 = true;
            player.setMonkT3 = true;
            //ninja gear
            player.blackBelt = true;
            player.spikedBoots = 2;
            player.dash = 1;
            //tele thru walls, pet
            modPlayer.ShinobiEffect(hideVisual);
            //scythe doom, pets
            modPlayer.SpookyEffect(hideVisual);

            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            //dark effigy
            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            thoriumPlayer.darkAura = true;

            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && (npc.FindBuffIndex(153) > -1 || npc.FindBuffIndex(thorium.BuffType("lightCurse")) > -1) && Vector2.Distance(npc.Center, player.Center) < 1000f)
                {
                    thoriumPlayer.effigy++;
                    player.AddBuff(thorium.BuffType("EffigyRegen"), 10, false);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NinjaEnchant");
            recipe.AddIngredient(null, "ShadowEnchant");
            recipe.AddIngredient(null, "NecroEnchant");
            recipe.AddIngredient(null, "SpookyEnchant");
            recipe.AddIngredient(null, "ShinobiEnchant");
            recipe.AddIngredient(null, "DarkArtistEnchant");

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}