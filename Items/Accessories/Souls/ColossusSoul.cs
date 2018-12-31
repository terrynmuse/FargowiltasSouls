using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Shield)]
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
Effects of the Brain of Confusion, Star Veil, Sweetheart Necklace, and Bee Cloak
Effects of Spore Sac, Paladin's Shield, and Frozen Turtle Shell
Enemies are more likely to target you");

//sweet vengence
//Increases movement speed after being damaged
//Increases length of invincibility after taking damage
//Causes stars to fall when damaged
//Causes bees to appear when damaged

//cape of the survivor
//Increases defense by 4
//Damage taken reduced by 5%
//Provides immunity to Weakness and Broken Armor
//Slightly increases length of invincibility after taking damage
//While active, damage taken cannot exceed 150
//This effect can only occur once every 15 seconds

//oceans retaliation
//Defense increased by 3
//Taking more than 4 damage will unleash a globule of energy
//Touching the globule will recover 35% of the damage you took
//30% of the damage you take is also dealt to the attacker
//Enemies that directly attack you will be poisoned and envenomed

//terrarium defender
//Grants immunity to most debuffs
//Grants immunity to knockback and fire blocks
//Maximum life increased by 20
//Prolonges after hit invincibility
//When above 25% life, absorbs 25% of damage done to nearby players on your team
//When below 20% life, the shield will rapidly regenerate your life
//When below 25% life, your defense is increased greatly

//blast shield
//Damage taken reduced by 10%
//Enemies are more likely to attack you
//Immune to enemy knockback
//Taking damage will unleash a volatile explosion all around you
//This effect needs to recharge for 2 seconds after triggering

//life quartz shield
//Increases the rate at which you regenerate life
//Receiving damage below 75% surrounds you in a protective bubble
//While in the bubble, you will recover life equal to your bonus healing every second
//Additionally, damage taken will be reduced by 15%
//This effect needs to recharge for 1 minute after triggering
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
            player.SporeSac();
            player.sporeSac = true;
            //flesh knuckles
            player.aggro += 400;
            //frozen turtle shell
            if (player.statLife <= player.statLifeMax2 * 0.5) player.AddBuff(BuffID.IceBarrier, 5, true);
            //paladins shield
            if (player.statLife > player.statLifeMax2 * .25)
            {
                player.hasPaladinShield = true;
                for (int k = 0; k < 255; k++)
                {
                    Player target = Main.player[k];

                    if (target.active && player != target && Vector2.Distance(target.Center, player.Center) < 400) target.AddBuff(BuffID.PaladinsShield, 30);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            /*if (Fargowiltas.Instance.ThoriumLoaded)
            {
            /*
            terrarium defender
            blast shield
            cape of the survivor
            ocean retaliation
            sweet vengeance
            
            life quartz shield
            
            
                recipe.AddIngredient(ItemID.HandWarmer);
                //recipe.AddIngredient(ItemID.BrainOfConfusion);
                recipe.AddIngredient(ItemID.PocketMirror);
                recipe.AddIngredient(ItemID.CharmofMyths);
                recipe.AddIngredient(ItemID.SporeSac);
                recipe.AddIngredient(ItemID.FleshKnuckles);
            }*/
            //else
            //{
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
            //}

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
