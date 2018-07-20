using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace FargowiltasSouls
{
    class Soulcheck : UIState
    {
        private UIPanel _checklistPanel;
        public static bool Visible = false;

        public static readonly Dictionary<string, bool> ToggleDict = new Dictionary<string, bool>();

        public static bool GetValue(string buff)
        {
            ToggleDict.TryGetValue(buff, out bool ret);
            ErrorLogger.Log(buff + ": " + ret);
            return ret;
        }

        private readonly Color _wtf = new Color(173, 94, 171);
        private float _left;
        private float _top = 20f;

        private void CreateCheckbox(string name, Color color)
        {
            ToggleDict.Add(name, true);

            UiCheckbox temp = new UiCheckbox(name, "", color, _wtf);
            temp.Left.Set(_left, 0f);
            temp.Top.Set(_top, 0f);
            temp.OnSelectedChanged += (o, e) =>
            {
                ToggleDict[name] = !ToggleDict[name];
            };
            _checklistPanel.Append(temp);

            _top += 25f;
            if (!(_top >= 540)) return;
            _top = 20f;
            _left += 190f;
        }

        public override void OnInitialize()
        {
            // Is initialize called? (Yes it is called on reload) I want to reset nicely with new character or new loaded mods: visible = false;

            _checklistPanel = new UIPanel();
            _checklistPanel.SetPadding(10);
            _checklistPanel.Width.Set(450f, 0f);
            _checklistPanel.Height.Set(600f, 0f);
            _checklistPanel.Left.Set(1000f, 0f);
            _checklistPanel.Top.Set(450f, 0f);
            _checklistPanel.BackgroundColor = new Color(73, 94, 171);
            _checklistPanel.OnMouseDown += DragOn;
            _checklistPanel.OnMouseUp += DragOff;
            Append(_checklistPanel);

            CreateCheckbox("Inferno Buff", new Color(244, 121, 13));
            CreateCheckbox("Hallowed Shield", new Color(224, 221, 44));
            CreateCheckbox("Split Enemies", new Color(242, 201, 21));
            CreateCheckbox("Seasonal Enemies", new Color(114, 74, 25));
            CreateCheckbox("Beetles", new Color(88, 89, 153));
            CreateCheckbox("Leaf Crystal", new Color(47, 224, 67));
            CreateCheckbox("Spore Explosion", new Color(12, 183, 32));
            CreateCheckbox("Forbidden Storm", new Color(221, 186, 171));
            CreateCheckbox("Stardust guardian", new Color(11, 221, 196));
            CreateCheckbox("Solar Shield", new Color(229, 124, 11));
            CreateCheckbox("Shroomite Stealth", new Color(11, 42, 196));
            CreateCheckbox("Orichalcum Fireball", new Color(211, 99, 192));
            CreateCheckbox("Spooky Scythes", new Color(37, 41, 68));
            CreateCheckbox("Hunter Buff", new Color(219, 143, 37));
            CreateCheckbox("Spelunker Buff", new Color(246, 255, 2));
            CreateCheckbox("Dangersense Buff", new Color(209, 75, 27));
            CreateCheckbox("Shine Buff", new Color(247, 255, 48));
            CreateCheckbox("Spore Sac", new Color(93, 255, 0));
            CreateCheckbox("Super Speed", new Color(255, 25, 52));
            CreateCheckbox("Melee Speed", new Color(255, 178, 0));
            CreateCheckbox("Splitting Projectiles", new Color(224, 58, 58));
            CreateCheckbox("Increase Use Speed", new Color(81, 181, 113));
            CreateCheckbox("Bees on Hit", new Color(81, 181, 113));
            CreateCheckbox("Baby Dino Pet", new Color(81, 181, 113));
            CreateCheckbox("Baby Penguin Pet", new Color(81, 181, 113));
            CreateCheckbox("Baby Skeletron Pet", new Color(81, 181, 113));
            CreateCheckbox("Turtle Pet", new Color(81, 181, 113));
            CreateCheckbox("Baby Snowman Pet", new Color(81, 181, 113));
            CreateCheckbox("Zephyr Fish Pet", new Color(81, 181, 113));
            CreateCheckbox("Companion Cube Pet", new Color(81, 181, 113));
            CreateCheckbox("Baby Grinch Pet", new Color(81, 181, 113));
            CreateCheckbox("Lizard Pet", new Color(81, 181, 113));
            CreateCheckbox("Suspicious Looking Eye Pet", new Color(81, 181, 113));
            CreateCheckbox("Mini Minotaur Pet", new Color(81, 181, 113));
            CreateCheckbox("Baby Eater Pet", new Color(81, 181, 113));
            CreateCheckbox("Baby Face Monster Pet", new Color(81, 181, 113));
            CreateCheckbox("Spider Pet", new Color(81, 181, 113));
            CreateCheckbox("Baby Hornet Pet", new Color(81, 181, 113));
            CreateCheckbox("Wisp Pet", new Color(81, 181, 113));
            CreateCheckbox("Cursed Sapling Pet", new Color(81, 181, 113));
            CreateCheckbox("Black Cat Pet", new Color(81, 181, 113));
            CreateCheckbox("Seedling Pet", new Color(81, 181, 113));
            CreateCheckbox("Crimson Heart Pet", new Color(81, 181, 113));

        }

        private Vector2 _offset;
        private bool _dragging;

        private void DragOn(UIMouseEvent evt, UIElement listeningElement)
        {
            _offset = new Vector2(evt.MousePosition.X - _checklistPanel.Left.Pixels, evt.MousePosition.Y - _checklistPanel.Top.Pixels);

            _dragging = true;
        }

        private void DragOff(UIMouseEvent evt, UIElement listeningElement)
        {
            Vector2 end = evt.MousePosition;
            _dragging = false;

            _checklistPanel.Left.Set(end.X - _offset.X, 0f);
            _checklistPanel.Top.Set(end.Y - _offset.Y, 0f);

            Recalculate();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Vector2 mousePosition = new Vector2(Main.mouseX, Main.mouseY);
            if (_checklistPanel.ContainsPoint(mousePosition))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            if (_dragging)
            {
                _checklistPanel.Left.Set(mousePosition.X - _offset.X, 0f);
                _checklistPanel.Top.Set(mousePosition.Y - _offset.Y, 0f);
                Recalculate();
            }
        }
    }
}