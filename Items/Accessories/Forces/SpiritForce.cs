using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class SpiritForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Spirit");

            string tooltip =
@"''
";

            /*if (thorium != null)
            {
                tooltip +=
@"If you reach zero HP you cheat death, returning with 100 HP and spawning bones
Double tap down to call an ancient storm to the cursor location
You gain a shield that can reflect projectiles
Attacks will inflict a Infested and spawn damaging and healing orbs
Killing enemies or continually damaging bosses generates soul wisps
Pressing the Special Ability key will summon a chorus of music playing ghosts
Taking damage heals nearby allies equal to 15% of the damage taken
Summons an Enchanted Sword and Li'l Cherub familiar
Summons several pets";
            }
            else
            {*/
                tooltip +=
@"If you reach zero HP you cheat death, returning with 100 HP and spawning bones
Double tap down to call an ancient storm to the cursor location
You gain a shield that can reflect projectiles
Attacks will inflict a Infested and spawn damaging and healing orbs
Summons an Enchanted Sword familiar
Summons several pets";
            //}

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //spectre works for all, spirit trapper works for all
            modPlayer.SpiritForce = true;
            //revive, bone zone, pet
            modPlayer.FossilEffect(25, hideVisual);
            //storm
            modPlayer.ForbiddenEffect();
            //sword, shield, pet
            modPlayer.HallowEffect(hideVisual, 100);
            //infested debuff, pet
            modPlayer.TikiEffect(hideVisual);
            //pet
            modPlayer.SpectreEffect(hideVisual);

           /* if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            //lil cherub
            modPlayer.SacredEnchant = true;
            modPlayer.AddMinion("Li'l Cherub Minion", thorium.ProjectileType("Angel"), 0, 0f);
            //twinkle pet
            modPlayer.AddPet("Life Spirit Pet", hideVisual, thorium.BuffType("LifeSpiritBuff"), thorium.ProjectileType("LifeSpirit"));
            thoriumPlayer.lifePet = true;
            //pet
            modPlayer.AddPet("Moogle Pet", hideVisual, thorium.BuffType("LilMogBuff"), thorium.ProjectileType("LilMog"));
            modPlayer.KnightEnchant = true;
            //conductor set bonus
            thoriumPlayer.conductorSet = true;
            //paladin set bonus
            thoriumPlayer.fallenPaladinSet = true;*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            /*if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "FossilEnchant");
                recipe.AddIngredient(null, "SacredEnchant");
                recipe.AddIngredient(null, "ForbiddenEnchant");
                recipe.AddIngredient(null, "HallowEnchant");
                recipe.AddIngredient(null, "TikiEnchant");
                recipe.AddIngredient(null, "SpectreEnchant");
                recipe.AddIngredient(null, "HarbingerEnchant");
                recipe.AddIngredient(null, "FallenPaladinEnchant");
                recipe.AddIngredient(null, "ConductorEnchant");
            }
            else
            {*/
                recipe.AddIngredient(null, "FossilEnchant");
                recipe.AddIngredient(null, "ForbiddenEnchant");
                recipe.AddIngredient(null, "HallowEnchant");
                recipe.AddIngredient(null, "TikiEnchant");
                recipe.AddIngredient(null, "SpectreEnchant");
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