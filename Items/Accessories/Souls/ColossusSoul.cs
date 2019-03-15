using CalamityMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Shield)]
    public class ColossusSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Colossus Soul");

            string tooltip =
@"'Nothing can stop you'
Increases HP by 100
15% damage reduction
Increases life regeneration by 5
Grants immunity to knockback and several debuffs
Enemies are more likely to target you
Effects of Brain of Confusion, Star Veil, and Sweetheart Necklace
Effects of Bee Cloak, Spore Sac, Paladin's Shield, and Frozen Turtle Shell";

            if (thorium != null)
            {
                tooltip += "\nEffects of Ocean's Retaliation and Cape of the Survivor\nEffects of Blast Shield and Terrarium Defender";
            }

            if (calamity != null)
            {
                tooltip += "\nEffects of Asgardian Aegis";
            }

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.defense = 10;
            item.value = 1000000;
            item.rare = 11;
            item.shieldSlot = 4;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(252, 59, 0));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 100;
            player.endurance += 0.15f;
            player.lifeRegen += 5;

            //hand warmer, pocket mirror, ankh shield
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Stoned] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.ChaosState] = true;
            player.noKnockback = true;
            player.fireWalk = true;
            //brain of confusion
            player.brainOfConfusion = true;
            //charm of myths
            player.pStone = true;
            //bee cloak, sweet heart necklace, star veil
            player.starCloak = true;
            player.bee = true;
            player.panic = true;
            player.longInvince = true;
            //spore sac
            if (Soulcheck.GetValue("Spore Sac"))
            {
                player.SporeSac();
                player.sporeSac = true;
            }
            //flesh knuckles
            player.aggro += 400;
            //frozen turtle shell
            if (player.statLife <= player.statLifeMax2 * 0.5) player.AddBuff(BuffID.IceBarrier, 5, true);
            //paladins shield
            player.hasPaladinShield = true;
            if (player.statLife > player.statLifeMax2 * .25)
            {
                for (int k = 0; k < 255; k++)
                {
                    Player target = Main.player[k];

                    if (target.active && player != target && Vector2.Distance(target.Center, player.Center) < 400) target.AddBuff(BuffID.PaladinsShield, 30);
                }
            }

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);

            if (Fargowiltas.Instance.CalamityLoaded) Calamity(player);
        }

        

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //terrarium defender
            if (player.statLife < player.statLifeMax * 0.2f)
            {
                player.AddBuff(thorium.BuffType("TerrariumRegen"), 10, true);
                player.lifeRegen += 20;
            }
            if (player.statLife < player.statLifeMax * 0.25f)
            {
                player.AddBuff(thorium.BuffType("TerrariumDefense"), 10, true);
                player.statDefense += 20;
            }
            //blast shield
            thoriumPlayer.blastHurt = true;
            //cape of the survivor
            if (player.FindBuffIndex(thorium.BuffType("Corporeal")) < 0)
            {
                thoriumPlayer.spiritBand2 = true;
            }
            //sweet vengeance
            thoriumPlayer.sweetVengeance = true;
            //oceans retaliation
            thoriumPlayer.turtleShield2 = true;
            thoriumPlayer.SpinyShield = true;
        }

        private void Calamity(Player player)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);
            //rampart of dieties
            modPlayer.dAmulet = true;
            if ((double)player.statLife <= (double)player.statLifeMax2 * 0.15)
            {
                player.endurance += 0.05f;
                player.statDefense += 10;
            }
            //becase calamity made it itself for some reason no duplicate
            player.starCloak = false;
            //asgardian aegis
            modPlayer.dashMod = 4;
            modPlayer.elysianAegis = true;
            player.buffImmune[calamity.BuffType("BrimstoneFlames")] = true;
            player.buffImmune[calamity.BuffType("HolyLight")] = true;
            player.buffImmune[calamity.BuffType("GlacialState")] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.HandWarmer);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(ItemID.BrainOfConfusion);
                recipe.AddIngredient(ItemID.PocketMirror);
                recipe.AddIngredient(ItemID.CharmofMyths);
                recipe.AddIngredient(thorium.ItemType("SweetVengeance"));
                recipe.AddIngredient(ItemID.FleshKnuckles);
                recipe.AddIngredient(thorium.ItemType("OceanRetaliation"));
                recipe.AddIngredient(thorium.ItemType("DeathEmbrace")); //cape of the survivor apparentlty
                recipe.AddIngredient(ItemID.SporeSac);
                recipe.AddIngredient(thorium.ItemType("BlastShield"));
                recipe.AddIngredient(thorium.ItemType("TerrariumDefender"));
            }
            else
            {
                recipe.AddIngredient(ItemID.WormScarf);
                recipe.AddIngredient(ItemID.BrainOfConfusion);
                recipe.AddIngredient(ItemID.PocketMirror);
                recipe.AddIngredient(ItemID.CharmofMyths);
                recipe.AddIngredient(ItemID.BeeCloak);
                recipe.AddIngredient(ItemID.SweetheartNecklace);
                recipe.AddIngredient(ItemID.StarVeil);
                recipe.AddIngredient(ItemID.FleshKnuckles);
                recipe.AddIngredient(ItemID.SporeSac);

                if (!Fargowiltas.Instance.CalamityLoaded)
                {
                    recipe.AddIngredient(ItemID.FrozenTurtleShell);
                    recipe.AddIngredient(ItemID.PaladinsShield);
                    recipe.AddIngredient(ItemID.AnkhShield);
                }
            }

            if (Fargowiltas.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(calamity.ItemType("RampartofDeities"));
                recipe.AddIngredient(calamity.ItemType("AsgardianAegis"));
            }

            recipe.AddTile(mod, "CrucibleCosmosSheet");
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
