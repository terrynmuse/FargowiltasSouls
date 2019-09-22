using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls
{
    public class PatreonPlayer : ModPlayer
    {
        public bool Gittle;

        public bool CompOrb;

        public override void ResetEffects()
        {
            Gittle = false;
            CompOrb = false;
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
            OnHitEither(target, damage, knockback, crit);

            
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            OnHitEither(target, damage, knockback, crit);

            
        }

        private void OnHitEither(NPC target, int damage, float knockback, bool crit)
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

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (CompOrb && !item.magic && player.statMana >= 10)
            {
                player.statMana -= 10;
                player.manaRegenDelay = 300;
                damage = (int)(damage * 1.25f);

                for (int num468 = 0; num468 < 20; num468++)
                {
                    int num469 = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), target.width, target.height, 60, -target.velocity.X * 0.2f,
                        -target.velocity.Y * 0.2f, 100, default(Color), 2f);
                    Main.dust[num469].noGravity = true;
                    Main.dust[num469].velocity *= 2f;
                    num469 = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), target.width, target.height, 60, -target.velocity.X * 0.2f,
                        -target.velocity.Y * 0.2f, 100);
                    Main.dust[num469].velocity *= 2f;
                }
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (CompOrb && !proj.magic && player.statMana >= 10)
            {
                player.statMana -= 10;
                player.manaRegenDelay = 300;
                damage = (int)(damage * 1.25f);

                for (int num468 = 0; num468 < 20; num468++)
                {
                    int num469 = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), target.width, target.height, 60, -target.velocity.X * 0.2f,
                        -target.velocity.Y * 0.2f, 100, Color.SkyBlue, 2f);
                    Main.dust[num469].noGravity = true;
                    Main.dust[num469].velocity *= 2f;
                    num469 = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), target.width, target.height, 60, -target.velocity.X * 0.2f,
                        -target.velocity.Y * 0.2f, 100, Color.SkyBlue);
                    Main.dust[num469].velocity *= 2f;
                }
            }
        }
    }
}