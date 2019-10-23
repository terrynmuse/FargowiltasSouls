using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Projectiles;

namespace FargowiltasSouls.Buffs
{
    internal class FargoGlobalBuff : GlobalBuff
    {
        public override void Update(int type, Player player, ref int buffIndex)
        {
            switch(type)
            {
                /*case BuffID.ShadowFlame:
                    player.GetModPlayer<FargoPlayer>().Shadowflame = true;
                    break;*/

                case BuffID.Slimed:
                    Main.buffNoTimeDisplay[type] = false;
                    player.GetModPlayer<FargoPlayer>().Slimed = true;
                    break;

                default:
                    break;
            }

            base.Update(type, player, ref buffIndex);
        }

        public override void Update(int type, NPC npc, ref int buffIndex)
        {
            switch(type)
            {
                case BuffID.Chilled:
                    npc.color = Colors.RarityBlue;
                    if (!npc.boss)
                        npc.position -= npc.velocity / 2;
                    break;

                case BuffID.Darkness:
                    npc.color = Color.Gray;
                    if (Main.rand.Next(20) == 0)
                    {
                        for (int i = 0; i < 200; i++)
                        {
                            NPC target = Main.npc[i];
                            if (target.active && !target.friendly && Vector2.Distance(npc.Center, target.Center) < 200)
                            {
                                Vector2 velocity = Vector2.Normalize(target.Center - npc.Center) * 5;
                                Projectile.NewProjectile(npc.Center, velocity, ProjectileID.ShadowFlame, npc.damage / 2, 0, Main.myPlayer);
                                if (Main.rand.Next(3) == 0)
                                    break;
                            }
                        }
                    }
                    break;

                case BuffID.Electrified:
                    npc.GetGlobalNPC<NPCs.FargoSoulsGlobalNPC>().Electrified = true;
                    break;

                case BuffID.Suffocation:
                    npc.color = Colors.RarityPurple;
                    npc.GetGlobalNPC<NPCs.FargoSoulsGlobalNPC>().Suffocation = true;
                    break;

                default:
                    break;
            }
        }
    }
}