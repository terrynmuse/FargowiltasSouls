using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FargowiltasSouls.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.NPCs
{
    public class FargoGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        //debuffs
        public bool SBleed;
        public bool Shock;
        public bool Rotting;
        public bool LeadPoison;
        public bool SqueakyToy;
        public bool SolarFlare;
        public bool TimeFrozen;
        public bool Chilled;
        public bool HellFire;

        public bool PillarSpawn = true;

        //masochist doom
        public bool Transform = false;
        public bool Revive;
        public int RegenTimer = 0;
        public int Counter = 1;
        public int Timer = 600;
        public int SharkCount;
        private static MethodInfo _startSandstormMethod;

        public override void ResetEffects(NPC npc)
        {
            SBleed = false;
            Shock = false;
            Rotting = false;
            LeadPoison = false;
            SqueakyToy = false;
            SolarFlare = false;
            Chilled = false;
            HellFire = false;
            npc.color = default(Color);
        }

        public override void SetDefaults(NPC npc)
        {
            if (FargoWorld.MasochistMode)
            {
                //ResetRegenTimer(npc);

                if (npc.type == NPCID.Salamander || npc.type == NPCID.Salamander2 || npc.type == NPCID.Salamander3 || npc.type == NPCID.Salamander4 || npc.type == NPCID.Salamander5 || npc.type == NPCID.Salamander6 || npc.type == NPCID.Salamander7 || npc.type == NPCID.Salamander8 || npc.type == NPCID.Salamander9)
                {
                    npc.Opacity /= 25;
                }

                if(npc.type == NPCID.RainbowSlime)
                {
                    npc.scale = 3f;
                    npc.lifeMax *= 5;
                }

                if(npc.type == NPCID.Hellhound)
                {
                    npc.lavaImmune = true;
                }

                if(npc.type == NPCID.VoodooDemon)
                {
                    npc.buffImmune[BuffID.OnFire] = false;
                }

                if (npc.type == NPCID.WalkingAntlion || npc.type == NPCID.DesertBeast)
                {
                    npc.knockBackResist = 0f;
                }

                if (npc.type == NPCID.DetonatingBubble && !NPC.downedBoss3)
                {
                    npc.noTileCollide = false;
                }

                #region boss scaling
                // +2.5% hp each kill 
                // +1.25% damage each kill
                // max of 4x hp and 2.5x damage

                //pre hm get 8x and 5x

                if (npc.type == NPCID.EyeofCthulhu || npc.type == NPCID.ServantofCthulhu)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.EyeCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.EyeCount * .0125));
                }
                else if (npc.type == NPCID.KingSlime)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.SlimeCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.SlimeCount * .0125));
                }
                else if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.EyeCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.EyeCount * .0125));
                }
                else if(npc.type == NPCID.BrainofCthulhu || npc.type == NPCID.Creeper)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.BrainCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.BrainCount * .0125));
                }
                else if(npc.type == NPCID.QueenBee)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.BeeCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.BeeCount * .0125));
                }
                else if(npc.type == NPCID.SkeletronHead || npc.type == NPCID.SkeletronHand)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.SkeletronCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.SkeletronCount * .0125));
                }
                else if(npc.type == NPCID.WallofFlesh || npc.type == NPCID.WallofFleshEye)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.WallCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.WallCount * .0125));
                }
                else if(npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.DestroyerCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.DestroyerCount * .0125));
                }
                else if(npc.type == NPCID.SkeletronPrime || npc.type == NPCID.PrimeCannon || npc.type == NPCID.PrimeLaser || npc.type == NPCID.PrimeSaw || npc.type == NPCID.PrimeVice)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PrimeCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.PrimeCount * .0125));
                }
                else if(npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.TwinsCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.TwinsCount * .0125));
                }
                else if(npc.type == NPCID.Plantera || npc.type == NPCID.PlanterasHook || npc.type == NPCID.PlanterasTentacle)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PlanteraCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.PlanteraCount * .0125));
                }
                else if(npc.type == NPCID.Golem || npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight || npc.type == NPCID.GolemHead || npc.type == NPCID.GolemHeadFree)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.GolemCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.GolemCount * .0125));
                }
                else if(npc.type == NPCID.CultistBoss)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.CultistCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.CultistCount * .0125));
                }
                else if(npc.type == NPCID.DukeFishron)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.FishronCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.FishronCount * .0125));
                }
                else if(npc.type == NPCID.MoonLordCore || npc.type == NPCID.MoonLordHand || npc.type == NPCID.MoonLordHead || npc.type == NPCID.MoonLordFreeEye || npc.type == NPCID.MoonLordLeechBlob)
                {
                    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.MoonlordCount * .025));
                    npc.damage = (int)(npc.damage * (1 + FargoWorld.MoonlordCount * .0125));
                }

                #endregion
            }
        }

        public override bool PreAI(NPC npc)
        {
            Player player = Main.player[Main.myPlayer];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();

            if(TimeFrozen)
            {
                npc.position = npc.oldPosition;
                //npc.frameCounter--;
                npc.frameCounter = 0;
                return false;
            }

            return true;
        }

        public override void AI(NPC npc)
        {
            if (FargoWorld.MasochistMode)
            {
                if (RegenTimer > 0)
                {
                    RegenTimer--;
                }

                //transformations
                if(!Transform)
                {
                    int npcType = 0;

                    if (npc.type == NPCID.BlueSlime && npc.netID == 1 && NPC.downedSlimeKing && Main.rand.Next(5) == 0)
                    {
                        npcType = NPCID.SlimeSpiked;
                    }

                    int[] transforms = { NPCID.Zombie, NPCID.ArmedZombie, NPCID.ZombieEskimo, NPCID.ArmedZombieEskimo, NPCID.PincushionZombie, NPCID.ArmedZombiePincussion, NPCID.FemaleZombie, NPCID.ArmedZombieCenx, NPCID.SlimedZombie, NPCID.ArmedZombieSlimed, NPCID.TwiggyZombie, NPCID.ArmedZombieTwiggy, NPCID.SwampZombie, NPCID.ArmedZombieSwamp, NPCID.Skeleton, NPCID.BoneThrowingSkeleton, NPCID.HeadacheSkeleton, NPCID.BoneThrowingSkeleton2, NPCID.MisassembledSkeleton, NPCID.BoneThrowingSkeleton3, NPCID.PantlessSkeleton, NPCID.BoneThrowingSkeleton4, NPCID.JungleSlime, NPCID.SpikedJungleSlime, NPCID.IceSlime, NPCID.SpikedIceSlime };

                    if(Array.IndexOf(transforms, npc.type) % 2 == 0 && NPC.downedBoss1 && Main.rand.Next(5) == 0)
                    {
                        npcType = transforms[Array.IndexOf(transforms, npc.type) + 1];
                    }

                    //zombie horde
                    if (npc.type == NPCID.Zombie && NPC.downedBoss1 && Main.rand.Next(5) == 0)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Vector2 pos = new Vector2(npc.Center.X + Main.rand.Next(-20, 20), npc.Center.Y);

                            if (!Collision.SolidCollision(pos, npc.width, npc.height))
                            {
                                int j = NPC.NewNPC((int)pos.X, (int)pos.Y, NPCID.Zombie);
                                NPC zombie = Main.npc[j];
                                zombie.GetGlobalNPC<FargoGlobalNPC>().Transform = true;
                            }
                        }
                    }

                    if ((npc.type == NPCID.Crawdad || npc.type == NPCID.GiantShelly) && Main.rand.Next(5) == 0)
                    {
                        //pick a random salamander
                        npcType = Main.rand.Next(498, 507);
                    }
                    else if ((npc.type == NPCID.Salamander || npc.type == NPCID.Salamander2 || npc.type == NPCID.Salamander3 || npc.type == NPCID.Salamander4 || npc.type == NPCID.GiantShelly2) && Main.rand.Next(5) == 0)
                    {
                        //pick a random crawdad
                        npcType = Main.rand.Next(494, 496);
                    }
                    else if ((npc.type == NPCID.Salamander5 || npc.type == NPCID.Salamander6 || npc.type == NPCID.Salamander7 || npc.type == NPCID.Salamander8 || npc.type == NPCID.Crawdad2) && Main.rand.Next(5) == 0)
                    {
                        //pick a random shelly
                        npcType = Main.rand.Next(496, 498);
                    }
                    //random sharks
                    else if((npc.type == NPCID.Goldfish || npc.type == NPCID.GoldfishWalker || npc.type == NPCID.BlueJellyfish) && Main.rand.Next(6) == 0)
                    {
                        npcType = NPCID.Shark;
                    }
                    //mimic swapping
                    else if(npc.type == NPCID.BigMimicCorruption && Main.rand.Next(4) == 0)
                    {
                        npcType = NPCID.BigMimicCrimson;
                    }
                    else if (npc.type == NPCID.BigMimicCrimson && Main.rand.Next(4) == 0)
                    {
                        npcType = NPCID.BigMimicCorruption;
                    }

                    if(npcType != 0)
                    {
                        npc.Transform(npcType);
                    }
                    
                    Transform = true;
                }

                if(Revive)
                {
                    if(npc.type == NPCID.SkeletronPrime)
                    {
                        npc.defense = 9999;
                        npc.damage = 1000;
                    }
                }

                //Auras
                if (npc.type == NPCID.Tim)
                {
                    Aura(npc, 800, BuffID.Silenced);
                }
                else if (npc.type == NPCID.RuneWizard)
                {
                    Aura(npc, 300, mod.BuffType("Hexed"), true);
                }
                else if(npc.type == NPCID.CochinealBeetle || npc.type == NPCID.CyanBeetle || npc.type == NPCID.LacBeetle)
                {
                    Aura(npc, 400, mod.BuffType("Lethargic"));
                }
                else if(npc.type == NPCID.EnchantedSword || npc.type == NPCID.CursedHammer || npc.type == NPCID.CrimsonAxe)
                {
                    Aura(npc, 400, BuffID.WitheredWeapon);
                }
                else if(npc.type == NPCID.Ghost)
                {
                    Aura(npc, 400, BuffID.Cursed);
                }
                else if(npc.type == NPCID.Mummy || npc.type == NPCID.DarkMummy || npc.type == NPCID.LightMummy)
                {
                    Aura(npc, 500, BuffID.Slow);
                }
                else if(npc.type == NPCID.Derpling)
                {
                    Aura(npc, 400, BuffID.Confused, true);
                }
                else if(npc.type == NPCID.ChaosElemental)
                {
                    Aura(npc, 1000, mod.BuffType("Flipped"));
                }
                else if (npc.type == NPCID.MeteorHead)
                {
                    Player player = Main.player[npc.FindClosestPlayer()];
                    if (Counter % 120 == 0 && npc.Distance(player.Center) < 600)
                    {
                        npc.velocity *= 5;
                        Counter = 1;
                    }

                    Aura(npc, 100, BuffID.Burning);
                }

                //Shootings
                if (npc.type == NPCID.BoneSerpentHead && Counter % 300 == 0 && npc.Distance(Main.player[npc.FindClosestPlayer()].Center) < 600) 
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.BurningSphere);
                    Counter = 1;
                }
                else if(npc.type == NPCID.Vulture && Counter % 300 == 0)
                {
                    Shoot(npc, 500, 10, ProjectileID.HarpyFeather, npc.damage / 2, 1, true);
                }
                else if(npc.type == NPCID.DoctorBones && Counter % 600 == 0)
                {
                    Shoot(npc, 1000, 14, ProjectileID.Boulder, npc.damage * 4, 2);
                }
                else if(npc.type == NPCID.Crab && Counter % 300 == 0)
                {
                    Shoot(npc, 800, 14, ProjectileID.Bubble, npc.damage / 2, 1, false, true);
                }
                else if(npc.type == NPCID.ArmoredViking && Counter % 10 == 0)
                {
                    Shoot(npc, 200, 10, ProjectileID.IceSickle, npc.damage / 2, 1, false, true);
                }
                else if((npc.type == NPCID.Crawdad || npc.type == NPCID.Crawdad2) && Counter % 300 == 0)
                {
                    Player player = Main.player[npc.FindClosestPlayer()];
                    if (npc.Distance(player.Center) < 800)
                    {
                        Vector2 velocity = Vector2.Normalize(player.Center - npc.Center) * 10;
                        int bubble = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.DetonatingBubble);
                        Main.npc[bubble].velocity = velocity;
                        Main.npc[bubble].damage = npc.damage / 2;
                    }

                    Counter = 1;
                }
                else if ((npc.type == NPCID.BloodCrawlerWall || npc.type == NPCID.WallCreeperWall) && Counter % 600 == 0)
                {
                    Shoot(npc, 400, 14, ProjectileID.WebSpit, npc.damage / 4, 0);
                }
                else if (npc.type == NPCID.SeekerHead && Counter % 10 == 0)
                {
                    Player player = Main.player[npc.FindClosestPlayer()];
                    if (npc.Distance(player.Center) < 500)
                    {
                        Projectile.NewProjectile(npc.Center, npc.velocity, ProjectileID.EyeFire, npc.damage / 3, 0f, npc.whoAmI);
                    }

                    Counter = 1;
                }
                else if (npc.type == NPCID.Demon && Counter % 600 == 0)
                {
                    Player player = Main.player[npc.FindClosestPlayer()];
                    if (npc.Distance(player.Center) < 800)
                    {
                        FargoGlobalProjectile.XWay(8, npc.Center, ProjectileID.DemonSickle, 1, npc.damage / 2, .5f);
                    }

                    Counter = 1;
                }

                //other memes
                if (npc.type == NPCID.VoodooDemon)
                {
                    if (npc.lavaWet && !npc.HasBuff(BuffID.OnFire))
                    {
                        npc.AddBuff(BuffID.OnFire, 300);
                    }
                    else if (npc.HasBuff(BuffID.OnFire) && npc.buffTime[npc.FindBuffIndex(BuffID.OnFire)] < 60 && !NPC.AnyNPCs(NPCID.WallofFlesh))
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.WallofFlesh);
                        Main.NewText("Wall of Flesh has awoken!", 175, 75);
                        npc.Transform(NPCID.Demon);
                    }
                }
                else if (npc.type == NPCID.Piranha && NPC.CountNPCS(NPCID.Piranha) <= 10 && Counter % 120 == 0 && Main.rand.Next(2) == 0)
                {
                    Player player = Main.player[npc.FindClosestPlayer()];
                    if (player.wet && player.HasBuff(BuffID.Bleeding))
                    {
                        int piranha = NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-20, 20), (int)npc.Center.Y + Main.rand.Next(-20, 20), NPCID.Piranha);
                        Main.npc[piranha].GetGlobalNPC<FargoGlobalNPC>().Counter = 1;
                    }

                    Counter = 1;
                }
                else if (npc.type == NPCID.Shark && Counter % 240 == 0 && SharkCount < 5)
                {
                    Player player = Main.player[npc.FindClosestPlayer()];
                    if (player.HasBuff(BuffID.Bleeding) && player.wet && npc.wet)
                    {
                        npc.damage = (int)(npc.damage * 1.5);
                        SharkCount++;
                    }

                    Counter = 1;
                }

                

                

                



                if ((npc.type == NPCID.BlackRecluse || npc.type == NPCID.BlackRecluseWall) && Counter % 240 == 0)
                {
                    Player player = Main.player[npc.FindClosestPlayer()];
                    player.AddBuff(BuffID.Webbed, 600);

                    if (npc.Distance(player.Center) < 2000f && player.HasBuff(BuffID.Webbed))
                    {
                        player.AddBuff(mod.BuffType("Defenseless"), 300);
                        npc.velocity *= 2;
                        //meme?
                        npc.damage = (int)(npc.damage * 1.5);
                    }

                    Counter = 1;
                }

                //BOSS CHANGES
                //phase 2 EoC 
                if (npc.type == NPCID.EyeofCthulhu && npc.life <= npc.lifeMax * 0.65 && Counter % 240 == 0 && NPC.CountNPCS(NPCID.ServantofCthulhu) < 12)
                {
                    int[] eyes = new int[4];

                    for (int i = 0; i < 4; i++)
                    {
                        eyes[i] = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.ServantofCthulhu);
                    }

                    Main.npc[eyes[0]].velocity = npc.velocity * new Vector2(2, 2);
                    Main.npc[eyes[1]].velocity = npc.velocity * new Vector2(-2, 2);
                    Main.npc[eyes[2]].velocity = npc.velocity * new Vector2(-2, -2);
                    Main.npc[eyes[3]].velocity = npc.velocity * new Vector2(2, -2);

                    Counter = 1;
                }
                //Twins revive
                if (npc.type == NPCID.Retinazer && !NPC.AnyNPCs(NPCID.Spazmatism))
                {
                    Timer--;

                    if (Timer == 0)
                    {
                        int spawn = NPC.NewNPC((int)npc.position.X + Main.rand.Next(-1000, 1000), (int)npc.position.Y + Main.rand.Next(-400, -100), NPCID.Spazmatism);
                        Main.npc[spawn].life = Main.npc[spawn].lifeMax / 2;
                        Main.NewText("Spazmatism has been revived!", 175, 75);
                        Timer = 600;
                    }
                }
                else if (npc.type == NPCID.Spazmatism && !NPC.AnyNPCs(NPCID.Retinazer))
                {
                    Timer--;

                    if (Timer == 0)
                    {
                        int spawn = NPC.NewNPC((int)npc.position.X + Main.rand.Next(-1000, 1000), (int)npc.position.Y + Main.rand.Next(-400, -100), NPCID.Retinazer);
                        Main.npc[spawn].life = Main.npc[spawn].lifeMax / 2;
                        Main.NewText("Retinazer has been revived!", 175, 75);
                        Timer = 600;
                    }
                }

                if (npc.type == NPCID.LunarTowerNebula)
                {
                    foreach (Player p in Main.player)
                    {
                        if (npc.Distance(p.Center) < 5000)
                        {
                            p.AddBuff(mod.BuffType("Atrophied"), 2);
                            p.AddBuff(mod.BuffType("Jammed"), 2);
                            p.AddBuff(mod.BuffType("Antisocial"), 2);
                        }
                    }
                }
                else if (npc.type == NPCID.LunarTowerSolar)
                {
                    foreach (Player p in Main.player)
                    {
                        if (npc.Distance(p.Center) < 5000)
                        {
                            p.buffImmune[BuffID.Silenced] = false;
                            p.AddBuff(BuffID.Silenced, 2);
                            p.AddBuff(mod.BuffType("Jammed"), 2);
                            p.AddBuff(mod.BuffType("Antisocial"), 2);
                        }
                    }
                }
                else if (npc.type == NPCID.LunarTowerStardust)
                {
                    foreach (Player p in Main.player)
                    {
                        if (npc.Distance(p.Center) < 5000)
                        {
                            p.AddBuff(mod.BuffType("Atrophied"), 2);
                            p.AddBuff(mod.BuffType("Jammed"), 2);
                            p.buffImmune[BuffID.Silenced] = false;
                            p.AddBuff(BuffID.Silenced, 2);
                        }
                    }
                }
                else if (npc.type == NPCID.LunarTowerVortex)
                {
                    foreach (Player p in Main.player)
                    {
                        if (npc.Distance(p.Center) < 5000)
                        {
                            p.AddBuff(mod.BuffType("Atrophied"), 2);
                            p.buffImmune[BuffID.Silenced] = false;
                            p.AddBuff(BuffID.Silenced, 2);
                            p.AddBuff(mod.BuffType("Antisocial"), 2);
                        }
                    }
                }

                if (npc.type == NPCID.CultistBoss)
                {
                    Aura(npc, 2000, mod.BuffType("ClippedWings"));
                }
            }

            Counter++;

            if(Counter > 10000)
            {
                Counter = 0;
            }   
        }

        private void Aura(NPC npc, float distance, int buff, bool reverse = false)
        {
            foreach (Player p in Main.player.Where(x => x.active && !x.dead))
            {
                if((reverse && npc.Distance(p.Center) > distance) || (!reverse && npc.Distance(p.Center) < distance))
                {
                    p.AddBuff(buff, 2);
                }
            }  
        }

        private void Shoot(NPC npc, float distance, int speed, int proj, int dmg, float kb, bool recolor = false, bool hostile = false)
        {
            Player player = Main.player[npc.FindClosestPlayer()];

            if (npc.Distance(player.Center) < distance)
            {
                Vector2 velocity = Vector2.Normalize(player.Center - npc.Center) * speed;
                Projectile p = Projectile.NewProjectileDirect(npc.Center, velocity, proj, dmg, kb, npc.whoAmI);

                if(recolor)
                {
                    p.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                }

                if(hostile)
                {
                    p.friendly = false;
                    p.hostile = true;
                }
            }

            Counter = 1;
        }

        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            if(npc.type == NPCID.Shark && SharkCount > 0)
            {
                Color r = new Color(0, 0, 0, 255);
                r.R = (byte)(SharkCount * 20 + 155);
                return r;
            }

            return null;
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (LeadPoison)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4, npc.height + 4, DustID.Lead, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Dust expr_1CCF_cp_0 = Main.dust[dust];
                    expr_1CCF_cp_0.velocity.Y = expr_1CCF_cp_0.velocity.Y - 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }

            if (HellFire)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4, npc.height + 4, DustID.SolarFlare, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);

                    Dust expr_1CCF_cp_0 = Main.dust[dust];
                    expr_1CCF_cp_0.velocity.Y = expr_1CCF_cp_0.velocity.Y - 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }

            if (SqueakyToy)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4, npc.height + 4, 44, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, Color.LimeGreen, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Dust expr_1CCF_cp_0 = Main.dust[dust];
                    expr_1CCF_cp_0.velocity.Y = expr_1CCF_cp_0.velocity.Y - 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }

                Lighting.AddLight((int)(npc.position.X / 16f), (int)(npc.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
            }
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            if(FargoWorld.MasochistMode)
            {
                if (Main.hardMode && Main.rand.Next(10) == 0)
                {
                    if (npc.type == NPCID.SlimeSpiked && !NPC.AnyNPCs(NPCID.KingSlime))
                    {
                        npc.Transform(NPCID.KingSlime);
                        npc.velocity.Y = -20f;
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                    }

                    if (npc.type == NPCID.WanderingEye && !NPC.AnyNPCs(NPCID.EyeofCthulhu))
                    {
                        npc.Transform(NPCID.EyeofCthulhu);
                        npc.velocity.Y = -5f;
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                    }
                }

                if (npc.netID == NPCID.Pinky && !target.noKnockback)
                {
                    Vector2 velocity = Vector2.Normalize(target.Center - npc.Center) * 30;
                    target.velocity = velocity;
                }
            }
        }

        public void ResetRegenTimer(NPC npc)
        {
            if (npc.boss)
            {
                //5 sec
                RegenTimer = 300;
            }
            else
            {
                //10 sec
                RegenTimer = 600;
            }
        }
		
		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			Player player = Main.player[Main.myPlayer];
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			int dmg;

            if (FargoWorld.MasochistMode)
            {
                if(!npc.dontTakeDamage && RegenTimer <= 0)
                {
                    npc.lifeRegen += 1 + npc.lifeMax / 25;
                }

                if(npc.type == NPCID.Golem || npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight || npc.type == NPCID.GolemHead)
                {
                    npc.lifeRegen += npc.lifeMax / 15;
                }
            }

            if (SBleed)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 40;
				if (damage < 10)
				{
					damage = 5;
				}
				
				if(Main.rand.Next(4) == 0)
				{
                    dmg = 20;
					
					int p = Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 40, 0f + Main.rand.Next(-5, 5), -5f, mod.ProjectileType("SuperBlood"), dmg, 0f, Main.myPlayer);

                    Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                }
			}

            if (Rotting)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= 100;

                if (damage < 5)
                { 
                    damage = 5;
                }
            }

            if(LeadPoison)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= 10;
            }

            if(SolarFlare)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= 200;

                if (damage < 20)
                {
                    damage = 20;
                }
            }

            if(HellFire)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= 50 * npc.lifeMax / npc.life;

                if (damage < 4)
                {
                    damage = 4;
                }
            }
		}
		
		public override void EditSpawnRate (Player player, ref int spawnRate, ref int maxSpawns)
		{
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			
            if(FargoWorld.MasochistMode)
            {
                if(!Main.hardMode)
                {
                    //1.3x spawn rate
                    spawnRate = (int)(spawnRate * 0.75);
                    //2x max spawn
                    maxSpawns = (int)(maxSpawns * 2f);
                }
                else
                {
                    //2x spawn rate
                    spawnRate = (int)(spawnRate * 0.5);
                    //3x max spawn
                    maxSpawns = (int)(maxSpawns * 3f);
                }
            }

            if (modPlayer.Bloodthirst)
            {
                //20x spawn rate
                spawnRate = (int)(spawnRate * 0.05);
                //20x max spawn
                maxSpawns = (int)(maxSpawns * 20f);
            }

            if (modPlayer.BuilderMode)
			{
				maxSpawns = (int)(maxSpawns * 0f);
			}
		}
		
		public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
		{
            //layers
			int y = spawnInfo.spawnTileY;
			bool cavern = y >= Main.maxTilesY * 0.4f && y <= Main.maxTilesY * 0.8f;
            bool underground = y > Main.worldSurface && y <= Main.maxTilesY * 0.4f;
            bool surface = y < Main.worldSurface && !spawnInfo.sky;
            bool underworld = spawnInfo.player.ZoneUnderworldHeight;
            bool sky = spawnInfo.sky;
            
            //times
            bool night = !Main.dayTime;
            bool day = Main.dayTime;

            //biomes
            bool noBiome = Fargowiltas.NoBiomeNormalSpawn(spawnInfo);
            bool ocean = spawnInfo.player.ZoneBeach;
            bool dungeon = spawnInfo.player.ZoneDungeon;
            bool meteor = spawnInfo.player.ZoneMeteor;
            bool spiderCave = spawnInfo.spiderCave;
            bool mushroom = spawnInfo.player.ZoneGlowshroom;
            bool jungle = spawnInfo.player.ZoneJungle;
            bool granite = spawnInfo.granite;
            bool marble = spawnInfo.marble;
            bool corruption = spawnInfo.player.ZoneCorrupt;
            bool crimson = spawnInfo.player.ZoneCrimson;
            bool snow = spawnInfo.player.ZoneSnow;
            bool hallow = spawnInfo.player.ZoneHoly;
            bool desert = spawnInfo.player.ZoneDesert;

            bool nebulaTower = spawnInfo.player.ZoneTowerNebula;
            bool vortexTower = spawnInfo.player.ZoneTowerVortex;
            bool stardustTower = spawnInfo.player.ZoneTowerStardust;
            bool solarTower = spawnInfo.player.ZoneTowerSolar;

            bool water = spawnInfo.water;

            //events
            bool goblinArmy = Main.invasionType == 1;
            bool frostLegion = Main.invasionType == 2;
            bool pirates = Main.invasionType == 3;
            bool martianMadness = Main.invasionType == 4;
            bool oldOnesArmy = DD2Event.Ongoing && spawnInfo.player.ZoneOldOneArmy;
            bool frostMoon = surface && night && Main.snowMoon;
            bool pumpkinMoon = surface && night && Main.pumpkinMoon;
            bool solarEclipse = surface && day && Main.eclipse;
            

            //no work?
            //is lava on screen
            bool nearLava = Collision.LavaCollision(spawnInfo.player.position, spawnInfo.spawnTileX, spawnInfo.spawnTileY);


            //ZoneCorrupt
            //ZoneHoly
            //ZoneMeteor
            //ZoneJungle
            //ZoneSnow
            //ZoneCrimson

            //ZoneDesert
            //ZoneGlowshroom
            //ZoneUndergroundDesert
            //ZoneSkyHeight
            //ZoneOverworldHeight
            //ZoneDirtLayerHeight
            //ZoneRockLayerHeight
            //ZoneUnderworldHeight
            //ZoneBeach
            //ZoneRain
            //ZoneSandstorm
            //Main.raining
            //NPC.AnyNPCs(NPCID.IceGolem)
            //TileID.Sets.Conversion.Sand[spawnInfo.spawnTileType]
            //spawnInfo.granite
            //spawnInfo.lihzahrd
            //spawnInfo.marble
            //spawnInfo.sky
            //spawnInfo.spiderCave
            //spawnInfo.player.adjLava
			
			//MASOCHIST MODE
			if(FargoWorld.MasochistMode)
			{
				//all the pre hardmode
				if(!Main.hardMode)
				{
                    //bosses
					if(surface && day && NPC.downedSlimeKing && NPC.downedBoss2 && Main.slimeRain)
					{
                        if(!NPC.AnyNPCs(NPCID.KingSlime))
                        {
                            pool[NPCID.KingSlime] = .04f;
                        }
                        else
                        {
                            pool[NPCID.KingSlime] = .01f;
                        }
					}
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && surface && night && NPC.downedBoss1 && NPC.downedBoss3)
					{
                        if (!NPC.AnyNPCs(NPCID.EyeofCthulhu))
                        {
                            pool[NPCID.EyeofCthulhu] = .004f;
                        }
                        else
                        {
                            pool[NPCID.EyeofCthulhu] = .001f;
                        }
                    }
					
					if(surface && night && NPC.downedBoss1 && NPC.downedBoss3 && Main.bloodMoon)
					{
                        if (!NPC.AnyNPCs(NPCID.EyeofCthulhu))
                        {
                            pool[NPCID.EyeofCthulhu] = .01f;
                        }
                        else
                        {
                            pool[NPCID.EyeofCthulhu] = .0025f;
                        }
                    }
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && NPC.downedBoss2 && NPC.downedBoss3 && NPC.downedQueenBee && corruption && !underworld)
					{
                        if (!NPC.AnyNPCs(NPCID.EaterofWorldsHead))
                        {
                            pool[NPCID.EaterofWorldsHead] = .005f;
                        }
                        else
                        {
                            pool[NPCID.EaterofWorldsHead] = .00125f;
                        }
                    }
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && NPC.downedBoss2 && NPC.downedBoss3 && NPC.downedQueenBee && crimson && !underworld)
					{
                        if (!NPC.AnyNPCs(NPCID.BrainofCthulhu))
                        {
                            pool[NPCID.BrainofCthulhu] = .005f;
                        }
                        else
                        {
                            pool[NPCID.BrainofCthulhu] = .00125f;
                        }
                    }

                    //random
                    if(noBiome && cavern && NPC.downedBoss3)
                    {
                        pool[NPCID.DarkCaster] = .1f;
                    }

                    //maybe working?
                    if(nearLava && !surface)
                    {
                        pool[NPCID.FireImp] = .1f;
                        pool[NPCID.LavaSlime] = .1f;
                    }

                    if(night && surface && noBiome)
                    {
                        pool[NPCID.CorruptBunny] = .1f;
                        pool[NPCID.CrimsonBunny] = .1f;
                    }

                    if(night && ocean)
                    {
                        pool[NPCID.CorruptGoldfish] = .1f;
                        pool[NPCID.CrimsonGoldfish] = .1f;
                    }

                    if(night && snow && surface)
                    {
                        pool[NPCID.CorruptPenguin] = .1f;
                        pool[NPCID.CrimsonPenguin] = .1f;
                    }

                    if(marble && NPC.downedBoss2)
                    {
                        pool[NPCID.Medusa] = .1f;
                    }

                    if(Fargowiltas.NormalSpawn(spawnInfo) && night && jungle && NPC.downedBoss1)
                    {
                        pool[NPCID.DoctorBones] = .05f;
                    }

                    if(granite)
                    {
                        pool[NPCID.GraniteFlyer] = .1f;
                        pool[NPCID.GraniteGolem] = .1f;
                    }

                    if (sky)
                    {
                        pool[NPCID.AngryNimbus] = .05f;
                    }

                    if (mushroom)
                    {
                        pool[NPCID.FungiBulb] = .1f;
                        pool[NPCID.MushiLadybug] = .1f;
                        pool[NPCID.ZombieMushroom] = .1f;
                        pool[NPCID.ZombieMushroomHat] = .1f;
                        pool[NPCID.AnomuraFungus] = .1f;
                    }

                    if(underworld)
                    {
                        pool[NPCID.LeechHead] = .05f;
                    }

                    if(corruption && NPC.downedBoss2)
                    {
                        pool[NPCID.SeekerHead] = .01f;
                    }

                    if (crimson && NPC.downedBoss2)
                    {
                        pool[NPCID.IchorSticker] = .01f;
                    }

                    if(Fargowiltas.NormalSpawn(spawnInfo) && NPC.downedMechBoss2 && !surface)
                    {
                        pool[NPCID.Mimic] = .01f;
                    }

                }
				//all the hardmode
                else
				{
                    //bosses
					if(Fargowiltas.NormalSpawn(spawnInfo) && surface && day && Main.slimeRain)
					{
                        if (!NPC.AnyNPCs(NPCID.KingSlime))
                        {
                            pool[NPCID.KingSlime] = .1f;
                        }
                        else
                        {
                            pool[NPCID.KingSlime] = .025f;
                        }
                    }
				
					if(surface && day && noBiome)
					{
                        if (!NPC.AnyNPCs(NPCID.KingSlime))
                        {
                            pool[NPCID.KingSlime] = .04f;
                        }
                        else
                        {
                            pool[NPCID.KingSlime] = .01f;
                        }
                    }
					
					if(surface && night && Fargowiltas.NormalSpawn(spawnInfo))
					{
                        if (!NPC.AnyNPCs(NPCID.EyeofCthulhu))
                        {
                            pool[NPCID.EyeofCthulhu] = .02f;
                        }
                        else
                        {
                            pool[NPCID.EyeofCthulhu] = .005f;
                        }
                    }
					
					if(surface && night && Main.bloodMoon)
					{
                        if (!NPC.AnyNPCs(NPCID.EyeofCthulhu))
                        {
                            pool[NPCID.EyeofCthulhu] = .05f;
                        }
                        else
                        {
                            pool[NPCID.EyeofCthulhu] = .0125f;
                        }
                    }
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && corruption && !underworld)
					{
                        if (!NPC.AnyNPCs(NPCID.EaterofWorldsHead))
                        {
                            pool[NPCID.EaterofWorldsHead] = .01f;
                        }
                        else
                        {
                            pool[NPCID.EaterofWorldsHead] = .0025f;
                        }
                    }
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && crimson && !underworld)
					{
                        if (!NPC.AnyNPCs(NPCID.BrainofCthulhu))
                        {
                            pool[NPCID.BrainofCthulhu] = .01f;
                        }
                        else
                        {
                            pool[NPCID.BrainofCthulhu] = .0025f;
                        }
                    }			
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && night && spawnInfo.player.ZoneDungeon && NPC.downedMechBossAny)
					{
                        if (!NPC.AnyNPCs(NPCID.SkeletronHead))
                        {
                            pool[NPCID.SkeletronHead] = .005f;
                        }
                        else
                        {
                            pool[NPCID.SkeletronHead] = .00125f;
                        }
                    }
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && day && spawnInfo.player.ZoneJungle && NPC.downedMechBossAny)
					{
                        if (!NPC.AnyNPCs(NPCID.QueenBee))
                        {
                            pool[NPCID.QueenBee] = .005f;
                        }
                        else
                        {
                            pool[NPCID.QueenBee] = .00125f;
                        }
                    }
				
					//all the hard mode bosses
					if(Fargowiltas.NormalSpawn(spawnInfo) && surface && night && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && Main.bloodMoon)
					{
                        if (!NPC.AnyNPCs(NPCID.Retinazer) || !NPC.AnyNPCs(NPCID.Spazmatism) || !NPC.AnyNPCs(NPCID.SkeletronPrime) || !NPC.AnyNPCs(NPCID.TheDestroyer))
                        {
                            pool[NPCID.Retinazer] = .005f;
                            pool[NPCID.Spazmatism] = .005f;
                            pool[NPCID.TheDestroyer] = .005f;
                            pool[NPCID.SkeletronPrime] = .005f;
                        }
                        else
                        {
                            pool[NPCID.Retinazer] = .0025f;
                            pool[NPCID.Spazmatism] = .0025f;
                            pool[NPCID.TheDestroyer] = .0025f;
                            pool[NPCID.SkeletronPrime] = .0025f;
                        }
                    }
					
					if(surface && night && NPC.downedPlantBoss && Main.bloodMoon)
					{
                        if (!NPC.AnyNPCs(NPCID.Retinazer) || !NPC.AnyNPCs(NPCID.Spazmatism) || !NPC.AnyNPCs(NPCID.SkeletronPrime) || !NPC.AnyNPCs(NPCID.TheDestroyer))
                        {
                            pool[NPCID.Retinazer] = .05f;
                            pool[NPCID.Spazmatism] = .05f;
                            pool[NPCID.TheDestroyer] = .05f;
                            pool[NPCID.SkeletronPrime] = .05f;
                        }
                        else
                        {
                            pool[NPCID.Retinazer] = .025f;
                            pool[NPCID.Spazmatism] = .025f;
                            pool[NPCID.TheDestroyer] = .025f;
                            pool[NPCID.SkeletronPrime] = .025f;
                        }
                    }

                    if(ocean && NPC.downedFishron)
                    {
                        pool[NPCID.DukeFishron] = .005f;
                    }

                    if(jungle && !surface && NPC.downedGolemBoss)
                    {
                        pool[NPCID.Plantera] = .005f;
                    }

                    if(underworld && NPC.downedGolemBoss)
                    {
                        pool[NPCID.DD2Betsy] = .005f;
                    }

                    //random
                    if(surface && day && NPC.downedMechBossAny)
                    {
                        pool[NPCID.CultistArcherWhite] = .05f;
                    }

                    if(!surface && jungle)
                    {
                        pool[NPCID.BigMimicJungle] = .05f;
                    }

                    if(desert && surface && night)
                    {
                        pool[NPCID.DesertBeast] = .05f;
                    }

                    if(NPC.AnyNPCs(NPCID.MoonLordHand))
                    {
                        pool[NPCID.AncientCultistSquidhead] = .05f;
                        pool[NPCID.CultistDragonHead] = .05f;
                    }

                    //weather bois
                    if (hallow && surface)
                    {
                        pool[NPCID.RainbowSlime] = .01f;
                    }                    

                    if (snow && surface)
                    {
                        pool[NPCID.IceGolem] = .01f;
                    }

                    //pumpkin and frost moon bois
                    if (NPC.downedMechBossAny)
                    {
                        if (surface)
                        {
                            if(night)
                            {
                                if (noBiome)
                                {
                                    pool[NPCID.Scarecrow1] = .01f;
                                    pool[NPCID.Scarecrow2] = .01f;
                                    pool[NPCID.Scarecrow3] = .01f;
                                    pool[NPCID.Scarecrow4] = .01f;
                                    pool[NPCID.Scarecrow5] = .01f;
                                    pool[NPCID.Scarecrow6] = .01f;
                                    pool[NPCID.Scarecrow7] = .01f;
                                    pool[NPCID.Scarecrow8] = .01f;
                                    pool[NPCID.Scarecrow9] = .01f;
                                    pool[NPCID.Scarecrow10] = .01f;

                                    if(NPC.downedHalloweenKing)
                                    {
                                        pool[NPCID.HeadlessHorseman] = .002f;
                                        pool[NPCID.Pumpking] = .001f;
                                    }
                                }
                                if (crimson || corruption)
                                {
                                    pool[NPCID.Splinterling] = .05f;

                                    if (NPC.downedHalloweenTree)
                                    {
                                        pool[NPCID.MourningWood] = .002f;
                                    }
                                }
                                if (hallow)
                                {
                                    pool[NPCID.PresentMimic] = .05f;
                                    pool[NPCID.GingerbreadMan] = .05f;
                                }
                                if (snow)
                                {
                                    pool[NPCID.ZombieElf] = .02f;
                                    pool[NPCID.ZombieElfBeard] = .02f;
                                    pool[NPCID.ZombieElfGirl] = .02f;
                                    pool[NPCID.Yeti] = .01f;

                                    if (NPC.downedChristmasSantank)
                                    {
                                        pool[NPCID.SantaNK1] = .002f;
                                    }
                                }
                            }
                            if (day && snow)
                            {
                                pool[NPCID.ElfArcher] = .05f;
                                pool[NPCID.ElfCopter] = .01f;

                                if (NPC.downedChristmasTree)
                                {
                                    pool[NPCID.Everscream] = .002f;
                                }
                            }
                        }
                        if(snow)
                        {
                            if(underground)
                            {
                                pool[NPCID.Nutcracker] = .05f;
                            }
                            if(cavern)
                            {
                                pool[NPCID.Krampus] = .05f;

                                if (NPC.downedChristmasIceQueen)
                                {
                                    pool[NPCID.IceQueen] = .001f;
                                }
                            }
                            
                        }
                        if(underworld)
                        {
                            pool[NPCID.Hellhound] = .05f;
                        }
                        if(cavern)
                        {
                            pool[NPCID.Poltergeist] = .05f;
                        }
                        
                        

                        
                    }

                    if(NPC.downedPlantBoss)
                    {
                        if(surface && night && Fargowiltas.NormalSpawn(spawnInfo))
                        {
                            pool[NPCID.SkeletonSniper] = .05f;
                            pool[NPCID.SkeletonCommando] = .05f;
                            pool[NPCID.TacticalSkeleton] = .05f;
                        }
                        if(!surface)
                        {
                            pool[NPCID.DiabolistRed] = .005f;
                            pool[NPCID.DiabolistWhite] = .005f;
                            pool[NPCID.Necromancer] = .005f;
                            pool[NPCID.NecromancerArmored] = .005f;
                            pool[NPCID.RaggedCaster] = .005f;
                            pool[NPCID.RaggedCasterOpenCoat] = .005f;
                        }
                    }

                    if(meteor && NPC.downedGolemBoss)
                    {
                        pool[NPCID.SolarCorite] = .01f;
                        pool[NPCID.SolarSroller] = .01f;
                    }

                    if(sky)
                    {
                        pool[NPCID.SolarCrawltipedeHead] = .01f;
                        pool[NPCID.VortexHornetQueen] = .01f;
                        pool[NPCID.NebulaBrain] = .01f;
                        pool[NPCID.StardustJellyfishBig] = .01f;
                    }
                }
			}
		
			
		
		}
		
		public override bool PreNPCLoot (NPC npc)
		{
            if((npc.type == NPCID.Medusa || npc.type == NPCID.IchorSticker || npc.type == NPCID.SeekerHead || npc.type == NPCID.Mimic || npc.type == NPCID.AngryNimbus) && !Main.hardMode)
            {
                return false;
            }

			return true;
		}

        private bool firstLoot = true;

		public override void NPCLoot(NPC npc)
		{
			Player player = Main.player[Main.myPlayer];
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if(modPlayer.PlatinumEnchant && firstLoot)
            {
                bool midas = npc.HasBuff(BuffID.Midas);
                int chance = 10;
                int bonus = 3;

                if(midas)
                {
                    chance/= 2;
                    bonus *= 2;
                }

                if(Main.rand.Next(chance) == 0)
                {
                    firstLoot = false;
                    for (int i = 0; i < bonus; i++)
                    {
                        npc.NPCLoot();
                        NPC.killCount[Item.NPCtoBanner(npc.BannerID())]--;
                    }
                }
            }

            firstLoot = false;
        }
		
		public override bool CheckDead(NPC npc)
		{
			Player player = Main.player[Main.myPlayer];
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if(TimeFrozen)
            {
                npc.life = 1;
                return false;
            }

            if(FargoWorld.MasochistMode)
            {
                if(npc.type == NPCID.Drippler)
                {
                    int[] eyes = new int[4];

                    for(int i = 0; i < 4; i++)
                    {
                        eyes[i] = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.DemonEye);
                        Main.npc[eyes[i]].velocity = new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(-3, 3));
                    }
                }

                if(npc.type == NPCID.GoblinPeon || npc.type == NPCID.GoblinWarrior)
                {
                    Projectile ball = Projectile.NewProjectileDirect(npc.Center, new Vector2(0, -5), ProjectileID.SpikyBall, 15, 0, npc.whoAmI);
                    ball.hostile = true;
                    ball.friendly = true;
                    ball.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                }

                if((npc.type == NPCID.AngryBones || npc.type == NPCID.AngryBonesBig || npc.type == NPCID.AngryBonesBigHelmet || npc.type == NPCID.AngryBonesBigMuscle) && Main.rand.Next(10) == 0)
                {
                    NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.CursedSkull);
                }

                if(npc.type == NPCID.DungeonSlime && NPC.downedPlantBoss)
                {
                    int paladin = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.Paladin);
                    Main.npc[paladin].scale = .65f;
                    Main.npc[paladin].defense /= 2;
                }

                //yellow slime
                if (npc.netID == -9)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int spawn = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), 1);
                        Main.npc[spawn].SetDefaults(NPCID.PurpleSlime);
                        Main.npc[spawn].velocity.X = npc.velocity.X * 2f;
                        Main.npc[spawn].velocity.Y = npc.velocity.Y;

                        NPC spawn2 = Main.npc[spawn];
                        spawn2.velocity.X = spawn2.velocity.X + (Main.rand.Next(-20, 20) * 0.1f + i * npc.direction * 0.3f);
                        NPC spawn3 = Main.npc[spawn];
                        spawn3.velocity.Y = spawn3.velocity.Y - (Main.rand.Next(0, 10) * 0.1f + i);
                        Main.npc[spawn].ai[0] = -1000 * Main.rand.Next(3);
                    }
                }
                //purple slime
                if (npc.netID == -7)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int spawn = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), 1);
                        Main.npc[spawn].SetDefaults(NPCID.RedSlime);
                        Main.npc[spawn].velocity.X = npc.velocity.X * 2f;
                        Main.npc[spawn].velocity.Y = npc.velocity.Y;

                        NPC spawn2 = Main.npc[spawn];
                        spawn2.velocity.X = spawn2.velocity.X + (Main.rand.Next(-20, 20) * 0.1f + i * npc.direction * 0.3f);
                        NPC spawn3 = Main.npc[spawn];
                        spawn3.velocity.Y = spawn3.velocity.Y - (Main.rand.Next(0, 10) * 0.1f + i);
                        Main.npc[spawn].ai[0] = -1000 * Main.rand.Next(3);
                    }
                }
                //red slime
                if (npc.netID == -8)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int spawn = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), 1);
                        Main.npc[spawn].SetDefaults(NPCID.GreenSlime);
                        Main.npc[spawn].velocity.X = npc.velocity.X * 2f;
                        Main.npc[spawn].velocity.Y = npc.velocity.Y;

                        NPC spawn2 = Main.npc[spawn];
                        spawn2.velocity.X = spawn2.velocity.X + (Main.rand.Next(-20, 20) * 0.1f + i * npc.direction * 0.3f);
                        NPC spawn3 = Main.npc[spawn];
                        spawn3.velocity.Y = spawn3.velocity.Y - (Main.rand.Next(0, 10) * 0.1f + i);
                        Main.npc[spawn].ai[0] = -1000 * Main.rand.Next(3);
                    }
                }

                if(npc.type == NPCID.DrManFly)
                {
                    for(int i = 0; i < 10; i++)
                    {
                        Projectile flask = Projectile.NewProjectileDirect(npc.Center, new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), ProjectileID.DrManFlyFlask, npc.damage * 2, 1f, npc.whoAmI);
                    }
                }

                //boss scaling
                if (npc.type == NPCID.EyeofCthulhu && FargoWorld.EyeCount < 280)
                {
                    FargoWorld.EyeCount++;
                }
                if (npc.type == NPCID.KingSlime && FargoWorld.SlimeCount < 280)
                {
                    FargoWorld.SlimeCount++;
                }
                if (npc.type == NPCID.EaterofWorldsHead && !NPC.AnyNPCs(NPCID.EaterofWorldsBody) && !NPC.AnyNPCs(NPCID.EaterofWorldsTail) && FargoWorld.EaterCount < 280)
                {
                    FargoWorld.EaterCount++;
                }
                if (npc.type == NPCID.BrainofCthulhu && FargoWorld.BrainCount < 280)
                {
                    FargoWorld.BrainCount++;
                }
                if (npc.type == NPCID.QueenBee && FargoWorld.BeeCount < 280)
                {
                    FargoWorld.BeeCount++;
                }
                if (npc.type == NPCID.SkeletronHead && FargoWorld.SkeletronCount < 280)
                {
                    FargoWorld.SkeletronCount++;
                }
                if (npc.type == NPCID.WallofFlesh && FargoWorld.WallCount < 280)
                {
                    FargoWorld.WallCount++;
                }
                if (npc.type == NPCID.TheDestroyer && FargoWorld.DestroyerCount < 120)
                {
                    FargoWorld.DestroyerCount++;
                }
                if (npc.type == NPCID.SkeletronPrime && FargoWorld.PrimeCount < 120)
                {
                    FargoWorld.PrimeCount++;
                }
                if (npc.type == NPCID.Retinazer && FargoWorld.TwinsCount < 120)
                {
                    FargoWorld.TwinsCount++;
                }
                if (npc.type == NPCID.Plantera && FargoWorld.PlanteraCount < 120)
                {
                    FargoWorld.PlanteraCount++;
                }
                if (npc.type == NPCID.Golem && FargoWorld.GolemCount < 120)
                {
                    FargoWorld.GolemCount++;
                }
                if (npc.type == NPCID.DukeFishron && FargoWorld.FishronCount < 120)
                {
                    FargoWorld.FishronCount++;
                }
                if (npc.type == NPCID.CultistBoss && FargoWorld.CultistCount < 120)
                {
                    FargoWorld.CultistCount++;
                }
                if (npc.type == NPCID.MoonLordCore && FargoWorld.MoonlordCount < 120)
                {
                    FargoWorld.MoonlordCount++;
                }

                //Revives
                if (!Revive)
                {
                    if (npc.type == NPCID.SkeletronPrime)
                    {
                        Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                        npc.life = 100;
                    }
                    else if (npc.type == NPCID.RainbowSlime)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            int slimeIndex = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.RainbowSlime);
                            NPC slime = Main.npc[slimeIndex];

                            slime.scale = 1f;
                            slime.GetGlobalNPC<FargoGlobalNPC>().Revive = true;
                            slime.velocity = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5));
                        }

                        for (int i = 0; i < 20; i++)
                        {
                            int num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, DustID.RainbowMk2, -npc.velocity.X * 0.2f, -npc.velocity.Y * 0.2f, 100, default(Color), 5f);
                            Main.dust[num469].noGravity = true;
                            Main.dust[num469].velocity *= 2f;
                            num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, DustID.RainbowMk2, -npc.velocity.X * 0.2f, -npc.velocity.Y * 0.2f, 100, default(Color), 2f);
                            Main.dust[num469].velocity *= 2f;
                        }
                    }
                    else
                    {
                        return true;
                    }

                    Revive = true;
                    return false;
                }
            }
            
            return true;
		}

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (FargoWorld.MasochistMode)
            {
                if(npc.type == NPCID.Salamander || npc.type == NPCID.Salamander2 || npc.type == NPCID.Salamander3 || npc.type == NPCID.Salamander4 || npc.type == NPCID.Salamander5 || npc.type == NPCID.Salamander6 || npc.type == NPCID.Salamander7 || npc.type == NPCID.Salamander8 || npc.type == NPCID.Salamander9)
                {
                    npc.Opacity *= 25;
                }

                if(npc.type == NPCID.GiantShelly || npc.type == NPCID.GiantShelly2)
                {
                    player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " was impaled by a Giant Shelly."), damage / 4, 0);
                }
            }
        }

        public override void ModifyHitByProjectile (NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            Player player = Main.player[Main.myPlayer];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (FargoWorld.MasochistMode)
            {
                if (npc.type == NPCID.Salamander || npc.type == NPCID.Salamander2 || npc.type == NPCID.Salamander3 || npc.type == NPCID.Salamander4 || npc.type == NPCID.Salamander5 || npc.type == NPCID.Salamander6 || npc.type == NPCID.Salamander7 || npc.type == NPCID.Salamander8 || npc.type == NPCID.Salamander9)
                {
                    npc.Opacity *= 25;
                }
            }

            //bees ignore defense
            if (modPlayer.BeeEnchant && projectile.type == ProjectileID.GiantBee)
            {
                damage = (int)(damage + npc.defense * .5);
            }

            if (projectile.type == mod.ProjectileType("FishNuke"))
			{
				damage = npc.lifeMax / 10;
				if(damage < 50)
				{
					damage = 50;
				}
			}			
		}

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            FargoPlayer modPlayer = target.GetModPlayer<FargoPlayer>(mod);

            if (FargoWorld.MasochistMode)
            {
                //SLIMES
                if (npc.netID == NPCID.GreenSlime || npc.netID == NPCID.BlueSlime || npc.type == NPCID.BunnySlimed || npc.type == NPCID.SlimeRibbonGreen || npc.type == NPCID.SlimeRibbonRed || npc.type == NPCID.SlimeRibbonWhite || npc.type == NPCID.SlimeRibbonYellow || npc.netID == NPCID.RedSlime || npc.netID == NPCID.PurpleSlime || npc.netID == NPCID.YellowSlime || npc.type == NPCID.IceSlime || npc.type == NPCID.SandSlime || npc.netID == NPCID.JungleSlime || npc.type == NPCID.SlimeSpiked || npc.type == NPCID.UmbrellaSlime || npc.type == NPCID.SlimedZombie || npc.type == NPCID.ArmedZombieSlimed || npc.netID == NPCID.Slimeling || npc.type == NPCID.Slimer)
                {
                    target.AddBuff(BuffID.Slimed, 300);
                }
                if (npc.netID == NPCID.BlackSlime)
                {
                    target.AddBuff(BuffID.Slimed, 300);
                    target.AddBuff(BuffID.Darkness, 1200);
                }
                if (npc.type == NPCID.SpikedIceSlime)
                {
                    target.AddBuff(BuffID.Slimed, 300);
                    target.AddBuff(BuffID.Frostburn, 600);
                }
                if (npc.type == NPCID.SpikedJungleSlime)
                {
                    target.AddBuff(BuffID.Slimed, 300);
                    target.AddBuff(BuffID.Venom, 600);
                }
                if (npc.type == NPCID.MotherSlime)
                {
                    target.AddBuff(BuffID.Slimed, 300);
                    target.AddBuff(mod.BuffType("Antisocial"), 1800);
                }
                if (npc.netID == NPCID.BabySlime)
                {
                    target.AddBuff(BuffID.Slimed, 240);
                }
                if (npc.type == NPCID.LavaSlime)
                {
                    target.AddBuff(BuffID.Slimed, 300);
                    target.AddBuff(BuffID.OnFire, 900);
                }
                if (npc.type == NPCID.DungeonSlime)
                {
                    target.AddBuff(BuffID.Slimed, 300);
                    target.AddBuff(BuffID.Blackout, 1200);
                }
                if (npc.netID == NPCID.Pinky)
                {
                    target.AddBuff(BuffID.Slimed, 600);
                }
                if (npc.type == NPCID.KingSlime)
                {
                    target.AddBuff(BuffID.Slimed, 600);
                    target.AddBuff(mod.BuffType("ClippedWings"), 600);

                    if(Main.rand.Next(20) == 0)
                    {
                        target.AddBuff(mod.BuffType("Stunned"), 60);
                    }
                }                
                if (npc.type == NPCID.ToxicSludge)
                {
                    target.AddBuff(BuffID.Slimed, 600);
                    target.AddBuff(mod.BuffType("Infested"), 600);
                }
                if (npc.type == NPCID.CorruptSlime)
                {
                    target.AddBuff(BuffID.Slimed, 600);
                    target.AddBuff(mod.BuffType("Rotting"), 1200);
                }
                if (npc.type == NPCID.Crimslime)
                {
                    target.AddBuff(BuffID.Slimed, 600);
                    target.AddBuff(mod.BuffType("Bloodthirsty"), 600);
                }
                if (npc.type == NPCID.Gastropod)
                {
                    target.AddBuff(BuffID.Slimed, 600);
                    target.AddBuff(mod.BuffType("Fused"), 1800);
                }
                if (npc.type == NPCID.IlluminantSlime)
                {
                    target.AddBuff(BuffID.Slimed, 600);
                    target.AddBuff(mod.BuffType("Purified"), 600);
                }
                if (npc.type == NPCID.RainbowSlime)
                {
                    target.AddBuff(BuffID.Slimed, 300);
                    target.AddBuff(mod.BuffType("FlamesoftheUniverse"), 600);
                }

                if ((npc.type == NPCID.DemonEye || npc.type == NPCID.DemonEyeOwl || npc.type == NPCID.DemonEyeSpaceship) && NPC.downedBoss2 && Main.rand.Next(4) == 0 && !target.HasBuff(BuffID.Stoned))
                {
                    target.AddBuff(BuffID.Stoned, 120);
                }

                if((npc.type == NPCID.EaterofSouls || npc.type == NPCID.Crimera) && Main.rand.Next(5) == 0)
                {
                    target.AddBuff(BuffID.Weak, 1800);
                }

                if(npc.type == NPCID.EyeofCthulhu)
                {
                    if(!target.HasBuff(mod.BuffType("Berserked")))
                    {
                        target.AddBuff(mod.BuffType("Berserked"), Main.rand.Next(300, 600));
                    }
                }

                if (npc.type == NPCID.ServantofCthulhu)
                {
                    target.AddBuff(mod.BuffType("Hexed"), 300);
                }

                if (npc.type == NPCID.QueenBee)
                {
                    target.AddBuff(mod.BuffType("Crippled"), 240);
                }

                if((npc.type == NPCID.WallofFlesh || npc.type == NPCID.WallofFleshEye) && !target.HasBuff(mod.BuffType("Unstable")))
                {
                    target.AddBuff(mod.BuffType("Unstable"), 240);
                }

                if (npc.type == NPCID.TheHungry || npc.type == NPCID.TheHungryII)
                {
                    target.AddBuff(mod.BuffType("Crippled"), 120);
                }

                if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
                {
                    target.AddBuff(mod.BuffType("Rotting"), 600);
                }

                if(npc.type == NPCID.CursedSkull)
                {
                    target.AddBuff(BuffID.Cursed, 600);
                }

                if(npc.type == NPCID.Snatcher && Main.rand.Next(2) == 0)
                {
                    target.AddBuff(BuffID.Bleeding, 1200);
                }

                if(npc.type == NPCID.ManEater && target.statLife < 100)
                {
                    target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by a Man Eater."), 999, 0);
                }

                if (npc.type == NPCID.TombCrawlerHead && target.statLife < 60)
                {
                    target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by a Tomb Crawler."), 999, 0);
                }

                if (npc.type == NPCID.DevourerHead && target.statLife < 50)
                {
                    target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by a Devourer."), 999, 0);
                }

                if (npc.type == NPCID.AngryTrapper && target.statLife < 180)
                {
                    target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by an Angry Trapper."), 999, 0);
                }

                if (npc.type == NPCID.SkeletronHead && Main.rand.Next(2) == 0)
                {
                    target.AddBuff(mod.BuffType("Lethargic"), 300);
                }

                if (npc.type == NPCID.SkeletronHand && Main.rand.Next(5) == 0)
                {
                    target.AddBuff(mod.BuffType("Stunned"), 30);
                }

                if(npc.type == NPCID.CaveBat)
                {
                    target.AddBuff(BuffID.Bleeding, 1200);
                    target.AddBuff(BuffID.Rabies, 7200);
                }

                if (npc.type == NPCID.Hellbat)
                {
                    target.AddBuff(BuffID.OnFire, 600);
                    target.AddBuff(BuffID.Rabies, 7200);
                }

                if (npc.type == NPCID.JungleBat)
                {
                    target.AddBuff(BuffID.Poisoned, 600);
                    target.AddBuff(BuffID.Rabies, 7200);
                }

                if (npc.type == NPCID.IceBat)
                {
                    if(target.HasBuff(BuffID.Chilled) && !target.HasBuff(BuffID.Frozen))
                    {
                        target.AddBuff(BuffID.Frozen, 90);
                    }

                    target.AddBuff(BuffID.Frostburn, 600);
                    target.AddBuff(BuffID.Rabies, 7200);
                }

                if (npc.type == NPCID.Lavabat)
                {
                    target.AddBuff(BuffID.Rabies, 7200);
                    target.AddBuff(BuffID.CursedInferno, 600);
                }

                if (npc.type == NPCID.GiantBat)
                {
                    target.AddBuff(BuffID.Rabies, 7200);

                    if(Main.rand.Next(5) == 0)
                    {
                        target.AddBuff(BuffID.Confused, 600);
                    }
                }

                if (npc.type == NPCID.IlluminantBat)
                {
                    target.AddBuff(BuffID.Rabies, 7200);

                    if (Main.rand.Next(4) == 0)
                    {
                        target.AddBuff(mod.BuffType("Flipped"), 1800);
                    }
                }

                if (npc.type == NPCID.GiantFlyingFox)
                {
                    target.AddBuff(BuffID.Rabies, 7200);
                    target.AddBuff(mod.BuffType("Bloodthirsty"), 600);
                }

                if (npc.type == NPCID.VampireBat || npc.type == NPCID.Vampire)
                {
                    target.AddBuff(BuffID.Darkness, 1800);
                    target.AddBuff(BuffID.Weak, 1800);
                    target.AddBuff(mod.BuffType("LivingWasteland"), 900);
                }

                if(npc.type == NPCID.SnowFlinx)
                {
                    target.AddBuff(mod.BuffType("Purified"), 600);
                }

                if(npc.type == NPCID.Piranha)
                {
                    target.AddBuff(BuffID.Bleeding, 600);
                }

                if(npc.type == NPCID.Medusa)
                {
                    if(!target.HasBuff(BuffID.Stoned))
                    {
                        target.AddBuff(BuffID.Stoned, 120);
                    }
                    
                    target.AddBuff(mod.BuffType("Flipped"), 180);
                }

                if (npc.type == NPCID.SpikeBall)
                {
                    target.AddBuff(BuffID.BrokenArmor, 1200);
                }

                if (npc.type == NPCID.BlazingWheel)
                {
                    target.AddBuff(BuffID.OnFire, 600);
                }

                if (npc.type == NPCID.Shark)
                {
                    target.AddBuff(BuffID.Bleeding, 300);
                }

                if ((npc.type == NPCID.BlueJellyfish || npc.type == NPCID.PinkJellyfish) && target.wet && Main.rand.Next(2) == 0)
                {
                    target.AddBuff(BuffID.Electrified, 240);
                }

                if (npc.type == NPCID.GraniteFlyer && Main.rand.Next(10) == 0 && !target.HasBuff(BuffID.Stoned))
                {
                    target.AddBuff(BuffID.Stoned, 120);
                }

                if (npc.type == NPCID.GraniteGolem && Main.rand.Next(2) == 0 && !target.HasBuff(BuffID.Stoned))
                {
                    target.AddBuff(BuffID.Stoned, 120);
                }

                if(npc.type == NPCID.LeechHead)
                {
                    target.AddBuff(BuffID.Bleeding, 900);
                }

                if(npc.type == NPCID.AnomuraFungus)
                {
                    target.AddBuff(BuffID.Poisoned, 600);
                }

                if (npc.type == NPCID.WaterSphere && Main.rand.Next(4) == 0)
                {
                    target.AddBuff(mod.BuffType("Flipped"), 1200);
                }

                if (npc.type == NPCID.DevourerHead)
                {
                    target.AddBuff(BuffID.BrokenArmor, 900);
                }

                if (npc.type == NPCID.GiantShelly || npc.type == NPCID.GiantShelly2)
                {
                    target.AddBuff(BuffID.Slow, 600);
                }

                if (npc.type == NPCID.Squid)
                {
                    target.AddBuff(BuffID.Obstructed, 240);
                }

                if (npc.type == NPCID.BloodZombie && Main.rand.Next(3) == 0)
                {
                    target.AddBuff(mod.BuffType("Bloodthirsty"), 240);     
                }

                if(npc.type == NPCID.Drippler && Main.rand.Next(2) == 0)
                {
                    target.AddBuff(mod.BuffType("Rotting"), 600);
                }

                if (npc.type == NPCID.ChaosBall)
                {
                    target.AddBuff(BuffID.ShadowFlame, 600);
                }

                if (npc.type == NPCID.Tumbleweed)
                {
                    target.AddBuff(mod.BuffType("Crippled"), 300);
                }

                if (npc.type == NPCID.CorruptBunny || npc.type == NPCID.CrimsonBunny || npc.type == NPCID.CorruptGoldfish || npc.type == NPCID.CrimsonGoldfish || npc.type == NPCID.CorruptPenguin || npc.type == NPCID.CrimsonPenguin || npc.type == NPCID.MothronSpawn || npc.type == NPCID.PigronCorruption || npc.type == NPCID.PigronCrimson || npc.type == NPCID.PigronHallow)
                {
                    target.AddBuff(mod.BuffType("SqueakyToy"), 600);
                }

                if (npc.type == NPCID.FaceMonster)
                {
                    target.AddBuff(BuffID.Rabies, 1800);
                }

                if (npc.type == NPCID.Harpy && Main.rand.Next(5) == 0)
                {
                    target.AddBuff(mod.BuffType("ClippedWings"), 480);
                }

                if (npc.type == NPCID.SeaSnail)
                {
                    target.AddBuff(BuffID.OgreSpit, 600);
                }

                if (npc.type == NPCID.BrainofCthulhu)
                {
                    target.AddBuff(mod.BuffType("Infested"), 600);
                }

                if(npc.type == NPCID.Creeper && Main.rand.Next(30) == 0)
                {
                    int[] debuffs = {mod.BuffType("Berserked"), mod.BuffType("Lethargic"), mod.BuffType("Flipped")};
                    target.AddBuff(debuffs[Main.rand.Next(debuffs.Length)], 600);
                }

                if(npc.type == NPCID.SwampThing)
                {
                    target.AddBuff(BuffID.OgreSpit, 300);
                }

                if (npc.type == NPCID.Frankenstein)
                {
                    target.AddBuff(mod.BuffType("LightningRod"), 600);
                }

                if (npc.type == NPCID.Reaper && !target.HasBuff(mod.BuffType("MarkedforDeath")))
                {
                    target.AddBuff(mod.BuffType("MarkedforDeath"), 1800);
                    target.AddBuff(mod.BuffType("LivingWasteland"), 900);
                }

                if (npc.type == NPCID.Butcher)
                {
                    target.AddBuff(mod.BuffType("Berserked"), 300);
                }

                if (npc.type == NPCID.ThePossessed)
                {
                    target.AddBuff(mod.BuffType("Hexed"), 300);
                }

                if (npc.type == NPCID.Wolf || npc.type == NPCID.Werewolf)
                {
                    target.AddBuff(BuffID.Rabies, 3000);
                }

                //all armored bones
                if(npc.type <= 80 && npc.type >= 69)
                {
                    target.AddBuff(mod.BuffType("Bloodthirsty"), 120);
                }

                if (npc.type == NPCID.GiantTortoise)
                {
                    target.AddBuff(mod.BuffType("Defenseless"), 300);
                }

                if (npc.type == NPCID.IceTortoise)
                {
                    target.AddBuff(mod.BuffType("Defenseless"), 300);

                    if(Main.rand.Next(10) == 0)
                    {
                        target.AddBuff(BuffID.Frozen, 120);
                    }
                }

                //CULTIST OP
                if (npc.type == NPCID.AncientDoom)
                {
                    target.AddBuff(mod.BuffType("MarkedforDeath"), 900);
                }
                if (npc.type == NPCID.AncientLight)
                {
                    target.AddBuff(mod.BuffType("Purified"), 240);
                }
                if (npc.type == NPCID.CultistBossClone)
                {
                    target.AddBuff(mod.BuffType("Hexed"), 300);
                }

                if (npc.type == NPCID.MossHornet)
                {
                    target.AddBuff(mod.BuffType("Infested"), 300);
                }

                if (npc.type == NPCID.Paladin)
                {
                    target.AddBuff(mod.BuffType("Lethargic"), 600);
                }

                if (npc.type == NPCID.DukeFishron)
                {
                    target.AddBuff(mod.BuffType("MutantNibble"), 600);
                }

                if (npc.type == NPCID.Hellhound)
                {
                    target.AddBuff(BuffID.WitheredWeapon, 900);
                    target.AddBuff(BuffID.Obstructed, 300);
                }

                if (npc.type == NPCID.Mimic || npc.type == NPCID.PresentMimic || npc.type == NPCID.BigMimicCorruption || npc.type == NPCID.BigMimicCrimson || npc.type == NPCID.BigMimicHallow || npc.type == NPCID.BigMimicJungle)
                {
                    target.AddBuff(mod.BuffType("Purified"), 180);
                }

                if (npc.type == NPCID.RuneWizard)
                {
                    target.AddBuff(BuffID.Cursed, 480);
                }

                if (npc.type == NPCID.Nutcracker || npc.type == NPCID.NutcrackerSpinning)
                {
                    target.AddBuff(mod.BuffType("ClippedWings"), 900);
                }

                if (npc.type == NPCID.Wraith)
                {
                    target.AddBuff(mod.BuffType("LivingWasteland"), 900);
                }

                if (npc.type == NPCID.Plantera)
                {
                    target.AddBuff(mod.BuffType("Infested"), 1500);
                }

                if (npc.type == NPCID.Spore)
                {
                    target.AddBuff(mod.BuffType("Infested"), 900);
                }

                if (npc.type == NPCID.ChaosElemental && Main.rand.Next(10) == 0)
                {
                    target.AddBuff(mod.BuffType("Unstable"), 900);
                }

                if (npc.type == NPCID.Flocko)
                {
                    target.AddBuff(BuffID.Chilled, 300);
                    target.AddBuff(BuffID.Frostburn, 600);
                }

                if(Main.hardMode && Main.rand.Next(5) == 0 && (npc.type == NPCID.GoblinPeon || npc.type == NPCID.PirateDeckhand || npc.type == NPCID.GrayGrunt))
                {
                    target.AddBuff(mod.BuffType("LivingWasteland"), 600);
                }

                if (npc.type == NPCID.Zombie)
                {
                    target.AddBuff(mod.BuffType("Rotting"), 300);
                }

                if (npc.type == NPCID.Corruptor)
                {
                    target.AddBuff(BuffID.Weak, 7200);
                }

                if (npc.type == NPCID.Mummy || npc.type == NPCID.DarkMummy || npc.type == NPCID.LightMummy)
                {
                    target.AddBuff(BuffID.Webbed, 480);
                }

                if (npc.type == NPCID.Derpling)
                {
                    target.AddBuff(mod.BuffType("Lethargic"), 600);
                }

                if (npc.type == NPCID.SkeletronPrime || npc.type == NPCID.PrimeCannon || npc.type == NPCID.PrimeLaser || npc.type == NPCID.PrimeSaw || npc.type == NPCID.PrimeVice || npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail || npc.type == NPCID.Spazmatism || npc.type == NPCID.Retinazer)
                {
                    target.AddBuff(mod.BuffType("ClippedWings"), 60);
                }

                if (npc.type == NPCID.MoonLordLeechBlob)
                {
                    target.AddBuff(mod.BuffType("MutantNibble"), 900);
                }

                if (npc.type == NPCID.BlackRecluse)
                {
                    target.AddBuff(BuffID.Venom, 900);
                }

                if (npc.type == NPCID.DesertBeast)
                {
                    target.AddBuff(mod.BuffType("Crippled"), 900);
                }


                if (npc.type == NPCID.Krampus)
                {
                    
                }

                    /*if (npc.type == NPCID.GiantFlyingFox)
                    {
                        target.AddBuff(mod.BuffType("Infested"), 6000);
                    }*/


                }
           
            if(target.HasBuff(mod.BuffType("ShellHide")))
            {
                damage *= 2;
            }

            if(SqueakyToy)
            {
                damage = 1;
                modPlayer.Squeak(target.Center);
            }
        }


        public override bool StrikeNPC (NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
            bool retValue = true;

			Player player = Main.player[Main.myPlayer];
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			
			if (FargoWorld.MasochistMode)
            {
                NPC damagedNpc = npc;
                //basically, is this a segment?
                if (npc.realLife >= 0)
                {
                    damagedNpc = Main.npc[damagedNpc.realLife];
                }

                if(damagedNpc.type == NPCID.SkeletronPrime)
                {
                    int armCount = 0;
                    bool[] arms = { NPC.AnyNPCs(NPCID.PrimeCannon), NPC.AnyNPCs(NPCID.PrimeLaser), NPC.AnyNPCs(NPCID.PrimeSaw), NPC.AnyNPCs(NPCID.PrimeVice) };

                    for(int i = 0; i < 4; i++)
                    {
                        if(arms[i])
                        {
                            armCount++;
                        }
                    }

                    switch (armCount)
                    {
                        case 4:
                            damage *= .05;
                            break;
                        case 3:
                            damage *= .10;
                            break;
                        case 2:
                            damage *= .25;
                            break;
                        case 1:
                            damage *= .50;
                            break;
                        default:
                            break;
                    }

                }

                ResetRegenTimer(damagedNpc);
			}

			if(modPlayer.UniverseEffect)
			{
				if(crit)
				{
					damage *= 5;
                    retValue = false;
				}
			}

            if(modPlayer.RedEnchant)
            {
                switch(npc.life / npc.lifeMax * 100)
                {
                    case 50:
                        damage *= 1.1;
                        break;
                    case 25:
                        damage *= 1.4;
                        break;
                    case 10:
                        damage *= 2;
                        break;
                }
            }
			
			if(crit && modPlayer.ShroomEnchant && !modPlayer.TerrariaSoul && modPlayer.IsStandingStill)
			{
			    damage *= 4;
                retValue = false;
			}
			
			if(modPlayer.FirstStrike && npc.lifeMax == npc.life)
			{
                crit = true;
                retValue = false;
            }
			
			//normal damage calc
			return retValue;
		}
		
		public override void OnHitByItem (NPC npc, Player player, Item item, int damage, float knockback, bool crit)
		{
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			
            if(modPlayer.ValhallaEnchant && Soulcheck.GetValue("Valhalla Knockback") && npc.type != NPCID.TargetDummy && npc.knockBackResist < 1)
            {
                npc.knockBackResist += .1f;
            }
		}

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            FargoPlayer modPlayer = Main.player[projectile.owner].GetModPlayer<FargoPlayer>(mod);

            //spears
            if(modPlayer.ValhallaEnchant && Soulcheck.GetValue("Valhalla Knockback") && (projectile.aiStyle == 19 || modPlayer.WillForce) && npc.type != NPCID.TargetDummy && npc.knockBackResist < 1)
            {
                npc.knockBackResist += .1f;
            }
            else
            {
                NPC n = new NPC();
                n.SetDefaults(npc.type);
                npc.knockBackResist = n.knockBackResist;
            }
        }
    }
}