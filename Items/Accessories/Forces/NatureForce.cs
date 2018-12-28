using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class NatureForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Nature");

            string tooltip =
@"'Tapped into every secret of the wilds'
";

            //if (thorium == null)
            //{
                tooltip +=
@"Greatly increases life regen
Hearts heal for 1.5x as much
Nearby enemies are ignited
When you die, you violently explode dealing massive damage
Icicles will start to appear around you
When there are three, attacking will launch them towards the cursor
Taking damage will release a poisoning spore explosion
Summons a leaf crystal to shoot at nearby enemies
Not moving puts you in stealth
While in stealth, crits deal 4x damage
Summons several pets";
            /*}
            else
            {
                tooltip +=
@"Allows you to breathe underwater and swim
Taking or dealing damage may release a poisoning spore cloud
Summons a leaf crystal to shoot at nearby enemies
Not moving puts you in stealth, While in stealth, crits deal 4x damage
Attacks inflict Fungal Growth
An icy aura and icicles will start to appear around you
Damage will duplicate itself for 33% of the damage and apply the Frozen debuff
Greatly increases life regen
Your damage has a 10% chance to cause an eruption of blood or flesh drop
Nearby enemies are ignited
When you die, you violently explode dealing massive damage to surrounding enemies
Enemies that you poison, envenom, or set on fire will take extra damage over time
Summons several pets";
            }*/

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
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //bulb, cryo effect
            modPlayer.NatureForce = true;
            //regen, hearts heal more, pets
            modPlayer.CrimsonEffect(hideVisual);
            //inferno and explode
            modPlayer.MoltenEffect(25);
            //icicles, pets
            modPlayer.FrostEffect(75, hideVisual);
            //spores
            modPlayer.JungleEffect();
            //crystal and pet
            modPlayer.ChloroEffect(hideVisual, 100);
            //stealth, crits, pet
            modPlayer.ShroomiteEffect(hideVisual);

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            //sandstone? nothin
            //ocean/depth diver stuff
            modPlayer.DepthEnchant = true;
            modPlayer.AddPet("Jellyfish Pet", hideVisual, thorium.BuffType("JellyPet"), thorium.ProjectileType("JellyfishPet"));
            //ocean set bonus, breath underwater
            if (player.breath <= player.breathMax + 2)
            {
                player.breath = player.breathMax + 3;
            }
            player.accFlipper = true;
            //night shade petal
            thoriumPlayer.nightshadeBoost = true;
            //pet
            modPlayer.AddPet("Owl Pet", hideVisual, thorium.BuffType("SnowyOwlBuff"), thorium.ProjectileType("SnowyOwlPet"));
            thoriumPlayer.snowyOwl = true;
            //icy set bonus
            thoriumPlayer.icySet = true;
            if (player.ownedProjectileCounts[thorium.ProjectileType("IcyAura")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("IcyAura"), 0, 0f, player.whoAmI, 0f, 0f);
            }
            //flesh set bonus
            thoriumPlayer.Symbiotic = true;
            //vampire gland
            thoriumPlayer.vampireGland = true;
            //blister pet
            modPlayer.AddPet("Blister Pet", hideVisual, thorium.BuffType("BlisterBuff"), thorium.ProjectileType("BlisterPet"));
            thoriumPlayer.blisterPet = true;
            //magma set bonus
            thoriumPlayer.magmaSet = true;*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            /*if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "SandstoneEnchant");
                recipe.AddIngredient(null, "DepthDiverEnchant");
                recipe.AddIngredient(null, "ChlorophyteEnchant");
                recipe.AddIngredient(null, "ShroomiteEnchant");
                recipe.AddIngredient(null, "CryoMagusEnchant");
                recipe.AddIngredient(null, "DemonBloodEnchant");
                recipe.AddIngredient(null, "BerserkerEnchant"); 
            }
            else
            {*/
                recipe.AddIngredient(null, "CrimsonEnchant");
                recipe.AddIngredient(null, "MoltenEnchant");
                recipe.AddIngredient(null, "FrostEnchant");
                recipe.AddIngredient(null, "ChlorophyteEnchant");
                recipe.AddIngredient(null, "ShroomiteEnchant");
            //}

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}