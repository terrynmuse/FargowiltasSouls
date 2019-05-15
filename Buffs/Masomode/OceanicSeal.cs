using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class OceanicSeal : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Oceanic Seal");
            Description.SetDefault("You will not escape");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderDebuff";
            return true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (FargoGlobalNPC.BossIsAlive(ref FargoGlobalNPC.fishBossEX, NPCID.DukeFishron))
                player.buffTime[buffIndex] = 2;
            else
                return;

            float distance = player.Distance(Main.npc[FargoGlobalNPC.fishBossEX].Center);
            if (distance > 1600)
            {
                if (distance > 2400)
                {
                    if (distance > 3200)
                    {
                        player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " tried to escape."), 7777, 0);
                        return;
                    }

                    player.frozen = true;
                    player.controlHook = false;
                    player.controlUseItem = false;
                    if (player.mount.Active)
                        player.mount.Dismount(player);
                    player.velocity.X = 0f;
                    player.velocity.Y = -0.4f;
                }

                Vector2 movement = Main.npc[FargoGlobalNPC.fishBossEX].Center - player.Center;
                float difference = movement.Length() - 1600f;
                movement.Normalize();
                movement *= difference < 17f ? difference : 17f;
                player.position += movement;

                for (int i = 0; i < 15; i++)
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, 135, 0f, 0f, 0, default(Color), 2f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].noLight = true;
                    Main.dust[d].velocity *= 5f;
                }
            }
        }
    }
}