using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class UniverseSoul : ModItem
    {
        public const int FIRE_PROJECTILES = 3;
        public const float FIRE_ANGLE_SPREAD = 120;
        public int FireCountdown = 0;
        private Mod _calamity = ModLoader.GetMod("CalamityMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Universe");
            Tooltip.SetDefault("'The heavens themselves bow to you' \n" +
                                "66% increased all damage \n" +
                                "50% chance to not consume any ammo or thrown item \n" +
                                "50% reduced mana usage \n" +
                                "35% increased melee speed \n" +
                                "35% increased throwing velocity \n" +
                                "25% increased all critical chance \n" +
                                "Crits deal 8x damage\n" +
                                "Increases your maximum mana by 150 \nIncreases your max number of minions by 8 \nIncreases your max number of sentries by 6 \nIncreased pickup range for mana stars and hearts\n" +
                                 "Increases all knockback \nGrants all the effects of the Yoyo Bag \nAll attacks inflict many debuffs\n" +
                                "All other effects of material souls");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(12, 5));
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 1500000;
            item.rare = -12;
            item.expert = true;

            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        /*public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(mod, "universe", "Damage Soul");
            line.overrideColor = new Color(255, 131, 0);
            tooltips.Add(line);
		}*/

        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            //mage
            player.manaCost -= .5f;
            player.magicDamage += .66f;
            player.magicCrit += 25;
            player.statManaMax2 += 150;
            player.magicCuffs = true;
            player.manaMagnet = true;
            player.manaFlower = true;

            //warrior
            if (Soulcheck.GetValue("Melee Speed"))
            {
                player.meleeSpeed += .35f;
            }
            player.meleeDamage += .66f;
            player.meleeCrit += 25;
            player.kbGlove = true;
            //yoyo bag
            player.counterWeight = 556 + Main.rand.Next(6);
            player.yoyoGlove = true;
            player.yoyoString = true;

            //ranger
            player.rangedCrit += 25;
            player.rangedDamage += .66f;

            //summoner
            player.maxMinions += 8;
            player.minionDamage += .66f;
            player.minionKB += 4f;
            player.maxTurrets += 6;

            //thrower 
            player.thrownVelocity += 0.35f;
            player.thrownDamage += .66f;
            player.thrownCrit += 25;
            player.GetModPlayer<FargoPlayer>(mod).ThrowSoul = true;

            //buffs
            player.kbBuff = true;
            player.nightVision = true;
            player.lifeMagnet = true;



            player.GetModPlayer<FargoPlayer>(mod).UniverseEffect = true;

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                //turn undead
                player.aggro -= 50;
                player.AddBuff(ModLoader.GetMod("ThoriumMod").BuffType("AegisAura"), 30, false);
                for (int k = 0; k < 255; k++)
                {
                    Player target = Main.player[k];

                    if (target.active && target != player && Vector2.Distance(target.Center, player.Center) < 275)
                    {
                        target.AddBuff(ModLoader.GetMod("ThoriumMod").BuffType("AegisAura"), 30, false);
                    }
                }

                //the rest
                Healer(player);
                Bard(player);
            }

            if (Fargowiltas.Instance.BlueMagicLoaded)
            {
                BlueMagnet(player);
            }

            //heart of elements and friends
            if (Fargowiltas.Instance.CalamityLoaded)
            {
                Talisman(player);
                Gauntlet(player);

                if (!hideVisual)
                {
                    Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, Main.DiscoR / 255f, Main.DiscoG / 255f, Main.DiscoB / 255f);

                    Waifus(player);

                    if (player.whoAmI == Main.myPlayer)
                    {
                        int damage = 300;
                        float damageMult = 2.5f;
                        if (player.FindBuffIndex(_calamity.BuffType("BrimstoneWaifu")) == -1)
                        {
                            player.AddBuff(_calamity.BuffType("BrimstoneWaifu"), 3600);
                        }
                        if (player.ownedProjectileCounts[_calamity.ProjectileType("BigBustyRose")] < 1)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, _calamity.ProjectileType("BigBustyRose"), (int)(damage * damageMult), 2f, Main.myPlayer);
                        }
                        if (player.FindBuffIndex(_calamity.BuffType("SirenLure")) == -1)
                        {
                            player.AddBuff(_calamity.BuffType("SirenLure"), 3600);
                        }
                        if (player.ownedProjectileCounts[_calamity.ProjectileType("SirenLure")] < 1)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, _calamity.ProjectileType("SirenLure"), 0, 0f, Main.myPlayer);
                        }
                        if (player.FindBuffIndex(_calamity.BuffType("DrewsSandyWaifu")) == -1)
                        {
                            player.AddBuff(_calamity.BuffType("DrewsSandyWaifu"), 3600);
                        }
                        if (player.ownedProjectileCounts[_calamity.ProjectileType("DrewsSandyWaifu")] < 1)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, _calamity.ProjectileType("DrewsSandyWaifu"), (int)(damage * damageMult * 1.5f), 2f, Main.myPlayer);
                        }
                        if (player.FindBuffIndex(_calamity.BuffType("SandyWaifu")) == -1)
                        {
                            player.AddBuff(_calamity.BuffType("SandyWaifu"), 3600);
                        }
                        if (player.ownedProjectileCounts[_calamity.ProjectileType("SandyWaifu")] < 1)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, _calamity.ProjectileType("SandyWaifu"), (int)(damage * damageMult * 1.5f), 2f, Main.myPlayer);
                        }
                        if (player.FindBuffIndex(_calamity.BuffType("CloudyWaifu")) == -1)
                        {
                            player.AddBuff(_calamity.BuffType("CloudyWaifu"), 3600);
                        }
                        if (player.ownedProjectileCounts[_calamity.ProjectileType("CloudyWaifu")] < 1)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, _calamity.ProjectileType("CloudyWaifu"), (int)(damage * damageMult), 2f, Main.myPlayer);
                        }
                    }
                }

                if (player.whoAmI != Main.myPlayer || player.velocity.Y != 0f || player.grappling[0] != -1) return;
                int num4 = (int)player.Center.X / 16;
                int num5 = (int)(player.position.Y + player.height - 1f) / 16;
                if (Main.tile[num4, num5] == null)
                {
                    Main.tile[num4, num5] = new Tile();
                }

                if (Main.tile[num4, num5].active() || Main.tile[num4, num5].liquid != 0 ||
                    Main.tile[num4, num5 + 1] == null || !WorldGen.SolidTile(num4, num5 + 1)) return;
                Main.tile[num4, num5].frameY = 0;
                Main.tile[num4, num5].slope(0);
                Main.tile[num4, num5].halfBrick(false);
                if (Main.tile[num4, num5 + 1].type == 0)
                {
                    if (Main.rand.Next(1000) == 0)
                    {
                        Main.tile[num4, num5].active(true);
                        Main.tile[num4, num5].type = 227;
                        Main.tile[num4, num5].frameX = (short)(34 * Main.rand.Next(1, 13));
                        while (Main.tile[num4, num5].frameX == 144)
                        {
                            Main.tile[num4, num5].frameX = (short)(34 * Main.rand.Next(1, 13));
                        }
                    }
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendTileSquare(-1, num4, num5, 1);
                    }
                }
                if (Main.tile[num4, num5 + 1].type == 2)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.tile[num4, num5].active(true);
                        Main.tile[num4, num5].type = 3;
                        Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(6, 11));
                        while (Main.tile[num4, num5].frameX == 144)
                        {
                            Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(6, 11));
                        }
                    }
                    else
                    {
                        Main.tile[num4, num5].active(true);
                        Main.tile[num4, num5].type = 73;
                        Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(6, 21));
                        while (Main.tile[num4, num5].frameX == 144)
                        {
                            Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(6, 21));
                        }
                    }
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendTileSquare(-1, num4, num5, 1);
                    }
                }
                else if (Main.tile[num4, num5 + 1].type == 109)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.tile[num4, num5].active(true);
                        Main.tile[num4, num5].type = 110;
                        Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(4, 7));
                        while (Main.tile[num4, num5].frameX == 90)
                        {
                            Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(4, 7));
                        }
                    }
                    else
                    {
                        Main.tile[num4, num5].active(true);
                        Main.tile[num4, num5].type = 113;
                        Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(2, 8));
                        while (Main.tile[num4, num5].frameX == 90)
                        {
                            Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(2, 8));
                        }
                    }
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendTileSquare(-1, num4, num5, 1);
                    }
                }
                else if (Main.tile[num4, num5 + 1].type == 60)
                {
                    Main.tile[num4, num5].active(true);
                    Main.tile[num4, num5].type = 74;
                    Main.tile[num4, num5].frameX = (short)(18 * Main.rand.Next(9, 17));
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendTileSquare(-1, num4, num5, 1);
                    }
                }
            }
        }

        private void Healer(Player player)
        {
            //general
            ThoriumMod.ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumMod.ThoriumPlayer>(ModLoader.GetMod("ThoriumMod"));
            
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
            ThoriumMod.ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumMod.ThoriumPlayer>(ModLoader.GetMod("ThoriumMod"));
            
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

        private void Gauntlet(Player player)
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
        }


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

                if (!Fargowiltas.Instance.CalamityLoaded)
                {
                    recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("CrystalEyeMask"));
                }
            }

            if (Fargowiltas.Instance.BlueMagicLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("Bluemagic").ItemType("AvengerSeal"));
            }

            if (Fargowiltas.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(_calamity.ItemType("DraedonsExoblade"));
                recipe.AddIngredient(_calamity.ItemType("HeavenlyGale"));
                recipe.AddIngredient(_calamity.ItemType("VividClarity"));
                recipe.AddIngredient(_calamity.ItemType("CosmicImmaterializer"));
                recipe.AddIngredient(_calamity.ItemType("Celestus"));

                //recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("VoidofExtinction"));
                //recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("PlagueHive"));
                /*
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("Earth"));
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("Megafleet"));
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("CrystylCrusher"));
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("RedSun"));*/
            }

            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();


        }


    }

}





