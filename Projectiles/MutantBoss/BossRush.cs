using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Projectiles.MutantBoss
{
    public class BossRush : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_454";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutant Seal");
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.GetGlobalProjectile<FargoGlobalProjectile>().TimeFreezeImmune = true;
        }

        public override void AI()
        {
            int ai0 = (int)projectile.ai[0];
            if (!(ai0 > -1 && ai0 < 200 && Main.npc[ai0].active && Main.npc[ai0].type == mod.NPCType("MutantBoss")))
            {
                projectile.Kill();
                return;
            }
            
            NPC npc = Main.npc[ai0];
            projectile.Center = npc.Center;
            projectile.timeLeft = 2;
            
            if (--projectile.ai[1] < 0)
            {
                projectile.ai[1] = 180;
                projectile.netUpdate = true;
                switch((int)projectile.localAI[0]++)
                {
                    case 0:
                        NPC.SpawnOnPlayer(npc.target, NPCID.EyeofCthulhu);
                        if (Main.dayTime)
                        {
                            Main.dayTime = false;
                            Main.time = 0;
                            if (Main.netMode == 2)
                                NetMessage.SendData(7); //sync world
                        }
                        break;

                    case 1:
                        NPC.SpawnOnPlayer(npc.target, NPCID.EaterofWorldsHead);
                        NPC.SpawnOnPlayer(npc.target, NPCID.BrainofCthulhu);
                        break;

                    case 2:
                        NPC.SpawnOnPlayer(npc.target, NPCID.QueenBee);
                        break;

                    case 3:
                        ManualSpawn(npc, NPCID.SkeletronHead);
                        if (Main.dayTime)
                        {
                            Main.dayTime = false;
                            Main.time = 0;
                            if (Main.netMode == 2)
                                NetMessage.SendData(7); //sync world
                        }
                        break;

                    case 4:
                        NPC.SpawnOnPlayer(npc.target, NPCID.Retinazer);
                        NPC.SpawnOnPlayer(npc.target, NPCID.Spazmatism);
                        if (Main.dayTime)
                        {
                            Main.dayTime = false;
                            Main.time = 0;
                            if (Main.netMode == 2)
                                NetMessage.SendData(7); //sync world
                        }
                        break;

                    case 5:
                        ManualSpawn(npc, NPCID.SkeletronPrime);
                        if (Main.dayTime)
                        {
                            Main.dayTime = false;
                            Main.time = 0;
                            if (Main.netMode == 2)
                                NetMessage.SendData(7); //sync world
                        }
                        break;

                    case 6:
                        NPC.SpawnOnPlayer(npc.target, NPCID.Plantera);
                        break;

                    case 7:
                        ManualSpawn(npc, NPCID.Golem);
                        break;

                    case 8:
                        ManualSpawn(npc, NPCID.DD2Betsy);
                        break;

                    case 9:
                        ManualSpawn(npc, NPCID.DukeFishron);
                        break;

                    case 10:
                        ManualSpawn(npc, NPCID.MoonLordCore);
                        break;

                    default:
                        break;
                }
            }
        }

        private void ManualSpawn(NPC npc, int type)
        {
            if (Main.netMode != 1)
            {
                int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, type);
                if (n < 200)
                {
                    if (Main.netMode == 0)
                    {
                        Main.NewText(Main.npc[n].FullName + " has awoken!", 175, 75, 255);
                    }
                    else if (Main.netMode == 2)
                    {
                        NetMessage.SendData(23, -1, -1, null, n);
                        NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(Main.npc[n].FullName + " has awoken!"), new Color(175, 75, 255));
                    }
                }
            }
        }
    }
}