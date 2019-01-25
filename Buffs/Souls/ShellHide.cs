using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Souls
{
    public class ShellHide : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Shell Hide");
            Description.SetDefault("Projectiles are being blocked, but you take double contact damage");
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FargoPlayer>().ShellHide = true;

            float distance = 3.5f * 16;

            Main.projectile.Where(x => x.active && x.hostile).ToList().ForEach(x =>
            {
                if (Vector2.Distance(x.Center, player.Center) <= distance)
                {
                    int dustId = Dust.NewDust(new Vector2(x.position.X, x.position.Y + 2f), x.width, x.height + 5, DustID.GoldFlame, x.velocity.X * 0.2f, x.velocity.Y * 0.2f, 100,
                        default(Color), 2f);
                    Main.dust[dustId].noGravity = true;
                    int dustId3 = Dust.NewDust(new Vector2(x.position.X, x.position.Y + 2f), x.width, x.height + 5, DustID.GoldFlame, x.velocity.X * 0.2f, x.velocity.Y * 0.2f, 100,
                        default(Color), 2f);
                    Main.dust[dustId3].noGravity = true;

                    x.Kill();
                }
            });
        }
    }
}