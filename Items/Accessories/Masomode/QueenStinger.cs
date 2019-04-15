using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class QueenStinger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Queen's Stinger");
            Tooltip.SetDefault("'Ripped right off of a defeated foe..' \nIncreases armor penetration by 10 \nYou are immune to Venom,  Bees and Hornets");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 3;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //pen
            player.armorPenetration = 10;

            //bees
            player.npcTypeNoAggro[210] = true;
            player.npcTypeNoAggro[211] = true;

            //hornets
            player.npcTypeNoAggro[42] = true;
            player.npcTypeNoAggro[176] = true;
            player.npcTypeNoAggro[231] = true;
            player.npcTypeNoAggro[232] = true;
            player.npcTypeNoAggro[233] = true;
            player.npcTypeNoAggro[234] = true;
            player.npcTypeNoAggro[235] = true;

            //stinger immmune/poison
            player.GetModPlayer<FargoPlayer>(mod).QueenStinger = true;
        }
    }
}
