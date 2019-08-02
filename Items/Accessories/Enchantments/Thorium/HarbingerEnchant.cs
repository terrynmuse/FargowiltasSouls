using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class HarbingerEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harbinger Enchantment");
            Tooltip.SetDefault(
@"'Doom comes next'
Maximum mana increased by 50%
While above 75% maximum mana, you become unstable
Magical attacks have a 33% chance to recover some mana
Effects of Shade Band and White Music Player
Summons a Moogle pet");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //set bonus
            player.statManaMax2 += (int)(player.statManaMax2 * 0.5);
            if (player.statMana > (int)(player.statManaMax2 * 0.75) || player.statMana > 300)
            {
                player.AddBuff(thorium.BuffType("Overcharge"), 2, true);
                player.magicDamage += 0.5f;
                player.magicCrit += 26;
            }
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3MaxLife = 2;
            //white knight set bonus
            thoriumPlayer.whiteKnightSet = true;
            //shade band
            thoriumPlayer.shadeBand = true;
            //pet
            modPlayer.AddPet("Moogle Pet", hideVisual, thorium.BuffType("LilMogBuff"), thorium.ProjectileType("LilMog"));
            modPlayer.KnightEnchant = true;
        }
        
        private readonly string[] items =
        {
            "TunePlayerMaxLife",
            "NightStaff",
            "BlackholeCannon",
            "GodKiller",
            "HarbingerSpear",
            "HarbingerBow"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("HarbingerHelmet"));
            recipe.AddIngredient(thorium.ItemType("HarbingerChestGuard"));
            recipe.AddIngredient(thorium.ItemType("HarbingerGreaves"));
            recipe.AddIngredient(null, "WhiteKnightEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
