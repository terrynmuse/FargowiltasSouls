using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace FargowiltasSouls
{
    internal class Soulcheck : UIState
    {
        private readonly static Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly static Mod calamity = ModLoader.GetMod("CalamityMod");

        public static bool Visible = false;
        public static string owner = "";
        public static int pageNumber = 1;
        public static int totalPages = 3;

        public static readonly Dictionary<string, bool> ToggleDict = new Dictionary<string, bool>();
        public static readonly Dictionary<string, UiCheckbox> checkboxDict = new Dictionary<string, UiCheckbox>();

        public static readonly Dictionary<string, Color> toggles = new Dictionary<string, Color>
        {
            #region enchantment toggles

            ["Hunter Buff"] = new Color(81, 181, 113),
            ["Dangersense Buff"] = new Color(81, 181, 113),
            ["Spelunker Buff"] = new Color(81, 181, 113),
            ["Shine Buff"] = new Color(81, 181, 113),
            ["Palm Tree Sentry"] = new Color(81, 181, 113),
            ["Boreal Snowball Support"] = new Color(81, 181, 113),
            ["Mahogany Hook Support"] = new Color(81, 181, 113),
            ["Shadowflame Aura"] = new Color(81, 181, 113),
            ["Cactus Needles"] = new Color(81, 181, 113),
            ["Pumpkin Fire"] = new Color(81, 181, 113),
            ["Copper Lightning"] = new Color(81, 181, 113),
            ["Tin Crit"] = new Color(81, 181, 113),
            ["Iron Magnet"] = new Color(81, 181, 113),
            ["Iron Shield"] = new Color(81, 181, 113),
            ["Silver Sword Familiar"] = new Color(81, 181, 113),
            ["Tungsten Effect"] = new Color(81, 181, 113),
            ["Gold Lucky Coin"] = new Color(81, 181, 113),
            ["Shadow Darkness"] = new Color(81, 181, 113),
            ["Gladiator Rain"] = new Color(81, 181, 113),
            ["Jungle Spores"] = new Color(81, 181, 113),
            ["Meteor Shower"] = new Color(81, 181, 113),
            ["Necro Guardian"] = new Color(81, 181, 113),
            ["Molten Inferno"] = new Color(81, 181, 113),
            ["Rainbow Trail"] = new Color(81, 181, 113),
            ["Cobalt Shards"] = new Color(81, 181, 113),
            ["Palladium Healing"] = new Color(81, 181, 113),
            ["Mythril Speedup"] = new Color(81, 181, 113),
            ["Orichalcum Fireballs"] = new Color(81, 181, 113),
            ["Adamantite Splitting"] = new Color(81, 181, 113),
            ["Titanium Shadow Dodge"] = new Color(81, 181, 113),
            ["Spider Swarm"] = new Color(81, 181, 113),
            ["Frost Icicles"] = new Color(81, 181, 113),
            ["Forbidden Storm"] = new Color(81, 181, 113),
            ["Enchanted Sword Familiar"] = new Color(81, 181, 113),
            ["Hallowed Shield"] = new Color(81, 181, 113),
            ["Chlorophyte Leaf Crystal"] = new Color(81, 181, 113),
            ["Turtle Shell Buff"] = new Color(81, 181, 113),
            ["Beetles"] = new Color(81, 181, 113),
            ["Shroomite Stealth"] = new Color(81, 181, 113),
            ["Spectre Orbs"] = new Color(81, 181, 113),
            ["Spooky Scythes"] = new Color(81, 181, 113),
            ["Dark Artist Effect"] = new Color(81, 181, 113),
            ["Shinobi Through Walls"] = new Color(81, 181, 113),
            ["Red Riding Super Bleed"] = new Color(81, 181, 113),
            ["Valhalla Knockback"] = new Color(81, 181, 113),
            ["Solar Shield"] = new Color(81, 181, 113),
            ["Vortex Stealth"] = new Color(81, 181, 113),
            ["Vortex Voids"] = new Color(81, 181, 113),
            ["Nebula Boosters"] = new Color(81, 181, 113),
            ["Stardust Guardian"] = new Color(81, 181, 113),

            #endregion

            #region soul toggles
            ["Melee Speed"] = new Color(81, 181, 113),
            ["Spore Sac"] = new Color(81, 181, 113),
            ["Builder Mode"] = new Color(81, 181, 113),
            ["Universe Attack Speed"] = new Color(81, 181, 113),
            ["Sniper Scope"] = new Color(81, 181, 113),
            ["Supersonic Speed Boosts"] = new Color(81, 181, 113),
            ["Slimy Shield Effects"] = new Color(81, 181, 113),
            ["Scythes When Dashing"] = new Color(81, 181, 113),
            ["Tiny Eaters"] = new Color(81, 181, 113),
            ["Creeper Shield"] = new Color(81, 181, 113),
            ["Frostfireballs"] = new Color(81, 181, 113),
            ["Inflict Clipped Wings"] = new Color(81, 181, 113),
            ["Inflict Lightning Rod"] = new Color(81, 181, 113),
            ["Pumpking's Cape Support"] = new Color(81, 181, 113),
            ["Lihzahrd Ground Pound"] = new Color(81, 181, 113),
            ["Celestial Rune Support"] = new Color(81, 181, 113),
            ["Spectral Fishron"] = new Color(81, 181, 113),
            ["Gravity Control"] = new Color(81, 181, 113),
            ["Skeletron Arms Minion"] = new Color(81, 181, 113),
            ["Rainbow Slime Minion"] = new Color(81, 181, 113),
            ["Probes Minion"] = new Color(81, 181, 113),
            ["Plantera Minion"] = new Color(81, 181, 113),
            ["Pungent Eye Minion"] = new Color(81, 181, 113),
            ["Flocko Minion"] = new Color(81, 181, 113),
            ["Saucer Minion"] = new Color(81, 181, 113),
            ["Cultist Minion"] = new Color(81, 181, 113),
            ["True Eyes Minion"] = new Color(81, 181, 113),
            ["Tentacles On Hit"] = new Color(81, 181, 113),
            ["Spiky Balls On Hit"] = new Color(81, 181, 113),
            ["Ancient Visions On Hit"] = new Color(81, 181, 113),
            ["Stars On Hit"] = new Color(81, 181, 113),
            ["Bees On Hit"] = new Color(81, 181, 113),
            ["Super Blood On Hit"] = new Color(81, 181, 113),
            ["Eternity Stacking"] = new Color(81, 181, 113),
            #endregion
        };

        public static readonly Dictionary<string, Color> togglesPets = new Dictionary<string, Color>
        {
            #region pet toggles
            ["Black Cat Pet"] = new Color(81, 181, 113),
            ["Companion Cube Pet"] = new Color(81, 181, 113),
            ["Crimson Heart Pet"] = new Color(81, 181, 113),
            ["Cursed Sapling Pet"] = new Color(81, 181, 113),
            ["Dino Pet"] = new Color(81, 181, 113),
            ["Dragon Pet"] = new Color(81, 181, 113),
            ["Eater Pet"] = new Color(81, 181, 113),
            ["Eye Spring Pet"] = new Color(81, 181, 113),
            ["Fairy Pet"] = new Color(81, 181, 113),
            ["Face Monster Pet"] = new Color(81, 181, 113),
            ["Flickerwick Pet"] = new Color(81, 181, 113),
            ["Gato Pet"] = new Color(81, 181, 113),
            //["Grinch Pet"] = new Color(81, 181, 113),
            ["Hornet Pet"] = new Color(81, 181, 113),
            ["Lizard Pet"] = new Color(81, 181, 113),
            ["Magic Lantern Pet"] = new Color(81, 181, 113),
            ["Mini Minotaur Pet"] = new Color(81, 181, 113),
            ["Parrot Pet"] = new Color(81, 181, 113),
            ["Penguin Pet"] = new Color(81, 181, 113),
            ["Puppy Pet"] = new Color(81, 181, 113),
            ["Seedling Pet"] = new Color(81, 181, 113),
            ["Shadow Orb Pet"] = new Color(81, 181, 113),
            ["Skeletron Pet"] = new Color(81, 181, 113),
            ["Snowman Pet"] = new Color(81, 181, 113),
            ["Spider Pet"] = new Color(81, 181, 113),
            ["Squashling Pet"] = new Color(81, 181, 113),
            ["Suspicious Eye Pet"] = new Color(81, 181, 113),
            ["Tiki Pet"] = new Color(81, 181, 113),
            ["Truffle Pet"] = new Color(81, 181, 113),
            ["Turtle Pet"] = new Color(81, 181, 113),
            ["Wisp Pet"] = new Color(81, 181, 113),
            ["Zephyr Fish Pet"] = new Color(81, 181, 113),
            #endregion
        };

        public static readonly Dictionary<string, Color> togglesReforges = new Dictionary<string, Color>
        {
            ["Warding"] = new Color(81, 181, 113),
            ["Violent"] = new Color(81, 181, 113),
            ["Quick"] = new Color(81, 181, 113),
            ["Lucky"] = new Color(81, 181, 113),
            ["Menacing"] = new Color(81, 181, 113),
            ["Legendary"] = new Color(81, 181, 113),
            ["Unreal"] = new Color(81, 181, 113),
            ["Mythical"] = new Color(81, 181, 113),
            ["Godly"] = new Color(81, 181, 113),
            ["Demonic"] = new Color(81, 181, 113),
            ["Ruthless"] = new Color(81, 181, 113),
            ["Light"] = new Color(81, 181, 113),
            ["Deadly"] = new Color(81, 181, 113),
            ["Rapid"] = new Color(81, 181, 113)
        };

        public static readonly Dictionary<string, Color> togglesThorium = new Dictionary<string, Color>
        {
            ["Air Walkers"] = new Color(81, 181, 113),
            ["Crystal Scorpion"] = new Color(81, 181, 113),
            ["Yuma's Pendant"] = new Color(81, 181, 113),
            ["Head Mirror"] = new Color(81, 181, 113),
            ["Celestial Aura"] = new Color(81, 181, 113),
            ["Ascension Statuette"] = new Color(81, 181, 113),
            ["Mana-Charged Rocketeers"] = new Color(81, 181, 113),
            ["Bronze Lightning"] = new Color(81, 181, 113),
            ["Illumite Rocket"] = new Color(81, 181, 113),
            ["Jester Bell"] = new Color(81, 181, 113),
            ["Eye of the Beholder"] = new Color(81, 181, 113),
            ["Terrarium Spirits"] = new Color(81, 181, 113),
            ["Crietz"] = new Color(81, 181, 113),
            ["Yew Wood Crits"] = new Color(81, 181, 113),
            ["Cryo-Magus Damage"] = new Color(81, 181, 113),
            ["White Dwarf Flares"] = new Color(81, 181, 113),
            ["Depth Diver Foam"] = new Color(81, 181, 113),
            ["Whispering Tentacles"] = new Color(81, 181, 113),
            ["Icy Barrier"] = new Color(81, 181, 113),
            ["Plague Lord's Flask"] = new Color(81, 181, 113),
            ["Tide Turner Globules"] = new Color(81, 181, 113),
            ["Tide Turner Daggers"] = new Color(81, 181, 113),
            ["Folv's Aura"] = new Color(81, 181, 113),
            ["Folv's Bolts"] = new Color(81, 181, 113),
            ["Vampire Gland"] = new Color(81, 181, 113),
            ["Flesh Drops"] = new Color(81, 181, 113),
            ["Dragon Flames"] = new Color(81, 181, 113),
            ["Harbinger Overcharge"] = new Color(81, 181, 113),
            ["Assassin Damage"] = new Color(81, 181, 113),
            ["Pyromancer Bursts"] = new Color(81, 181, 113),
            ["Conduit Shield"] = new Color(81, 181, 113),
            ["Incandescent Spark"] = new Color(81, 181, 113),
            ["Greedy Magnet"] = new Color(81, 181, 113),
            ["Cyber Punk States"] = new Color(81, 181, 113),
            ["Metronome"] = new Color(81, 181, 113),
            ["Mix Tape"] = new Color(81, 181, 113),
            ["Lodestone Resistance"] = new Color(81, 181, 113),
            ["Biotech Probe"] = new Color(81, 181, 113),
            ["Proof of Avarice"] = new Color(81, 181, 113),
            ["Slag Stompers"] = new Color(81, 181, 113),
            ["Bee Booties"] = new Color(81, 181, 113),
            ["Ghastly Carapace"] = new Color(81, 181, 113),
            ["Spirit Trapper Wisps"] = new Color(81, 181, 113),
            ["Warlock Wisps"] = new Color(81, 181, 113),
            ["Dread Speed"] = new Color(81, 181, 113),


            ["Li'l Devil Minion"] = new Color(81, 181, 113),
            ["Li'l Cherub Minion"] = new Color(81, 181, 113),
            ["Sapling Minion"] = new Color(81, 181, 113),

            ["Omega Pet"] = new Color(81, 181, 113),
            ["I.F.O. Pet"] = new Color(81, 181, 113),
            ["Bio-Feeder Pet"] = new Color(81, 181, 113),
            ["Blister Pet"] = new Color(81, 181, 113),
            ["Wyvern Pet"] = new Color(81, 181, 113),
            ["Inspiring Lantern Pet"] = new Color(81, 181, 113),
            ["Lock Box Pet"] = new Color(81, 181, 113),
            ["Life Spirit Pet"] = new Color(81, 181, 113),
            ["Holy Goat Pet"] = new Color(81, 181, 113),
            ["Owl Pet"] = new Color(81, 181, 113),
            ["Jellyfish Pet"] = new Color(81, 181, 113),
            ["Moogle Pet"] = new Color(81, 181, 113),
            ["Maid Pet"] = new Color(81, 181, 113),
            ["Pink Slime Pet"] = new Color(81, 181, 113),
            ["Glitter Pet"] = new Color(81, 181, 113),
            ["Coin Bag Pet"] = new Color(81, 181, 113),
        };

        public static readonly Dictionary<string, Color> togglesCalamity = new Dictionary<string, Color>
        {
            ["Victide Sea Urchin"] = new Color(81, 181, 113),
            ["Profaned Soul Artifact"] = new Color(81, 181, 113),
            ["Slime God Minion"] = new Color(81, 181, 113),
            ["Reaver Orb Minion"] = new Color(81, 181, 113),
            ["Omega Blue Tentacles"] = new Color(81, 181, 113),
            ["Silva Crystal Minion"] = new Color(81, 181, 113),
            ["Godly Soul Artifact"] = new Color(81, 181, 113),
            ["Mechworm Minion"] = new Color(81, 181, 113),
            ["Nebulous Core"] = new Color(81, 181, 113),
            ["Red Devil Minion"] = new Color(81, 181, 113),
            ["Permafrost's Concoction"] = new Color(81, 181, 113),
            ["Daedalus Crystal Minion"] = new Color(81, 181, 113),
            ["Polterghast Mines"] = new Color(81, 181, 113),
            ["Plague Hive"] = new Color(81, 181, 113),
            ["Chaos Spirit Minion"] = new Color(81, 181, 113),
            ["Valkyrie Minion"] = new Color(81, 181, 113),
            ["Yharim's Gift"] = new Color(81, 181, 113),
            ["Fungal Clump Minion"] = new Color(81, 181, 113),
            ["Elemental Waifus"] = new Color(81, 181, 113),
            ["Shellfish Minions"] = new Color(81, 181, 113),
            ["Amidias' Pendant"] = new Color(81, 181, 113),
            ["Giant Pearl"] = new Color(81, 181, 113),
            ["Poisonous Sea Water"] = new Color(81, 181, 113),
            //[""] = new Color(81, 181, 113),
            //abyssal diving gear
            //every thing reee
        };

        private static readonly Color _defaultColor = new Color(81, 181, 113);
        private static readonly Color _wtf = new Color(173, 94, 171);
        private static UIPanel _checklistPanel;
        private bool _dragging;

        private Vector2 _offset;
        private static float _left;
        private static float _top = 20f;

        public override void OnInitialize()
        {
            // Is initialize called? (Yes it is called on reload) I want to reset nicely with new character or new loaded mods: visible = false;

            _checklistPanel = new UIPanel();
            _checklistPanel.SetPadding(10);
            _checklistPanel.Width.Set(1000f, 0f);
            _checklistPanel.Height.Set(600f, 0f);
            _checklistPanel.Left.Set((Main.screenWidth - 1200) / 2f, 0f);
            _checklistPanel.Top.Set((Main.screenHeight - 700) / 2f, 0f);
            _checklistPanel.BackgroundColor = new Color(73, 94, 171, 150);
            //_checklistPanel.OnMouseDown += DragOn;
            //_checklistPanel.OnMouseUp += DragOff;
            Append(_checklistPanel);

            UiCheckbox.CheckboxTexture = Fargowiltas.Instance.GetTexture("checkBox");

            if (thorium != null && calamity != null)
            {
                totalPages = 5;
            }
            else if (calamity != null || thorium != null)
            {
                totalPages = 4;
            }

            for (int i = 1; i <= totalPages; i++)
            {
                PlaceBoxes();
                pageNumber++;
            }

            pageNumber = 1;
        }

        public static void PlaceBoxes()
        {
            _left = 0;
            _top = 20f;
            _checklistPanel.RemoveAllChildren();

            switch (pageNumber)
            {
                case 1:
                    foreach (KeyValuePair<string, Color> toggle in toggles)
                    {
                        CreateCheckbox(toggle.Key, toggle.Value);
                    }
                    break;
                case 2:
                    foreach (KeyValuePair<string, Color> toggle in togglesPets)
                    {
                        CreateCheckbox(toggle.Key, toggle.Value);
                    }
                    break;
                case 3:
                    foreach (KeyValuePair<string, Color> toggle in togglesReforges)
                    {
                        CreateCheckbox(toggle.Key, toggle.Value);
                    }
                    break;
                case 4:
                    if (thorium == null)
                    {
                        foreach (KeyValuePair<string, Color> toggle in togglesCalamity)
                        {
                            CreateCheckbox(toggle.Key, toggle.Value);
                        }
                    }
                    else
                    {
                        foreach (KeyValuePair<string, Color> toggle in togglesThorium)
                        {
                            CreateCheckbox(toggle.Key, toggle.Value);
                        }
                    }
                    break;
                case 5:
                    foreach (KeyValuePair<string, Color> toggle in togglesCalamity)
                    {
                        CreateCheckbox(toggle.Key, toggle.Value);
                    }
                    break;
            }

            CreateNextButton();
        }

        private static void CreateCheckbox(string name, Color color)
        {
            if (!ToggleDict.ContainsKey(name)) ToggleDict.Add(name, true);

            UiCheckbox uibox = new UiCheckbox(name, "", color, _wtf);
            
            uibox.Left.Set(_left, 0f);
            uibox.Top.Set(_top, 0f);
            
            uibox.OnSelectedChanged += (o, e) =>
            {
                ToggleDict[name] = !ToggleDict[name];
                uibox.Color = uibox.Color == _defaultColor ? Color.Gray : _defaultColor;
            };

            _checklistPanel.Append(uibox);

            if (checkboxDict.ContainsKey(name))
            {
                bool value;
                ToggleDict.TryGetValue(name, out value);
                if (!value)
                {
                    uibox.Color = Color.Gray;
                }
            }
            else
            {
                checkboxDict.Add(name, uibox);
            }
                

            _top += 25f;
            if (!(_top >= 565)) return;
            _top = 20f;
            _left += 250;
        }

        private static void CreateNextButton()
        {
            UiCheckbox uibox = new UiCheckbox("Next Page", "", Color.Red, _wtf);

            uibox.Left.Set(750, 0f);
            uibox.Top.Set(545, 0f);

            uibox.OnSelectedChanged += (o, e) =>
            {
                pageNumber++;

                if (pageNumber > totalPages)
                {
                    pageNumber = 1;
                }

                PlaceBoxes();
            };

            _checklistPanel.Append(uibox);
        }

        public static bool GetValue(string buff)
        {
            bool ret;
            ToggleDict.TryGetValue(buff, out ret);
            return ret;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Vector2 mousePosition = new Vector2(Main.mouseX, Main.mouseY);

            if (_checklistPanel.ContainsPoint(mousePosition)) Main.LocalPlayer.mouseInterface = true;

            if (!_dragging) return;

            _checklistPanel.Left.Set(mousePosition.X - _offset.X, 0f);
            _checklistPanel.Top.Set(mousePosition.Y - _offset.Y, 0f);
            Recalculate();
        }

        /*private void DragOn(UIMouseEvent evt, UIElement listeningElement)
        {
            _offset = new Vector2(evt.MousePosition.X - _checklistPanel.Left.Pixels,
                evt.MousePosition.Y - _checklistPanel.Top.Pixels);

            _dragging = true;
        }

        private void DragOff(UIMouseEvent evt, UIElement listeningElement)
        {
            Vector2 end = evt.MousePosition;
            _dragging = false;

            _checklistPanel.Left.Set(end.X - _offset.X, 0f);
            _checklistPanel.Top.Set(end.Y - _offset.Y, 0f);

            Recalculate();
        }*/
    }
}