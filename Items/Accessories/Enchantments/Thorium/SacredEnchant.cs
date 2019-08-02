using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class SacredEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sacred Enchantment");
            Tooltip.SetDefault(
@"'It glimmers with comforting power'
Healing spells heal an additional 5 life
Every 5 seconds you generate up to 3 holy crosses
When casting healing spells, a cross is used instead of mana
Summons a Li'l Cherub to periodically heal damaged allies
Summons a pet Life Spirit");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 4;
            item.value = 120000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //sacred set bonus
            thoriumPlayer.healBonus += 5;
            //novice cleric set bonus
            thoriumPlayer.clericSet = true;
            thoriumPlayer.orbital = true;
            thoriumPlayer.orbitalRotation3 = Utils.RotatedBy(thoriumPlayer.orbitalRotation3, -0.05000000074505806, default(Vector2));
            timer++;
            if (thoriumPlayer.clericSetCrosses < 3)
            {
                if (timer > 300)
                {
                    thoriumPlayer.clericSetCrosses++;
                    timer = 0;
                    return;
                }
            }
            else
            {
                timer = 0;
            }
            //lil cherub
            modPlayer.SacredEnchant = true;
            modPlayer.AddMinion("Li'l Cherub Minion", thorium.ProjectileType("Angel"), 0, 0f);
            //twinkle pet
            modPlayer.AddPet("Life Spirit Pet", hideVisual, thorium.BuffType("LifeSpiritBuff"), thorium.ProjectileType("LifeSpirit"));
            thoriumPlayer.lifePet = true;
        }
        
        private readonly string[] items =
        {
            "RegenStaff",
            "LightBurstWand",
            "HallowedBludgeon",
            "AngelStaff",
            "Liberation",
            "Twinkle"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("HallowedPaladinHelmet"));
            recipe.AddIngredient(thorium.ItemType("HallowedPaladinBreastplate"));
            recipe.AddIngredient(thorium.ItemType("HallowedPaladinLeggings"));
            recipe.AddIngredient(null, "NoviceClericEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
