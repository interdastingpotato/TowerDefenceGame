using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game2.Objects
{
   public class Defaulttower : Basetower//Inherits the Basetower class methods
    {
        public Defaulttower(Texture2D texture, Texture2D bulletTexture, Vector2 position):base(texture,bulletTexture,position)//Constructor calls from the parent class using :base
        {
            this.towerDamage = 15;//Initialises variables 
            this.towerCost = 50;
            this.towerRadius = 150;
        }
        public override void Update(GameTime gameTime)//Update method
        {
            if(bulletElapsed >= 0.75f && target != null)//If the bullet has existed for more than 0.75 and there is a target 
            {
                Bullet bullet = new Bullet(bulletTexture, Vector2.Subtract(centre, new Vector2(bulletTexture.Width / 2)), rotation, 6, towerDamage);//Creates a new instance of bullet in front of the tower
                bulletList.Add(bullet);//Adds the bullet to the bullet list 
                bulletElapsed = 0;//The time the bullet has existed is set back to 0 
            }
            for(int i = 0; i<bulletList.Count;i++)//For statement, 0 to the number of bullets in the list
            {
                Bullet bullet = bulletList[i];//Current bullet
                bullet.setRotation(rotation);//Sets the swerve of the bullet using a matrix
                bullet.Update(gameTime);//Updates the bullet
                if(!InRange(bullet.Centre))//If the bullet goes out of the tower radius 
                {
                    bullet.kill();//Kill the bullet
                }
                if(target!=null && Vector2.Distance(bullet.Centre, target.Centre) < 12)//If the there is a target an the distance between the bullets centre and 
                {                                                                      //target centre is below 12 
                    target.CurrentHealth -= bullet.Damage;//Current target loses health
                    bullet.kill();//Kill the bullet 
                }
                if(bullet.isDead())//If the bullet is dead 
                {
                    bulletList.Remove(bullet);//Removes the bullet from the bullet list
                    i--;//decrements i 
                }
            }
            base.Update(gameTime);
        }
    }
}
