using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class NecromanticBrew : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necromantic Brew");
            Tooltip.SetDefault(@"'The bone-growing solution of a defeated foe'
Grants immunity to Lethargic
Summons 2 Skeletron arms to whack enemies");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 4;
            item.value = Item.sellPrice(0, 3);
            item.defense = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("Lethargic")] = true;
            if (Soulcheck.GetValue("Skeletron Arms Minion"))
                player.AddBuff(mod.BuffType("SkeletronArms"), 2);
        }
    }
}
