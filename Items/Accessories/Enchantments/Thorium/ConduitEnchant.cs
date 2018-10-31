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
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Conduit Enchantment");
            Tooltip.SetDefault(
                @"''
+50% martian weapon damage
Moving around generates up to 5 static rings, with each one generating life shielding
When fully charged, a bubble of energy will protect you from one attack 
When the bubble blocks an attack, an electrical discharge is released at nearby enemies
Summons a planetary visitor
Summons a pet probe that has offensive capabilities");
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
            
            ConduitEffect(player);
        }
        
        private void ConduitEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.martianDamage += 0.5f;
            thoriumPlayer.conduitSet = true;
            thoriumPlayer.orbital = true;
            //thoriumPlayer.orbitalRotation1 = Utils.RotatedBy(((ThoriumPlayer)player.GetModPlayer(base.mod, "ThoriumPlayer")).orbitalRotation1, -0.10000000149011612, default(Vector2));
            Lighting.AddLight(player.position, 0.2f, 0.35f, 0.7f);
            if ((player.velocity.X > 0f || player.velocity.X < 0f) && ((ThoriumPlayer)player.GetModPlayer(mod, "ThoriumPlayer")).circuitStage < 6)
            {
                ((ThoriumPlayer)player.GetModPlayer(mod, "ThoriumPlayer")).circuitCharge++;
                for (int i = 0; i < 1; i++)
                {
                    int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 185, 0f, 0f, 100, default(Color), 1f);
                    Main.dust[num].noGravity = true;
                }
            }
        }
        
        private readonly string[] items =
        {
            "ConduitHelmet",
            "ConduitSuit",
            "ConduitLeggings",
            "UFOCommunicator", //strange communicator?
            "VegaPhaser",
            "SuperPlasmaCannon",
            "LivewireCrasher",
            "Triangle"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(ItemID.BrainScrambler);
            recipe.AddIngredient(thorium.ItemType("OmegaDrive"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
