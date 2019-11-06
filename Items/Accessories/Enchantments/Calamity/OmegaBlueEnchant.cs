using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.CalPlayer;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class OmegaBlueEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Omega Blue Enchantment");
            Tooltip.SetDefault(
@"'The darkness of the Abyss has overwhelmed you...'
Increases armor penetration by 100
Short-ranged tentacles heal you by sucking enemy life
Press Y to activate abyssal madness for 5 seconds
Abyssal madness increases damage, critical strike chance, and tentacle aggression/range
This effect has a 30 second cooldown
Effects of the Abyssal Diving Suit, Lumenous Amulet, and Aquatic Emblem
Summons a Siren pet");
            DisplayName.AddTranslation(GameCulture.Chinese, "蓝色欧米茄魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'深渊的黑暗淹没了你...'
增加100点护甲穿透
触手会吸取敌人的生命治愈你
按'Y'键开启5秒'深渊狂乱'状态
此状态下增加攻击, 暴击率, 触手攻击性和射程");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 13;
            item.value = 1000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            player.ignoreWater = true;

            if (SoulConfig.Instance.GetValue("Omega Blue Tentacles"))
            {
                modPlayer.omegaBlueSet = true;
                if (modPlayer.omegaBlueCooldown > 0)
                {
                    if (modPlayer.omegaBlueCooldown == 1)
                    {
                        for (int i = 0; i < 66; i++)
                        {
                            int num = Dust.NewDust(player.position, player.width, player.height, 20, 0f, 0f, 100, Color.Transparent, 2.6f);
                            Main.dust[num].noGravity = true;
                            Main.dust[num].noLight = true;
                            Main.dust[num].fadeIn = 1f;
                            Main.dust[num].velocity *= 6.6f;
                        }
                    }
                    modPlayer.omegaBlueCooldown--;
                }
                if (modPlayer.omegaBlueCooldown > 1500)
                {
                    modPlayer.omegaBlueHentai = true;
                    int num2 = Dust.NewDust(player.position, player.width, player.height, 20, 0f, 0f, 100, Color.Transparent, 1.6f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].noLight = true;
                    Main.dust[num2].fadeIn = 1f;
                    Main.dust[num2].velocity *= 3f;
                }
            }

            //abyssal diving suit
            if (SoulConfig.Instance.GetValue("Abyssal Diving Suit"))
            {
                //because screw that slow speed out of water ech
                if (!Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
                {
                    player.runAcceleration *= 2.5f;
                    player.maxRunSpeed *= 2.5f;
                }
                modPlayer.abyssalDivingSuit = true;
                if (hideVisual)
                {
                    modPlayer.abyssalDivingSuitHide = true;
                }
            }

            //lumenous amulet
            modPlayer.abyssalAmulet = true;
            modPlayer.lumenousAmulet = true;
            //reaper tooth necklace
            player.armorPenetration += 100;
            //aquatic emblem
            modPlayer.aquaticEmblem = true;

            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            fargoPlayer.OmegaBlueEnchant = true;
            fargoPlayer.AddPet("Siren Pet", hideVisual, calamity.BuffType("StrangeOrb"), calamity.ProjectileType("SirenYoung"));
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("OmegaBlueHelmet"));
            recipe.AddIngredient(calamity.ItemType("OmegaBlueChestplate"));
            recipe.AddIngredient(calamity.ItemType("OmegaBlueLeggings"));
            recipe.AddIngredient(calamity.ItemType("AbyssalDivingSuit"));
            recipe.AddIngredient(calamity.ItemType("LumenousAmulet"));
            recipe.AddIngredient(calamity.ItemType("ReaperToothNecklace"));
            recipe.AddIngredient(calamity.ItemType("AquaticEmblem"));
            recipe.AddIngredient(calamity.ItemType("HalibutCannon"));
            recipe.AddIngredient(calamity.ItemType("CalamarisLament"));
            recipe.AddIngredient(calamity.ItemType("TheReaper"));
            recipe.AddIngredient(calamity.ItemType("EidolicWail"));
            recipe.AddIngredient(calamity.ItemType("SDFMG"));
            recipe.AddIngredient(calamity.ItemType("NeptunesBounty"));
            recipe.AddIngredient(calamity.ItemType("StrangeOrb"));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
