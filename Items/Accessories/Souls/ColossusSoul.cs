using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Shield)]
    public class ColossusSoul : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Colossus Soul");
			Tooltip.SetDefault(
@"'Nothing can stop you'
Increases HP by 100
15% damage reduction
Increases life regeneration by 5
Grants immunity to knockback and several debuffs
Enemies are more likely to target you
Effects of the Brain of Confusion, Star Veil, Sweetheart Necklace, and Bee Cloak
Effects of Spore Sac, Paladin's Shield, and Frozen Turtle Shell");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.defense = 10;
            item.value = 1000000;
			item.expert = true;
			item.rare = -12;
            
            //item.shieldSlot = 4;
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
            player.SporeSac();
            player.sporeSac = true;
            //flesh knuckles
            player.aggro += 400;
            //frozen turtle shell
            if (player.statLife <= player.statLifeMax2 * 0.5)
            {
                player.AddBuff(BuffID.IceBarrier, 5, true);
            }
            //paladins shield
            if (player.statLife > player.statLifeMax2 * .25)
            {
                player.hasPaladinShield = true;
                for (int k = 0; k < 255; k++)
                {
                    Player target = Main.player[k];

                    if (target.active && player != target && Vector2.Distance(target.Center, player.Center) < 400)
                    {
                        target.AddBuff(BuffID.PaladinsShield, 30);
                    }
                }
            }
        }

        /*public void CalamityTank(Player player)
		{
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("BrimstoneFlames")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("HolyLight")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("GlacialState")] = true;
				player.GetModPlayer<CalamityMod.CalamityPlayer>(ModLoader.GetMod("CalamityMod")).elysianAegis = true;
				player.GetModPlayer<CalamityMod.CalamityPlayer>(ModLoader.GetMod("CalamityMod")).dashMod = 4;
				player.GetModPlayer<CalamityMod.CalamityPlayer>(ModLoader.GetMod("CalamityMod")).aSpark = true;
				player.GetModPlayer<CalamityMod.CalamityPlayer>(ModLoader.GetMod("CalamityMod")).gShell = true;
				player.GetModPlayer<CalamityMod.CalamityPlayer>(ModLoader.GetMod("CalamityMod")).fCarapace = true;
				player.GetModPlayer<CalamityMod.CalamityPlayer>(ModLoader.GetMod("CalamityMod")).absorber = true;
		}
		
		public void BlueTank(Player player)
		{
				player.GetModPlayer<Bluemagic.BluemagicPlayer>(ModLoader.GetMod("Bluemagic")).lifeMagnet2 = true;
				player.GetModPlayer<Bluemagic.BluemagicPlayer>(ModLoader.GetMod("Bluemagic")).crystalCloak = true;
		}*/

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			
			if(Fargowiltas.Instance.ThoriumLoaded)
			{

            }
            else
            {
                recipe.AddIngredient(ItemID.HandWarmer);
                recipe.AddIngredient(ItemID.WormScarf);
                recipe.AddIngredient(ItemID.BrainOfConfusion);
                recipe.AddIngredient(ItemID.PocketMirror);
                recipe.AddIngredient(ItemID.CharmofMyths);
                recipe.AddIngredient(ItemID.BeeCloak);
                recipe.AddIngredient(ItemID.SweetheartNecklace);
                recipe.AddIngredient(ItemID.StarVeil);
                recipe.AddIngredient(ItemID.SporeSac);
                recipe.AddIngredient(ItemID.FleshKnuckles);
                recipe.AddIngredient(ItemID.FrozenTurtleShell);
                recipe.AddIngredient(ItemID.PaladinsShield);
                recipe.AddIngredient(ItemID.AnkhShield);
            }

            //tank.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}