using FargowiltasSouls.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.NPCs
{
    public class DetonatingBubble : ModNPC
    {
        public override string Texture => "Terraria/NPC_371";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Detonating Bubble");
            Main.npcFrameCount[npc.type] = 2;
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
            npc.alpha = 255;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.aiStyle = -1;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 150;
            npc.lifeMax = 1;
            npc.life = 1;
        }

        public override void AI()
        {
            if (npc.alpha > 50)
                npc.alpha -= 30;
            else
                npc.alpha = 50;

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
            if (FargoGlobalNPC.BossIsAlive(ref FargoGlobalNPC.fishBoss, NPCID.DukeFishron))
            {
                target.GetModPlayer<FargoPlayer>(mod).MaxLifeReduction += 25;
                target.AddBuff(mod.BuffType<OceanicMaul>(), Main.rand.Next(900, 1800));
            }
            target.AddBuff(BuffID.Wet, 420);
            target.AddBuff(mod.BuffType<SqueakyToy>(), Main.rand.Next(60, 180));
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = Main.npcTexture[npc.type].Height / 2;
        }
    }
}