using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class UniverseSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Universe");
            Tooltip.SetDefault(
@"'The heavens themselves bow to you'
66% increased all damage
50% increased use speed for all weapons
50% increased shoot speed
25% increased all critical chance
Crits deal 5x damage
All weapons have double knockback and have auto swing
Increases your maximum mana by 300
Increases your max number of minions by 8
Increases your max number of sentries by 4
Grants the effects of the Yoyo Bag, Sniper Scope, Celestial Cuffs, and Mana Flower
All attacks inflict Flames of the Universe");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(12, 5));
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 5000000;
            item.rare = -12;
            item.expert = true;

            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            modPlayer.AllDamageUp(.66f);
            modPlayer.AllCritUp(25);
            //use speed, velocity, debuffs, crit dmg, mana up, double knockback
            modPlayer.UniverseEffect = true;
            
            if (Soulcheck.GetValue("Universe Speedup"))
            {
                modPlayer.AttackSpeed *= 1.5f;
            }

            player.maxMinions += 8;
            player.maxTurrets += 4;

            //accessorys
            player.counterWeight = 556 + Main.rand.Next(6);
            player.yoyoGlove = true;
            player.yoyoString = true;
            if (Soulcheck.GetValue("Universe Scope"))
            {
                player.scope = true;
            }
            player.manaFlower = true;
            player.manaMagnet = true;
            player.magicCuffs = true;

            //if (player.controlUseItem && !player.HeldItem.autoReuse) player.HeldItem.autoReuse = true;
            //if (player.HeldItem.useStyle == 1) player.HeldItem.scale = 2;
        }

        private void Healer(Player player)
        {
            
        }

        private void Bard(Player player)
        {
            
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GladiatorsSoul");
            recipe.AddIngredient(null, "SharpshootersSoul");
            recipe.AddIngredient(null, "ArchWizardsSoul");
            recipe.AddIngredient(null, "ConjuristsSoul");
            recipe.AddIngredient(null, "OlympiansSoul");

            /*if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "GuardianAngelsSoul");
                recipe.AddIngredient(null, "BardSoul");
                recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("TheRing"));
                
                /*
                
                black midi - bard
                
            }*/

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
