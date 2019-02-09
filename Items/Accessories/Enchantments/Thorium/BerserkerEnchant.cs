using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class BerserkerEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public bool allowJump = true;
        public int timer;
       
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berserker Enchantment");
            Tooltip.SetDefault(
@"'I'd rather fight for my life than live it'
Damage is increased by 15% at every 25% segment of life
Fire surrounds your armour and melee weapons
Enemies that you set on fire or singe will take additional damage over time
Nearby enemies are ignited
When you die, you violently explode dealing massive damage to surrounding enemies
Effects of Spring Steps and Slag Stompers
Effects of Molten Spear Tip and Orange Music Player");
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

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.orbital = true;
            thoriumPlayer.orbitalRotation3 = Utils.RotatedBy(thoriumPlayer.orbitalRotation3, -0.075000002980232239, default(Vector2));
            //making divers code less of a meme :scuseme:
            if (player.statLife > player.statLifeMax * 0.75)
            {
                modPlayer.AllDamageUp(.15f);
                thoriumPlayer.berserkStage = 1;
            }
            else if (player.statLife > player.statLifeMax * 0.5)
            {
                modPlayer.AllDamageUp(.3f);
                thoriumPlayer.berserkStage = 2;
            }
            else if (player.statLife > player.statLifeMax * 0.25)
            {
                modPlayer.AllDamageUp(.45f);
                thoriumPlayer.berserkStage = 3;
            }
            else
            {
                modPlayer.AllDamageUp(.6f);
                thoriumPlayer.berserkStage = 4;
            }
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3AttackSpeed = 2;
            //magma set bonus
            player.magmaStone = true;
            thoriumPlayer.magmaSet = true;
            //spring steps
            player.extraFall += 10;
            if (player.velocity.Y < 0f && allowJump)
            {
                allowJump = false;
                thoriumPlayer.jumps++;
            }
            if (player.velocity.Y > 0f || player.sliding || player.justJumped)
            {
                allowJump = true;
            }
            if (thoriumPlayer.jumps == 0)
            {
                player.jumpSpeedBoost += 5f;
            }
            if (thoriumPlayer.jumps == 1)
            {
                player.jumpSpeedBoost += 1f;
            }
            if (thoriumPlayer.jumps == 2)
            {
                player.jumpSpeedBoost += 1.75f;
            }
            if (thoriumPlayer.jumps >= 3)
            {
                float num = 16f;
                int num2 = 0;
                while (num2 < num)
                {
                    Vector2 vector = Vector2.UnitX * 0f;
                    vector += -Utils.RotatedBy(Vector2.UnitY, (num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(5f, 20f);
                    vector = Utils.RotatedBy(vector, Utils.ToRotation(player.velocity), default(Vector2));
                    int num3 = Dust.NewDust(player.Center, 0, 0, 127, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num3].scale = 1.35f;
                    Main.dust[num3].noGravity = true;
                    Main.dust[num3].position = player.Center + vector;
                    Dust dust = Main.dust[num3];
                    dust.position.Y = dust.position.Y + 12f;
                    Main.dust[num3].velocity = player.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                    int num4 = num2;
                    num2 = num4 + 1;
                }
                Main.PlaySound(SoundID.Item74, player.position);
                thoriumPlayer.jumps = 0;
            }
            //slag stompers
            timer++;
            if (timer > 20)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0.1f * Main.rand.Next(-25, 25), 2f, thorium.ProjectileType("SlagPro"), 20, 1f, Main.myPlayer, 0f, 0f);
                timer = 0;
            }
            //molten spear tip
            thoriumPlayer.spearFlame = true;
            //molten explode and inferno
            modPlayer.MoltenEffect(20);
        }
        
        private readonly string[] items =
        {
            "TunePlayerAttackSpeed",
            "SurtrsSword",
            "BloodyHighClaws",
            "ThermogenicImpaler",
            "WyvernSlayer"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("BerserkerMask"));
            recipe.AddIngredient(thorium.ItemType("BerserkerBreastplate"));
            recipe.AddIngredient(thorium.ItemType("BerserkerGreaves"));
            recipe.AddIngredient(null, "MagmaEnchant");
            recipe.AddIngredient(null, "MoltenEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
