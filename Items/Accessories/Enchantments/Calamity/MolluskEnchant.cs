﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.CalPlayer;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class MolluskEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mollusk Enchantment");
            Tooltip.SetDefault(
@"'The world is your oyster'
Two shellfishes aid you in combat
Effects of Giant Pearl and Amidias' Pendant
Summons a Danny Devito pet");
            DisplayName.AddTranslation(GameCulture.Chinese, "软壳魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'世界任你驰骋'
召唤2个海贝为你而战
拥有大珍珠和阿米迪亚斯之垂饰的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();

            if (SoulConfig.Instance.GetValue("Shellfish Minions"))
            {
                //set bonus clams
                modPlayer.molluskSet = true;
                player.maxMinions += 4;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("Shellfish")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("Shellfish"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.ProjectileType("Shellfish")] < 2)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("Shellfish"), (int)(1500.0 * (double)player.minionDamage), 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            if (SoulConfig.Instance.GetValue("Giant Pearl"))
            {
                modPlayer.giantPearl = true;
                Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 0.45f, 0.8f, 0.8f);
            }

            if (SoulConfig.Instance.GetValue("Amidias' Pendant"))
            {
                calamity.GetItem("AmidiasPendant").UpdateAccessory(player, hideVisual);
            }

            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            fargoPlayer.MolluskEnchant = true;
            fargoPlayer.AddPet("Danny Pet", hideVisual, calamity.BuffType("DannyDevito"), calamity.ProjectileType("DannyDevito"));
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("MolluskShellmet"));
            recipe.AddIngredient(calamity.ItemType("MolluskShellplate"));
            recipe.AddIngredient(calamity.ItemType("MolluskShelleggings"));
            recipe.AddIngredient(calamity.ItemType("GiantPearl"));
            recipe.AddIngredient(calamity.ItemType("AmidiasPendant"));
            recipe.AddIngredient(calamity.ItemType("BlackAnurian"));
            recipe.AddIngredient(calamity.ItemType("Archerfish"));
            recipe.AddIngredient(calamity.ItemType("Lionfish"));
            recipe.AddIngredient(calamity.ItemType("HerringStaff"));
            recipe.AddIngredient(calamity.ItemType("SeafoamBomb"));
            recipe.AddIngredient(calamity.ItemType("AmidiasTrident"));
            recipe.AddIngredient(calamity.ItemType("EutrophicShank"));
            recipe.AddIngredient(calamity.ItemType("Serpentine"));
            recipe.AddIngredient(calamity.ItemType("TrashmanTrashcan"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
