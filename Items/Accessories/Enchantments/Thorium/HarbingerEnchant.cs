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
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harbinger Enchantment");
            Tooltip.SetDefault(
@"''
Maximum mana increased by 50%
While above 75% maximum mana, you become unstable
Your symphonic damage will empower all nearby allies with: Maximum Life II");
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
            
            HarbingerEffect(player);
        }
        
        private void HarbingerEffect(Player player)
        {
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
        }
        
        private readonly string[] items =
        {
            "HarbingerHelmet",
            "HarbingerChestGuard",
            "HarbingerGreaves",
            "TunePlayerMaxLife",
            "NightStaff",
            "BlackholeCannon",
            "GodKiller",
            "HarbingerSpear",
            "HarbingerBow",
            "HarbingerSurgeWand"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
