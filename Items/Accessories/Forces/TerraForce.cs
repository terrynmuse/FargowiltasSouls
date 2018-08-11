using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class TerraForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Force");
            Tooltip.SetDefault(
@"''
Attacks have a chance to shock enemies with lightning
If an enemy is wet, the chance and damage is increased
Sets your critical strike chance to 10%
Every crit will increase it by 5%
Getting hit drops your crit back down
Allows the player to dash into the enemy
Right Click to guard with your shield
You attract items from a much larger range and fall 5 times as quickly
Attacks may inflict enemies with Lead Poisoning
Your weapons shoot at 1/3 the speed
100% increased damage
Grants immunity to fire blocks and lava
Increases armor penetration by 10
While standing in lava, you gain 10 more armor penetration, 50% attack speed, and your attacks ignite enemies");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 600000;
            item.shieldSlot = 5;
        }

        public override string Texture
        {
            get
            {
                return "FargowiltasSouls/Items/Placeholder";
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.TerraForce = true;
            //lightning
            modPlayer.CopperEnchant = true;
            //crit memes
            modPlayer.TinEnchant = true;
            modPlayer.AllCritEquals(modPlayer.TinCrit);
            //EoC Shield
            player.dash = 2;
            //shield raise stuff
            modPlayer.IronEffect();
            //item attract
            modPlayer.IronEnchant = true;
            player.maxFallSpeed *= 5;
            modPlayer.LeadEnchant = true;
            modPlayer.AllDamageUp(1f);
            player.fireWalk = true;
            player.lavaImmune = true;
            player.armorPenetration += 10;

            //in lava effects
            if (player.lavaWet)
            {
                player.armorPenetration += 10;
                modPlayer.ObsidianEnchant = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CopperEnchant");
            recipe.AddIngredient(null, "TinEnchant");
            recipe.AddIngredient(null, "IronEnchant");
            recipe.AddIngredient(null, "LeadEnchant");
            recipe.AddIngredient(null, "TungstenEnchant");
            recipe.AddIngredient(null, "ObsidianEnchant");

            if (Fargowiltas.Instance.FargosLoaded)
            {
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            }
            else
            {
                recipe.AddTile(TileID.LunarCraftingStation);
            }

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}