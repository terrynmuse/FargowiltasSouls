using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class HellZone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hell Zone");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 30;//
            item.useTime = 5;//
            item.width = 54;
            item.height = 14;
            item.shoot = mod.ProjectileType("HellFlame");
            item.useAmmo = AmmoID.Gel;
            item.UseSound = SoundID.Item34;//
            item.damage = 240;//
            item.knockBack = 0.5f;
            item.shootSpeed = 10f; //
            item.noMelee = true;
            item.value = Item.sellPrice(0, 15, 0, 0); //
            item.rare = 10; //
            item.ranged = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.myPlayer, Main.rand.Next(6));
            return false;
        }

        public override bool ConsumeAmmo(Player player)
        {
            //
            return Main.rand.Next(4) != 0;
        }

        //make them hold it different
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-30, 4);
        }
    }
}