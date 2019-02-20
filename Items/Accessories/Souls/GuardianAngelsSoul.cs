using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.ID;
using System.Collections.Generic;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Face)]
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
30% increased radiant damage
20% increased healing and radiant casting speed
15% increased radiant critical strike chance
Healing spells will heal an additional 5 life
Points you towards the ally with the least health
Healing an ally will increase your movement speed and increase their life regen and defense
Upon drinking a healing potion, all allies will recover 25 life and 50 mana
Nearby allies take 10% reduced damage
Nearby allies that die drop a wisp of spirit energy");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 750000;
            item.rare = 11;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(255, 30, 247));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            //general
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.radiantBoost += 0.4f;
            thoriumPlayer.radiantSpeed -= 0.25f;
            thoriumPlayer.healingSpeed += 0.25f;
            thoriumPlayer.radiantCrit += 20;
            //support stash
            thoriumPlayer.supportSash = true;
            thoriumPlayer.quickBelt = true;
            //saving grace
            thoriumPlayer.crossHeal = true;
            thoriumPlayer.healBloom = true;
            //soul guard
            thoriumPlayer.graveGoods = true;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && player2 != player && Vector2.Distance(player2.Center, player.Center) < 400f)
                {
                    player2.AddBuff(thorium.BuffType("AegisAura"), 30, false);
                }
            }
            //archdemon's curse
            thoriumPlayer.darkAura = true;
            //archangels heart
            thoriumPlayer.healBonus += 5;
            //medical bag
            thoriumPlayer.medicalAcc = true;
            //head mirror arrow 
            if (Soulcheck.GetValue("Head Mirror"))
            {
                float num = 0f;
                int num2 = player.whoAmI;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && Main.player[i] != player && !Main.player[i].dead && (Main.player[i].statLifeMax2 - Main.player[i].statLife) > num)
                    {
                        num = (Main.player[i].statLifeMax2 - Main.player[i].statLife);
                        num2 = i;
                    }
                }
                if (player.ownedProjectileCounts[thorium.ProjectileType("HealerSymbol")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("HealerSymbol"), 0, 0f, player.whoAmI, 0f, 0f);
                }
                for (int j = 0; j < 1000; j++)
                {
                    Projectile projectile = Main.projectile[j];
                    if (projectile.active && projectile.owner == player.whoAmI && projectile.type == thorium.ProjectileType("HealerSymbol"))
                    {
                        projectile.timeLeft = 2;
                        projectile.ai[1] = num2;
                    }
                }
            }
            
        }
        
        private readonly string[] items =
        {
            "SupportSash",
            "SavingGrace",
            "SoulGuard",
            "ArchDemonCurse",
            "ArchangelHeart",
            "MedicalBag",
            "TeslaDefibrillator",
            "MoonlightStaff",
            "TerrariumHolyScythe",
            "TerraScythe",
            "PheonixStaff", 
            "ShieldDroneBeacon", 
            "LifeAndDeath" 
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            //recipe.AddIngredient(null, "BardEssence");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(mod, "CrucibleCosmosSheet");
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
