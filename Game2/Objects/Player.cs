using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Game2.Objects
{
   public class Player//Player class is responsible for tower spawning and tracking the amount of money and lives the player has
    {
        private int lives = 5;
        private int money = 100;
        private List<Basetower> towers = new List<Basetower>();//Creates a list of the towers in the game
        private MouseState mouseState;//MouseState variables stores the state of the mouse at that time 
        private MouseState oldState;
        private Texture2D towerTexture;
        private Texture2D bulletTexture;
        private int cellX, cellY, tileX, tileY;
        public int Money//Method gets the current Money the player has
        {
            get { return money; }
            set { money = value; }
        }
        public int Lives//Methods gets the current Lives the player has
        {
            get { return lives; }
            set { lives = value; }
        }
        private Level level;//Creates a private level for the player class to work on 
        private Texture2D slowtowerTexture;

        public Player(Level level, Texture2D towerTexture, Texture2D bulletTexture, Texture2D slowtowerTexture)//Constructor for the player 
        {
            this.level = level;//Initialising variables
            this.towerTexture = towerTexture;
            this.bulletTexture = bulletTexture;
            this.slowtowerTexture = slowtowerTexture;
        }
        private bool isClear()//Method checks if the cell the player wants to spawn a tower on, is free
        {
            bool inBounds = cellX >= 0 && cellY >= 0 && cellX < level.getWidth && cellY < level.getHeight;//Bool variable checks if the mouse is in bounds of the screen
            bool Clear = true;
            foreach(Basetower tower in towers)//Loops through each tower in the list 
            {
                Clear = (tower.Position != new Vector2(tileX, tileY));//Checks if the place a player wants to spawn a tower has a tower on it already
                if (!Clear)
                    break;
            }
            bool onPath = (level.getIndex(cellX, cellY) != 1);//Checks if the position is on the path of the enemies or not
            return inBounds && Clear && onPath;//Returns the value as one bool
        }
        public void Update(GameTime gameTime, List<Enemy> enemies)//Update method
        {
            mouseState = Mouse.GetState();//Gets the current state of the mouse
            cellX = (int)(mouseState.X / 64);//We integer divide the mouseState to get a whole number then scale it up again, so we dont have a float for the position
            cellY = (int)(mouseState.Y / 64);
            tileX = cellX * 64;
            tileY = cellY * 64;
            if(lives < 1)
            {
                lives = 5;
                money = 100;
                towers.Clear();
            }
            if(mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)//if the left mouse button is clicked and released
            {
                if(isClear())//Calls the method to check if the area is clear 
                {
                    if (money < 50 )//Checks the players money 
                    {

                    }
                    else//If they have enough...
                    {
                        Defaulttower tower = new Defaulttower(towerTexture, bulletTexture, new Vector2(tileX, tileY));//A new tower is spawned at the tileX, tileY position
                        towers.Add(tower);//Adds the tower to the tower list
                        money -= tower.TowerCost;//Takes the money away from the player
                    }

                }
            }
            if (mouseState.RightButton == ButtonState.Released && oldState.RightButton == ButtonState.Pressed)//if the left mouse button is clicked and released
            {
                if (isClear())//Calls the method to check if the area is clear 
                {
                    if (money < 75)//Checks the players money 
                    {

                    }
                    else//If they have enough...
                    {
                        Slowtower tower = new Slowtower(slowtowerTexture, bulletTexture, new Vector2(tileX, tileY));//A new tower is spawned at the tileX, tileY position
                        towers.Add(tower);//Adds the tower to the tower list
                        money -= tower.TowerCost;//Takes the money away from the player
                    }

                }
            }
            foreach (Basetower tower in towers)//Loops through each tower in the tower list
            {
                if(tower.Target == null)//If the current tower has no target
                {
                    tower.getEnemy(enemies);//Calls the getEnemy Method
                }
                tower.Update(gameTime);
            }
            oldState = mouseState;//Resets the mouseState
        }
        public void Draw(SpriteBatch spriteBatch)//Draw method
        {
            foreach(Basetower tower in towers)//Loops through each tower, drawing it onto the map
            {
                tower.Draw(spriteBatch);
            }
        }
    }
}
