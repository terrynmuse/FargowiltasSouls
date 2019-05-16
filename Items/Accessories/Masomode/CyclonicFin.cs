using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class CyclonicFin : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cyclonic Fin");
            Tooltip.SetDefault(@"'The wind is howling'
Grants immunity to Frozen, Curse of the Moon, and Oceanic Maul
Your attacks inflict Curse of the Moon and Oceanic Maul
Spectral Fishron periodically manifests to support your critical hits
Spectral Fishron also inflicts Mutant Nibble, disabling enemy healing");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 11;
            item.value = Item.sellPrice(0, 17);
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(Main.DiscoR, 51, 255 - (int)(Main.DiscoR * 0.4));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[mod.BuffType("OceanicMaul")] = true;
            player.buffImmune[mod.BuffType("CurseoftheMoon")] = true;
            player.GetModPlayer<FargoPlayer>().CyclonicFin = true;
            if (player.GetModPlayer<FargoPlayer>().CyclonicFinCD > 0)
                player.GetModPlayer<FargoPlayer>().CyclonicFinCD--;
        }
    }
}
