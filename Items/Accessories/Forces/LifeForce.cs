using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class LifeForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Life");

            string tooltip =
@"'Rare is a living thing that dare disobey your will'
";

            /*if (thorium != null)
            {
                tooltip +=
@"You leave behind a trail of fire and bees when you walk
Eating a Pumpkin Pie heals you to full HP
100% of contact damage is reflected
Enemies will explode into needles on death
Increases the strength of friendly bees and they ignore most enemy defense
After four consecutive non-critical strikes, your next attack will mini-crit for 150% damage
Critical strikes release a splash of foam, slowing nearby enemies
Brightens the area directly in front of you and allows quicker movement in water
You may summon nearly twice as many spider minions
Summons a living wood sapling and its attacks will home in on enemies
Pressing the 'Encase' key will place you within a fragile cocoon
Your attacks have a 15% chance to release a blinding flash of light
When standing still and not attacking, you gain the Shell Hide buff
Beetles protect you from damage and your wings last twice as long
Summons a pet Squashling, Hornet, Spider, Holy Goat, Lizard, Turtle and Parrot";
            }
            else
            {*/
                tooltip +=
@"You leave behind a trail of fire when you walk
Eating Pumpkin Pie also heals you to full HP
100% of contact damage is reflected
Enemies will explode into needles on death
Increases the strength of friendly bees
Bees ignore most enemy defense
You may summon nearly twice as many spider minions
When standing still and not attacking, you gain the Shell Hide buff
Beetles protect you from damage
Your wings last twice as long
Summons several pets";
            //}

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
            //tide hunter, yew wood, iridescent effects
            modPlayer.LifeForce = true;
            //bees ignore defense, super bees, pet
            modPlayer.BeeEffect(hideVisual);
            //more spiders and pet
            modPlayer.SpiderEffect(hideVisual);
            //defense beetle bois
            modPlayer.BeetleEffect();
            //extra wing time
            modPlayer.BeetleEnchant = true;
            //flame trail, pie heal, pet
            modPlayer.PumpkinEffect(25, hideVisual);
            //shell hide, pets
            modPlayer.TurtleEffect(hideVisual);
            player.thorns = 1f;
            player.turtleThorns = true;
            //needle spray
            modPlayer.CactusEffect();

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //bee booties
            if ((player.velocity.X > 1f && player.velocity.X > 0f) || (player.velocity.X < 1f && player.velocity.X < 0f))
            {
                timer++;
                if (timer > 45)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("BeeSummonSpawn"), 0, 0f, player.whoAmI, 0f, 0f);
                    timer = 0;
                }
            }
            //chrysalis
            thoriumPlayer.cocoonAcc = true;
            //living wood set bonus
            thoriumPlayer.livingWood = true;
            //free boi
            modPlayer.LivingWoodEnchant = true;
            modPlayer.AddMinion("Sapling Minion", thorium.ProjectileType("MinionSapling"), 40, 2f);
            //angler bowl
            if (!hideVisual)
            {
                if (player.direction > 0 && Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(player.Center.X + 56f, player.Center.Y - 10f, 0f, 0f, thorium.ProjectileType("AnglerLight"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                if (player.direction < 0 && Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(player.Center.X - 56f, player.Center.Y - 10f, 0f, 0f, thorium.ProjectileType("AnglerLight"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
            }         
            //iridescent set bonus - heal flash
            thoriumPlayer.iridescentSet = true;
            //quicker in water from nagaskin
            player.ignoreWater = true;
            if (player.wet)
            {
                player.moveSpeed += 0.15f;
            }
            //pets
            modPlayer.AddPet("Parrot Pet", hideVisual, BuffID.PetParrot, ProjectileID.Parrot);
            modPlayer.FlightEnchant = true;
            modPlayer.AddPet("Holy Goat Pet", hideVisual, thorium.BuffType("HolyGoatBuff"), thorium.ProjectileType("HolyGoat"));
            modPlayer.BinderEnchant = true;
            thoriumPlayer.goatPet = true;*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "PumpkinEnchant");
            recipe.AddIngredient(null, "BeeEnchant");

            /*if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(thorium.ItemType("TideHunterEnchant"));
                recipe.AddIngredient(thorium.ItemType("NagaSkinEnchant"));
                recipe.AddIngredient(null, "SpiderEnchant");
                recipe.AddIngredient(thorium.ItemType("LifeBloomEnchant"));
                recipe.AddIngredient(thorium.ItemType("LifeBinderEnchant")); 
            }
            else
            {*/
                recipe.AddIngredient(null, "SpiderEnchant");
            //}

            recipe.AddIngredient(null, "TurtleEnchant");
            recipe.AddIngredient(null, "BeetleEnchant");

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}