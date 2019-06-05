using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class BionomicCluster : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bionomic Cluster");
            Tooltip.SetDefault(@"'The amalgamate born of a thousand common enemies'
Grants immunity to Frostburn, Shadowflame, Squeaky Toy, Guilty, Mighty Wind, and Suffocation
Grants immunity to Flames of the Universe, Clipped Wings, Crippled, Webbed, and Purified
Grants autofire to all weapons and immunity to enemies that steal items or coins
Your attacks have a 10% chance to inflict Clipped Wings on non-boss enemies
Your attacks summon Frostfireballs to attack your enemies
You respawn twice as fast when no boss is alive and have improved night vision
Automatically use mana potions when needed and gives modifier protection
Attacks have a chance to squeak and deal 1 damage to you
You erupt into Shadowflame tentacles when injured
Summons a friendly rainbow slime");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 7;
            item.value = Item.sellPrice(0, 5);
            item.defense = 6;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();

            //concentrated rainbow matter
            player.buffImmune[mod.BuffType("FlamesoftheUniverse")] = true;
            if (Soulcheck.GetValue("Rainbow Slime Minion"))
                player.AddBuff(mod.BuffType("RainbowSlime"), 2);

            //dragon fang
            player.buffImmune[mod.BuffType("ClippedWings")] = true;
            player.buffImmune[mod.BuffType("Crippled")] = true;
            if (Soulcheck.GetValue("Inflict Clipped Wings"))
                fargoPlayer.DragonFang = true;

            //frigid gemstone
            player.buffImmune[BuffID.Frostburn] = true;
            if (Soulcheck.GetValue("Frostfireballs"))
            {
                fargoPlayer.FrigidGemstone = true;
                if (fargoPlayer.FrigidGemstoneCD > 0)
                    fargoPlayer.FrigidGemstoneCD--;
            }

            //wretched pouch
            player.buffImmune[BuffID.ShadowFlame] = true;
            player.GetModPlayer<FargoPlayer>().WretchedPouch = true;

            //sands of time
            player.buffImmune[BuffID.WindPushed] = true;
            fargoPlayer.SandsofTime = true;

            //squeaky toy
            player.buffImmune[mod.BuffType("SqueakyToy")] = true;
            player.buffImmune[mod.BuffType("Guilty")] = true;
            fargoPlayer.SqueakyAcc = true;

            //tribal charm
            player.buffImmune[BuffID.Webbed] = true;
            player.buffImmune[mod.BuffType("Purified")] = true;
            fargoPlayer.TribalCharm = true;

            //mystic skull
            player.buffImmune[BuffID.Suffocation] = true;
            player.manaFlower = true;

            //security wallet
            fargoPlayer.SecurityWallet = true;

            //carrot
            player.nightVision = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("SqueakyToy"));
            recipe.AddIngredient(mod.ItemType("DragonFang"));
            recipe.AddIngredient(mod.ItemType("FrigidGemstone"));
            recipe.AddIngredient(mod.ItemType("SandsofTime"));
            recipe.AddIngredient(mod.ItemType("ConcentratedRainbowMatter"));
            recipe.AddIngredient(mod.ItemType("TribalCharm"));
            recipe.AddIngredient(mod.ItemType("MysticSkull"));
            recipe.AddIngredient(mod.ItemType("WretchedPouch"));
            recipe.AddIngredient(mod.ItemType("OrdinaryCarrot"));
            recipe.AddIngredient(mod.ItemType("SecurityWallet"));
            recipe.AddIngredient(ItemID.SoulofLight, 20);
            recipe.AddIngredient(ItemID.SoulofNight, 20);

            recipe.AddTile(TileID.CookingPots);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
