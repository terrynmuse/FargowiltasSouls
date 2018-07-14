using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using FargowiltasSouls.Items;
using FargowiltasSouls.NPCs;
using FargowiltasSouls.Projectiles;
using System.Linq;
using System.Collections.Generic;

// why
using Terraria.IO;
using System.IO;

namespace FargowiltasSouls
{
    class Fargowiltas : Mod
    {
        internal static ModHotKey CheckListKey;
        internal static ModHotKey HomeKey;
        internal Soulcheck SoulCheck;
        public UserInterface customResources;

        //loaded
        internal bool blueMagicLoaded;
        internal bool calamityLoaded;
        internal bool terraCompLoaded;
        internal bool thoriumLoaded;

        //stoned (ID 156) is placeholder for modded debuffs
        //add more 156s after the currently existing ones (not at the actual end of array) and then overwrite them in PostSetupContent when adding buffs
        internal static int[] debuffIDs =
        {156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 20, 21, 22, 23, 24, 30, 31, 32, 33, 35, 36, 37, 39, 44, 46, 67, 68, 69, 70, 80, 94, 103, 120, 137, 144, 145, 148, 153, 156, 160, 163, 164, 195, 196, 197
        };

        internal static Fargowiltas instance;

        public Fargowiltas()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };

        }

        public override void Load()
        {
            instance = this;
            CheckListKey = RegisterHotKey("Soul Toggles", "L");
            HomeKey = RegisterHotKey("Teleport Home", "+");

            if (!Main.dedServ)
            {

                customResources = new UserInterface();
                SoulCheck = new Soulcheck();
                Soulcheck.visible = false;
                customResources.SetState(SoulCheck);
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
            "CustomBars: Custom Resource Bar",
            delegate
            {
                if (Soulcheck.visible)
                {
                    //Update CustomBars
                    customResources.Update(Main._drawInterfaceGameTime);
                    SoulCheck.Draw(Main.spriteBatch);
                }
                return true;
            },
            InterfaceScaleType.UI)
            );
        }

        //bool sheet
        public override void PostSetupContent()
        {
            try
            {
                blueMagicLoaded = ModLoader.GetMod("Bluemagic") != null;
                calamityLoaded = ModLoader.GetMod("CalamityMod") != null;
                terraCompLoaded = ModLoader.GetMod("TerraCompilation") != null;
                thoriumLoaded = ModLoader.GetMod("ThoriumMod") != null;

                debuffIDs[0] = this.BuffType("Antisocial");
                debuffIDs[1] = this.BuffType("Atrophied");
                debuffIDs[2] = this.BuffType("Berserked");
                debuffIDs[3] = this.BuffType("Bloodthirsty");
                debuffIDs[4] = this.BuffType("ClippedWings");
                debuffIDs[5] = this.BuffType("Crippled");
                debuffIDs[6] = this.BuffType("Defenseless");
                debuffIDs[7] = this.BuffType("FlamesoftheUniverse");
                debuffIDs[8] = this.BuffType("Flipped");
                debuffIDs[9] = this.BuffType("Fused");
                debuffIDs[10] = this.BuffType("GodEater");
                debuffIDs[11] = this.BuffType("Hexed");
                debuffIDs[12] = this.BuffType("Infested");
                debuffIDs[13] = this.BuffType("Jammed");
                debuffIDs[14] = this.BuffType("Lethargic");
                debuffIDs[15] = this.BuffType("LightningRod");
                debuffIDs[16] = this.BuffType("LivingWasteland");
                debuffIDs[17] = this.BuffType("MarkedforDeath");
                debuffIDs[18] = this.BuffType("MutantNibble");
                debuffIDs[19] = this.BuffType("Purified");
                debuffIDs[20] = this.BuffType("Rotting");
                debuffIDs[21] = this.BuffType("SqueakyToy");
                debuffIDs[22] = this.BuffType("Stunned");
                debuffIDs[23] = this.BuffType("Unstable");

            }
            catch (Exception e)
            {
                ErrorLogger.Log("FargowiltasSouls PostSetupContent Error: " + e.StackTrace + e.Message);
            }

        }

        public override void AddRecipeGroups()
        {
            //drax
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Drax", new int[]
            {
                ItemID.Drax,
                ItemID.PickaxeAxe,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyDrax", group);

            if (Fargowiltas.instance.terraCompLoaded)
            {
                //cobalt
                group = new RecipeGroup(() => Lang.misc[37] + " Cobalt Repeater", new int[]
                {
                ItemID.CobaltRepeater,
                ItemID.PalladiumRepeater,
                ModLoader.GetMod("TerraCompilation").ItemType("CobaltComp"),
                ModLoader.GetMod("TerraCompilation").ItemType("PaladiumComp"),

                });
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltRepeater", group);

                //mythril
                group = new RecipeGroup(() => Lang.misc[37] + " Mythril Repeater", new int[]
                {
                ItemID.MythrilRepeater,
                ItemID.OrichalcumRepeater,
                ModLoader.GetMod("TerraCompilation").ItemType("MythrilComp"),
                ModLoader.GetMod("TerraCompilation").ItemType("OrichalcumComp"),
                });
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilRepeater", group);

                //adamantite
                group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Repeater", new int[]
                {
                ItemID.AdamantiteRepeater,
                ItemID.TitaniumRepeater,
                ModLoader.GetMod("TerraCompilation").ItemType("AdamantiteComp"),
                ModLoader.GetMod("TerraCompilation").ItemType("TitaniumComp"),

                });
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantiteRepeater", group);
            }

            else
            {

                //cobalt
                group = new RecipeGroup(() => Lang.misc[37] + " Cobalt Repeater", new int[]
                {
                ItemID.CobaltRepeater,
                ItemID.PalladiumRepeater,
                });
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltRepeater", group);

                //mythril
                group = new RecipeGroup(() => Lang.misc[37] + " Mythril Repeater", new int[]
                {
                ItemID.MythrilRepeater,
                ItemID.OrichalcumRepeater,
               });
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilRepeater", group);

                //adamantite
                group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Repeater", new int[]
                {
                ItemID.AdamantiteRepeater,
                ItemID.TitaniumRepeater,
                });
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantiteRepeater", group);
            }

            //evil chest
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Chest", new int[]
            {
                ItemID.VampireKnives,
                ItemType("VampireKnivesThrown"),
                ItemID.ScourgeoftheCorruptor,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilChest", group);

            //evil wood
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Wood", new int[]
            {
                ItemID.Ebonwood,
                ItemID.Shadewood,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilWood", group);

            //evilbow
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Bow", new int[]
            {
                ItemID.DemonBow,
                ItemID.TendonBow,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilBow", group);

            //evilgun
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Gun", new int[]
            {
                ItemID.Musket,
                ItemID.TheUndertaker,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilGun", group);

            //silverstaff
            group = new RecipeGroup(() => Lang.misc[37] + " Silver Staff", new int[]
            {
                ItemID.SapphireStaff,
                ItemID.EmeraldStaff,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySilverStaff", group);

            //goldstaff
            group = new RecipeGroup(() => Lang.misc[37] + " Gold Staff", new int[]
            {
                ItemID.RubyStaff,
                ItemID.DiamondStaff,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyGoldStaff", group);

            //evilmagic
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Magic Weapon", new int[]
            {
                ItemID.Vilethorn,
                ItemID.CrimsonRod,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilMagic", group);

            //expertevil
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Expert Drop", new int[]
            {
                ItemID.WormScarf,
                ItemID.BrainOfConfusion,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilExpert", group);

            //evilmimic acc
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Mimic Accessory", new int[]
            {
                ItemID.FleshKnuckles,
                ItemID.PutridScent,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilMimic", group);

            //tier 1 sentry
            group = new RecipeGroup(() => Lang.misc[37] + " Tier 1 Sentry", new int[]
            {
                ItemID.DD2LightningAuraT1Popper,
                ItemID.DD2FlameburstTowerT1Popper,
                ItemID.DD2ExplosiveTrapT1Popper,
                ItemID.DD2BallistraTowerT1Popper,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySentry1", group);

            //tier 3 sentry
            group = new RecipeGroup(() => Lang.misc[37] + " Tier 3 Sentry", new int[]
            {
                ItemID.DD2LightningAuraT3Popper,
                ItemID.DD2FlameburstTowerT3Popper,
                ItemID.DD2ExplosiveTrapT3Popper,
                ItemID.DD2BallistraTowerT3Popper,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySentry", group);

            //anvil HM
            group = new RecipeGroup(() => Lang.misc[37] + " Mythril Anvil", new int[]
            {
                ItemID.MythrilAnvil,
                ItemID.OrichalcumAnvil,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAnvil", group);

            //forge HM
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Forge", new int[]
            {
                ItemID.AdamantiteForge,
                ItemID.TitaniumForge,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyForge", group);

            //any adamantite
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Bar", new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantite", group);

            //shroomite head
            group = new RecipeGroup(() => Lang.misc[37] + " Shroomite Head Piece", new int[]
            {
                ItemID.ShroomiteHeadgear,
                ItemID.ShroomiteMask,
                ItemID.ShroomiteHelmet,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyShroomHead", group);

            //orichalcum head
            group = new RecipeGroup(() => Lang.misc[37] + " Orichalcum Head Piece", new int[]
            {
                ItemID.OrichalcumHeadgear,
                ItemID.OrichalcumMask,
                ItemID.OrichalcumHelmet,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyOriHead", group);

            //palladium head
            group = new RecipeGroup(() => Lang.misc[37] + " Palladium Head Piece", new int[]
            {
                ItemID.PalladiumHeadgear,
                ItemID.PalladiumMask,
                ItemID.PalladiumHelmet,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyPallaHead", group);

            //cobalt head
            group = new RecipeGroup(() => Lang.misc[37] + " Cobalt Head Piece", new int[]
            {
                ItemID.CobaltHelmet,
                ItemID.CobaltHat,
                ItemID.CobaltMask,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltHead", group);

            //mythril head
            group = new RecipeGroup(() => Lang.misc[37] + " Mythril Head Piece", new int[]
            {
                ItemID.MythrilHat,
                ItemID.MythrilHelmet,
                ItemID.MythrilHood,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilHead", group);

            //titanium head
            group = new RecipeGroup(() => Lang.misc[37] + " Titanium Head Piece", new int[]
            {
                ItemID.TitaniumHeadgear,
                ItemID.TitaniumMask,
                ItemID.TitaniumHelmet,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyTitaHead", group);

            //hallowed head
            group = new RecipeGroup(() => Lang.misc[37] + " Hallowed Head Piece", new int[]
            {
                ItemID.HallowedMask,
                ItemID.HallowedHeadgear,
                ItemID.HallowedHelmet,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyHallowHead", group);

            //adamantite head
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Head Piece", new int[]
            {
                ItemID.AdamantiteHelmet,
                ItemID.AdamantiteMask,
                ItemID.AdamantiteHeadgear,

            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamHead", group);

            //chloro head
            group = new RecipeGroup(() => Lang.misc[37] + " Chlorophyte Head Piece", new int[]
            {
                ItemID.ChlorophyteMask,
                ItemID.ChlorophyteHelmet,
                ItemID.ChlorophyteHeadgear,

            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyChloroHead", group);

            //spectre head
            group = new RecipeGroup(() => Lang.misc[37] + " Spectre Head Piece", new int[]
            {
                ItemID.SpectreHood,
                ItemID.SpectreMask,

            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySpectreHead", group);

            //book cases
            group = new RecipeGroup(() => Lang.misc[37] + " Wooden Bookcase", new int[]
            {
                ItemID.Bookcase,
                ItemID.EbonwoodBookcase,
                ItemID.RichMahoganyBookcase,
                ItemID.LivingWoodBookcase,
                ItemID.ShadewoodBookcase,
                ItemID.PalmWoodBookcase,
                ItemID.BorealWoodBookcase,
            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBookcase", group);

            //beetle body
            group = new RecipeGroup(() => Lang.misc[37] + " Beetle Body", new int[]
            {
                ItemID.BeetleShell,
                ItemID.BeetleScaleMail,

            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBeetle", group);

            //phasesabers
            group = new RecipeGroup(() => Lang.misc[37] + " Phasesaber", new int[]
            {
                ItemID.RedPhasesaber,
                ItemID.BluePhasesaber,
                ItemID.GreenPhasesaber,
                ItemID.PurplePhasesaber,
                ItemID.WhitePhasesaber,
                ItemID.YellowPhasesaber,

            });
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyPhasesaber", group);

        }

        public static bool NoInvasion(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.invasion && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime);
        }

        public static bool NoBiome(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            return !player.ZoneJungle && !player.ZoneDungeon && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneHoly && !player.ZoneSnow && !player.ZoneUndergroundDesert;
        }

        public static bool NoZoneAllowWater(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.sky && !spawnInfo.player.ZoneMeteor && !spawnInfo.spiderCave;
        }

        public static bool NoZone(NPCSpawnInfo spawnInfo)
        {
            return NoZoneAllowWater(spawnInfo) && !spawnInfo.water;
        }

        public static bool NormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.playerInTown && NoInvasion(spawnInfo);
        }

        public static bool NoZoneNormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoZone(spawnInfo);
        }

        public static bool NoZoneNormalSpawnAllowWater(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoZoneAllowWater(spawnInfo);
        }

        public static bool NoBiomeNormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoBiome(spawnInfo) && NoZone(spawnInfo);
        }

    }
}
