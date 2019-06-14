using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls
{
    public class PatreonPlayer : ModPlayer
    {
        public bool Gittle;

        public override void ResetEffects()
        {
            Gittle = false;
        }

        public override void PostUpdateMiscEffects()
        {
            if (player.name == "gittle" || player.name == "gittle lirl")
            {
                Gittle = true;
                player.pickSpeed -= .15f;
                //shine effect
                Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Gittle && Main.rand.Next(10) == 0)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (Vector2.Distance(target.Center, npc.Center) < 50)
                    {
                        npc.AddBuff(BuffID.OnFire, 300);
                    }
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Gittle && Main.rand.Next(10) == 0)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (Vector2.Distance(target.Center, npc.Center) < 50)
                    {
                        npc.AddBuff(BuffID.OnFire, 300);
                    }
                }
            }
        }
    }
}