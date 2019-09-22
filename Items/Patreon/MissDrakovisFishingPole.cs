using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;

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
            DisplayName.AddTranslation(GameCulture.Chinese, "Drakovi小姐的钓竿");
            Tooltip.AddTranslation(GameCulture.Chinese, "右键循环切换攻击模式 \n每种伤害类型对应一种模式");
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

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool UseItem(Player player)
        {
            //right click
            if (player.altFunctionUse == 2)
            {
                Main.NewText("Hi " + mode);

                mode++;

                if (mode > 5)
                {
                    mode = 1;
                }

                SetUpItem();
            }

            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            /*//right click
            if (player.altFunctionUse == 2)
            {
                mode++;

                if (mode > 5)
                {
                    mode = 1;
                }

                SetUpItem();

                Main.NewText("Hi " + mode);

                return false;
            }*/

            switch (mode)
            {
                //melee
                case 1:
                    break;
                //range
                case 2:
                    int num129 = Main.rand.Next(4, 6);
                    for (int num130 = 0; num130 < num129; num130++)
                    {
                        float num131 = (float)Main.mouseX + Main.screenPosition.X - position.X;
                        float num132 = (float)Main.mouseY + Main.screenPosition.Y - position.Y;
                        num131 += (float)Main.rand.Next(-40, 41) * 0.05f;
                        num132 += (float)Main.rand.Next(-40, 41) * 0.05f;
                        Projectile.NewProjectile(position, new Vector2(num131, num132), ProjectileID.Bullet, damage, knockBack, player.whoAmI);
                    }
                    break;
                //magic
                case 3:
                    for (int num178 = 0; num178 < 3; num178++)
                    {
                        float num179 = (float)Main.mouseX + Main.screenPosition.X - position.X;
                        float num180 = (float)Main.mouseY + Main.screenPosition.Y - position.Y;
                        num179 += (float)Main.rand.Next(-40, 41) * 0.1f;
                        num180 += (float)Main.rand.Next(-40, 41) * 0.1f;
                        Projectile.NewProjectile(position, new Vector2(num179, num180), ProjectileID.Bubble, damage, knockBack, player.whoAmI);
                    }
                    break;
                //summon
                case 4:
                    break;
                //throwing
                default:
                    for (int i = 0; i < 0; i++)
                    {
                        Projectile.NewProjectile(position, new Vector2(speedX + Main.rand.Next(-2, 2), speedY + Main.rand.Next(-2, 2)), ProjectileID.SpikyBall, damage, knockBack, player.whoAmI);
                    }
                    break;
            }

            return false;
        }

        private void SetUpItem()
        {
            ResetDamageType();

            switch (mode)
            {
                //melee
                case 1:
                    item.melee = true;
                    item.useStyle = 1;
                    item.useTime = 15;
                    item.useAnimation = 15;
                    item.UseSound = SoundID.Item1;
                    item.knockBack = 6;
                    item.noMelee = false;
                    item.shoot = 0;
                    break;
                //range
                case 2:
                    item.ranged = true;
                    //item.shoot = ProjectileID.Bullet;
                    item.knockBack = 6.5f;
                    item.useStyle = 5;
                    item.useAnimation = 45;
                    item.useTime = 45;
                    item.shoot = 10;
                    item.useAmmo = AmmoID.Bullet;
                    item.UseSound = SoundID.Item36;
                    item.shootSpeed = 7f;
                    item.noMelee = true;
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

        private void ResetDamageType()
        {
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.summon = false;
            item.ranged = false;
            item.mana = 0;
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