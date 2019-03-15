using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Misc
{
    public class PandorasBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pandora's Box");
            Tooltip.SetDefault("Summons something at random\n" +
                                "Much friendlier options during the day");
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

        public override bool UseItem(Player player)
        {
            int totalNPCs = NPCLoader.NPCCount;

            for (int i = 0; i < 5; i++)
            {
                NPC npc = new NPC();
                npc.SetDefaults(Main.rand.Next(totalNPCs));

                if (Main.dayTime)
                {
                    if (npc.lifeMax > 400 || npc.boss || npc.townNPC || npc.dontTakeDamage)
                    {
                        i--;
                    }
                    else
                    {
                        int spawn = NPC.NewNPC((int)player.position.X + Main.rand.Next(-800, 800), (int)player.position.Y + Main.rand.Next(-1000, -250), npc.type);

                        if (npc.friendly)
                        {
                            Main.npc[spawn].defense = 999;
                        }
                    }
                }
                //night
                else
                {
                    if (npc.townNPC || npc.dontTakeDamage)
                    {
                        i--;
                    }
                    else
                    {
                        NPC.NewNPC((int)player.position.X + Main.rand.Next(-800, 800), (int)player.position.Y + Main.rand.Next(-1000, -250), npc.type);
                    }
                }
            }

            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }
    }
}