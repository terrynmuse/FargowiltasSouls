using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader.IO;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class BerserkerEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berserker Enchantment");
            Tooltip.SetDefault(
                @"''
Damage is increased by 15% at every 25% segment of life

Hitting an enemy inflicts berserked, while berserked you gain attack speed and damage
Your symphonic damage will empower all nearby allies with: Attack Speed II");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            BerserkerEffect(player);
        }
        
        private void BerserkerEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            //thoriumPlayer.orbital = true;
            //Vector2 meme = Vector2.UnitY;
            //thoriumPlayer.orbitalRotation3 = Utils.RotatedBy(((ThoriumPlayer)player.GetModPlayer(base.mod, "ThoriumPlayer")).orbitalRotation3, -0.075000002980232239, default(Vector2));
            //Texture2D texture9 = mod.GetTexture("Gores/Static");



            //making divers code less of a meme :scuseme:
            if (player.statLife > player.statLifeMax * 0.75)
            {
                player.meleeDamage += 0.15f;
                player.magicDamage += 0.15f;
                player.rangedDamage += 0.15f;
                player.thrownDamage += 0.15f;
                //thoriumPlayer.berserkStage = 1;
            }
            else if (player.statLife > player.statLifeMax * 0.5)
            {
                player.meleeDamage += 0.3f;
                player.magicDamage += 0.3f;
                player.rangedDamage += 0.3f;
                player.thrownDamage += 0.3f;
                //thoriumPlayer.berserkStage = 2;
            }
            else if (player.statLife > player.statLifeMax * 0.25)
            {
                player.meleeDamage += 0.45f;
                player.magicDamage += 0.45f;
                player.rangedDamage += 0.45f;
                player.thrownDamage += 0.45f;
                //thoriumPlayer.berserkStage = 3;
            }
            else
            {
                player.meleeDamage += 0.6f;
                player.magicDamage += 0.6f;
                player.rangedDamage += 0.6f;
                player.thrownDamage += 0.6f;
                //thoriumPlayer.berserkStage = 4;
            }
        }
        
        private readonly string[] items =
        {
            "BerserkerMask",
            "BerserkerBreastplate",
            "BerserkerGreaves",
            "TunePlayerAttackSpeed",
            "SurtrsSword",
            "BloodyHighClaws",
            "ThermogenicImpaler",
            "BerserkBreaker",
            "BerserkSoulStaff",
            "WyvernSlayer"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            //molten or magma
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
