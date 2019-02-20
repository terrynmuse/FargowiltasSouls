using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class CalamityForce : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("CalamityMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Calamities");
            Tooltip.SetDefault(
@"''
All armor bonuses from Victide, Aerospec, and Statigel
All armor bonuses from Daedalus, Reaver, and Astral
All armor bonuses from Ataxia and Xeroc
Effects of the Spirit Glyph and Raider's Talisman
Effects of the Permafrost's Concoction, Astral Arcanum, Plague Hive");

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
            //WULFRUM
            //spirit glyph
            calamityPlayer.sGenerator = true;
            //raiders talisman
            calamityPlayer.raiderTalisman = true;
            //VICTIDE
            mod.GetItem("VictideEnchant").UpdateAccessory(player, hideVisual);
            //AEROSPEC
            mod.GetItem("AerospecEnchant").UpdateAccessory(player, hideVisual);
            //STATIGEL
            calamityPlayer.statigelSet = true;

            //DAEDALUS
            mod.GetItem("DaedalusEnchant").UpdateAccessory(player, hideVisual);
            //REAVER
            mod.GetItem("ReaverEnchant").UpdateAccessory(player, hideVisual);
            //ASTRAL
            mod.GetItem("AstralEnchant").UpdateAccessory(player, hideVisual);
            //ATAXIA
            calamityPlayer.ataxiaBlaze = true;
            //melee
            calamityPlayer.ataxiaGeyser = true;
            //range
            calamityPlayer.ataxiaBolt = true;
            //magic
            calamityPlayer.ataxiaMage = true;
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
            //throw
            calamityPlayer.ataxiaVolley = true;
            //plague hive
            player.buffImmune[calamity.BuffType("Plague")] = true;
            calamityPlayer.uberBees = true;
            player.strongBees = true;
            calamityPlayer.alchFlask = true;
            //XEROC
            calamityPlayer.xerocSet = true;
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
            recipe.AddIngredient(null, "AtaxiaEnchant");
            recipe.AddIngredient(null, "XerocEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
