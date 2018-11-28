using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game2.Objects
{
    public class Enemy:Sprite //The Enemy class inherits from the Sprite class
    {
        protected float startHealth, currentHealth;//Health variables
        protected bool isAlive = true;//Boolean 
        protected float speed = 0.75f;
        protected int goldGiven;
        private Queue<Vector2> waypoints = new Queue<Vector2>();//Creates a queue for the waypoints to be read off
        public float CurrentHealth//Method returns current health and sets it to the current value
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public bool IsDead//Methods returns False 
        {
            get { return !isAlive; }
        }
        public int GoldGiven//Method returns the gold given after an enemy has died 
        {
            get { return goldGiven; }
        }
        public float distanceRemain//Method returns a distance as a vector between two points. The position is passed through the constructor and
        {                          //the waypoint is passed from the first waypoint in the queue at that time, which is fetched using the .Peek command
            get { return Vector2.Distance(position, waypoints.Peek());}
        }
        public Enemy(Texture2D texture, Vector2 position, float health, int goldGiven, float speed):base(texture, position)//Constructor for enemy, uses methods from sprite class
        {
            this.startHealth = health;//Initialises all variables for the enemy to be used by methods
            this.currentHealth = startHealth;
            this.goldGiven = goldGiven;
            this.speed = speed;
        }
        public override void Update(GameTime gameTime)//Update method
        {
            base.Update(gameTime);
            if (waypoints.Count > 0 && isAlive)//Checks if there is still waypoints remaining and that the enemy is alive
            {
                if (distanceRemain < speed)//Checks if we are near enough to the waypoint, so we can set the new waypoint. It enables for some tolerance.
                {
                    position = waypoints.Peek();
                    waypoints.Dequeue();//Removed waypoint
                }
                else
                {
                    Vector2 direction = waypoints.Peek() - position;//If the enemy has distance, the direction of the enemy is the next waypoint - the current position
                    direction.Normalize();//Normalising makes sure the enemy's vector is the same length, if diagonal 
                    velocity = Vector2.Multiply(direction, speed);//The velocity is then calculated from multiplying the direction and speed
                    position += velocity;//Velocity is added on to position
                }
            }
            else 
                 isAlive = false;
            if (currentHealth <= 0)
                isAlive = false;
               
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!isAlive)
            {
                base.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }
        public void setPoints(Queue<Vector2> waypoints)//This method intialises the waypoints from the Wave class
        {
            foreach (Vector2 waypoint in waypoints)//iterates through each waypoint
                this.waypoints.Enqueue(waypoint);//adds waypoint to the class specific waypoint queue 
            this.position = this.waypoints.Dequeue();
        }
     
    }
}
