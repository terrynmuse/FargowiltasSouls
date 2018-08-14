using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class SpiritForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Spirit");
            Tooltip.SetDefault(
@"'The strength of your spirit amazes even the Mutant'
If you reach zero HP you cheat death, returning with 20 HP
For a few seconds after reviving, you are immune to all damage and spawn bones everywhere
Double tap down to call an ancient storm to the cursor location
Any projectiles shot through your storm gain double pierce and 50% damage
You are immune to the Mighty Wind debuff
You gain a shield that can reflect projectiles
Summons an Enchanted Sword familiar that scales with minion damage
Attacks will inflict a random debuff
All damage can spawn damaging orbs and healing orbs
Summons a pet Baby Dino, Magical Fairy, Tiki Spirit, and Wisp");
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
            modPlayer.SpiritForce = true;
            //revive
            modPlayer.FossilEnchant = true;
            //bone zone
            modPlayer.FossilEffect(20);
            //storm
            modPlayer.ForbiddenEffect();
            player.buffImmune[BuffID.WindPushed] = true;
            //sword and shield bois
            modPlayer.HallowEnchant = true;
            modPlayer.AddMinion("Hallowed Shield", mod.ProjectileType("HallowShield"), 0, 0f);
            modPlayer.AddMinion("Enchanted Sword Familiar", mod.ProjectileType("HallowSword"), (int)(80 * player.minionDamage), 0f);
            //random debuffs
            modPlayer.TikiEnchant = true;
            //spectre orbs
            modPlayer.SpectreEffect();
            modPlayer.AddPet("Baby Dino Pet", hideVisual, BuffID.BabyDinosaur, ProjectileID.BabyDino);
            modPlayer.AddPet("Fairy Pet", hideVisual, BuffID.FairyBlue, ProjectileID.BlueFairy);
            modPlayer.AddPet("Tiki Pet", hideVisual, BuffID.TikiSpirit, ProjectileID.TikiSpirit);
            modPlayer.AddPet("Wisp Pet", hideVisual, BuffID.Wisp, ProjectileID.Wisp);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FossilEnchant");
            recipe.AddIngredient(null, "ForbiddenEnchant");
            recipe.AddIngredient(null, "HallowEnchant");
            recipe.AddIngredient(null, "TikiEnchant");
            recipe.AddIngredient(null, "SpectreEnchant");
            
            if (Fargowiltas.Instance.FargosLoaded)
            {
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            }
            else
            {
                recipe.AddTile(TileID.LunarCraftingStation);
            }

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}