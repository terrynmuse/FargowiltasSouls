using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class NymphsPerfume : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nymph's Perfume");
            Tooltip.SetDefault(@"'The scent is somewhat overpowering'
Grants immunity to Lovestruck and Stinky
Your attacks occasionally produce hearts");
            DisplayName.AddTranslation(GameCulture.Chinese, "染血女神的香水");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'气味有点太浓了'
免疫热恋和恶臭
攻击偶尔会生成心");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 5;
            item.value = Item.sellPrice(0, 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Lovestruck] = true;
            player.buffImmune[mod.BuffType("Lovestruck")] = true;
            player.buffImmune[BuffID.Stinky] = true;
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            fargoPlayer.NymphsPerfume = true;
            if (fargoPlayer.NymphsPerfumeCD > 0)
                fargoPlayer.NymphsPerfumeCD--;
        }
    }
}
