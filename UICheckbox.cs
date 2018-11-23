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
        public static Texture2D CheckboxTexture;
        private readonly bool _clickable;
        private bool _selected = true;
        private readonly string _test;
        private readonly string _tooltip;
        public Color Color;
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once NotAccessedField.Global
        public Color Olor;

        private const float ORDER = 0;

        public UiCheckbox(string text, string tooltip, Color main, Color threed, bool clickable = true, float textScale = 1, bool large = false) : base("", textScale, large)
        {
            Color = main;
            Olor = threed;
            _tooltip = tooltip;
            _clickable = clickable;
            _test = "   " + text;
            SetText("   ");
            // ReSharper disable once VirtualMemberCallInConstructor
            Recalculate();
        }

        private bool Selected
        {
            get { return _selected; }
            set
            {
                if (value == _selected) return;
                _selected = value;
                OnSelectedChanged?.Invoke(this, EventArgs.Empty);
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
            // Vector2 three = new Vector2(innerDimensions.X + 2, innerDimensions.Y - 3); //the positioning of the 3d part

            spriteBatch.Draw(CheckboxTexture, pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            base.DrawSelf(spriteBatch);
            //Utils.DrawBorderString(spriteBatch, this.test, three, this.olor, 1f, 0f, 0f, -1); the 3d part
            Utils.DrawBorderString(spriteBatch, _test, pos, Color);
            
            if (!IsMouseHovering || _tooltip.Length <= 0) return;
            
            Main.HoverItem = new Item();
            Main.hoverItemName = _tooltip;
        }

        public override int CompareTo(object obj)
        {
            UiCheckbox other = obj as UiCheckbox;
            return ORDER.CompareTo(ORDER);
        }
    }
}