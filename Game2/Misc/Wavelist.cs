using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game2.Objects;

namespace Game2.Misc
{
    class Wavelist
    {
        private int numWaves;
        private float elapsedWave;
        private Queue<Wave> waves = new Queue<Wave>();//List of waves needed to spawn 
        private bool waveFinished = false;//Bool value to check if the next wave needs to be spaawned
        private Texture2D enemyTexture;
        private Level level;
        public Wave CurrentWave//Methods get the current wave
        {
            get { return waves.Peek(); }
        }
        public Queue<Wave> Waves
        {
            get { return waves; }
        }
        public List<Enemy>Enemies
        {
            get { return CurrentWave.Enemies; }
        }
        public int Round
        {
            get { return CurrentWave.waveNumber + 1; }
        }
        public int NumWaves
        {
            get { return numWaves; }
        }
        private void nextWave()//Method loads the next wave
        {
            if(waves.Count > 0)//If there is still waves spawn 
            {
                waves.Peek().Start();//Starts the wave
                elapsedWave = 0;
                waveFinished = false;
            }
        }
        public Wavelist(Player player, Level level, int numWaves, Texture2D enemyTexture)//Constructor
        {
            this.numWaves = numWaves;//Initalises variables
            this.level = level;
            this.enemyTexture = enemyTexture;
            for(int i = 0; i < numWaves; i++)//For loop constructs each wave and increments the difficulty by the number of waves 
            {
                int initialNum = 5;
                int numMod = (i / 4) + 1;
                Wave wave = new Wave(i, initialNum * numMod, level, enemyTexture, player);//Creates a new instance of wave 
                waves.Enqueue(wave);//Adds the wave to the wave list
            }
            nextWave();//Starts the first wave by calling the nextWave method
        }
        public void Update(GameTime gameTime)//Update method
        {
            CurrentWave.Update(gameTime);//Updates the current wave
            if(CurrentWave.waveOver)//if the wave is over
            {
                waveFinished = true;//the Local waveFinished variable is set to true
            }
            if(waveFinished)//If the wave is finished the time the wave has been elapsed is incremented
            {
                elapsedWave += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if(elapsedWave > 1.0f)//If the time wave has been elapsed is more than 1 second the next wave is removed from the list 
            {                      //and the next wave is started
                waves.Dequeue();
                nextWave();
            }
        }
        public void Draw(SpriteBatch spriteBatch)//Draw method, draws the current wave
        {
            CurrentWave.Draw(spriteBatch);
        }
    }
}
