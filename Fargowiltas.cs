using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
// why

namespace FargowiltasSouls
{
    class Fargowiltas : Mod
    {
        internal static ModHotKey CheckListKey;
        internal static ModHotKey HomeKey;
        internal Soulcheck SoulCheck;
        public UserInterface CustomResources;

        //loaded
        internal bool FargosLoaded;
        internal bool BlueMagicLoaded;
        internal bool CalamityLoaded;
        internal bool TerraCompLoaded;
        internal bool ThoriumLoaded;

        //stoned (ID 156) is placeholder for modded debuffs
        //add more 156s after the currently existing ones (not at the actual end of array) and then overwrite them in PostSetupContent when adding buffs
        internal static int[] DebuffIDs =
        {156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 156, 20, 21, 22, 23, 24, 30, 31, 32, 33, 35, 36, 37, 39, 44, 46, 67, 68, 69, 70, 80, 94, 103, 120, 137, 144, 145, 148, 153, 156, 160, 163, 164, 195, 196, 197
        };

        internal static Fargowiltas Instance;

        public Fargowiltas()
        {
            Properties = new ModProperties
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };

        }

        public override void Load()
        {
            Instance = this;
            CheckListKey = RegisterHotKey("Soul Toggles", "L");
            HomeKey = RegisterHotKey("Teleport Home", "+");

            if (!Main.dedServ)
            {

                CustomResources = new UserInterface();
                SoulCheck = new Soulcheck();
                Soulcheck.Visible = false;
                CustomResources.SetState(SoulCheck);
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
            "CustomBars: Custom Resource Bar",
            delegate
            {
                if (Soulcheck.Visible)
                {
                    //Update CustomBars
                    CustomResources.Update(Main._drawInterfaceGameTime);
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
                FargosLoaded = ModLoader.GetMod("Fargowiltas") != null;
                BlueMagicLoaded = ModLoader.GetMod("Bluemagic") != null;
                CalamityLoaded = ModLoader.GetMod("CalamityMod") != null;
                TerraCompLoaded = ModLoader.GetMod("TerraCompilation") != null;
                ThoriumLoaded = ModLoader.GetMod("ThoriumMod") != null;

                DebuffIDs[0] = BuffType("Antisocial");
                DebuffIDs[1] = BuffType("Atrophied");
                DebuffIDs[2] = BuffType("Berserked");
                DebuffIDs[3] = BuffType("Bloodthirsty");
                DebuffIDs[4] = BuffType("ClippedWings");
                DebuffIDs[5] = BuffType("Crippled");
                DebuffIDs[6] = BuffType("Defenseless");
                DebuffIDs[7] = BuffType("FlamesoftheUniverse");
                DebuffIDs[8] = BuffType("Flipped");
                DebuffIDs[9] = BuffType("Fused");
                DebuffIDs[10] = BuffType("GodEater");
                DebuffIDs[11] = BuffType("Hexed");
                DebuffIDs[12] = BuffType("Infested");
                DebuffIDs[13] = BuffType("Jammed");
                DebuffIDs[14] = BuffType("Lethargic");
                DebuffIDs[15] = BuffType("LightningRod");
                DebuffIDs[16] = BuffType("LivingWasteland");
                DebuffIDs[17] = BuffType("MarkedforDeath");
                DebuffIDs[18] = BuffType("MutantNibble");
                DebuffIDs[19] = BuffType("Purified");
                DebuffIDs[20] = BuffType("Rotting");
                DebuffIDs[21] = BuffType("SqueakyToy");
                DebuffIDs[22] = BuffType("Stunned");
                DebuffIDs[23] = BuffType("Unstable");

            }
            catch (Exception e)
            {
                ErrorLogger.Log("FargowiltasSouls PostSetupContent Error: " + e.StackTrace + e.Message);
            }

        }

        public override void AddRecipeGroups()
        {
            //drax
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Drax", ItemID.Drax, ItemID.PickaxeAxe);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyDrax", group);

            if (Instance.TerraCompLoaded)
            {
                //cobalt
                group = new RecipeGroup(() => Lang.misc[37] + " Cobalt Repeater", ItemID.CobaltRepeater, ItemID.PalladiumRepeater, ModLoader.GetMod("TerraCompilation").ItemType("CobaltComp"), ModLoader.GetMod("TerraCompilation").ItemType("PaladiumComp"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltRepeater", group);

                //mythril
                group = new RecipeGroup(() => Lang.misc[37] + " Mythril Repeater", ItemID.MythrilRepeater, ItemID.OrichalcumRepeater, ModLoader.GetMod("TerraCompilation").ItemType("MythrilComp"), ModLoader.GetMod("TerraCompilation").ItemType("OrichalcumComp"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilRepeater", group);

                //adamantite
                group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Repeater", ItemID.AdamantiteRepeater, ItemID.TitaniumRepeater, ModLoader.GetMod("TerraCompilation").ItemType("AdamantiteComp"), ModLoader.GetMod("TerraCompilation").ItemType("TitaniumComp"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantiteRepeater", group);
            }

            else
            {

                //cobalt
                group = new RecipeGroup(() => Lang.misc[37] + " Cobalt Repeater", ItemID.CobaltRepeater, ItemID.PalladiumRepeater);
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltRepeater", group);

                //mythril
                group = new RecipeGroup(() => Lang.misc[37] + " Mythril Repeater", ItemID.MythrilRepeater, ItemID.OrichalcumRepeater);
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilRepeater", group);

                //adamantite
                group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Repeater", ItemID.AdamantiteRepeater, ItemID.TitaniumRepeater);
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantiteRepeater", group);
            }

            //evil chest
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Chest", ItemID.VampireKnives, ItemType("VampireKnivesThrown"), ItemID.ScourgeoftheCorruptor);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilChest", group);

            //evil wood
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Wood", ItemID.Ebonwood, ItemID.Shadewood);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilWood", group);

            //evilbow
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Bow", ItemID.DemonBow, ItemID.TendonBow);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilBow", group);

            //evilgun
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Gun", ItemID.Musket, ItemID.TheUndertaker);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilGun", group);

            //silverstaff
            group = new RecipeGroup(() => Lang.misc[37] + " Silver Staff", ItemID.SapphireStaff, ItemID.EmeraldStaff);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySilverStaff", group);

            //goldstaff
            group = new RecipeGroup(() => Lang.misc[37] + " Gold Staff", ItemID.RubyStaff, ItemID.DiamondStaff);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyGoldStaff", group);

            //evilmagic
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Magic Weapon", ItemID.Vilethorn, ItemID.CrimsonRod);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilMagic", group);

            //expertevil
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Expert Drop", ItemID.WormScarf, ItemID.BrainOfConfusion);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilExpert", group);

            //evilmimic acc
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Mimic Accessory", ItemID.FleshKnuckles, ItemID.PutridScent);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilMimic", group);

            //tier 1 sentry
            group = new RecipeGroup(() => Lang.misc[37] + " Tier 1 Sentry", ItemID.DD2LightningAuraT1Popper, ItemID.DD2FlameburstTowerT1Popper, ItemID.DD2ExplosiveTrapT1Popper, ItemID.DD2BallistraTowerT1Popper);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySentry1", group);

            //tier 3 sentry
            group = new RecipeGroup(() => Lang.misc[37] + " Tier 3 Sentry", ItemID.DD2LightningAuraT3Popper, ItemID.DD2FlameburstTowerT3Popper, ItemID.DD2ExplosiveTrapT3Popper, ItemID.DD2BallistraTowerT3Popper);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySentry", group);

            //anvil HM
            group = new RecipeGroup(() => Lang.misc[37] + " Mythril Anvil", ItemID.MythrilAnvil, ItemID.OrichalcumAnvil);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAnvil", group);

            //forge HM
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Forge", ItemID.AdamantiteForge, ItemID.TitaniumForge);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyForge", group);

            //any adamantite
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Bar", ItemID.AdamantiteBar, ItemID.TitaniumBar);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantite", group);

            //shroomite head
            group = new RecipeGroup(() => Lang.misc[37] + " Shroomite Head Piece", ItemID.ShroomiteHeadgear, ItemID.ShroomiteMask, ItemID.ShroomiteHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyShroomHead", group);

            //orichalcum head
            group = new RecipeGroup(() => Lang.misc[37] + " Orichalcum Head Piece", ItemID.OrichalcumHeadgear, ItemID.OrichalcumMask, ItemID.OrichalcumHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyOriHead", group);

            //palladium head
            group = new RecipeGroup(() => Lang.misc[37] + " Palladium Head Piece", ItemID.PalladiumHeadgear, ItemID.PalladiumMask, ItemID.PalladiumHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyPallaHead", group);

            //cobalt head
            group = new RecipeGroup(() => Lang.misc[37] + " Cobalt Head Piece", ItemID.CobaltHelmet, ItemID.CobaltHat, ItemID.CobaltMask);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltHead", group);

            //mythril head
            group = new RecipeGroup(() => Lang.misc[37] + " Mythril Head Piece", ItemID.MythrilHat, ItemID.MythrilHelmet, ItemID.MythrilHood);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilHead", group);

            //titanium head
            group = new RecipeGroup(() => Lang.misc[37] + " Titanium Head Piece", ItemID.TitaniumHeadgear, ItemID.TitaniumMask, ItemID.TitaniumHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyTitaHead", group);

            //hallowed head
            group = new RecipeGroup(() => Lang.misc[37] + " Hallowed Head Piece", ItemID.HallowedMask, ItemID.HallowedHeadgear, ItemID.HallowedHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyHallowHead", group);

            //adamantite head
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Head Piece", ItemID.AdamantiteHelmet, ItemID.AdamantiteMask, ItemID.AdamantiteHeadgear);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamHead", group);

            //chloro head
            group = new RecipeGroup(() => Lang.misc[37] + " Chlorophyte Head Piece", ItemID.ChlorophyteMask, ItemID.ChlorophyteHelmet, ItemID.ChlorophyteHeadgear);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyChloroHead", group);

            //spectre head
            group = new RecipeGroup(() => Lang.misc[37] + " Spectre Head Piece", ItemID.SpectreHood, ItemID.SpectreMask);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySpectreHead", group);

            //book cases
            group = new RecipeGroup(() => Lang.misc[37] + " Wooden Bookcase", ItemID.Bookcase, ItemID.EbonwoodBookcase, ItemID.RichMahoganyBookcase, ItemID.LivingWoodBookcase, ItemID.ShadewoodBookcase, ItemID.PalmWoodBookcase, ItemID.BorealWoodBookcase);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBookcase", group);

            //beetle body
            group = new RecipeGroup(() => Lang.misc[37] + " Beetle Body", ItemID.BeetleShell, ItemID.BeetleScaleMail);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBeetle", group);

            //phasesabers
            group = new RecipeGroup(() => Lang.misc[37] + " Phasesaber", ItemID.RedPhasesaber, ItemID.BluePhasesaber, ItemID.GreenPhasesaber, ItemID.PurplePhasesaber, ItemID.WhitePhasesaber, ItemID.YellowPhasesaber);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyPhasesaber", group);

        }

        public static bool NoInvasion(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.invasion && (!Main.pumpkinMoon && !Main.snowMoon || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime);
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
