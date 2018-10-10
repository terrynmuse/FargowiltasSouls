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
All swords are twice as large
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
            item.value = 2000000;
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

            player.maxMinions += 8;
            player.maxTurrets += 4;

            //accessorys
            player.counterWeight = 556 + Main.rand.Next(6);
            player.yoyoGlove = true;
            player.yoyoString = true;
            if (Soulcheck.GetValue("Universe Scope"))
                player.scope = true;
            player.manaFlower = true;
            player.manaMagnet = true;
            player.magicCuffs = true;

            if (player.controlUseItem && !player.HeldItem.autoReuse) player.HeldItem.autoReuse = true;

            if (player.HeldItem.useStyle == 1) player.HeldItem.scale = 2;
        }

        private void Healer(Player player)
        {
            //general
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod"));

            thoriumPlayer.radiantBoost += 0.66f; //radiant damage
            thoriumPlayer.radiantSpeed -= 0.25f; //radiant casting speed
            thoriumPlayer.healingSpeed += 0.25f; //healing spell casting speed
            thoriumPlayer.radiantCrit += 25;

            //archdemon's curse
            thoriumPlayer.darkAura = true; //Dark intent purple coloring effect

            //support stash
            thoriumPlayer.quickBelt = true; //bonus movement from healing
            thoriumPlayer.apothLife = true; //drinking health potion recovers life
            thoriumPlayer.apothMana = true; //drinking health potion recovers mana

            //ascension statuette
            thoriumPlayer.ascension = true; //turn into healing thing on death

            //wynebg..........
            thoriumPlayer.Wynebgwrthucher = true; //heals on healing ally

            //archangels heart
            thoriumPlayer.healBonus += 5; //Bonus healing

            //saving grace
            thoriumPlayer.crossHeal = true; //bonus defense in heal
            thoriumPlayer.healBloom = true; //bonus life regen on heal
        }

        private void Bard(Player player)
        {
            //general
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(ModLoader.GetMod("ThoriumMod"));

            thoriumPlayer.symphonicDamage += 0.66f; //symphonic damage
            thoriumPlayer.symphonicCrit += 25;
            thoriumPlayer.symphonicSpeed += .25f;

            //woofers
            thoriumPlayer.subwooferFrost = true;
            thoriumPlayer.subwooferVenom = true;
            thoriumPlayer.subwooferIchor = true;
            thoriumPlayer.subwooferCursed = true;
            thoriumPlayer.subwooferTerrarium = true;

            //type buffs
            thoriumPlayer.bardHomingBool = true;
            thoriumPlayer.bardHomingBonus = 5f;
            thoriumPlayer.bardMute2 = true;
            thoriumPlayer.tuner2 = true;
            thoriumPlayer.bardBounceBonus = 5;
        }

        /*private void Gauntlet(Player player)
        {
            player.GetModPlayer<CalamityMod.CalamityPlayer>(_calamity).eGauntlet = true;
        }

        private void Talisman(Player player)
        {
            player.GetModPlayer<CalamityMod.CalamityPlayer>(_calamity).eTalisman = true;
        }

        private void Waifus(Player player)
        {
            player.GetModPlayer<CalamityMod.CalamityPlayer>(_calamity).brimstoneWaifu = true;
            player.GetModPlayer<CalamityMod.CalamityPlayer>(_calamity).sandBoobWaifu = true;
            player.GetModPlayer<CalamityMod.CalamityPlayer>(_calamity).sandWaifu = true;
            player.GetModPlayer<CalamityMod.CalamityPlayer>(_calamity).cloudWaifu = true;
            player.GetModPlayer<CalamityMod.CalamityPlayer>(_calamity).sirenWaifu = true;
        }

        private void BlueMagnet(Player player)
        {
            player.GetModPlayer<Bluemagic.BluemagicPlayer>(ModLoader.GetMod("Bluemagic")).manaMagnet2 = true;
        }*/

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GladiatorsSoul");
            recipe.AddIngredient(null, "SharpshootersSoul");
            recipe.AddIngredient(null, "ArchWizardsSoul");
            recipe.AddIngredient(null, "ConjuristsSoul");
            recipe.AddIngredient(null, "OlympiansSoul");

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "GuardianAngelsSoul");
                recipe.AddIngredient(null, "BardSoul");
                recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("TheRing"));

                recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("CrystalEyeMask"));
                
                /*
                plague lords flask
                */
            }

            if (Fargowiltas.Instance.BlueMagicLoaded) recipe.AddIngredient(ModLoader.GetMod("Bluemagic").ItemType("AvengerSeal"));

            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
