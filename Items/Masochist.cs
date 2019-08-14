using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items
{
    public class Masochist : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutant's Gift");
            Tooltip.SetDefault("'Use this to turn on/off Masochist Mode'");
            DisplayName.AddTranslation(GameCulture.Chinese, "突变体的礼物");
            Tooltip.AddTranslation(GameCulture.Chinese, "'用开/关受虐模式'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.rare = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = false;
        }

        public override bool UseItem(Player player)
        {
            bool bossExists = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active && Main.npc[i].boss)
                {
                    bossExists = true;
                    break;
                }
            }

            if (!bossExists)
            {
                FargoWorld.MasochistMode = !FargoWorld.MasochistMode;
                Main.expertMode = true;
                string text = FargoWorld.MasochistMode ? "Masochist Mode initiated!" : "Masochist Mode deactivated!";
                if (Main.netMode == 0)
                {
                    Main.NewText(text, 175, 75, 255);
                }
                else if (Main.netMode == 2)
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), new Color(175, 75, 255));
                    NetMessage.SendData(7); //sync world
                }
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            }
            return true;
        }
    }
}