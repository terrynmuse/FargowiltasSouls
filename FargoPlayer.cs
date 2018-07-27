using System;
using System.Collections.Generic;
using System.Linq;
using FargowiltasSouls.NPCs;
using FargowiltasSouls.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace FargowiltasSouls
{
    public class FargoPlayer : ModPlayer
    {
        //for convenience
        public bool IsStandingStill;

        public bool Wood;
        public bool Eater;
        public bool QueenStinger;
        public bool Infinity;
        public bool NpcBoost = false;

        //minions
        public bool BrainMinion;
        public bool EaterMinion;

        //enchantments
        public bool ShadowEnchant;
        public bool CrimsonEnchant;
        public bool SpectreEnchant;
        public bool SpecHeal;
        public int HealTown;
        public bool BeeEnchant;
        public bool SpiderEnchant;
        public bool StardustEnchant;
        public bool MythrilEnchant;
        public bool FossilEnchant;
        public bool JungleEnchant;
        public bool ElementEnchant;
        public bool ShroomEnchant;
        public bool CobaltEnchant;
        public bool SpookyEnchant;
        public bool HallowEnchant;
        public bool ChloroEnchant;
        public bool VortexEnchant;
        public static int VortexCrit = 4;
        public bool VortexStealth;
        public bool AdamantiteEnchant;
        public bool FrostEnchant;
        public bool PalladEnchant;
        public bool OriEnchant;
        public bool MeteorEnchant;
        private int meteorTimer = 150;
        private int meteorCD = 0;
        private int prevMana;
        private bool meteorShower = false;

        public bool MoltenEnchant;


        public bool CopperEnchant;
        public bool NinjaEnchant;
        public bool FirstStrike;
        public bool NearSmoke;
        public bool IronEnchant;
        public bool TurtleEnchant;
        public bool LeadEnchant;
        public bool GladEnchant;
        public bool GoldEnchant;
        public bool CactusEnchant;
        private int cactusCD;
        public bool BeetleEnchant;
        public bool ForbiddenEnchant;
        public bool MinerEnchant;
        public bool TungstenEnchant;
        public bool PumpkinEnchant;
        private int pumpkinCD;
        public bool SilverEnchant;

        //pets
        public bool FlickerPet;
        public bool MoonEye;
        public bool DinoPet;
        public bool SeedPet;
        public bool DogPet;
        public bool SkullPet;
        public bool SaplingPet;
        public bool LizPet;
        public bool EyePet;
        public bool DragPet;
        public bool ShadowPet;
        public bool ShadowPet2;
        public bool CrimsonPet;
        public bool CrimsonPet2;
        public bool PenguinPet;
        public bool SnowmanPet;
        public bool FishPet;
        public bool CubePet;
        public bool GrinchPet;
        public bool SuspiciousEyePet;

        //soul effects
        public bool MeleeEffect;
        public bool ThrowSoul;
        public bool RangedEffect;
        public bool MiniRangedEffect;
        public bool BuilderEffect;
        public bool BuilderMode;
        public bool UniverseEffect;
        public bool SpeedEffect;
        public bool TankEffect;
        public bool FishSoul1;
        public bool FishSoul2;
        public bool DimensionSoul;
        public bool TerrariaSoul;
        public int HealTimer;
        public bool VoidSoul;

        //debuffs
        public bool Hexed;
        public bool Unstable;
        private int _unstableCd;
        public bool Fused;
        public bool Shadowflame;

        public bool GodEater;               //defense removed, endurance removed, colossal DOT
        public bool FlamesoftheUniverse;    //activates various vanilla debuffs
        public bool MutantNibble;           //disables potions, moon bite effect, feral bite effect, disables lifesteal
        public int StatLifePrevious = -1;           //used for mutantNibble
        public bool Asocial;                //disables minions, disables pets
        public bool WasAsocial;
        public bool HidePetToggle0 = true;
        public bool HidePetToggle1 = true;
        public bool Kneecapped;             //disables running :v
        public bool Defenseless;            //-30 defense, no damage reduction, cross necklace and knockback prevention effects disabled
        public bool Lethargic;              //all item speed reduced to 75%
        public bool Purified;               //purges all other buffs
        public bool Infested;               //weak DOT that grows exponentially stronger
        public int MaxInfestTime;
        public bool FirstInfection = true;
        public float InfestedDust;
        public bool Rotting;                //inflicts DOT and almost every stat reduced
        public bool SqueakyToy;             //all attacks do one damage and make squeaky noises
        public bool Bloodthirst;
        public bool Atrophied;              //melee speed and damage reduced. maybe player cannot fire melee projectiles?
        public bool Jammed;                 //ranged damage and speed reduced, all non-custom ammo set to baseline ammos
        public bool Slimed;



        public override TagCompound Save()
        {
            TagCompound tagCompound = new TagCompound();
            foreach (KeyValuePair<String, Boolean> entry in Soulcheck.ToggleDict)
            {
                tagCompound.Add(entry.Key, entry.Value);
            }
            return tagCompound;
            // return base.Save();
        }
        public override void Load(TagCompound tag)
        {
            foreach (KeyValuePair<String, Object> entry in tag)
            {
                try
                {
                    if (Soulcheck.ToggleDict.ContainsKey(entry.Key))
                    {
                        Soulcheck.ToggleDict[entry.Key] = (bool)entry.Value;
                    }
                    else
                    {
                        Soulcheck.ToggleDict.Add(entry.Key, (bool)entry.Value);
                    }
                }
                catch (Exception e) { }
            }
            base.Load(tag);
        }


        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Fargowiltas.CheckListKey.JustPressed)
            {
                if (Soulcheck.Visible == false)
                {
                    Soulcheck.Visible = true;
                }
                else
                {
                    Soulcheck.Visible = false;
                }
            }

            //may need cooldown?
            if (VoidSoul && Fargowiltas.HomeKey.JustPressed)
            {
                if (Main.rand.Next(2) == 0)
                    Dust.NewDust(player.position, player.width, player.height, 15, 0.0f, 0.0f, 150, Color.White, 1.1f);

                for (int index = 0; index < 70; ++index)
                    Dust.NewDust(player.position, player.width, player.height, 15, (float)(player.velocity.X * 0.5), (float)(player.velocity.Y * 0.5), 150, Color.White, 1.5f);
                player.grappling[0] = -1;
                player.grapCount = 0;
                for (int index = 0; index < 1000; ++index)
                {
                    if (Main.projectile[index].active && Main.projectile[index].owner == player.whoAmI && Main.projectile[index].aiStyle == 7)
                        Main.projectile[index].Kill();
                }
                player.Spawn();
                for (int index = 0; index < 70; ++index)
                    Dust.NewDust(player.position, player.width, player.height, 15, 0.0f, 0.0f, 150, Color.White, 1.5f);
            }
        }
        public override void ResetEffects()
        {

            Wood = false;
            Eater = false;

            QueenStinger = false;
            Infinity = false;

            BrainMinion = false;
            EaterMinion = false;

            //enchantments
            ShadowEnchant = false;
            CrimsonEnchant = false;
            SpectreEnchant = false;
            BeeEnchant = false;
            SpiderEnchant = false;
            StardustEnchant = false;
            MythrilEnchant = false;
            FossilEnchant = false;
            JungleEnchant = false;
            ElementEnchant = false;
            ShroomEnchant = false;
            CobaltEnchant = false;
            SpookyEnchant = false;
            HallowEnchant = false;
            ChloroEnchant = false;
            VortexEnchant = false;
            VortexStealth = false;
            AdamantiteEnchant = false;
            FrostEnchant = false;
            PalladEnchant = false;
            OriEnchant = false;
            MeteorEnchant = false;
            MoltenEnchant = false;
            CopperEnchant = false;
            NinjaEnchant = false;
            FirstStrike = false;
            NearSmoke = false;
            IronEnchant = false;
            TurtleEnchant = false;
            LeadEnchant = false;
            GladEnchant = false;
            GoldEnchant = false;
            CactusEnchant = false;
            BeetleEnchant = false;
            ForbiddenEnchant = false;
            MinerEnchant = false;
            TungstenEnchant = false;
            PumpkinEnchant = false;
            SilverEnchant = false;

            //pets
            FlickerPet = false;
            MoonEye = false;
            DinoPet = false;
            SeedPet = false;
            DogPet = false;
            SkullPet = false;
            SaplingPet = false;
            LizPet = false;
            EyePet = false;
            DragPet = false;
            ShadowPet = false;
            ShadowPet2 = false;
            CrimsonPet = false;
            CrimsonPet2 = false;
            PenguinPet = false;
            SnowmanPet = false;
            FishPet = false;
            CubePet = false;
            GrinchPet = false;
            SuspiciousEyePet = false;
            


            //add missing pets to kill pets!

            //souls
            MeleeEffect = false;
            ThrowSoul = false;
            RangedEffect = false;
            MiniRangedEffect = false;
            BuilderEffect = false;
            BuilderMode = false;
            UniverseEffect = false;
            SpeedEffect = false;
            TankEffect = false;
            FishSoul1 = false;
            FishSoul2 = false;
            DimensionSoul = false;
            TerrariaSoul = false;
            HealTimer = 0;
            VoidSoul = false;

            //debuffs
            Hexed = false;
            Unstable = false;
            Fused = false;
            Shadowflame = false;
            Slimed = false;

            GodEater = false;
            FlamesoftheUniverse = false;
            MutantNibble = false;
            Asocial = false;
            Kneecapped = false;
            Defenseless = false;
            Lethargic = false;
            Purified = false;
            Infested = false;
            Rotting = false;
            SqueakyToy = false;
            Bloodthirst = false;
            Atrophied = false;
            Jammed = false;
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (MoltenEnchant)
            {
                Projectile.NewProjectileDirect(player.Center, Vector2.Zero, mod.ProjectileType("Explosion"), (int)(500 * player.meleeDamage), 0f, Main.myPlayer);
            }

            if (VoidSoul)
            {
                player.respawnTimer = (int)(player.respawnTimer * .5);
            }

            //remove this after testing you fool
            player.respawnTimer = (int)(player.respawnTimer * .01);
        }

        public override void UpdateDead()
        {
            //debuffs
            Hexed = false;
            Unstable = false;
            Fused = false;
            Shadowflame = false;
            Slimed = false;

            GodEater = false;
            FlamesoftheUniverse = false;
            MutantNibble = false;
            Asocial = false;
            Kneecapped = false;
            Defenseless = false;
            Lethargic = false;
            Purified = false;
            Infested = false;
            Rotting = false;
            SqueakyToy = false;
            Bloodthirst = false;
            Atrophied = false;
            Jammed = false;
        }

        public override void PreUpdate()
        {
            IsStandingStill = Math.Abs(player.velocity.X) < 0.05 && Math.Abs(player.velocity.Y) < 0.05;

            if (FargoWorld.MasochistMode)
            {
                if (Main.bloodMoon)
                {
                    player.buffImmune[BuffID.Bleeding] = false;
                    player.AddBuff(BuffID.Bleeding, 2);
                }

                if(player.HasBuff(BuffID.Webbed) && NPC.AnyNPCs(NPCID.BlackRecluse))
                {
                    player.AddBuff(mod.BuffType("Defenseless"), 300);
                }
            }

            if (!Infested && !FirstInfection)
            {
                FirstInfection = true;
            }

            if(CactusEnchant && cactusCD !=0)
            {
                cactusCD--;
            }

            if(NinjaEnchant)
            {
                //ninja smoke bomb nonsense
                Projectile nearestProj = null;
                float distance = 4 * 16;

                Main.projectile.Where(x => x.type == ProjectileID.SmokeBomb && x.active).ToList().ForEach(x =>
                {
                    if (nearestProj == null && Vector2.Distance(x.Center, player.Center) <= distance)
                    {
                        nearestProj = x;
                        distance = Vector2.Distance(x.Center, player.Center);
                    }
                    else if (nearestProj != null && Vector2.Distance(nearestProj.Center, player.Center) <= distance)
                    {
                        nearestProj = x;
                        distance = Vector2.Distance(nearestProj.Center, player.Center);
                    }
                });

                if (nearestProj != null)
                {
                    player.AddBuff(mod.BuffType("FirstStrike"), 300);
                }
            }

            if(TurtleEnchant && IsStandingStill && !player.controlUseItem)
            {
                player.AddBuff(mod.BuffType("ShellHide"), 2);
            }

            //reflect proj
            if (HallowEnchant)
            {
                float distance = 5f * 16;

                Main.projectile.Where(x => x.active && x.hostile).ToList().ForEach(x =>
                {
                    if (Vector2.Distance(x.Center, player.Center) <= distance && Main.rand.Next(5) == 0)
                    {
                        for(int i = 0; i < 5; i++)
                        {
                            int dustId = Dust.NewDust(new Vector2(x.position.X, x.position.Y + 2f), x.width, x.height + 5, DustID.GoldFlame, x.velocity.X * 0.2f, x.velocity.Y * 0.2f, 100, default(Color), 3f);
                            Main.dust[dustId].noGravity = true;
                        }

                        // Set ownership
                        x.hostile = false;
                        x.friendly = true;
                        x.owner = player.whoAmI;

                        // Turn around
                        x.velocity *= -1f;
                        //x.penetrate = 1;

                        // Flip sprite
                        if (x.Center.X > player.Center.X * 0.5f)
                        {
                            x.direction = 1;
                            x.spriteDirection = 1;
                        }
                        else
                        {
                            x.direction = -1;
                            x.spriteDirection = -1;
                        }

                        // Don't know if this will help but here it is
                        x.netUpdate = true;
                    }
                });
            }

            if (MeteorEnchant)
            {
                if (meteorShower)
                {
                    if (meteorTimer % 2 == 0)
                    {
                        Projectile.NewProjectile(player.Center.X + Main.rand.Next(-1000, 1000), player.Center.Y - 1000, Main.rand.Next(-2, 2), 0f + Main.rand.Next(8, 12), Main.rand.Next(424, 427), (int)(50 * player.magicDamage), 0f, Main.myPlayer, 0f, 0.5f + (float)Main.rand.NextDouble() * 0.3f);
                    }

                    meteorTimer--;

                    if (meteorTimer <= 0)
                    {
                        meteorCD = 0;
                        meteorTimer = 150;
                        meteorShower = false;
                    }
                }
                else
                {
                    if (meteorCD <= 0)
                    {
                        prevMana = player.statMana;
                        meteorCD = 150;
                    }
                    else
                    {
                        meteorCD--;

                        if (prevMana - player.statMana >= 50 || (player.HeldItem.mana >= 50 && player.controlUseItem))
                        {
                            //commence shower
                            meteorShower = true;
                        }
                    }
                }  
            }

            if(PumpkinEnchant && (player.controlLeft || player.controlRight) && !IsStandingStill)
            {
                if(pumpkinCD <= 0)
                {
                    Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileID.MolotovFire, (int)(12 * player.magicDamage), 1f, player.whoAmI);
                    pumpkinCD = 10;
                }

                pumpkinCD--;
            }

            //tele through wall until open space on dash into wall
            if (VoidSoul && player.dashDelay > 0 && player.velocity.X == 0)
            {
                var teleportPos = new Vector2();
                int direction = player.direction;

                teleportPos.X = player.position.X + direction;
                teleportPos.Y = player.position.Y;

                while (Collision.SolidCollision(teleportPos, player.width, player.height))
                {
                    if (direction == 1)
                    {
                        teleportPos.X++;
                    }
                    else
                    {
                        teleportPos.X--;
                    }
                }
                if (teleportPos.X > 50 && teleportPos.X < (double)(Main.maxTilesX * 16 - 50) && teleportPos.Y > 50 && teleportPos.Y < (double)(Main.maxTilesY * 16 - 50))
                {
                    player.Teleport(teleportPos, 1);
                    NetMessage.SendData(65, -1, -1, null, 0, player.whoAmI, teleportPos.X, teleportPos.Y, 1);
                }
            }


            if (Unstable)
            {
                if (_unstableCd >= 60)
                {
                    Vector2 pos = Main.screenPosition;

                    int x = Main.rand.Next((int)pos.X, (int)pos.X + Main.screenWidth);
                    int y = Main.rand.Next((int)pos.Y, (int)pos.Y + Main.screenHeight);
                    Vector2 teleportPos = new Vector2(x, y);

                    while (Collision.SolidCollision(teleportPos, player.width, player.height) && teleportPos.X > 50 && teleportPos.X < (double)(Main.maxTilesX * 16 - 50) && teleportPos.Y > 50 && teleportPos.Y < (double)(Main.maxTilesY * 16 - 50))
                    {
                        x = Main.rand.Next((int)pos.X, (int)pos.X + Main.screenWidth);
                        y = Main.rand.Next((int)pos.Y, (int)pos.Y + Main.screenHeight);
                        teleportPos = new Vector2(x, y);
                    }

                    player.Teleport(teleportPos, 1);
                    NetMessage.SendData(65, -1, -1, null, 0, player.whoAmI, teleportPos.X, teleportPos.Y, 1);

                    _unstableCd = 0;
                }
                _unstableCd++;
            }
        }

        public override void PostUpdate()
        {

        }

        public override void PostUpdateMiscEffects()
        {
            if (Slimed)
            {
                //slowed effect
                player.moveSpeed /= 2f;
                player.jump /= 2;
            }

            if (GodEater)
            {
                player.statDefense = 0;
                player.endurance = 0;
            }

            if (MutantNibble)
            {
                //disables lifesteal, mostly
                if (player.statLife > StatLifePrevious)
                    player.statLife = StatLifePrevious;
                else
                    StatLifePrevious = player.statLife;
            }
            else
            {
                StatLifePrevious = player.statLife;
            }

            if (Defenseless)
            {
                player.statDefense -= 30;
                player.endurance = 0;
                player.longInvince = false;
                player.noKnockback = false;
            }

            if (Purified)
            {
                KillPets();

                //removes all buffs/debuffs, but it interacts really weirdly with luiafk infinite potions.
                
                for (int i = 0; i < 22; i++)
                {
                    if (player.buffType[i] > 0 && player.buffTime[i] > 0 && Array.IndexOf(Fargowiltas.DebuffIDs, player.buffType[i]) == -1)
                    {
                        player.DelBuff(i);
                    }
                }
            }
            else if (Asocial)
            {
                KillPets();
                player.maxMinions = 0;
                player.minionDamage -= .5f;
            }
            else if (WasAsocial) //should only occur when above debuffs end
            {
                player.hideMisc[0] = HidePetToggle0;
                player.hideMisc[1] = HidePetToggle1;

                WasAsocial = false;
            }

            if (Rotting)
            {
                player.moveSpeed *= 0.75f;
            }

            if (Kneecapped)
            {
                player.accRunSpeed = player.maxRunSpeed;
            }

            if (Atrophied)
            {
                player.meleeSpeed = 0f; //melee silence
                player.thrownVelocity = 0f;
                //just in case
                player.meleeDamage = 0.01f;
                player.meleeCrit = 0;
            }
        }

        public override void SetupStartInventory(IList<Item> items)
        {
            Item item = new Item();
            item.SetDefaults(mod.ItemType("Masochist"));
            items.Add(item);
        }

        public override float UseTimeMultiplier(Item item)
        {
            float multiplier = 1f;

            if (Lethargic)
            {
                multiplier *= .5f;
            }

            if (Rotting)
            {
                multiplier *= .75f;
            }

            if(TungstenEnchant)
            {
                multiplier *= .125f;
            }

            if(MythrilEnchant)
            {
                multiplier *= 1.3f;
            }

            return multiplier;
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {

        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (CactusEnchant && cactusCD == 0)
            {
                Projectile[] projs = FargoGlobalProjectile.XWay(16, player.Center, ProjectileID.PineNeedleFriendly, 5, (int)(30 * player.meleeDamage), 5f);

                for(int i = 0; i < projs.Length; i++)
                {
                    Projectile p = projs[i];
                    p.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                    p.magic = false;
                    p.melee = true;
                }

                cactusCD = 30;
            }
        }

        public override void OnHitAnything(float x, float y, Entity victim)
        {

        }

        public override void PostUpdateRunSpeeds()
        {
            if (!SpeedEffect) return;
            player.maxRunSpeed = 1000;
            player.accRunSpeed = 2;
            player.runAcceleration = 5;
        }

        public override void UpdateBadLifeRegen()
        {
            if (Shadowflame)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 10;

            }

            if (GodEater)
            {
                if (player.lifeRegen > 0)
                    player.lifeRegen = 0;

                player.lifeRegenTime = 0;
                player.lifeRegen -= 90;
            }

            if (MutantNibble)
            {
                if (player.lifeRegen > 0)
                    player.lifeRegen = 0;

                if (player.lifeRegenCount > 0)
                    player.lifeRegenCount = 0;

                player.lifeRegenTime = 0;
            }

            if (Infested)
            {
                if (player.lifeRegen > 0)
                    player.lifeRegen = 0;

                player.lifeRegenTime = 0;

                player.lifeRegen -= InfestedExtraDot();
            }

            if (Rotting)
            {
                if (player.lifeRegen > 0)
                    player.lifeRegen = 0;

                if (player.lifeRegenCount > 0)
                    player.lifeRegenCount = 0;

                player.lifeRegenTime = 0;

                player.lifeRegen -= 12;
            }


        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {

            if (Shadowflame)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, DustID.Shadowflame, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);

                    fullBright = true;
                }
            }

            if (Infested)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, DustID.Shadowflame, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), InfestedDust);
                    Main.dust[dust].noGravity = true;
                    //Main.dust[dust].velocity *= 1.8f;
                    // Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);

                    fullBright = true;
                }
            }

            if (GodEater) //plague dust code but its pink
            {
                if (Main.rand.Next(3) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 86, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.2f;
                    Main.dust[dust].velocity.Y -= 0.15f;
                    Main.playerDrawDust.Add(dust);
                }
                //plague -> proportions HEX DEC newcolor
                r *= 0.15f; //0.07f -> 0.50 FF 255 0.15f
                g *= 0.03f; //0.15f -> 1.00 33 051 0.03f
                b *= 0.09f; //0.01f -> ?.?? 99 153 0.09f
                fullBright = true;
            }

            if (FlamesoftheUniverse)
            {
                drawInfo.drawPlayer.onFire = true;
                drawInfo.drawPlayer.onFire2 = true;
                drawInfo.drawPlayer.onFrostBurn = true;
                drawInfo.drawPlayer.ichor = true;
                drawInfo.drawPlayer.burned = true;

                //shadowflame
                if (Main.rand.Next(4) != 0 || drawInfo.shadow != 0f) return;
                int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, DustID.Shadowflame, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
                Main.playerDrawDust.Add(dust);

                fullBright = true;

            }
        }


        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Hexed)
            {
                target.life += damage;
                target.HealEffect(damage);

                if (target.life > target.lifeMax)
                {
                    target.life = target.lifeMax;
                }

                damage = 0;
                knockback = 0;
                crit = false;

                return;

            }

            if (SqueakyToy)
            {
                damage = 1;
                Squeak(target.Center);
                return;
            }

            if (proj.type == ProjectileID.Kraken || proj.type == ProjectileID.Trident || proj.type == ProjectileID.Flairon || proj.type == ProjectileID.FlaironBubble || proj.type == ProjectileID.WaterStream || proj.type == ProjectileID.WaterBolt || proj.type == ProjectileID.RainNimbus || proj.type == ProjectileID.Bubble)
            {
                target.AddBuff(BuffID.Wet, 180, true);
            }

            if (TerrariaSoul)
            {
                target.AddBuff(BuffID.Venom, 240, true);
            }

            if (QueenStinger)
            {
                target.AddBuff(BuffID.Poisoned, 120, true);
            }

            if (SpiderEnchant && proj.minion)
            {
                target.AddBuff(mod.BuffType("Swarmed"), 600);
            }

            /*if (meleeEffect)
            {
               if(proj.melee)
			   {
				  target.AddBuff(BuffID.CursedInferno, 240, true);
			   }  
			}   */

            if (RangedEffect)
            {
                if (proj.ranged)
                {
                    target.AddBuff(BuffID.ShadowFlame, 240, true);
                }
            }

            if (UniverseEffect)
            {
                target.AddBuff(mod.BuffType("FlamesoftheUniverse"), 240, true);
            }

            if (ShadowEnchant && Main.rand.Next(10) == 0)
            {
                target.AddBuff(BuffID.CursedInferno, 120, true);
            }

            if (CopperEnchant && !target.GetGlobalNPC<FargoGlobalNPC>().Shock)
            {
                knockback = 0f;
                target.AddBuff(mod.BuffType("Shock"), 60, true);
                //Projectile.NewProjectile(target.Center.X, target.Center.Y, target.velocity.X, target.velocity.Y, mod.ProjectileType("Shock"), 20, 0, Main.myPlayer, 0f, 0f);
            }

            if (GoldEnchant)
            {
                target.AddBuff(BuffID.Midas, 120, true);
            }
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (Hexed)
            {
                target.life += damage;
                target.HealEffect(damage);

                if (target.life > target.lifeMax)
                {
                    target.life = target.lifeMax;
                }

                damage = 0;
                knockback = 0;
                crit = false;

                return;

            }

            if (SqueakyToy)
            {
                damage = 1;
                Squeak(target.Center);
                return;
            }

            if (VoidSoul && Main.rand.Next(25) == 0 && target.type != NPCID.TargetDummy)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y - 10, 0f, 0f, 518, 0, 0f, Main.myPlayer);
            }

            if (TerrariaSoul)
            {
                target.AddBuff(BuffID.Venom, 240, true);
            }

            if (QueenStinger)
            {
                target.AddBuff(BuffID.Poisoned, 120, true);
            }

            if (SpiderEnchant && item.summon)
            {
                target.AddBuff(mod.BuffType("Swarmed"), 600);
            }

            if (MeleeEffect)
            {
                if (item.melee)
                {
                    // target.AddBuff(BuffID.CursedInferno, 240, true);
                    target.AddBuff(mod.BuffType("SuperBleed"), 240, true);

                }
            }

            if (RangedEffect)
            {
                if (item.ranged)
                {
                    target.AddBuff(BuffID.ShadowFlame, 240, true);
                }

            }

            if (UniverseEffect)
            {
                target.AddBuff(mod.BuffType("FlamesoftheUniverse"), 240, true);
            }

            if (ShadowEnchant && Main.rand.Next(4) == 0)
            {
                target.AddBuff(BuffID.ShadowFlame, 120, true);
            }

            if (GoldEnchant)
            {
                target.AddBuff(BuffID.Midas, 120, true);
            }
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
        {
            if (!SqueakyToy) return;
            damage = 1;
            Squeak(target.Center);
        }

        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
        {
            if (!SqueakyToy) return;
            damage = 1;
            Squeak(target.Center);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Infinity && (player.HeldItem.ranged || player.HeldItem.magic || player.HeldItem.thrown))
            {
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " self destructed."), player.HeldItem.damage / 33, 0);
                player.immune = false;
            }

            //no heal on dummy!!!
            if (target.type == NPCID.TargetDummy)
            {
                return;
            }

            if(JungleEnchant && Main.rand.Next(4) == 0)
            {
                player.ManaEffect(5);
                player.statMana += 5;
            }

            if (SpectreEnchant && proj.magic)
            {
                if (crit)
                {
                    SpecHeal = true;
                    HealTown++;
                }
                else
                {
                    if (HealTown != 0 && HealTown <= 10)
                    {
                        HealTown++;
                    }
                    else
                    {
                        SpecHeal = false;
                        HealTown = 0;
                    }
                }
            }

            if (VortexEnchant && proj.ranged)
            {
                if (crit && VortexCrit < 100)
                {
                    VortexCrit += 4;
                }
            }

            if (TerrariaSoul)
            {
                if (crit && VortexCrit < 100)
                {
                    VortexCrit += 4;
                }
                else if (VortexCrit >= 100 && proj.type != ProjectileID.CrystalLeafShot)
                {
                    if (HealTimer == 0)
                    {
                        player.statLife += damage / 25;
                        player.HealEffect(damage / 25);
                        HealTimer = 10;
                    }
                    else
                    {
                        HealTimer--;
                    }
                }
            }

            if (PalladEnchant && Main.rand.Next(40) == 0)
            {
                player.statLife += damage / 3;
                player.HealEffect(damage / 3);
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {

            if (PalladEnchant && Main.rand.Next(40) == 0)
            {
                player.statLife += damage / 3;
                player.HealEffect(damage / 3);
            }
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (!QueenStinger) return true;
            return proj.type != ProjectileID.Stinger;
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (!FargoWorld.MasochistMode) return !VoidSoul || Main.rand.Next(10) != 0;
            
            //no work
            //lava
            
            if (damageSource == PlayerDeathReason.ByOther(2))
            {
                player.Hurt(PlayerDeathReason.ByOther(2), 999, 1);
            }
            return !VoidSoul || Main.rand.Next(10) != 0;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (JungleEnchant)
            {
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 62);
                FargoGlobalProjectile.XWay(8, player.Center, mod.ProjectileType("SporeBoom"), 5, (int)(30 * player.magicDamage), 5f);
                FargoGlobalProjectile.XWay(8, player.Center, mod.ProjectileType("SporeBoom"), 2.5f, 0, 0f);
            }

            if (TerrariaSoul && JungleEnchant)
            {
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 62);

                FargoGlobalProjectile.XWay(8, player.Center, mod.ProjectileType("SporeBoom"), 5, (int)(120 * player.magicDamage), 5f);
                FargoGlobalProjectile.XWay(8, player.Center, mod.ProjectileType("SporeBoom"), 2.5f, 0, 0f);
            }

            if ((VortexEnchant || TerrariaSoul) && VortexCrit > 4)
            {
                VortexCrit /= 2;
            }

            if (TankEffect && Main.rand.Next(5) == 0)
            {
                player.statLife += Convert.ToInt32(damage * .5);
                player.HealEffect(Convert.ToInt32(damage * .5));
            }

            if (DimensionSoul && Main.rand.Next(3) == 0)
            {
                player.statLife += Convert.ToInt32(damage * .75);
                player.HealEffect(Convert.ToInt32(damage * .75));
            }
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            //add rando debuff thing
            //insert check for masomode here
            // int rng = Main.rand.Next(FargoDebuffs.instance.debuffIDs.Length);
            //int debuff = FargoDebuffs.instance.debuffIDs[rng];
            //player.AddBuff(debuff, 300);
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (FossilEnchant && player.FindBuffIndex(mod.BuffType("Revived")) == -1)
            {
                player.statLife = 20;
                player.HealEffect(20);
                player.immune = true;
                player.immuneTime = player.longInvince ? 180 : 120;
                Main.NewText("You've been revived!", 175, 75);
                player.AddBuff(mod.BuffType("Revived"), 18000);
                return false;
            }
            if (TerrariaSoul && player.FindBuffIndex(mod.BuffType("Revived")) == -1)
            {
                player.statLife = 200;
                player.HealEffect(200);
                player.immune = true;
                player.immuneTime = player.longInvince ? 180 : 120;
                Main.NewText("You've been revived!", 175, 75);
                player.AddBuff(mod.BuffType("Revived"), 14400);
                return false;
            }

            //add more tbh
            if (Infested && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " could not handle the infection.");
            }

            if (Rotting && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " rotted away.");
            }

            if ((GodEater || FlamesoftheUniverse) && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " was annihilated by divine wrath.");
            }

            return true;
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (Infinity)
            {
                return false;
            }

            if (UniverseEffect)
            {
                if (Main.rand.Next(100) < 50)
                {
                    return false;
                }
            }

            if (RangedEffect)
            {
                if (Main.rand.Next(100) < 32)
                {
                    return false;
                }
            }

            if (MiniRangedEffect)
            {
                if (Main.rand.Next(100) < 4)
                {
                    return false;
                }
            }

            if (ThrowSoul)
            {
                if (Main.rand.Next(100) < 32)
                {
                    return false;
                }

            }
            return true;


        }

        /*public override void SetControls ()
		{
			vortexStealth = player.vortexStealthActive;
		}*/
		
		public override void PostUpdateEquips ()
		{
            /*if(vortexEnchant)
			{
				player.setVortex = true;
				player.vortexStealthActive = vortexStealth;
			}*/

            if(BeetleEnchant)
            {
                player.wingTimeMax = (int)(player.wingTimeMax * 1.5);
            }
		}

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (TerrariaSoul && Main.rand.Next(2) == 0 && Soulcheck.GetValue("Splitting Projectiles") && !item.summon)
            {
                float spread = 2f * 0.1250f;
                float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
                double baseAngle = Math.Atan2(speedX, speedY);
                double offsetAngle;

                for (float i = -1f; i <= 1f; i++)
                {
                    offsetAngle = baseAngle + i * spread;
                    Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);
                }

                return false;
            }

            return true;
        }

        public void AddPet(string toggle, int buff, int proj)
        {
            if (player.whoAmI == Main.myPlayer && player.FindBuffIndex(buff) == -1)
            {
                if (Soulcheck.GetValue(toggle) && player.ownedProjectileCounts[proj] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, proj, 0, 0f, Main.myPlayer);
                }
            }      
        }

        public void AddMinion(string toggle, int proj, int damage, float knockback)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (Soulcheck.GetValue(toggle) && player.ownedProjectileCounts[proj] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, proj, damage, knockback, Main.myPlayer);
                }
            }
        }

        private void KillPets()
        {
            int petId = player.miscEquips[0].buffType;
            int lightPetId = player.miscEquips[1].buffType;

            player.buffImmune[petId] = true;
            player.buffImmune[lightPetId] = true;

            player.ClearBuff(petId);
            player.ClearBuff(lightPetId);

            CrimsonPet = false;
            CrimsonPet2 = false;
            CubePet = false;
            DinoPet = false;
            DogPet = false;
            DragPet = false;
            EyePet = false;
            FishPet = false;
            GrinchPet = false;
            LizPet = false;
            PenguinPet = false;
            SaplingPet = false;
            SeedPet = false;
            ShadowPet = false;
            ShadowPet2 = false;
            SkullPet = false;
            SnowmanPet = false;
            SuspiciousEyePet = false;

            //memorizes player selections
            if (!WasAsocial)
            {
                HidePetToggle0 = player.hideMisc[0];
                HidePetToggle1 = player.hideMisc[1];

                WasAsocial = true;
            }

            //disables pet and light pet too!
            if (!player.hideMisc[0])
            {
                player.TogglePet();
            }

            if (!player.hideMisc[1])
            {
                player.ToggleLight();
            }

            player.hideMisc[0] = true;
            player.hideMisc[1] = true;
        }

        private void Squeak(Vector2 center)
        {
            int rng = Main.rand.Next(6);

            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/SqueakyToy/squeak" + (rng + 1)).WithVolume(1f).WithPitchVariance(.5f), center);

        }

        private int InfestedExtraDot()
        {
            int buffIndex = player.FindBuffIndex(mod.BuffType("Infested"));

            if (buffIndex == -1)
                return 0;

            int timeLeft = player.buffTime[buffIndex];

            float baseVal = (float)(MaxInfestTime - timeLeft) / 120;
            //change the denominator to adjust max power of DOT
            int modifier = (int)(baseVal * baseVal + 8);

            InfestedDust = baseVal / 10 + 1f;

            if (InfestedDust > 5f)
            {
                InfestedDust = 5f;
            }

            return modifier;
        }

        public void AllDamageUp(float dmg)
        {
            player.magicDamage += dmg;
            player.meleeDamage += dmg;
            player.rangedDamage += dmg;
            player.minionDamage += dmg;
            player.thrownDamage += dmg;

            if(Fargowiltas.Instance.ThoriumLoaded)
            {
                ///
            }
        }

        public void AllCritUp(int crit)
        {
            player.meleeCrit += crit;
            player.rangedCrit += crit;
            player.magicCrit += crit;
            player.thrownCrit += crit;

            //thorium meme
        }

    }
}
