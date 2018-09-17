using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FargowiltasSouls
{
    // TODO, tri-state checkbox.
    internal class UiCheckbox : UIText
    {
        private static readonly Texture2D _checkboxTexture = Fargowiltas.Instance.GetTexture("checkBox");
        private static readonly Texture2D _checkmarkTexture = Fargowiltas.Instance.GetTexture("checkMark");
        private readonly bool _clickable = true;
        private bool _selected = true;
        private readonly string _test;
        private readonly string _tooltip = "";
        public Color Color;
        public Color Olor;

        public float Order = 0;

        public UiCheckbox(string text, string tooltip, Color main, Color threed, bool clickable = true, float textScale = 1, bool large = false) : base("", textScale, large)
        {
            Color = main;
            Olor = threed;
            _tooltip = tooltip;
            _clickable = clickable;
            _test = "   " + text;
            SetText("   ");
            Recalculate();
        }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (value != _selected)
                {
                    _selected = value;
                    if (OnSelectedChanged != null) OnSelectedChanged(this, EventArgs.Empty);
                    //OnSelectedChanged?.Invoke();
                }
            }
        }

        public event EventHandler OnSelectedChanged;

        public override void Click(UIMouseEvent evt)
        {
            if (_clickable) Selected = !Selected;
            Recalculate();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle innerDimensions = GetInnerDimensions();
            //Vector2 pos = new Vector2(innerDimensions.X - 20, innerDimensions.Y - 5);
            Vector2 pos = new Vector2(innerDimensions.X, innerDimensions.Y - 5);
            Vector2 three = new Vector2(innerDimensions.X + 2, innerDimensions.Y - 3); //the positioning of the 3d part

            spriteBatch.Draw(_checkboxTexture, pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            if (Selected)
                spriteBatch.Draw(_checkmarkTexture, pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            base.DrawSelf(spriteBatch);
            //Utils.DrawBorderString(spriteBatch, this.test, three, this.olor, 1f, 0f, 0f, -1); the 3d part
            Utils.DrawBorderString(spriteBatch, _test, pos, Color);
            if (IsMouseHovering && _tooltip.Length > 0)
            {
                Main.HoverItem = new Item();
                Main.hoverItemName = _tooltip;
            }
        }

        // public void AddCheck()
        // {
        // spriteBatch.Draw(checkmarkTexture, pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        // }

        // public void RemoveCheck()
        // {
        // spriteBatch.Draw("", pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        // }

        public override int CompareTo(object obj)
        {
            UiCheckbox other = obj as UiCheckbox;
            return Order.CompareTo(other.Order);
        }
    }
}