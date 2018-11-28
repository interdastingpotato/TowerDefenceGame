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
    class Wave
    {
        private int numEnemy, waveNum;
        private int enemySpawned = 0;
        private float spawnTimer = 0;
        private bool enemyEnd, actSpawn;
        private Level level;//Creates a private level for the wave manager to use 
        private Texture2D enemyTexture;
        public List<Enemy> enemies = new List<Enemy>();//Enemy list for wave to load
        private Queue<Vector2> waypoints = new Queue<Vector2>();//Waypoints passed to enemy class 
        private Player player;//Private player 
        public bool waveOver//Method checks if the wave is over by lookng at the enemy count and if there is enough enemies spawned 
        {
            get { return enemies.Count == 0 && enemySpawned == numEnemy; }
        }
        public int waveNumber//Method returns current wave number
        {
            get { return waveNum; }
        }
        public bool enemyAtEnd//Method gets the amount of enemies at the end
        {
            get { return enemyEnd; }
            set { enemyEnd = value;}
        }
        public List<Enemy> Enemies
        {
            get { return enemies; }
        }
        public Queue<Vector2> Waypoints
        {
            get { return waypoints; }
        }
        public Wave(int waveNum, int numEnemy, Level level, Texture2D enemyTexture, Player player )//Constructor
        {
            this.waveNum = waveNum;//Initalises variables
            this.numEnemy = numEnemy;
            this.level = level;
            this.enemyTexture = enemyTexture;
            this.player = player;
            constructWaypoints();//Calls waypoint method
        }
        public void constructWaypoints()//Method appends vectors to the waypoint list 
        {
           for(int x = 0; x < level.getWidth; x++)
                for(int y = 0; y < level.getHeight; y++)
                {
                   int waypointPos = level.getwaypointIndex(x, y);
                    if (waypointPos == -1)
                    {
                        continue;
                    }
                    else if(waypointPos == 1)
                    {
                        waypoints.Enqueue(new Vector2(x, y)*64);
                    }
                }
        }
        private void addEnemy()//Method adds enemy
        {
            if(waveNum == 3 || waveNum == 8 )
            {
                Enemy enemy = new Enemy(enemyTexture, waypoints.Peek(), 50, 50, 2.0f);//Creates an instance of enemy at the first waypoint
                enemy.setPoints(waypoints);//Passes the waypoint list to the enemy class
                enemies.Add(enemy);//Adds enemy to the enemy list
            }
            else if(waveNum == 10 || waveNum == 15 || waveNum == 20 )
            {
                Enemy enemy = new Enemy(enemyTexture, waypoints.Peek(), 1000, 100, 0.5f);//Creates an instance of enemy at the first waypoint
                enemy.setPoints(waypoints);//Passes the waypoint list to the enemy class
                enemies.Add(enemy);//Adds enemy to the enemy list
            }
            else
            {
                Enemy enemy = new Enemy(enemyTexture, waypoints.Peek(), 50, 25, 1.0f);//Creates an instance of enemy at the first waypoint
                enemy.setPoints(waypoints);//Passes the waypoint list to the enemy class
                enemies.Add(enemy);//Adds enemy to the enemy list
            }
            spawnTimer = 0;//Spawn timer is set back to zero to allow another enemy to spawn
            enemySpawned++;//Increments the enemySpawned variable by 1
        }
        public void Start()//Method allows wave to start spawning
        {
            actSpawn = true;
        }
        public void Update(GameTime gameTime)//Update method 
        {
            if (enemySpawned == numEnemy)//If the number of enemies spawned equals the number of enemies, no more enemies are spawned
                actSpawn = false;
            if(actSpawn)//If enemies need spawning 
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;//Timer is incremented by the amount of seconds that have passed since the last update
                if (spawnTimer > 1)//If more than 1 second has passed 
                    addEnemy();//Calls the addEnemy method 
            }
            for(int i = 0; i<enemies.Count;i++)//For each enemy in the list
            {
                Enemy enemy = enemies[i];//The current enemy 
                enemy.Update(gameTime);//Calls the update method in the enemy class
                if(enemy.IsDead)//If enemy is dead 
                {
                    if(enemy.CurrentHealth > 0)//If current health is bigger than 0, the enemy is considered to be at the end and the player loses lives
                    {
                        enemyAtEnd = true;
                        player.Lives -= 1;
                    }
                    else//If the enemy has no remaining health the player is given currency
                    {
                        player.Money += enemy.GoldGiven;
                    }
                    enemies.Remove(enemy);//We remove the current enemy from the list
                    i--;//Decrement the i value 
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)//Draw method
        {
            foreach (Enemy enemy in enemies)//Draws each enemy in the enemy list
                enemy.Draw(spriteBatch);
        }
    }
}
