using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class SlimyShield : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        private bool falling;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slimy Shield");
            Tooltip.SetDefault(@"'Torn from the innards of a defeated foe'
Grants immunity to Slimed
15% increased fall speed
When you land after a jump, slime will fall from the sky over your cursor");
            DisplayName.AddTranslation(GameCulture.Chinese, "粘液盾");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'从被打败的敌人的内脏中撕裂而来'
免疫黏糊
增加15%下落速度
跳跃落地后,在光标处落下史莱姆");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 2;
            item.value = Item.sellPrice(0, 1);
            item.defense = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Slimed] = true;
            player.maxFallSpeed *= 2f;
            player.GetModPlayer<FargoPlayer>().SlimyShield = true;

            /*if (falling)
            {
                if (player.velocity.Y == 0f) //landing
                {
                    falling = false;
                    Main.PlaySound(SoundID.Item21, player.Center);
                    Vector2 mouse = Main.MouseWorld;
                    for (int i = 0; i < 5; i++)
                    {
                        Vector2 spawn = new Vector2(mouse.X + Main.rand.Next(-200, 201), mouse.Y - Main.rand.Next(600, 901));
                        Vector2 speed = mouse - spawn;
                        speed.Normalize();
                        speed *= 10f;
                        Projectile.NewProjectile(spawn, speed, mod.ProjectileType("SlimeBall"), 20, 1f, Main.myPlayer);
                    }
                }
            }
            else if (player.velocity.Y > 0)
            {
                falling = true;
            }*/
        }
    }
}
