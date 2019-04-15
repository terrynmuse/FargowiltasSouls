using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class SlimyShield : ModItem
    {
        bool falling = false;
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slimy Shield");
            Tooltip.SetDefault(@"''
Grants immunity to Slimed
15% increased fall speed
When you land after a jump, slime will fall from the sky over your cursor");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 4;
            item.value = Item.sellPrice(0, 4);
            item.defense = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Slimed] = true;
            player.maxFallSpeed *= 2f;

            if (falling)
            {
                if (player.velocity.Y == 0f) //landing
                {
                    falling = false;

                    Vector2 mouse = Main.MouseWorld;

                    for (int i = 0; i < 5; i++)
                    {
                        int p = Projectile.NewProjectile(new Vector2(mouse.X, mouse.Y - Main.rand.Next(900, 1000)), new Vector2(0, 10), mod.ProjectileType("SlimeBall"), 20, 1f, Main.myPlayer);
                    }
                }
            }
            else if (player.velocity.Y > 0)
            {
                falling = true;
            }
        }
    }
}
