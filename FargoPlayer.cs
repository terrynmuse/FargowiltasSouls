using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Graphics.Capture;
using FargowiltasSouls.Buffs.Masomode;
using FargowiltasSouls.Buffs.Souls;
using FargowiltasSouls.Items;
using FargowiltasSouls.NPCs;
using FargowiltasSouls.Projectiles;
using FargowiltasSouls.Projectiles.BossWeapons;
using FargowiltasSouls.Projectiles.Souls;
using ThoriumMod;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace FargowiltasSouls
{
    public class FargoPlayer : ModPlayer
    {
        //for convenience
        public bool IsStandingStill;
        public float AttackSpeed;

        public bool Wood;
        public bool QueenStinger;
        public bool Infinity;

        //minions
        public bool BrainMinion;
        public bool EaterMinion;

        #region enchantments
        public bool PetsActive = true;
        public bool ShadowEnchant;
        public bool CrimsonEnchant;
        public bool SpectreEnchant;
        public bool SpecHeal;
        public int HealTown;
        public bool BeeEnchant;
        public bool SpiderEnchant;
        public bool StardustEnchant;
        public bool FreezeTime = false;
        private int freezeLength = 300;
        public int FreezeCD = 0;
        public bool MythrilEnchant;
        public bool FossilEnchant;
        public bool FossilBones = false;
        private int boneCD = 0;
        public bool JungleEnchant;
        public bool ElementEnchant;
        public bool ShroomEnchant;
        public bool CobaltEnchant;
        public bool SpookyEnchant;
        public bool HallowEnchant;
        public bool ChloroEnchant;
        public bool VortexEnchant;
        public bool VortexStealth = false;
        private int vortexCD = 0;
        public bool AdamantiteEnchant;
        public bool FrostEnchant;
        public int IcicleCount = 0;
        private int icicleCD = 0;
        private Projectile[] icicles = new Projectile[3];
        public bool PalladEnchant;
        private int palladiumCD = 0;
        public bool OriEnchant;
        public bool OriSpawn = false;
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
        public bool ShellHide;
        public bool LeadEnchant;
        public bool GladEnchant;
        public bool GoldEnchant;
        public bool CactusEnchant;
        public bool BeetleEnchant;
        public bool ForbiddenEnchant;
        public bool MinerEnchant;
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
        public int NebulaCounter;
        public bool ShinobiEnchant;
        public bool ValhallaEnchant;
        public bool DarkEnchant;
        private int darkCD = 0;
        Item prevWeapon = new Item();
        Vector2 prevPosition;
        public bool RedEnchant;

        public bool CosmoForce;
        public bool EarthForce;
        public bool LifeForce;
        public bool NatureForce;
        public bool SpiritForce;
        public bool ShadowForce;
        public bool TerraForce;
        public bool WillForce;

        //thorium 
        public bool IcyEnchant;
        public bool WarlockEnchant;
        public bool SacredEnchant;
        public bool BinderEnchant;
        public bool FlightEnchant;
        public bool LivingWoodEnchant;
        public bool DepthEnchant;
        public bool KnightEnchant;
        public bool DreamEnchant;
        public bool IllumiteEnchant;

        private int[] wetProj = { ProjectileID.Kraken, ProjectileID.Trident, ProjectileID.Flairon, ProjectileID.FlaironBubble, ProjectileID.WaterStream, ProjectileID.WaterBolt, ProjectileID.RainNimbus, ProjectileID.Bubble, ProjectileID.WaterGun };

        #endregion

        //soul effects
        public bool MagicSoul;

        public bool ThrowSoul;
        public bool RangedSoul;
        public bool RangedEssence;
        public bool BuilderMode;
        public bool UniverseEffect;
        public bool UniverseStoredAutofire;
        public bool FishSoul1;
        public bool FishSoul2;
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
        public int StatLifePrevious = -1;   //used for mutantNibble
        public bool Asocial;                //disables minions, disables pets
        public bool WasAsocial;
        public bool HidePetToggle0 = true;
        public bool HidePetToggle1 = true;
        public bool Kneecapped;             //disables running :v
        public bool Defenseless;            //-30 defense, no damage reduction, cross necklace and knockback prevention effects disabled
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

        public IList<string> disabledSouls = new List<string>();

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override TagCompound Save()
        {
            if (Soulcheck.owner == player.name) //to prevent newly made characters from taking the toggles of another char
            {
                string name = "FargoDisabledSouls" + player.name;
                //string log = name + " saved: ";

                var FargoDisabledSouls = new List<string>();
                foreach (KeyValuePair<string, bool> entry in Soulcheck.ToggleDict)
                {
                    if (!entry.Value)
                    {
                        FargoDisabledSouls.Add(entry.Key);
                        //log += entry.Key + ". ";
                    }
                }

                //ErrorLogger.Log(log);

                return new TagCompound {
                {name, FargoDisabledSouls}
                }; ;
            }

            return null;
        }

        public override void Load(TagCompound tag)
        {
            string name = "FargoDisabledSouls" + player.name;
            //string log = name + " loaded: ";

            disabledSouls = tag.GetList<string>(name);

            //var FargoDisabledSouls = tag.GetList<string>(name);
            //foreach (string disabledSoul in FargoDisabledSouls)
            //{
            //    disabledSouls.Add(disabledSoul);

            //    log += disabledSoul + ". ";
            //}

            //ErrorLogger.Log(log);
        }

        public override void OnEnterWorld(Player player)
        {
            foreach (KeyValuePair<string, Color> buff in Soulcheck.toggles)
            {
                if (Soulcheck.ToggleDict.ContainsKey(buff.Key))
                {
                    if (disabledSouls.Contains(buff.Key))
                    {
                        Soulcheck.ToggleDict[buff.Key] = false;
                        Soulcheck.checkboxDict[buff.Key].Color = Color.Gray;
                        //Main.NewText(buff.Key);
                    }
                    else
                    {
                        Soulcheck.ToggleDict[buff.Key] = true;
                        Soulcheck.checkboxDict[buff.Key].Color = new Color(81, 181, 113);
                    }
                }
            }

            Soulcheck.owner = player.name;

            disabledSouls.Clear();
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

            if(Fargowiltas.FreezeKey.JustPressed && StardustEnchant && FreezeCD == 0)
            {
                FreezeTime = true;
                FreezeCD = 360;//3600; bring back for after testing

                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/ZaWarudo").WithVolume(1f).WithPitchVariance(.5f), player.Center);
            }
        }
        public override void ResetEffects()
        {
            AttackSpeed = 1f;

            Wood = false;

            QueenStinger = false;
            Infinity = false;

            BrainMinion = false;
            EaterMinion = false;

            #region enchantments 
            PetsActive = true;
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
            ShellHide = false;
            LeadEnchant = false;
            GladEnchant = false;
            GoldEnchant = false;
            CactusEnchant = false;
            BeetleEnchant = false;
            ForbiddenEnchant = false;
            MinerEnchant = false;
            PumpkinEnchant = false;
            SilverEnchant = false;
            PlatinumEnchant = false;
            NecroEnchant = false;
            ObsidianEnchant = false;
            TinEnchant = false;
            TikiEnchant = false;
            SolarEnchant = false;
            ShinobiEnchant = false;
            ValhallaEnchant = false;
            DarkEnchant = false;
            RedEnchant = false;
            NebulaEnchant = false;

            CosmoForce = false;
            EarthForce = false;
            LifeForce = false;
            NatureForce = false;
            SpiritForce = false;
            TerraForce = false;
            ShadowForce = false;
            WillForce = false;

            //thorium
            IcyEnchant = false;
            WarlockEnchant = false;
            SacredEnchant = false;
            BinderEnchant = false;
            FlightEnchant = false;
            LivingWoodEnchant = false;
            DepthEnchant = false;
            KnightEnchant = false;
            DreamEnchant = false;
            IllumiteEnchant = false;

            #endregion

            //add missing pets to kill pets!

            //souls
            MagicSoul = false;

            ThrowSoul = false;
            RangedSoul = false;
            RangedEssence = false;
            BuilderMode = false;
            UniverseEffect = false;
            FishSoul1 = false;
            FishSoul2 = false;
            TerrariaSoul = false;
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
            //remove this after testing you fool
            //player.respawnTimer = (int)(player.respawnTimer * .01);
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
            
            player.npcTypeNoAggro[0] = true;

            if (FargoWorld.MasochistMode)
            {
                if (Main.bloodMoon)
                {
                    player.buffImmune[BuffID.Bleeding] = false;
                    player.AddBuff(BuffID.Bleeding, 2);
                }

                //falling gives you dazed and confused even with protection. wings save you
                if (player.velocity.Y == 0f)
                {
                    //bool wings = false;
                    //for (int n = 3; n < 10; n++)
                    //{
                    //    if (player.armor[n].stack > 0 && player.armor[n].wingSlot > -1)
                    //    {
                    //        wings = true;
                    //        break;
                    //    }
                    //}

                    if(player.wings == 0)
                    {
                        int num21 = 25;
                        num21 += player.extraFall;
                        int num22 = (int)(player.position.Y / 16f) - player.fallStart;
                        if (player.mount.CanFly)
                        {
                            num22 = 0;
                        }
                        if (player.mount.Cart && Minecart.OnTrack(player.position, player.width, player.height))
                        {
                            num22 = 0;
                        }
                        if (player.mount.Type == 1)
                        {
                            num22 = 0;
                        }
                        player.mount.FatigueRecovery();

                        if (((player.gravDir == 1f && num22 > num21) || (player.gravDir == -1f && num22 < -num21)))
                        {
                            player.immune = false;
                            int dmg = (int)(num22 * player.gravDir - num21) * 10;
                            if (player.mount.Active)
                            {
                                dmg = (int)(dmg * player.mount.FallDamage);
                            }
                            player.AddBuff(BuffID.Dazed, dmg);
                            player.AddBuff(BuffID.Confused, dmg);
                        }
                        player.fallStart = (int)(player.position.Y / 16f);
                    } 
                }

                if (player.ZoneUnderworldHeight && !player.fireWalk)
                {
                    player.AddBuff(BuffID.OnFire, 2);
                }

                if (player.wet && !(player.accFlipper || player.gills))
                {
                    player.AddBuff(mod.BuffType<Lethargic>(), 2);
                }

                int tileX = (int)Main.player[player.whoAmI].position.X / 16;
                int tileY = (int)Main.player[player.whoAmI].position.Y / 16;
                Tile currentTile = Framing.GetTileSafely(tileX, tileY);
                if (currentTile.wall == WallID.SpiderUnsafe && player.stickyBreak > 0)
                {
                    player.AddBuff(BuffID.Webbed, 30);
                    //player.stickyBreak = 1000;

                    Vector2 vector = Collision.StickyTiles(player.position, player.velocity, player.width, player.height);
                    int num3 = (int)vector.X;
                    int num4 = (int)vector.Y;
                    WorldGen.KillTile(num3, num4, false, false, false);
                    if (Main.netMode == 1 && !Main.tile[num3, num4].active() && Main.netMode == 1)
                    {
                        NetMessage.SendData(17, -1, -1, null, 0, (float)num3, (float)num4, 0f, 0, 0, 0);
                    }
                }
            }

            if (!Infested && !FirstInfection)
            {
                FirstInfection = true;
            }

            if(TerrariaSoul && TinCrit < 25)
            {
                TinCrit = 25;
            }
            else if (TerraForce && TinCrit < 10)
            {
                TinCrit = 10;
            }

            if(OriSpawn && !OriEnchant)
            {
                OriSpawn = false;
            }

            if (VortexStealth && !VortexEnchant)
            {
                VortexStealth = false;
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
                player.moveSpeed *= .7f;
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

            if(UniverseEffect)
            {
                player.statManaMax2 += 300;
            }
        }

        public override void SetupStartInventory(IList<Item> items)
        {
            Item item = new Item();
            item.SetDefaults(mod.ItemType<Masochist>());
            items.Add(item);
        }

        public override float UseTimeMultiplier(Item item)
        {
            int useTime = item.useTime;
            int useAnimate = item.useAnimation;

            if (useTime == 0 || useAnimate == 0)
            {
                return 1f;
            }

            if (RangedEssence && item.ranged)
            {
                AttackSpeed *= 1.05f;
            }

            if (RangedSoul && item.ranged)
            {
                AttackSpeed *= 1.2f;
            }

            if (MagicSoul && item.magic)
            {
                AttackSpeed *= 1.2f;
            }

            if (ThrowSoul && item.thrown)
            {
                AttackSpeed *= 1.2f;
            }

            //works for 5 sec, 15 sec CD
            if (NebulaEnchant && NebulaCounter >= 900)
            {
                if (CosmoForce || player.HeldItem.magic)
                {
                    AttackSpeed *= 2f;
                }
            }

            //checks so weapons dont break
            while (useTime / AttackSpeed < 1)
            {
                AttackSpeed -= .1f;
            }

            while (useAnimate / AttackSpeed < 3)
            {
                AttackSpeed -= .1f;
            }

            return AttackSpeed;
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
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, 44, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), InfestedDust);
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
                target.AddBuff(mod.BuffType<LeadPoison>(), 120);
            }

            if (CosmoForce && !TerrariaSoul && Main.rand.Next(4) == 0)
            {
                target.AddBuff(mod.BuffType<SolarFlare>(), 300);
            }

            //full moon
            if (RedEnchant && !TerrariaSoul && Soulcheck.GetValue("Red Riding Super Bleed") && Main.rand.Next(5) == 0 && ((Main.moonPhase == 0) || (WillForce)))
            {
                target.AddBuff(mod.BuffType<SuperBleed>(), 240, true);
            }

            if (ShadowEnchant && !TerrariaSoul && Main.rand.Next(15) == 0)
            {
                target.AddBuff(BuffID.Darkness, 600, true);
            }

            if (TikiEnchant && !TerrariaSoul)
            {
                target.AddBuff(mod.BuffType("Infested"), 1800, true);
            }

            if (Array.IndexOf(wetProj, proj.type) > -1)
            {
                target.AddBuff(BuffID.Wet, 180, true);
            }

            if (QueenStinger)
            {
                target.AddBuff(BuffID.Poisoned, 120, true);
            }

            if (ObsidianEnchant)
            {
                target.AddBuff(BuffID.OnFire, 600);
            }

            if (UniverseEffect && !target.townNPC)
            {
                target.AddBuff(mod.BuffType<Buffs.Masomode.FlamesoftheUniverse>(), 240, true);
            }

            if (GoldEnchant)
            {
                target.AddBuff(BuffID.Midas, 120, true);
            }

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            if (LifeForce)
            {
                //yew wood for all dmg type
                if (!crit)
                {
                    thoriumPlayer.yewChargeTimer = 120;
                    if (player.ownedProjectileCounts[thorium.ProjectileType("YewVisual")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("YewVisual"), 0, 0f, player.whoAmI, 0f, 0f);
                    }
                    if (thoriumPlayer.yewCharge < 4)
                    {
                        thoriumPlayer.yewCharge++;
                    }
                    else
                    {
                        crit = true;
                        damage = (int)(damage * 0.75);
                        thoriumPlayer.yewCharge = 0;
                    }
                }
            }

            if(ShadowForce)
            {
                player.AddBuff(thorium.BuffType("ShadowDance"), 90000, false);
            }

            if(ShroomEnchant)
            {
                target.AddBuff(thorium.BuffType("Mycelium"), 120, true);
            }

            if(NatureForce)
            {
                if (proj.type != thorium.ProjectileType("CryoDamage"))
                {
                    Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("ReactionNitrogen"), 0, 5f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("CryoDamage"), proj.damage / 3, 5f, Main.myPlayer, 0f, 0f);
                }
            }

            if(CosmoForce)
            {
                //white dwarf
                if (crit)
                {
                    Main.PlaySound(2, (int)target.position.X, (int)target.position.Y, 92, 1f, 0f);
                    Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("WhiteFlare"), (int)(target.lifeMax * 0.005f), 0f, Main.myPlayer, 0f, 0f);
                }
                //tide turner
                if (player.ownedProjectileCounts[thorium.ProjectileType("TideDagger")] < 24 && proj.type != thorium.ProjectileType("ThrowingGuideFollowup") && proj.type != thorium.ProjectileType("TideDagger") && target.type != 488 && Main.rand.Next(5) == 0)
                {
                    //diver meme code, I could simplify but meh
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 43, 1f, 0f);
                    Projectile.NewProjectile(((int)player.Center.X), ((int)player.Center.Y), 3f, 0f, thorium.ProjectileType("TideDagger"), (int)(proj.damage * 0.75), 3f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(((int)player.Center.X), ((int)player.Center.Y), -3f, 0f, thorium.ProjectileType("TideDagger"), (int)(proj.damage * 0.75), 3f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(((int)player.Center.X), ((int)player.Center.Y), -1.5f, -2.15f, thorium.ProjectileType("TideDagger"), (int)(proj.damage * 0.75), 3f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(((int)player.Center.X), ((int)player.Center.Y), 1.5f, -2.15f, thorium.ProjectileType("TideDagger"), (int)(proj.damage * 0.75), 3f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(((int)player.Center.X), ((int)player.Center.Y), -1.5f, 2.15f, thorium.ProjectileType("TideDagger"), (int)(proj.damage * 0.75), 3f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(((int)player.Center.X), ((int)player.Center.Y), 1.5f, 2.15f, thorium.ProjectileType("TideDagger"), (int)(proj.damage * 0.75), 3f, player.whoAmI, 0f, 0f);
                }
                //assassin
                if (target.type != 488 && Utils.NextFloat(Main.rand) < 0.05f)
                {
                    if ((target.boss || NPCID.Sets.BossHeadTextures[target.type] > -1) && target.life < target.lifeMax * 0.05)
                    {
                        CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), new Color(135, 255, 45), "ERADICATED", false, false);
                        Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasmaDamage"), (int)(target.lifeMax * 1.25f), 0f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasma"), 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                    else if (NPCID.Sets.BossHeadTextures[target.type] < 0)
                    {
                        CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), new Color(135, 255, 45), "ERADICATED", false, false);
                        Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasmaDamage"), (int)(target.lifeMax * 1.25f), 0f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasma"), 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
                //pyro
                if (proj.type != thorium.ProjectileType("PyroBurst"))
                {
                    Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("PyroBurst"), 100, 1f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("PyroExplosion2"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
            }*/
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
                target.AddBuff(mod.BuffType<LeadPoison>(), 180);
            }

            if (SolarEnchant && Main.rand.Next(4) == 0)
            {
                target.AddBuff(mod.BuffType<SolarFlare>(), 300);
            }

            if (RedEnchant && !TerrariaSoul && Soulcheck.GetValue("Red Riding Super Bleed") && Main.rand.Next(5) == 0 && ((Main.moonPhase == 0) || (WillForce)))
            {
                target.AddBuff(mod.BuffType<SuperBleed>(), 240, true);
            }

            if (ShadowEnchant && !TerrariaSoul && Main.rand.Next(15) == 0)
            {
                target.AddBuff(BuffID.Darkness, 600, true);
            }

            if (TikiEnchant && !TerrariaSoul)
            {
                target.AddBuff(mod.BuffType("Infested"), 1800, true);
            }

            if (QueenStinger)
            {
                target.AddBuff(BuffID.Poisoned, 120, true);
            }

            if (ObsidianEnchant)
            {
                target.AddBuff(BuffID.OnFire, 600);
            }

            if (UniverseEffect)
            {
                target.AddBuff(mod.BuffType<Buffs.Masomode.FlamesoftheUniverse>(), 240, true);
            }

            if (GoldEnchant)
            {
                target.AddBuff(BuffID.Midas, 120, true);
            }

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            if (LifeForce)
            {
                //yew wood for all dmg type
                if (!crit)
                {
                    thoriumPlayer.yewChargeTimer = 120;
                    if (player.ownedProjectileCounts[thorium.ProjectileType("YewVisual")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("YewVisual"), 0, 0f, player.whoAmI, 0f, 0f);
                    }
                    if (thoriumPlayer.yewCharge < 4)
                    {
                        thoriumPlayer.yewCharge++;
                    }
                    else
                    {
                        crit = true;
                        damage = (int)(damage * 0.75);
                        thoriumPlayer.yewCharge = 0;
                    }
                }
            }

            if (ShadowForce)
            {
                player.AddBuff(thorium.BuffType("ShadowDance"), 90000, false);
            }

            if (ShroomEnchant)
            {
                target.AddBuff(thorium.BuffType("Mycelium"), 120, true);
            }

            if (NatureForce)
            {
                Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("ReactionNitrogen"), 0, 5f, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("CryoDamage"), damage / 3, 5f, Main.myPlayer, 0f, 0f);
            }

            if (CosmoForce)
            {
                //white dwarf
                if (crit)
                {
                    Main.PlaySound(2, (int)target.position.X, (int)target.position.Y, 92, 1f, 0f);
                    Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("WhiteFlare"), (int)(target.lifeMax * 0.005f), 0f, Main.myPlayer, 0f, 0f);
                }
                //tide turner
                if (player.ownedProjectileCounts[thorium.ProjectileType("TideDagger")] < 24 && target.type != 488 && Main.rand.Next(5) == 0)
                {
                    //diver meme code, I could simplify but meh
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 43, 1f, 0f);
                    Projectile.NewProjectile(((int)player.Center.X), ((int)player.Center.Y), 3f, 0f, thorium.ProjectileType("TideDagger"), (int)(item.damage * 0.75), 3f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(((int)player.Center.X), ((int)player.Center.Y), -3f, 0f, thorium.ProjectileType("TideDagger"), (int)(item.damage * 0.75), 3f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(((int)player.Center.X), ((int)player.Center.Y), -1.5f, -2.15f, thorium.ProjectileType("TideDagger"), (int)(item.damage * 0.75), 3f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(((int)player.Center.X), ((int)player.Center.Y), 1.5f, -2.15f, thorium.ProjectileType("TideDagger"), (int)(item.damage * 0.75), 3f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(((int)player.Center.X), ((int)player.Center.Y), -1.5f, 2.15f, thorium.ProjectileType("TideDagger"), (int)(item.damage * 0.75), 3f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(((int)player.Center.X), ((int)player.Center.Y), 1.5f, 2.15f, thorium.ProjectileType("TideDagger"), (int)(item.damage * 0.75), 3f, player.whoAmI, 0f, 0f);
                }
                //assassin
                if (target.type != 488 && Utils.NextFloat(Main.rand) < 0.05f)
                {
                    if ((target.boss || NPCID.Sets.BossHeadTextures[target.type] > -1) && target.life < target.lifeMax * 0.05)
                    {
                        CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), new Color(135, 255, 45), "ERADICATED", false, false);
                        Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasmaDamage"), (int)(target.lifeMax * 1.25f), 0f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasma"), 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                    else if (NPCID.Sets.BossHeadTextures[target.type] < 0)
                    {
                        CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), new Color(135, 255, 45), "ERADICATED", false, false);
                        Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasmaDamage"), (int)(target.lifeMax * 1.25f), 0f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasma"), 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
                //pyro
                Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("PyroBurst"), 100, 1f, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("PyroExplosion2"), 0, 0f, Main.myPlayer, 0f, 0f);
            }*/
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
            if (CopperEnchant && Soulcheck.GetValue("Copper Lightning") && proj.type != ProjectileID.CultistBossLightningOrbArc && Array.IndexOf(wetProj, proj.type) == -1)
            {
                CopperEffect(target);
            }

            if (Infinity && (player.HeldItem.ranged || player.HeldItem.magic || player.HeldItem.thrown))
            {
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " self destructed."), player.HeldItem.damage / 33, 0);
                player.immune = false;
            }

            if (NecroEnchant && necroCD == 0 && Soulcheck.GetValue("Necro Guardian") && proj.type != mod.ProjectileType<DungeonGuardian>())
            {
                necroCD = 1200;
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
                Projectile p = Projectile.NewProjectileDirect(new Vector2(screenX, screenY), new Vector2(velocityX, velocityY), mod.ProjectileType<DungeonGuardian>(), 500, 0f, player.whoAmI, 0, 120);
                p.penetrate = 1;
                p.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
            }

            if (JungleEnchant && !NatureForce && Main.rand.Next(4) == 0)
            {
                player.ManaEffect(5);
                player.statMana += 4;
            }

            if (Soulcheck.GetValue("Spectre Orbs"))
            {
                if ((SpiritForce || TerrariaSoul) && proj.type != ProjectileID.SpectreWrath)
                {
                    SpectreHeal(target, proj);
                    SpectreHurt(proj);
                }
                else if (SpectreEnchant && !SpiritForce && proj.magic)
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
            }

            if (TerrariaSoul)
            {
                if (crit && TinCrit < 100)
                {
                    TinCrit += 5;
                }
                else if (TinCrit >= 100)
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
            else if (TinEnchant && crit && TinCrit < 100)
            {
                if (TerraForce)
                {
                    TinCrit += 5;
                }
                else
                {
                    TinCrit += 4;
                }
            }

            if (PalladEnchant && palladiumCD == 0)
            {
                int heal = damage / 3;

                if(heal > 100)
                {
                    heal = 100;
                }

                player.statLife += heal;
                player.HealEffect(heal);
                palladiumCD = 600;
            }

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");

            if (LifeForce)
            {
                //tide hunter no dmg type
                if (crit)
                {
                    for (int m = 0; m < 10; m++)
                    {
                        int num11 = Dust.NewDust(target.position, target.width, target.height, 217, (float)Main.rand.Next(-4, 4), (float)Main.rand.Next(-4, 4), 100, default(Color), 1f);
                        Main.dust[num11].noGravity = true;
                        Main.dust[num11].noLight = true;
                    }
                    for (int n = 0; n < 200; n++)
                    {
                        NPC npc = Main.npc[n];
                        if (npc.active && npc.FindBuffIndex(thorium.BuffType("Oozed")) < 0 && !npc.friendly && Vector2.Distance(npc.Center, target.Center) < 80f)
                        {
                            npc.AddBuff(thorium.BuffType("Oozed"), 90, false);
                        }
                    }
                }
            }

            if (NatureForce)
            {
                if (Main.rand.Next(4) == 0)
                {
                    Main.PlaySound(2, (int)proj.position.X, (int)proj.position.Y, 34, 1f, 0f);
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, thorium.ProjectileType("BloomCloud"), 0, 0f, proj.owner, 0f, 0f);
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, thorium.ProjectileType("BloomCloudDamage"), (int)(10f * player.magicDamage), 0f, proj.owner, 0f, 0f);
                }
            }

            if(SpiritForce)
            {
                if (target.life < 0 && target.value > 0f)
                {
                    Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, -2f, thorium.ProjectileType("SpiritTrapperSpirit"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                if (target.boss || NPCID.Sets.BossHeadTextures[target.type] > -1)
                {
                    thoriumPlayer.spiritTrapperHit++;
                }
            }*/
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (CopperEnchant && Soulcheck.GetValue("Copper Lightning"))
            {
                CopperEffect(target);
            }

            if (PalladEnchant && palladiumCD == 0)
            {
                int heal = damage / 3;

                if (heal > 100)
                {
                    heal = 100;
                }

                player.statLife += heal;
                player.HealEffect(heal);
                palladiumCD = 600;
            }

            if (Soulcheck.GetValue("Spectre Orbs") && (SpiritForce || TerrariaSoul))
            {
                //forced orb spawn reeeee
                float num = 4f;
                float speedX = Main.rand.Next(-100, 101);
                float speedY = Main.rand.Next(-100, 101);
                float num2 = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
                num2 = num / num2;
                speedX *= num2;
                speedY *= num2;
                Projectile p = Projectile.NewProjectileDirect(target.position, new Vector2(speedX, speedY), ProjectileID.SpectreWrath, damage / 2, 0, player.whoAmI, target.whoAmI);

                SpectreHeal(target, p);
            }

            if (TerrariaSoul)
            {
                if (crit && TinCrit < 100)
                {
                    TinCrit += 5;
                }
                else if (TinCrit >= 100)
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
            else if (TinEnchant && crit && TinCrit < 100)
            {
                if (TerraForce)
                {
                    TinCrit += 5;
                }
                else
                {
                    TinCrit += 4;
                }
            }

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            if (LifeForce)
            {
                //tide hunter no dmg type
                if (crit)
                {
                    for (int m = 0; m < 10; m++)
                    {
                        int num11 = Dust.NewDust(target.position, target.width, target.height, 217, (float)Main.rand.Next(-4, 4), (float)Main.rand.Next(-4, 4), 100, default(Color), 1f);
                        Main.dust[num11].noGravity = true;
                        Main.dust[num11].noLight = true;
                    }
                    for (int n = 0; n < 200; n++)
                    {
                        NPC npc = Main.npc[n];
                        if (npc.active && npc.FindBuffIndex(thorium.BuffType("Oozed")) < 0 && !npc.friendly && Vector2.Distance(npc.Center, target.Center) < 80f)
                        {
                            npc.AddBuff(thorium.BuffType("Oozed"), 90, false);
                        }
                    }
                }
            }

            if(NatureForce)
            {
                if (Main.rand.Next(4) == 0)
                {
                    Main.PlaySound(2, (int)target.position.X, (int)target.position.Y, 34, 1f, 0f);
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, thorium.ProjectileType("BloomCloud"), 0, 0f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, thorium.ProjectileType("BloomCloudDamage"), (int)(10f * player.magicDamage), 0f, player.whoAmI, 0f, 0f);
                }
            }*/
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (ShellHide) return false;
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

            if (IronGuard && internalTimer > 0 && !player.immune)
            {
                player.immune = true;
                player.immuneTime = player.longInvince ? 60 : 30;
                player.AddBuff(BuffID.ParryDamageBuff, 300);
                return false;
            }

            return true;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (JungleEnchant && Soulcheck.GetValue("Jungle Spores"))
            {
                int dmg = 30;

                if (NatureForce)
                {
                    dmg = 100;
                }

                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 62);

                Projectile[] projs = FargoGlobalProjectile.XWay(8, player.Center, mod.ProjectileType<SporeBoom>(), 5, (int)(dmg * player.magicDamage), 5f);
                Projectile[] projs2 = FargoGlobalProjectile.XWay(8, player.Center, mod.ProjectileType<SporeBoom>(), 2.5f, 0, 0f);

                for(int i = 0; i < projs.Length; i++)
                {
                    projs[i].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                    projs2[i].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                }
            }

            if(TinEnchant)
            {
                if (TerrariaSoul && TinCrit != 25)
                {
                    TinCrit = 25;
                }
                else if(TerraForce && TinCrit != 10)
                {
                    TinCrit = 10;
                }
                else if(TinCrit != 4)
                {
                    TinCrit = 4;
                }
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
                Projectile p = Projectile.NewProjectileDirect(player.Center, Vector2.Zero, mod.ProjectileType<Explosion>(), (int)(500 * player.meleeDamage), 0f, Main.myPlayer);

                p.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
            }

            if(player.FindBuffIndex(mod.BuffType<Revived>()) == -1)
            {
                if (TerrariaSoul)
                {
                    player.statLife = 200;
                    player.HealEffect(200);
                    player.immune = true;
                    player.immuneTime = player.longInvince ? 180 : 120;
                    Main.NewText("You've been revived!", 175, 75);
                    player.AddBuff(mod.BuffType<Revived>(), 14400);
                    retVal = false;
                }
                else if (FossilEnchant)
                {
                    player.statLife = 20;

                    if(SpiritForce)
                    {
                        player.statLife = 100;
                    }

                    player.HealEffect(20);
                    player.immune = true;
                    player.immuneTime = player.longInvince ? 300 : 200;
                    FossilBones = true;
                    Main.NewText("You've been revived!", 175, 75);
                    player.AddBuff(mod.BuffType<Revived>(), 18000);
                    retVal = false;
                }
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

            return retVal;
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (Infinity)
            {
                return false;
            }

            return true;
        }

        public override void PostUpdateEquips()
        {
            if (BeetleEnchant)
            {
                player.wingTimeMax = (int)(player.wingTimeMax * 2);
            }
        }

        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (IronGuard)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 10;
            }
        }

        public void AddPet(string toggle, bool vanityToggle, int buff, int proj)
        {
            if(vanityToggle)
            {
                PetsActive = false;
                return;
            }

            if (Soulcheck.GetValue(toggle) && player.FindBuffIndex(buff) == -1 && player.ownedProjectileCounts[proj] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, proj, 0, 0f, player.whoAmI);
            }
        }

        public void AddMinion(string toggle, int proj, int damage, float knockback)
        {
            if(player.ownedProjectileCounts[proj] >= 1)
            {
                return;
            }

            if (Soulcheck.GetValue(toggle))
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, proj, damage, knockback, Main.myPlayer);
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
            int buffIndex = player.FindBuffIndex(mod.BuffType<Infested>());

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

            if (Fargowiltas.Instance.ThoriumLoaded)
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

        public void BeeEffect(bool hideVisual)
        {
            player.strongBees = true;
            //bees ignore defense
            BeeEnchant = true;  
            AddPet("Hornet Pet", hideVisual, BuffID.BabyHornet, ProjectileID.BabyHornet);
        }

        public void BeetleEffect()
        {
            if (!Soulcheck.GetValue("Beetles")) return;

            player.beetleDefense = true;
            player.beetleCounter += 1f;
            int num5 = 180;
            if (player.beetleCounter >= num5)
            {
                if (player.beetleOrbs > 0 && player.beetleOrbs < 3)
                {
                    for (int k = 0; k < 22; k++)
                    {
                        if (player.buffType[k] >= 95 && player.buffType[k] <= 96)
                        {
                            player.DelBuff(k);
                        }
                    }
                }
                if (player.beetleOrbs < 3)
                {
                    player.AddBuff(95 + player.beetleOrbs, 5, false);
                    player.beetleCounter = 0f;
                }
                else
                {
                    player.beetleCounter = num5;
                }
            }

            if (!player.beetleDefense && !player.beetleOffense)
            {
                player.beetleCounter = 0f;
            }
            else
            {
                player.beetleFrameCounter++;
                if (player.beetleFrameCounter >= 1)
                {
                    player.beetleFrameCounter = 0;
                    player.beetleFrame++;
                    if (player.beetleFrame > 2)
                    {
                        player.beetleFrame = 0;
                    }
                }
                for (int l = player.beetleOrbs; l < 3; l++)
                {
                    player.beetlePos[l].X = 0f;
                    player.beetlePos[l].Y = 0f;
                }
                for (int m = 0; m < player.beetleOrbs; m++)
                {
                    player.beetlePos[m] += player.beetleVel[m];
                    Vector2[] expr_6EcCp0 = player.beetleVel;
                    int expr_6EcCp1 = m;
                    expr_6EcCp0[expr_6EcCp1].X = expr_6EcCp0[expr_6EcCp1].X + Main.rand.Next(-100, 101) * 0.005f;
                    Vector2[] expr71ACp0 = player.beetleVel;
                    int expr71ACp1 = m;
                    expr71ACp0[expr71ACp1].Y = expr71ACp0[expr71ACp1].Y + Main.rand.Next(-100, 101) * 0.005f;
                    float num6 = player.beetlePos[m].X;
                    float num7 = player.beetlePos[m].Y;
                    float num8 = (float)Math.Sqrt(num6 * num6 + num7 * num7);
                    if (num8 > 100f)
                    {
                        num8 = 20f / num8;
                        num6 *= -num8;
                        num7 *= -num8;
                        int num9 = 10;
                        player.beetleVel[m].X = (player.beetleVel[m].X * (num9 - 1) + num6) / num9;
                        player.beetleVel[m].Y = (player.beetleVel[m].Y * (num9 - 1) + num7) / num9;
                    }
                    else if (num8 > 30f)
                    {
                        num8 = 10f / num8;
                        num6 *= -num8;
                        num7 *= -num8;
                        int num10 = 20;
                        player.beetleVel[m].X = (player.beetleVel[m].X * (num10 - 1) + num6) / num10;
                        player.beetleVel[m].Y = (player.beetleVel[m].Y * (num10 - 1) + num7) / num10;
                    }
                    num6 = player.beetleVel[m].X;
                    num7 = player.beetleVel[m].Y;
                    num8 = (float)Math.Sqrt(num6 * num6 + num7 * num7);
                    if (num8 > 2f)
                    {
                        player.beetleVel[m] *= 0.9f;
                    }
                    player.beetlePos[m] -= player.velocity * 0.25f;
                }
            }
        }

        public void CactusEffect()
        {
            if(Soulcheck.GetValue("Cactus Needles"))
            {
                CactusEnchant = true;
            }
        }

        public void ChloroEffect(bool hideVisual, int dmg)
        {
            AddMinion("Chlorophyte Leaf Crystal", mod.ProjectileType<Chlorofuck>(), dmg, 10f);
            AddPet("Seedling Pet", hideVisual, BuffID.PetSapling, ProjectileID.Sapling);
        }

        public void CopperEffect(NPC target)
        {
            int dmg = 5;
            int chance = 20;

            if (target.FindBuffIndex(BuffID.Wet) != -1)
            {
                dmg *= 2;
                chance /= 4;
            }

            if (TerraForce)
            {
                dmg *= 2;
            }

            if (Main.rand.Next(chance) == 0)
            {
                float closestDist = 500f;
                NPC closestNPC;

                for (int i = 0; i < 5; i++)
                {
                    closestNPC = null;

                    for (int j = 0; j < 200; j++)
                    {
                        NPC npc = Main.npc[j];
                        if (npc.active && npc != target && !npc.HasBuff(mod.BuffType<Shock>()) && npc.Distance(target.Center) < closestDist && npc.Distance(target.Center) >= 50)
                        {
                            closestNPC = npc;
                            break;
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
                        p.timeLeft = 60;
                        p.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;

                        target.AddBuff(mod.BuffType<Shock>(), 60);
                    }
                    else
                    {
                        break;
                    }

                    target = closestNPC;
                }
            }
        }

        public void CrimsonEffect(bool hideVisual)
        {
            player.crimsonRegen = true;
            //increase heart heal
            CrimsonEnchant = true;
            AddPet("Face Monster Pet", hideVisual, BuffID.BabyFaceMonster, ProjectileID.BabyFaceMonster);
            AddPet("Crimson Heart Pet", hideVisual, BuffID.CrimsonHeart, ProjectileID.CrimsonHeart);
        }

        public void DarkArtistEffect(bool hideVisual)
        {
            //shadow shoot meme
            if (Soulcheck.GetValue("Dark Artist Effect"))
            {
                Item heldItem = player.HeldItem;
                Projectile proj = new Projectile();
                proj.SetDefaults(heldItem.shoot);

                if (darkCD == 0 && !heldItem.summon && heldItem.shoot > 0 && heldItem.damage > 0 && !heldItem.channel && proj.aiStyle != 19 && player.controlUseItem && Vector2.Distance(prevPosition, player.position) > 25)
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

                if (darkCD < 0)
                {
                    darkCD = 0;
                }
            }

            DarkEnchant = true;
            AddPet("Flickerwick Pet", hideVisual, BuffID.PetDD2Ghost, ProjectileID.DD2PetGhost);
        }

        public void ForbiddenEffect()
        {
            if (!Soulcheck.GetValue("Forbidden Storm")) return;

            player.setForbidden = true;
            player.UpdateForbiddenSetLock();
            Lighting.AddLight(player.Center, 0.8f, 0.7f, 0.2f);
            //storm boosted
            ForbiddenEnchant = true;
        }

        public void FossilEffect(int dmg, bool hideVisual)
        {
            FossilEnchant = true;

            //bone zone
            if (FossilBones)
            {
                if (boneCD == 0)
                {
                    for (int i = 0; i < Main.rand.Next(4, 12); i++)
                    {
                        float randX, randY;

                        do
                        {
                            randX = Main.rand.Next(-10, 10);
                        } while (randX <= 4f && randX >= -4f);

                        do
                        {
                            randY = Main.rand.Next(-10, 10);
                        } while (randY <= 4f && randY >= -4f);

                        Projectile p = Projectile.NewProjectileDirect(player.Center, new Vector2(randX, randY), ProjectileID.BoneGloveProj, (int)(dmg * player.thrownDamage), 2, Main.myPlayer);
                        p.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                    }

                    Projectile p2 = Projectile.NewProjectileDirect(player.Center, Vector2.Zero, ProjectileID.Bone, (int)(dmg * 1.5 * player.thrownDamage), 0f, player.whoAmI);
                    p2.GetGlobalProjectile<FargoGlobalProjectile>().Rotate = true;
                    p2.GetGlobalProjectile<FargoGlobalProjectile>().RotateDist = Main.rand.Next(32, 128);
                    p2.GetGlobalProjectile<FargoGlobalProjectile>().RotateDir = Main.rand.Next(2);
                    p2.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                    p2.noDropItem = true;

                    boneCD = 20;
                }

                boneCD--;

                if (!player.immune)
                {
                    FossilBones = false;
                }
            }

            AddPet("Dino Pet", hideVisual, BuffID.BabyDinosaur, ProjectileID.BabyDino);
        }

        public void FrostEffect(int dmg, bool hideVisual)
        {
            FrostEnchant = true;

            if (Soulcheck.GetValue("Frost Icicles"))
            {
                if (icicleCD == 0 && IcicleCount < 3)
                {
                    Projectile p = Projectile.NewProjectileDirect(player.Center, Vector2.Zero, ProjectileID.Blizzard, 0, 0, player.whoAmI);
                    p.GetGlobalProjectile<FargoGlobalProjectile>().Rotate = true;
                    p.width = 10;
                    p.height = 10;
                    p.timeLeft = 2;

                    icicles[IcicleCount] = p;
                    IcicleCount++;
                    icicleCD = 120;
                }

                if (icicleCD != 0)
                {
                    icicleCD--;
                }

                if (IcicleCount == 3 && player.controlUseItem && player.HeldItem.damage > 0)
                {
                    for (int i = 0; i < icicles.Length; i++)
                    {
                        Vector2 vel = (Main.MouseWorld - icicles[i].Center).SafeNormalize(-Vector2.UnitY) * 5;

                        int p = Projectile.NewProjectile(icicles[i].Center, vel, icicles[i].type, dmg, 1f, player.whoAmI);
                        icicles[i].Kill();

                        Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                    }

                    IcicleCount = 0;
                    icicleCD = 300;
                }
            }
            
            AddPet("Snowman Pet", hideVisual, BuffID.BabySnowman, ProjectileID.BabySnowman);
            AddPet("Penguin Pet", hideVisual, BuffID.BabyPenguin, ProjectileID.Penguin);
        }

        public void GladiatorEffect(bool hideVisual)
        {
            GladEnchant = true;
            AddPet("Mini Minotaur Pet", hideVisual, BuffID.MiniMinotaur, ProjectileID.MiniMinotaur);
        }

        public void GoldEffect(bool hideVisual)
        {
            //gold ring
            player.goldRing = true;
            //lucky coin
            if (Soulcheck.GetValue("Gold Coins on Hit"))
                player.coins = true;
            //discount card
            player.discount = true;
            //midas
            GoldEnchant = true;

            //if (Fargowiltas.Instance.ThoriumLoaded) return;

            AddPet("Parrot Pet", hideVisual, BuffID.PetParrot, ProjectileID.Parrot);
        }

        public void HallowEffect(bool hideVisual, int dmg)
        {
            HallowEnchant = true;
            AddMinion("Enchanted Sword Familiar", mod.ProjectileType<Projectiles.Minions.HallowSword>(), (int)(dmg * player.minionDamage), 0f);

            //reflect proj
            if (Soulcheck.GetValue("Hallowed Shield"))
            {
                const int focusRadius = 50;

                if (player.velocity.X < 1.5f && player.velocity.Y < 1.5f)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        Vector2 offset = new Vector2();
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * focusRadius);
                        offset.Y += (float)(Math.Cos(angle) * focusRadius);
                        Dust dust = Main.dust[Dust.NewDust(
                            player.Center + offset - new Vector2(4, 4), 0, 0,
                            DustID.GoldFlame, 0, 0, 100, Color.White, 1f
                            )];
                        dust.velocity = player.velocity;
                        dust.noGravity = true;
                    }
                }

                float distance = 5f * 16;

                Main.projectile.Where(x => x.active && x.hostile).ToList().ForEach(x =>
                {
                    if (Main.rand.Next(5) == 0 && Vector2.Distance(x.Center, player.Center) <= distance)
                    {
                        for (int i = 0; i < 5; i++)
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

            AddPet("Fairy Pet", hideVisual, BuffID.FairyBlue, ProjectileID.BlueFairy);
        }

        private int internalTimer = 0;
        private bool wasHoldingShield = false;

        public void IronEffect()
        {
            //no need when player has brand of inferno
            if (player.inventory[player.selectedItem].type == ItemID.DD2SquireDemonSword)
            {
                internalTimer = 0;
                wasHoldingShield = false;
                return;
            }

            player.shieldRaised = player.selectedItem != 58 && player.controlUseTile && (!player.tileInteractionHappened && player.releaseUseItem) && (!player.controlUseItem && !player.mouseInterface && (!CaptureManager.Instance.Active && !Main.HoveringOverAnNPC)) && !Main.SmartInteractShowingGenuine && !player.mount.Active && (player.itemAnimation == 0 || PlayerInput.Triggers.JustPressed.MouseRight);

            if (internalTimer > 0)
            {
                internalTimer++;
                player.shieldParryTimeLeft = internalTimer;
                if (player.shieldParryTimeLeft > 20)
                {
                    player.shieldParryTimeLeft = 0;
                    internalTimer = 0;
                }
            }

            if (player.shieldRaised)
            {
                IronGuard = true;

                for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
                {
                    if (player.shield == -1 && player.armor[i].shieldSlot != -1)
                        player.shield = player.armor[i].shieldSlot;
                }

                if (!wasHoldingShield)
                {
                    wasHoldingShield = true;

                    if (player.shield_parry_cooldown == 0)
                    {
                        internalTimer = 1;
                    }
                        
                    player.itemAnimation = 0;
                    player.itemTime = 0;
                    player.reuseDelay = 0;
                }
            }
            else if (wasHoldingShield)
            {
                wasHoldingShield = false;
                player.shield_parry_cooldown = 15;
                player.shieldParryTimeLeft = 0;
                internalTimer = 0;
            }
        }

        public void JungleEffect()
        {
            JungleEnchant = true;

            if (/*Fargowiltas.Instance.ThoriumLoaded || */NatureForce) return;

            player.cordage = true;
        }

        public void MeteorEffect(int damage)
        {
            MeteorEnchant = true;

            if (Soulcheck.GetValue("Meteor Shower"))
            {
                if (meteorShower)
                {
                    if (meteorTimer % 2 == 0)
                    {
                        int p = Projectile.NewProjectile(player.Center.X + Main.rand.Next(-1000, 1000), player.Center.Y - 1000, Main.rand.Next(-2, 2), 0f + Main.rand.Next(8, 12), Main.rand.Next(424, 427), (int)(damage * player.magicDamage), 0f, player.whoAmI, 0f, 0.5f + (float)Main.rand.NextDouble() * 0.3f);

                        Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
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
                    if (player.controlUseItem)
                    {
                        meteorCD--;

                        if (meteorCD == 0)
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
        }

        public void MinerEffect(bool hideVisual, float pickSpeed)
        {
            player.pickSpeed -= pickSpeed;

            if (Soulcheck.GetValue("Spelunker Buff"))
            {
                player.findTreasure = true;
            }

            if (Soulcheck.GetValue("Hunter Buff"))
            {
                player.detectCreature = true;
            }

            if (Soulcheck.GetValue("Dangersense Buff"))
            {
                player.dangerSense = true;
            }

            if (Soulcheck.GetValue("Shine Buff"))
            {
                Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);
            }

            MinerEnchant = true;

            AddPet("Magic Lantern Pet", hideVisual, BuffID.MagicLantern, ProjectileID.MagicLantern);
        }

        public void MoltenEffect(int dmg)
        {
            MoltenEnchant = true;

            if (Soulcheck.GetValue("Molten Inferno"))
            {
                player.inferno = true;
                Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.65f, 0.4f, 0.1f);
                int buff = BuffID.OnFire;
                float distance = 200f;
                bool doDmg = player.infernoCounter % 60 == 0;
                int damage = (int)(dmg * player.meleeDamage);

                if (player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && !npc.friendly && npc.damage > 0 && !npc.dontTakeDamage && !npc.buffImmune[buff] && Vector2.Distance(player.Center, npc.Center) <= distance)
                        {
                            if (npc.FindBuffIndex(buff) == -1)
                            {
                                npc.AddBuff(buff, 120);
                            }
                            if (doDmg)
                            {
                                player.ApplyDamageToNPC(npc, damage, 0f, 0, false);
                            }
                        }
                    }
                }
            }
        }

        public void NebulaEffect()
        {
            NebulaEnchant = true;

            if (!Soulcheck.GetValue("Nebula Boosters")) return;

            if (player.nebulaCD > 0)
            {
                player.nebulaCD--;
            }
            player.setNebula = true;

            if (TerrariaSoul) return;

            if (player.nebulaLevelDamage == 3 && player.nebulaLevelLife == 3 && player.nebulaLevelMana == 3 && NebulaCounter == 0)
            {
                    NebulaCounter = 1200;
            }
            else if(NebulaCounter != 0)
            {
                NebulaCounter--;
            }
        }

        public void NecroEffect(bool hideVisual)
        {
            NecroEnchant = true;

            if (necroCD != 0)
            {
                necroCD--;
            }

            AddPet("Skeletron Pet", hideVisual, BuffID.BabySkeletronHead, ProjectileID.BabySkeletronHead);
        }

        public void NinjaEffect(bool hideVisual)
        {
            NinjaEnchant = true;

            //ninja smoke bomb nonsense
            float distance = 4 * 16;
            List<Projectile> projs = Main.projectile.Where(x => x.active && x.type == ProjectileID.SmokeBomb).ToList();

            foreach(Projectile p in projs)
            {
                if (Vector2.Distance(p.Center, player.Center) <= distance)
                {
                    player.AddBuff(mod.BuffType<FirstStrike>(), 300);
                    break;
                }
            }

            AddPet("Black Cat Pet", hideVisual, BuffID.BlackCat, ProjectileID.BlackCat);
        }

        public void ObsidianEffect()
        {
            player.fireWalk = true;
            player.lavaImmune = true;
            player.armorPenetration += 10;

            //in lava effects
            if (player.lavaWet)
            {
                player.armorPenetration += 10;
                AttackSpeed *= 1.15f;
                ObsidianEnchant = true;
            }
        }

        public void OrichalcumEffect()
        {
            if (!Soulcheck.GetValue("Orichalcum Fireballs")) return;

            player.onHitPetal = true;

            OriEnchant = true;

            if (!OriSpawn)
            {
                int[] fireballs = { ProjectileID.BallofFire, ProjectileID.BallofFrost, ProjectileID.CursedFlameFriendly };

                int ballAmt = 3;
                float degree;

                for (int i = 0; i < ballAmt; i++)
                {
                    degree = (360 / ballAmt) * i;
                    Projectile fireball = Projectile.NewProjectileDirect(player.Center, Vector2.Zero, fireballs[i], (int)(10 * player.magicDamage), 0f, player.whoAmI, 0, degree);
                    fireball.GetGlobalProjectile<FargoGlobalProjectile>().Rotate = true;
                    fireball.GetGlobalProjectile<FargoGlobalProjectile>().RotateDist = 96;
                    fireball.timeLeft = 2;
                    fireball.penetrate = -1;
                }

                OriSpawn = true;
            }
        }

        public void PalladiumEffect()
        {
            player.onHitRegen = true;
            PalladEnchant = true;

            if(palladiumCD != 0)
            {
                palladiumCD--;
            }
        }

        public void PumpkinEffect(int dmg, bool hideVisual)
        {
            //pumpkin pies
            PumpkinEnchant = true;

            if (Soulcheck.GetValue("Pumpkin Fire") && (player.controlLeft || player.controlRight) && !IsStandingStill)
            {
                if (pumpkinCD <= 0)
                {
                    int p = Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileID.MolotovFire, (int)(dmg * player.magicDamage), 1f, player.whoAmI);

                    Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                    pumpkinCD = 20;
                }

                pumpkinCD--;
            }

            AddPet("Squashling Pet", hideVisual, BuffID.Squashling, ProjectileID.Squashling);
        }

        public void RedRidingEffect(bool hideVisual)
        {
            //super bleed, low hp dmg
            RedEnchant = true;

            AddPet("Puppy Pet", hideVisual, BuffID.Puppy, ProjectileID.Puppy);
        }
        
        public void ShadowEffect(bool hideVisual)
        {
            ShadowEnchant = true;
            AddPet("Eater Pet", hideVisual, BuffID.BabyEater, ProjectileID.BabyEater);
            AddPet("Shadow Orb Pet", hideVisual, BuffID.ShadowOrb, ProjectileID.ShadowOrb);
        }

        public void ShinobiEffect(bool hideVisual)
        {
            //tele through wall until open space on dash into wall
            if (Soulcheck.GetValue("Shinobi Through Walls") && player.dashDelay > 0 && player.velocity.X == 0)
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

            ShinobiEnchant = true;
            AddPet("Gato Pet", hideVisual, BuffID.PetDD2Gato, ProjectileID.DD2PetGato);
        }

        public void ShroomiteEffect(bool hideVisual)
        {
            if (!TerrariaSoul && Soulcheck.GetValue("Shroomite Stealth"))
            {
                player.shroomiteStealth = true;
            }

            ShroomEnchant = true;
            AddPet("Truffle Pet", hideVisual, BuffID.BabyTruffle, ProjectileID.Truffle);
        }

        public void SolarEffect()
        {  
            if (!Soulcheck.GetValue("Solar Shield")) return;

            player.AddBuff(BuffID.SolarShield3, 5, false);
            player.setSolar = true;
            player.solarCounter++;
            int solarCD = 240;
            if (player.solarCounter >= solarCD)
            {
                if (player.solarShields > 0 && player.solarShields < 3)
                {
                    for (int i = 0; i < 22; i++)
                    {
                        if (player.buffType[i] >= BuffID.SolarShield1 && player.buffType[i] <= BuffID.SolarShield2)
                        {
                            player.DelBuff(i);
                        }
                    }
                }
                if (player.solarShields < 3)
                {
                    player.AddBuff(BuffID.SolarShield1 + player.solarShields, 5, false);
                    for (int i = 0; i < 16; i++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 6, 0f, 0f, 100)];
                        dust.noGravity = true;
                        dust.scale = 1.7f;
                        dust.fadeIn = 0.5f;
                        dust.velocity *= 5f;
                    }
                    player.solarCounter = 0;
                }
                else
                {
                    player.solarCounter = solarCD;
                }
            }
            for (int i = player.solarShields; i < 3; i++)
            {
                player.solarShieldPos[i] = Vector2.Zero;
            }
            for (int i = 0; i < player.solarShields; i++)
            {
                player.solarShieldPos[i] += player.solarShieldVel[i];
                Vector2 value = (player.miscCounter / 100f * 6.28318548f + i * (6.28318548f / player.solarShields)).ToRotationVector2() * 6f;
                value.X = player.direction * 20;
                player.solarShieldVel[i] = (value - player.solarShieldPos[i]) * 0.2f;
            }
            if (player.dashDelay >= 0)
            {
                player.solarDashing = false;
                player.solarDashConsumedFlare = false;
            }
            bool flag = player.solarDashing && player.dashDelay < 0;
            if (player.solarShields > 0 || flag)
            {
                player.dash = 3;
            }
        }

        public void SpectreEffect(bool hideVisual)
        {
            SpectreEnchant = true;
            AddPet("Wisp Pet", hideVisual, BuffID.Wisp, ProjectileID.Wisp);

            if (SpiritForce || TerrariaSoul) return;

            if (SpecHeal)
            {
                player.ghostHeal = true;
            }
            else
            {
                player.ghostHurt = true;
            }
        }

        public void SpectreHeal(NPC npc, Projectile proj)
        {
            if (npc.canGhostHeal && !player.moonLeech)
            {
                float num = 0.2f;
                num -= proj.numHits * 0.05f;
                if (num <= 0f)
                {
                    return;
                }
                float num2 = proj.damage * num;
                if ((int)num2 <= 0)
                {
                    return;
                }
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= num2;

                float num3 = 0f;
                int num4 = proj.owner;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && !Main.player[i].dead && ((!Main.player[proj.owner].hostile && !Main.player[i].hostile) || Main.player[proj.owner].team == Main.player[i].team))
                    {
                        float num5 = Math.Abs(Main.player[i].position.X + (Main.player[i].width / 2) - proj.position.X + (proj.width / 2)) + Math.Abs(Main.player[i].position.Y + (Main.player[i].height / 2) - proj.position.Y + (proj.height / 2));
                        if (num5 < 1200f && (Main.player[i].statLifeMax2 - Main.player[i].statLife) > num3)
                        {
                            num3 = (Main.player[i].statLifeMax2 - Main.player[i].statLife);
                            num4 = i;
                        }
                    }
                }
                Projectile.NewProjectile(proj.position.X, proj.position.Y, 0f, 0f, ProjectileID.SpiritHeal, 0, 0f, proj.owner, num4, num2);
            }
        }

        public void SpectreHurt(Projectile proj)
        {
            int num = proj.damage / 2;
            if (proj.damage / 2 <= 1)
            {
                return;
            }
            int num2 = 1000;
            if (Main.player[Main.myPlayer].ghostDmg > (float)num2)
            {
                return;
            }
            Main.player[Main.myPlayer].ghostDmg += (float)num;
            int[] array = new int[200];
            int num3 = 0;
            int num4 = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(this, false))
                {
                    float num5 = Math.Abs(Main.npc[i].position.X + (Main.npc[i].width / 2) - proj.position.X + (proj.width / 2)) + Math.Abs(Main.npc[i].position.Y + (Main.npc[i].height / 2) - proj.position.Y + (proj.height / 2));
                    if (num5 < 800f)
                    {
                        if (Collision.CanHit(proj.position, 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height) && num5 > 50f)
                        {
                            array[num4] = i;
                            num4++;
                        }
                        else if (num4 == 0)
                        {
                            array[num3] = i;
                            num3++;
                        }
                    }
                }
            }
            if (num3 == 0 && num4 == 0)
            {
                return;
            }
            int num6;
            if (num4 > 0)
            {
                num6 = array[Main.rand.Next(num4)];
            }
            else
            {
                num6 = array[Main.rand.Next(num3)];
            }
            float num7 = 4f;
            float num8 = Main.rand.Next(-100, 101);
            float num9 = Main.rand.Next(-100, 101);
            float num10 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
            num10 = num7 / num10;
            num8 *= num10;
            num9 *= num10;
            Projectile.NewProjectile(proj.position.X, proj.position.Y, num8, num9, ProjectileID.SpectreWrath, num, 0f, proj.owner, (float)num6, 0f);
        }

        public void SpiderEffect(bool hideVisual)
        {
            //half price spiders
            SpiderEnchant = true;
            AddPet("Spider Pet", hideVisual, BuffID.PetSpider, ProjectileID.Spider);
        }

        public void SpookyEffect(bool hideVisual)
        {
            //scythe doom
            SpookyEnchant = true;
            AddPet("Cursed Sapling Pet", hideVisual, BuffID.CursedSapling, ProjectileID.CursedSapling);
            AddPet("Eye Spring Pet", hideVisual, BuffID.EyeballSpring, ProjectileID.EyeSpring);
        }

        public void StardustEffect()
        {
            StardustEnchant = true;
            AddPet("Stardust Guardian", false, BuffID.StardustGuardianMinion, ProjectileID.StardustGuardian);
            player.setStardust = true;

            if (FreezeTime && freezeLength != 0)
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && !npc.HasBuff(mod.BuffType("TimeFrozen")))
                    {
                        npc.AddBuff(mod.BuffType("TimeFrozen"), freezeLength);
                    }
                }

                for (int i = 0; i < 1000; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.GetGlobalProjectile<FargoGlobalProjectile>().TimeFrozen == 0)
                    {
                        p.GetGlobalProjectile<FargoGlobalProjectile>().TimeFrozen = freezeLength;
                    }
                }

                freezeLength--;

                if (freezeLength == 0)
                {
                    FreezeTime = false;
                    freezeLength = 300;

                    for (int i = 0; i < 200; i++)
                    {
                        NPC npc = Main.npc[i];

                        if (npc.active && npc.life == 1)
                        {
                            npc.StrikeNPC(9999, 0f, 0);
                        }
                    }
                }
            }

            if (FreezeCD != 0 && !FreezeTime)
            {
                FreezeCD--;

                if (FreezeCD == 0)
                {
                    Main.PlaySound(SoundID.MaxMana, player.Center);
                }
            }
        }

        public void TikiEffect(bool hideVisual)
        {
            TikiEnchant = true;
            AddPet("Tiki Pet", hideVisual, BuffID.TikiSpirit, ProjectileID.TikiSpirit);
        }

        public void TinEffect()
        {
            if (!Soulcheck.GetValue("Tin Crit")) return;

            TinEnchant = true;
            AllCritEquals(TinCrit);
        }

        public void TitaniumEffect()
        {
            if(!TerrariaSoul && player.statLife == player.statLifeMax2)
            {
                player.endurance = .9f;
            }

            player.onHitDodge = true;
        }

        public void TungstenEffect()
        {
            if (!Soulcheck.GetValue("Tungsten Effect")) return;

            AllDamageUp(3);
            AllCritUp(25);
            AttackSpeed *= .125f;
        }

        public void TurtleEffect(bool hideVisual)
        {
            TurtleEnchant = true;
            AddPet("Turtle Pet", hideVisual, BuffID.PetTurtle, ProjectileID.Turtle);
            AddPet("Lizard Pet", hideVisual, BuffID.PetLizard, ProjectileID.PetLizard);

            if (!TerrariaSoul && Soulcheck.GetValue("Turtle Shell Buff") && IsStandingStill && !player.controlUseItem)
            {
                player.AddBuff(mod.BuffType<ShellHide>(), 2);
            }
        }

        public void ValhallaEffect(bool hideVisual)
        {
            //knockback memes
            ValhallaEnchant = true;
            AddPet("Dragon Pet", hideVisual, BuffID.PetDD2Dragon, ProjectileID.DD2PetDragon);
        }

        public void VortexEffect(bool hideVisual)
        {
            //portal spawn
            VortexEnchant = true;
            //stealth memes
            if ((player.controlDown && player.releaseDown))
            {
                if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15)
                {
                    VortexStealth = !VortexStealth;

                    if(Soulcheck.GetValue("Vortex Voids") && vortexCD == 0 && VortexStealth)
                    {
                        int p = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType<Projectiles.Void>(), 60, 5f, player.whoAmI);

                        Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                        vortexCD = 1200;
                    }
                }
            }

            if(vortexCD != 0)
            {
                vortexCD--;
            }

            if (player.mount.Active)
            {
                VortexStealth = false;
            }

            if (VortexStealth)
            {
                player.moveSpeed *= 0.3f;
                player.aggro -= 1200;
                player.setVortex = true;
                player.stealth = 0f;
            }

            AddPet("Companion Cube Pet", hideVisual, BuffID.CompanionCube, ProjectileID.CompanionCube);
        }

        public override bool PreItemCheck()
        {
            if (UniverseEffect)
            {
                UniverseStoredAutofire = player.HeldItem.autoReuse;
                player.HeldItem.autoReuse = true;
            }

            return true;
        }

        public override void PostItemCheck()
        {
            if (UniverseEffect)
            {
                player.HeldItem.autoReuse = UniverseStoredAutofire;
            }
        }

    }
}
