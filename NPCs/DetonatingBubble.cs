using FargowiltasSouls.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.NPCs
{
    public class DetonatingBubble : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Detonating Bubble");
        }

        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 36;
            npc.damage = 100;
            npc.defense = 0;
            npc.lifeMax = 1;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            //npc.alpha = 255;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.aiStyle = -1;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 150;
        }

        public override void AI()
        {
            npc.velocity *= 1.04f;

            npc.ai[0]++;
            if (npc.ai[0] >= 120f)
            {
                npc.life = 0;
                npc.checkDead();
                npc.active = false;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (BossIsAlive(ref fishBoss, NPCID.DukeFishron))
                target.AddBuff(mod.BuffType<Defenseless>(), Main.rand.Next(600, 900));
            target.AddBuff(BuffID.Wet, 420);
            target.AddBuff(mod.BuffType<SqueakyToy>(), Main.rand.Next(60, 180));
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return new Color(255, 255, 255);
        }
    }
}