using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Graphics.Capture;
using FargowiltasSouls.NPCs;
using FargowiltasSouls.Projectiles;
using ThoriumMod;
using CalamityMod.Items.CalamityCustomThrowingDamage;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace FargowiltasSouls
{
    public class FargoPlayer : ModPlayer
    {
        //for convenience
        public bool IsStandingStill;
        public float AttackSpeed;
        public float wingTimeModifier;

        public bool Wood;
        public bool QueenStinger;

        //minions
        public bool BrainMinion;
        public bool EaterMinion;

        //pet
        public bool RoombaPet;

        #region enchantments
        public bool PetsActive = true;
        public bool ShadowEnchant;
        public bool CrimsonEnchant;
        public bool SpectreEnchant;
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
        public int CobaltCD = 0;
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
        private int copperCD = 0;
        public bool NinjaEnchant;
        public bool FirstStrike;
        public bool NearSmoke;
        public bool IronEnchant;
        public bool IronGuard;
        public bool TurtleEnchant;
        public bool ShellHide;
        public bool LeadEnchant;
        public bool GladEnchant;
        private int gladCount = 0;
        public bool GoldEnchant;
        public bool GoldShell;
        private int goldCD = 0;
        private int goldHP;
        public bool CactusEnchant;
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
        Vector2 prevPosition;
        public bool RedEnchant;
        public bool TungstenEnchant;
        private float tungstenPrevSizeSave = -1;

        public bool MahoganyEnchant;
        public bool BorealEnchant;
        public int BorealCount = 0;
        public bool WoodEnchant;
        public bool ShadeEnchant;
        private int pearlCounter = 0;
        public bool SuperBleed;

        public bool CosmoForce;
        public bool EarthForce;
        public bool LifeForce;
        public bool NatureForce;
        public bool SpiritForce;
        public bool ShadowForce;
        public bool TerraForce;
        public bool WillForce;
        public bool WoodForce;

        //thorium 
        public bool IcyEnchant;
        public bool WarlockEnchant;
        public bool SacredEnchant;
        public bool BinderEnchant;
        public bool LivingWoodEnchant;
        public bool DepthEnchant;
        public bool KnightEnchant;
        public bool DreamEnchant;
        public bool IllumiteEnchant;

        public bool AsgardForce;
        public bool MidgardForce;
        public bool SvartalfheimForce;
        public bool HelheimForce;
        public bool VanaheimForce;
        public bool MuspelheimForce;
        public bool JotunheimForce;
        public bool ThoriumSoul;

        private int[] wetProj = { ProjectileID.Kraken, ProjectileID.Trident, ProjectileID.Flairon, ProjectileID.FlaironBubble, ProjectileID.WaterStream, ProjectileID.WaterBolt, ProjectileID.RainNimbus, ProjectileID.Bubble, ProjectileID.WaterGun };

        #endregion

        //soul effects
        public bool Infinity;
        public int InfinityCounter = 0;

        public bool MagicSoul;
        public bool ThrowSoul;
        public bool RangedSoul;
        public bool RangedEssence;
        public bool BuilderMode;
        public bool UniverseEffect;
        public bool FishSoul1;
        public bool FishSoul2;
        public bool TerrariaSoul;
        public int HealTimer;
        public bool Eternity;
        private float eternityDamage = 0;

        //maso items
        public bool SlimyShield;
        public bool SlimyShieldFalling;
        public bool AgitatingLens;
        public int AgitatingLensCD;
        public bool CorruptHeart;
        public int CorruptHeartCD;
        public bool GuttedHeart;
        public int GuttedHeartCD = 60; //should prevent spawning despite disabled toggle when loading into world
        public bool NecromanticBrew;
        public bool PureHeart;
        public bool PungentEyeballMinion;
        public bool FusedLens;
        public bool GroundStick;
        public bool Probes;
        public bool MagicalBulb;
        public bool SkullCharm;
        public bool PumpkingsCape;
        public bool LihzahrdTreasureBox;
        public int GroundPound;
        public bool BetsysHeart;
        public bool MutantAntibodies;
        public bool GravityGlobeEX;
        public bool CelestialRune;
        public bool AdditionalAttacks;
        public int AdditionalAttacksTimer;
        public bool MoonChalice;
        public bool LunarCultist;
        public bool TrueEyes;
        public bool CyclonicFin;
        public int CyclonicFinCD;
        public bool MasochistSoul;
        public bool CelestialSeal;
        public bool SandsofTime;
        public bool DragonFang;
        public bool SecurityWallet;
        public bool FrigidGemstone;
        public bool WretchedPouch;
        public int FrigidGemstoneCD;
        public bool NymphsPerfume;
        public int NymphsPerfumeCD = 30;
        public bool SqueakyAcc;
        public bool RainbowSlime;
        public bool SkeletronArms;
        public bool SuperFlocko;
        public bool MiniSaucer;
        public bool TribalCharm;
        public bool TribalAutoFire;
        public bool SupremeDeathbringerFairy;

        //debuffs
        private int webCounter = 0;
        public bool Hexed;
        public bool Unstable;
        private int unstableCD = 0;
        public bool Fused;
        public bool Shadowflame;
        public bool Oiled;
        public bool DeathMarked;
        public bool noDodge;
        public bool noSupersonic;
        public bool Bloodthirsty;
        public bool SinisterIcon;

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
        public bool Atrophied;              //melee speed and damage reduced. maybe player cannot fire melee projectiles?
        public bool Jammed;                 //ranged damage and speed reduced, all non-custom ammo set to baseline ammos
        public bool Slimed;
        public byte lightningRodTimer;
        public bool ReverseManaFlow;
        public bool CurseoftheMoon;
        public bool OceanicMaul;
        public int MaxLifeReduction;
        public bool Midas;

        public int MasomodeCrystalTimer = 0;
        public int MasomodeFreezeTimer = 0;
        public int MasomodeSpaceBreathTimer = 0;

        public IList<string> disabledSouls = new List<string>();

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private Mod dbzMod = ModLoader.GetMod("DBZMOD");

        public override TagCompound Save()
        {
            if (Soulcheck.owner == player.name) //to prevent newly made characters from taking the toggles of another char
            {
                string name = "FargoDisabledSouls" + player.name;

                var FargoDisabledSouls = new List<string>();
                foreach (KeyValuePair<string, bool> entry in Soulcheck.ToggleDict)
                {
                    if (!entry.Value)
                    {
                        FargoDisabledSouls.Add(entry.Key);
                    }
                }

                //ErrorLogger.Log(log);

                if (CelestialSeal)
                    FargoDisabledSouls.Add("CelestialSeal");

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

            CelestialSeal = disabledSouls.Contains("CelestialSeal");

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
                    }
                    else
                    {
                        Soulcheck.ToggleDict[buff.Key] = true;
                        Soulcheck.checkboxDict[buff.Key].Color = new Color(81, 181, 113);
                    }
                }
            }

            foreach (KeyValuePair<string, Color> buff in Soulcheck.togglesPets)
            {
                if (Soulcheck.ToggleDict.ContainsKey(buff.Key))
                {
                    if (disabledSouls.Contains(buff.Key))
                    {
                        Soulcheck.ToggleDict[buff.Key] = false;
                        Soulcheck.checkboxDict[buff.Key].Color = Color.Gray;
                    }
                    else
                    {
                        Soulcheck.ToggleDict[buff.Key] = true;
                        Soulcheck.checkboxDict[buff.Key].Color = new Color(81, 181, 113);
                    }
                }
            }

            foreach (KeyValuePair<string, Color> buff in Soulcheck.togglesReforges)
            {
                if (Soulcheck.ToggleDict.ContainsKey(buff.Key))
                {
                    if (disabledSouls.Contains(buff.Key))
                    {
                        Soulcheck.ToggleDict[buff.Key] = false;
                        Soulcheck.checkboxDict[buff.Key].Color = Color.Gray;
                    }
                    else
                    {
                        Soulcheck.ToggleDict[buff.Key] = true;
                        Soulcheck.checkboxDict[buff.Key].Color = new Color(81, 181, 113);
                    }
                }
                else
                {
                    if (disabledSouls.Contains(buff.Key))
                    {
                        Soulcheck.ToggleDict.Add(buff.Key, false);
                        Soulcheck.checkboxDict[buff.Key].Color = Color.Gray;
                    }
                    else
                    {
                        Soulcheck.ToggleDict.Add(buff.Key, true);
                        Soulcheck.checkboxDict[buff.Key].Color = new Color(81, 181, 113);
                    }
                }
            }

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                foreach (KeyValuePair<string, Color> buff in Soulcheck.togglesThorium)
                {
                    if (Soulcheck.ToggleDict.ContainsKey(buff.Key))
                    {
                        if (disabledSouls.Contains(buff.Key))
                        {
                            Soulcheck.ToggleDict[buff.Key] = false;
                            Soulcheck.checkboxDict[buff.Key].Color = Color.Gray;
                        }
                        else
                        {
                            Soulcheck.ToggleDict[buff.Key] = true;
                            Soulcheck.checkboxDict[buff.Key].Color = new Color(81, 181, 113);
                        }
                    }
                }
            }

            if (Fargowiltas.Instance.CalamityLoaded)
            {
                foreach (KeyValuePair<string, Color> buff in Soulcheck.togglesCalamity)
                {
                    if (Soulcheck.ToggleDict.ContainsKey(buff.Key))
                    {
                        if (disabledSouls.Contains(buff.Key))
                        {
                            Soulcheck.ToggleDict[buff.Key] = false;
                            Soulcheck.checkboxDict[buff.Key].Color = Color.Gray;
                        }
                        else
                        {
                            Soulcheck.ToggleDict[buff.Key] = true;
                            Soulcheck.checkboxDict[buff.Key].Color = new Color(81, 181, 113);
                        }
                    }
                }
            }

            Soulcheck.owner = player.name;
            Soulcheck.PlaceBoxes();

            disabledSouls.Clear();

            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].type == NPCID.LunarTowerSolar
                || Main.npc[i].type == NPCID.LunarTowerVortex
                || Main.npc[i].type == NPCID.LunarTowerNebula
                || Main.npc[i].type == NPCID.LunarTowerStardust)
                {
                    if (Main.netMode == 1)
                    {
                        var netMessage = mod.GetPacket();
                        netMessage.Write((byte)1);
                        netMessage.Write((byte)i);
                        netMessage.Send();
                        Main.npc[i].lifeMax *= 5;
                    }
                    else
                    {
                        Main.npc[i].GetGlobalNPC<FargoGlobalNPC>().SetDefaults(Main.npc[i]);
                        Main.npc[i].life = Main.npc[i].lifeMax;
                    }
                }
            }
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
                FreezeCD = 3600; 

                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/ZaWarudo").WithVolume(1f).WithPitchVariance(.5f), player.Center);
            }

            if (Fargowiltas.GoldKey.JustPressed && GoldEnchant && goldCD == 0)
            {
                player.AddBuff(mod.BuffType("GoldenStasis"), 150);
                goldCD = 7350;
                goldHP = player.statLife;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Zhonyas").WithVolume(1f), player.Center);
            }
        }

        public override void ResetEffects()
        {
            if (CelestialSeal)
            {
                player.extraAccessory = true;
                player.extraAccessorySlots = 2;
            }

            AttackSpeed = 1f;

            Wood = false;

            wingTimeModifier = 1f;

            QueenStinger = false;
            Infinity = false;

            BrainMinion = false;
            EaterMinion = false;

            RoombaPet = false;

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
            GoldShell = false;
            CactusEnchant = false;
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
            TungstenEnchant = false;

            MahoganyEnchant = false;
            BorealEnchant = false;
            WoodEnchant = false;
            ShadeEnchant = false;
            SuperBleed = false;

            CosmoForce = false;
            EarthForce = false;
            LifeForce = false;
            NatureForce = false;
            SpiritForce = false;
            TerraForce = false;
            ShadowForce = false;
            WillForce = false;
            WoodForce = false;

            //thorium
            IcyEnchant = false;
            WarlockEnchant = false;
            SacredEnchant = false;
            BinderEnchant = false;
            LivingWoodEnchant = false;
            DepthEnchant = false;
            KnightEnchant = false;
            DreamEnchant = false;
            IllumiteEnchant = false;

            AsgardForce = false;
            MidgardForce = false;
            SvartalfheimForce = false;
            HelheimForce = false;
            VanaheimForce = false;
            MuspelheimForce = false;
            JotunheimForce = false;
            ThoriumSoul = false;

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
            //VoidSoul = false;
            Eternity = false;

            //maso
            SlimyShield = false;
            AgitatingLens = false;
            CorruptHeart = false;
            GuttedHeart = false;
            NecromanticBrew = false;
            PureHeart = false;
            PungentEyeballMinion = false;
            FusedLens = false;
            GroundStick = false;
            Probes = false;
            MagicalBulb = false;
            SkullCharm = false;
            PumpkingsCape = false;
            LihzahrdTreasureBox = false;
            BetsysHeart = false;
            MutantAntibodies = false;
            GravityGlobeEX = false;
            CelestialRune = false;
            AdditionalAttacks = false;
            MoonChalice = false;
            LunarCultist = false;
            TrueEyes = false;
            CyclonicFin = false;
            MasochistSoul = false;
            SandsofTime = false;
            DragonFang = false;
            SecurityWallet = false;
            FrigidGemstone = false;
            WretchedPouch = false;
            NymphsPerfume = false;
            SqueakyAcc = false;
            RainbowSlime = false;
            SkeletronArms = false;
            SuperFlocko = false;
            MiniSaucer = false;
            TribalCharm = false;
            SupremeDeathbringerFairy = false;

            //debuffs
            Hexed = false;
            Unstable = false;
            Fused = false;
            Shadowflame = false;
            Oiled = false;
            Slimed = false;
            noDodge = false;
            noSupersonic = false;
            Bloodthirsty = false;
            SinisterIcon = false;

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
            Atrophied = false;
            Jammed = false;
            ReverseManaFlow = false;
            CurseoftheMoon = false;
            OceanicMaul = false;
            DeathMarked = false;
            Midas = false;
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (Eternity)
                player.respawnTimer = (int)(player.respawnTimer * .1);
            else if (SandsofTime && (!FargoGlobalNPC.AnyBossAlive() || MasochistSoul))
                player.respawnTimer = (int)(player.respawnTimer * .5);
        }

        public override void UpdateDead()
        {
            wingTimeModifier = 1f;

            //debuffs
            Hexed = false;
            Unstable = false;
            unstableCD = 0;
            Fused = false;
            Shadowflame = false;
            Oiled = false;
            Slimed = false;
            noDodge = false;
            noSupersonic = false;
            lightningRodTimer = 0;

            BuilderMode = false;

            SlimyShieldFalling = false;
            CorruptHeartCD = 60;
            GuttedHeartCD = 60;
            NecromanticBrew = false;
            GroundPound = 0;
            NymphsPerfume = false;
            NymphsPerfumeCD = 30;
            PungentEyeballMinion = false;
            MagicalBulb = false;
            LunarCultist = false;
            TrueEyes = false;

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
            Atrophied = false;
            Jammed = false;
            CurseoftheMoon = false;
            OceanicMaul = false;
            DeathMarked = false;
            Midas = false;
            SuperBleed = false;
            Bloodthirsty = false;
            SinisterIcon = false;

            MaxLifeReduction = 0;
        }

        public override void PreUpdate()
        {
            IsStandingStill = Math.Abs(player.velocity.X) < 0.05 && Math.Abs(player.velocity.Y) < 0.05;
            
            player.npcTypeNoAggro[0] = true;

            if (FargoWorld.MasochistMode)
            {
                //falling gives you dazed even with protection. wings save you
                if (player.velocity.Y == 0f && player.wings == 0)
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

                        if (!player.noFallDmg)
                        {
                            player.Hurt(PlayerDeathReason.ByOther(0), dmg, 0);
                        }

                        int buffTime = (int)(dmg * (player.noFallDmg ? .5 : 2));
                        
                        player.AddBuff(BuffID.Dazed, buffTime);
                    }
                    player.fallStart = (int)(player.position.Y / 16f);
                }

                if (player.ZoneUnderworldHeight && !(player.fireWalk || PureHeart))
                    player.AddBuff(BuffID.OnFire, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);

                if (player.ZoneJungle && player.wet && !MutantAntibodies)
                    player.AddBuff(Main.hardMode ? BuffID.Venom : BuffID.Poisoned, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);

                if (player.ZoneSnow && Main.hardMode && !Main.dayTime)
                {
                    if (!PureHeart)
                        player.AddBuff(BuffID.Chilled, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);

                    if (player.wet && !MutantAntibodies)
                    {
                        player.AddBuff(BuffID.Frostburn, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                        MasomodeFreezeTimer++;
                        if (MasomodeFreezeTimer >= 300)
                        {
                            player.AddBuff(BuffID.Frozen, Main.expertMode && Main.expertDebuffTime > 1 ? 60 : 120);
                            MasomodeFreezeTimer = -300;
                        }
                    }
                    else
                    {
                        MasomodeFreezeTimer = 0;
                    }
                }
                else
                {
                    MasomodeFreezeTimer = 0;
                }

                if (player.ZoneCorrupt && Main.hardMode)
                {
                    if (!PureHeart)
                        player.AddBuff(BuffID.Darkness, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                    if(player.wet && !MutantAntibodies)
                        player.AddBuff(BuffID.CursedInferno, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                }

                if (player.ZoneCrimson && Main.hardMode)
                {
                    if (!PureHeart)
                        player.AddBuff(BuffID.Bleeding, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                    if (player.wet && !MutantAntibodies)
                        player.AddBuff(BuffID.Ichor, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                }

                if (player.ZoneHoly && (player.ZoneRockLayerHeight || player.ZoneDirtLayerHeight) && player.active)
                {
                    if (!PureHeart)
                        player.AddBuff(mod.BuffType("FlippedHallow"), 120);
                    if (player.wet && !MutantAntibodies)
                        player.AddBuff(BuffID.Confused, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                }

                if (!PureHeart && Main.raining && !player.ZoneSnow && (player.ZoneOverworldHeight || player.ZoneSkyHeight))
                {
                    Tile currentTile = Framing.GetTileSafely(player.Center);
                    if (currentTile.wall == WallID.None)
                        player.AddBuff(BuffID.Wet, 2);
                }

                if (player.wet && !(player.accFlipper || player.gills || MutantAntibodies))
                    player.AddBuff(mod.BuffType("Lethargic"), 2);

                if (!PureHeart && !player.buffImmune[BuffID.Suffocation] && player.ZoneSkyHeight && player.whoAmI == Main.myPlayer)
                {
                    bool inLiquid = Collision.DrownCollision(player.position, player.width, player.height, player.gravDir);
                    if (!inLiquid)
                        player.breath -= 3;
                    if (++MasomodeSpaceBreathTimer > 10)
                    {
                        MasomodeSpaceBreathTimer = 0;
                        player.breath--;
                    }
                    if (player.breath == 0)
                        Main.PlaySound(23);
                    if (player.breath <= 0)
                        player.AddBuff(BuffID.Suffocation, 2);
                }

                if (!PureHeart && !player.buffImmune[BuffID.Webbed] && player.stickyBreak > 0)
                {
                    Vector2 tileCenter = player.Center;
                    tileCenter.X /= 16;
                    tileCenter.Y /= 16;
                    Tile currentTile = Framing.GetTileSafely((int)tileCenter.X, (int)tileCenter.Y);
                    if (currentTile != null && currentTile.wall == WallID.SpiderUnsafe)
                    {
                        player.AddBuff(BuffID.Webbed, 30);
                        player.stickyBreak = 0;
                        //player.stickyBreak = 1000;
                        Vector2 vector = Collision.StickyTiles(player.position, player.velocity, player.width, player.height);
                        if (vector.X != -1 && vector.Y != -1)
                        {
                            int num3 = (int)vector.X;
                            int num4 = (int)vector.Y;
                            WorldGen.KillTile(num3, num4, false, false, false);
                            if (Main.netMode == 1 && !Main.tile[num3, num4].active())
                                NetMessage.SendData(17, -1, -1, null, 0, num3, num4, 0f, 0, 0, 0);
                        }
                    }
                    /*webCounter++;
                    if (webCounter >= 30 && player.HasBuff(BuffID.Webbed))
                    {
                        player.DelBuff(player.FindBuffIndex(BuffID.Webbed));
                        player.stickyBreak = 0;
                        webCounter = 0;
                    }*/
                }
                if (!SandsofTime)
                {
                    Vector2 tileCenter = player.Center;
                    tileCenter.X /= 16;
                    tileCenter.Y /= 16;
                    Tile currentTile = Framing.GetTileSafely((int)tileCenter.X, (int)tileCenter.Y);
                    if (currentTile != null && currentTile.type == TileID.Cactus && currentTile.nactive())
                    {
                        if (player.hurtCooldowns[0] <= 0) //same i-frames as spike tiles
                            player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " was pricked by a Cactus."), 10, 0, false, false, false, 0);
                    }
                }

                if (MasomodeCrystalTimer > 0)
                    MasomodeCrystalTimer--;
            }

            if (!Infested && !FirstInfection)
                FirstInfection = true;

            if (Eternity && TinCrit < 50)
                TinCrit = 50;
            else if(TerrariaSoul && TinCrit < 25)
                TinCrit = 25;
            else if (TerraForce && TinCrit < 10)
                TinCrit = 10;

            if(OriSpawn && !OriEnchant)
                OriSpawn = false;

            if (VortexStealth && !VortexEnchant)
                VortexStealth = false;

            if (Unstable)
            {
                if (unstableCD == 0)
                {
                    Vector2 pos = player.position;

                    int x = Main.rand.Next((int)pos.X - 500, (int)pos.X + 500);
                    int y = Main.rand.Next((int)pos.Y - 500, (int)pos.Y + 500);
                    Vector2 teleportPos = new Vector2(x, y);

                    while (Collision.SolidCollision(teleportPos, player.width, player.height) && teleportPos.X > 50 && teleportPos.X < (double)(Main.maxTilesX * 16 - 50) && teleportPos.Y > 50 && teleportPos.Y < (double)(Main.maxTilesY * 16 - 50))
                    {
                        x = Main.rand.Next((int)pos.X - 500, (int)pos.X + 500);
                        y = Main.rand.Next((int)pos.Y - 500, (int)pos.Y + 500);
                        teleportPos = new Vector2(x, y);
                    }

                    player.Teleport(teleportPos, 1);
                    NetMessage.SendData(65, -1, -1, null, 0, player.whoAmI, teleportPos.X, teleportPos.Y, 1);

                    unstableCD = 60;
                }
                unstableCD--;
            }

            if (SuperBleed && Main.rand.Next(4) == 0)
            {
                Projectile.NewProjectile(player.position.X + Main.rand.Next(player.width), player.Center.Y + Main.rand.Next(player.height),
                    0f + Main.rand.Next(-5, 5),  Main.rand.Next(-6, -2), mod.ProjectileType("SuperBlood"), 5, 0f, Main.myPlayer);
            }

            if (CopperEnchant && copperCD > 0)
                copperCD--;

            if (GoldEnchant && goldCD > 0)
                goldCD--;

            if (GoldShell)
            {
                player.controlJump = false;
                player.controlDown = false;
                player.controlLeft = false;
                player.controlRight = false;
                player.controlUp = false;
                player.controlUseItem = false;
                player.controlUseTile = false;
                player.controlThrow = false;
                player.gravDir = 1f;

                player.immune = true;
                player.immuneTime = 2;

                //immune to DoT
                if (player.statLife < goldHP)
                    player.statLife = goldHP;

                if (player.ownedProjectileCounts[mod.ProjectileType("GoldShellProj")] <= 0)
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("GoldShellProj"), 0, 0, Main.myPlayer);

                //AddMinion("Chlorophyte Leaf Crystal", mod.ProjectileType("Chlorofuck"), dmg, 10f);
            }

            if (CobaltEnchant && CobaltCD > 0)
                CobaltCD--;

            if (LihzahrdTreasureBox && player.gravDir > 0 && Soulcheck.GetValue("Lihzahrd Ground Pound"))
            {
                if (player.controlDown && !player.mount.Active && !player.controlJump)
                {
                    if (player.velocity.Y != 0f)
                    {
                        if (player.velocity.Y < 15f)
                            player.velocity.Y = 15f;
                        if (GroundPound <= 0)
                            GroundPound = 1;
                    }
                }
                if (GroundPound > 0)
                {
                    if (player.velocity.Y < 0f || player.mount.Active)
                    {
                        GroundPound = 0;
                    }
                    else if (player.velocity.Y == 0f)
                    {
                        if (player.whoAmI == Main.myPlayer)
                        {
                            int x = (int)(player.Center.X) / 16;
                            int y = (int)(player.position.Y + player.height + 8) / 16;
                            if (GroundPound > 15 && x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY
                                && Main.tile[x, y] != null && Main.tile[x, y].nactive() && Main.tileSolid[Main.tile[x, y].type])
                            {
                                int baseDamage = 80;
                                if (MasochistSoul)
                                    baseDamage *= 2;
                                Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("ExplosionSmall"), baseDamage * 2, 12f, player.whoAmI);
                                y -= 2;
                                for (int i = -3; i <= 3; i++)
                                {
                                    if (i == 0)
                                        continue;
                                    int tilePosX = x + 16 * i;
                                    int tilePosY = y;
                                    if (Main.tile[tilePosX, tilePosY] != null && tilePosX >= 0 && tilePosX < Main.maxTilesX)
                                    {
                                        while (Main.tile[tilePosX, tilePosY] != null && tilePosY >= 0 && tilePosY < Main.maxTilesY
                                            && !(Main.tile[tilePosX, tilePosY].nactive() && Main.tileSolid[Main.tile[tilePosX, tilePosY].type]))
                                        {
                                            tilePosY++;
                                        }
                                        Projectile.NewProjectile(tilePosX * 16 + 8, tilePosY * 16 + 8, 0f, -8f, mod.ProjectileType("GeyserFriendly"), baseDamage, 8f, player.whoAmI);
                                    }
                                }
                            }
                        }
                        GroundPound = 0;
                    }
                    else
                    {
                        player.maxFallSpeed = 15f;
                        GroundPound++;
                    }
                }
            }
        }

        public override void PostUpdateMiscEffects()
        {
            if (Atrophied)
            {
                player.meleeSpeed = 0f; //melee silence
                player.thrownVelocity = 0f;
                //just in case
                player.meleeDamage = 0.01f;
                player.meleeCrit = 0;
            }

            if (SlimyShield)
            {
                //player.justJumped use this tbh
                if (SlimyShieldFalling) //landing
                {
                    if (player.velocity.Y < 0f)
                        SlimyShieldFalling = false;

                    if (player.velocity.Y == 0f)
                    {
                        SlimyShieldFalling = false;
                        if (player.whoAmI == Main.myPlayer && player.gravDir > 0 && Soulcheck.GetValue("Slimy Shield Effects"))
                        {
                            Main.PlaySound(SoundID.Item21, player.Center);
                            Vector2 mouse = Main.MouseWorld;
                            int damage = 15;
                            if (SupremeDeathbringerFairy)
                                damage = 25;
                            if (MasochistSoul)
                                damage = 50;
                            damage = (int)(damage * player.meleeDamage);
                            for (int i = 0; i < 3; i++)
                            {
                                Vector2 spawn = new Vector2(mouse.X + Main.rand.Next(-200, 201), mouse.Y - Main.rand.Next(600, 901));
                                Vector2 speed = mouse - spawn;
                                speed.Normalize();
                                speed *= 10f;
                                Projectile.NewProjectile(spawn, speed, mod.ProjectileType("SlimeBall"), damage, 1f, Main.myPlayer);
                            }
                        }
                    }
                }
                else if (player.velocity.Y > 3f)
                {
                    SlimyShieldFalling = true;
                }
            }

            if (AgitatingLens)
            {
                if (AgitatingLensCD++ > 10)
                {
                    AgitatingLensCD = 0;
                    if (player.velocity.Length() >= 6f && player.whoAmI == Main.myPlayer && Soulcheck.GetValue("Scythes When Dashing"))
                    {
                        int damage = 20;
                        if (SupremeDeathbringerFairy)
                            damage = 30;
                        if (MasochistSoul)
                            damage = 60;
                        damage = (int)(damage * player.magicDamage);
                        int proj = Projectile.NewProjectile(player.Center, player.velocity.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-5, 6))) * 0.1f,
                            mod.ProjectileType("BloodScytheFriendly"), damage, 5f, player.whoAmI);
                    }
                }
            }

            if (GuttedHeart)
            {
                //player.statLifeMax2 += player.statLifeMax / 10;
                GuttedHeartCD--;
                if (GuttedHeartCD <= 0)
                {
                    GuttedHeartCD = 900;
                    if (player.whoAmI == Main.myPlayer && Soulcheck.GetValue("Creeper Shield"))
                    {
                        int count = 0;
                        for (int i = 0; i < 200; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == mod.NPCType("CreeperGutted") && Main.npc[i].ai[0] == player.whoAmI)
                                count++;
                        }
                        if (count < 5)
                        {
                            int multiplier = 1;
                            if (PureHeart)
                                multiplier = 2;
                            if (MasochistSoul)
                                multiplier = 5;
                            if (Main.netMode == 0)
                            {
                                int n = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, mod.NPCType("CreeperGutted"), 0, player.whoAmI, 0f, multiplier);
                                if (n != 200)
                                    Main.npc[n].velocity = Vector2.UnitX.RotatedByRandom(2 * Math.PI) * 8;
                            }
                            else if (Main.netMode == 1)
                            {
                                var netMessage = mod.GetPacket();
                                netMessage.Write((byte)0);
                                netMessage.Write((byte)player.whoAmI);
                                netMessage.Write((byte)multiplier);
                                netMessage.Send();
                            }
                        }
                        else
                        {
                            int lowestHealth = -1;
                            for (int i = 0; i < 200; i++)
                            {
                                if (Main.npc[i].active && Main.npc[i].type == mod.NPCType("CreeperGutted") && Main.npc[i].ai[0] == player.whoAmI)
                                {
                                    if (lowestHealth < 0)
                                        lowestHealth = i;
                                    else if (Main.npc[i].life < Main.npc[lowestHealth].life)
                                        lowestHealth = i;
                                }
                            }
                            if (Main.npc[lowestHealth].life < Main.npc[lowestHealth].lifeMax)
                            {
                                if (Main.netMode == 0)
                                {
                                    int damage = Main.npc[lowestHealth].lifeMax - Main.npc[lowestHealth].life;
                                    Main.npc[lowestHealth].life = Main.npc[lowestHealth].lifeMax;
                                    CombatText.NewText(Main.npc[lowestHealth].Hitbox, CombatText.HealLife, damage);
                                }
                                else if (Main.netMode == 1)
                                {
                                    var netMessage = mod.GetPacket();
                                    netMessage.Write((byte)11);
                                    netMessage.Write((byte)player.whoAmI);
                                    netMessage.Write((byte)lowestHealth);
                                    netMessage.Send();
                                }
                            }
                        }
                    }
                }
            }

            //additive with gutted heart
            //if (PureHeart) player.statLifeMax2 += player.statLifeMax / 10;

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
                for (int i = 21; i >= 0; i--)
                {
                    if (player.buffType[i] > 0 && player.buffTime[i] > 0 && !Main.debuff[player.buffType[i]])
                        player.DelBuff(i);
                }
            }
            else if (Asocial)
            {
                KillPets();
                player.maxMinions = 0;
                player.maxTurrets = 0;
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

            if (OceanicMaul)
            {
                player.statDefense -= 60;
                player.endurance = 0;

                if (MaxLifeReduction > player.statLifeMax2 - 100)
                    MaxLifeReduction = player.statLifeMax2 - 100;
                player.statLifeMax2 -= MaxLifeReduction;
                //if (player.statLife > player.statLifeMax2) player.statLife = player.statLifeMax2;
            }
            else
            {
                MaxLifeReduction = 0;
            }

            if (Infinity)
            {
                player.manaCost -= 1f;
            }

            if (Eternity)
                player.statManaMax2 = 999;
            else if (UniverseEffect)
                player.statManaMax2 += 300;

            Item item = player.HeldItem;

            if (TungstenEnchant && Soulcheck.GetValue("Tungsten Effect"))
            {
                if (((item.melee && (item.useStyle == 1 || item.useStyle == 3)) || TerraForce) && item.damage > 0 && item.scale < 2.5f)
                {
                    tungstenPrevSizeSave = item.scale;
                    item.scale = 2.5f;
                }
            }
            else if ((!Soulcheck.GetValue("Tungsten Effect") || !TungstenEnchant) && tungstenPrevSizeSave != -1)
            {
                item.scale = tungstenPrevSizeSave;
            }

            if (AdditionalAttacks && AdditionalAttacksTimer > 0)
                AdditionalAttacksTimer--;

            if (player.whoAmI == Main.myPlayer && player.controlUseItem && player.HeldItem.type == mod.ItemType("EaterLauncher"))
            {

                for (int i = 0; i < 20; i++)
                {
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 300);
                    offset.Y += (float)(Math.Cos(angle) * 300);
                    Dust dust = Main.dust[Dust.NewDust(
                        player.Center + offset - new Vector2(4, 4), 0, 0,
                        DustID.PurpleCrystalShard, 0, 0, 100, Color.White, 1f
                        )];
                    dust.velocity = player.velocity;
                    dust.noGravity = true;

                    Vector2 offset2 = new Vector2();
                    double angle2 = Main.rand.NextDouble() * 2d * Math.PI;
                    offset2.X += (float)(Math.Sin(angle2) * 400);
                    offset2.Y += (float)(Math.Cos(angle2) * 400);
                    Dust dust2 = Main.dust[Dust.NewDust(
                        player.Center + offset2 - new Vector2(4, 4), 0, 0,
                        DustID.PurpleCrystalShard, 0, 0, 100, Color.White, 1f
                        )];
                    dust2.velocity = player.velocity;
                    dust2.noGravity = true;
                }
            }


            if (Fargowiltas.Instance.ThoriumLoaded) ThoriumPostUpdate();
        }

        private void ThoriumPostUpdate()
        {
            if (SpiritForce && player.ownedProjectileCounts[thorium.ProjectileType("SpiritTrapperSpirit")] >= 5)
            {
                player.statLife += 10;
                player.HealEffect(10, true);
                for (int num23 = 0; num23 < 5; num23++)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("SpiritTrapperVisual"), 0, 0f, player.whoAmI, (float)num23, 0f);
                }
                for (int num24 = 0; num24 < 1000; num24++)
                {
                    Projectile projectile3 = Main.projectile[num24];
                    if (projectile3.active && projectile3.type == thorium.ProjectileType("SpiritTrapperSpirit"))
                    {
                        projectile3.Kill();
                    }
                }
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
            int useTime = item.useTime;
            int useAnimate = item.useAnimation;

            if (useTime == 0 || useAnimate == 0 || item.damage <= 0)
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
                    player.lifeRegen = 0;

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

                player.lifeRegenTime = 0;
                player.lifeRegen -= 2;
            }

            if (CurseoftheMoon)
            {
                if (player.lifeRegen > 0)
                    player.lifeRegen = 0;

                if (player.lifeRegenCount > 0)
                    player.lifeRegenCount = 0;

                player.lifeRegenTime = 0;
                player.lifeRegen -= 8;
            }

            if (Oiled && player.lifeRegen < 0)
            {
                player.lifeRegen *= 2;
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
                }
                fullBright = true;
            }

            if (Hexed)
            {
                if (Main.rand.Next(3) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, DustID.BubbleBlock, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 2.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 2f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].color = Color.GreenYellow;
                    Main.playerDrawDust.Add(dust);
                }
                fullBright = true;
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
                }
                fullBright = true;
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
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f) //shadowflame
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, DustID.Shadowflame, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                fullBright = true;
            }

            if (CurseoftheMoon)
            {
                if (Main.rand.Next(5) == 0)
                {
                    int d = Dust.NewDust(player.Center, 0, 0, 229, player.velocity.X * 0.4f, player.velocity.Y * 0.4f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 3f;
                    Main.playerDrawDust.Add(d);
                }
                if (Main.rand.Next(5) == 0)
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, 229, player.velocity.X * 0.4f, player.velocity.Y * 0.4f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity.Y -= 1f;
                    Main.dust[d].velocity *= 2f;
                    Main.playerDrawDust.Add(d);
                }
            }

            if (DeathMarked)
            {
                if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, 109, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default(Color), 1.5f);
                    Main.dust[dust].velocity.Y--;
                    if (Main.rand.Next(3) != 0)
                    {
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale += 0.5f;
                        Main.dust[dust].velocity *= 3f;
                        Main.dust[dust].velocity.Y -= 0.5f;
                    }
                    Main.playerDrawDust.Add(dust);
                }
                r *= 0.2f;
                g *= 0.2f;
                b *= 0.2f;
                fullBright = true;
            }

            if (Fused)
            {
                if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position + new Vector2(player.width / 2, player.height / 5), 0, 0, DustID.Fire, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default(Color), 2f);
                    Main.dust[dust].velocity.Y -= 2f;
                    Main.dust[dust].velocity *= 2f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].scale += 0.5f;
                        Main.dust[dust].noGravity = true;
                    }
                    Main.playerDrawDust.Add(dust);
                }
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

            if (Fargowiltas.Instance.ThoriumLoaded) ThoriumModifyProj(proj, target, damage, crit);
        }

        private void ThoriumModifyProj(Projectile proj, NPC target, int damage, bool crit)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            if (ShroomEnchant && !TerrariaSoul && Main.rand.Next(5) == 0)
                target.AddBuff(thorium.BuffType("Mycelium"), 120);

            if (proj.type == thorium.ProjectileType("MeteorPlasmaDamage") || proj.type == thorium.ProjectileType("PyroBurst") || proj.type == thorium.ProjectileType("LightStrike") || proj.type == thorium.ProjectileType("WhiteFlare") || proj.type == thorium.ProjectileType("CryoDamage") || proj.type == thorium.ProjectileType("MixtapeNote") || proj.type == thorium.ProjectileType("DragonPulse"))
            {
                return;
            }

            if (AsgardForce)
            {
                //tide turner daggers
                if (Soulcheck.GetValue("Tide Turner Daggers") && player.ownedProjectileCounts[thorium.ProjectileType("TideDagger")] < 24 && proj.type != thorium.ProjectileType("ThrowingGuideFollowup") && proj.type != thorium.ProjectileType("TideDagger") && target.type != 488 && Main.rand.Next(5) == 0)
                {
                    FargoGlobalProjectile.XWay(4, player.position, thorium.ProjectileType("TideDagger"), 3, (int)(proj.damage * 0.75), 3);
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 43, 1f, 0f);
                }
                //mini crits
                if (thoriumPlayer.tideOrb > 0 && !crit)
                {
                    float num = 30f;
                    int num2 = 0;
                    while ((float)num2 < num)
                    {
                        Vector2 vector = Vector2.UnitX * 0f;
                        vector += -Utils.RotatedBy(Vector2.UnitY, (double)((float)num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(12f, 12f);
                        vector = Utils.RotatedBy(vector, (double)Utils.ToRotation(target.velocity), default(Vector2));
                        int num3 = Dust.NewDust(target.Center, 0, 0, 176, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num3].scale = 1.5f;
                        Main.dust[num3].noGravity = true;
                        Main.dust[num3].position = target.Center + vector;
                        Main.dust[num3].velocity = target.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                        int num4 = num2;
                        num2 = num4 + 1;
                    }
                    crit = true;
                    damage = (int)((double)damage * 0.75);
                    thoriumPlayer.tideOrb--;
                }
                //assassin duplicate damage
                if (Soulcheck.GetValue("Assassin Damage") && Utils.NextFloat(Main.rand) < 0.1f)
                {
                    Main.PlaySound(2, (int)target.position.X, (int)target.position.Y, 92, 1f, 0f);
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasmaDamage"), (int)((float)proj.damage * 1.15f), 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasma"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                //insta kill
                if (target.type != 488 && target.lifeMax < 100000 && Utils.NextFloat(Main.rand) < 0.05f)
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
                target.AddBuff(24, 300, true);
                target.AddBuff(thorium.BuffType("Singed"), 300, true);

                if (Soulcheck.GetValue("Pyromancer Bursts") && proj.type != thorium.ProjectileType("PyroBurst"))
                {
                    Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("PyroBurst"), 100, 1f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("PyroExplosion2"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
            }

            if (SvartalfheimForce && Soulcheck.GetValue("Bronze Lightning") && Main.rand.Next(5) == 0 && proj.type != thorium.ProjectileType("LightStrike") && proj.type != thorium.ProjectileType("ThrowingGuideFollowup"))
            {
                target.immune[proj.owner] = 5;
                Projectile.NewProjectile(target.Center.X, target.Center.Y - 600f, 0f, 15f, thorium.ProjectileType("LightStrike"), (int)(proj.damage / 4), 1f, proj.owner, 0f, 0f);
            }

            if (VanaheimForce)
            {
                //malignant
                if (crit)
                {
                    target.AddBuff(24, 900, true);
                    target.AddBuff(thorium.BuffType("lightCurse"), 900, true);
                    for (int i = 0; i < 8; i++)
                    {
                        int num5 = Dust.NewDust(target.position, target.width, target.height, 127, (float)Main.rand.Next(-6, 6), (float)Main.rand.Next(-10, 10), 0, default(Color), 1.2f);
                        Main.dust[num5].noGravity = true;
                    }
                    for (int j = 0; j < 8; j++)
                    {
                        int num6 = Dust.NewDust(target.position, target.width, target.height, 65, (float)Main.rand.Next(-6, 6), (float)Main.rand.Next(-10, 10), 0, default(Color), 1.2f);
                        Main.dust[num6].noGravity = true;
                    }
                }
                //white dwarf
                if (Soulcheck.GetValue("White Dwarf Flares") && crit)
                {
                    Main.PlaySound(2, (int)target.position.X, (int)target.position.Y, 92, 1f, 0f);
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("WhiteFlare"), (int)((float)target.lifeMax * 0.001f), 0f, Main.myPlayer, 0f, 0f);
                }
            }

            if (JotunheimForce)
            {
                //yew wood
                if (Soulcheck.GetValue("Yew Wood Crits") && !crit)
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
                        damage = (int)((double)damage * 0.75);
                        thoriumPlayer.yewCharge = 0;
                    }
                }

                //cryo
                if (Soulcheck.GetValue("Cryo-Magus Damage") && proj.type != thorium.ProjectileType("CryoDamage"))
                {
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("ReactionNitrogen"), 0, 5f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("CryoDamage"), proj.damage / 3, 5f, Main.myPlayer, 0f, 0f);
                }
            }

            if (Soulcheck.GetValue("Warlock Wisps") && ThoriumSoul && !(proj.modProjectile is ThoriumProjectile && ((ThoriumProjectile)proj.modProjectile).radiant))
            {
                //warlock
                if (crit && player.ownedProjectileCounts[thorium.ProjectileType("ShadowWisp")] < 15)
                {
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, -2f, thorium.ProjectileType("ShadowWisp"), (int)((float)proj.damage * 0.75f), 0f, Main.myPlayer, 0f, 0f);
                }
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

            if (FirstStrike)
            {
                crit = true;
                damage = (int)(damage * 1.5f);
                player.ClearBuff(mod.BuffType("FirstStrike"));
            }

            if (Fargowiltas.Instance.ThoriumLoaded) ThoriumModifyNPC(target, item, damage, crit);
        }

        private void ThoriumModifyNPC(NPC target, Item item, int damage, bool crit)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            if (ShroomEnchant && !TerrariaSoul && Main.rand.Next(5) == 0)
                target.AddBuff(thorium.BuffType("Mycelium"), 120);

            if (AsgardForce)
            {
                //tide turner daggers
                if (Soulcheck.GetValue("Tide Turner Daggers") && player.ownedProjectileCounts[thorium.ProjectileType("TideDagger")] < 24 && target.type != 488 && Main.rand.Next(5) == 0)
                {
                    FargoGlobalProjectile.XWay(4, player.position, thorium.ProjectileType("TideDagger"), 3, (int)(item.damage * 0.75), 3);
                    Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 43, 1f, 0f);
                }
                //mini crits
                if (thoriumPlayer.tideOrb > 0 && !crit)
                {
                    float num = 30f;
                    int num2 = 0;
                    while ((float)num2 < num)
                    {
                        Vector2 vector = Vector2.UnitX * 0f;
                        vector += -Utils.RotatedBy(Vector2.UnitY, (double)((float)num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(12f, 12f);
                        vector = Utils.RotatedBy(vector, (double)Utils.ToRotation(target.velocity), default(Vector2));
                        int num3 = Dust.NewDust(target.Center, 0, 0, 176, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num3].scale = 1.5f;
                        Main.dust[num3].noGravity = true;
                        Main.dust[num3].position = target.Center + vector;
                        Main.dust[num3].velocity = target.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                        int num4 = num2;
                        num2 = num4 + 1;
                    }
                    crit = true;
                    damage = (int)((double)damage * 0.75);
                    thoriumPlayer.tideOrb--;
                }
                //assassin duplicate damage
                if (Soulcheck.GetValue("Assassin Damage") && Utils.NextFloat(Main.rand) < 0.1f)
                {
                    Main.PlaySound(2, (int)target.position.X, (int)target.position.Y, 92, 1f, 0f);
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasmaDamage"), (int)((float)item.damage * 1.15f), 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasma"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                //insta kill
                if (target.type != 488 && target.lifeMax < 100000 && Utils.NextFloat(Main.rand) < 0.05f)
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
                target.AddBuff(24, 300, true);
                target.AddBuff(thorium.BuffType("Singed"), 300, true);

                if (Soulcheck.GetValue("Pyromancer Bursts"))
                {
                    Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("PyroBurst"), 100, 1f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("PyroExplosion2"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
            }

            if (SvartalfheimForce && Soulcheck.GetValue("Bronze Lightning") && Main.rand.Next(5) == 0)
            {
                target.immune[player.whoAmI] = 5;
                Projectile.NewProjectile(target.Center.X, target.Center.Y - 600f, 0f, 15f, thorium.ProjectileType("LightStrike"), (int)(item.damage / 4), 1f, player.whoAmI, 0f, 0f);
            }

            if (VanaheimForce)
            {
                //malignant
                if (crit)
                {
                    target.AddBuff(24, 900, true);
                    target.AddBuff(thorium.BuffType("lightCurse"), 900, true);
                    for (int i = 0; i < 8; i++)
                    {
                        int num5 = Dust.NewDust(target.position, target.width, target.height, 127, (float)Main.rand.Next(-6, 6), (float)Main.rand.Next(-10, 10), 0, default(Color), 1.2f);
                        Main.dust[num5].noGravity = true;
                    }
                    for (int j = 0; j < 8; j++)
                    {
                        int num6 = Dust.NewDust(target.position, target.width, target.height, 65, (float)Main.rand.Next(-6, 6), (float)Main.rand.Next(-10, 10), 0, default(Color), 1.2f);
                        Main.dust[num6].noGravity = true;
                    }
                }
                //white dwarf
                if (Soulcheck.GetValue("White Dwarf Flares") && crit)
                {
                    Main.PlaySound(2, (int)target.position.X, (int)target.position.Y, 92, 1f, 0f);
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("WhiteFlare"), (int)((float)target.lifeMax * 0.001f), 0f, Main.myPlayer, 0f, 0f);
                }
            }

            if (JotunheimForce)
            {
                //yew wood
                if (Soulcheck.GetValue("Yew Wood Crits") && !crit)
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
                        damage = (int)((double)damage * 0.75);
                        thoriumPlayer.yewCharge = 0;
                    }
                }

                if (Soulcheck.GetValue("Cryo-Magus Damage"))
                {
                    //cryo
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("ReactionNitrogen"), 0, 5f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("CryoDamage"), item.damage / 3, 5f, Main.myPlayer, 0f, 0f);
                }
            }

            if (Soulcheck.GetValue("Warlock Wisps") && ThoriumSoul)
            {
                //warlock
                if (crit && player.ownedProjectileCounts[thorium.ProjectileType("ShadowWisp")] < 15)
                {
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, -2f, thorium.ProjectileType("ShadowWisp"), (int)((float)item.damage * 0.75f), 0f, Main.myPlayer, 0f, 0f);
                }
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
            if (target.friendly)
                return;

            OnHitNPCEither(target, damage, knockback, crit, proj.type);

            if (CosmoForce && !TerrariaSoul && Main.rand.Next(4) == 0)
                target.AddBuff(mod.BuffType("SolarFlare"), 300);

            if (Array.IndexOf(wetProj, proj.type) > -1)
                target.AddBuff(BuffID.Wet, 180, true);

            if (Soulcheck.GetValue("Spectre Orbs") && !target.immortal)
            {
                if (SpectreEnchant && proj.type != ProjectileID.SpectreWrath)
                {
                    SpectreHurt(proj);

                    if (SpiritForce || (crit && Main.rand.Next(5) == 0))
                    {
                        SpectreHeal(target, proj);
                    }
                }
            }

            if (CyclonicFin)
            {
                target.AddBuff(mod.BuffType("OceanicMaul"), 900);
                //target.AddBuff(mod.BuffType("CurseoftheMoon"), 900);

                if (crit && CyclonicFinCD <= 0 && proj.type != mod.ProjectileType("RazorbladeTyphoonFriendly") && Soulcheck.GetValue("Spectral Fishron"))
                {
                    CyclonicFinCD = 360;

                    float screenX = Main.screenPosition.X;
                    if (player.direction < 0)
                        screenX += Main.screenWidth;
                    float screenY = Main.screenPosition.Y;
                    screenY += Main.rand.Next(Main.screenHeight);
                    Vector2 spawn = new Vector2(screenX, screenY);
                    Vector2 vel = target.Center - spawn;
                    vel.Normalize();
                    vel *= 27f;
                    int dam = 150;
                    int damageType;
                    if (proj.melee)
                    {
                        dam = (int)(dam * player.meleeDamage);
                        damageType = 1;
                    }
                    else if (proj.ranged)
                    {
                        dam = (int)(dam * player.rangedDamage);
                        damageType = 2;
                    }
                    else if (proj.magic)
                    {
                        dam = (int)(dam * player.magicDamage);
                        damageType = 3;
                    }
                    else if (proj.minion)
                    {
                        dam = (int)(dam * player.minionDamage);
                        damageType = 4;
                    }
                    else if (proj.thrown)
                    {
                        dam = (int)(dam * player.thrownDamage);
                        damageType = 5;
                    }
                    else
                    {
                        damageType = 0;
                    }
                    Projectile.NewProjectile(spawn, vel, mod.ProjectileType("SpectralFishron"), dam, 10f, proj.owner, target.whoAmI, damageType);
                }
            }

            if (CorruptHeart && CorruptHeartCD <= 0)
            {
                CorruptHeartCD = 60;
                if (proj.type != ProjectileID.TinyEater && Soulcheck.GetValue("Tiny Eaters"))
                {
                    Main.PlaySound(3, (int)player.Center.X, (int)player.Center.Y, 1, 1f, 0.0f);
                    for (int index1 = 0; index1 < 20; ++index1)
                    {
                        int index2 = Dust.NewDust(player.position, player.width, player.height, 184, 0.0f, 0.0f, 0, new Color(), 1f);
                        Dust dust = Main.dust[index2];
                        dust.scale = dust.scale * 1.1f;
                        Main.dust[index2].noGravity = true;
                    }
                    for (int index1 = 0; index1 < 30; ++index1)
                    {
                        int index2 = Dust.NewDust(player.position, player.width, player.height, 184, 0.0f, 0.0f, 0, new Color(), 1f);
                        Dust dust1 = Main.dust[index2];
                        dust1.velocity = dust1.velocity * 2.5f;
                        Dust dust2 = Main.dust[index2];
                        dust2.scale = dust2.scale * 0.8f;
                        Main.dust[index2].noGravity = true;
                    }
                    int num = 2;
                    if (Main.rand.Next(3) == 0)
                        ++num;
                    if (Main.rand.Next(6) == 0)
                        ++num;
                    if (Main.rand.Next(9) == 0)
                        ++num;
                    int dam = PureHeart ? 30 : 12;
                    if (MasochistSoul)
                        dam *= 2;
                    for (int index = 0; index < num; ++index)
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.Next(-35, 36) * 0.02f * 10f,
                            Main.rand.Next(-35, 36) * 0.02f * 10f, ProjectileID.TinyEater, (int)(dam * player.meleeDamage), 1.75f, player.whoAmI);
                }
            }

            if (FrigidGemstone && FrigidGemstoneCD <= 0 && !target.immortal && proj.type != mod.ProjectileType("Shadowfrostfireball"))
            {
                FrigidGemstoneCD = 30;
                float screenX = Main.screenPosition.X;
                if (player.direction < 0)
                    screenX += Main.screenWidth;
                float screenY = Main.screenPosition.Y;
                screenY += Main.rand.Next(Main.screenHeight);
                Vector2 spawn = new Vector2(screenX, screenY);
                Vector2 vel = target.Center - spawn;
                vel.Normalize();
                vel *= 8f;
                int dam = (int)(40 * player.magicDamage);
                if (MasochistSoul)
                    dam *= 2;
                Projectile.NewProjectile(spawn, vel, mod.ProjectileType("Shadowfrostfireball"), dam, 6f, player.whoAmI, target.whoAmI);
            }

            if (Fargowiltas.Instance.ThoriumLoaded) ThoriumHitProj(proj, target, damage, crit);
        }

        private void ThoriumHitProj(Projectile proj, NPC target, int damage, bool crit)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            if (ChloroEnchant && !TerrariaSoul && Main.rand.Next(4) == 0)
            {
                Main.PlaySound(2, (int)proj.position.X, (int)proj.position.Y, 34, 1f, 0f);
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, thorium.ProjectileType("BloomCloud"), 0, 0f, proj.owner, 0f, 0f);
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, thorium.ProjectileType("BloomCloudDamage"), (int)(10f * player.magicDamage), 0f, proj.owner, 0f, 0f);
            }

            if (SpiritForce && Soulcheck.GetValue("Spirit Trapper Wisps"))
            {
                if (target.life < 0 && target.value > 0f)
                {
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, -2f, thorium.ProjectileType("SpiritTrapperSpirit"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                if (target.boss || NPCID.Sets.BossHeadTextures[target.type] > -1)
                {
                    thoriumPlayer.spiritTrapperHit++;
                }
            }

            if (JotunheimForce)
            {
                //tide hunter
                if (Soulcheck.GetValue("Depth Diver Foam") && crit)
                {
                    for (int n = 0; n < 10; n++)
                    {
                        int num10 = Dust.NewDust(target.position, target.width, target.height, 217, (float)Main.rand.Next(-4, 4), (float)Main.rand.Next(-4, 4), 100, default(Color), 1f);
                        Main.dust[num10].noGravity = true;
                        Main.dust[num10].noLight = true;
                    }
                    for (int num11 = 0; num11 < 200; num11++)
                    {
                        NPC npc = Main.npc[num11];
                        if (npc.active && npc.FindBuffIndex(thorium.BuffType("Oozed")) < 0 && !npc.friendly && Vector2.Distance(npc.Center, target.Center) < 80f)
                        {
                            npc.AddBuff(thorium.BuffType("Oozed"), 90, false);
                        }
                    }
                }
            }

            if (MuspelheimForce)
            {
                //feral fure
                if (!ThoriumSoul && crit)
                {
                    for (int m = 0; m < 5; m++)
                    {
                        int num9 = Dust.NewDust(target.position, target.width, target.height, 5, (float)Main.rand.Next(-4, 4), (float)Main.rand.Next(-4, 4), 0, default(Color), 1.8f);
                        Main.dust[num9].noGravity = true;
                    }
                    Main.PlaySound(3, (int)player.position.X, (int)player.position.Y, 6, 1f, 0f);
                    player.AddBuff(thorium.BuffType("AlphaRage"), 300, true);
                }
                //life bloom
                if (target.type != 488 && Main.rand.Next(4) == 0 && thoriumPlayer.lifeBloomMax < 50)
                {
                    for (int l = 0; l < 10; l++)
                    {
                        int num7 = Dust.NewDust(target.position, target.width, target.height, 44, (float)Main.rand.Next(-5, 5), (float)Main.rand.Next(-5, 5), 0, default(Color), 1f);
                        Main.dust[num7].noGravity = true;
                    }
                    int num8 = Main.rand.Next(1, 4);
                    player.statLife += num8;
                    player.HealEffect(num8, true);
                    thoriumPlayer.lifeBloomMax += num8;
                }
            }

            if (proj.type == thorium.ProjectileType("MeteorPlasmaDamage") || proj.type == thorium.ProjectileType("PyroBurst") || proj.type == thorium.ProjectileType("LightStrike") || proj.type == thorium.ProjectileType("WhiteFlare") || proj.type == thorium.ProjectileType("CryoDamage") || proj.type == thorium.ProjectileType("MixtapeNote") || proj.type == thorium.ProjectileType("DragonPulse"))
            {
                return;
            }

            if (ThoriumSoul)
            {
                //mixtape
                if (Soulcheck.GetValue("Mix Tape") && crit && proj.type != thorium.ProjectileType("MixtapeNote"))
                {
                    int num23 = Main.rand.Next(3);
                    Main.PlaySound(2, (int)target.position.X, (int)target.position.Y, 73, 1f, 0f);
                    for (int n = 0; n < 5; n++)
                    {
                        Projectile.NewProjectile(target.Center.X, target.Center.Y, Utils.NextFloat(Main.rand, -5f, 5f), Utils.NextFloat(Main.rand, -5f, 5f), thorium.ProjectileType("MixtapeNote"), (int)((float)proj.damage * 0.25f), 2f, proj.owner, (float)num23, 0f);
                    }
                }
            }
        }

        public void OnHitNPCEither(NPC target, int damage, float knockback, bool crit, int projectile = -1)
        {
            if (CopperEnchant && Soulcheck.GetValue("Copper Lightning") && copperCD == 0)
                CopperEffect(target);

            if (NecroEnchant && necroCD == 0 && Soulcheck.GetValue("Necro Guardian"))
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
                Projectile p = FargoGlobalProjectile.NewProjectileDirectSafe(new Vector2(screenX, screenY), new Vector2(velocityX, velocityY),
                    mod.ProjectileType("DungeonGuardianNecro"), (int)(500 * player.rangedDamage), 0f, player.whoAmI, 0, 120);
                if (p != null)
                {
                    p.penetrate = 1;
                    p.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                }
            }

            if (JungleEnchant && !NatureForce && Main.rand.Next(4) == 0 && !target.immortal)
            {
                player.ManaEffect(5);
                player.statMana += 4;
            }

            if (GladEnchant && Soulcheck.GetValue("Gladiator Rain") && projectile != ProjectileID.JavelinFriendly)
            {
                gladCount++;

                if (gladCount >= 10)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Vector2 spawn = new Vector2(target.Center.X + Main.rand.Next(-150, 151), target.Center.Y - Main.rand.Next(600, 801));
                        Vector2 speed = target.Center - spawn;
                        speed.Normalize();
                        speed *= 15f;
                        int p = Projectile.NewProjectile(spawn, speed, ProjectileID.JavelinFriendly, damage / 2, 1f, Main.myPlayer);
                        Main.projectile[p].tileCollide = false;
                        Main.projectile[p].penetrate = 1;
                    }

                    gladCount = 0;
                }
            }

            if (Eternity)
            {
                if (crit && TinCrit < 100)
                {
                    TinCrit += 10;
                }
                else if (TinCrit >= 100)
                {
                    if (damage / 10 > 0)
                    {
                        player.statLife += damage / 10;
                        player.HealEffect(damage / 10);
                    }

                    if (Soulcheck.GetValue("Eternity Stacking"))
                    {
                        eternityDamage += .1f;
                    }
                }
            }
            else if (TerrariaSoul)
            {
                if (crit && TinCrit < 100)
                {
                    TinCrit += 5;
                }
                else if (TinCrit >= 100)
                {
                    if (HealTimer <= 0 && damage / 25 > 0)
                    {
                        if (!player.moonLeech)
                        {
                            player.statLife += damage / 25;
                            player.HealEffect(damage / 25);
                        }
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
                    TinCrit += 5;
                else
                    TinCrit += 4;
            }

            if (PalladEnchant && palladiumCD == 0 && !target.immortal && !player.moonLeech)
            {
                int heal = damage / 10;
                if (heal > 8)
                    heal = 8;
                else if (heal < 1)
                    heal = 1;
                player.statLife += heal;
                player.HealEffect(heal);
                palladiumCD = 240;
            }

            if (NymphsPerfume && NymphsPerfumeCD <= 0 && !target.immortal && !player.moonLeech)
            {
                NymphsPerfumeCD = 600;
                if (Main.netMode == 0)
                {
                    Item.NewItem(target.Hitbox, ItemID.Heart);
                }
                else if (Main.netMode == 1)
                {
                    var netMessage = mod.GetPacket();
                    netMessage.Write((byte)9);
                    netMessage.Write((byte)target.whoAmI);
                    netMessage.Send();
                }
            }

            if (UniverseEffect)
                target.AddBuff(mod.BuffType("FlamesoftheUniverse"), 240, true);

            if (MasochistSoul)
            {
                if (target.FindBuffIndex(mod.BuffType("Sadism")) < 0 && target.aiStyle != 37)
                {
                    /*target.buffImmune[BuffID.Poisoned] = true;
                    target.buffImmune[BuffID.Venom] = true;
                    target.buffImmune[BuffID.Ichor] = true;
                    target.buffImmune[BuffID.CursedInferno] = true;
                    target.buffImmune[BuffID.BetsysCurse] = true;
                    target.buffImmune[BuffID.Electrified] = true;
                    target.buffImmune[mod.BuffType("OceanicMaul")] = true;
                    target.buffImmune[mod.BuffType("CurseoftheMoon")] = true;
                    target.buffImmune[mod.BuffType("Infested")] = true;
                    target.buffImmune[mod.BuffType("Rotting")] = true;
                    target.buffImmune[mod.BuffType("MutantNibble")] = true;*/

                    target.DelBuff(4);
                    target.buffImmune[mod.BuffType("Sadism")] = false;
                    target.AddBuff(mod.BuffType("Sadism"), 600);
                }
            }
            else
            {
                if (BetsysHeart && crit)
                    target.AddBuff(BuffID.BetsysCurse, 300);

                if (PumpkingsCape && crit)
                    target.AddBuff(mod.BuffType("Rotting"), 300);

                if (QueenStinger)
                    target.AddBuff(SupremeDeathbringerFairy ? BuffID.Venom : BuffID.Poisoned, 120, true);

                if (FusedLens)
                    target.AddBuff(Main.rand.Next(2) == 0 ? BuffID.CursedInferno : BuffID.Ichor, 360);
            }

            if (!TerrariaSoul)
            {
                //full moon
                if (RedEnchant && Soulcheck.GetValue("Red Riding Super Bleed")
                    && Main.rand.Next(5) == 0 && (Main.moonPhase == 0 || WillForce))
                    target.AddBuff(mod.BuffType("SuperBleed"), 240, true);

                if (ShadowEnchant && Main.rand.Next(15) == 0)
                    target.AddBuff(BuffID.Darkness, 600, true);

                if (TikiEnchant)
                    target.AddBuff(mod.BuffType("Infested"), 1800, true);

                if (FrostEnchant)
                    target.AddBuff(BuffID.Frostburn, 300);

                if (ObsidianEnchant)
                    target.AddBuff(BuffID.OnFire, 600);
            }

            if (GroundStick && Main.rand.Next(10) == 0 && Soulcheck.GetValue("Inflict Lightning Rod"))
                target.AddBuff(mod.BuffType("LightningRod"), 300);

            if (LeadEnchant && Main.rand.Next(5) == 0)
                target.AddBuff(mod.BuffType("LeadPoison"), 120);

            if (GoldEnchant)
                target.AddBuff(BuffID.Midas, 120, true);

            if (DragonFang && !target.boss && Main.rand.Next(10) == 0)
            {
                target.velocity.X = 0f;
                target.velocity.Y = 10f;
                target.AddBuff(mod.BuffType("ClippedWings"), 240);
                target.netUpdate = true;
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (target.friendly)
                return;

            OnHitNPCEither(target, damage, knockback, crit);

            if (SpectreEnchant)
            {
                //forced orb spawn reeeee
                float num = 4f;
                float speedX = Main.rand.Next(-100, 101);
                float speedY = Main.rand.Next(-100, 101);
                float num2 = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
                num2 = num / num2;
                speedX *= num2;
                speedY *= num2;
                Projectile p = FargoGlobalProjectile.NewProjectileDirectSafe(target.position, new Vector2(speedX, speedY), ProjectileID.SpectreWrath, damage / 2, 0, player.whoAmI, target.whoAmI);

                if ((SpiritForce || (crit && Main.rand.Next(5) == 0)) && p != null)
                {
                    SpectreHeal(target, p);
                }
            }

            if (SolarEnchant && Main.rand.Next(4) == 0)
                target.AddBuff(mod.BuffType("SolarFlare"), 300);

            if (CyclonicFin)
            {
                target.AddBuff(mod.BuffType("OceanicMaul"), 900);
                //target.AddBuff(mod.BuffType("CurseoftheMoon"), 900);

                if (crit && CyclonicFinCD <= 0 && Soulcheck.GetValue("Spectral Fishron"))
                {
                    CyclonicFinCD = 360;

                    float screenX = Main.screenPosition.X;
                    if (player.direction < 0)
                        screenX += Main.screenWidth;
                    float screenY = Main.screenPosition.Y;
                    screenY += Main.rand.Next(Main.screenHeight);
                    Vector2 spawn = new Vector2(screenX, screenY);
                    Vector2 vel = target.Center - spawn;
                    vel.Normalize();
                    vel *= 27f;
                    int dam = (int)(150 * player.meleeDamage);
                    int damageType = 1;
                    Projectile.NewProjectile(spawn, vel, mod.ProjectileType("SpectralFishron"), dam, 10f, player.whoAmI, target.whoAmI, damageType);
                }
            }

            if (CorruptHeart && CorruptHeartCD <= 0)
            {
                CorruptHeartCD = 60;
                if (Soulcheck.GetValue("Tiny Eaters"))
                {
                    Main.PlaySound(3, (int)player.Center.X, (int)player.Center.Y, 1, 1f, 0.0f);
                    for (int index1 = 0; index1 < 20; ++index1)
                    {
                        int index2 = Dust.NewDust(player.position, player.width, player.height, 184, 0.0f, 0.0f, 0, new Color(), 1f);
                        Dust dust = Main.dust[index2];
                        dust.scale = dust.scale * 1.1f;
                        Main.dust[index2].noGravity = true;
                    }
                    for (int index1 = 0; index1 < 30; ++index1)
                    {
                        int index2 = Dust.NewDust(player.position, player.width, player.height, 184, 0.0f, 0.0f, 0, new Color(), 1f);
                        Dust dust1 = Main.dust[index2];
                        dust1.velocity = dust1.velocity * 2.5f;
                        Dust dust2 = Main.dust[index2];
                        dust2.scale = dust2.scale * 0.8f;
                        Main.dust[index2].noGravity = true;
                    }
                    int num = 2;
                    if (Main.rand.Next(3) == 0)
                        ++num;
                    if (Main.rand.Next(6) == 0)
                        ++num;
                    if (Main.rand.Next(9) == 0)
                        ++num;
                    int dam = PureHeart ? 30 : 12;
                    if (MasochistSoul)
                        dam *= 2;
                    for (int index = 0; index < num; ++index)
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.Next(-35, 36) * 0.02f * 10f,
                            Main.rand.Next(-35, 36) * 0.02f * 10f, ProjectileID.TinyEater, (int)(dam * player.meleeDamage), 1.75f, player.whoAmI);
                }
            }

            if (FrigidGemstone && FrigidGemstoneCD <= 0 && !target.immortal)
            {
                FrigidGemstoneCD = 30;
                float screenX = Main.screenPosition.X;
                if (player.direction < 0)
                    screenX += Main.screenWidth;
                float screenY = Main.screenPosition.Y;
                screenY += Main.rand.Next(Main.screenHeight);
                Vector2 spawn = new Vector2(screenX, screenY);
                Vector2 vel = target.Center - spawn;
                vel.Normalize();
                vel *= 10f;
                int dam = (int)(40 * player.magicDamage);
                Projectile.NewProjectile(spawn, vel, mod.ProjectileType("Shadowfrostfireball"), dam, 6f, player.whoAmI, target.whoAmI);
            }

            if (Fargowiltas.Instance.ThoriumLoaded) ThoriumHitNPC(target, item, crit);
        }

        private void ThoriumHitNPC(NPC target, Item item, bool crit)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            if (ChloroEnchant && !TerrariaSoul && Main.rand.Next(4) == 0)
            {
                Main.PlaySound(2, (int)player.Center.X, (int)player.Center.Y, 34, 1f, 0f);
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, thorium.ProjectileType("BloomCloud"), 0, 0f, player.whoAmI, 0f, 0f);
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, thorium.ProjectileType("BloomCloudDamage"), (int)(10f * player.magicDamage), 0f, player.whoAmI, 0f, 0f);
            }

            if (SpiritForce && Soulcheck.GetValue("Spirit Trapper Wisps"))
            {
                if (target.life < 0 && target.value > 0f)
                {
                    Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, -2f, thorium.ProjectileType("SpiritTrapperSpirit"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                if (target.boss || NPCID.Sets.BossHeadTextures[target.type] > -1)
                {
                    thoriumPlayer.spiritTrapperHit++;
                }
            }

            if (JotunheimForce)
            {
                //tide hunter
                if (Soulcheck.GetValue("Depth Diver Foam") && crit)
                {
                    for (int n = 0; n < 10; n++)
                    {
                        int num10 = Dust.NewDust(target.position, target.width, target.height, 217, (float)Main.rand.Next(-4, 4), (float)Main.rand.Next(-4, 4), 100, default(Color), 1f);
                        Main.dust[num10].noGravity = true;
                        Main.dust[num10].noLight = true;
                    }
                    for (int num11 = 0; num11 < 200; num11++)
                    {
                        NPC npc = Main.npc[num11];
                        if (npc.active && npc.FindBuffIndex(thorium.BuffType("Oozed")) < 0 && !npc.friendly && Vector2.Distance(npc.Center, target.Center) < 80f)
                        {
                            npc.AddBuff(thorium.BuffType("Oozed"), 90, false);
                        }
                    }
                }
            }

            if (MuspelheimForce)
            {
                //feral fure
                if (!ThoriumSoul && crit)
                {
                    for (int m = 0; m < 5; m++)
                    {
                        int num9 = Dust.NewDust(target.position, target.width, target.height, 5, (float)Main.rand.Next(-4, 4), (float)Main.rand.Next(-4, 4), 0, default(Color), 1.8f);
                        Main.dust[num9].noGravity = true;
                    }
                    Main.PlaySound(3, (int)player.position.X, (int)player.position.Y, 6, 1f, 0f);
                    player.AddBuff(thorium.BuffType("AlphaRage"), 300, true);
                }
                //life bloom
                if (target.type != 488 && Main.rand.Next(4) == 0 && thoriumPlayer.lifeBloomMax < 50)
                {
                    for (int l = 0; l < 10; l++)
                    {
                        int num7 = Dust.NewDust(target.position, target.width, target.height, 44, (float)Main.rand.Next(-5, 5), (float)Main.rand.Next(-5, 5), 0, default(Color), 1f);
                        Main.dust[num7].noGravity = true;
                    }
                    int num8 = Main.rand.Next(1, 4);
                    player.statLife += num8;
                    player.HealEffect(num8, true);
                    thoriumPlayer.lifeBloomMax += num8;
                }
            }

            if (ThoriumSoul)
            {
                //mixtape
                if (Soulcheck.GetValue("Mix Tape") && crit)
                {
                    int num23 = Main.rand.Next(3);
                    Main.PlaySound(2, (int)target.position.X, (int)target.position.Y, 73, 1f, 0f);
                    for (int n = 0; n < 5; n++)
                    {
                        Projectile.NewProjectile(target.Center.X, target.Center.Y, Utils.NextFloat(Main.rand, -5f, 5f), Utils.NextFloat(Main.rand, -5f, 5f), thorium.ProjectileType("MixtapeNote"), (int)((float)item.damage * 0.25f), 2f, player.whoAmI, (float)num23, 0f);
                    }
                }
            }
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (ShellHide)
                return false;
            if (QueenStinger && !Main.hardMode && proj.type == ProjectileID.Stinger && !FargoGlobalNPC.BossIsAlive(ref FargoGlobalNPC.beeBoss, NPCID.QueenBee))
                return false;
            return true;
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            //no work
            //lava

            if (damageSource == PlayerDeathReason.ByOther(2))
                player.Hurt(PlayerDeathReason.ByOther(2), 999, 1);

            if (IronGuard && internalTimer > 0 && !player.immune)
            {
                player.immune = true;
                player.immuneTime = player.longInvince ? 60 : 30;
                player.AddBuff(BuffID.ParryDamageBuff, 300);
                return false;
            }

            if (SqueakyAcc && Main.rand.Next(10) == 0)
            {
                Squeak(player.Center);
                damage = 1;
            }

            if (DeathMarked)
            {
                damage *= 3;
            }

            return true;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (JungleEnchant && Soulcheck.GetValue("Jungle Spores"))
            {
                int dmg = NatureForce ? 100 : 30;
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 62);
                Projectile[] projs = FargoGlobalProjectile.XWay(8, player.Center, mod.ProjectileType("SporeBoom"), 5, (int)(dmg * player.magicDamage), 5f);
                Projectile[] projs2 = FargoGlobalProjectile.XWay(8, player.Center, mod.ProjectileType("SporeBoom"), 2.5f, 0, 0f);
            }

            if (ShadeEnchant && Soulcheck.GetValue("Super Blood On Hit"))
            {
                if (player.ZoneCrimson || WoodForce)
                    player.AddBuff(mod.BuffType("SuperBleed"), 300);
                for (int i = 0; i < 10; i++)
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 40, Main.rand.Next(-5, 6), Main.rand.Next(-6, -2), mod.ProjectileType("SuperBlood"), 5, 0f, Main.myPlayer);
            }

            if(TinEnchant)
            {
                if(Eternity)
                {
                    TinCrit = 50;
                    eternityDamage = 0;
                }
                else if (TerrariaSoul && TinCrit != 25)
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

            if (MoonChalice)
            {
                if (Soulcheck.GetValue("Ancient Visions On Hit"))
                {
                    int dam = 50;
                    if (MasochistSoul)
                        dam *= 2;
                    for (int i = 0; i < 5; i++)
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.Next(-10, 11), Main.rand.Next(-10, 11),
                            mod.ProjectileType("AncientVision"), (int)(dam * player.minionDamage), 6f, player.whoAmI);
                }
            }
            else if (CelestialRune && Soulcheck.GetValue("Ancient Visions On Hit"))
            {
                Projectile.NewProjectile(player.Center, new Vector2(0, -10), mod.ProjectileType("AncientVision"),
                    (int)(40 * player.minionDamage), 3f, player.whoAmI);
            }

            if (LihzahrdTreasureBox && Soulcheck.GetValue("Spiky Balls On Hit"))
            {
                int dam = 60;
                if (MasochistSoul)
                    dam *= 2;
                for (int i = 0; i < 9; i++)
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.Next(-10, 11), Main.rand.Next(-10, 11),
                        mod.ProjectileType("LihzahrdSpikyBallFriendly"), (int)(dam * player.meleeDamage), 2f, player.whoAmI);
            }

            if (WretchedPouch && Soulcheck.GetValue("Tentacles On Hit"))
            {
                Vector2 vel = new Vector2(9f, 0f).RotatedByRandom(2 * Math.PI);
                int dam = 30;
                if (MasochistSoul)
                    dam *= 3;
                for (int i = 0; i < 6; i++)
                {
                    Vector2 speed = vel.RotatedBy(2 * Math.PI / 6 * (i + Main.rand.NextDouble() - 0.5));
                    float ai1 = Main.rand.Next(10, 80) * (1f / 1000f);
                    if (Main.rand.Next(2) == 0)
                        ai1 *= -1f;
                    float ai0 = Main.rand.Next(10, 80) * (1f / 1000f);
                    if (Main.rand.Next(2) == 0)
                        ai0 *= -1f;
                    Projectile.NewProjectile(player.Center, speed, mod.ProjectileType("ShadowflameTentacle"),
                        (int)(dam * player.magicDamage), 3.75f, player.whoAmI, ai0, ai1);
                }
            }

            if (Midas)
                player.DropCoins();
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
                Projectile p = FargoGlobalProjectile.NewProjectileDirectSafe(player.Center, Vector2.Zero, mod.ProjectileType("Explosion"), (int)(500 * player.meleeDamage), 0f, Main.myPlayer);
                if (p != null)
                    p.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
            }

            if (player.whoAmI == Main.myPlayer && player.FindBuffIndex(mod.BuffType("Revived")) == -1)
            {
                if(Eternity)
                {
                    player.statLife = player.statLifeMax2;
                    player.HealEffect(player.statLifeMax2);
                    player.immune = true;
                    player.immuneTime = player.longInvince ? 180 : 120;
                    Main.NewText("You've been revived!", 175, 75);
                    player.AddBuff(mod.BuffType("Revived"), 7200);
                    retVal = false;
                }
                else if (TerrariaSoul)
                {
                    player.statLife = 200;
                    player.HealEffect(200);
                    player.immune = true;
                    player.immuneTime = player.longInvince ? 180 : 120;
                    Main.NewText("You've been revived!", 175, 75);
                    player.AddBuff(mod.BuffType("Revived"), 14400);
                    retVal = false;
                }
                else if (FossilEnchant)
                {
                    int heal = SpiritForce ? 100 : 20;
                    player.statLife = heal;
                    player.HealEffect(heal);
                    player.immune = true;
                    player.immuneTime = player.longInvince ? 300 : 200;
                    FossilBones = true;
                    Main.NewText("You've been revived!", 175, 75);
                    player.AddBuff(mod.BuffType("Revived"), 18000);
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

            if ((GodEater || FlamesoftheUniverse || CurseoftheMoon) && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " was annihilated by divine wrath.");
            }

            if (DeathMarked)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " was reaped by the cold hand of death.");
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
            player.wingTimeMax = (int)(player.wingTimeMax * wingTimeModifier);

            if (noDodge)
            {
                player.onHitDodge = false;
                player.shadowDodge = false;
                player.blackBelt = false;
            }
        }

        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (IronGuard)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 10;
            }
        }

        public void InfinityHurt()
        {
            player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " self destructed."), Main.rand.Next(2, 6), 0);
            player.immune = false;
            InfinityCounter = 0;
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
            if(player.ownedProjectileCounts[proj] < 1 && player.whoAmI == Main.myPlayer && Soulcheck.GetValue(toggle))
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, proj, damage, knockback, Main.myPlayer);
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
            float baseVal = (float)(MaxInfestTime - timeLeft) / 120; //change the denominator to adjust max power of DOT
            int modifier = (int)(baseVal * baseVal + 8);

            InfestedDust = baseVal / 10 + 1f;
            if (InfestedDust > 5f)
                InfestedDust = 5f;

            return modifier;
        }

        public void AllDamageUp(float dmg)
        {
            player.magicDamage += dmg;
            player.meleeDamage += dmg;
            player.rangedDamage += dmg;
            player.minionDamage += dmg;
            player.thrownDamage += dmg;

            if (Fargowiltas.Instance.ThoriumLoaded) ThoriumDamage(dmg);

            if (Fargowiltas.Instance.CalamityLoaded) CalamityDamage(dmg);

            if (Fargowiltas.Instance.DBTLoaded) DBTDamage(dmg);
        }

        private void ThoriumDamage(float dmg)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.radiantBoost += dmg;
            thoriumPlayer.symphonicDamage += dmg;
        }

        private void CalamityDamage(float dmg)
        {
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += dmg;
        }

        private void DBTDamage(float dmg)
        {
            DBZMOD.MyPlayer dbtPlayer = player.GetModPlayer<DBZMOD.MyPlayer>(dbzMod);
            dbtPlayer.KiDamage += dmg;
        }

        public void AllCritUp(int crit)
        {
            player.meleeCrit += crit;
            player.rangedCrit += crit;
            player.magicCrit += crit;
            player.thrownCrit += crit;

            if (Fargowiltas.Instance.ThoriumLoaded) ThoriumCrit(crit);

            if (Fargowiltas.Instance.CalamityLoaded) CalamityCrit(crit);

            if (Fargowiltas.Instance.DBTLoaded) DBTCrit(crit);
        }

        private void ThoriumCrit(int crit)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.radiantCrit += crit;
            thoriumPlayer.symphonicCrit += crit;
        }

        private void CalamityCrit(int crit)
        {
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += crit;
        }

        private void DBTCrit(int crit)
        {
            DBZMOD.MyPlayer dbtPlayer = player.GetModPlayer<DBZMOD.MyPlayer>(dbzMod);
            dbtPlayer.kiCrit += crit;
        }

        public void AllCritEquals(int crit)
        {
            player.meleeCrit = crit;
            player.rangedCrit = crit;
            player.magicCrit = crit;
            player.thrownCrit = crit;

            if (Fargowiltas.Instance.ThoriumLoaded) ThoriumCritEquals(crit);

            if (Fargowiltas.Instance.CalamityLoaded) CalamityCritEquals(crit);

            if (Fargowiltas.Instance.DBTLoaded) DBTCritEquals(crit);
        }

        private void ThoriumCritEquals(int crit)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.radiantCrit = crit;
            thoriumPlayer.symphonicCrit = crit;
        }

        private void CalamityCritEquals(int crit)
        {
            //CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit = crit;
        }

        private void DBTCritEquals(int crit)
        {
            //DBZMOD.MyPlayer dbtPlayer = player.GetModPlayer<DBZMOD.MyPlayer>(dbzMod);
            //dbtPlayer.kiCrit = crit;
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
            AddMinion("Chlorophyte Leaf Crystal", mod.ProjectileType("Chlorofuck"), dmg, 10f);
            AddPet("Seedling Pet", hideVisual, BuffID.PetSapling, ProjectileID.Sapling);
        }

        public void CopperEffect(NPC target)
        {
            int dmg = 20;
            int chance = 20;

            if (target.FindBuffIndex(BuffID.Wet) != -1)
            {
                dmg *= 3;
                chance /= 5;
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
                        if (npc.active && npc != target && !npc.HasBuff(mod.BuffType("Shock")) && npc.Distance(target.Center) < closestDist && npc.Distance(target.Center) >= 50)
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

                        Projectile p = FargoGlobalProjectile.NewProjectileDirectSafe(target.Center, velocity, ProjectileID.CultistBossLightningOrbArc, (int)(dmg * player.magicDamage), 0f, player.whoAmI, ai.ToRotation(), ai2);
                        if (p != null)
                        {
                            p.friendly = true;
                            p.hostile = false;
                            p.penetrate = -1;
                            p.timeLeft = 60;
                            p.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                        }
                        target.AddBuff(mod.BuffType("Shock"), 60);
                    }
                    else
                    {
                        break;
                    }

                    target = closestNPC;
                }
            }

            copperCD = 300;
        }

        public void CrimsonEffect(bool hideVisual)
        {
            player.crimsonRegen = true;
            CrimsonEnchant = true;
            AddPet("Face Monster Pet", hideVisual, BuffID.BabyFaceMonster, ProjectileID.BabyFaceMonster);
            AddPet("Crimson Heart Pet", hideVisual, BuffID.CrimsonHeart, ProjectileID.CrimsonHeart);
        }

        public void DarkArtistEffect(bool hideVisual)
        {
            player.setApprenticeT2 = true;
            player.setApprenticeT3 = true;

            //shadow shoot meme
            if (Soulcheck.GetValue("Dark Artist Effect"))
            {
                Item heldItem = player.HeldItem;

                if (darkCD == 0 && heldItem.shoot > 0 && heldItem.damage > 0 && player.controlUseItem && prevPosition != null)
                {
                    if (prevPosition != null)
                    {
                        Vector2 vel = (Main.MouseWorld - prevPosition).SafeNormalize(-Vector2.UnitY);

                        Projectile.NewProjectile(prevPosition, vel * heldItem.shootSpeed, ProjectileID.DD2FlameBurstTowerT3Shot, heldItem.damage / 2, 1, player.whoAmI);

                        for (int i = 0; i < 5; i++)
                        {
                            int dustId = Dust.NewDust(new Vector2(prevPosition.X, prevPosition.Y + 2f), player.width, player.height + 5, DustID.Shadowflame, 0, 0, 100, Color.Black, 2f);
                            Main.dust[dustId].noGravity = true;
                        }
                    }

                    prevPosition = player.position;
                    darkCD = 20;
                }

                if (darkCD > 0)
                {
                    darkCD--;
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
                if (boneCD <= 0 && !player.dead)
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

                        Projectile p = FargoGlobalProjectile.NewProjectileDirectSafe(player.Center, new Vector2(randX, randY), ProjectileID.BoneGloveProj, (int)(dmg * player.thrownDamage), 2, Main.myPlayer);
                        if (p != null)
                            p.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                    }

                    Projectile p2 = FargoGlobalProjectile.NewProjectileDirectSafe(player.Center, Vector2.Zero, ProjectileID.Bone, (int)(dmg * 1.5 * player.thrownDamage), 0f, player.whoAmI);
                    if (p2 != null)
                    {
                        p2.GetGlobalProjectile<FargoGlobalProjectile>().Rotate = true;
                        p2.GetGlobalProjectile<FargoGlobalProjectile>().RotateDist = Main.rand.Next(32, 128);
                        p2.GetGlobalProjectile<FargoGlobalProjectile>().RotateDir = Main.rand.Next(2);
                        p2.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                        p2.noDropItem = true;
                    }

                    boneCD = 20;
                }
                else
                {
                    boneCD--;
                }

                if (!player.immune)
                    FossilBones = false;
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
                    Projectile p = FargoGlobalProjectile.NewProjectileDirectSafe(player.Center, Vector2.Zero, ProjectileID.Blizzard, 0, 0, player.whoAmI);
                    if (p != null)
                    {
                        p.GetGlobalProjectile<FargoGlobalProjectile>().Rotate = true;
                        p.width = 10;
                        p.height = 10;
                        p.timeLeft = 2;
                        icicles[IcicleCount] = p;
                        IcicleCount++;
                    }
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

            if(!Fargowiltas.Instance.ThoriumLoaded)
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
            if (Soulcheck.GetValue("Gold Lucky Coin"))
                player.coins = true;
            //discount card
            player.discount = true;
            //midas
            GoldEnchant = true;

            AddPet("Parrot Pet", hideVisual, BuffID.PetParrot, ProjectileID.Parrot);
        }

        public void HallowEffect(bool hideVisual, int dmg)
        {
            HallowEnchant = true;
            AddMinion("Enchanted Sword Familiar", mod.ProjectileType("HallowSword"), (int)(dmg * player.minionDamage), 0f);

            //reflect proj
            if (Soulcheck.GetValue("Hallowed Shield") && !noDodge)
            {
                const int focusRadius = 50;

                if (Math.Abs(player.velocity.X) < .5f && Math.Abs(player.velocity.Y) < .5f)
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
                    if ((Eternity || Main.rand.Next(5) == 0) && Vector2.Distance(x.Center, player.Center) <= distance)
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

            if (Fargowiltas.Instance.ThoriumLoaded || NatureForce) return;

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
                player.nebulaCD--;
            player.setNebula = true;

            if (TerrariaSoul) return;

            if (player.nebulaLevelDamage == 3 && player.nebulaLevelLife == 3 && player.nebulaLevelMana == 3 && NebulaCounter == 0)
                NebulaCounter = 1200;
            else if(NebulaCounter != 0)
                NebulaCounter--;
        }

        public void NecroEffect(bool hideVisual)
        {
            NecroEnchant = true;

            if (necroCD != 0)
                necroCD--;

            AddPet("Skeletron Pet", hideVisual, BuffID.BabySkeletronHead, ProjectileID.BabySkeletronHead);
        }

        public void NinjaEffect(bool hideVisual)
        {
            NinjaEnchant = true;
            AddPet("Black Cat Pet", hideVisual, BuffID.BlackCat, ProjectileID.BlackCat);
        }

        public void ObsidianEffect()
        {
            player.buffImmune[BuffID.OnFire] = true;
            player.fireWalk = true;

            if (TerraForce)
            {
                player.lavaImmune = true;
            }
            else
            {
                player.lavaMax += 300;
            }
            
            player.armorPenetration += 5;
            player.noFallDmg = true;

            //in lava effects
            if (player.lavaWet)
            {
                player.armorPenetration += 15;
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

                if(Eternity)
                    ballAmt = 30;

                if (player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < ballAmt; i++)
                    {
                        float degree = (360 / ballAmt) * i;
                        Projectile fireball = FargoGlobalProjectile.NewProjectileDirectSafe(player.Center, Vector2.Zero, fireballs[i % 3], (int)(10 * player.magicDamage), 0f, player.whoAmI, 0, degree);
                        if (fireball != null)
                        {
                            fireball.GetGlobalProjectile<FargoGlobalProjectile>().Rotate = true;
                            fireball.GetGlobalProjectile<FargoGlobalProjectile>().RotateDist = 96;
                            fireball.timeLeft = 2;
                            fireball.penetrate = -1;
                            fireball.ignoreWater = true;
                        }
                    }
                }

                OriSpawn = true;
            }
        }

        public void PalladiumEffect()
        {
            //no lifesteal needed here for SoE
            if (Eternity) return;

            if (Soulcheck.GetValue("Palladium Healing"))
            {
                player.onHitRegen = true;
                PalladEnchant = true;

                if (palladiumCD > 0)
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
            player.setHuntressT2 = true;
            player.setHuntressT3 = true;
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
            player.setMonkT2 = true;
            player.setMonkT3 = true;
            //tele through wall until open space on dash into wall
            if (Soulcheck.GetValue("Shinobi Through Walls") && player.dashDelay > 0 && player.mount.Type == -1 && player.velocity.X == 0)
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
                player.shroomiteStealth = true;

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
                Main.player[Main.myPlayer].lifeSteal -= num2 * 5; //original damage

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

            if (Eternity)
            {
                if (eternityDamage > 20000)
                    eternityDamage = 20000;
                AllDamageUp(eternityDamage);
                player.statDefense += (int)(eternityDamage * 100); //10 defense per .1 damage
            }
        }

        public void TitaniumEffect()
        {
            if (Soulcheck.GetValue("Titanium Shadow Dodge"))
            {
                player.onHitDodge = true;
            }
        }

        public void TurtleEffect(bool hideVisual)
        {
            TurtleEnchant = true;
            AddPet("Turtle Pet", hideVisual, BuffID.PetTurtle, ProjectileID.Turtle);
            AddPet("Lizard Pet", hideVisual, BuffID.PetLizard, ProjectileID.PetLizard);

            if (!TerrariaSoul && Soulcheck.GetValue("Turtle Shell Buff") && IsStandingStill && !player.controlUseItem && !noDodge)
                player.AddBuff(mod.BuffType("ShellHide"), 2);
        }

        public void ValhallaEffect(bool hideVisual)
        {
            player.setSquireT2 = true;
            player.setSquireT3 = true;
            //knockback memes
            ValhallaEnchant = true;
            AddPet("Dragon Pet", hideVisual, BuffID.PetDD2Dragon, ProjectileID.DD2PetDragon);
        }

        public void VortexEffect(bool hideVisual)
        {
            //portal spawn
            VortexEnchant = true;
            //stealth memes
            if (Soulcheck.GetValue("Vortex Stealth") && (player.controlDown && player.releaseDown))
            {
                if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15)
                {
                    VortexStealth = !VortexStealth;
                    if(Soulcheck.GetValue("Vortex Voids") && vortexCD == 0 && VortexStealth)
                    {
                        int p = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Void"), 60, 5f, player.whoAmI);

                        Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                        vortexCD = 1200;
                    }
                }
            }

            if(vortexCD != 0)
                vortexCD--;

            if (player.mount.Active)
                VortexStealth = false;

            if (VortexStealth)
            {
                player.moveSpeed *= 0.3f;
                player.aggro -= 1200;
                player.setVortex = true;
                player.stealth = 0f;
            }

            AddPet("Companion Cube Pet", hideVisual, BuffID.CompanionCube, ProjectileID.CompanionCube);
        }

        public void EbonEffect()
        {
            if (!Soulcheck.GetValue("Shadowflame Aura"))
                return;

            int dist = 150;

            if (player.ZoneCorrupt || WoodForce)
                dist *= 2;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.lifeMax > 1 && npc.Distance(player.Center) < dist)
                    npc.AddBuff(BuffID.ShadowFlame, 120);
            }

            for (int i = 0; i < 20; i++)
            {
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * dist);
                offset.Y += (float)(Math.Cos(angle) * dist);
                Dust dust = Main.dust[Dust.NewDust(
                    player.Center + offset - new Vector2(4, 4), 0, 0,
                    DustID.Shadowflame, 0, 0, 100, Color.White, 1f
                    )];
                dust.velocity = player.velocity;
                if (Main.rand.Next(3) == 0)
                    dust.velocity += Vector2.Normalize(offset) * -5f;
                dust.noGravity = true;
            }
        }

        public void PalmEffect()
        {
            if (Soulcheck.GetValue("Palm Tree Sentry") && (player.controlDown && player.releaseDown))
            {
                if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15 && player.ownedProjectileCounts[mod.ProjectileType("PalmTreeSentry")] == 0)
                {
                    Vector2 mouse = Main.MouseWorld;

                    if (player.ownedProjectileCounts[mod.ProjectileType("PalmTreeSentry")] == 0)
                    {
                        Projectile.NewProjectile(mouse.X, mouse.Y - 10, 0f, 0f, mod.ProjectileType("PalmTreeSentry"), WoodForce? 15 : 45, 0f, player.whoAmI);

                        //dust?
                    }
                }
            }
        }

        public void PearlEffect()
        {
            pearlCounter++;

            if (pearlCounter >= 4)
            {
                pearlCounter = 0;
                if (Soulcheck.GetValue("Rainbow Trail") && player.velocity.Length() > 1 && player.whoAmI == Main.myPlayer)
                {
                    int direction = player.velocity.X > 0 ? 1 : -1;
                    int p = Projectile.NewProjectile(player.Center, player.velocity, ProjectileID.RainbowBack, 30, 0, Main.myPlayer);
                    Projectile proj = Main.projectile[p];
                    proj.GetGlobalProjectile<FargoGlobalProjectile>().Rainbow = true;
                    proj.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                }
            }
        }

        public override bool PreItemCheck()
        {
            if (TribalCharm)
            {
                TribalAutoFire = player.HeldItem.autoReuse;
                player.HeldItem.autoReuse = true;
            }
            return true;
        }

        public override void PostItemCheck()
        {
            if (TribalCharm)
            {
                player.HeldItem.autoReuse = TribalAutoFire;
            }
        }

        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (bait.type == mod.ItemType("TruffleWormEX"))
            {
                caughtType = 0;
                bool spawned = false;
                for (int i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].bobber
                        && Main.projectile[i].owner == player.whoAmI && player.whoAmI == Main.myPlayer)
                    {
                        Main.projectile[i].ai[0] = 2f; //cut fishing lines
                        Main.projectile[i].netUpdate = true;

                        if (!spawned && Main.projectile[i].wet && Main.projectile[i].velocity.Y == 0f
                            && FargoWorld.MasochistMode && !NPC.AnyNPCs(NPCID.DukeFishron)) //should spawn boss
                        {
                            spawned = true;
                            if (Main.netMode == 0) //singleplayer
                            {
                                FargoGlobalNPC.spawnFishronEX = true;
                                NPC.NewNPC((int)Main.projectile[i].Center.X, (int)Main.projectile[i].Center.Y + 100,
                                    NPCID.DukeFishron, 0, 0f, 0f, 0f, 0f, player.whoAmI);
                                FargoGlobalNPC.spawnFishronEX = false;
                                Main.NewText("Duke Fishron EX has awoken!", 50, 100, 255);
                            }
                            else if (Main.netMode == 1) //MP, broadcast(?) packet from spawning player's client
                            {
                                var netMessage = mod.GetPacket();
                                netMessage.Write((byte)77);
                                netMessage.Write((byte)player.whoAmI);
                                netMessage.Write((int)Main.projectile[i].Center.X);
                                netMessage.Write((int)Main.projectile[i].Center.Y + 100);
                                netMessage.Send();
                            }
                            else if (Main.netMode == 2)
                            {
                                NetMessage.BroadcastChatMessage(Terraria.Localization.NetworkText.FromLiteral("???????"), Color.White);
                            }
                        }
                    }
                }
                /*if (FargoWorld.MasochistMode && bait.owner == Main.myPlayer && !NPC.AnyNPCs(NPCID.DukeFishron))
                {
                    if (Main.netMode != 1)
                    {
                        Main.NewText("now spawning");
                        FargoGlobalNPC.spawnFishronEX = true;
                        NPC.SpawnOnPlayer(bait.owner, NPCID.DukeFishron);
                        FargoGlobalNPC.spawnFishronEX = false;
                    }
                    else
                    {
                        Main.NewText("other netmode?");
                        NetMessage.SendData(61, -1, -1, null, bait.owner, NPCID.DukeFishron);
                    }
                }*/
                if (spawned)
                {
                    bait.stack--;
                    if (bait.stack <= 0)
                        bait.SetDefaults(0);
                }
            }
        }

    }
}
