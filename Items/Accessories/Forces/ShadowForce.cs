using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class ShadowForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Force");
            Tooltip.SetDefault(
@"''
Throw a smoke bomb to teleport to it
Standing nearby smoke gives you the First Strike buff
You will recieve escalating Darkness debuffs while hitting enemies
Surrounding enemies will take rapid damage when it is the darkest
A Dungeon Guardian will occasionally annihilate a foe when struck by any attack
All of your minions may occasionally spew massive scythes everywhere
Greatly enhances Lightning Aura and Flameburst effectiveness
Effects of the Master Ninja Gear
Dash into any walls, to teleport through them to the next opening
Projectile weapons occasionally shoot from the shadows of where you used to be
Summons a pet Black Cat, Baby Eater of Souls, Shadow Orb, Baby Skeletron Head, Cursed Sapling, Eyeball Spring, Gato, and Flickerwick");
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

        public override string Texture
        {
            get
            {
                return "FargowiltasSouls/Items/Placeholder";
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.ShadowForce = true;
            //smoke tele
            modPlayer.NinjaEnchant = true;
            //darkness doom
            modPlayer.ShadowEnchant = true;
            //DG hit
            modPlayer.NecroEnchant = true;
            //scythe doom
            modPlayer.SpookyEnchant = true;
            player.setMonkT2 = true;
            player.setMonkT3 = true;
            //ninja gear
            player.blackBelt = true;
            player.spikedBoots = 2;
            player.dash = 1;
            //tele thru walls
            modPlayer.ShinobiEnchant = true;
            player.setApprenticeT2 = true;
            player.setApprenticeT3 = true;
            //shadow shoot meme
            modPlayer.DarkEnchant = true;
            modPlayer.AddPet("Black Cat Pet", BuffID.BlackCat, ProjectileID.BlackCat);
            modPlayer.AddPet("Baby Eater Pet", BuffID.BabyEater, ProjectileID.BabyEater);
            modPlayer.AddPet("Shadow Orb Pet", BuffID.ShadowOrb, ProjectileID.ShadowOrb);
            modPlayer.AddPet("Baby Skeletron  Pet", BuffID.BabySkeletronHead, ProjectileID.BabySkeletronHead);
            modPlayer.AddPet("Cursed Sapling Pet", BuffID.CursedSapling, ProjectileID.CursedSapling);
            modPlayer.AddPet("Eye Spring Pet", BuffID.EyeballSpring, ProjectileID.EyeSpring);
            modPlayer.AddPet("Gato Pet", BuffID.PetDD2Gato, ProjectileID.DD2PetGato);
            modPlayer.AddPet("Flickerwick Pet", BuffID.PetDD2Ghost, ProjectileID.DD2PetGhost);
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
            {
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            }
            else
            {
                recipe.AddTile(TileID.LunarCraftingStation);
            }

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}