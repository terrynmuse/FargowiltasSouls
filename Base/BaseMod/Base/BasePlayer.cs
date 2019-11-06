using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls
{
    public class BasePlayer
    {
        //------------------------------------------------------//
        //------------------BASE PLAYER CLASS-------------------//
        //------------------------------------------------------//
        // Contains methods relating to players.                //
        //------------------------------------------------------//
        //  Author(s): Grox the Great                           //
        //------------------------------------------------------//

		//NOTE: DO NOT CALL IN ANY ConsumeAmmo HOOK!!! Will cause an infinite loop!
		public static bool ConsumeAmmo(Player player, Item item, Item ammo)
		{
			bool consume = true;
			if (player.magicQuiver && ammo.ammo == AmmoID.Arrow && Main.rand.Next(5) == 0) consume = false;
			if (player.ammoBox && Main.rand.Next(5) == 0) consume = false;
			if (player.ammoPotion && Main.rand.Next(5) == 0) consume = false;	
			if (player.ammoCost80 && Main.rand.Next(5) == 0) consume = false;
			if (player.ammoCost75 && Main.rand.Next(4) == 0) consume = false;	
			if (!PlayerHooks.ConsumeAmmo(player, item, ammo)) consume = false;
			if (!ItemLoader.ConsumeAmmo(item, ammo, player)) consume = false;
			return consume;
		}

        public static void ReduceSlot(Player player, int slot, int amount)
        {
            player.inventory[slot].stack -= amount;
            if (player.inventory[slot].stack <= 0)
            {
                player.inventory[slot] = new Item();
            }   
        }



        /*
         * A manual way for the player to use the current held item they have.
         * (NOTE: this only works if it on the player in question's client, not on the server!)
         */
        public static void UseHeldItem(Player player)
        {
            if(Main.myPlayer == player.whoAmI && player.itemAnimation == 0){ MPlayer.useItem = true; }
        }

        /*
         * Returns true if it successfully reduces mana. If there is not enough mana to reduce it by the amount given it returns false.
         * autoRefill : If true, checks if the player has a mana flower and automatically boosts mana if it's needed.
         */
        public static bool ReduceMana(Player player, int amount, bool autoRefill = true)
        {
            if(autoRefill && player.manaFlower && player.statMana < (int)(amount * player.manaCost))
            {
               player.QuickMana();
            }
            if(player.statMana >= (int)(amount * player.manaCost))
            {
                //TODO: FIX
                //player.manaRegenDelay = (int)player.maxRegenDelay;
                player.statMana -= (int)(amount * player.manaCost);
                if(player.statMana < 0){ player.statMana = 0; }
                return true;
            }
            return false;
        }

        public static bool HasHelmet(Player player, int itemType, bool vanity = true){ return HasArmor(player, itemType, 0, vanity); }
        public static bool HasChestplate(Player player, int itemType, bool vanity = true) { return HasArmor(player, itemType, 1, vanity); }
        public static bool HasLeggings(Player player, int itemType, bool vanity = true) { return HasArmor(player, itemType, 2, vanity); }
        
        /*
         * Returns true if the player is wearing the given armor
         * armorType : 0 == helmet, 1 == chestplate, 2 == leggings.
         * vanity : If true, include vanity slots.
         */
        public static bool HasArmor(Player player, int itemType, int armorType, bool vanity = true)
        {
            if (vanity)
            {
                if (armorType == 0)
                    return (player.armor[10] != null && player.armor[10].type == itemType) || (player.armor[0] != null && player.armor[0].type == itemType);
                if (armorType == 1)
                    return (player.armor[11] != null && player.armor[11].type == itemType) || (player.armor[1] != null && player.armor[1].type == itemType);
                if (armorType == 2)
                    return (player.armor[12] != null && player.armor[12].type == itemType) || (player.armor[2] != null && player.armor[2].type == itemType);
            }else
            {
                if (armorType == 0)
                    return (player.armor[0] != null && player.armor[0].type == itemType);
                if (armorType == 1)
                    return (player.armor[1] != null && player.armor[1].type == itemType);
                if (armorType == 2)
                    return (player.armor[2] != null && player.armor[2].type == itemType);
            }
            return false;
        }


        /**
         * Returns the total monetary value (in copper coins) the player has.
         * includeInventory : True to include inventory slots, false to only include coin slots.
         */
        public static int GetMoneySum(Player player, bool includeInventory = false)
        {
			int totalSum = 0;
            for (int m = (includeInventory ? 0 : 50); m < 54; m++)
            {
                Item item = player.inventory[m];
                if(item != null) 
                {
                    if (item.type == 71) { totalSum += item.stack; }
                    else
                        if (item.type == 72) { totalSum += item.stack * 100; }
                        else
                            if (item.type == 73) { totalSum += item.stack * 10000; }
                            else
                                if (item.type == 74) { totalSum += item.stack * 1000000; }
                }
            }
            return totalSum;
        }


        public static int GetItemstackSum(Player player, int type, bool typeIsAmmo = false, bool includeAmmo = false, bool includeCoins = false)
        {
            return GetItemstackSum(player, new int[] { type }, typeIsAmmo, includeAmmo, includeCoins);
        }

        /*
         * Returns the total itemstack sum of a specific item in the player's inventory.
         * 
         * typeIsAmmo : If true, types are considered ammo types. Else, types are considered item types.
         */
        public static int GetItemstackSum(Player player, int[] types, bool typeIsAmmo = false, bool includeAmmo = false, bool includeCoins = false)
        {
            int stackCount = 0;
			if (includeCoins)
			{
				for (int m = 50; m < 54; m++)
				{
					Item item = player.inventory[m];
					if (item != null && (typeIsAmmo ? BaseUtility.InArray(types, item.ammo) : BaseUtility.InArray(types, item.type))) { stackCount += item.stack; }
				}
			}
			if (includeAmmo)
			{
				for (int m = 54; m < 58; m++)
				{
					Item item = player.inventory[m];
					if (item != null && (typeIsAmmo ? BaseUtility.InArray(types, item.ammo) : BaseUtility.InArray(types, item.type))) { stackCount += item.stack; }
				}
			}
            for (int m = 0; m < 50; m++)
            {
                Item item = player.inventory[m];
                if (item != null && (typeIsAmmo ? BaseUtility.InArray(types, item.ammo) : BaseUtility.InArray(types, item.type))) { stackCount += item.stack; }
            }
            return stackCount;
        }

        public static bool HasItem(Player player, int[] types, int[] counts = default(int[]), bool includeAmmo = false, bool includeCoins = false)
        {
            int dummyIndex = 0;
            bool hasItem = HasItem(player, types, ref dummyIndex, counts, includeAmmo, includeCoins);
            return hasItem;
        }

        /**
         * Returns true if the given player has any of the given item types in thier inventory.
         * index : Is set to the index of the item found. If it isn't found, it is set to -1.
         * counts : the minimum stack per item needed for HasItem to return true.
         * includeAmmo : true if you wish to include the ammo slots.
         * includeCoins : true if you wish to include the coin slots.
         */
        public static bool HasItem(Player player, int[] types, ref int index, int[] counts = default(int[]), bool includeAmmo = false, bool includeCoins = false)
        {
			if(types == null || types.Length == 0) return false; //no types to check!			
            if(counts == null || counts.Length == 0){ counts = BaseUtility.FillArray(new int[types.Length], 1); }
            int countIndex = -1;
			if (includeCoins)
			{
				for (int m = 50; m < 54; m++)
				{
					Item item = player.inventory[m];
					if (item != null && BaseUtility.InArray(types, item.type, ref countIndex) && item.stack >= counts[countIndex]) { index = m; return true; }
				}
			}
			if (includeAmmo)
			{
				for (int m = 54; m < 58; m++)
				{
					Item item = player.inventory[m];
					if (item != null && BaseUtility.InArray(types, item.type, ref countIndex) && item.stack >= counts[countIndex]) { index = m; return true; }
				}
			}
            for (int m = 0; m < 50; m++)
            {
                Item item = player.inventory[m];
                if (item != null && BaseUtility.InArray(types, item.type, ref countIndex) && item.stack >= counts[countIndex]) { index = m; return true; }
            }
            return false;
        }

		public static bool HasAllItems(Player player, int[] types, ref int[] indicies, int[] counts = default(int[]), bool includeAmmo = false, bool includeCoins = false)
		{
			if(types == null || types.Length == 0) return false; //no types to check!
            if(counts == null || counts.Length == 0){ counts = BaseUtility.FillArray(new int[types.Length], 1); }			
			int[] indexArray = new int[types.Length];
			bool[] foundItem = new bool[types.Length];
			if (includeCoins)
			{
				for (int m = 50; m < 54; m++)
				{
					for(int m2 = 0; m2 < types.Length; m2++)
					{
						if(foundItem[m2]) continue;
						Item item = player.inventory[m];
						if (item != null && item.type == types[m2] && item.stack >= counts[m2]) { foundItem[m2] = true; indexArray[m2] = m; }
					}
				}
			}
			if (includeAmmo)
			{
				for (int m = 54; m < 58; m++)
				{
					for(int m2 = 0; m2 < types.Length; m2++)
					{
						if(foundItem[m2]) continue;
						Item item = player.inventory[m];
						if (item != null && item.type == types[m2] && item.stack >= counts[m2]) { foundItem[m2] = true; indexArray[m2] = m; }
					}
				}
			}
			for(int m = 0; m < 50; m++)
			{
				for(int m2 = 0; m2 < types.Length; m2++)
				{
					if(foundItem[m2]) continue;
					Item item = player.inventory[m];
					if (item != null && item.type == types[m2] && item.stack >= counts[m2]) { foundItem[m2] = true; indexArray[m2] = m; }
				}
			}
			foreach(bool f in foundItem) if(!f) return false;
			return true;
		}

        public static bool HasItem(Player player, int type, int count = 1, bool includeAmmo = false, bool includeCoins = false)
        {
            int dummyIndex = 0;
            bool hasItem = HasItem(player, type, ref dummyIndex, count, includeAmmo, includeCoins);
            return hasItem;
        }

        /**
         * Returns true if the given player has the given item type in thier inventory.
         * 
         * index : Is set to the index of the item found. If it isn't found, it is set to -1.
         * count : the minimum stack needed for HasItem to return true.
         * includeAmmo : true if you wish to include the ammo slots.
         * includeCoins : true if you wish to include the coin slots.
         */
        public static bool HasItem(Player player, int type, ref int index, int count = 1, bool includeAmmo = false, bool includeCoins = false)
        {
			if (includeCoins)
			{
				for (int m = 50; m < 54; m++)
				{
					Item item = player.inventory[m];
					if (item != null && item.type == type && item.stack >= count) { index = m; return true; }
				}
			}
			if (includeAmmo)
			{
				for (int m = 54; m < 58; m++)
				{
					Item item = player.inventory[m];
					if (item != null && item.type == type && item.stack >= count) { index = m; return true; }
				}
			}
            for(int m = 0; m < 50; m++)
            {
                Item item = player.inventory[m];
                if(item != null && item.type == type && item.stack >= count){ index = m;  return true; }
            }
            index = -1;
            return false;
        }


        /**
         * Returns true if the given player has the given item ammo type in thier inventory.
         * 
         * index : Is set to the index of the item found. If it isn't found, it is set to -1.
         * count : the minimum stack needed for HasItem to return true.
         * includeAmmo : true if you wish to include the ammo slots.
         * includeCoins : true if you wish to include the coin slots.
		 * ingoreConsumable : true if you wish to ignore consumable checks (this is used for infinite ammo items like musket pouch)
         */
        public static bool HasAmmo(Player player, int ammoType, ref int index, int count = 1, bool includeAmmo = false, bool includeCoins = false, bool ignoreConsumable = false)
        {
			if (includeCoins)
			{
				for (int m = 50; m < 54; m++)
				{
					Item item = player.inventory[m];
					if (item != null && item.ammo == ammoType && ((!ignoreConsumable && !item.consumable) || item.stack >= count)) { index = m; return true; }
				}
			}
			if (includeAmmo)
			{
				for (int m = 54; m < 58; m++)
				{
					Item item = player.inventory[m];
					if (item != null && item.ammo == ammoType && ((!ignoreConsumable && !item.consumable) || item.stack >= count)) { index = m; return true; }
				}
			}
            for (int m = 0; m < 50; m++)
            {
                Item item = player.inventory[m];
				if (item != null && item.ammo == ammoType && ((!ignoreConsumable && !item.consumable) || item.stack >= count)) { index = m; return true; }
            }
            index = -1;
            return false;
        }

		




        public static bool IsVanitySlot(int slot, bool acc = true) { return (acc ? slot >= 13 && slot <= 18 : slot >= 10 && slot <= 12); }


        
    }
}


