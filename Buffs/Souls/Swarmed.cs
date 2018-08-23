using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Souls
{
    public class Swarmed : ModBuff
    {
        private int counter;

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Swarmed");
            Main.buffNoSave[Type] = true;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderDebuff";

            return true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (counter % 6 == 0)
            {
                Projectile p = Projectile.NewProjectileDirect(new Vector2(Main.rand.Next((int)npc.Center.X - 100, (int)npc.Center.X + 100), Main.rand.Next((int)npc.Center.Y - 100, (int)npc.Center.Y)), new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), ProjectileID.BabySpider, 20, 0f, Main.myPlayer);
                counter = 1;
            }

            counter++;
        }
    }
}