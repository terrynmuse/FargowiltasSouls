using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class LifeForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Life");
            Tooltip.SetDefault(
@"'Rare is a living thing that dare disobey your will'
Getting hit by a projectile causes a needle spray
You leave behind a trail of fire when you walk
Eating Pumpkin Pie also heals you to full HP
Increases the strength of friendly bees
Bees ignore enemy defense
Attacks may cause the enemy to be Swarmed
When standing still and not attacking, you gain the Shell Hide buff
100% of damage taken by melee attacks is reflected
Enemies are more likely to target you
Beetles protect you from damage
Your wings last 1.5x as long
Summons a pet Squashling, Baby Hornet, Spider, Lizard, and Turtle");
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
            //needle spray
            modPlayer.CactusEnchant = true;
            //pie
            modPlayer.PumpkinEnchant = true;
            //flames
            modPlayer.PumpkinEffect(40);
            player.strongBees = true;
            //bees ignore defense
            modPlayer.BeeEnchant = true;
            //swarm debuff
            modPlayer.SpiderEnchant = true;
            //hide in shell buff
            modPlayer.TurtleEnchant = true;
            player.thorns = 1f;
            player.turtleThorns = true;
            player.aggro += 50;
            //wing time up
            modPlayer.BeetleEnchant = true;
            //beetle resistance
            modPlayer.BeetleEffect();
            modPlayer.AddPet("Squashling Pet", BuffID.Squashling, ProjectileID.Squashling);
            modPlayer.AddPet("Baby Hornet Pet", BuffID.BabyHornet, ProjectileID.BabyHornet);
            modPlayer.AddPet("Spider Pet", BuffID.PetSpider, ProjectileID.Spider);
            modPlayer.AddPet("Turtle Pet", BuffID.PetTurtle, ProjectileID.Turtle);
            modPlayer.AddPet("Lizard Pet", BuffID.PetLizard, ProjectileID.PetLizard);
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