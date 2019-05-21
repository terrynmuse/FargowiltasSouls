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
@"'To inflict suffering, you must first embrace it'
Increases max life by 250, wing time by 200%, and armor penetration by 50
Increases max life by 50%, damage by 40%, crit rate by 30%, and damage reduction by 20%
Increases life regen drastically, increases max number of minions and sentries by 10
Grants gravity control, fastfall, and immunity to all Masochist Mode debuffs and more
Empowers Cute Fishron and makes armed and magic skeletons less hostile outside the Dungeon
Your attacks create additional attacks and inflict Sadism as a cocktail of Masochist Mode debuffs
You respawn twice as fast and erupt into Spiky Balls and Ancient Visions when injured
Attacks have a chance to squeak and deal 1 damage to you
Summons the aid of all Masochist Mode bosses to your side");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 11;
            item.value = 5000000;
            item.defense = 30;
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

            //stat modifiers
            player.meleeDamage += 0.4f;
            player.rangedDamage += 0.4f;
            player.magicDamage += 0.4f;
            player.minionDamage += 0.4f;
            player.thrownDamage += 0.4f;
            player.meleeCrit += 30;
            player.rangedCrit += 30;
            player.magicCrit += 30;
            player.thrownCrit += 30;
            player.endurance += 0.2f;
            player.maxMinions += 10;
            player.maxTurrets += 10;
            player.armorPenetration += 50;
            player.statLifeMax2 += player.statLifeMax / 2;
            player.statLifeMax2 += 250;
            player.lifeRegen += 7;
            player.lifeRegenTime += 7;
            player.lifeRegenCount += 7;
            fargoPlayer.wingTimeModifier += 2f;

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

            //necromantic brew
            if (Soulcheck.GetValue("Skeletron Arms Minion"))
                player.AddBuff(mod.BuffType("SkeletronArms"), 2);

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

            //lump of flesh
            player.buffImmune[BuffID.Blackout] = true;
            player.buffImmune[BuffID.Obstructed] = true;
            player.buffImmune[BuffID.Dazed] = true;
            fargoPlayer.SkullCharm = true;
            if (!player.ZoneDungeon)
            {
                player.npcTypeNoAggro[NPCID.SkeletonSniper] = true;
                player.npcTypeNoAggro[NPCID.SkeletonCommando] = true;
                player.npcTypeNoAggro[NPCID.TacticalSkeleton] = true;
                player.npcTypeNoAggro[NPCID.DiabolistRed] = true;
                player.npcTypeNoAggro[NPCID.DiabolistWhite] = true;
                player.npcTypeNoAggro[NPCID.Necromancer] = true;
                player.npcTypeNoAggro[NPCID.NecromancerArmored] = true;
                player.npcTypeNoAggro[NPCID.RaggedCaster] = true;
                player.npcTypeNoAggro[NPCID.RaggedCasterOpenCoat] = true;
            }
            fargoPlayer.LumpOfFlesh = true;
            if (Soulcheck.GetValue("Pungent Eye Minion"))
                player.AddBuff(mod.BuffType("PungentEyeball"), 2);

            //concentrated rainbow matter
            if (Soulcheck.GetValue("Rainbow Slime Minion"))
                player.AddBuff(mod.BuffType("RainbowSlime"), 2);

            //dragon fang
            if (Soulcheck.GetValue("Inflict Clipped Wings"))
                fargoPlayer.DragonFang = true;

            //frigid gemstone
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.ShadowFlame] = true;
            if (Soulcheck.GetValue("Shadowfrostfireballs"))
            {
                fargoPlayer.FrigidGemstone = true;
                if (fargoPlayer.FrigidGemstoneCD > 0)
                    fargoPlayer.FrigidGemstoneCD -= 5;
            }

            //sands of time
            player.buffImmune[BuffID.WindPushed] = true;
            fargoPlayer.SandsofTime = true;

            //squeaky toy
            fargoPlayer.SqueakyAcc = true;

            //tribal charm
            player.buffImmune[BuffID.Webbed] = true;
            player.buffImmune[BuffID.Suffocation] = true;
            fargoPlayer.autofire = true;

            //dubious circuitry
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Electrified] = true;
            fargoPlayer.FusedLens = true;
            fargoPlayer.GroundStick = true;
            fargoPlayer.DubiousCircuitry = true;
            if (Soulcheck.GetValue("Probes Minion"))
                player.AddBuff(mod.BuffType("Probes"), 2);
            player.noKnockback = true;

            //magical bulb
            player.buffImmune[BuffID.Venom] = true;
            if (Soulcheck.GetValue("Plantera Minion"))
                player.AddBuff(mod.BuffType("PlanterasChild"), 2);

            //ice queen's crown
            player.buffImmune[BuffID.Frozen] = true;
            if (Soulcheck.GetValue("Flocko Minion"))
                player.AddBuff(mod.BuffType("SuperFlocko"), 2);

            //lihzahrd treasure
            player.buffImmune[BuffID.Burning] = true;
            fargoPlayer.LihzahrdTreasureBox = true;

            //betsy's heart
            player.buffImmune[BuffID.OgreSpit] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            fargoPlayer.BetsysHeart = true;

            //celestial rune/pumpking's cape
            fargoPlayer.CelestialRune = true;
            fargoPlayer.PumpkingsCape = true;
            fargoPlayer.AdditionalAttacks = true;
            if (fargoPlayer.AdditionalAttacksTimer > 0)
                fargoPlayer.AdditionalAttacksTimer -= 2;

            //chalice
            fargoPlayer.MoonChalice = true;
            if (Soulcheck.GetValue("Cultist Minion"))
                player.AddBuff(mod.BuffType("LunarCultist"), 2);

            //galactic globe
            player.buffImmune[BuffID.VortexDebuff] = true;
            player.buffImmune[BuffID.ChaosState] = true;
            fargoPlayer.GravityGlobeEX = true;
            if (Soulcheck.GetValue("Gravity Control"))
                player.gravControl = true;
            if (Soulcheck.GetValue("True Eyes Minion"))
                player.AddBuff(mod.BuffType("TrueEyes"), 2);

            //heart of maso
            player.buffImmune[mod.BuffType("NullificationCurse")] = true;
            NPCs.FargoGlobalNPC.masoStateML = 4;

            //cyclonic fin
            player.buffImmune[BuffID.Frozen] = true;
            fargoPlayer.CyclonicFin = true;
            if (fargoPlayer.CyclonicFinCD > 0)
                fargoPlayer.CyclonicFinCD -= 2;
            if (player.mount.Active && player.mount.Type == MountID.CuteFishron)
            {
                if (player.ownedProjectileCounts[mod.ProjectileType("CuteFishronRitual")] < 1 && player.whoAmI == Main.myPlayer)
                    Projectile.NewProjectile(player.MountedCenter, Vector2.Zero, mod.ProjectileType("CuteFishronRitual"), 0, 0f, Main.myPlayer);
                player.MountFishronSpecialCounter = 300;
                player.meleeDamage += 0.5f;
                player.rangedDamage += 0.5f;
                player.magicDamage += 0.5f;
                player.minionDamage += 0.5f;
                player.thrownDamage += 0.5f;
                player.meleeCrit += 20;
                player.rangedCrit += 20;
                player.magicCrit += 20;
                player.thrownCrit += 20;
                player.statDefense += 20;
                player.lifeRegen += 3;
                player.lifeRegenCount += 3;
                player.lifeRegenTime += 3;
                if (player.controlLeft == player.controlRight)
                {
                    if (player.velocity.X != 0)
                        player.velocity.X -= player.mount.Acceleration * Math.Sign(player.velocity.X);
                    if (player.velocity.X != 0)
                        player.velocity.X -= player.mount.Acceleration * Math.Sign(player.velocity.X);
                }
                else if (player.controlLeft)
                {
                    player.velocity.X -= player.mount.Acceleration * 4f;
                    if (player.velocity.X < -16f)
                        player.velocity.X = -16f;
                    if (!player.controlUseItem)
                        player.direction = -1;
                }
                else if (player.controlRight)
                {
                    player.velocity.X += player.mount.Acceleration * 4f;
                    if (player.velocity.X > 16f)
                        player.velocity.X = 16f;
                    if (!player.controlUseItem)
                        player.direction = 1;
                }
                if (player.controlUp == player.controlDown)
                {
                    if (player.velocity.Y != 0)
                        player.velocity.Y -= player.mount.Acceleration * Math.Sign(player.velocity.Y);
                    if (player.velocity.Y != 0)
                        player.velocity.Y -= player.mount.Acceleration * Math.Sign(player.velocity.Y);
                }
                else if (player.controlUp)
                {
                    player.velocity.Y -= player.mount.Acceleration * 4f;
                    if (player.velocity.Y < -16f)
                        player.velocity.Y = -16f;
                }
                else if (player.controlDown)
                {
                    player.velocity.Y += player.mount.Acceleration * 4f;
                    if (player.velocity.Y > 16f)
                        player.velocity.Y = 16f;
                }
            }

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
            recipe.AddIngredient(mod.ItemType("BionomicCluster"));
            recipe.AddIngredient(mod.ItemType("DubiousCircuitry"));
            recipe.AddIngredient(mod.ItemType("PureHeart"));
            recipe.AddIngredient(mod.ItemType("LumpOfFlesh"));
            recipe.AddIngredient(mod.ItemType("HeartoftheMasochist"));
            recipe.AddIngredient(mod.ItemType("CyclonicFin"));
            recipe.AddIngredient(mod.ItemType("Sadism"), 30);

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
