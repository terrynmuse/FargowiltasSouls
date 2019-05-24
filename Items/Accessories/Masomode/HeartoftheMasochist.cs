using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class HeartoftheMasochist : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart of the Masochist");
            Tooltip.SetDefault(@"'Suffering no longer hurts, mostly'
Grants immunity to Living Wasteland, Frozen, Oozed, Withered Weapon, and Withered Armor
Grants immunity to Feral Bite, Mutant Nibble, Flipped, Unstable, Distorted, and Chaos State
Grants immunity to Electrified, Nullification Curse, and most debuffs caused by entering water
Increases damage, critical strike chance, and damage reduction by 10%
Increases flight time by 100%
You may periodically fire additional attacks depending on weapon type
Your critical strikes inflict Betsy's Curse
Grants effects of Wet debuff while riding Cute Fishron and gravity control
Summons a friendly super Flocko, Mini Saucer, and true eyes of Cthulhu");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 11;
            item.value = Item.sellPrice(0, 9);
            item.defense = 10;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            player.meleeDamage += 0.1f;
            player.rangedDamage += 0.1f;
            player.magicDamage += 0.1f;
            player.thrownDamage += 0.1f;
            player.minionDamage += 0.1f;
            player.meleeCrit += 10;
            player.rangedCrit += 10;
            player.magicCrit += 10;
            player.thrownCrit += 10;
            player.endurance += 0.1f;

            //pumpking's cape
            player.buffImmune[mod.BuffType("LivingWasteland")] = true;
            fargoPlayer.PumpkingsCape = true;
            fargoPlayer.AdditionalAttacks = true;

            //ice queen's crown
            player.buffImmune[BuffID.Frozen] = true;
            if (Soulcheck.GetValue("Flocko Minion"))
                player.AddBuff(mod.BuffType("SuperFlocko"), 2);

            //saucer control console
            player.buffImmune[BuffID.Electrified] = true;
            if (Soulcheck.GetValue("Saucer Minion"))
                player.AddBuff(mod.BuffType("SaucerMinion"), 2);

            //betsy's heart
            player.buffImmune[BuffID.OgreSpit] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            fargoPlayer.BetsysHeart = true;

            //mutant antibodies
            player.buffImmune[BuffID.Rabies] = true;
            player.buffImmune[mod.BuffType("MutantNibble")] = true;
            fargoPlayer.MutantAntibodies = true;
            if (player.mount.Active && player.mount.Type == MountID.CuteFishron)
                player.dripping = true;

            //galactic globe
            player.buffImmune[mod.BuffType("Flipped")] = true;
            player.buffImmune[mod.BuffType("FlippedHallow")] = true;
            player.buffImmune[mod.BuffType("Unstable")] = true;
            //player.buffImmune[mod.BuffType("CurseoftheMoon")] = true;
            player.buffImmune[BuffID.VortexDebuff] = true;
            player.buffImmune[BuffID.ChaosState] = true;
            if (Soulcheck.GetValue("Gravity Control"))
                player.gravControl = true;
            if (Soulcheck.GetValue("True Eyes Minion"))
                player.AddBuff(mod.BuffType("TrueEyes"), 2);
            fargoPlayer.GravityGlobeEX = true;
            fargoPlayer.wingTimeModifier += 1f;

            //heart of maso
            player.buffImmune[mod.BuffType("NullificationCurse")] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("PumpkingsCape"));
            recipe.AddIngredient(mod.ItemType("IceQueensCrown"));
            recipe.AddIngredient(mod.ItemType("SaucerControlConsole"));
            recipe.AddIngredient(mod.ItemType("BetsysHeart"));
            recipe.AddIngredient(mod.ItemType("MutantAntibodies"));
            recipe.AddIngredient(mod.ItemType("GalacticGlobe"));
            recipe.AddIngredient(mod.ItemType("LunarCrystal"), 30);
            recipe.AddIngredient(ItemID.LunarBar, 15);

            recipe.AddTile(mod, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
