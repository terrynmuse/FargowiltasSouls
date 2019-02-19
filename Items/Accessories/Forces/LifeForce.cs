using Microsoft.Xna.Framework;
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
You leave behind a trail of fire when you walk
Eating Pumpkin Pie also heals you to full HP
100% of contact damage is reflected
Enemies will explode into needles on death
Increases the strength of friendly bees
Bees ignore most enemy defense
You may summon nearly twice as many spider minions
When standing still and not attacking, you gain the Shell Hide buff
Beetles protect you from damage
Your wings last twice as long
";

            if (thorium != null)
            {
                tooltip += "Effects of Bee Booties and Arachnid's Subwoofer\n";
            }

            tooltip += "Summons several pets";

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

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);           
        }

        private void Thorium(Player player)
        {
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

            //venom woofer
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerVenom = true;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

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