using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Misc
{
    public class PandorasBox : ModItem
    {
        public int occur = -1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pandora's Box");
            Tooltip.SetDefault("Summons something at random\n" +
                                "Use at your own risk");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 20;
            item.value = 1000;
            item.rare = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime;
        }

        public override bool UseItem(Player player)
        {
            occur = (Main.rand.Next(44));
            switch (occur)
            {
                //bosses
                case 1:
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.EyeofCthulhu);
                    break;
                case 2:
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.KingSlime);
                    break;
                case 3:
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.QueenBee);
                    break;
                case 4:
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.TheDestroyer);
                    break;
                case 5:
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.SkeletronPrime);
                    break;
                case 6:
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.Plantera);
                    break;
                case 8:
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 400, NPCID.DukeFishron);
                    Main.NewText("Duke Fishron has awoken!", 175, 75, 255);
                    break;
                case 9:
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.SkeletronHead);
                    Main.NewText("Skeletron has awoken!", 175, 75, 255);
                    break;
                case 10:
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.Retinazer);
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.Spazmatism);
                    break;
                case 11:
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsHead);
                    break;
                case 12:
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.BrainofCthulhu);
                    break;
                //minibosses and event bosses
                case 13:
                    NPC.NewNPC((int)player.position.X + 200, (int)player.position.Y - 50, NPCID.Paladin);
                    NPC.NewNPC((int)player.position.X - 200, (int)player.position.Y - 50, NPCID.Paladin);
                    break;
                case 14:
                    NPC.NewNPC((int)player.position.X + 200, (int)player.position.Y - 50, NPCID.IceGolem);
                    NPC.NewNPC((int)player.position.X - 200, (int)player.position.Y - 50, NPCID.IceGolem);
                    break;
                case 15:
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.MartianSaucerCore);
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.MartianSaucerCore);
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.MartianSaucerCore);
                    break;
                case 16:
                    for (int i = 0; i < 40; i++)
                    {
                        NPC.SpawnOnPlayer(player.whoAmI, NPCID.Zombie);
                    }
                    break;
                case 17:
                    NPC.NewNPC((int)player.position.X + 200, (int)player.position.Y, NPCID.SandElemental);
                    NPC.NewNPC((int)player.position.X - 200, (int)player.position.Y, NPCID.SandElemental);
                    break;
                case 18:
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.DD2Betsy);
                    Main.NewText("Betsy has awoken!", 175, 75, 255);
                    break;
                case 19:
                    NPC.NewNPC((int)player.position.X + 20, (int)player.position.Y - 220, NPCID.DD2OgreT3);
                    break;
                case 20:
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.WyvernHead);
                    break;
                case 21:
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.CultistDragonHead);
                    break;
                //rare enemies
                case 22:
                    NPC.NewNPC((int)player.position.X + 200, (int)player.position.Y - 50, NPCID.Tim);
                    break;
                case 23:
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.UndeadMiner);
                    break;
                case 24:
                    NPC.NewNPC((int)player.position.X + 200, (int)player.position.Y - 50, NPCID.RuneWizard);
                    break;
                case 25:
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.Moth);
                    break;
                case 26:
                    NPC.NewNPC((int)player.position.X + 50, (int)player.position.Y - 220, NPCID.Medusa);
                    break;
                case 27:
                    NPC.NewNPC((int)player.position.X + 50, (int)player.position.Y - 220, NPCID.LostGirl);
                    NPC.NewNPC((int)player.position.X + 100, (int)player.position.Y - 220, NPCID.LostGirl);
                    NPC.NewNPC((int)player.position.X + 150, (int)player.position.Y - 220, NPCID.LostGirl);
                    NPC.NewNPC((int)player.position.X + 200, (int)player.position.Y - 220, NPCID.LostGirl);
                    NPC.NewNPC((int)player.position.X - 50, (int)player.position.Y - 220, NPCID.LostGirl);
                    NPC.NewNPC((int)player.position.X - 100, (int)player.position.Y - 220, NPCID.LostGirl);
                    NPC.NewNPC((int)player.position.X - 150, (int)player.position.Y - 220, NPCID.LostGirl);
                    NPC.NewNPC((int)player.position.X - 200, (int)player.position.Y - 220, NPCID.LostGirl);
                    NPC.NewNPC((int)player.position.X - 250, (int)player.position.Y - 220, NPCID.LostGirl);
                    NPC.NewNPC((int)player.position.X + 250, (int)player.position.Y - 220, NPCID.LostGirl);
                    break;
                case 28:
                    NPC.NewNPC((int)player.position.X + 50, (int)player.position.Y - 220, NPCID.DoctorBones);
                    break;
                //groups of enemy
                case 29:
                    NPC.NewNPC((int)player.position.X + 100, (int)player.position.Y - 220, NPCID.CrimsonAxe);
                    NPC.NewNPC((int)player.position.X - 100, (int)player.position.Y - 220, NPCID.CursedHammer);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.EnchantedSword);
                    break;
                case 30:
                    NPC.NewNPC((int)player.position.X + 50, (int)player.position.Y - 120, NPCID.IceTortoise);
                    NPC.NewNPC((int)player.position.X - 50, (int)player.position.Y - 120, NPCID.GiantTortoise);
                    break;
                case 31:
                    NPC.NewNPC((int)player.position.X + 200, (int)player.position.Y - 220, NPCID.PigronCorruption);
                    NPC.NewNPC((int)player.position.X - 200, (int)player.position.Y - 220, NPCID.PigronCrimson);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.PigronHallow);
                    break;
                case 32:
                    NPC.NewNPC((int)player.position.X + 100, (int)player.position.Y - 220, NPCID.BigMimicCorruption);
                    NPC.NewNPC((int)player.position.X - 100, (int)player.position.Y - 220, NPCID.BigMimicCrimson);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.BigMimicHallow);
                    break;
                case 33:
                    NPC.NewNPC((int)player.position.X + 100, (int)player.position.Y - 220, NPCID.BloodCrawler);
                    NPC.NewNPC((int)player.position.X + 50, (int)player.position.Y - 220, NPCID.WallCreeper);

                    NPC.NewNPC((int)player.position.X - 50, (int)player.position.Y - 220, NPCID.JungleCreeper);
                    NPC.NewNPC((int)player.position.X - 100, (int)player.position.Y - 220, NPCID.BlackRecluse);
                    break;
                case 34:
                    NPC.NewNPC((int)player.position.X + 50, (int)player.position.Y - 120, NPCID.Harpy);
                    NPC.NewNPC((int)player.position.X + 100, (int)player.position.Y - 120, NPCID.Harpy);
                    NPC.NewNPC((int)player.position.X - 50, (int)player.position.Y - 120, NPCID.Harpy);
                    NPC.NewNPC((int)player.position.X - 100, (int)player.position.Y - 120, NPCID.Harpy);
                    break;
                case 35:
                    NPC.NewNPC((int)player.position.X + 50, (int)player.position.Y - 50, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X + 100, (int)player.position.Y - 50, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X + 150, (int)player.position.Y - 50, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X + 200, (int)player.position.Y - 50, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X - 50, (int)player.position.Y - 50, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X - 100, (int)player.position.Y - 50, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X - 150, (int)player.position.Y - 50, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X - 200, (int)player.position.Y - 50, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X + 50, (int)player.position.Y - 120, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X + 100, (int)player.position.Y - 120, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X + 150, (int)player.position.Y - 120, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X + 200, (int)player.position.Y - 120, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X - 50, (int)player.position.Y - 120, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X - 100, (int)player.position.Y - 120, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X - 150, (int)player.position.Y - 120, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X - 200, (int)player.position.Y - 120, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X - 250, (int)player.position.Y - 120, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X + 250, (int)player.position.Y - 120, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X + 300, (int)player.position.Y - 120, NPCID.FlyingFish);
                    NPC.NewNPC((int)player.position.X - 300, (int)player.position.Y - 120, NPCID.FlyingFish);
                    break;
                case 36:
                case 37:
                    NPC.NewNPC((int)player.position.X + 80, (int)player.position.Y - 150, NPCID.CaveBat);
                    NPC.NewNPC((int)player.position.X - 80, (int)player.position.Y - 150, NPCID.JungleBat);
                    NPC.NewNPC((int)player.position.X + 60, (int)player.position.Y - 150, NPCID.Hellbat);
                    NPC.NewNPC((int)player.position.X - 60, (int)player.position.Y - 150, NPCID.GiantBat);
                    NPC.NewNPC((int)player.position.X + 40, (int)player.position.Y - 150, NPCID.IceBat);

                    NPC.NewNPC((int)player.position.X - 40, (int)player.position.Y - 150, NPCID.IlluminantBat);
                    NPC.NewNPC((int)player.position.X + 20, (int)player.position.Y - 150, NPCID.Lavabat);
                    NPC.NewNPC((int)player.position.X - 20, (int)player.position.Y - 150, NPCID.GiantFlyingFox);
                    break;
                case 38:
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.StardustWormHead);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.StardustCellBig);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.StardustJellyfishBig);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.StardustSpiderBig);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.StardustSoldier);
                    break;
                case 39:
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.SolarCrawltipedeHead);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.SolarDrakomireRider);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.SolarSroller);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.SolarCorite);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.SolarSolenian);
                    break;
                case 40:
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.NebulaBrain);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.NebulaHeadcrab);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.NebulaBeast);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.NebulaSoldier);
                    break;
                case 41:
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.VortexRifleman);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.VortexHornetQueen);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.VortexHornetQueen);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.VortexSoldier);
                    break;
                case 42:
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.MartianWalker);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.ScutlixRider);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.MartianDrone);
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 220, NPCID.MartianOfficer);
                    break;
                default:
                    NPC.NewNPC((int)player.position.X + 50, (int)player.position.Y - 220, NPCID.Duck);
                    NPC.NewNPC((int)player.position.X + 150, (int)player.position.Y - 220, NPCID.Duck);
                    NPC.NewNPC((int)player.position.X - 100, (int)player.position.Y - 220, NPCID.Duck);
                    NPC.NewNPC((int)player.position.X - 200, (int)player.position.Y - 220, NPCID.Duck);
                    NPC.NewNPC((int)player.position.X + 100, (int)player.position.Y - 220, NPCID.DuckWhite);
                    NPC.NewNPC((int)player.position.X + 200, (int)player.position.Y - 220, NPCID.DuckWhite);
                    NPC.NewNPC((int)player.position.X - 50, (int)player.position.Y - 220, NPCID.DuckWhite);
                    NPC.NewNPC((int)player.position.X - 150, (int)player.position.Y - 220, NPCID.DuckWhite);
                    break;
            }

            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }
    }
}