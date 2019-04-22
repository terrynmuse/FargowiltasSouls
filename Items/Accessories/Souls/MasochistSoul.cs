using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class MasochistSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Masochist");
            Tooltip.SetDefault(
@"'Embrace suffering'
Increases max life by 100, wing time by 150%, and armor penetration by 25
Increases max life by 20%, damage by 30%, and damage reduction by 15%
Increases max number of minions and sentries by 5
Grants fastfall and immunity to all Masochist Mode debuffs and more
Makes armed and magic skeletons less hostile outside the Dungeon
Your attacks inflict Sadism
You periodically fire additional attacks depending on weapon type and Tiny Eaters
Spectral Fishron periodically manifests to support your critical hits
You erupt into Spiky Balls and Ancient Visions when injured
Summons friendly Creepers, probes, plant's offspring, pungent eye, Cultist, and true eyes
Allows the holder to control gravity");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 11;
            item.value = 5000000;
            item.defense = 20;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(Main.DiscoR, 51, 255 - (int)(Main.DiscoR * 0.4));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            fargoPlayer.MasochistSoul = true;

            //slimy shield
            player.buffImmune[BuffID.Slimed] = true;
            if (Soulcheck.GetValue("Slimy Shield Effects"))
            {
                player.maxFallSpeed *= 2f;
                fargoPlayer.SlimyShield = true;
            }

            //agitating lens
            fargoPlayer.AgitatingLens = true;

            //queen stinger
            player.armorPenetration += 25;
            player.npcTypeNoAggro[210] = true;
            player.npcTypeNoAggro[211] = true;
            player.npcTypeNoAggro[42] = true;
            player.npcTypeNoAggro[176] = true;
            player.npcTypeNoAggro[231] = true;
            player.npcTypeNoAggro[232] = true;
            player.npcTypeNoAggro[233] = true;
            player.npcTypeNoAggro[234] = true;
            player.npcTypeNoAggro[235] = true;
            //fargoPlayer.QueenStinger = true;

            //pure heart
            fargoPlayer.PureHeart = true;

            //corrupt heart
            //player.moveSpeed += 0.1f;
            fargoPlayer.CorruptHeart = true;
            if (fargoPlayer.CorruptHeartCD > 0)
                fargoPlayer.CorruptHeartCD -= 2;

            //gutted heart
            fargoPlayer.GuttedHeart = true;
            fargoPlayer.GuttedHeartCD -= 2; //faster spawns

            //mutant antibodies
            player.buffImmune[BuffID.Rabies] = true;
            fargoPlayer.MutantAntibodies = true;
            player.meleeDamage += 0.3f;
            player.rangedDamage += 0.3f;
            player.magicDamage += 0.3f;
            player.minionDamage += 0.3f;
            player.thrownDamage += 0.3f;

            //lump of flesh
            player.buffImmune[BuffID.Blackout] = true;
            player.buffImmune[BuffID.Obstructed] = true;
            player.buffImmune[BuffID.Dazed] = true;
            fargoPlayer.SkullCharm = true;
            fargoPlayer.LumpOfFlesh = true;
            player.maxMinions += 5;
            player.maxTurrets += 5;
            if (Soulcheck.GetValue("Pungent Eye Minion"))
                player.AddBuff(mod.BuffType("PungentEyeball"), 2);

            //tribal charm buffed
            player.statLifeMax2 += 100;
            fargoPlayer.wingTimeModifier += 0.5f;

            //dubious circuitry
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Electrified] = true;
            fargoPlayer.FusedLens = true;
            fargoPlayer.GroundStick = true;
            fargoPlayer.DubiousCircuitry = true;
            if (Soulcheck.GetValue("Probes Minion"))
                player.AddBuff(mod.BuffType("Probes"), 2);
            player.endurance += 0.15f;

            //magical bulb
            player.buffImmune[BuffID.Venom] = true;
            if (Soulcheck.GetValue("Plantera Minion"))
                player.AddBuff(mod.BuffType("PlanterasChild"), 2);

            //lihzahrd treasure
            player.buffImmune[BuffID.Burning] = true;
            fargoPlayer.LihzahrdTreasureBox = true;

            //betsy's heart
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            fargoPlayer.BetsysHeart = true;

            //celestial rune
            fargoPlayer.CelestialRune = true;
            if (fargoPlayer.CelestialRuneTimer > 0)
                fargoPlayer.CelestialRuneTimer -= 2;

            //chalice
            fargoPlayer.MoonChalice = true;
            if (Soulcheck.GetValue("Cultist Minion"))
                player.AddBuff(mod.BuffType("LunarCultist"), 2);

            //galactic globe
            player.buffImmune[BuffID.VortexDebuff] = true;
            player.buffImmune[BuffID.ChaosState] = true;
            fargoPlayer.GravityGlobeEX = true;
            fargoPlayer.wingTimeModifier += 1f;
            if (Soulcheck.GetValue("Gravity Control"))
                player.gravControl = true;
            if (Soulcheck.GetValue("True Eyes Minion"))
                player.AddBuff(mod.BuffType("TrueEyes"), 2);

            //cyclonic fin
            fargoPlayer.CyclonicFin = true;
            if (fargoPlayer.CyclonicFinCD > 0)
                fargoPlayer.CyclonicFinCD -= 2;

            //sadism
            player.buffImmune[mod.BuffType("Antisocial")] = true;
            player.buffImmune[mod.BuffType("Atrophied")] = true;
            player.buffImmune[mod.BuffType("Berserked")] = true;
            player.buffImmune[mod.BuffType("Bloodthirsty")] = true;
            player.buffImmune[mod.BuffType("ClippedWings")] = true;
            player.buffImmune[mod.BuffType("Crippled")] = true;
            player.buffImmune[mod.BuffType("CurseoftheMoon")] = true;
            player.buffImmune[mod.BuffType("Defenseless")] = true;
            player.buffImmune[mod.BuffType("FlamesoftheUniverse")] = true;
            player.buffImmune[mod.BuffType("Flipped")] = true;
            player.buffImmune[mod.BuffType("FlippedHallow")] = true;
            player.buffImmune[mod.BuffType("Fused")] = true;
            player.buffImmune[mod.BuffType("GodEater")] = true;
            player.buffImmune[mod.BuffType("Hexed")] = true;
            player.buffImmune[mod.BuffType("Infested")] = true;
            player.buffImmune[mod.BuffType("Jammed")] = true;
            player.buffImmune[mod.BuffType("Lethargic")] = true;
            player.buffImmune[mod.BuffType("LightningRod")] = true;
            player.buffImmune[mod.BuffType("LivingWasteland")] = true;
            player.buffImmune[mod.BuffType("MarkedforDeath")] = true;
            player.buffImmune[mod.BuffType("MutantNibble")] = true;
            player.buffImmune[mod.BuffType("OceanicMaul")] = true;
            player.buffImmune[mod.BuffType("Purified")] = true;
            player.buffImmune[mod.BuffType("ReverseManaFlow")] = true;
            player.buffImmune[mod.BuffType("Rotting")] = true;
            player.buffImmune[mod.BuffType("SqueakyToy")] = true;
            player.buffImmune[mod.BuffType("Stunned")] = true;
            player.buffImmune[mod.BuffType("Unstable")] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("SupremeDeathbringerFairy"));
            recipe.AddIngredient(mod.ItemType("PureHeart"));
            recipe.AddIngredient(mod.ItemType("DubiousCircuitry"));
            recipe.AddIngredient(mod.ItemType("LumpOfFlesh"));
            recipe.AddIngredient(mod.ItemType("BetsysHeart"));
            recipe.AddIngredient(mod.ItemType("MutantAntibodies"));
            recipe.AddIngredient(mod.ItemType("ChaliceoftheMoon"));
            recipe.AddIngredient(mod.ItemType("GalacticGlobe"));
            recipe.AddIngredient(mod.ItemType("CyclonicFin"));
            recipe.AddIngredient(mod.ItemType("Sadism"));

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
