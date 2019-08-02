using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class CalamityForce : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Calamities");
            Tooltip.SetDefault(
@"'Take only lives, leave only wastelands'
All armor bonuses from Victide, Aerospec, and Statigel
All armor bonuses from Daedalus, Reaver, and Astral
All armor bonuses from Mollusk, Ataxia, and Xeroc
Effects of Permafrost's Concoction, Astral Arcanum, and Hide of Astrum Deus
Effects of Plague Hive, Giant Pearl, and Amidias' Pendant");
            DisplayName.AddTranslation(GameCulture.Chinese, "灾厄之力");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'只带走生命,只留下废土'
拥有胜潮,天蓝和斯塔提斯的套装效果
拥有代达罗斯,掠夺者和星幻的套装效果
拥有软壳,阿塔西亚和克希洛克的套装效果
拥有佩码·福洛斯特之融魔台,星陨幻空石和星神游龙外壳的效果
拥有瘟疫蜂巢,大珍珠和阿米迪亚斯之垂饰的效果");

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
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            CalamityPlayer calamityPlayer = player.GetModPlayer<CalamityPlayer>(calamity);

            if (!modPlayer.TerrariaSoul)
            {
                //WULFRUM
                //spirit glyph
                calamityPlayer.sGenerator = true;
                //raiders talisman
                calamityPlayer.raiderTalisman = true;
                //trinket of chi
                calamityPlayer.trinketOfChi = true;
            }
            
            //VICTIDE
            mod.GetItem("VictideEnchant").UpdateAccessory(player, hideVisual);
            //AEROSPEC
            mod.GetItem("AerospecEnchant").UpdateAccessory(player, hideVisual);
            //STATIGEL
            calamityPlayer.statigelSet = true;
            calamity.GetItem("FungalSymbiote").UpdateAccessory(player, hideVisual);
            if (Soulcheck.GetValue("Slime God Minion"))
            {
                //summon
                calamityPlayer.slimeGod = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("SlimeGod")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("SlimeGod"), 3600, true);
                    }
                    if (WorldGen.crimson && player.ownedProjectileCounts[calamity.ProjectileType("SlimeGodAlt")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("SlimeGodAlt"), 33, 0f, Main.myPlayer, 0f, 0f);
                        return;
                    }
                    if (!WorldGen.crimson && player.ownedProjectileCounts[calamity.ProjectileType("SlimeGod")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("SlimeGod"), 33, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            //DAEDALUS
            mod.GetItem("DaedalusEnchant").UpdateAccessory(player, hideVisual);
            //REAVER
            mod.GetItem("ReaverEnchant").UpdateAccessory(player, hideVisual);
            //ASTRAL
            mod.GetItem("AstralEnchant").UpdateAccessory(player, hideVisual);
            //MOLLUSK
            mod.GetItem("MolluskEnchant").UpdateAccessory(player, hideVisual);
            //ATAXIA
            if (Soulcheck.GetValue("Ataxia Effects"))
            {
                calamityPlayer.ataxiaBlaze = true;
                //melee
                calamityPlayer.ataxiaGeyser = true;
                //range
                calamityPlayer.ataxiaBolt = true;
                //magic
                calamityPlayer.ataxiaMage = true;
                //throw
                calamityPlayer.ataxiaVolley = true;
            }
                
            if (Soulcheck.GetValue("Chaos Spirit Minion"))
            {
                //summon
                calamityPlayer.chaosSpirit = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("ChaosSpirit")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("ChaosSpirit"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.ProjectileType("ChaosSpirit")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("ChaosSpirit"), 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            
            if (Soulcheck.GetValue("Plague Hive"))
            {
                //plague hive
                player.buffImmune[calamity.BuffType("Plague")] = true;
                calamityPlayer.uberBees = true;
                player.strongBees = true;
                calamityPlayer.alchFlask = true;
            }

            //XEROC
            if (Soulcheck.GetValue("Xeroc Effects"))
            {
                calamityPlayer.xerocSet = true;
            }
        }


        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "WulfrumEnchant");
            recipe.AddIngredient(null, "VictideEnchant");
            recipe.AddIngredient(null, "AerospecEnchant");
            recipe.AddIngredient(null, "StatigelEnchant");
            recipe.AddIngredient(null, "DaedalusEnchant");
            recipe.AddIngredient(null, "ReaverEnchant");
            recipe.AddIngredient(null, "AstralEnchant");
            recipe.AddIngredient(null, "MolluskEnchant");
            recipe.AddIngredient(null, "AtaxiaEnchant");
            recipe.AddIngredient(null, "XerocEnchant");

            recipe.AddTile(mod, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
