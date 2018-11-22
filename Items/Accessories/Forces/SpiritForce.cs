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

            /*string tooltip =
@"''
Double tap down to call an ancient storm to the cursor location
Any projectiles shot through your storm gain double pierce and 50% damage
If you reach zero HP you cheat death, returning with 20 HP
For a few seconds after reviving, you are immune to all damage and spawn bones everywhere
Bones scale with throwing damage
Summons a pet Baby Dino
You gain a shield that can reflect projectiles
Summons an Enchanted Sword familiar
Summons a magical fairy
Any damage has a chance to spawn damaging and healing orbs
Summons a pet Wisp
Attacks will inflict a Infested on enemies
Infested deals increasing damage over time
Summons a Tiki Spirit
";

            if (thorium != null)
            {
                tooltip +=
@"Your healing streak fuels an ancient crucible every 100 life up to 500 total
The crucible increases your radiant damage by 5% per 100 life in the crucible
While worn, taking fatal damage will instead return you to 100 life and instantly teleport you back to your home (2 minute recharge time)
Killing enemies or continually damaging bosses generates soul wisps
After generating 5 wisps, they are instantly consumed to heal you for 10 life
After healing a nearby ally, a life spirit is released from you
This spirit seeks out your ally with the lowest life and heals them for 2 life";
            }

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
            //spectre works for all,
            modPlayer.SpiritForce = true;
            //storm
            modPlayer.ForbiddenEffect();
            //revive, bone zone, pet
            modPlayer.FossilEffect(20, hideVisual);
            //sword, shield, pet
            modPlayer.HallowEffect(hideVisual, 80);
            //orbs, pet
            modPlayer.SpectreEffect(hideVisual);
            //infested debuff, pet
            modPlayer.TikiEffect(hideVisual);

            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            //karmic holder
            thoriumPlayer.karmicHolder = true;
            if (thoriumPlayer.healStreak >= 0 && player.ownedProjectileCounts[thorium.ProjectileType("KarmicHolderPro")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("KarmicHolderPro"), 0, 0f, player.whoAmI, 0f, 0f);
            }
            //ghastly carapace
            if (!thoriumPlayer.lifePrevent)
            {
                player.AddBuff(thorium.BuffType("GhastlySoul"), 60, true);
            }
            thoriumPlayer.soulStorage = true;
            //spirit trapper set bonus
            thoriumPlayer.spiritTrapper = true;
            //inner flame
            thoriumPlayer.spiritFlame = true;
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
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}