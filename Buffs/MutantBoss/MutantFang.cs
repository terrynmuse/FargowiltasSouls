using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;
using Terraria.Localization;

namespace FargowiltasSouls.Buffs.MutantBoss
{
    public class MutantFang : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Mutant Fang");
            Description.SetDefault("The power of Masochist Mode compels you");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderDebuff";
            return true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>(mod);
            player.poisoned = true;
            player.venom = true;
            player.ichor = true;
            player.onFire2 = true;
            player.electrified = true;
            //fargoPlayer.OceanicMaul = true;
            fargoPlayer.CurseoftheMoon = true;
            if (fargoPlayer.FirstInfection)
            {
                fargoPlayer.MaxInfestTime = player.buffTime[buffIndex];
                fargoPlayer.FirstInfection = false;
            }
            fargoPlayer.Infested = true;
            fargoPlayer.Rotting = true;
            fargoPlayer.MutantNibble = true;
            player.potionDelay = player.buffTime[buffIndex];
            if (Fargowiltas.Instance.MasomodeEX && !FargoSoulsWorld.downedFishronEX && player.buffTime[buffIndex] > 1
                && FargoSoulsGlobalNPC.BossIsAlive(ref FargoSoulsGlobalNPC.mutantBoss, mod.NPCType("MutantBoss")))
            {
                player.AddBuff(ModLoader.GetMod("MasomodeEX").BuffType("MutantJudgement"), player.buffTime[buffIndex]);
                player.buffTime[buffIndex] = 1;
            }
        }
    }
}