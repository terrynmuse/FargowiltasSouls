using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces.Thorium
{
    public class AlfheimForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Alfheim");
            Tooltip.SetDefault(
@"'Elven mysteries unfold before you...'
Healing spells heal an additional 5 life and shortly increase the healed player's maximum life by 50
Every 5 seconds you generate up to 3 holy crosses
Radiant critical strikes will generate up to 15 shadow wisps
Pressing the 'Special Ability' key will unleash them towards your cursor's position
A biotech probe will assist you in healing your allies
Your radiant damage has a 15% chance to release a blinding flash of light
Taking damage heals nearby allies equal to 15% of the damage taken
If an ally is below half health, you will gain increased healing abilities
Effects of Demon Tongue, Aloe Leaf, and Equalizer 
Effects of Wynebgwrthucher and Rebirth Statuette
Summons a Li'l Cherub and Li'l Devil
Summons a pet Life Spirit and Holy Goat");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            //sacred
            mod.GetItem("SacredEnchant").UpdateAccessory(player, hideVisual);

            //warlock
            mod.GetItem("WarlockEnchant").UpdateAccessory(player, hideVisual);

            //biotech
            mod.GetItem("BiotechEnchant").UpdateAccessory(player, hideVisual);

            //life binder
            mod.GetItem("LifeBinderEnchant").UpdateAccessory(player, hideVisual);

            //fallen paladin
            mod.GetItem("FallenPaladinEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "SacredEnchant");
            recipe.AddIngredient(null, "WarlockEnchant");
            recipe.AddIngredient(null, "BiotechEnchant");
            recipe.AddIngredient(null, "LifeBinderEnchant");
            recipe.AddIngredient(null, "FallenPaladinEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
