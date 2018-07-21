using Terraria;
using Terraria.ModLoader;
using static Terraria.ID.ItemID;

namespace FargowiltasSouls.Items.Accessories.Souls
{
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
            if (Soulcheck.GetValue("Shine Buff") == false)
            {
                Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);
            }
            //presserator
            player.autoActuator = true;

            if (!hideVisual)
            {
                /*player.magicDamage*= 0f;
                player.meleeDamage*= 0f;
                player.rangedDamage*= 0f;
                player.minionDamage*= 0f;
                player.thrownDamage*= 0f;*/

                player.GetModPlayer<FargoPlayer>(mod).BuilderMode = true;
            }

        }

        public override void AddRecipes()
        {
            ModRecipe build = new ModRecipe(mod);

            build.AddIngredient(Toolbelt);
            build.AddIngredient(Toolbox);
            build.AddIngredient(ArchitectGizmoPack);
            build.AddIngredient(ActuationAccessory);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    //thorium and calamity
                    build.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("AncientFossil"));
                    build.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("CrystalineCharm"));
                    build.AddRecipeGroup("FargowiltasSouls:AnyDrax");
                    build.AddIngredient(ShroomiteDiggingClaw);
                    build.AddIngredient(Picksaw);
                    build.AddIngredient(LaserDrill);
                    build.AddIngredient(DrillContainmentUnit);
                    build.AddIngredient(PeaceCandle, 10);
                    build.AddIngredient(RoyalGel);
                    build.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("OceanCrest"));
                }

                if (!Fargowiltas.Instance.CalamityLoaded)
                {
                    //just thorium
                    build.AddIngredient(MiningHelmet);
                    build.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("CrystalineCharm"));
                    build.AddRecipeGroup("FargowiltasSouls:AnyDrax");
                    build.AddIngredient(ShroomiteDiggingClaw);
                    build.AddIngredient(Picksaw);
                    build.AddIngredient(LaserDrill);
                    build.AddIngredient(DrillContainmentUnit);
                    build.AddIngredient(Sunflower, 50);
                    build.AddIngredient(PeaceCandle, 10);
                    build.AddIngredient(RoyalGel);
                }
            }

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    //just calamity
                    build.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("AncientFossil"));
                    build.AddRecipeGroup("FargowiltasSouls:AnyDrax");
                    build.AddIngredient(ShroomiteDiggingClaw);
                    build.AddIngredient(MoltenPickaxe);
                    build.AddIngredient(Picksaw);
                    build.AddIngredient(LaserDrill);
                    build.AddIngredient(DrillContainmentUnit);
                    build.AddIngredient(PeaceCandle, 10);
                    build.AddIngredient(RoyalGel);
                    build.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("OceanCrest"));
                }

                if (!Fargowiltas.Instance.CalamityLoaded)
                {
                    //no others
                    build.AddRecipeGroup("FargowiltasSouls:AnyDrax");
                    build.AddIngredient(ShroomiteDiggingClaw);
                    build.AddIngredient(MoltenPickaxe);
                    build.AddIngredient(Picksaw);
                    build.AddIngredient(LaserDrill);
                    build.AddIngredient(DrillContainmentUnit);
                    build.AddIngredient(Sunflower, 50);
                    build.AddIngredient(PeaceCandle, 10);
                    build.AddIngredient(RoyalGel);
                }
            }

            //build.AddTile(null, "CrucibleCosmosSheet");
            build.SetResult(this);
            build.AddRecipe();
        }
    }
}