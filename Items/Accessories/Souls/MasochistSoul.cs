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
Increases damage by 20%, increases damage reduction by 10%
Increases max number of minions and sentries by 2
Grants immunity to all Masochist Mode debuffs and more
Makes armed and magic skeletons less hostile outside the Dungeon
Your attacks can inflict Cursed Inferno, Ichor, and Electrified
Your critical strikes inflict Betsy's Curse
You erupt into Spiky Balls and Ancient Visions when injured
Summons a friendly plant's offspring and Cultist
Allows the holder to control gravity");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 10;
            item.value = Item.sellPrice(0, 7);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //mutant antibodies
            player.buffImmune[BuffID.Rabies] = true;
            player.GetModPlayer<FargoPlayer>().MutantAntibodies = true;
            player.meleeDamage += 0.2f;
            player.rangedDamage += 0.2f;
            player.magicDamage += 0.2f;
            player.minionDamage += 0.2f;
            player.thrownDamage += 0.2f;

            //lump of flesh
            player.buffImmune[BuffID.TheTongue] = true;
            player.buffImmune[BuffID.Dazed] = true;
            player.GetModPlayer<FargoPlayer>().SkullCharm = true;
            player.GetModPlayer<FargoPlayer>().LumpOfFlesh = true;
            player.maxMinions += 2;
            player.maxTurrets += 2;

            //dubious circuitry
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Electrified] = true;
            player.GetModPlayer<FargoPlayer>().FusedLens = true;
            player.GetModPlayer<FargoPlayer>().GroundStick = true;
            player.endurance += 0.1f;

            //magical bulb
            player.buffImmune[BuffID.Venom] = true;
            if (Soulcheck.GetValue("Plantera Minion"))
            {
                player.AddBuff(mod.BuffType("PlanterasChild"), 5);
            }
            //lihzahrd treasure
            player.buffImmune[BuffID.Burning] = true;
            player.GetModPlayer<FargoPlayer>().LihzahrdTreasureBox = true;
            //betsy's heart
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            player.GetModPlayer<FargoPlayer>().BetsysHeart = true;
            //celestial rune

            //chalice
            player.GetModPlayer<FargoPlayer>().MoonChalice = true;
            if (Soulcheck.GetValue("Cultist Minion"))
            {
                player.AddBuff(mod.BuffType("LunarCultist"), 5);
            }

            //galactic globe
            player.buffImmune[BuffID.VortexDebuff] = true;
            player.buffImmune[BuffID.ChaosState] = true;
            if (Soulcheck.GetValue("Gravity Control"))
            {
                player.gravControl = true;
            }
            player.GetModPlayer<FargoPlayer>().GravityGlobeEX = true;

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

            recipe.AddIngredient(mod.ItemType("MutantAntibodies"));
            recipe.AddIngredient(mod.ItemType("LumpOfFlesh"));
            recipe.AddIngredient(mod.ItemType("DubiousCircuitry"));
            recipe.AddIngredient(mod.ItemType("BetsysHeart"));
            recipe.AddIngredient(mod.ItemType("ChaliceoftheMoon"));
            recipe.AddIngredient(mod.ItemType("GalacticGlobe"));
            recipe.AddIngredient(mod.ItemType("Sadism"));

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
