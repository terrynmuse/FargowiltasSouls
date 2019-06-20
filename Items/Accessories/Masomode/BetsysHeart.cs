using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class BetsysHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Betsy's Heart");
            Tooltip.SetDefault(@"'Lightly roasted, medium rare'
Grants immunity to Oozed, Withered Weapon, and Withered Armor
Your critical strikes inflict Betsy's Curse");
            DisplayName.AddTranslation(GameCulture.Chinese, "贝特希之心");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'微烤,五分熟'
免疫渗出,枯萎武器和枯萎盔甲
暴击造成贝特希的诅咒");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 9;
            item.value = Item.sellPrice(0, 7);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.OgreSpit] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            player.GetModPlayer<FargoPlayer>().BetsysHeart = true;
        }
    }
}
