using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;

namespace FargowiltasSouls.Items.Accessories.Enchantments.AA
{
    public class TrueCopperEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMody");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Copper Enchantment");

            string tooltip = 
@"'Behold'
Attacks have a chance to shock enemies with lightning
If an enemy is wet, the chance and damage is increased
Attacks that cause Wet cannot proc the lightning
Lightning scales with highest damage
Cannot be equipped with the copper enchantment";
            string tooltip_ch =
@"'注视'
攻击有概率用闪电打击敌人
如果敌人处于潮湿状态,增加概率和伤害
造成潮湿的攻击不能触发闪电
闪电伤害与你的最高伤害有关";

            if(thorium != null)
            {
                tooltip += "\nEffects of the Copper Buckler";
                tooltip_ch += "\n拥有铜制圆盾的效果";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "铜魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).TrueCopper = true;

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            //copper shield
            timer++;
            if (timer >= 30)
            {
                int num = 10;
                if (thoriumPlayer.shieldHealth <= num)
                {
                    thoriumPlayer.shieldHealthTimerStop = true;
                }
                if (thoriumPlayer.shieldHealth < num)
                {
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 255, 255), 1, false, true);
                    thoriumPlayer.shieldHealth++;
                    player.statLife++;
                }
                timer = 0;
            }
        }

        public override void AddRecipes()
        {
            /*ModRecipe recipe = new ModRecipe(mod);
            if (Fargowiltas.Instance.AALoaded)
            {
                recipe.AddIngredient(AA, "TrueCopperHelm");
                recipe.AddIngredient(AA, "TrueCopperPlate");
                recipe.AddIngredient(AA, "TrueCopperLeggings");
                recipe.AddIngredient(AA, "TrueCopperShortsword");
                recipe.AddIngredient(mod.ItemType<CopperEnchant>());
            }

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();*/
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/Accessories/Enchantments/CopperEnchant");
            Vector2 pos = new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                );
            spriteBatch.Draw(texture, pos, new Rectangle(0, 0, texture.Width, texture.Height), lightColor, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(Main.itemTexture[item.type], pos, new Rectangle(0, 0, texture.Width, texture.Height), Main.DiscoColor, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = mod.GetTexture("Items/Accessories/Enchantments/CopperEnchant");
            Texture2D texture2 = Main.itemTexture[item.type];
            spriteBatch.Draw(texture, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture2, position, null, Main.DiscoColor, 0, origin, scale, SpriteEffects.None, 0f);

            return false;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == mod.ItemType<CopperEnchant>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
