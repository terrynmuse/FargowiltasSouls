using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class SpectreEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spectre Enchantment");

            string tooltip = @"'Their lifeforce will be their own undoing'
Magic damage has a chance to spawn damaging orbs
If you crit, you get a burst of healing orbs instead
";

            if(thorium != null)
            {
                tooltip += "While worn, taking fatal damage will instead return you to 100 life and instantly teleport you back to your home (2 minute recharge time)\n";
            }

            tooltip += "Summons a pet Wisp";

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).SpectreEffect(hideVisual);

            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            if (!thoriumPlayer.lifePrevent)
            {
                player.AddBuff(thorium.BuffType("GhastlySoul"), 60, true);
            }
            thoriumPlayer.soulStorage = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.SpectreMask);
                recipe.AddIngredient(ItemID.SpectreHood);
                recipe.AddIngredient(ItemID.SpectreRobe);
                recipe.AddIngredient(ItemID.SpectrePants);
                recipe.AddIngredient(ItemID.GhostWings);
                recipe.AddIngredient(thorium.ItemType("GhastlyCarapace"));
                recipe.AddIngredient(ItemID.SpectreStaff);
                recipe.AddIngredient(thorium.ItemType("MusicSheet5"));
                recipe.AddIngredient(thorium.ItemType("EctoplasmicButterfly"));
            }
            else
            {
                recipe.AddRecipeGroup("FargowiltasSouls:AnySpectreHead");
                recipe.AddIngredient(ItemID.SpectreRobe);
                recipe.AddIngredient(ItemID.SpectrePants);
                recipe.AddIngredient(ItemID.SpectreHamaxe);
                recipe.AddIngredient(ItemID.SpectreStaff);
                recipe.AddIngredient(ItemID.UnholyTrident);
            }
            
            recipe.AddIngredient(ItemID.WispinaBottle);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
