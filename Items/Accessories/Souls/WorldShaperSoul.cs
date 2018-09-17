using Terraria;
using Terraria.ModLoader;
using static Terraria.ID.ItemID;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Back)]
    public class WorldShaperSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("World Shaper Soul");
            Tooltip.SetDefault(@"'Limitless possibilities'
Near infinite block placement and mining reach
Increased block and wall placement speed by 25% 
Mining speed doubled 
Auto paint and actuator effect 
Provides light 
Toggle vanity to enable Builder Mode:
Anything that creates a tile will not be consumed 
No enemies can spawn
");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 750000;
            item.expert = true;
            item.rare = -12;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).BuilderEffect = true;

            player.tileSpeed += 0.25f;
            player.wallSpeed += 0.25f;

            //toolbox
            Player.tileRangeX += 50;
            Player.tileRangeY += 50;

            //gizmo pack
            player.autoPaint = true;

            //pick axe stuff
            player.pickSpeed -= 0.50f;

            //mining helmet
            if (Soulcheck.GetValue("Shine Buff") == false) Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);
            //presserator
            player.autoActuator = true;

            if (!hideVisual) player.GetModPlayer<FargoPlayer>(mod).BuilderMode = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(LaserRuler);
                recipe.AddIngredient(GravityGlobe);
                recipe.AddIngredient(CellPhone);
                recipe.AddIngredient(PutridScent);
                recipe.AddIngredient(Toolbelt);
                recipe.AddIngredient(Toolbox);
                recipe.AddIngredient(ArchitectGizmoPack);
                recipe.AddIngredient(ActuationAccessory);


                recipe.AddRecipeGroup("FargowiltasSouls:AnyDrax");
                recipe.AddIngredient(ShroomiteDiggingClaw);
                recipe.AddIngredient(Picksaw);
                recipe.AddIngredient(LaserDrill);
                recipe.AddIngredient(DrillContainmentUnit);
                recipe.AddIngredient(RoyalGel);
            }

            //build.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}