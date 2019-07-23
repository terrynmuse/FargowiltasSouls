using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class MagmaEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public bool allowJump = true;
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magma Enchantment");
            Tooltip.SetDefault(
@"'Bursting with heat'
Fire surrounds your armour and melee weapons
Enemies that you set on fire or singe will take additional damage over time
Effects of Spring Steps, Slag Stompers, and Molten Spear Tip");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 60000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //set bonus
            player.magmaStone = true;
            thoriumPlayer.magmaSet = true;
            //spring steps
            thorium.GetItem("SpringSteps").UpdateAccessory(player, hideVisual);

            if (Soulcheck.GetValue("Slag Stompers"))
            {
                //slag stompers
                timer++;
                if (timer > 20)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0.1f * Main.rand.Next(-25, 25), 2f, thorium.ProjectileType("SlagPro"), 20, 1f, Main.myPlayer, 0f, 0f);
                    timer = 0;
                }
            }
            //molten spear tip
            thoriumPlayer.spearFlame = true;
        }
        
        private readonly string[] items =
        {
            "MagmaHelmet",
            "MagmaChestGuard",
            "MagmaGreaves",
            "SpringSteps",
            "SlagStompers",
            "MoltenSpearTip",
            "MagmaShiv",
            "MagmaPolearm",
            "MagmaticRicochet",
            "MagmaFlail"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
