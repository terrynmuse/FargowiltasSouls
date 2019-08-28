using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using FargowiltasSouls.NPCs;
using FargowiltasSouls.Items.Accessories.Enchantments;
using FargowiltasSouls.Items.Accessories.Forces;

namespace FargowiltasSouls
{
    internal class Fargowiltas : Mod
    {
        internal static ModHotKey CheckListKey;
        internal static ModHotKey FreezeKey;
        internal static ModHotKey GoldKey;
        
        internal static List<int> DebuffIDs;

        internal static Fargowiltas Instance;
        //loaded
        internal bool FargosLoaded;
        internal bool TerraCompLoaded;
        internal bool ThoriumLoaded;
        internal bool BlueMagicLoaded;
        internal bool CalamityLoaded;
        internal bool DBTLoaded;
        internal bool MasomodeEX;

        public UserInterface CustomResources;
        internal Soulcheck SoulCheck;

        internal static readonly Dictionary<int, int> ModProjDict = new Dictionary<int, int>();

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
            if(Language.ActiveCulture == GameCulture.Chinese)
            {
            CheckListKey = RegisterHotKey("灵魂切换菜单", "L");
            FreezeKey = RegisterHotKey("冻结时间", "P");
            GoldKey = RegisterHotKey("金身", "O");
            }
            else
            {
            CheckListKey = RegisterHotKey("Soul Toggles", "L");
            FreezeKey = RegisterHotKey("Freeze Time", "P");
            GoldKey = RegisterHotKey("Turn Gold", "O");
            }
            ModTranslation text = CreateTranslation("WoodHeader");
            text.SetDefault("[i:" + Instance.ItemType<WoodForce>() + "] Force of Wood");
            AddTranslation(text);
            ModTranslation borealtrans = CreateTranslation("BorealConfig");
            borealtrans.SetDefault("[i:" + Instance.ItemType<BorealWoodEnchant>() + "][c/8B7464: Boreal Snowballs]");
            AddTranslation(borealtrans);
            text = CreateTranslation("MahoganyConfig");
            text.SetDefault("[i:" + Instance.ItemType<RichMahoganyEnchant>() + "][c/b56c64: Mahogany Hook Speed]");
            AddTranslation(text);
            text = CreateTranslation("EbonConfig");
            text.SetDefault("[i:" + Instance.ItemType<EbonwoodEnchant>() + "][c/645a8d: Ebonwood Shadowflame]");
            AddTranslation(text);
            text = CreateTranslation("PalmConfig");
            text.SetDefault("[i:" + Instance.ItemType<PalmWoodEnchant>() + "][c/b78d56: Palmwood Sentry]");
            AddTranslation(text);
            text = CreateTranslation("PearlConfig");
            text.SetDefault("[i:" + Instance.ItemType<PearlwoodEnchant>() + "][c/ad9a5f: Pearlwood Rainbow]");
            AddTranslation(text);
            text = CreateTranslation("EarthHeader");
            text.SetDefault("[i:" + Instance.ItemType<EarthForce>() + "] Force of Earth");
            AddTranslation(text);
            text = CreateTranslation("AdamantiteConfig");
            text.SetDefault("[i:" + Instance.ItemType<AdamantiteEnchant>() + "][c/dd557d: Adamantite Projectile Splitting]");
            AddTranslation(text);
            text = CreateTranslation("CobaltConfig");
            text.SetDefault("[i:" + Instance.ItemType<CobaltEnchant>() + "][c/3da4c4: Cobalt Shards]");
            AddTranslation(text);
            text = CreateTranslation("MythrilConfig");
            text.SetDefault("[i:" + Instance.ItemType<MythrilEnchant>() + "][c/9dd290: Mythril Weapon Speed]");
            AddTranslation(text);
            text = CreateTranslation("OrichalcumConfig");
            text.SetDefault("[i:" + Instance.ItemType<OrichalcumEnchant>() + "][c/eb3291: Orichalcum Fireballs]");
            AddTranslation(text);
            text = CreateTranslation("PalladiumConfig");
            text.SetDefault("[i:" + Instance.ItemType<PalladiumEnchant>() + "][c/f5ac28: Palladium Healing]");
            AddTranslation(text);
            text = CreateTranslation("TitaniumConfig");
            text.SetDefault("[i:" + Instance.ItemType<TitaniumEnchant>() + "][c/828c88: Titanium Shadow Dodge]");
            AddTranslation(text);
            if (!Main.dedServ)
            {
                CustomResources = new UserInterface();
                SoulCheck = new Soulcheck();
                Soulcheck.Visible = false;
                CustomResources.SetState(SoulCheck);
            }
        }

        public override void Unload()
        {
            Soulcheck.ToggleDict.Clear();
            Soulcheck.checkboxDict.Clear();
            DebuffIDs.Clear();
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
                DBTLoaded = ModLoader.GetMod("DBZMOD") != null;
                MasomodeEX = ModLoader.GetMod("MasomodeEX") != null;

                DebuffIDs = new List<int> { 20, 22, 23, 24, 36, 39, 44, 46, 47, 67, 68, 69, 70, 80,
                    88, 94, 103, 137, 144, 145, 148, 149, 153, 156, 160, 163, 164, 195, 196, 197, 199 };
                DebuffIDs.Add(BuffType("Antisocial"));
                DebuffIDs.Add(BuffType("Atrophied"));
                DebuffIDs.Add(BuffType("Berserked"));
                DebuffIDs.Add(BuffType("Bloodthirsty"));
                DebuffIDs.Add(BuffType("ClippedWings"));
                DebuffIDs.Add(BuffType("Crippled"));
                DebuffIDs.Add(BuffType("CurseoftheMoon"));
                DebuffIDs.Add(BuffType("Defenseless"));
                DebuffIDs.Add(BuffType("FlamesoftheUniverse"));
                DebuffIDs.Add(BuffType("Flipped"));
                DebuffIDs.Add(BuffType("FlippedHallow"));
                DebuffIDs.Add(BuffType("Fused"));
                DebuffIDs.Add(BuffType("GodEater"));
                DebuffIDs.Add(BuffType("Guilty"));
                DebuffIDs.Add(BuffType("Hexed"));
                DebuffIDs.Add(BuffType("Infested"));
                DebuffIDs.Add(BuffType("Jammed"));
                DebuffIDs.Add(BuffType("Lethargic"));
                DebuffIDs.Add(BuffType("LightningRod"));
                DebuffIDs.Add(BuffType("LivingWasteland"));
                DebuffIDs.Add(BuffType("MarkedforDeath"));
                DebuffIDs.Add(BuffType("Midas"));
                DebuffIDs.Add(BuffType("MutantNibble"));
                DebuffIDs.Add(BuffType("NullificationCurse"));
                DebuffIDs.Add(BuffType("Oiled"));
                DebuffIDs.Add(BuffType("OceanicMaul"));
                DebuffIDs.Add(BuffType("Purified"));
                DebuffIDs.Add(BuffType("ReverseManaFlow"));
                DebuffIDs.Add(BuffType("Rotting"));
                DebuffIDs.Add(BuffType("SqueakyToy"));
                DebuffIDs.Add(BuffType("Stunned"));
                DebuffIDs.Add(BuffType("Unstable"));

                Mod bossChecklist = ModLoader.GetMod("BossChecklist");
                if (bossChecklist != null)
                {
                    bossChecklist.Call("AddBossWithInfo", "Duke Fishron EX", 14.1f, (Func<bool>)(() => FargoSoulsWorld.downedFishronEX), "Fish using a [i:" + ItemType("TruffleWormEX") + "]");
                    bossChecklist.Call("AddBossWithInfo", "Mutant", 14.2f, (Func<bool>)(() => FargoSoulsWorld.downedMutant), "Spawn by throwing [i:" + ItemType("AbominationnVoodooDoll") + "] in lava in Mutant's presence");
                }

                if (ThoriumLoaded)
                {
                    Mod thorium = ModLoader.GetMod("ThoriumMod");
                    ModProjDict.Add(thorium.ProjectileType("IFO"), 1);
                    ModProjDict.Add(thorium.ProjectileType("BioFeederPet"), 2);
                    ModProjDict.Add(thorium.ProjectileType("BlisterPet"), 3);
                    ModProjDict.Add(thorium.ProjectileType("WyvernPet"), 4);
                    ModProjDict.Add(thorium.ProjectileType("SupportLantern"), 5);
                    ModProjDict.Add(thorium.ProjectileType("LockBoxPet"), 6);
                    ModProjDict.Add(thorium.ProjectileType("Devil"), 7);
                    ModProjDict.Add(thorium.ProjectileType("Angel"), 8);
                    ModProjDict.Add(thorium.ProjectileType("LifeSpirit"), 9);
                    ModProjDict.Add(thorium.ProjectileType("HolyGoat"), 10);
                    ModProjDict.Add(thorium.ProjectileType("MinionSapling"), 11);
                    ModProjDict.Add(thorium.ProjectileType("SnowyOwlPet"), 12);
                    ModProjDict.Add(thorium.ProjectileType("JellyfishPet"), 13);
                    ModProjDict.Add(thorium.ProjectileType("LilMog"), 14);
                    ModProjDict.Add(thorium.ProjectileType("Maid1"), 15);
                    ModProjDict.Add(thorium.ProjectileType("PinkSlime"), 16);
                    ModProjDict.Add(thorium.ProjectileType("ShinyPet"), 17);
                    ModProjDict.Add(thorium.ProjectileType("DrachmaBag"), 18);
                }
            }
            catch (Exception e)
            {
                ErrorLogger.Log("FargowiltasSouls PostSetupContent Error: " + e.StackTrace + e.Message);
            }
        }

        public override void AddRecipes()
        {
            if (ThoriumLoaded)
            {
                Mod thorium = ModLoader.GetMod("ThoriumMod");
                ModRecipe recipe = new ModRecipe(this);

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelArrow"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelAxe"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelBattleAxe"), 10);
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelBlade"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelBow"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelChestplate"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelGreaves"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelHelmet"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelMallet"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelPickaxe"));
                recipe.AddRecipe();
            }
        }

        public override void AddRecipeGroups()
        {
            //drax
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Drax", ItemID.Drax, ItemID.PickaxeAxe);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyDrax", group);

            //does this mod even exist anymore tbh
            if (Instance.TerraCompLoaded)
            {
                //cobalt
                group = new RecipeGroup(() => Lang.misc[37] + " Cobalt Repeater", ItemID.CobaltRepeater, ItemID.PalladiumRepeater,
                    ModLoader.GetMod("TerraCompilation").ItemType("CobaltComp"), ModLoader.GetMod("TerraCompilation").ItemType("PaladiumComp"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltRepeater", group);

                //mythril
                group = new RecipeGroup(() => Lang.misc[37] + " Mythril Repeater", ItemID.MythrilRepeater, ItemID.OrichalcumRepeater,
                    ModLoader.GetMod("TerraCompilation").ItemType("MythrilComp"), ModLoader.GetMod("TerraCompilation").ItemType("OrichalcumComp"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilRepeater", group);

                //adamantite
                group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Repeater", ItemID.AdamantiteRepeater, ItemID.TitaniumRepeater,
                    ModLoader.GetMod("TerraCompilation").ItemType("AdamantiteComp"), ModLoader.GetMod("TerraCompilation").ItemType("TitaniumComp"));
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

            if (Instance.ThoriumLoaded)
            {
                Mod thorium = ModLoader.GetMod("ThoriumMod");

                //combo yoyos
                group = new RecipeGroup(() => Lang.misc[37] + " Combination Yoyo", thorium.ItemType("Nocturnal"), thorium.ItemType("Sanguine"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyThoriumYoyo", group);
            }

            //evil wood
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Wood", ItemID.Ebonwood, ItemID.Shadewood);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilWood", group);

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
            group = new RecipeGroup(() => Lang.misc[37] + " Wooden Bookcase", ItemID.Bookcase, ItemID.EbonwoodBookcase, ItemID.RichMahoganyBookcase, ItemID.LivingWoodBookcase,
                ItemID.ShadewoodBookcase, ItemID.PalmWoodBookcase, ItemID.BorealWoodBookcase);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBookcase", group);

            //beetle body
            group = new RecipeGroup(() => Lang.misc[37] + " Beetle Body", ItemID.BeetleShell, ItemID.BeetleScaleMail);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBeetle", group);

            //phasesabers
            group = new RecipeGroup(() => Lang.misc[37] + " Phasesaber", ItemID.RedPhasesaber, ItemID.BluePhasesaber, ItemID.GreenPhasesaber, ItemID.PurplePhasesaber, ItemID.WhitePhasesaber,
                ItemID.YellowPhasesaber);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyPhasesaber", group);

            //vanilla butterflies
            group = new RecipeGroup(() => Lang.misc[37] + " Butterfly", ItemID.JuliaButterfly, ItemID.MonarchButterfly, ItemID.PurpleEmperorButterfly,
                ItemID.RedAdmiralButterfly, ItemID.SulphurButterfly, ItemID.TreeNymphButterfly, ItemID.UlyssesButterfly, ItemID.ZebraSwallowtailButterfly);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyButterfly", group);

            if (ThoriumLoaded)
            {
                Mod thorium = ModLoader.GetMod("ThoriumMod");

                //jester mask
                group = new RecipeGroup(() => Lang.misc[37] + " Jester Mask", thorium.ItemType("JestersMask"), thorium.ItemType("JestersMask2"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyJesterMask", group);
                //jester shirt
                group = new RecipeGroup(() => Lang.misc[37] + " Jester Shirt", thorium.ItemType("JestersShirt"), thorium.ItemType("JestersShirt2"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyJesterShirt", group);
                //jester legging
                group = new RecipeGroup(() => Lang.misc[37] + " Jester Leggings", thorium.ItemType("JestersLeggings"), thorium.ItemType("JestersLeggings2"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyJesterLeggings", group);
                //evil wood tambourine
                group = new RecipeGroup(() => Lang.misc[37] + " Evil Wood Tambourine", thorium.ItemType("EbonWoodTambourine"), thorium.ItemType("ShadeWoodTambourine"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyTambourine", group);
                //fan letter
                group = new RecipeGroup(() => Lang.misc[37] + " Fan Letter", thorium.ItemType("FanLetter"), thorium.ItemType("FanLetter2"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyLetter", group);

                //butterflies
                group = new RecipeGroup(() => Lang.misc[37] + " Dungeon Butterfly", thorium.ItemType("BlueDungeonButterfly"), thorium.ItemType("GreenDungeonButterfly"), thorium.ItemType("PinkDungeonButterfly"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyDungeonButterfly", group);
            }
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            switch (reader.ReadByte())
            {
                case 0: //server side spawning creepers
                    if (Main.netMode == 2)
                    {
                        byte p = reader.ReadByte();
                        int multiplier = reader.ReadByte();
                        int n = NPC.NewNPC((int)Main.player[p].Center.X, (int)Main.player[p].Center.Y, NPCType("CreeperGutted"), 0,
                            p, 0f, multiplier, 0f);
                        if (n != 200)
                        {
                            Main.npc[n].velocity = Vector2.UnitX.RotatedByRandom(2 * Math.PI) * 8;
                            NetMessage.SendData(23, -1, -1, null, n);
                        }
                    }
                    break;

                case 1: //server side synchronize pillar data request
                    if (Main.netMode == 2)
                    {
                        byte pillar = reader.ReadByte();
                        if (!Main.npc[pillar].GetGlobalNPC<FargoSoulsGlobalNPC>().masoBool[1])
                        {
                            Main.npc[pillar].GetGlobalNPC<FargoSoulsGlobalNPC>().masoBool[1] = true;
                            Main.npc[pillar].GetGlobalNPC<FargoSoulsGlobalNPC>().SetDefaults(Main.npc[pillar]);
                            Main.npc[pillar].life = Main.npc[pillar].lifeMax;
                        }
                    }
                    break;

                case 2: //net updating maso
                    FargoSoulsGlobalNPC fargoNPC = Main.npc[reader.ReadByte()].GetGlobalNPC<FargoSoulsGlobalNPC>();
                    fargoNPC.masoBool[0] = reader.ReadBoolean();
                    fargoNPC.masoBool[1] = reader.ReadBoolean();
                    fargoNPC.masoBool[2] = reader.ReadBoolean();
                    fargoNPC.masoBool[3] = reader.ReadBoolean();
                    break;

                case 3: //rainbow slime/paladin, MP clients syncing to server
                    if (Main.netMode == 1)
                    {
                        byte npc = reader.ReadByte();
                        Main.npc[npc].lifeMax = reader.ReadInt32();
                        float newScale = reader.ReadSingle();
                        Main.npc[npc].position = Main.npc[npc].Center;
                        Main.npc[npc].width = (int)(Main.npc[npc].width / Main.npc[npc].scale * newScale);
                        Main.npc[npc].height = (int)(Main.npc[npc].height / Main.npc[npc].scale * newScale);
                        Main.npc[npc].scale = newScale;
                        Main.npc[npc].Center = Main.npc[npc].position;
                    }
                    break;

                case 4: //moon lord vulnerability synchronization
                    if (Main.netMode == 1)
                    {
                        int ML = reader.ReadByte();
                        Main.npc[ML].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter = reader.ReadInt32();
                        FargoSoulsGlobalNPC.masoStateML = reader.ReadByte();
                    }
                    break;

                case 5: //retinazer laser MP sync
                    if (Main.netMode == 1)
                    {
                        int reti = reader.ReadByte();
                        Main.npc[reti].GetGlobalNPC<FargoSoulsGlobalNPC>().masoBool[2] = reader.ReadBoolean();
                        Main.npc[reti].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter = reader.ReadInt32();
                    }
                    break;

                case 6: //shark MP sync
                    if (Main.netMode == 1)
                    {
                        int shark = reader.ReadByte();
                        Main.npc[shark].GetGlobalNPC<FargoSoulsGlobalNPC>().SharkCount = reader.ReadByte();
                    }
                    break;

                case 7: //client to server activate dark caster family
                    if (Main.netMode == 2)
                    {
                        int caster = reader.ReadByte();
                        if (Main.npc[caster].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter2 == 0)
                            Main.npc[caster].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter2 = reader.ReadInt32();
                    }
                    break;

                case 8: //server to clients reset counter
                    if (Main.netMode == 1)
                    {
                        int caster = reader.ReadByte();
                        Main.npc[caster].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter2 = 0;
                    }
                    break;

                case 9: //client to server, request heart spawn
                    if (Main.netMode == 2)
                    {
                        int n = reader.ReadByte();
                        Item.NewItem(Main.npc[n].Hitbox, ItemID.Heart);
                    }
                    break;

                case 10: //client to server, sync cultist data
                    if (Main.netMode == 2)
                    {
                        int cult = reader.ReadByte();
                        FargoSoulsGlobalNPC cultNPC = Main.npc[cult].GetGlobalNPC<FargoSoulsGlobalNPC>();
                        cultNPC.Counter += reader.ReadInt32();
                        cultNPC.Counter2 += reader.ReadInt32();
                        cultNPC.Timer += reader.ReadInt32();
                        Main.npc[cult].localAI[3] += reader.ReadSingle();
                    }
                    break;

                case 11: //refresh creeper
                    if (Main.netMode != 0)
                    {
                        byte player = reader.ReadByte();
                        NPC creeper = Main.npc[reader.ReadByte()];
                        if (creeper.active && creeper.type == NPCType("CreeperGutted") && creeper.ai[0] == player)
                        {
                            int damage = creeper.lifeMax - creeper.life;
                            creeper.life = creeper.lifeMax;
                            if (damage > 0)
                                CombatText.NewText(creeper.Hitbox, CombatText.HealLife, damage);
                            if (Main.netMode == 2)
                                creeper.netUpdate = true;
                        }
                    }
                    break;

                case 77: //server side spawning fishron EX
                    if (Main.netMode == 2)
                    {
                        byte target = reader.ReadByte();
                        int x = reader.ReadInt32();
                        int y = reader.ReadInt32();
                        FargoSoulsGlobalNPC.spawnFishronEX = true;
                        NPC.NewNPC(x, y, NPCID.DukeFishron, 0, 0f, 0f, 0f, 0f, target);
                        FargoSoulsGlobalNPC.spawnFishronEX = false;
                        NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("Duke Fishron EX has awoken!"), new Color(50, 100, 255));
                    }
                    break;

                case 78: //confirming fish EX max life
                    int f = reader.ReadInt32();
                    Main.npc[f].lifeMax = reader.ReadInt32();
                    break;

                default:
                    break;
            }

            //BaseMod Stuff
            MsgType msg = (MsgType)reader.ReadByte();
            if (msg == MsgType.ProjectileHostility) //projectile hostility and ownership
            {
                int owner = reader.ReadInt32();
                int projID = reader.ReadInt32();
                bool friendly = reader.ReadBoolean();
                bool hostile = reader.ReadBoolean();
                if (Main.projectile[projID] != null)
                {
                    Main.projectile[projID].owner = owner;
                    Main.projectile[projID].friendly = friendly;
                    Main.projectile[projID].hostile = hostile;
                }
                if (Main.netMode == 2) MNet.SendBaseNetMessage(0, owner, projID, friendly, hostile);
            }
            else
            if (msg == MsgType.SyncAI) //sync AI array
            {
                int classID = reader.ReadByte();
                int id = reader.ReadInt16();
                int aitype = reader.ReadByte();
                int arrayLength = reader.ReadByte();
                float[] newAI = new float[arrayLength];
                for (int m = 0; m < arrayLength; m++)
                {
                    newAI[m] = reader.ReadSingle();
                }
                if (classID == 0 && Main.npc[id] != null && Main.npc[id].active && Main.npc[id].modNPC != null && Main.npc[id].modNPC is ParentNPC)
                {
                    ((ParentNPC)Main.npc[id].modNPC).SetAI(newAI, aitype);
                }
                else
                if (classID == 1 && Main.projectile[id] != null && Main.projectile[id].active && Main.projectile[id].modProjectile != null && Main.projectile[id].modProjectile is ParentProjectile)
                {
                    ((ParentProjectile)Main.projectile[id].modProjectile).SetAI(newAI, aitype);
                }
                if (Main.netMode == 2) BaseNet.SyncAI(classID, id, newAI, aitype);
            }
        }

        public static bool NoInvasion(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.invasion && (!Main.pumpkinMoon && !Main.snowMoon || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) &&
                   (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime);
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

    enum MsgType : byte
    {
        ProjectileHostility,
        SyncAI
    }
}
