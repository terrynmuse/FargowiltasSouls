using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
    public class UiPageSelect : UIImageButton
    {
        private Texture2D _normal;
        private Texture2D _nope;
        internal string HoverText;
        private int _pag;
        private static bool _math;
        private int _max;

        public UiPageSelect(Texture2D normal, Texture2D nope, string hoverText) : base(normal)
        {
            _normal = normal;
            _nope = nope;
            Width.Set(_normal.Width, 0f);
            Height.Set(_normal.Height, 0f);
            HoverText = hoverText;
        }

        public static void ClickMe(UIMouseEvent evt, UIElement listeningElement, ref int page, bool add, int limit)
        {
            if (add)
            {
                if (page < limit)
                {
                    page++;
                }
            }
            else
            {
                if (page > limit)
                {
                    page--;
                }
            }
            if (page == limit)
            {
                _math = false;
            }
            else
            {
                _math = true;
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(_normal, dimensions.Position(), Color.White);

        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);
            Main.PlaySound(12);
        }
    }
}