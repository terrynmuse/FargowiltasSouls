using FargowiltasSouls.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class FossilEnchant : ModItem
	{
        int boneCD = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fossil Enchantment");
			Tooltip.SetDefault(
@"'Beyond a forgotten age'
If you reach zero HP you cheat death, returning with 20 HP
For a few seconds after reviving, you are immune to all damage and spawn bones everywhere
Bones scale with throwing damage
Summons a pet Baby Dino");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;			
			ItemID.Sets.ItemNoGravity[item.type] = true;
			item.rare = 2; 
			item.value = 40000; 
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //revive
			modPlayer.FossilEnchant = true;
            
            //bone zone
            if(modPlayer.FossilBones)
            {
                if (boneCD == 0)
                {
                    for (int i = 0; i < Main.rand.Next(4, 12); i++)
                    {
                        float randX, randY;

                        do
                        {
                            randX = Main.rand.Next(-10, 10);
                        } while (randX <= 4f && randX >= -4f);

                        do
                        {
                            randY = Main.rand.Next(-10, 10);
                        } while (randY <= 4f && randY >= -4f);

                        Projectile p =Projectile.NewProjectileDirect(player.Center, new Vector2(randX, randY), ProjectileID.BoneGloveProj, (int)(10 * player.thrownDamage), 2, Main.myPlayer);
                        p.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                    }

                    Projectile p2 = Projectile.NewProjectileDirect(player.Center, Vector2.Zero, ProjectileID.Bone, (int)(15 * player.thrownDamage), 0f, player.whoAmI);
                    p2.GetGlobalProjectile<FargoGlobalProjectile>().Rotate = true;
                    p2.GetGlobalProjectile<FargoGlobalProjectile>().RotateDist = Main.rand.Next(32, 128);
                    p2.GetGlobalProjectile<FargoGlobalProjectile>().RotateDir = Main.rand.Next(2);
                    p2.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                    p2.noDropItem = true;

                    boneCD = 20;
                }

                boneCD--;

                if(!player.immune)
                {
                    modPlayer.FossilBones = false;
                }
            }

            modPlayer.AddPet("Baby Dino Pet", BuffID.BabyDinosaur, ProjectileID.BabyDino);
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FossilHelm);
            recipe.AddIngredient(ItemID.FossilShirt);
			recipe.AddIngredient(ItemID.FossilPants);
			recipe.AddIngredient(ItemID.AmberStaff);
			recipe.AddIngredient(ItemID.AntlionClaw);
			recipe.AddIngredient(ItemID.AmberMosquito);
			recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}