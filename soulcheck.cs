using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace FargowiltasSouls
{
    internal class Soulcheck : UIState
    {
        private UIPanel _checklistPanel;
        public static bool Visible = false;

        public static readonly Dictionary<string, bool> ToggleDict = new Dictionary<string, bool>();

        public static bool GetValue(string buff)
        {
            bool ret;
            ToggleDict.TryGetValue(buff, out ret);
            ErrorLogger.Log(buff + ": " + ret);
            return ret;
        }

        private readonly Color _wtf = new Color(173, 94, 171);
        private float _left;
        private float _top = 20f;

        private void CreateCheckbox(string name, Color color)
        {
            if (ToggleDict.Count != _buffs.Count)
            {
                ToggleDict.Add(name, true);
            }


            UiCheckbox uibox = new UiCheckbox(name, "", color, _wtf);
            uibox.Left.Set(_left, 0f);
            uibox.Top.Set(_top, 0f);
            uibox.OnSelectedChanged += (o, e) => { ToggleDict[name] = !ToggleDict[name]; };
            _checklistPanel.Append(uibox);

            _top += 25f;
            if (!(_top >= 565)) return;
            _top = 20f;
            _left += 190f;
        }

        private readonly Dictionary<string, Color> _buffs = new Dictionary<string, Color>
        {
            ["Inferno Buff"] = new Color(244, 121, 13),
            ["Hallowed Shield"] = new Color(224, 221, 44),
            ["Split Enemies"] = new Color(242, 201, 21),
            ["Seasonal Enemies"] = new Color(114, 74, 25),
            ["Beetles"] = new Color(88, 89, 153),
            ["Leaf Crystal"] = new Color(47, 224, 67),
            ["Forbidden Storm"] = new Color(221, 186, 171),
            ["Stardust Guardian"] = new Color(11, 221, 196),
            ["Solar Shield"] = new Color(229, 124, 11),
            ["Shroomite Stealth"] = new Color(11, 42, 196),
            ["Orichalcum Fireball"] = new Color(211, 99, 192),
            ["Spooky Scythes"] = new Color(37, 41, 68),
            ["Hunter Buff"] = new Color(219, 143, 37),
            ["Spelunker Buff"] = new Color(246, 255, 2),
            ["Dangersense Buff"] = new Color(209, 75, 27),
            ["Shine Buff"] = new Color(247, 255, 48),
            ["Spore Sac"] = new Color(93, 255, 0),
            ["Super Speed"] = new Color(255, 25, 52),
            ["Melee Speed"] = new Color(255, 178, 0),
            ["Splitting Projectiles"] = new Color(224, 58, 58),
            ["Increase Use Speed"] = new Color(81, 181, 113),
            ["Bees on Hit"] = new Color(81, 181, 113),
            ["Baby Dino Pet"] = new Color(81, 181, 113),
            ["Baby Penguin Pet"] = new Color(81, 181, 113),
            ["Baby Skeletron Pet"] = new Color(81, 181, 113),
            ["Turtle Pet"] = new Color(81, 181, 113),
            ["Baby Snowman Pet"] = new Color(81, 181, 113),
            ["Zephyr Fish Pet"] = new Color(81, 181, 113),
            ["Companion Cube Pet"] = new Color(81, 181, 113),
            ["Baby Grinch Pet"] = new Color(81, 181, 113),
            ["Lizard Pet"] = new Color(81, 181, 113),
            ["Suspicious Looking Eye Pet"] = new Color(81, 181, 113),
            ["Mini Minotaur Pet"] = new Color(81, 181, 113),
            ["Baby Eater Pet"] = new Color(81, 181, 113),
            ["Baby Face Monster Pet"] = new Color(81, 181, 113),
            ["Spider Pet"] = new Color(81, 181, 113),
            ["Baby Hornet Pet"] = new Color(81, 181, 113),
            ["Wisp Pet"] = new Color(81, 181, 113),
            ["Cursed Sapling Pet"] = new Color(81, 181, 113),
            ["Black Cat Pet"] = new Color(81, 181, 113),
            ["Seedling Pet"] = new Color(81, 181, 113),
            ["Crimson Heart Pet"] = new Color(81, 181, 113),
            ["Magic Lantern Pet"] = new Color(81, 181, 113),
            ["Truffle Pet"] = new Color(81, 181, 113),
            ["Squashling Pet"] = new Color(81, 181, 113),
            ["Silver Sword Familiar"] = new Color(81, 181, 113),
            ["Enchanted Sword Familiar"] = new Color(81, 181, 113),
            ["Tiki Pet"] = new Color(81, 181, 113),
            ["Gato Pet"] = new Color(81, 181, 113),
            ["Flickerwick Pet"] = new Color(81, 181, 113),
            ["Puppy Pet"] = new Color(81, 181, 113),
            ["Dragon Pet"] = new Color(81, 181, 113),
            ["Parrot Pet"] = new Color(81, 181, 113),
            ["Fairy Pet"] = new Color(81, 181, 113),
            ["Companion Cube Pet"] = new Color(81, 181, 113),
            ["Eye Spring Pet"] = new Color(81, 181, 113),
        };

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

            foreach (KeyValuePair<string, Color> buff in _buffs)
            {
                CreateCheckbox(buff.Key, buff.Value);
            }
        }

        private Vector2 _offset;
        private bool _dragging;

        private void DragOn(UIMouseEvent evt, UIElement listeningElement)
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
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Vector2 mousePosition = new Vector2(Main.mouseX, Main.mouseY);
            if (_checklistPanel.ContainsPoint(mousePosition))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (!_dragging) return;
            _checklistPanel.Left.Set(mousePosition.X - _offset.X, 0f);
            _checklistPanel.Top.Set(mousePosition.Y - _offset.Y, 0f);
            Recalculate();
        }
    }
}