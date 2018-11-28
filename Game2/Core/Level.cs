using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2
{
    public class Level
    {
        public Level()//Constructor 
        {

        }
       public int[,] levelLayout = new int[,]//The levelLayout variable creates a 2D array using 1's and 0's 
        {
            {0,0,1,0,0,0,0,0,},
            {0,0,1,0,0,0,0,0,},
            {0,0,1,1,1,0,0,0,},
            {0,0,0,0,1,0,0,0,},
            {0,0,0,0,1,0,0,0,},
            {0,0,0,0,1,0,0,0,},
            {0,0,0,0,1,1,0,0,},
            {0,0,0,0,0,1,1,0,},
        };
        private List<Texture2D> levelTexture = new List<Texture2D>();//Creates a texture list to store loaded textures from the Game1 class
        public void appendTexture(Texture2D texture)//Method allows textures from the Game1 class to be added to the levelTexture list
        {
            levelTexture.Add(texture);
        }
        public int getHeight//Method allows the height of the array created to be returned 
        {
            get { return levelLayout.GetLength(0); }
        }
        public int getWidth//Method allows the width of the array to be returned 
        {
            get { return levelLayout.GetLength(1); }
        }
        public void Draw(SpriteBatch spriteBatch)//Draw methods uses Monogames SpriteBatch to draw sprites onto the screen
        {
            for (int x = 0; x < getWidth; x++) // A nested for loop allows each element in the array to considered
            {
                for (int y = 0; y < getHeight; y++)
                {
                    int texturePosition = levelLayout[y, x];//The texture position is stored in an index of the array
                    if (texturePosition == -1)//Error handler 
                        continue;
                    Texture2D texture = levelTexture[texturePosition];//depending on the index the appropriate texture is loaded, the order the textures are loaded is important
                    spriteBatch.Draw(texture, new Rectangle(x * 64, y * 64, 100, 100), Color.White);//Draws the texture as a rectangle at the co-ordinates 
                }
            }
        }
        public int getIndex(int cellX, int cellY)//Method fetches the current index for the player class
        {
            if (cellX < 0 || cellX > getWidth - 1 || cellY < 0 || cellY > getHeight - 1)//Error handler for mouse positions outside the array
                return 0;
            return levelLayout[cellY, cellX];
        }
        public int getwaypointIndex(int x, int y)
        {
            return levelLayout[ y, x];
        }
    }
}
