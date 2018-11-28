using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Objects
{
    public class Basetower : Sprite//Inherits methods from Sprite class
    {
        protected int towerCost, towerDamage;
        protected float towerRadius;
        protected int i = 0;
        protected Enemy target;
        protected Texture2D bulletTexture;
        protected float bulletElapsed;
        protected List<Bullet> bulletList = new List<Bullet>();//Creates a list of bullets needed to spawn
        public Enemy Target
        {
            get { return target; }
        }
        public int TowerCost
        {
            get { return towerCost; }
        }
        public int TowerDamage
        {
            get { return towerDamage; }
        }
        public float TowerRadius
        {
            get { return towerRadius; }
        }
        public Basetower(Texture2D texture, Texture2D bulletTexture, Vector2 position):base(texture, position)//Constructor for tower
        {
            towerRadius = 1000;//sets radius of the tower to 1000 units
            this.bulletTexture = bulletTexture;//loads texture
        }
        public bool InRange(Vector2 position)//This method returns a boolean value if the target is in range of the tower
        {
            if (Vector2.Distance(centre, position) <= towerRadius)//Checks the distance between the center of enemy compared to the position of the tower
                return true;                                      //If the distance is below the tower radius returns true
            return false;
        }
        public bool findRange(Vector2 position)//Method physically returns the range
        {
            return Vector2.Distance(centre, position) <= towerRadius;
        }
        public void getEnemy(List<Enemy> enemies)//Method gets the nearest enemy using the enemy list as a parameter
        {
            target = null;
            List<float>minList = new List<float>();
            float minRange = towerRadius;
            foreach(Enemy enemy in enemies)
            {
                if(Vector2.Distance(centre,position) < minRange)
                {
                    minRange = Vector2.Distance(centre, enemy.Centre);
                    minList.Add(minRange);
                    float targetrange = minList.Min();
                    int targetrangeIndex = minList.IndexOf(targetrange);
                    target = enemies[targetrangeIndex + i];
                }
                else
                {
                    i += 1;
                }
            }
        }
        protected void initiatarget()//Finds the inital target of the tower
        {
            Vector2 direction = centre - target.Centre;//Finds the direction the tower will point towards
            direction.Normalize();
            rotation = (float)Math.Atan2(-direction.X, direction.Y);//Turns the tower towards the enemy
        }
        public override void Update(GameTime gameTime)//Update method
        {
            base.Update(gameTime);
            bulletElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;//Counts how long the bullet has been alive
            if(target != null)//If there is a target
            {
                initiatarget();//Calls the method
                if(!findRange(target.Centre)||target.IsDead)//If no range can be found or the target is dead
                {
                    target = null;//Resets everything 
                    bulletElapsed = 0;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)//Draw method
        {
            
            foreach (Bullet bullet in bulletList)//Draws each bullet in the bullet list
                bullet.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
