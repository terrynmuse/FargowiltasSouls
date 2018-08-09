using Terraria;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class IronEnchant : ModItem
	{
        int internalTimer = 0;
        bool wasHoldingShield = false;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Iron Enchantment");
			Tooltip.SetDefault(
@"'Strike while the iron is hot'
Allows the player to dash into the enemy
Right Click to guard with your shield
You attract items from a much larger range and fall 5 times as quickly");
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
            player.dash = 2;
            
            if (player.inventory[player.selectedItem].type == ItemID.DD2SquireDemonSword)
            {
                internalTimer = 0;
                wasHoldingShield = false;
                return;
            }

            player.shieldRaised = player.selectedItem != 58 && player.controlUseTile && (!player.tileInteractionHappened && player.releaseUseItem) && (!player.controlUseItem && !player.mouseInterface && (!CaptureManager.Instance.Active && !Main.HoveringOverAnNPC)) && !Main.SmartInteractShowingGenuine && (player.hasRaisableShield && !player.mount.Active) && (player.itemAnimation == 0 || PlayerInput.Triggers.JustPressed.MouseRight);

            if (internalTimer > 0)
            {
                internalTimer++;
                player.shieldParryTimeLeft = internalTimer;
                if (player.shieldParryTimeLeft > 20)
                {
                    player.shieldParryTimeLeft = 0;
                    internalTimer = 0;
                }
            }

            if (player.shieldRaised)
            {
                modPlayer.IronGuard = true;

                for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
                {
                    if (player.shield == -1 && player.armor[i].shieldSlot != -1)
                    {
                        player.shield = player.armor[i].shieldSlot;
                    } 
                }

                if (!wasHoldingShield)
                {
                    wasHoldingShield = true;

                    if (player.shield_parry_cooldown == 0)
                    {
                        internalTimer = 1;
                    }

                    player.itemAnimation = 0;
                    player.itemTime = 0;
                    player.reuseDelay = 0;
                }
            }
            else
            {
                wasHoldingShield = false;
                player.shield_parry_cooldown = 15;
                player.shieldParryTimeLeft = 0;
                internalTimer = 0;

                if (player.attackCD < 20)
                {
                    player.attackCD = 20;
                }
            }

            //item attract
            modPlayer.IronEnchant = true;
            player.maxFallSpeed *= 5;
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronHelmet);
			recipe.AddIngredient(ItemID.IronChainmail);
			recipe.AddIngredient(ItemID.IronGreaves);
            recipe.AddIngredient(ItemID.EoCShield);
            recipe.AddIngredient(ItemID.IronBroadsword);
            recipe.AddIngredient(ItemID.IronAnvil);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}