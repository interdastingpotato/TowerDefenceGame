using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game2.Objects
{
    public class Bullet : Sprite//Inherits from Sprite Class
    {
        private int damage, age, speed;
        public int Damage
        {
            get { return damage; }
        }
        public bool isDead()
        {
            return age > 100;
        }
        public Bullet(Texture2D texture, Vector2 position, float rotation, int speed, int damage ):base(texture, position)//Constructor
        {
            this.rotation = rotation;//Initialises variables
            this.damage = damage;
            this.speed = speed; 
        }
        public void kill()//Method sets age to 200, therefore killing it
        {
            this.age = 200;
        }
        public override void Update(GameTime gameTime)//Update method
        {
            age++;//Increases age
            position += velocity;//Increases velocity
            base.Update(gameTime);
        }
        public void setRotation(float value)//This method allows the bullet to adjust itself mid-path to hit the enemy
        {
            rotation = value;
            velocity = Vector2.Transform(new Vector2(0, -speed), Matrix.CreateRotationZ(rotation));
        }
    }
}
