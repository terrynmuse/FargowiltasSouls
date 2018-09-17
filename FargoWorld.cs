using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace FargowiltasSouls
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FargoWorld : ModWorld
    {
        private static bool _movedLumberjack;
        private static bool _downedBetsy;
        private static bool _downedBoss;

        //masomode
        public static bool MasochistMode;
        public static int EyeCount;
        public static int SlimeCount;
        public static int EaterCount;
        public static int BrainCount;
        public static int BeeCount;
        public static int SkeletronCount;
        public static int WallCount;
        public static int DestroyerCount;
        public static int PrimeCount;
        public static int TwinsCount;
        public static int PlanteraCount;
        public static int GolemCount;
        public static int FishronCount;
        public static int CultistCount;
        public static int MoonlordCount;

        //town npcs
        private static bool _guide;
        private static bool _merch;
        private static bool _nurse;
        private static bool _demo;
        private static bool _dye;
        private static bool _dryad;
        private static bool _keep;
        private static bool _dealer;
        private static bool _style;
        private static bool _paint;
        private static bool _angler;
        private static bool _goblin;
        private static bool _doc;
        private static bool _cloth;
        private static bool _mech;
        private static bool _party;
        private static bool _wiz;
        private static bool _tax;
        private static bool _truf;
        private static bool _pirate;
        private static bool _steam;
        private static bool _borg;

        public override void Initialize()
        {
            _movedLumberjack = false;
            _downedBetsy = false;
            _downedBoss = false;

            //masomode
            MasochistMode = false;
            EyeCount = 0;
            SlimeCount = 0;
            EaterCount = 0;
            BrainCount = 0;
            BeeCount = 0;
            SkeletronCount = 0;
            WallCount = 0;
            DestroyerCount = 0;
            PrimeCount = 0;
            TwinsCount = 0;
            PlanteraCount = 0;
            GolemCount = 0;
            FishronCount = 0;
            CultistCount = 0;
            MoonlordCount = 0;

            //town npcs
            _guide = false;
            _merch = false;
            _nurse = false;
            _demo = false;
            _dye = false;
            _dryad = false;
            _keep = false;
            _dealer = false;
            _style = false;
            _paint = false;
            _angler = false;
            _goblin = false;
            _doc = false;
            _cloth = false;
            _mech = false;
            _party = false;
            _wiz = false;
            _tax = false;
            _truf = false;
            _pirate = false;
            _steam = false;
            _borg = false;
        }

        public override TagCompound Save()
        {
            List<int> count = new List<int>
            {
                EyeCount,
                SlimeCount,
                EaterCount,
                BrainCount,
                BeeCount,
                SkeletronCount,
                WallCount,
                DestroyerCount,
                PrimeCount,
                TwinsCount,
                PlanteraCount,
                GolemCount,
                FishronCount,
                CultistCount,
                MoonlordCount
            };

            List<string> downed = new List<string>();
            if (_movedLumberjack) downed.Add("lumberjack");
            if (_downedBetsy) downed.Add("betsy");
            if (_downedBoss) downed.Add("boss");

            //masomode
            if (MasochistMode) downed.Add("masochist");

            //town npcs
            if (_guide) downed.Add("guide");
            if (_merch) downed.Add("merch");
            if (_nurse) downed.Add("nurse");
            if (_demo) downed.Add("demo");
            if (_dye) downed.Add("dye");
            if (_dryad) downed.Add("dryad");
            if (_keep) downed.Add("keep");
            if (_dealer) downed.Add("dealer");
            if (_style) downed.Add("style");
            if (_paint) downed.Add("paint");
            if (_angler) downed.Add("angler");
            if (_goblin) downed.Add("goblin");
            if (_doc) downed.Add("doc");
            if (_cloth) downed.Add("cloth");
            if (_mech) downed.Add("mech");
            if (_party) downed.Add("party");
            if (_wiz) downed.Add("wiz");
            if (_tax) downed.Add("tax");
            if (_truf) downed.Add("truf");
            if (_pirate) downed.Add("pirate");
            if (_steam) downed.Add("steam");
            if (_borg) downed.Add("borg");

            return new TagCompound
            {
                {"downed", downed}, {"count", count}
            };
        }

        public override void Load(TagCompound tag)
        {
            if (tag.ContainsKey("count"))
            {
                IList<int> count = tag.GetList<int>("count");
                EyeCount = count[0];
                SlimeCount = count[1];
                EaterCount = count[2];
                BrainCount = count[3];
                BeeCount = count[4];
                SkeletronCount = count[5];
                WallCount = count[6];
                DestroyerCount = count[7];
                PrimeCount = count[8];
                TwinsCount = count[9];
                PlanteraCount = count[10];
                GolemCount = count[11];
                FishronCount = count[12];
                CultistCount = count[13];
                MoonlordCount = count[14];
            }

            IList<string> downed = tag.GetList<string>("downed");
            _movedLumberjack = downed.Contains("lumberjack");
            _downedBetsy = downed.Contains("betsy");
            _downedBoss = downed.Contains("boss");
            MasochistMode = downed.Contains("masochist");

            _guide = downed.Contains("guide");
            _merch = downed.Contains("merch");
            _nurse = downed.Contains("nurse");
            _demo = downed.Contains("demo");
            _dye = downed.Contains("dye");
            _dryad = downed.Contains("dryad");
            _keep = downed.Contains("keep");
            _dealer = downed.Contains("dealer");
            _style = downed.Contains("style");
            _paint = downed.Contains("paint");
            _angler = downed.Contains("angler");
            _goblin = downed.Contains("goblin");
            _doc = downed.Contains("doc");
            _cloth = downed.Contains("cloth");
            _mech = downed.Contains("mech");
            _party = downed.Contains("party");
            _wiz = downed.Contains("wiz");
            _tax = downed.Contains("tax");
            _truf = downed.Contains("truf");
            _pirate = downed.Contains("pirate");
            _steam = downed.Contains("steam");
            _borg = downed.Contains("borg");
        }

        public override void NetReceive(BinaryReader reader)
        {
            EyeCount = reader.ReadInt32();
            SlimeCount = reader.ReadInt32();
            EaterCount = reader.ReadInt32();
            BrainCount = reader.ReadInt32();
            BeeCount = reader.ReadInt32();
            SkeletronCount = reader.ReadInt32();
            WallCount = reader.ReadInt32();
            DestroyerCount = reader.ReadInt32();
            PrimeCount = reader.ReadInt32();
            TwinsCount = reader.ReadInt32();
            PlanteraCount = reader.ReadInt32();
            GolemCount = reader.ReadInt32();
            FishronCount = reader.ReadInt32();
            CultistCount = reader.ReadInt32();
            MoonlordCount = reader.ReadInt32();

            BitsByte flags = reader.ReadByte();
            _downedBetsy = flags[0];
            _downedBoss = flags[1];
            MasochistMode = flags[2];
            _guide = flags[3];
            _merch = flags[4];
            _nurse = flags[5];
            _demo = flags[6];
            _dye = flags[7];

            BitsByte flags2 = reader.ReadByte();
            _dryad = flags2[0];
            _keep = flags2[1];
            _dealer = flags2[2];
            _style = flags2[3];
            _paint = flags2[4];
            _angler = flags2[5];
            _goblin = flags2[6];
            _doc = flags2[7];

            BitsByte flags3 = reader.ReadByte();
            _cloth = flags3[0];
            _mech = flags3[1];
            _party = flags3[2];
            _wiz = flags3[3];
            _tax = flags3[4];
            _truf = flags3[5];
            _pirate = flags3[6];
            _steam = flags3[7];

            BitsByte flags4 = reader.ReadByte();
            _borg = flags4[0];
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(EyeCount);
            writer.Write(SlimeCount);
            writer.Write(EaterCount);
            writer.Write(BrainCount);
            writer.Write(BeeCount);
            writer.Write(SkeletronCount);
            writer.Write(WallCount);
            writer.Write(DestroyerCount);
            writer.Write(PrimeCount);
            writer.Write(TwinsCount);
            writer.Write(PlanteraCount);
            writer.Write(GolemCount);
            writer.Write(FishronCount);
            writer.Write(CultistCount);
            writer.Write(MoonlordCount);

            BitsByte flags = new BitsByte
            {
                [0] = _downedBetsy,
                [1] = _downedBoss,
                [2] = MasochistMode,
                [3] = _guide,
                [4] = _merch,
                [5] = _nurse,
                [6] = _demo,
                [7] = _dye
            };

            BitsByte flags2 = new BitsByte
            {
                [0] = _dryad,
                [1] = _keep,
                [2] = _dealer,
                [3] = _style,
                [4] = _paint,
                [5] = _angler,
                [6] = _goblin,
                [7] = _doc
            };

            BitsByte flags3 = new BitsByte
            {
                [0] = _cloth,
                [1] = _mech,
                [2] = _party,
                [3] = _wiz,
                [4] = _tax,
                [5] = _truf,
                [6] = _pirate,
                [7] = _steam
            };

            BitsByte flags4 = new BitsByte {[0] = _borg};

            writer.Write(flags);
            writer.Write(flags2);
            writer.Write(flags3);
            writer.Write(flags4);
        }

        public override void PostUpdate()
        {
            #region commented

            //right when day starts
            /*if(/*Main.time == 0 && Main.dayTime && !Main.eclipse && FargoWorld.masochistMode)
			{
					Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0, 1f, 0f);
					
					if (Main.netMode == 0)
					{
						Main.eclipse = true;
						//Main.NewText(Lang.misc[20], 50, 255, 130, false);
					}
					else
					{
						//NetMessage.SendData(61, -1, -1, "", player.whoAmI, -6f, 0f, 0f, 0, 0, 0);
					}
				
				
			}*/

            // if (this.itemTime == 0 && this.itemAnimation > 0 && item.type == 361 && Main.CanStartInvasion(1, true))
            // {
            // this.itemTime = item.useTime;
            // Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0, 1f, 0f);
            // if (Main.netMode != 1)
            // {
            // if (Main.invasionType == 0)
            // {
            // Main.invasionDelay = 0;
            // Main.StartInvasion(1);
            // }
            // }
            // else
            // {
            // NetMessage.SendData(61, -1, -1, "", this.whoAmI, -1f, 0f, 0f, 0, 0, 0);
            // }
            // }
            // if (this.itemTime == 0 && this.itemAnimation > 0 && item.type == 602 && Main.CanStartInvasion(2, true))
            // {
            // this.itemTime = item.useTime;
            // Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0, 1f, 0f);
            // if (Main.netMode != 1)
            // {
            // if (Main.invasionType == 0)
            // {
            // Main.invasionDelay = 0;
            // Main.StartInvasion(2);
            // }
            // }
            // else
            // {
            // NetMessage.SendData(61, -1, -1, "", this.whoAmI, -2f, 0f, 0f, 0, 0, 0);
            // }
            // }
            // if (this.itemTime == 0 && this.itemAnimation > 0 && item.type == 1315 && Main.CanStartInvasion(3, true))
            // {
            // this.itemTime = item.useTime;
            // Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0, 1f, 0f);
            // if (Main.netMode != 1)
            // {
            // if (Main.invasionType == 0)
            // {
            // Main.invasionDelay = 0;
            // Main.StartInvasion(3);
            // }
            // }
            // else
            // {
            // NetMessage.SendData(61, -1, -1, "", this.whoAmI, -3f, 0f, 0f, 0, 0, 0);
            // }
            // }
            // if (this.itemTime == 0 && this.itemAnimation > 0 && item.type == 1844 && !Main.dayTime && !Main.pumpkinMoon && !Main.snowMoon && !DD2Event.Ongoing)
            // {
            // this.itemTime = item.useTime;
            // Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0, 1f, 0f);
            // if (Main.netMode != 1)
            // {
            // Main.NewText(Lang.misc[31], 50, 255, 130, false);
            // Main.startPumpkinMoon();
            // }
            // else
            // {
            // NetMessage.SendData(61, -1, -1, "", this.whoAmI, -4f, 0f, 0f, 0, 0, 0);
            // }
            // }

            // if (this.itemTime == 0 && this.itemAnimation > 0 && item.type == 3601 && NPC.downedGolemBoss && Main.hardMode && !NPC.AnyDanger() && !NPC.AnyoneNearCultists())
            // {
            // Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0, 1f, 0f);
            // this.itemTime = item.useTime;
            // if (Main.netMode == 0)
            // {
            // WorldGen.StartImpendingDoom();
            // }
            // else
            // {
            // NetMessage.SendData(61, -1, -1, "", this.whoAmI, -8f, 0f, 0f, 0, 0, 0);
            // }
            // }
            // if (this.itemTime == 0 && this.itemAnimation > 0 && item.type == 1958 && !Main.dayTime && !Main.pumpkinMoon && !Main.snowMoon && !DD2Event.Ongoing)
            // {
            // this.itemTime = item.useTime;
            // Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0, 1f, 0f);
            // if (Main.netMode != 1)
            // {
            // Main.NewText(Lang.misc[34], 50, 255, 130, false);
            // Main.startSnowMoon();
            // }
            // else
            // {
            // NetMessage.SendData(61, -1, -1, "", this.whoAmI, -5f, 0f, 0f, 0, 0, 0);
            // }
            // }

            #endregion
        }
    }
}