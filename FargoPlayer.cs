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
        private int shadowCD = 0;
        private int shadowDeathCD = 0;
        public bool CrimsonEnchant;
        public bool SpectreEnchant;
        public bool SpecHeal;
        public int HealTown;
        public bool BeeEnchant;
        public bool SpiderEnchant;
        public bool StardustEnchant;
        public bool FreezeTime = false;
        private int freezeLength = 600;
        public int FreezeCD = 0;
        public bool MythrilEnchant;
        public bool FossilEnchant;
        public bool FossilBones = false;
        public bool JungleEnchant;
        public bool ElementEnchant;
        public bool ShroomEnchant;
        public bool CobaltEnchant;
        public bool SpookyEnchant;
        public bool HallowEnchant;
        public bool ChloroEnchant;
        public bool VortexEnchant;
        public bool VortexStealth = false;
        public int VortexDust = 0;
        public bool AdamantiteEnchant;
        public bool FrostEnchant;
        public int IcicleCount = 0;
        public bool PalladEnchant;
        public bool OriEnchant;
        public bool MeteorEnchant;
        private int meteorTimer = 150;
        private int meteorCD = 0;
        private bool meteorShower = false;
        public bool MoltenEnchant;
        public bool CopperEnchant;
        public bool NinjaEnchant;
        public bool FirstStrike;
        public bool NearSmoke;
        public bool IronEnchant;
        public bool IronGuard;
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
        public bool PlatinumEnchant;
        public bool NecroEnchant;
        private int necroCD;
        public bool ObsidianEnchant;
        public bool TinEnchant;
        public int TinCrit = 4;
        public bool TikiEnchant;
        public bool SolarEnchant;
        public bool NebulaEnchant;
        public int NebulaCounter = 0;
        public bool ShinobiEnchant;
        public bool ValhallaEnchant;
        public bool DarkEnchant;
        private int darkCD = 0;
        Item prevWeapon = new Item();
        Vector2 prevPosition;
        public bool RedEnchant;

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

        private int[] wetProj = { ProjectileID.Kraken, ProjectileID.Trident, ProjectileID.Flairon, ProjectileID.FlaironBubble, ProjectileID.WaterStream, ProjectileID.WaterBolt, ProjectileID.RainNimbus, ProjectileID.Bubble, ProjectileID.WaterGun };

        private int[] tikiDebuffs = { 0, 0, BuffID.OnFire, BuffID.CursedInferno, BuffID.Frostburn, BuffID.Ichor, BuffID.ShadowFlame, BuffID.Venom, BuffID.Poisoned, BuffID.Confused, BuffID.Stinky };

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
            IronGuard = false;
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
            PlatinumEnchant = false;
            NecroEnchant = false;
            ObsidianEnchant = false;
            TinEnchant = false;
            TikiEnchant = false;
            SolarEnchant = false;
            NebulaEnchant = false;
            ShinobiEnchant = false;
            ValhallaEnchant = false;
            DarkEnchant = false;
            RedEnchant = false;

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

            tikiDebuffs[0] = mod.BuffType("Rotting");
            tikiDebuffs[1] = mod.BuffType("SqueakyToy");
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
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

            if (NecroEnchant && necroCD != 0)
            {
                necroCD--;
            }

            if(ShadowEnchant)
            {
                if (shadowDeathCD != 0)
                {
                    
                    for (int i = 0; i < 200; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && Main.rand.Next(4) == 0 && Vector2.Distance(npc.Center, player.Center) < 1000 && !npc.townNPC )
                        {
                            
                            npc.StrikeNPC(Main.rand.Next(5, 10), 0, 0);
                        }
                    }
                    shadowDeathCD--;
                }

                if(shadowCD != 0)
                {
                    shadowCD--;
                }
            }

            if (VortexDust > 0)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Vector2 vector = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                    Dust dust = Main.dust[Dust.NewDust(player.Center - vector * 30f, 0, 0, 229, 0f, 0f, 0, default(Color), 1f)];
                    dust.noGravity = true;
                    dust.position = player.Center - vector * (float)Main.rand.Next(5, 11);
                    dust.velocity = vector.RotatedBy(1.5707963705062866, default(Vector2)) * 4f;
                    dust.scale = 0.5f + Main.rand.NextFloat();
                    dust.fadeIn = 0.5f;
                }
                if (Main.rand.Next(2) == 0)
                {
                    Vector2 vector2 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                    Dust dust2 = Main.dust[Dust.NewDust(player.Center - vector2 * 30f, 0, 0, 240, 0f, 0f, 0, default(Color), 1f)];
                    dust2.noGravity = true;
                    dust2.position = player.Center - vector2 * 12f;
                    dust2.velocity = vector2.RotatedBy(-1.5707963705062866, default(Vector2)) * 2f;
                    dust2.scale = 0.5f + Main.rand.NextFloat();
                    dust2.fadeIn = 0.5f;
                }

                VortexDust--;
            }

            if(VortexStealth && !VortexEnchant)
            {
                VortexStealth = false;
            }

            if (NinjaEnchant)
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
                        meteorCD = 300;
                        meteorTimer = 150;
                        meteorShower = false;
                    }
                }
                else
                {
                    if(player.HeldItem.magic && player.controlUseItem)
                    {
                        meteorCD--;

                        if(meteorCD == 0)
                        {
                            meteorShower = true;
                        }
                    }
                    else
                    {
                        meteorCD = 300;
                    }
                }  
            }

            if(StardustEnchant)
            {
                if(FreezeTime && freezeLength != 0)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        if (Main.npc[i].active)
                        {
                            Main.npc[i].GetGlobalNPC<FargoGlobalNPC>().TimeFrozen = true;
                        }
                    }

                    for (int i = 0; i < 1000; i++)
                    {
                        if (Main.projectile[i].active)
                        {
                            Main.projectile[i].GetGlobalProjectile<FargoGlobalProjectile>().TimeFrozen = true;
                        }
                    }

                    freezeLength--;

                    if(freezeLength == 0)
                    {
                        FreezeTime = false;

                        freezeLength = 600;

                        for (int i = 0; i < 200; i++)
                        {
                            NPC npc = Main.npc[i];
                            if (npc.active && npc.GetGlobalNPC<FargoGlobalNPC>().TimeFrozen)
                            {
                                npc.GetGlobalNPC<FargoGlobalNPC>().TimeFrozen = false;

                                if(npc.life == 1)
                                {
                                    npc.StrikeNPC(9999, 0f, 0);
                                }
                            }
                        }

                        for (int i = 0; i < 1000; i++)
                        {
                            Projectile proj = Main.projectile[i];
                            if (proj.active && proj.GetGlobalProjectile<FargoGlobalProjectile>().TimeFrozen)
                            {
                                proj.GetGlobalProjectile<FargoGlobalProjectile>().TimeFrozen = false;
                            }
                        }
                    }
                }

                if(!FreezeTime && FreezeCD != 0)
                {
                    FreezeCD--;

                    if(FreezeCD == 0)
                    {
                        Main.PlaySound(SoundID.MaxMana, player.Center);
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
            if (ShinobiEnchant && player.dashDelay > 0 && player.velocity.X == 0)
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

            if(DarkEnchant)
            {
                Item heldItem = player.HeldItem;
                Projectile proj = new Projectile();
                proj.SetDefaults(heldItem.shoot);

                if(darkCD == 0 && heldItem.magic && !heldItem.summon && heldItem.shoot > 0 && heldItem.damage > 0 && !heldItem.channel && proj.aiStyle != 19 && player.controlUseItem && Vector2.Distance(prevPosition, player.position) > 25)
                {
                    if (prevPosition != null)
                    {
                        Vector2 vel = (Main.MouseWorld - prevPosition).SafeNormalize(-Vector2.UnitY);

                        if (prevWeapon.useAmmo > 0)
                        {
                            bool canShoot = true;
                            player.PickAmmo(heldItem, ref heldItem.shoot, ref heldItem.shootSpeed, ref canShoot, ref heldItem.damage, ref heldItem.knockBack);
                        }

                        Projectile p = Projectile.NewProjectileDirect(prevPosition, vel * prevWeapon.shootSpeed, prevWeapon.shoot, prevWeapon.damage / 2, prevWeapon.knockBack, player.whoAmI);

                        for (int i = 0; i < 5; i++)
                        {
                            int dustId = Dust.NewDust(new Vector2(prevPosition.X, prevPosition.Y + 2f), player.width, player.height + 5, DustID.Shadowflame, 0, 0, 100, Color.Black, 2f);
                            Main.dust[dustId].noGravity = true;
                        }
                    }

                    prevPosition = player.position;
                    prevWeapon = player.HeldItem;
                    darkCD = 60;
                }

                darkCD--;

                if(darkCD < 0)
                {
                    darkCD = 0;
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
            int useTime = item.useTime;
            int useAnimate = item.useAnimation;

            
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

            if(ObsidianEnchant)
            {
                multiplier *= 1.1f;
            }

            if(NebulaEnchant && NebulaCounter < 0 && player.HeldItem.magic)
            {
                multiplier *= 5;
            }

            //checks so weapons dont break
            while (useTime / multiplier < 1)
            {
                multiplier -= .1f;
            }

            while (useAnimate / multiplier < 2)
            {
                multiplier -= .1f;
            }

            return multiplier;
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

            if (LeadEnchant && Main.rand.Next(5) == 0)
            {
                target.AddBuff(mod.BuffType("LeadPoison"), 120);
            }

            if(TikiEnchant && Main.rand.Next(3) == 0)
            {
                target.AddBuff(tikiDebuffs[Main.rand.Next(tikiDebuffs.Length)], 300);
            }

            if (SolarEnchant && Main.rand.Next(4) == 0)
            {
                target.AddBuff(mod.BuffType("SolarFlare"), 120);
            }

            //full moon
            if(RedEnchant && proj.ranged && Main.moonPhase == 0)
            {
                target.AddBuff(mod.BuffType("SuperBleed"), 240, true);
            }

            if(VortexEnchant && proj.type != mod.ProjectileType("Void") && Main.rand.Next(10) == 0)
            {
                Projectile.NewProjectile(proj.Center.X, proj.Center.Y, 0f, 0f, mod.ProjectileType("Void"), 60, 5f, proj.owner);
            }

            if (Array.IndexOf(wetProj, proj.type) > -1)
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

            if(ObsidianEnchant)
            {
                target.AddBuff(BuffID.OnFire, 600);
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

            if (LeadEnchant && Main.rand.Next(5) == 0)
            {
                target.AddBuff(mod.BuffType("LeadPoison"), 180);
            }

            if (SolarEnchant && Main.rand.Next(4) == 0)
            {
                target.AddBuff(mod.BuffType("SolarFlare"), 300);
            }

            if (TikiEnchant && Main.rand.Next(3) == 0)
            {
                target.AddBuff(tikiDebuffs[Main.rand.Next(tikiDebuffs.Length)], 300);
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

            if (ObsidianEnchant)
            {
                target.AddBuff(BuffID.OnFire, 600);
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
            if (CopperEnchant && proj.type != ProjectileID.CultistBossLightningOrbArc && Array.IndexOf(wetProj, proj.type) == -1)
            {
                int dmg = 5;
                int chance = 20;

                if (target.FindBuffIndex(BuffID.Wet) != -1)
                {
                    dmg *= 2;
                    chance /= 4;
                }

                if (Main.rand.Next(chance) == 0)
                {
                    float closestDist;
                    NPC closestNPC;

                    for(int i = 0; i < 5; i++)
                    {
                        closestDist = 500f;
                        closestNPC = null;

                        for (int j = 0; j < 200; j++)
                        {
                            NPC npc = Main.npc[j];
                            if (npc.active && npc != target && !npc.GetGlobalNPC<FargoGlobalNPC>().Shock && npc.Distance(target.Center) < closestDist && npc.Distance(target.Center) >= 100)
                            {
                                closestNPC = npc;
                                closestDist = npc.Distance(target.Center);
                            }
                        }

                        if (closestNPC != null)
                        {
                            Vector2 ai = closestNPC.Center - target.Center;
                            float ai2 = Main.rand.Next(100);
                            Vector2 velocity = Vector2.Normalize(ai) * 20;

                            Projectile p = Projectile.NewProjectileDirect(target.Center, velocity, ProjectileID.CultistBossLightningOrbArc, (int)(dmg * player.magicDamage), 0f, player.whoAmI, ai.ToRotation(), ai2);
                            p.friendly = true;
                            p.hostile = false;
                            p.penetrate = -1;
                            p.timeLeft = 90;

                            target.AddBuff(mod.BuffType("Shock"), 60);
                        }
                        else
                        {
                            break;
                        }

                        target = closestNPC;
                    }

                }
            }

            if(ShadowEnchant)
            {
                ShadowEffect();
            }

            if (Infinity && (player.HeldItem.ranged || player.HeldItem.magic || player.HeldItem.thrown))
            {
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " self destructed."), player.HeldItem.damage / 33, 0);
                player.immune = false;
            }

            if (NecroEnchant && proj.type != mod.ProjectileType("DungeonGuardian") && necroCD == 0)
            {
                //necroTimer = 1200;
                necroCD = 60;
                float screenX = Main.screenPosition.X;
                if (player.direction < 0)
                {
                    screenX += Main.screenWidth;
                }
                float screenY = Main.screenPosition.Y;
                screenY += Main.rand.Next(Main.screenHeight);
                Vector2 vector = new Vector2(screenX, screenY);
                float velocityX = target.Center.X - vector.X;
                float velocityY = target.Center.Y - vector.Y;
                velocityX += Main.rand.Next(-50, 51) * 0.1f;
                velocityY += Main.rand.Next(-50, 51) * 0.1f;
                int num5 = 24;
                float num6 = (float)Math.Sqrt(velocityX * velocityX + velocityY * velocityY);
                num6 = num5 / num6;
                velocityX *= num6;
                velocityY *= num6;
                Projectile p = Projectile.NewProjectileDirect(new Vector2(screenX, screenY), new Vector2(velocityX, velocityY), mod.ProjectileType("DungeonGuardian"), 500, 0f, player.whoAmI, 0, 120);
                p.penetrate = 1;
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

            if (TinEnchant)
            {
                if (crit && TinCrit < 100)
                {
                    TinCrit += 4;
                }
            }

            /*if (TerrariaSoul)
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
            }*/

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

            if (ShadowEnchant)
            {
                ShadowEffect();
            }
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (!QueenStinger) return true;
            return proj.type != ProjectileID.Stinger;
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            //no work
            //lava
            
            if (damageSource == PlayerDeathReason.ByOther(2))
            {
                player.Hurt(PlayerDeathReason.ByOther(2), 999, 1);
            }

            return true;
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

            if ((TinEnchant || TerrariaSoul) && TinCrit != 4)
            {
                TinCrit = 4;
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
            bool retVal = true;

            if (MoltenEnchant)
            {
                Projectile.NewProjectileDirect(player.Center, Vector2.Zero, mod.ProjectileType("Explosion"), (int)(500 * player.meleeDamage), 0f, Main.myPlayer);
            }

            if (FossilEnchant && player.FindBuffIndex(mod.BuffType("Revived")) == -1)
            {
                player.statLife = 20;
                player.HealEffect(20);
                player.immune = true;
                player.immuneTime = player.longInvince ? 600 : 400;
                FossilBones = true;
                Main.NewText("You've been revived!", 175, 75);
                player.AddBuff(mod.BuffType("Revived"), 18000);
                retVal = false;
            }
            /*if (TerrariaSoul && player.FindBuffIndex(mod.BuffType("Revived")) == -1)
            {
                player.statLife = 200;
                player.HealEffect(200);
                player.immune = true;
                player.immuneTime = player.longInvince ? 180 : 120;
                Main.NewText("You've been revived!", 175, 75);
                player.AddBuff(mod.BuffType("Revived"), 14400);
                retVal = false;
            }*/

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

            return retVal;
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
		
		public override void PostUpdateEquips ()
		{
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

        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (IronGuard)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 10;
            }
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

        public void Squeak(Vector2 center)
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

        private void ShadowEffect()
        {
            if(shadowCD == 0)
            {
                if (player.HasBuff(BuffID.Blackout))
                {
                    player.AddBuff(BuffID.Obstructed, 150);
                    player.DelBuff(player.FindBuffIndex(BuffID.Blackout));

                    shadowDeathCD = 120;
                    shadowCD = 720;
                }
                else if (player.HasBuff(BuffID.Darkness))
                {
                    player.AddBuff(BuffID.Blackout, 300);
                    player.DelBuff(player.FindBuffIndex(BuffID.Darkness));
                    shadowCD = 120;
                }
                else
                {
                    player.AddBuff(BuffID.Darkness, 300);
                    shadowCD = 120;
                }

                
            }
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

        public void AllCritEquals(int crit)
        {
            player.meleeCrit = crit;
            player.rangedCrit = crit;
            player.magicCrit = crit;
            player.thrownCrit = crit;
        }

        public void FlowerBoots()
        {
            int x = (int)player.Center.X / 16;
            int y = (int)(player.position.Y + player.height - 1f) / 16;

            if (Main.tile[x, y] == null)
            {
                Main.tile[x, y] = new Tile();
            }

            if (!Main.tile[x, y].active() && Main.tile[x, y].liquid == 0 && Main.tile[x, y + 1] != null && WorldGen.SolidTile(x, y + 1))
            {
                Main.tile[x, y].frameY = 0;
                Main.tile[x, y].slope(0);
                Main.tile[x, y].halfBrick(false);

                if (Main.tile[x, y + 1].type == 2)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.tile[x, y].active(true);
                        Main.tile[x, y].type = 3;
                        Main.tile[x, y].frameX = (short)(18 * Main.rand.Next(6, 11));
                        while (Main.tile[x, y].frameX == 144)
                        {
                            Main.tile[x, y].frameX = (short)(18 * Main.rand.Next(6, 11));
                        }
                    }
                    else
                    {
                        Main.tile[x, y].active(true);
                        Main.tile[x, y].type = 73;
                        Main.tile[x, y].frameX = (short)(18 * Main.rand.Next(6, 21));

                        while (Main.tile[x, y].frameX == 144)
                        {
                            Main.tile[x, y].frameX = (short)(18 * Main.rand.Next(6, 21));
                        }
                    }

                    if (Main.netMode == 1)
                    {
                        NetMessage.SendTileSquare(-1, x, y, 1, TileChangeType.None);
                    }
                }
                else if (Main.tile[x, y + 1].type == 109)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.tile[x, y].active(true);
                        Main.tile[x, y].type = 110;
                        Main.tile[x, y].frameX = (short)(18 * Main.rand.Next(4, 7));

                        while (Main.tile[x, y].frameX == 90)
                        {
                            Main.tile[x, y].frameX = (short)(18 * Main.rand.Next(4, 7));
                        }
                    }
                    else
                    {
                        Main.tile[x, y].active(true);
                        Main.tile[x, y].type = 113;
                        Main.tile[x, y].frameX = (short)(18 * Main.rand.Next(2, 8));

                        while (Main.tile[x, y].frameX == 90)
                        {
                            Main.tile[x, y].frameX = (short)(18 * Main.rand.Next(2, 8));
                        }
                    }
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendTileSquare(-1, x, y, 1, TileChangeType.None);
                    }
                }
                else if (Main.tile[x, y + 1].type == 60)
                {
                    Main.tile[x, y].active(true);
                    Main.tile[x, y].type = 74;
                    Main.tile[x, y].frameX = (short)(18 * Main.rand.Next(9, 17));

                    if (Main.netMode == 1)
                    {
                        NetMessage.SendTileSquare(-1, x, y, 1, TileChangeType.None);
                    }
                }
            }
        }
    }
}
