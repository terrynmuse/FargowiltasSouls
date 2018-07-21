using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class SharpshootersSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharpshooter's Soul");
            Tooltip.SetDefault("'Ready, aim, fire' \n40% increased range damage \n33% chance not to consume ammo \n25% increased ranged critical chance \nIncreased armor penetration by 10 \nRanged attacks inflict shadowflame");
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

        //tooltip color?
        /*public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color(0, 255, 0);
				}
			}
		}*/

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).RangedEffect = true;

            player.rangedDamage += .4f;
            player.rangedCrit += 25;

            player.armorPenetration = 10;
        }

        public override void AddRecipes()
        {
            ModRecipe range2 = new ModRecipe(mod);

            range2.AddIngredient(null, "SnipersEssence");
            range2.AddIngredient(ItemID.MagicQuiver);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    if (Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //all 3
                        range2.AddIngredient(ModLoader.GetMod("Bluemagic").ItemType("CrystalDestroyerScope"));
                    }

                    range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("DaedalusEmblem"));
                    range2.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("DragonTalonNecklace"));
                    range2.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("TerraBow"));
                    range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("BarracudaGun"));

                    if (!Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //thorium and calamity
                        range2.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("ShadowFlareBow"));
                    }

                    range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("Scorpion"));
                    range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("ElementalBlaster"));
                    range2.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("CelestialBow"));
                    range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("SDFMG"));
                }

                if (!Fargowiltas.Instance.CalamityLoaded)
                {
                    range2.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("DragonTalonNecklace"));

                    if (Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //blue and thorium
                        range2.AddIngredient(ModLoader.GetMod("Bluemagic").ItemType("CrystalDestroyerScope"));
                    }

                    range2.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("DestroyersRage"));
                    range2.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("TerraBow"));
                    range2.AddIngredient(ItemID.PiranhaGun);
                    range2.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("ShadowFlareBow"));
                    range2.AddIngredient(ItemID.CandyCornRifle);
                    range2.AddIngredient(ItemID.SnowmanCannon);
                    range2.AddIngredient(ItemID.Xenopopper);
                    range2.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("TerrariumPulseRifle"));

                    if (!Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //just thorium
                        range2.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("NovaRifle"));
                    }

                    range2.AddIngredient(ItemID.SDMG);
                    range2.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("CelestialBow"));
                }
            }

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    if (Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //calamity and blue
                        range2.AddIngredient(ModLoader.GetMod("Bluemagic").ItemType("CrystalDestroyerScope"));
                    }

                    range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("DaedalusEmblem"));
                    range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("MagnaStriker"));
                    range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("BarracudaGun"));

                    if (!Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //just calamity
                        range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("PlanetaryAnnihilation"));
                    }

                    range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("OnyxChainBlaster"));
                    range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("Shredder"));
                    range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("Scorpion"));
                    range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("ElementalBlaster"));
                    range2.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("SDFMG"));
                }

                if (!Fargowiltas.Instance.CalamityLoaded)
                {
                    if (Fargowiltas.Instance.BlueMagicLoaded)
                    {
                        //just blue
                        range2.AddIngredient(ModLoader.GetMod("Bluemagic").ItemType("CrystalDestroyerScope"));
                    }

                    else
                    {
                        //no others
                        range2.AddIngredient(ItemID.SniperScope);
                    }


                    range2.AddIngredient(ItemID.Marrow);
                    range2.AddIngredient(ItemID.Megashark);
                    range2.AddIngredient(ItemID.NailGun);
                    range2.AddIngredient(ItemID.VenusMagnum);
                    range2.AddIngredient(ItemID.SniperRifle);
                    range2.AddIngredient(ItemID.PiranhaGun);
                    range2.AddIngredient(ItemID.Stynger);
                    range2.AddIngredient(ItemID.CandyCornRifle);
                    range2.AddIngredient(ItemID.SnowmanCannon);
                    range2.AddIngredient(ItemID.Xenopopper);
                    range2.AddIngredient(ItemID.SDMG);
                }
            }

            //range2.AddTile(null, "CrucibleCosmosSheet");
            range2.SetResult(this);
            range2.AddRecipe();
        }
    }
}