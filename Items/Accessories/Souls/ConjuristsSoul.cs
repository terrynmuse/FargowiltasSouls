using Terraria;
using Terraria.ModLoader;
using static Terraria.ID.ItemID;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class ConjuristsSoul : ModItem
    {
        private Mod _bluemagic;
        private readonly Mod _calamity = ModLoader.GetMod("CalamityMod");
        private Mod _thorium;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Conjurist's Soul");

            if (Fargowiltas.Instance.CalamityLoaded)
            {
                Tooltip.SetDefault("'An army at your disposal' \n" +
                                    "Increases your max number of minions by 4 \n" +
                                    "Increases your max number of sentries by 2 \n" +
                                    "40% increased summon damage \n" +
                                    "Increased minion knockback \n" +
                                    "Summons all waifus to protect you, turn off vanity to despawn them");
            }
            else
            {
                Tooltip.SetDefault("'An army at your disposal' \n" +
                                    "Increases your max number of minions by 4 \n" +
                                    "Increases your max number of sentries by 2 \n" +
                                    "40% increased summon damage \n" +
                                    "Increased minion knockback");
            }

        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 750000;
            item.rare = -12;
            item.expert = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            player.maxMinions += 4;
            player.minionDamage += 0.4f;
            player.minionKB += 3f;
            player.maxTurrets += 2;

            //heart of elements
            if (!Fargowiltas.Instance.CalamityLoaded) return;
            if (!hideVisual)
            {
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, Main.DiscoR / 255f, Main.DiscoG / 255f, Main.DiscoB / 255f);

                Waifus(player);

                if (player.whoAmI == Main.myPlayer)
                {
                    const int damage = 300;
                    const float damageMult = 2.5f;
                    if (player.FindBuffIndex(_calamity.BuffType("BrimstoneWaifu")) == -1)
                    {
                        player.AddBuff(_calamity.BuffType("BrimstoneWaifu"), 3600);
                    }
                    if (player.ownedProjectileCounts[_calamity.ProjectileType("BigBustyRose")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, _calamity.ProjectileType("BigBustyRose"), (int)(damage * damageMult), 2f, Main.myPlayer);
                    }
                    if (player.FindBuffIndex(_calamity.BuffType("SirenLure")) == -1)
                    {
                        player.AddBuff(_calamity.BuffType("SirenLure"), 3600);
                    }
                    if (player.ownedProjectileCounts[_calamity.ProjectileType("SirenLure")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, _calamity.ProjectileType("SirenLure"), 0, 0f, Main.myPlayer);
                    }
                    if (player.FindBuffIndex(_calamity.BuffType("DrewsSandyWaifu")) == -1)
                    {
                        player.AddBuff(_calamity.BuffType("DrewsSandyWaifu"), 3600);
                    }
                    if (player.ownedProjectileCounts[_calamity.ProjectileType("DrewsSandyWaifu")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, _calamity.ProjectileType("DrewsSandyWaifu"), (int)(damage * damageMult * 1.5f), 2f, Main.myPlayer);
                    }
                    if (player.FindBuffIndex(_calamity.BuffType("SandyWaifu")) == -1)
                    {
                        player.AddBuff(_calamity.BuffType("SandyWaifu"), 3600);
                    }
                    if (player.ownedProjectileCounts[_calamity.ProjectileType("SandyWaifu")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, _calamity.ProjectileType("SandyWaifu"), (int)(damage * damageMult * 1.5f), 2f, Main.myPlayer);
                    }
                    if (player.FindBuffIndex(_calamity.BuffType("CloudyWaifu")) == -1)
                    {
                        player.AddBuff(_calamity.BuffType("CloudyWaifu"), 3600);
                    }
                    if (player.ownedProjectileCounts[_calamity.ProjectileType("CloudyWaifu")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, _calamity.ProjectileType("CloudyWaifu"), (int)(damage * damageMult), 2f, Main.myPlayer);
                    }
                }
            }

            if (player.whoAmI != Main.myPlayer || player.velocity.Y != 0f || player.grappling[0] != -1) return;
            int num4 = (int)player.Center.X / 16;
            int num5 = (int)(player.position.Y + player.height - 1f) / 16;
            if (Main.tile[num4, num5] == null)
            {
                Main.tile[num4, num5] = new Tile();
            }

            if (Main.tile[num4, num5].active() || Main.tile[num4, num5].liquid != 0 ||
                Main.tile[num4, num5 + 1] == null || !WorldGen.SolidTile(num4, num5 + 1)) return;
            Main.tile[num4, num5].frameY = 0;
            Main.tile[num4, num5].slope(0);
            Main.tile[num4, num5].halfBrick(false);
            
            if (Main.tile[num4, num5 + 1].type == 0)
            {
                if (Main.rand.Next(1000) == 0)
                {
                    Main.tile[num4, num5].active(true);
                    Main.tile[num4, num5].type = 227;
                    Main.tile[num4, num5].frameX = (short)(34 * Main.rand.Next(1, 13));
                    while (Main.tile[num4, num5].frameX == 144)
                    {
                        Main.tile[num4, num5].frameX = (short)(34 * Main.rand.Next(1, 13));
                    }
                }
                if (Main.netMode == 1)
                {
                    NetMessage.SendTileSquare(-1, num4, num5, 1);
                }
            }
            if (Main.tile[num4, num5 + 1].type == 2)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Main.tile[num4, num5].active(true);
                    Main.tile[num4, num5].type = 3;
                    Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(6, 11));
                    while (Main.tile[num4, num5].frameX == 144)
                    {
                        Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(6, 11));
                    }
                }
                else
                {
                    Main.tile[num4, num5].active(true);
                    Main.tile[num4, num5].type = 73;
                    Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(6, 21));
                    while (Main.tile[num4, num5].frameX == 144)
                    {
                        Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(6, 21));
                    }
                }
                if (Main.netMode == 1)
                {
                    NetMessage.SendTileSquare(-1, num4, num5, 1);
                }
            }
            else if (Main.tile[num4, num5 + 1].type == 109)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Main.tile[num4, num5].active(true);
                    Main.tile[num4, num5].type = 110;
                    Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(4, 7));
                    while (Main.tile[num4, num5].frameX == 90)
                    {
                        Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(4, 7));
                    }
                }
                else
                {
                    Main.tile[num4, num5].active(true);
                    Main.tile[num4, num5].type = 113;
                    Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(2, 8));
                    while (Main.tile[num4, num5].frameX == 90)
                    {
                        Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(2, 8));
                    }
                }
                if (Main.netMode == 1)
                {
                    NetMessage.SendTileSquare(-1, num4, num5, 1);
                }
            }
            else if (Main.tile[num4, num5 + 1].type == 60)
            {
                Main.tile[num4, num5].active(true);
                Main.tile[num4, num5].type = 74;
                Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(9, 17));
                if (Main.netMode == 1)
                {
                    NetMessage.SendTileSquare(-1, num4, num5, 1);
                }
            }
        }

        private void Waifus(Player player)
        {
            CalamityPlayer calamityPlayer = player.GetModPlayer<CalamityMod.CalamityPlayer>(_calamity);
            
            calamityPlayer.brimstoneWaifu = true;
            calamityPlayer.sandBoobWaifu = true;
            calamityPlayer.sandWaifu = true;
            calamityPlayer.cloudWaifu = true;
            calamityPlayer.sirenWaifu = true;
        }

        public override void AddRecipes()
        {
            ModRecipe summon2 = new ModRecipe(mod);
            summon2.AddIngredient(null, "OccultistsEssence");

            _bluemagic = ModLoader.GetMod("Bluemagic");
            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                _thorium = ModLoader.GetMod("ThoriumMod");
                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    if (Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //all 3
                        summon2.AddIngredient(_bluemagic.ItemType("InfinityScarab"));
                    }

                    summon2.AddIngredient(_calamity.ItemType("StatisCurse"));
                    summon2.AddIngredient(_calamity.ItemType("HeartoftheElements"));
                    summon2.AddIngredient(_thorium.ItemType("BloodCellStaff"));
                    summon2.AddIngredient(_thorium.ItemType("HailBomber"));
                    summon2.AddIngredient(_calamity.ItemType("BlightedEyeStaff"));
                    summon2.AddIngredient(StaffoftheFrostHydra);
                    summon2.AddRecipeGroup("FargowiltasSouls:AnySentry");

                    if (!Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //thorium and calamity
                        summon2.AddIngredient(TempestStaff);
                    }

                    summon2.AddIngredient(_thorium.ItemType("TerrariumSummon"));

                    summon2.AddIngredient(MoonlordTurretStaff);
                    summon2.AddIngredient(_thorium.ItemType("EmberStaff"));
                    summon2.AddIngredient(_calamity.ItemType("StaffoftheMechworm"));
                }

                if (!Fargowiltas.Instance.CalamityLoaded)
                {
                    summon2.AddIngredient(Fargowiltas.Instance.BlueMagicLoaded
                        ? _bluemagic.ItemType("InfinityScarab")
                        : PapyrusScarab);

                    summon2.AddIngredient(_thorium.ItemType("MastersLibram"));
                    summon2.AddIngredient(_thorium.ItemType("BloodCellStaff"));
                    summon2.AddIngredient(_thorium.ItemType("HailBomber"));
                    summon2.AddIngredient(StaffoftheFrostHydra);
                    summon2.AddIngredient(_thorium.ItemType("TheIncubator"));
                    summon2.AddRecipeGroup("FargowiltasSouls:AnySentry");
                    summon2.AddIngredient(TempestStaff);
                    summon2.AddIngredient(RavenStaff);
                    summon2.AddIngredient(XenoStaff);
                    summon2.AddIngredient(_thorium.ItemType("TerrariumSummon"));
                    summon2.AddIngredient(MoonlordTurretStaff);
                    summon2.AddIngredient(_thorium.ItemType("EmberStaff"));
                }
            }

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {

                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    if (Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //calamity and blue
                        summon2.AddIngredient(_bluemagic.ItemType("InfinityScarab"));
                    }

                    summon2.AddIngredient(_calamity.ItemType("StatisCurse"));
                    summon2.AddIngredient(_calamity.ItemType("HeartoftheElements"));
                    summon2.AddIngredient(_calamity.ItemType("BlightedEyeStaff"));
                    summon2.AddIngredient(StaffoftheFrostHydra);

                    if (!Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //just calamity

                    }

                    summon2.AddRecipeGroup("FargowiltasSouls:AnySentry");
                    summon2.AddIngredient(TempestStaff);
                    summon2.AddIngredient(RavenStaff);
                    summon2.AddIngredient(XenoStaff);

                    summon2.AddIngredient(MoonlordTurretStaff);
                    summon2.AddIngredient(_calamity.ItemType("Cosmilamp"));
                    summon2.AddIngredient(_calamity.ItemType("StaffoftheMechworm"));
                }

                if (!Fargowiltas.Instance.CalamityLoaded)
                {
                    if (Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //just blue
                        summon2.AddIngredient(_bluemagic.ItemType("InfinityScarab"));
                    }

                    else
                    {
                        //no others
                        summon2.AddIngredient(PygmyNecklace);
                        summon2.AddIngredient(PapyrusScarab);
                    }

                    summon2.AddIngredient(QueenSpiderStaff);
                    summon2.AddIngredient(OpticStaff);
                    summon2.AddIngredient(TempestStaff);
                    summon2.AddIngredient(RavenStaff);
                    summon2.AddIngredient(XenoStaff);
                    summon2.AddRecipeGroup("FargowiltasSouls:AnySentry");
                    summon2.AddRecipeGroup("FargowiltasSouls:AnySentry");
                    summon2.AddIngredient(MoonlordTurretStaff);
                }
            }

            //summon2.AddTile(null, "CrucibleCosmosSheet");
            summon2.SetResult(this);
            summon2.AddRecipe();
        }

    }
}