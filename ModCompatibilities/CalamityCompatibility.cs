using CalamityMod.Items.Armor;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.ModCompatibilities
{
    public class CalamityCompatibility : ModCompatibility
    {
        public CalamityCompatibility(Mod callerMod) : base(callerMod, nameof(CalamityMod))
        {
        }


        protected override void AddRecipeGroups()
        {
            //Aerospec
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Aerospec Helmet", ModContent.ItemType<AerospecHat>(), ModContent.ItemType<AerospecHeadgear>(), ModContent.ItemType<AerospecHelm>(), ModContent.ItemType<AerospecHood>(), ModContent.ItemType<AerospecHelmet>());
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAerospecHelmet", group);

            //Ataxia
            group = new RecipeGroup(() => Lang.misc[37] + " Ataxia Helmet", ModContent.ItemType<AtaxiaHeadgear>(), ModContent.ItemType<AtaxiaHelm>(), ModContent.ItemType<AtaxiaHood>(), ModContent.ItemType<AtaxiaHelmet>(), ModContent.ItemType<AtaxiaMask>());
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAtaxiaHelmet", group);
            
            //Auric
            group = new RecipeGroup(() => Lang.misc[37] + " Auric Helmet", ModContent.ItemType<AuricTeslaHelm>(), ModContent.ItemType<AuricTeslaPlumedHelm>(), ModContent.ItemType<AuricTeslaHoodedFacemask>(), ModContent.ItemType<AuricTeslaSpaceHelmet>(), ModContent.ItemType<AuricTeslaWireHemmedVisage>());
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAuricHelmet", group);
            
            //Bloodflare
            group = new RecipeGroup(() => Lang.misc[37] + " Bloodflare Helmet", ModContent.ItemType<BloodflareHelm>(), ModContent.ItemType<BloodflareHelmet>(), ModContent.ItemType<BloodflareHornedHelm>(), ModContent.ItemType<BloodflareHornedMask>(), ModContent.ItemType<BloodflareMask>());
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBloodflareHelmet", group);
            
            //Daedalus
            group = new RecipeGroup(() => Lang.misc[37] + " Daedalus Helmet", ModContent.ItemType<DaedalusHelm>(), ModContent.ItemType<DaedalusHelmet>(), ModContent.ItemType<DaedalusHat>(), ModContent.ItemType<DaedalusHeadgear>(), ModContent.ItemType<DaedalusVisor>());
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyDaedalusHelmet", group);
            
            // Godslayer
            group = new RecipeGroup(() => Lang.misc[37] + " Godslayer Helmet", ModContent.ItemType<GodSlayerHelm>(), ModContent.ItemType<GodSlayerHelmet>(), ModContent.ItemType<GodSlayerVisage>(), ModContent.ItemType<GodSlayerHornedHelm>(), ModContent.ItemType<GodSlayerMask>());
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyGodslayerHelmet", group);
            
            // Reaver
            group = new RecipeGroup(() => Lang.misc[37] + " Reaver Helmet", ModContent.ItemType<ReaverHelm>(), ModContent.ItemType<ReaverVisage>(), ModContent.ItemType<ReaverMask>(), ModContent.ItemType<ReaverHelmet>(), ModContent.ItemType<ReaverCap>());
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyReaverHelmet", group);
            
            //Silva
            group = new RecipeGroup(() => Lang.misc[37] + " Silva Helmet", ModContent.ItemType<SilvaHelm>(), ModContent.ItemType<SilvaHornedHelm>(), ModContent.ItemType<SilvaMaskedCap>(), ModContent.ItemType<SilvaHelmet>(), ModContent.ItemType<SilvaMask>());
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySilvaHelmet", group);
            
            //Statigel
            group = new RecipeGroup(() => Lang.misc[37] + " Statigel Helmet", ModContent.ItemType<StatigelHelm>(), ModContent.ItemType<StatigelHeadgear>(), ModContent.ItemType<StatigelCap>(), ModContent.ItemType<StatigelHood>(), ModContent.ItemType<StatigelMask>());
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyStatigelHelmet", group);
            
            //Tarragon
            group = new RecipeGroup(() => Lang.misc[37] + " Tarragon Helmet", ModContent.ItemType<TarragonHelm>(), ModContent.ItemType<TarragonVisage>(), ModContent.ItemType<TarragonMask>(), ModContent.ItemType<TarragonHornedHelm>(), ModContent.ItemType<TarragonHelmet>());
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyTarragonHelmet", group);
            
            //Victide
            group = new RecipeGroup(() => Lang.misc[37] + " Victide Helmet", ModContent.ItemType<VictideHelm>(), ModContent.ItemType<VictideVisage>(), ModContent.ItemType<VictideMask>(), ModContent.ItemType<VictideHelmet>(), ModContent.ItemType<VictideHeadgear>());
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyVictideHelmet", group);
            
            //Wulfrum
            group = new RecipeGroup(() => Lang.misc[37] + " Wulfrum Helmet", ModContent.ItemType<WulfrumHelm>(), ModContent.ItemType<WulfrumHeadgear>(), ModContent.ItemType<WulfrumHood>(), ModContent.ItemType<WulfrumHelmet>(), ModContent.ItemType<WulfrumMask>());
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyWulfrumHelmet", group);
        }
    }
}