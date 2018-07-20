using Terraria;
using Terraria.ModLoader;
using static Terraria.ID.ItemID;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class ArchWizardsSoul : ModItem
    {
        private readonly Mod _calamity = ModLoader.GetMod("CalamityMod");
        private readonly Mod _thorium = ModLoader.GetMod("ThoriumMod");
        private Mod _bluemagic = ModLoader.GetMod("Bluemagic");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arch Wizard's Soul");
            Tooltip.SetDefault("'Arcane to the core' \n40% increased magic damage \n20% increased magic critical chance \nIncreases your maximum mana by 100 \nReduces mana usage by 33% \nIncreased pickup range for mana stars \nAutomatically use mana potions when needed");
            if (Fargowiltas.Instance.CalamityLoaded)
            {
                Tooltip.SetDefault("'Arcane to the core' \n40% increased magic damage \n20% increased magic critical chance \nIncreases your maximum mana by 100 \nReduces mana usage by 33% \nIncreased pickup range for mana stars \nAutomatically use mana potions when needed \nGrants the effects of the Ethereal Talisman");
            }
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 750000;
            item.rare = -12;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            player.manaCost -= .33f;
            player.magicDamage += .4f;
            player.magicCrit += 20;
            player.statManaMax2 += 100;
            player.magicCuffs = true;
            player.manaMagnet = true;
            player.manaFlower = true;

            if (Fargowiltas.Instance.BlueMagicLoaded)
            {
                blueMagnet(player);
            }
            if (Fargowiltas.Instance.CalamityLoaded)
            {
                Talisman(player);
            }
        }

        public void Talisman(Player player)
        {
            player.GetModPlayer<CalamityMod.CalamityPlayer>(_calamity).eTalisman = true;
        }

        public void blueMagnet(Player player)
        {
            player.GetModPlayer<Bluemagic.BluemagicPlayer>(_bluemagic).manaMagnet2 = true;
        }

        public override void AddRecipes()
        {
            ModRecipe magic2 = new ModRecipe(mod);

            magic2.AddIngredient(null, "ApprenticesEssence");


            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    magic2.AddIngredient(Fargowiltas.Instance.BlueMagicLoaded
                        ? _bluemagic.ItemType("CelestialSeal")
                        : CelestialCuffs);

                    magic2.AddIngredient(_calamity.ItemType("EtherealTalisman"));
                    magic2.AddIngredient(_thorium.ItemType("SpectrelBlade"));
                    magic2.AddIngredient(_thorium.ItemType("TerraStaff"));
                    magic2.AddIngredient(_calamity.ItemType("CosmicRainbow"));
                    magic2.AddIngredient(_thorium.ItemType("TerrariumSageStaff"));
                    magic2.AddIngredient(_calamity.ItemType("Effervescence"));
                    magic2.AddIngredient(_calamity.ItemType("AlphaRay"));
                    magic2.AddIngredient(LastPrism);
                    magic2.AddIngredient(_thorium.ItemType("AlmanacofDespair"));
                    magic2.AddIngredient(_calamity.ItemType("T1000"));
                }

                if (!Fargowiltas.Instance.CalamityLoaded)
                {
                    magic2.AddIngredient(Fargowiltas.Instance.BlueMagicLoaded
                        ? _bluemagic.ItemType("CelestialSeal")
                        : CelestialCuffs);

                    magic2.AddIngredient(_thorium.ItemType("PrismaticSpray"));
                    magic2.AddIngredient(RainbowGun);
                    magic2.AddIngredient(_thorium.ItemType("SpectrelBlade"));
                    magic2.AddIngredient(_thorium.ItemType("TerraStaff"));
                    magic2.AddIngredient(_thorium.ItemType("OldGodGrasp"));
                    magic2.AddIngredient(_thorium.ItemType("NuclearFury"));
                    magic2.AddIngredient(ApprenticeStaffT3);
                    magic2.AddIngredient(BatScepter);
                    magic2.AddIngredient(BlizzardStaff);
                    magic2.AddIngredient(_thorium.ItemType("TerrariumSageStaff"));
                    magic2.AddIngredient(LastPrism);
                    magic2.AddIngredient(_thorium.ItemType("AlmanacofDespair"));
                }
            }
            else
            {

                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    magic2.AddIngredient(Fargowiltas.Instance.BlueMagicLoaded
                        ? _bluemagic.ItemType("CelestialSeal")
                        : CelestialCuffs);

                    magic2.AddIngredient(_calamity.ItemType("EtherealTalisman"));
                    magic2.AddIngredient(_calamity.ItemType("InfernalRift"));
                    magic2.AddIngredient(_calamity.ItemType("CosmicRainbow"));
                    magic2.AddIngredient(ApprenticeStaffT3);
                    magic2.AddIngredient(BatScepter);
                    magic2.AddIngredient(BlizzardStaff);
                    magic2.AddIngredient(_calamity.ItemType("Effervescence"));
                    magic2.AddIngredient(_calamity.ItemType("AlphaRay"));
                    magic2.AddIngredient(LastPrism);
                    magic2.AddIngredient(_calamity.ItemType("T1000"));
                }
                else
                {
                    magic2.AddIngredient(Fargowiltas.Instance.BlueMagicLoaded
                        ? _bluemagic.ItemType("CelestialSeal")
                        : CelestialCuffs);

                    magic2.AddIngredient(MeteorStaff);
                    magic2.AddIngredient(CrystalStorm);
                    magic2.AddIngredient(MagicalHarp);
                    magic2.AddIngredient(NettleBurst);
                    magic2.AddIngredient(RainbowGun);
                    magic2.AddIngredient(InfernoFork);
                    magic2.AddIngredient(ApprenticeStaffT3);
                    magic2.AddIngredient(RazorbladeTyphoon);
                    magic2.AddIngredient(BatScepter);
                    magic2.AddIngredient(BlizzardStaff);
                    magic2.AddIngredient(LaserMachinegun);
                    magic2.AddIngredient(LastPrism);
                }
            }

            //magic2.AddTile(null, "CrucibleCosmosSheet");
            magic2.SetResult(this);
            magic2.AddRecipe();
        }

    }
}