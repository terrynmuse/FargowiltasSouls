using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CalamityMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class AtaxiaEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("CalamityMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ataxia Enchantment");
            Tooltip.SetDefault(
@"'Not be confused with Ataraxia Enchantment'
Inferno effect when below 50% life
You have a 20% chance to emit a blazing explosion on hit
Melee attacks and projectiles cause chaos flames to erupt on enemy hits
You have a 50% chance to fire a homing chaos flare when using ranged weapons
Magic attacks summon damaging and healing flare orbs on hit
Summons a chaos spirit to protect you
Rogue weapons have a 10% chance to unleash a volley of chaos flames around the player that chase enemies when used");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);

            //all
            modPlayer.ataxiaBlaze = true;
            if (player.statLife <= player.statLifeMax2 * 0.5f)
            {
                player.AddBuff(116, 2, true);
            }
            //melee
            modPlayer.ataxiaGeyser = true;
            //range
            modPlayer.ataxiaBolt = true;
            //magic
            modPlayer.ataxiaMage = true;
            //summon
            modPlayer.chaosSpirit = true;
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
            modPlayer.ataxiaVolley = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

/*Vesuvius
Omniblade
Tumbleweed
Sand Sharknado Staff
Forbidden Sun
Barracuda Gun
Impaler
Lucrecia
Holiday Halberd
Soul Harvester
Plague Hive
The Hive*/


            recipe.AddIngredient(calamity.ItemType("AtaxiaHelm"));
            recipe.AddIngredient(calamity.ItemType("AtaxiaHeadgear"));
            recipe.AddIngredient(calamity.ItemType("AtaxiaMask"));
            recipe.AddIngredient(calamity.ItemType("AtaxiaHelmet"));
            recipe.AddIngredient(calamity.ItemType("AtaxiaHood"));
            recipe.AddIngredient(calamity.ItemType("AtaxiaArmor"));
            recipe.AddIngredient(calamity.ItemType("AtaxiaSubligar"));
            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));
            recipe.AddIngredient(calamity.ItemType(""));

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
