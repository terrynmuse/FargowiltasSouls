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

            string tooltip = @"''
";

            if (thorium != null)
            {
                tooltip +=
@"A Dungeon Guardian will occasionally annihilate a foe when struck
Critical strikes will generate up to 15 shadow wisps
Pressing the 'Special Ability' key will unleash every stored shadow wisp towards your cursor's position
Halves radiant life costs but not its life transferring effect
Running builds up momentum, Crashing into an enemy releases all stored momentum
Your attacks have a chance to unleash an explosion of Dragon's Flame and inflict Darkness
All of your minions may occasionally spew massive scythes everywhere
Throw a smoke bomb to teleport to it, Standing nearby smoke gives you the First Strike buff
Dash into any walls, to teleport through them
Effects of the Master Ninja Gear
Striking an enemy with any weapon will trigger 'Shadow Dance'
Your weapon's projectiles occasionally shoot from the shadows
Using any weapon item has a 20% chance to unleash two Blight Daggers
Pressing the Special Ability key will trigger True Strikes
Summons a Li'l Devil to attack enemies
Summons several pets";
            }
            else
            {
                tooltip +=
@"Your attacks may inflict Darkness on enemies
A Dungeon Guardian will occasionally annihilate a foe when struck
All of your minions may occasionally spew massive scythes everywhere
Throw a smoke bomb to teleport to it
Standing nearby smoke gives you the First Strike buff
Dash into any walls, to teleport through them to the next opening
Effects of the Master Ninja Gear
Your weapon's projectiles occasionally shoot from the shadows of where you used to be
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
            item.rare = 10;
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

            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            //warlock set bonus
            thoriumPlayer.warlockSet = true;
            //demon tongue
            thoriumPlayer.radiantLifeCost = 2;
            //lil devil
            modPlayer.WarlockEnchant = true;
            modPlayer.AddMinion("Li'l Devil Minion", thorium.ProjectileType("Devil"), 20, 2f);
            //crash boots
            player.moveSpeed += 0.0015f * thoriumPlayer.momentum;
            player.maxRunSpeed += 0.0025f * thoriumPlayer.momentum;
            if (player.velocity.X > 0f || player.velocity.X < 0f)
            {
                if (thoriumPlayer.momentum < 180)
                {
                    thoriumPlayer.momentum++;
                }
                if (thoriumPlayer.momentum > 60 && Collision.SolidCollision(player.position, player.width, player.height + 4))
                {
                    int num = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + player.height - 2f), player.width + 4, 4, 6, 0f, 0f, 100, default(Color), 0.625f + 0.0075f * thoriumPlayer.momentum);
                    Main.dust[num].noGravity = true;
                    Main.dust[num].noLight = true;
                    Dust dust = Main.dust[num];
                    dust.velocity *= 0f;
                }
            }
            //dragon 
            thoriumPlayer.dragonSet = true;
            //wyvern pet
            modPlayer.AddPet("Wyvern Pet", hideVisual, thorium.BuffType("WyvernPetBuff"), thorium.ProjectileType("WyvernPet"));
            thoriumPlayer.wyvernPet = true;
            //darkness, pets
            modPlayer.ShadowEffect(hideVisual);
            //lich gaze
            thoriumPlayer.lichGaze = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "NecroEnchant");
                recipe.AddIngredient(null, "WarlockEnchant");
                recipe.AddIngredient(null, "DreadEnchant");
                recipe.AddIngredient(null, "SpookyEnchant");
                recipe.AddIngredient(null, "ShinobiEnchant");
                recipe.AddIngredient(null, "DarkArtistEnchant");
                recipe.AddIngredient(null, "PlagueDoctorEnchant");
            }
            else
            {
                recipe.AddIngredient(null, "ShadowEnchant");
                recipe.AddIngredient(null, "NecroEnchant");
                recipe.AddIngredient(null, "SpookyEnchant");
                recipe.AddIngredient(null, "ShinobiEnchant");
                recipe.AddIngredient(null, "DarkArtistEnchant");
            }

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}