using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Joint_Project_2
{
    /// <summary>
    ///Aaron Curry 
    ///C00202505
    ///Worked on : ... I forgot to record but it took a long time!
    ///bugs: One spawn location spawns one col more than it should to the right sometimes
    ///      When restarting the game the maze walls are loaded but textures are not
    ///Notes:With increased rounds the enemies move faster .. these rounds depend on the score
    ///      I have lowered the score needed for each round so its easier to demo
    ///      Player changes colour depnding on lifes left.
    ///      End screen has 90% opacity i was quite proud of this!
 
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Texture2D endScreen;
        Vector2 endScreenPos;
        const int MaxRows = 12;
        const int MaxCols = 12;
        const int SquareSize = 40;
        const int maxEnemies = 4;
        int x;
        int y;
        int timer = 0;
        int endTimer = 0;
        public int round = 0;
        int highRound = 0;
        int highScore = 0;
        string currentfile = @"g:\PlayerSave.txt";
        enum gameState { gameplay, fileSaving, fileLoading}
        gameState theGameState;
        string aMessage = "";
        bool reset = true;
        KeyboardState keyState = Keyboard.GetState();
       
        

        SpriteFont font;
        Random random = new Random();
        

        
        
        AwesomeSquare player;
        EvilSquares[] enemies = new EvilSquares [maxEnemies];
        WorldSquare[,] maze = new WorldSquare [MaxRows,MaxCols];


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 480;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           // WorldSquare[,] maze = new WorldSquare[MaxRows, MaxCols];

            random = new Random();

            player = new AwesomeSquare();
            for (int i = 0; i < maxEnemies; i++ )
            {
                enemies[i] = new EvilSquares(random, maze);
            }
                
            
            //Initialise the map array
            for (int row = 0; row < MaxRows; row++)
            {
                for (int col = 0; col < MaxCols; col++)
                {
                    maze[row, col] = new WorldSquare(x, y);
                    x = x + SquareSize;
                }
                x = 0;
                y = y + SquareSize;

            }


            //Grid of the map
            int[,] screen = { {1,1,1,1,1,1,1,1,1,1,1,1},
                               {1,1,0,0,0,0,0,0,1,1,1,1},
                               {1,0,0,1,1,1,1,0,0,0,0,1},
                               {1,1,0,0,0,0,1,1,1,1,0,1},
                               {1,0,0,1,1,0,0,0,0,1,0,1},
                               {1,1,0,1,1,0,1,1,0,0,0,1},
                               {1,0,0,0,1,0,1,1,1,1,1,1},
                               {1,1,1,0,1,0,0,0,0,0,0,1},
                               {1,0,0,0,1,0,1,1,1,0,0,1},
                               {1,0,1,0,1,0,0,0,1,1,0,1},
                               {1,0,1,0,0,0,1,0,0,0,0,1},
                               {1,1,1,1,1,1,1,1,1,1,1,1} };


            

            for (int col = 0; col < MaxCols; col++)
            {
                for (int row = 0; row < MaxRows; row++)
                {
                    if (screen[row, col] == 1)
                    {
                        maze[row,col].containsSquare = true;
                    }
                }
            }
           

            
                base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            

            // TODO: use this.Content to load your game content here
            player.LoadContent(this.Content, "greenSquare");
            endScreen = Content.Load<Texture2D>("White_square");
           
            font = Content.Load<SpriteFont>("SpriteFont1");

            for (int i = 0; i < maxEnemies; i++)
            {
                enemies[i].LoadContent(this.Content, "redSquare");
            }
            
               
            for(int row = 0; row < MaxRows; row ++)
            {
                for(int col = 0; col < MaxCols; col++)
                {
                    maze[row, col].LoadContent(this.Content, "blueSquare");
                    
                }
            }
            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //What keys are currently pressed
            keyState = Keyboard.GetState();

            // TODO: Add your update logic here

            //Restart the game
            if (keyState.IsKeyUp(Keys.Q))
            {
                reset = true;
            }
            if(keyState.IsKeyDown(Keys.Q) && reset == true&& player.lives == 0)
            {
                
                this.Initialize();
                
                reset = false;

            }
            if(keyState.IsKeyDown(Keys.S))
            {
                theGameState = gameState.fileSaving;
            }
            if(keyState.IsKeyDown(Keys.R))
            {
                theGameState = gameState.fileLoading;
               
            }
            //check state of the game
            if(theGameState == gameState.fileSaving)
            {
                SaveToFile();
                theGameState = gameState.gameplay;
            }
            else if (theGameState == gameState.fileLoading)
            {
                LoadFromFile();
                theGameState = gameState.gameplay;
            }
            else //Play Game
            {

                timer++;
                //Game proression
                if (player.score < 1)
                {
                    round = 1;
                    //Enemy moves every 3 seconds
                    if (timer == 180)
                    {
                        for (int i = 0; i < maxEnemies; i++)
                        {
                            enemies[i].Move(maze, random);
                        }

                        timer = 0;
                    }
                }
                if (player.score >= 1)
                {
                    round = 2;

                    if (timer == 120)
                    {
                        for (int i = 0; i < maxEnemies; i++)
                        {
                            enemies[i].Move(maze, random);
                        }

                        timer = 0;
                    }
                }
                if (player.score >= 3)
                {
                    round = 3;
                    if (timer == 80)
                    {
                        for (int i = 0; i < maxEnemies; i++)
                        {
                            enemies[i].Move(maze, random);
                        }

                        timer = 0;
                    }
                }
                if (player.score >= 5)
                {
                    round = 4;

                    if (timer == 60)
                    {
                        for (int i = 0; i < maxEnemies; i++)
                        {
                            enemies[i].Move(maze, random);
                        }

                        timer = 0;
                    }
                }
                if (player.score >= 7)
                {
                    round = 5;
                    if (timer == 45)
                    {
                        for (int i = 0; i < maxEnemies; i++)
                        {
                            enemies[i].Move(maze, random);
                        }

                        timer = 0;
                    }
                }
                if (player.score >= 9)
                {
                    round = 6;
                    if (timer == 30)
                    {
                        for (int i = 0; i < maxEnemies; i++)
                        {
                            enemies[i].Move(maze, random);
                        }

                        timer = 0;
                    }
                }




                for (int i = 0; i < maxEnemies; i++)
                {
                    enemies[i].Die(random, maze);
                }

                for (int i = 0; i < maxEnemies; i++)
                {
                    enemies[i].BoundaryChecking(maze, player);
                }


                for (int i = 0; i < maxEnemies; i++)
                {
                    player.Die(enemies[i], round);
                }

                player.Update();
                player.MoveLeft(maze);
                player.MoveRight(maze);
                player.MoveUp(maze);
                player.MoveDown(maze);
                player.Kick(maze);
                player.Respawn(random);
                player.MoveEye();


                for (int col = 0; col < MaxCols; col++)
                {
                    for (int row = 0; row < MaxRows; row++)
                    {

                        maze[row, col].Update();

                    }
                }
                //Logic for high score borad
                if (player.score > highScore)
                {
                    highScore = player.score;
                }
                if (round > highRound)
                {
                    highRound = round;
                }

                base.Update(gameTime);
            }


            }//end else play game

       

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            
            
            // TODO: Add your drawing code here
            for(int row = 0; row < MaxRows; row++)
            {
                for(int col = 0; col < MaxCols; col++)
                {
                    maze[row,col].Draw(this.spriteBatch, player);
                    

                }
            }
            player.Draw(this.spriteBatch);

            for (int i = 0; i < maxEnemies; i++)
            {
                enemies[i].Draw(this.spriteBatch);
            }
            

            spriteBatch.DrawString(font, round + ":ROUND:" + round , new Vector2(177, 5), Color.OrangeRed);
            spriteBatch.DrawString(font, "SCORE:" + player.score, new Vector2(10, 5), Color.DeepSkyBlue);
            spriteBatch.DrawString(font, player.lives +":LIVES " , new Vector2(370, 5), Color.LimeGreen);
            spriteBatch.DrawString(font, ": HIGHSCORE :", new Vector2(150, 450), Color.Yellow);
            spriteBatch.DrawString(font, "" + highScore, new Vector2(115, 450), Color.DeepSkyBlue);
            spriteBatch.DrawString(font, "" + highRound, new Vector2(355, 450), Color.OrangeRed);

            //End screen 
              if(player.lives == 0)
            {
                endTimer++;//Timer used to flash the message

                
                spriteBatch.Draw(endScreen, endScreenPos, Color.White);
                spriteBatch.DrawString(font, "GAME OVER" , new Vector2(180, 200), Color.Red);

                  if(endTimer >= 100)
                  {
                      spriteBatch.DrawString(font, "Press Q to Replay", new Vector2(120, 300), Color.Green);
                  }
                  if(endTimer == 200)
                  {
                      endTimer = 0;
                  }


            }





              GraphicsDevice.Clear(Color.White);

           
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void LoadFromFile()
            //reload the game from file
        {
            string line;
            try
            {
               // StreamReader inputStream = File.OpenText(currentfile);

                //line = inputStream.Readline();

                //Loads the player object from the file 
               // player.LoadFromFile(line);

               // inputStream.Close; // close file stream


            }
            catch
            {

            }
        }
        public void SaveToFile() // Save the game to a file
        {
            try
            {
               // System.IO.Stream = File.CreateText(currentfile);
               // player.WriteToFile(outputStream);
               // outputStream.Close();
            }
            catch
            {
               // aMessage = "Error occured with write to PlayerSave.txt. \n" + problem.Message;
            }
        }
    }
}
