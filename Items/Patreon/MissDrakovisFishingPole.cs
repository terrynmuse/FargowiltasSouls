using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Patreon
{
    public class MissDrakovisFishingPole : ModItem
    {
        private int mode = 1;

        public override string Texture => "Terraria/Item_2296";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Miss Drakovi's Fishing Pole");
            Tooltip.SetDefault("Right click to cycle through options of attack \nEvery damage type has one");
        }

        /*
Melee: The Sprite of the Rod swings down whacking anyone targeted by it and in close enough range with a Puffer Fish
Magic: The Rod points outward much like a Staff and shoots a series of bubbles that explode into water like projectiles that break on collision.
Summon: The Rod is held up and a Fish who's place holder will be the Zephyr Fish but colored differently will be summoned, this fish shoots a stream from its mouth that does damage to enemies and acts almost like golden shower does.
Throwing: The rod is swung forward sending out multiple lures that may or may not either attach to enemies or land on the ground acting as spike balls.
Ranged: Makes a shot gun sound as multiple lures are shot out acting as Bullets. Works like a shot gun.
*/


        public override void SetDefaults()
        {
            item.damage = 100;
            item.width = 24;
            item.height = 28;
            item.value = 100000;
            item.rare = 10;
            item.autoReuse = true;

            SetUpItem();
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            mode++;

            if (mode > 5)
            {
                mode = 1;
            }

            SetUpItem();
        }

        private void SetUpItem()
        {
            switch (mode)
            {
                //melee
                case 1:
                    item.melee = true;
                    item.useStyle = 1;
                    item.useTime = 10;
                    item.useAnimation = 10;
                    item.UseSound = SoundID.Item1;
                    item.knockBack = 6;
                    item.noMelee = false;
                    item.shoot = 0;
                    break;
                //range
                case 2:
                    item.ranged = true;
                    break;
                //magic
                case 3:
                    item.magic = true;
                    item.mana = 15;
                    break;
                //minion
                case 4:
                    item.summon = true;
                    break;
                //throwing
                case 5:
                    item.thrown = true;
                    break;
            }

            /*item.UseSound = SoundID.Item21;
            item.magic = true;
            item.useTime = 5;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.mana = 12;
            item.shoot = 1;
            item.shootSpeed = 18f;*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldenFishingRod);
            recipe.AddIngredient(ItemID.BalloonPufferfish);
            recipe.AddIngredient(ItemID.PurpleClubberfish);
            recipe.AddIngredient(ItemID.FrostDaggerfish, 500);
            recipe.AddIngredient(ItemID.ZephyrFish);
            recipe.AddIngredient(ItemID.Toxikarp);
            recipe.AddIngredient(ItemID.Bladetongue);
            recipe.AddIngredient(ItemID.CrystalSerpent);
            recipe.AddIngredient(ItemID.ScalyTruffle);
            recipe.AddIngredient(ItemID.ObsidianSwordfish);

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}