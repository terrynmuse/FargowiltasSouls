using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class DemonBloodEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Blood Enchantment");
            Tooltip.SetDefault(
@"'Infused with Corrupt Blood'
Consecutive attacks against enemies might drop flesh, which grants bonus life and damage
Effects of Vampire Gland, Demon Blood Badge, and Vile Flail-Core
Effects of Blood Demon's Subwoofer and Yellow Music Player
Summons a pet Flying Blister");
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

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.demonbloodSet = true;
            //demon blood badge
            thoriumPlayer.CrimsonBadge = true;
            //vile core
            thoriumPlayer.vileCore = true;
            //subwoofer
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerIchor = true;
                }
            }
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3CriticalStrike = 2;
            //flesh set bonus
            thoriumPlayer.Symbiotic = true;
            //vampire gland
            thoriumPlayer.vampireGland = true;
            //blister pet
            modPlayer.AddPet("Blister Pet", hideVisual, thorium.BuffType("BlisterBuff"), thorium.ProjectileType("BlisterPet"));
            thoriumPlayer.blisterPet = true;
        }
        
        private readonly string[] items =
        {
            "DemonRageBadge",
            "VileCore",
            "CrimsonSubwoofer",
            "TunePlayerCritChance",
            "DarkContagionBook"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("DemonBloodHelmet"));
            recipe.AddIngredient(thorium.ItemType("DemonBloodBreastPlate"));
            recipe.AddIngredient(thorium.ItemType("DemonBloodGreaves"));
            recipe.AddIngredient(null, "FleshEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(thorium.ItemType("FesteringBalloon"), 300);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
