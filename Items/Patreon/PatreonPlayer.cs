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
                player.manaRegenDelay = 60;
                damage = (int)(damage * 1.25f);

                //dust
                int num1 = 36;
                for (int index1 = 0; index1 < num1; ++index1)
                {
                    Vector2 vector2_1 = (Vector2.Normalize(target.velocity) * new Vector2((float)target.width / 2f, (float)target.height) * 0.75f).RotatedBy((double)(index1 - (num1 / 2 - 1)) * 6.28318548202515 / (double)num1, new Vector2()) + target.Center;
                    Vector2 vector2_2 = vector2_1 - target.Center;
                    int index2 = Dust.NewDust(vector2_1 + vector2_2, 0, 0, DustID.PlatinumCoin, vector2_2.X * 2f, vector2_2.Y * 2f, 100, Color.DeepSkyBlue, 1.4f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].noLight = true;
                    Main.dust[index2].velocity = vector2_2;
                }

            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (CompOrb && !proj.magic && player.statMana >= 10)
            {
                player.statMana -= 10;
                damage = (int)(damage * 1.25f);

            }
        }
    }
}