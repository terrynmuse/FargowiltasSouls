using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ID.ItemID;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Waist)]
    public class GladiatorsSoul : ModItem
    {
        private Mod thorium;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berserker's Soul");
            Tooltip.SetDefault(
                @"'None shall live to tell the tale'
30% increased melee damage
20% increased melee speed
15% increased melee crit chance
Increased melee knockback
Grants the effects of the Yoyo Bag");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 1000000;
            item.rare = -12;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += .3f;
            player.meleeSpeed += .2f;
            player.meleeCrit += 15;

            //gauntlet
            player.magmaStone = true;
            player.kbGlove = true;
            //yoyo bag
            player.counterWeight = 556 + Main.rand.Next(6);
            player.yoyoGlove = true;
            player.yoyoString = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "BarbariansEssence");

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(FireGauntlet);
                recipe.AddIngredient(YoyoBag);
                //crystal spear tip
                //cursed flail core
                
                recipe.AddIngredient(Arkhalis);
                //ebony tail
                recipe.AddIngredient(IceSickle);
                //blitzzard
                //poseidon charge
                //absinthe fury
                //surtrs sword
                //prime fury
                recipe.AddIngredient(MonkStaffT2);
                recipe.AddIngredient(TerraBlade);
                recipe.AddIngredient(ScourgeoftheCorruptor);
                recipe.AddIngredient(Kraken);
                //terrarium hyper disk
                //spearmint
                //wyvern slayer
                recipe.AddIngredient(Flairon);
                recipe.AddIngredient(TheHorsemansBlade);
                recipe.AddIngredient(NorthPole);
                recipe.AddIngredient(InfluxWaver);
                recipe.AddIngredient(Meowmere);
                /*
            SoU
            terarians last knife
            ocean judgement
                 * */
            }
            else
            {
                recipe.AddIngredient(FireGauntlet);
                recipe.AddIngredient(YoyoBag);
                recipe.AddIngredient(Arkhalis);
                recipe.AddIngredient(IceSickle);
                recipe.AddIngredient(MonkStaffT2);
                recipe.AddIngredient(TerraBlade);
                recipe.AddIngredient(ScourgeoftheCorruptor);
                recipe.AddIngredient(Kraken);
                recipe.AddIngredient(Flairon);
                recipe.AddIngredient(TheHorsemansBlade);
                recipe.AddIngredient(NorthPole);
                recipe.AddIngredient(InfluxWaver);
                recipe.AddIngredient(Meowmere);
            }
            
            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
