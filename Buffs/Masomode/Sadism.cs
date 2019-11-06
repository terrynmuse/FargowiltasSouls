using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;
using Terraria.Localization;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Sadism : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Sadism");
            Description.SetDefault("The power of Masochist Mode is with you");
            Main.buffNoSave[Type] = false;
            DisplayName.AddTranslation(GameCulture.Chinese, "施虐狂");
            Description.AddTranslation(GameCulture.Chinese, "受虐模式的力量与你同在");
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderBuff";
            return true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffImmune[mod.BuffType("Antisocial")] = true;
            player.buffImmune[mod.BuffType("Atrophied")] = true;
            player.buffImmune[mod.BuffType("Berserked")] = true;
            player.buffImmune[mod.BuffType("Bloodthirsty")] = true;
            player.buffImmune[mod.BuffType("ClippedWings")] = true;
            player.buffImmune[mod.BuffType("Crippled")] = true;
            player.buffImmune[mod.BuffType("CurseoftheMoon")] = true;
            player.buffImmune[mod.BuffType("Defenseless")] = true;
            player.buffImmune[mod.BuffType("FlamesoftheUniverse")] = true;
            player.buffImmune[mod.BuffType("Flipped")] = true;
            player.buffImmune[mod.BuffType("FlippedHallow")] = true;
            player.buffImmune[mod.BuffType("Fused")] = true;
            player.buffImmune[mod.BuffType("GodEater")] = true;
            player.buffImmune[mod.BuffType("Guilty")] = true;
            player.buffImmune[mod.BuffType("Hexed")] = true;
            player.buffImmune[mod.BuffType("Infested")] = true;
            player.buffImmune[mod.BuffType("IvyVenom")] = true;
            player.buffImmune[mod.BuffType("Jammed")] = true;
            player.buffImmune[mod.BuffType("Lethargic")] = true;
            player.buffImmune[mod.BuffType("LightningRod")] = true;
            player.buffImmune[mod.BuffType("LivingWasteland")] = true;
            player.buffImmune[mod.BuffType("Lovestruck")] = true;
            player.buffImmune[mod.BuffType("MarkedforDeath")] = true;
            player.buffImmune[mod.BuffType("Midas")] = true;
            player.buffImmune[mod.BuffType("MutantNibble")] = true;
            player.buffImmune[mod.BuffType("NullificationCurse")] = true;
            player.buffImmune[mod.BuffType("Oiled")] = true;
            player.buffImmune[mod.BuffType("OceanicMaul")] = true;
            player.buffImmune[mod.BuffType("Purified")] = true;
            player.buffImmune[mod.BuffType("ReverseManaFlow")] = true;
            player.buffImmune[mod.BuffType("Rotting")] = true;
            player.buffImmune[mod.BuffType("Shadowflame")] = true;
            player.buffImmune[mod.BuffType("SqueakyToy")] = true;
            player.buffImmune[mod.BuffType("Swarming")] = true;
            player.buffImmune[mod.BuffType("Stunned")] = true;
            player.buffImmune[mod.BuffType("Unstable")] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            FargoSoulsGlobalNPC fargoNPC = npc.GetGlobalNPC<FargoSoulsGlobalNPC>();
            npc.poisoned = true;
            npc.venom = true;
            npc.ichor = true;
            npc.onFire2 = true;
            npc.betsysCurse = true;
            npc.midas = true;
            fargoNPC.Electrified = true;
            fargoNPC.OceanicMaul = true;
            fargoNPC.CurseoftheMoon = true;
            fargoNPC.Infested = true;
            fargoNPC.Rotting = true;
            fargoNPC.MutantNibble = true;
            fargoNPC.Sadism = true;
        }
    }
}