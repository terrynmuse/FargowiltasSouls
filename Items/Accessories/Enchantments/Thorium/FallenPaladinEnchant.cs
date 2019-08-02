using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class FallenPaladinEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fallen Paladin Enchantment");
            Tooltip.SetDefault(
@"'Silently, they walk the dungeon halls'
Taking damage heals nearby allies equal to 15% of the damage taken
If an ally is below half health, you will gain increased healing abilities
Effects of Wynebgwrthucher and Rebirth Statuette");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //paladin set bonus
            thoriumPlayer.fallenPaladinSet = true;
            //wyne
            thoriumPlayer.Wynebgwrthucher = true;
            //rebirth statue
            thoriumPlayer.quickRevive = true;
            //templar set bonus
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && player2.statLife < (int)(player2.statLifeMax2 * 0.5) && player2 != player)
                {
                    player.AddBuff(thorium.BuffType("HealingMastery"), 120, false);
                }
            }
        }
        
        private readonly string[] items =
        {
            "Wynebgwrthucher",
            "RebirthStatuette",
            "TwilightStaff",
            "HolyHammer",
            "SpiritFireWand",
            "PillPopper"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("FallenPaladinFacegaurd"));
            recipe.AddIngredient(thorium.ItemType("FallenPaladinCuirass"));
            recipe.AddIngredient(thorium.ItemType("FallenPaladinGreaves"));
            recipe.AddIngredient(null, "TemplarEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
