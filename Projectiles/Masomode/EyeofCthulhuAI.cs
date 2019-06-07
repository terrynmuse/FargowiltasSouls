using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class EyeofCthulhuAI : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Projectiles/Explosion";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eye of Cthulhu AI");
        }

        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
        }

        public override void AI()
        {
            int ai0 = (int)projectile.ai[0];
            if (ai0 > -1 && ai0 < 200 && Main.npc[ai0].active && Main.npc[ai0].type == NPCID.EyeofCthulhu)
            {
                projectile.timeLeft = 2;
                NPC npc = Main.npc[ai0];
                FargoGlobalNPC fargoGlobalNPC = npc.GetGlobalNPC<FargoGlobalNPC>();

                FargoGlobalNPC.eyeBoss = FargoGlobalNPC.boss = npc.whoAmI;

                fargoGlobalNPC.Counter++;
                if (fargoGlobalNPC.Counter >= 300)
                {
                    fargoGlobalNPC.Counter = 0;
                    if (npc.life <= npc.lifeMax * 0.65 && NPC.CountNPCS(NPCID.ServantofCthulhu) < 12 && Main.netMode != 1)
                    {
                        Vector2 vel = new Vector2(2, 2);
                        for (int i = 0; i < 4; i++)
                        {
                            int n = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.ServantofCthulhu);
                            if (n != 200)
                            {
                                Main.npc[n].velocity = vel.RotatedBy(Math.PI / 2 * i);
                                if (Main.netMode == 2)
                                    NetMessage.SendData(23, -1, -1, null, n);
                            }
                        }
                    }
                }

                //during dashes in phase 2
                if (npc.ai[1] == 3f && npc.life < npc.lifeMax * .4f)
                {
                    fargoGlobalNPC.Counter2 = 30;
                    if (Main.netMode != 1)
                        FargoGlobalProjectile.XWay(8, npc.Center, mod.ProjectileType("BloodScythe"), 2, npc.damage / 4, 1f);
                }

                if (fargoGlobalNPC.Counter2 > 0 && fargoGlobalNPC.Counter2 % 5 == 0 && Main.netMode != 1)
                    Projectile.NewProjectile(new Vector2(npc.Center.X + Main.rand.Next(-15, 15), npc.Center.Y),
                        npc.velocity / 10, mod.ProjectileType("BloodScythe"), npc.damage / 4, 1f, Main.myPlayer);
                fargoGlobalNPC.Counter2--;
            }
        }

        public override bool CanDamage()
        {
            return false;
        }
    }
}