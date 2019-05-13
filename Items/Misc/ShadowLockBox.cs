using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Misc
{
	public class ShadowLockBox : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Lock Box");
            Tooltip.SetDefault("Right click to open\nRequires a Shadow Key");
		}

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.maxStack = 99;
            item.rare = 3;
            item.consumable = true;
            item.UseSound = SoundID.Item3;
            item.value = Item.sellPrice(0, 0, 50, 0);
        }

        public override void RightClick(Player player)
        {
            bool hasKey = false;
            foreach (Item i in player.inventory)
            {
                if (i.stack > 0 && i.type == ItemID.ShadowKey)
                {
                    hasKey = true;
                    break;
                }
            }
            if (hasKey)
            {
                //consume lock box
                if (--item.stack <= 0)
                    item.SetDefaults();
                Main.PlaySound(21, player.Center);
                //drop rare items
                switch(Main.rand.Next(5))
                {
                    case 0: Item.NewItem(player.Center, ItemID.DarkLance); break;
                    case 1: Item.NewItem(player.Center, ItemID.Flamelash); break;
                    case 2: Item.NewItem(player.Center, ItemID.FlowerofFire); break;
                    case 3: Item.NewItem(player.Center, ItemID.Sunfury); break;
                    case 4: Item.NewItem(player.Center, ItemID.HellwingBow); break;
                }
                //drop paintings
                for (int i = 0; i < 2; i++)
                {
                    switch (Main.rand.Next(12))
                    {
                        case 0: Item.NewItem(player.Center, ItemID.DarkSoulReaper); break;
                        case 1: Item.NewItem(player.Center, ItemID.Darkness); break;
                        case 2: Item.NewItem(player.Center, ItemID.DemonsEye); break;
                        case 3: Item.NewItem(player.Center, ItemID.FlowingMagma); break;
                        case 4: Item.NewItem(player.Center, ItemID.HandEarth); break;
                        case 5: Item.NewItem(player.Center, ItemID.ImpFace); break;
                        case 6: Item.NewItem(player.Center, ItemID.LakeofFire); break;
                        case 7: Item.NewItem(player.Center, ItemID.LivingGore); break;
                        case 8: Item.NewItem(player.Center, ItemID.OminousPresence); break;
                        case 9: Item.NewItem(player.Center, ItemID.ShiningMoon); break;
                        case 10: Item.NewItem(player.Center, ItemID.Skelehead); break;
                        case 11: Item.NewItem(player.Center, ItemID.TrappedGhost); break;
                    }
                }
                //drop shadow chest items
                if (Main.rand.Next(3) == 0)
                    Item.NewItem(player.Center, ItemID.Dynamite);
                if (Main.rand.Next(2) == 0)
                    Item.NewItem(player.Center, Main.rand.Next(2) == 0 ? ItemID.MeteoriteBar : ItemID.GoldBar, Main.rand.Next(15, 30));
                if (Main.rand.Next(2) == 0)
                    Item.NewItem(player.Center, Main.rand.Next(2) == 0 ? ItemID.HellfireArrow : ItemID.SilverBullet, Main.rand.Next(50, 75));
                if (Main.rand.Next(2) == 0)
                    Item.NewItem(player.Center, Main.rand.Next(2) == 0 ? ItemID.LesserRestorationPotion : ItemID.RestorationPotion, Main.rand.Next(15, 27));
                if (Main.rand.Next(4) != 0)
                {
                    switch(Main.rand.Next(8))
                    {
                        case 0: Item.NewItem(player.Center, ItemID.SpelunkerPotion, Main.rand.Next(1, 3)); break;
                        case 1: Item.NewItem(player.Center, ItemID.FeatherfallPotion, Main.rand.Next(1, 3)); break;
                        case 2: Item.NewItem(player.Center, ItemID.ManaRegenerationPotion, Main.rand.Next(1, 3)); break;
                        case 3: Item.NewItem(player.Center, ItemID.ObsidianSkinPotion, Main.rand.Next(1, 3)); break;
                        case 4: Item.NewItem(player.Center, ItemID.MagicPowerPotion, Main.rand.Next(1, 3)); break;
                        case 5: Item.NewItem(player.Center, ItemID.InvisibilityPotion, Main.rand.Next(1, 3)); break;
                        case 6: Item.NewItem(player.Center, ItemID.HunterPotion, Main.rand.Next(1, 3)); break;
                        case 7: Item.NewItem(player.Center, ItemID.HeartreachPotion, Main.rand.Next(1, 3)); break;
                    }
                }
                if (Main.rand.Next(3) != 0)
                {
                    switch (Main.rand.Next(8))
                    {
                        case 0: Item.NewItem(player.Center, ItemID.GravitationPotion, Main.rand.Next(1, 3)); break;
                        case 1: Item.NewItem(player.Center, ItemID.ThornsPotion, Main.rand.Next(1, 3)); break;
                        case 2: Item.NewItem(player.Center, ItemID.WaterWalkingPotion, Main.rand.Next(1, 3)); break;
                        case 3: Item.NewItem(player.Center, ItemID.ObsidianSkinPotion, Main.rand.Next(1, 3)); break;
                        case 4: Item.NewItem(player.Center, ItemID.BattlePotion, Main.rand.Next(1, 3)); break;
                        case 5: Item.NewItem(player.Center, ItemID.TeleportationPotion, Main.rand.Next(1, 3)); break;
                        case 6: Item.NewItem(player.Center, ItemID.InfernoPotion, Main.rand.Next(1, 3)); break;
                        case 7: Item.NewItem(player.Center, ItemID.LifeforcePotion, Main.rand.Next(1, 3)); break;
                    }
                }
                if (Main.rand.Next(3) == 0)
                    Item.NewItem(player.Center, ItemID.RecallPotion, Main.rand.Next(1, 3));
                if (Main.rand.Next(2) == 0)
                    Item.NewItem(player.Center, Main.rand.Next(2) == 0 ? ItemID.Torch : ItemID.Glowstick, Main.rand.Next(15, 30));
                if (Main.rand.Next(2) == 0)
                    Item.NewItem(player.Center, ItemID.GoldCoin, Main.rand.Next(2, 5));
            }
        }
    }
}
