using Game2.Objects;
using Game2.Misc;
using Game2.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace Game2
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Wavelist waveList;
        Level level = new Level();
        aUI ui;
        Buttonadder playButton;
        Buttonadder hardButton;
        Buttonadder mediumButton;
        Buttonadder easyButton;
        Buttonadder resetButton;
        Buttonadder resetButton1;

        enum GameState
        {
            MainMenu,
            Playing,
            DifficultySelect,
            Lost,
            Win
        }
        GameState CurrentGameState = GameState.MainMenu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = level.getWidth * 64;
            graphics.PreferredBackBufferHeight = level.getHeight * 64 + 64;
            graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D background = Content.Load<Texture2D>("ground");
            Texture2D path = Content.Load<Texture2D>("path");
            Texture2D towerTexture = Content.Load<Texture2D>("defaulttower");
            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");
            Texture2D button = Content.Load<Texture2D>("button");
            Texture2D button1 = Content.Load<Texture2D>("button");
            Texture2D button2 = Content.Load<Texture2D>("button");
            Texture2D button3 = Content.Load<Texture2D>("button");
            Texture2D UIbar = Content.Load<Texture2D>("toolbar");
            Texture2D slowtower = Content.Load<Texture2D>("slowtower");
            SpriteFont font = Content.Load<SpriteFont>("Arial");
            player = new Player(level, towerTexture, bulletTexture,slowtower);
            playButton = new Buttonadder(button, graphics.GraphicsDevice, font, 100, 100, "Play", new Vector2(140,120));
            hardButton = new Buttonadder(button1, graphics.GraphicsDevice, font, 100, 100, "Hard", new Vector2(150, 120));
            mediumButton = new Buttonadder(button2, graphics.GraphicsDevice, font, 100, 240, "Medium", new Vector2(150, 260));
            easyButton = new Buttonadder(button3, graphics.GraphicsDevice, font, 100, 380, "Easy", new Vector2(150, 400));
            resetButton = new Buttonadder(button1, graphics.GraphicsDevice, font, 100, 100, "You lost! press here to go to the Main Menu", new Vector2(140, 120));
            resetButton1 = new Buttonadder(button1, graphics.GraphicsDevice, font, 100, 100, "You win! press here to go to the Main Menu", new Vector2(140, 120));

            ui = new aUI(UIbar, font, new Vector2(0, level.getHeight * 64 + 35));
            level.appendTexture(background);
            level.appendTexture(path);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    playButton.Update(gameTime);
                    if (playButton.isClicked == true)
                        CurrentGameState = GameState.DifficultySelect;
                        playButton.Update(gameTime);
                    break;
                case GameState.DifficultySelect:
                    hardButton.Update(gameTime);
                    mediumButton.Update(gameTime);
                    easyButton.Update(gameTime);
                    Texture2D enemyTexture = Content.Load<Texture2D>("enemy");
                    if (hardButton.isClicked == true)
                    {
                        CurrentGameState = GameState.Playing;
                        waveList = new Wavelist(player, level, 20, enemyTexture);
                    }
                    else if (mediumButton.isClicked == true)
                    {
                        CurrentGameState = GameState.Playing;
                        waveList = new Wavelist(player, level, 15, enemyTexture);
                    }
                    else if (easyButton.isClicked == true)
                    {
                        CurrentGameState = GameState.Playing;
                        waveList = new Wavelist(player, level, 10, enemyTexture);
                    }

                    break;
                case GameState.Playing:
                    if (player.Lives < 1)
                    {
                        CurrentGameState = GameState.Lost;
                        player.Update(gameTime, null);

                    }
                    else if (waveList.Waves.Count == 0)
                    {
                        player.Lives = 0;
                        CurrentGameState = GameState.Win;
                        player.Update(gameTime, null);

                    }
                    else
                    {
                        player.Update(gameTime, waveList.Enemies);
                        waveList.Update(gameTime);
                    }

                    break;
                case GameState.Lost:
                    resetButton.Update(gameTime);
                    if(resetButton.isClicked)
                    {

                        CurrentGameState = GameState.MainMenu;

                    }
                    break;
                case GameState.Win:
                    resetButton1.Update(gameTime);
                    if(resetButton1.isClicked)
                    {
                        CurrentGameState = GameState.MainMenu;

                    }
                    break;
            }
             base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            switch(CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, 800, 600), Color.White);
                    playButton.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case GameState.DifficultySelect:
                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, 800, 600), Color.White);
                    hardButton.Draw(spriteBatch);
                    mediumButton.Draw(spriteBatch);
                    easyButton.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case GameState.Playing:
                    spriteBatch.Begin();
                    level.Draw(spriteBatch);
                    if (waveList.Waves.Count > 0)
                    {
                        waveList.Draw(spriteBatch);
                        ui.Draw(spriteBatch, player, waveList);
                    }
                    player.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case GameState.Lost:
                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, 800, 600), Color.White);
                    resetButton.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case GameState.Win:
                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, 800, 600), Color.White);
                    resetButton1.Draw(spriteBatch);
                    spriteBatch.End();
                    break;

            }
            base.Draw(gameTime);
        }
    }
}
