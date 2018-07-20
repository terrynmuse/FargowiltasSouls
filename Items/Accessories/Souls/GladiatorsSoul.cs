using Terraria;
using Terraria.ModLoader;
using static Terraria.ID.ItemID;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    // ReSharper disable once UnusedMember.Global
    public class GladiatorsSoul : ModItem
    {
        private readonly Mod _calamity = ModLoader.GetMod("CalamityMod");
        private Mod _thorium;
        private Mod _bluemagic;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berserker's Soul");
            Tooltip.SetDefault(
                "'None shall live to tell the tale' \n30% increased melee damage \n25% increased melee speed \n20% increased melee crit chance \nIncreased melee knockback \nMelee attacks inflict excessive bleeding \nSword swings have a chance to cut an enemy in two \nGrants the effects of the Yoyo Bag");

            if (Fargowiltas.Instance.CalamityLoaded)
            {
                Tooltip.SetDefault(
                    "'None shall live to tell the tale' \n30% increased melee damage \n25% increased melee speed \n20% increased melee crit chance \nIncreased melee knockback \nGrants the effects of the Yoyo Bag and Elemental Gauntlet");
            }
        }

        public override void SetDefaults()
        {
            ;
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.defense = 5;
            item.value = 750000;
            item.rare = -12;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).MeleeEffect = true;

            if (Soulcheck.GetValue("Melee Speed") == false)
            {
                player.meleeSpeed += .1f;
            }
            else
            {
                player.meleeSpeed += .25f;
            }

            player.meleeDamage += .3f;
            player.meleeCrit += 20;
            player.kbGlove = true;

            //yoyo bag
            player.counterWeight = 556 + Main.rand.Next(6);
            player.yoyoGlove = true;
            player.yoyoString = true;


            if (Fargowiltas.Instance.CalamityLoaded)
            {
                Gauntlet(player);
            }
        }

        private void Gauntlet(Player player)
        {
            player.GetModPlayer<CalamityMod.CalamityPlayer>(_calamity).eGauntlet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe melee2 = new ModRecipe(mod);

            melee2.AddIngredient(null, "BarbariansEssence");
            melee2.AddIngredient(YoyoBag);

            _bluemagic = ModLoader.GetMod("Bluemagic");
            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                _thorium = ModLoader.GetMod("ThoriumMod");
                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    if (Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //all 3
                        melee2.AddIngredient(_bluemagic.ItemType("FlamingCrystalGauntlet"));
                    }

                    melee2.AddIngredient(_calamity.ItemType("ElementalGauntlet"));
                    melee2.AddIngredient(IceSickle);
                    melee2.AddIngredient(DD2SquireBetsySword);
                    melee2.AddIngredient(Flairon);
                    melee2.AddIngredient(InfluxWaver);
                    melee2.AddIngredient(_thorium.ItemType("SickThrow"));
                    melee2.AddIngredient(_calamity.ItemType("SpatialLance"));
                    melee2.AddIngredient(StarWrath);
                    melee2.AddIngredient(_thorium.ItemType("TerrariansKnife"));

                    if (!Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //thorium and calamity
                        melee2.AddIngredient(_calamity.ItemType("MirrorBlade"));
                    }

                    melee2.AddIngredient(_calamity.ItemType("DevilsDevastation"));
                }

                if (!Fargowiltas.Instance.CalamityLoaded)
                {
                    if (Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //blue and thorium
                        melee2.AddIngredient(_bluemagic.ItemType("FlamingCrystalGauntlet"));
                    }

                    else
                    {
                        //just thorium
                        melee2.AddIngredient(FireGauntlet);
                    }

                    melee2.AddIngredient(IceSickle);
                    melee2.AddIngredient(_thorium.ItemType("PrimesFury"));
                    melee2.AddIngredient(TerraBlade);
                    melee2.AddIngredient(DD2SquireBetsySword);
                    melee2.AddIngredient(Flairon);
                    melee2.AddIngredient(TheHorsemansBlade);
                    melee2.AddIngredient(NorthPole);
                    melee2.AddIngredient(InfluxWaver);
                    melee2.AddIngredient(_thorium.ItemType("SickThrow"));
                    melee2.AddIngredient(StarWrath);
                    melee2.AddIngredient(_thorium.ItemType("TerrariansKnife"));
                }
            }

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    if (Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //calamity and blue
                        melee2.AddIngredient(_bluemagic.ItemType("FlamingCrystalGauntlet"));
                    }

                    melee2.AddIngredient(_calamity.ItemType("ElementalGauntlet"));
                    melee2.AddIngredient(IceSickle);

                    if (!Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //just calamity
                        melee2.AddIngredient(_calamity.ItemType("TrueTyrantYharimsUltisword"));
                    }

                    melee2.AddIngredient(DD2SquireBetsySword);
                    melee2.AddIngredient(Flairon);
                    melee2.AddIngredient(InfluxWaver);
                    melee2.AddIngredient(_calamity.ItemType("SpatialLance"));
                    melee2.AddIngredient(StarWrath);
                    melee2.AddIngredient(_calamity.ItemType("MirrorBlade"));
                    melee2.AddIngredient(_calamity.ItemType("NeptunesBounty"));
                    melee2.AddIngredient(_calamity.ItemType("DevilsDevastation"));
                }
                else
                {
                    melee2.AddIngredient
                    (
                        Fargowiltas.Instance.BlueMagicLoaded
                            ? _bluemagic.ItemType("FlamingCrystalGauntlet")
                            : FireGauntlet
                    );

                    melee2.AddIngredient(IceSickle);
                    melee2.AddIngredient(Cutlass);
                    melee2.AddIngredient(PsychoKnife);
                    melee2.AddIngredient(Kraken);
                    melee2.AddIngredient(TerraBlade);
                    melee2.AddIngredient(DD2SquireBetsySword);
                    melee2.AddIngredient(Flairon);
                    melee2.AddIngredient(TheHorsemansBlade);
                    melee2.AddIngredient(NorthPole);
                    melee2.AddIngredient(InfluxWaver);
                    melee2.AddIngredient(StarWrath);
                }
            }

            melee2.SetResult(this);
            melee2.AddRecipe();
        }
    }
}