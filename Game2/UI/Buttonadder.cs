using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Game2.UI
{
    class Buttonadder
    {
        Texture2D buttonTexture;
        private SpriteFont font;
        private Vector2 textPosition;
        Rectangle rectangle;
        private MouseState mouseState;
        private MouseState oldState;
        private int posX, posY;
        private string buttonText = "";
        Color colour = new Color(255, 255, 255, 255);
        public Buttonadder(Texture2D texture, GraphicsDevice graphics, SpriteFont font, int posX, int posY,string buttonText, Vector2 textPosition)
        {
            buttonTexture = texture;
            this.font = font;
            this.textPosition = textPosition;
            this.posX = posX;
            this.posY = posY;
            this.buttonText = buttonText;
        }
        public bool isClicked;
        public void Update(GameTime gameTime)
        {
            oldState = mouseState;
            mouseState = Mouse.GetState();
            rectangle = new Rectangle(posX, posY, 300, 50);
            Rectangle mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
            if(mouseRectangle.Intersects(rectangle))
            {
               if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)
                    isClicked = true;
            }
            else 
            {
                 isClicked = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, rectangle, colour);
            string text = string.Format(buttonText);
            spriteBatch.DrawString(font, text, textPosition, Color.Black);
        }
    }
}
