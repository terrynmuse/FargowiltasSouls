using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;
using Terraria.Localization;

namespace FargowiltasSouls.Buffs.MutantBoss
{
    public class MutantPresence : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Mutant Presence");
            Description.SetDefault("Defense, damage reduction, and life regen reduced; all soul toggles disabled; Chaos State effect");
            DisplayName.AddTranslation(GameCulture.Chinese, "突变驾到");
            Description.AddTranslation(GameCulture.Chinese, "一位压倒性的存在削弱了你的存在性");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
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
            //also halves defense, DR, and cripples life regen
            player.GetModPlayer<FargoPlayer>(mod).noDodge = true;
            player.GetModPlayer<FargoPlayer>(mod).noSupersonic = true;
            player.GetModPlayer<FargoPlayer>(mod).MutantPresence = true;
            player.moonLeech = true;
            player.chaosState = true;
        }
    }
}