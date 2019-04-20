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
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (type == BuffID.ShadowFlame) modPlayer.Shadowflame = true;

            if (type == BuffID.Slimed)
            {
                Main.buffNoTimeDisplay[type] = false;
                modPlayer.Slimed = true;
            }

            base.Update(type, player, ref buffIndex);
        }

        public override void Update(int type, NPC npc, ref int buffIndex)
        {
            if (type == BuffID.Chilled)
            {
                npc.color = Colors.RarityBlue;

                if (!npc.boss) npc.velocity *= .5f;
            }
            else if(type == BuffID.Darkness)
            {
                npc.color = Color.Gray;

                if(Main.rand.Next(20) == 0)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        NPC target = Main.npc[i];
                        if (target.active && Vector2.Distance(npc.Center, target.Center) < 200)
                        {
                            Vector2 velocity = Vector2.Normalize(target.Center - npc.Center) * 5;
                            Projectile.NewProjectile(npc.Center, velocity, ProjectileID.ShadowFlame, npc.damage / 2, 0, Main.myPlayer);

                            if(Main.rand.Next(3) == 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else if (type == BuffID.Electrified)
            {
                npc.GetGlobalNPC<NPCs.FargoGlobalNPC>().Electrified = true;
            }
            
        }
    }
}