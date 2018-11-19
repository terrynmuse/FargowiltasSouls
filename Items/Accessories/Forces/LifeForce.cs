using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class LifeForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Life");

            /*string tooltip =
@"'Rare is a living thing that dare disobey your will'
Increases the strength of friendly bees
Bees ignore most enemy defense
Beetles protect you from damage
Your wings last 1.5x as long
Getting hit by a projectile causes a needle spray
You leave behind a trail of fire when you walk
Eating Pumpkin Pie also heals you to full HP
Summons a pet squashling
You may summon nearly twice as many spider minions
Summons a pet Spider
When standing still and not attacking, you gain the Shell Hide buff
100% of damage taken by melee attacks is reflected
Summons a pet Lizard and Turtle";

            if (thorium != null)
            {
                tooltip += 
@"While running, you will periodically generate bees
Your symphonic damage empowers all nearby allies with: Spider Bite
Damage done against envenomed enemies is increased by 8%
Doubles the range of your empowerments effect radius";
            }

            tooltip += "Summons a pet Baby Hornet";

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
            //bees ignore defense, super bees, pet
            modPlayer.BeeEffect(hideVisual);
            //defense beetle bois
            player.GetModPlayer<FargoPlayer>(mod).BeetleEffect();
            //extra wing time
            player.GetModPlayer<FargoPlayer>(mod).BeetleEnchant = true;
            //needle spray
            modPlayer.CactusEffect();
            //flame trail, pie heal, pet
            modPlayer.PumpkinEffect(25, hideVisual);
            //reflect, shell hide, pets
            modPlayer.TurtleEffect(hideVisual);

            if (!Fargowiltas.Instance.ThoriumLoaded) return;

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
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CactusEnchant");
            recipe.AddIngredient(null, "PumpkinEnchant");
            recipe.AddIngredient(null, "BeeEnchant");
            recipe.AddIngredient(null, "SpiderEnchant");
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