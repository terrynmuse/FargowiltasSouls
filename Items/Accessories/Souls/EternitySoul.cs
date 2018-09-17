using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class EternitySoul : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Eternity");
            Tooltip.SetDefault(
                @"''
");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 24));
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 100000000;
            item.shieldSlot = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //includes revive, both spectres, adamantite
            modPlayer.TerrariaSoul = true;
            modPlayer.TerrariaPets(hideVisual);

            player.onHitDodge = true;
            player.onHitRegen = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.strongBees = true;

            modPlayer.BeetleEffect(); //just beetles
            modPlayer.ChloroEffect(hideVisual, 100); //everything
            modPlayer.CrimsonEffect(hideVisual); //everything
            modPlayer.DarkArtistEffect(hideVisual); //no meme effect
            modPlayer.ForbiddenEffect(); //everything, maybe kill storm boost
            modPlayer.FrostEffect(80, hideVisual);
            modPlayer.GoldEffect(hideVisual); // just greed ring effect
            modPlayer.HallowEffect(hideVisual, 100); //everything
            modPlayer.IronEffect(); //everything
            modPlayer.MoltenEffect(20); //everything
            modPlayer.NebulaEffect(); //no meme speed
            modPlayer.NecroEffect(hideVisual); //everything
            modPlayer.OrichalcumEffect(); //no petals, permanent 4 balls instead of spawning
            modPlayer.RedRidingEffect(hideVisual); //no super bleed or increase low Hp dmg
            modPlayer.ShinobiEffect(hideVisual); //all
            modPlayer.SolarEffect(); //no solar flare debuff
            modPlayer.StardustEffect(); //all, may kill freeze time
            modPlayer.TinEffect(); //lifesteal bonus
            modPlayer.ValhallaEffect(hideVisual); //no KB memes
            modPlayer.VortexEffect(hideVisual); //all may kill voids

            //CUT
            //cactus, cobalt, copper, fossil bone zone, gladiator, jungle, lead, meteor, nebula meme speed, ninja, obsidian, platinum, pumpkin, red riding blood, shadow, shroomite, solar flare debuff, spider, spooky, tungsten, turtle, dark artist shadow shoot

            //ALTERED
            //ori changed, tin and palladium fused, titanium changed

            //ELSEWHERE
            //mythril, tiki, mining
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TerraForce");
            recipe.AddIngredient(null, "EarthForce");
            recipe.AddIngredient(null, "NatureForce");
            recipe.AddIngredient(null, "LifeForce");
            recipe.AddIngredient(null, "SpiritForce");
            recipe.AddIngredient(null, "ShadowForce");
            recipe.AddIngredient(null, "WillForce");
            recipe.AddIngredient(null, "CosmoForce");

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}