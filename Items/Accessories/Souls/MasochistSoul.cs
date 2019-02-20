using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class MasochistSoul : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Masochist");
            Tooltip.SetDefault(
@"'Suffering'
Grants immunity to all Masochist Mode debuffs and more
Makes armed and magic skeletons less hostile outside the Dungeon
Your attacks have a small chance to inflict Electrified
Attracts a legendary plant's offspring which flourishes in combat
You erupt into Ancient Visions when injured
Summons a friendly Cultist
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
            player.buffImmune[mod.BuffType("MutantNibble")] = true;

            //lump of flesh
            player.buffImmune[mod.BuffType("Rotting")] = true;
            player.buffImmune[BuffID.Dazed] = true;
            player.GetModPlayer<FargoPlayer>().SkullCharm = true;

            //dubious circuitry
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[mod.BuffType("Defenseless")] = true;
            player.GetModPlayer<FargoPlayer>().GroundStick = true;

            //magical bulb
            player.buffImmune[BuffID.Venom] = true;
            player.AddBuff(mod.BuffType("PlanterasChild"), 5);
            //lihzahrd treasure
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[mod.BuffType("Fused")] = true;
            //celestial rune
            player.buffImmune[mod.BuffType("MarkedforDeath")] = true;
            player.buffImmune[mod.BuffType("ClippedWings")] = true;
            player.buffImmune[mod.BuffType("Hexed")] = true;
            //chalice
            player.buffImmune[mod.BuffType("Atrophied")] = true;
            player.buffImmune[mod.BuffType("Jammed")] = true;
            player.buffImmune[mod.BuffType("ReverseManaFlow")] = true;
            player.buffImmune[mod.BuffType("Antisocial")] = true;
            player.GetModPlayer<FargoPlayer>().MoonChalice = true;

            //galactic globe
            player.buffImmune[mod.BuffType("Flipped")] = true;
            player.buffImmune[mod.BuffType("FlippedHallow")] = true;
            player.buffImmune[mod.BuffType("Unstable")] = true;
            player.buffImmune[BuffID.VortexDebuff] = true;
            player.gravControl = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("MutantAntibodies"));
            recipe.AddIngredient(mod.ItemType("LumpOfFlesh"));
            recipe.AddIngredient(mod.ItemType("DubiousCircuitry"));
            recipe.AddIngredient(mod.ItemType("ChaliceoftheMoon"));
            recipe.AddIngredient(mod.ItemType("GalacticGlobe"));

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
