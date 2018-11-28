using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Objects
{
   public class Sprite
    {
        protected Texture2D texture;//Protected variables means the child class can only access them 
        protected Vector2 position, velocity,centre,origin;
        protected float rotation;
        public Vector2 Position//Method gets the position
        {
            get { return position; }
        }
        public Vector2 Centre//Methods gets the centre 
        {
            get { return centre; }
        }
        public Sprite(Texture2D texture, Vector2 position)//Sprite Method
        {
            this.texture = texture;//Intialises texture
            this.position = position;//Initialises position
            centre = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);//Centre is the midpoint of the sprite + the current position
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }
        public virtual void Update(GameTime gameTime)//This method uses GameTime as a parameter, therefore the method will update every frame
        {
            this.centre = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);//Updates the centre of the current sprite
        }
        public virtual void Draw(SpriteBatch spriteBatch)//Draw method, using SpriteBatch
        {
            spriteBatch.Draw(texture, centre, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);//Will draw the sprite with the correct properties
        }
        
         //Virtual methods are methods that can be overriden by the child if the conditions aren't good enough 
    }
}
