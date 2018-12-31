using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class CryoMagusEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryo Magus Enchantment");
            Tooltip.SetDefault(
@"''
Magic damage will duplicate itself for 33% of the damage and apply the Frozen debuff to hit enemies
Icicles will start to appear around you
When there are three, attacking will launch them towards the cursor
An icy aura surrounds you, which freezes nearby enemies after a short delay
Damage done against slowed targets is increased by 15% and has a chance to heal you lightly
Your symphonic damage will empower all nearby allies with: Mana Regeneration II
Your symphonic damage empowers all nearby allies with: Cold Shoulder
Damage done against frostburnt enemies is increased by 8% 
Doubles the range of your empowerments effect radius
Summons a pet Penguin, Snowman, and Owl");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //cryo set bonus, dmg duplicate
            thoriumPlayer.cryoSet = true;
            //strider hide
            thoriumPlayer.frostBonusDamage = true;
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3ManaRegen = 2;
            //pet
            modPlayer.AddPet("Owl Pet", hideVisual, thorium.BuffType("SnowyOwlBuff"), thorium.ProjectileType("SnowyOwlPet"));
            thoriumPlayer.snowyOwl = true;
            //frost effect
            modPlayer.FrostEffect(60, hideVisual);
            //subwoofer
            thoriumPlayer.subwooferFrost = true;
            thoriumPlayer.bardRangeBoost += 450;
            //icy set bonus
            thoriumPlayer.icySet = true;
            if (player.ownedProjectileCounts[thorium.ProjectileType("IcyAura")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("IcyAura"), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
        
        private readonly string[] items =
        {
            "IceBoundStriderHide",
            "TunePlayerManaRegen",
            "IceFairyStaff",
            "FrostBurntTongue",
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("CryoMagusSpark"));
            recipe.AddIngredient(thorium.ItemType("CryoMagusTabard"));
            recipe.AddIngredient(thorium.ItemType("CryoMagusLeggings"));
            recipe.AddIngredient(null, "FrostEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(ItemID.FrostStaff);
            recipe.AddIngredient(thorium.ItemType("LostMail"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
