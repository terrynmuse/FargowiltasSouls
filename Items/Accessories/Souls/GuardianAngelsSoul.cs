using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Face)]
    public class GuardianAngelsSoul : ModItem
    {
        private string _tooltip = null;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guardian Angel's Soul");

            if (ModLoader.GetLoadedMods().Contains("ThoriumMod"))
                Tooltip.SetDefault(
                    @"'Divine Intervention'
40% increased radiant damage
25% increased healing and radiant casting speed
20% increased radiant critical strike chance
Healing spells will heal an additional 5 life
Healing an ally will increase your movement speed and increase their life regen and defense
Upon drinking a healing potion, all allies will recover 25 life and 40 mana
You and nearby allies will take 8% reduced damage
Taking fatal damage unleashes your inner spirit");
            else
                Tooltip.SetDefault("'Divine Intervention' \n" +
                                   "-Enable Thorium for this soul's full potential-");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 750000;
            item.expert = true;
            item.rare = -12;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            //Turn undead
            player.aggro -= 50;
            player.AddBuff(ModLoader.GetMod("ThoriumMod").BuffType("AegisAura"), 30, false);

            for (int k = 0; k < 255; k++)
            {
                Player target = Main.player[k];

                if (target.active && target != player && Vector2.Distance(target.Center, player.Center) < 275)
                    target.AddBuff(ModLoader.GetMod("ThoriumMod").BuffType("AegisAura"), 30, false);
            }

            //the rest
            Healer(player);
        }

        private static void Healer(Player player)
        {
            //general
            player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod")).radiantBoost += 0.4f; //radiant damage
            player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod")).radiantSpeed -= 0.25f; //radiant casting speed
            player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod")).healingSpeed += 0.25f; //healing spell casting speed
            player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod")).radiantCrit += 20;

            //archdemon's curse
            player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod")).darkAura = true; //Dark intent purple coloring effect

            //support stash
            player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod")).quickBelt = true; //bonus movement from healing
            player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod")).apothLife = true; //drinking health potion recovers life
            player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod")).apothMana = true; //drinking health potion recovers mana

            //ascension statuette
            player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod")).ascension = true; //turn into healing thing on death

            //wynebg..........
            player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod")).Wynebgwrthucher = true; //heals on healing ally

            //archangels heart
            player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod")).healBonus += 5; //Bonus healing

            //saving grace
            player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod")).crossHeal = true; //bonus defense in heal
            player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod")).healBloom = true; //bonus life regen on heal
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("ArchDemonCurse"));
            recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("SupportSash"));
            recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("TurnUndead"));
            recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("AscensionStatuette"));
            recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("Wynebgwrthucher"));
            recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("ArchangelHeart"));
            recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("SavingGrace"));
            recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("BoneBaton"));
            recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("TerraScythe"));
            recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("ChristmasCheer"));
            recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("TerrariumHolyScythe"));
            recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("RealitySlasher"));

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
