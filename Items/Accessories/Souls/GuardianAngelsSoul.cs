using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.ID;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Face)]
    public class GuardianAngelsSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guardian Angel's Soul");

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

//archangels heart
//Maximum life increased by 20
//Maximum mana increased by 20
//5% increased radiant damage
//15% increased healing speed
//Healing spells will heal an additional 1 life

//archdemons curse
//Corrupts your radiant powers
//Maximum life increased by 20
//10% increased radiant casting speed
//20% increased radiant damage
//12% increased radiant critical strike chance

//medical bag
//Points you towards the ally with the least health
//That ally will receive 1 additional healing
//Healing allies that are in combat grants them 1 life recovery for 10 seconds

//support sash
//Healing an ally will significantly increase your movement speed
//Upon drinking a healing potion, all nearby players recover 25 life and 50 mana
//Aditionally, allies will receive increased healing for 30 seconds

//saving grace
//5% increased healing speed
//Healing an ally increases their life regeneration and regeneration rate
//Healing spells will increase the healed targets defense by 20 for 15 seconds
//Increased length of invincibility after taking damage

//soul guard
//Enemies are less likely to target you
//You and nearby allies take 10% reduced damage
//Nearby allies that die drop a wisp of spirit energy
//Players that touch the wisp replenish health equal to 15% of the allies max health
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
        
        private readonly string[] items =
        {
            "SupportSash",
            "SavingGrace",
            "SoulGuard",
            "ArchDemonCurse",
            "ArchangelHeart",
            "MedicalBag",
            
            
            "TeslaDefibrillator - frankensteins drop",
            "MoonlightStaff - drop lycan",
            "TerrariumHolyScythe",
            "TerraScythe",
            "PhoenixStaff", //biome chest
            "ShieldDroneBeacon", 
            "LifeandDeath" //ml drop
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);
            
            //essence
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
