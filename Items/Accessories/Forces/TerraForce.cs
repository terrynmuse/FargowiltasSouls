using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class TerraForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Force");
            string tooltip = "''\n";

            if (thorium != null)
            {
                tooltip +=
@"While in combat, you generate a 25 life shield
";
            }

            tooltip +=
@"Attacks have a chance to shock enemies with lightning
If an enemy is wet, the chance and damage is increased
Attacks that cause Wet cannot proc the lightning
Lightning scales with magic damage
Allows the player to dash into the enemy
Right Click to guard with your shield
You attract items from a larger range
Attacks may inflict enemies with Lead Poisoning
Grants immunity to fire blocks and lava
Increases armor penetration by 10
While standing in lava, you gain 10 more armor penetration, 15% attack speed, and your attacks ignite enemies
Sets your critical strike chance to 10%
Every crit will increase it by 5%
Getting hit drops your crit back down
";

            Tooltip.SetDefault(tooltip);
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

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //crit effect improved, smaller tungsten effect
            modPlayer.TerraForce = true;
            //lightning
            modPlayer.CopperEnchant = true;
            //EoC Shield
            player.dash = 2;
            //magnet, shield
            modPlayer.IronEffect();
            //lead poison
            modPlayer.LeadEnchant = true;
            //lava immune, armor pen
            modPlayer.ObsidianEffect();
            //crits
            modPlayer.TinEffect();
            //slower, stronger
            modPlayer.TungstenEffect();
            

            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            //copper buckler
            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            thoriumPlayer.metallurgyShield = true;
            player.statDefense++;
            if (!thoriumPlayer.outOfCombat)
            {
                timer++;
                if (timer >= 30)
                {
                    int num = 25;
                    if (thoriumPlayer.shieldHealth < num)
                    {
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 255, 255), 1, false, true);
                        thoriumPlayer.shieldHealth++;
                    }
                    timer = 0;
                    return;
                }
            }
            else
            {
                timer = 0;
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
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}