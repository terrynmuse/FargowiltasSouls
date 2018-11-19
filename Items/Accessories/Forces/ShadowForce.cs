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
Enemies afflicted with shadowflame or light curse increase your life regeneration
Your boots vibrate at an unreal frequency, increasing movement speed significantly
While moving, your melee damage and critical strike chance are increased
Your attacks have a chance to unleash an explosion of Dragon's Flame
Your attacks may inflict Darkness on enemies
Running builds up momentum and increases movement speed
Crashing into an enemy releases all stored momentum, catapulting the enemy
Flail weapons have a chance to release rolling spike balls on hit that apply cursed flames to damaged enemies
Your symphonic damage empowers all nearby allies with: Vile Flames
Damage done against curse flamed enemies is increased by 8%
Doubles the range of your empowerments effect radius
Your symphonic damage will empower all nearby allies with: Movement Speed II
Increases armor penetration by 15
Summons a pet Wyvern, Eater of Souls, and Shadow Orb
Striking an enemy with any throwing weapon will trigger 'Shadow Dance'
Additonally, while Shadow Dance is active you deal 15% more throwing damage";
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
            //dread set bonus
            player.moveSpeed += 0.8f;
            player.maxRunSpeed += 10f;
            player.runAcceleration += 0.05f;
            if (player.velocity.X > 0f || player.velocity.X < 0f)
            {
                player.meleeDamage += 0.35f;
                player.meleeCrit += 26;
                player.endurance += 0.1f;
                for (int i = 0; i < 2; i++)
                {
                    int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 65, 0f, 0f, 0, default(Color), 1.75f);
                    int num2 = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 75, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num].noGravity = true;
                    Main.dust[num2].noGravity = true;
                    Main.dust[num].noLight = true;
                    Main.dust[num2].noLight = true;
                }
            }
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
            //cursed core
            thoriumPlayer.cursedCore = true;
            //corrupt woofer
            thoriumPlayer.subwooferCursed = true;
            thoriumPlayer.bardRangeBoost += 450;
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3MovementSpeed = 2;
            //dragon 
            thoriumPlayer.dragonSet = true;
            //dragon tooth necklace
            player.armorPenetration += 15;
            //wyvern pet
            modPlayer.AddPet("Wyvern Pet", hideVisual, thorium.BuffType("WyvernPetBuff"), thorium.ProjectileType("WyvernPet"));
            thoriumPlayer.wyvernPet = true;
            //darkness, pets
            modPlayer.ShadowEffect(hideVisual);
            //shade set bonus
            thoriumPlayer.shadeSet = true;
            if (thoriumPlayer.shadeTele)
            {
                player.thrownDamage += 0.15f;
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