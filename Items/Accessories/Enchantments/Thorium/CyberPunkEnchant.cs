using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class CyberPunkEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cyber Punk Enchantment");
            Tooltip.SetDefault(
@"'Techno rave!'
Pressing the 'Special Ability' key will cycle you through four states
Effects of Auto Tuner and Red Music Player");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 6;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            if (Soulcheck.GetValue("Cyber Punk States"))
            {
                //cyber set bonus, good lord
                thoriumPlayer.cyberHeadAllowed = false;
                thoriumPlayer.cyberBodyAllowed = false;
                thoriumPlayer.cyberLegsAllowed = false;
                thoriumPlayer.cyberBard = true;
                for (int i = 0; i < 255; i++)
                {
                    Player player2 = Main.player[i];
                    if (player2.active && Vector2.Distance(player2.Center, player.Center) < 400f)
                    {
                        if (thoriumPlayer.cyberBardValue == 0)
                        {
                            if (thoriumPlayer.empowerDamage < 2)
                            {
                                CombatText.NewText(new Rectangle((int)player2.position.X, (int)player2.position.Y - 10, player2.width, player2.height), new Color(255, 0, 0), "+8% damage", false, false);
                                thoriumPlayer.empowerDamage = 2;
                            }
                            if (thoriumPlayer.empowerCriticalStrike < 2)
                            {
                                CombatText.NewText(new Rectangle((int)player2.position.X, (int)player2.position.Y - 10, player2.width, player2.height), new Color(255, 215, 75), "+8% critical strike chance", false, false);
                                thoriumPlayer.empowerCriticalStrike = 2;
                            }
                            if (thoriumPlayer.empowerDamage == 2)
                            {
                                thoriumPlayer.empowerTimerDamage = 60;
                            }
                            if (thoriumPlayer.empowerCriticalStrike == 2)
                            {
                                thoriumPlayer.empowerTimerCriticalStrike = 60;
                            }
                        }
                        if (thoriumPlayer.cyberBardValue == 1)
                        {
                            if (thoriumPlayer.empowerAttackSpeed < 2)
                            {
                                CombatText.NewText(new Rectangle((int)player2.position.X, (int)player2.position.Y, player2.width, player2.height), new Color(225, 150, 50), "+8% attack speed", false, false);
                                thoriumPlayer.empowerAttackSpeed = 2;
                            }
                            if (thoriumPlayer.empowerMovementSpeed < 2)
                            {
                                CombatText.NewText(new Rectangle((int)player2.position.X, (int)player2.position.Y, player2.width, player2.height), new Color(102, 255, 0), "+15% movement speed", false, false);
                                thoriumPlayer.empowerMovementSpeed = 2;
                            }
                            if (thoriumPlayer.empowerAttackSpeed == 2)
                            {
                                thoriumPlayer.empowerTimerAttackSpeed = 60;
                            }
                            if (thoriumPlayer.empowerMovementSpeed == 2)
                            {
                                thoriumPlayer.empowerTimerMovementSpeed = 60;
                            }
                        }
                        if (thoriumPlayer.cyberBardValue == 2)
                        {
                            if (thoriumPlayer.empowerLifeRegen < 2)
                            {
                                CombatText.NewText(new Rectangle((int)player2.position.X, (int)player2.position.Y - 10, player2.width, player2.height), new Color(255, 100, 175), "+2 life/sec", false, false);
                                thoriumPlayer.empowerLifeRegen = 2;
                            }
                            if (thoriumPlayer.empowerManaRegen < 2)
                            {
                                CombatText.NewText(new Rectangle((int)player2.position.X, (int)player2.position.Y, player2.width, player2.height), new Color(102, 102, 255), "+4 mana/sec", false, false);
                                thoriumPlayer.empowerManaRegen = 2;
                            }
                            if (thoriumPlayer.empowerLifeRegen == 2)
                            {
                                thoriumPlayer.empowerTimerLifeRegen = 60;
                            }
                            if (thoriumPlayer.empowerManaRegen == 2)
                            {
                                thoriumPlayer.empowerTimerManaRegen = 60;
                            }
                        }
                        if (thoriumPlayer.cyberBardValue == 3)
                        {
                            if (thoriumPlayer.empowerDamageReduction < 2)
                            {
                                CombatText.NewText(new Rectangle((int)player2.position.X, (int)player2.position.Y - 10, player2.width, player2.height), new Color(100, 175, 255), "+8% damage reduction", false, false);
                                thoriumPlayer.empowerDamageReduction = 2;
                            }
                            if (thoriumPlayer.empowerDefense < 2)
                            {
                                CombatText.NewText(new Rectangle((int)player2.position.X, (int)player2.position.Y, player2.width, player2.height), new Color(160, 160, 160), "+8 defense", false, false);
                                thoriumPlayer.empowerDefense = 2;
                            }
                            if (thoriumPlayer.empowerDamageReduction == 2)
                            {
                                thoriumPlayer.empowerTimerDamageReduction = 60;
                            }
                            if (thoriumPlayer.empowerDefense == 2)
                            {
                                thoriumPlayer.empowerTimerDefense = 60;
                            }
                        }
                    }
                }
            }
            
            //auto tuner
            thoriumPlayer.autoTunerBool = true;
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3Damage = 2;
        }
       
        private readonly string[] items =
        {
            "CyberPunkHeadset",
            "CyberPunkSuit",
            "CyberPunkLeggings",
            "AutoTuner",
            "TunePlayerDamage",
            "VinylRecord",
            "Kazoo",
            "HallowedMegaphone",
            "BassBooster",
            "MidnightBassBooster",
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
