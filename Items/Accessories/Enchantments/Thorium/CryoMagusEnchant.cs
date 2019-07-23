using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class CryoMagusEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryo-Magus Enchantment");
            Tooltip.SetDefault(
@"'What killed the dinosaurs? The ice age!'
Magic damage will duplicate itself for 33% of the damage and apply the Frozen debuff to hit enemies
An icy aura surrounds you, which freezes nearby enemies after a short delay
Effects of Frostburn Pouch, Ice Bound Strider Hide, and Blue Music Player
Summons a pet Penguin and Owl");
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
            //pets
            modPlayer.IcyEnchant = true;
            modPlayer.AddPet("Penguin Pet", hideVisual, BuffID.BabyPenguin, ProjectileID.Penguin);
            modPlayer.AddPet("Owl Pet", hideVisual, thorium.BuffType("SnowyOwlBuff"), thorium.ProjectileType("SnowyOwlPet"));
            //icy set bonus
            thoriumPlayer.icySet = true;
            if (player.ownedProjectileCounts[thorium.ProjectileType("IcyAura")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("IcyAura"), 0, 0f, player.whoAmI, 0f, 0f);
            }
            //frostburn pouch
            thoriumPlayer.frostburnPouch = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("CryoMagusSpark"));
            recipe.AddIngredient(thorium.ItemType("CryoMagusTabard"));
            recipe.AddIngredient(thorium.ItemType("CryoMagusLeggings"));
            recipe.AddIngredient(null, "IcyEnchant");
            recipe.AddIngredient(thorium.ItemType("IceBoundStriderHide"));
            recipe.AddIngredient(thorium.ItemType("TunePlayerManaRegen"));
            recipe.AddIngredient(thorium.ItemType("IceFairyStaff"));
            recipe.AddIngredient(ItemID.FrostStaff);
            recipe.AddIngredient(thorium.ItemType("Cryotherapy"));
            recipe.AddIngredient(thorium.ItemType("LostMail"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
