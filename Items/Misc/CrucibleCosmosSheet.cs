using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Misc
{
    public class CrucibleCosmosSheet : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Width = 4;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Crucible of the Cosmos");
            AddMapEntry(new Color(200, 200, 200), name);
            disableSmartCursor = true;
            //counts as
            adjTiles = new int[] { TileID.WorkBenches, TileID.HeavyWorkBench, TileID.Anvils, TileID.MythrilAnvil, TileID.Furnaces, TileID.Hellforge, TileID.AdamantiteForge, TileID.Bottles, TileID.AlchemyTable, TileID.Sawmill, TileID.Loom, TileID.CookingPots, TileID.Solidifier, TileID.DyeVat, TileID.TinkerersWorkbench, TileID.DemonAltar, TileID.Bookcases,  TileID.CrystalBall, TileID.Autohammer,  TileID.LunarCraftingStation, TileID.Campfire, TileID.Sinks, TileID.ImbuingStation, TileID.Kegs };

            if (ModLoader.GetMod("ThoriumMod") != null)
            {
                Array.Resize(ref adjTiles, adjTiles.Length + 3);
                adjTiles[adjTiles.Length - 1] = ModLoader.GetMod("ThoriumMod").TileType("ThoriumAnvil");
                adjTiles[adjTiles.Length - 2] = ModLoader.GetMod("ThoriumMod").TileType("ArcaneArmorFabricator");
                adjTiles[adjTiles.Length - 3] = ModLoader.GetMod("ThoriumMod").TileType("SoulForge");
            }

            animationFrameHeight = 54;
            
            name.AddTranslation(GameCulture.Chinese, "宇宙坩埚");
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("CrucibleCosmos"));
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter >= -1) //replace with duration of frame in ticks
            {
                frameCounter = 0;
                frame++;
                frame %= 8;
            }
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.player[Main.myPlayer];
            player.alchemyTable = true;
        }
    }
}