using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class ConduitEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Conduit Enchantment");
            Tooltip.SetDefault(
@"'Shocked out of this world'
A meteor shower initiates every few seconds while attacking
Moving around generates up to 5 static rings, with each one generating life shielding
When fully charged, a bubble of energy will protect you from one attack 
When the bubble blocks an attack, an electrical discharge is released at nearby enemies
Summons a pet Omega, I.F.O., and Bio-Feeder");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
           
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            if (Soulcheck.GetValue("Conduit Shield"))
            {
                //conduit set bonus
                thoriumPlayer.conduitSet = true;
                thoriumPlayer.orbital = true;
                thoriumPlayer.orbitalRotation1 = Utils.RotatedBy(thoriumPlayer.orbitalRotation1, -0.10000000149011612, default(Vector2));
                Lighting.AddLight(player.position, 0.2f, 0.35f, 0.7f);
                if ((player.velocity.X > 0f || player.velocity.X < 0f) && thoriumPlayer.circuitStage < 6)
                {
                    thoriumPlayer.circuitCharge++;
                    for (int i = 0; i < 1; i++)
                    {
                        int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 185, 0f, 0f, 100, default(Color), 1f);
                        Main.dust[num].noGravity = true;
                    }
                }
            }
            //meteor effect
            modPlayer.MeteorEffect(60);
            //pets
            //modPlayer.AddPet("Omega Pet", hideVisual, thorium.BuffType("OmegaBuff"), thorium.ProjectileType("Omega"));
            modPlayer.AddPet("I.F.O. Pet", hideVisual, thorium.BuffType("Identified"), thorium.ProjectileType("IFO"));
            modPlayer.AddPet("Bio-Feeder Pet", hideVisual, thorium.BuffType("BioFeederBuff"), thorium.ProjectileType("BioFeederPet"));
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("ConduitHelmet"));
            recipe.AddIngredient(thorium.ItemType("ConduitSuit"));
            recipe.AddIngredient(thorium.ItemType("ConduitLeggings"));
            recipe.AddIngredient(null, "MeteorEnchant");
            recipe.AddIngredient(thorium.ItemType("VegaPhaser"));
            recipe.AddIngredient(thorium.ItemType("LivewireCrasher"));
            recipe.AddIngredient(thorium.ItemType("SuperPlasmaCannon"));
            recipe.AddIngredient(thorium.ItemType("Triangle"));
            recipe.AddIngredient(thorium.ItemType("OmegaDrive"));
            recipe.AddIngredient(thorium.ItemType("UFOCommunicator"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
