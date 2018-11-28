using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game2.Objects;
using Game2.Misc;



namespace Game2.UI
{
    class aUI
    {
        private Texture2D UItexture;
        private SpriteFont UIfont;
        private Vector2 UIposition;
        private Vector2 UItextPosition;
        public aUI(Texture2D UItexture, SpriteFont UIfont, Vector2 UIposition)
        {
            this.UItexture = UItexture;
            this.UIfont = UIfont;
            this.UIposition = UIposition;
            UItextPosition = new Vector2(130, UIposition.Y + 10);
        }
        public void Draw(SpriteBatch spriteBatch, Player player, Wavelist waveList)
        {
            spriteBatch.Draw(UItexture, UIposition, Color.White);
            string text = string.Format("Gold : {0} Lives : {1} Wave number : {2} Number of waves: {3}", player.Money, player.Lives, waveList.Round, waveList.NumWaves);
            spriteBatch.DrawString(UIfont, text, UItextPosition, Color.White);
        }
    }
}
