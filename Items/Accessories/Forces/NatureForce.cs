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

            /*string tooltip =
@"'Tapped into every secret of the wilds'
Summons a leaf crystal to shoot at nearby enemies
Flowers grow on the grass you walk on
All herb collection is doubled
Summons a pet Seedling
Greatly increases life regen
Hearts heal for 1.5x as much
Summons a Baby Face Monster and a Crimson Heart
Icicles will start to appear around you
When there are three, attacking will launch them towards the cursor
Chance to steal 4 mana with each attack
Taking damage will release a poisoning spore explosion
Nearby enemies are ignited
When you die, you violently explode dealing massive damage to surrounding enemies
Not moving puts you in stealth
While in stealth crits deal 4x damage
Summons a pet Baby Truffle";

            if (thorium != null)
            {
                tooltip +=
@"An icy aura surrounds you, which freezes nearby enemies after a short delay
Your symphonic damage empowers all nearby allies with: Cold Shoulder
Damage done against frostburnt enemies is increased by 8% 
Doubles the range of your empowerments effect radius
Your symphonic damage empowers all nearby allies with: Jungle's Nibble
Damage done against poisoned enemies is increased by 8%
Doubles the range of your empowerments effect radius

Maximum life increased by 100
Getting hit will trigger 'Sanguine', increasing defensive abilities briefly.
Flail weapons have a chance to release rolling spike balls on hit that apply ichor to damaged enemies.
Your symphonic damage empowers all nearby allies with: Abomination's Blood
Damage done against ichor'd enemies is increased by 5%
Doubles the range of your empowerments effect radius
Your symphonic damage will empower all nearby allies with: Critical Strike II
Damage against enemies has a 10% chance to drop flesh, which grants bonus life and damage when picked up
Greatly increases life regen
Hearts heal for 1.5x as much
Your damage will have a 10% chance to cause an eruption of blood
This blood can be picked up by players to heal themselves for 15% of the damage you dealt
Healing amount cannot exceed 15 life and picking up blood causes bleeding for 5 seconds
Summons a pet Flying Blister, Face Monster, and Crimson Heart";
            }

            tooltip += "Summons a pet Penguin and Snowman";

            Tooltip.SetDefault(tooltip);*/
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
            //crystal and pet
            modPlayer.ChloroEffect(hideVisual, 100);
            //herb double
            modPlayer.ChloroEnchant = true;
            modPlayer.FlowerBoots();
            //regen, hearts heal more, pets
            modPlayer.CrimsonEffect(hideVisual);
            //icicles, pets
            modPlayer.FrostEffect(75, hideVisual);
            //mana steal, spore, cordage
            modPlayer.JungleEffect();
            //inferno and explode
            modPlayer.MoltenEffect(20);
            //stealth, crits, pet
            modPlayer.ShroomiteEffect(hideVisual);

            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            thoriumPlayer.subwooferFrost = true;
            thoriumPlayer.bardRangeBoost += 450;
            //icy set bonus
            thoriumPlayer.icySet = true;
            if (player.ownedProjectileCounts[thorium.ProjectileType("IcyAura")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("IcyAura"), 0, 0f, player.whoAmI, 0f, 0f);
            }
            //demon blood
            thoriumPlayer.demonbloodSet = true;
            player.statLifeMax2 += 100;
            //demon blood badge
            thoriumPlayer.CrimsonBadge = true;
            //vile core
            thoriumPlayer.vileCore = true;
            //subwoofer
            thoriumPlayer.subwooferIchor = true;
            thoriumPlayer.bardRangeBoost += 450;
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3CriticalStrike = 2;
            //flesh set bonus
            thoriumPlayer.Symbiotic = true;
            //vampire gland
            thoriumPlayer.vampireGland = true;
            //blister pet
            modPlayer.AddPet("Blister Pet", hideVisual, thorium.BuffType("BlisterBuff"), thorium.ProjectileType("BlisterPet"));
            thoriumPlayer.blisterPet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CrimsonEnchant");
            recipe.AddIngredient(null, "JungleEnchant");
            recipe.AddIngredient(null, "MoltenEnchant");
            recipe.AddIngredient(null, "FrostEnchant");
            recipe.AddIngredient(null, "ChlorophyteEnchant");
            recipe.AddIngredient(null, "ShroomiteEnchant");

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}