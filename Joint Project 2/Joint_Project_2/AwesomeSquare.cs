using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Joint_Project_2
{
    class AwesomeSquare
    {
        
        public int row = 3;
        public int col = 3;
        Texture2D image;
        Texture2D eyeImage;
        Vector2 eyePos;
        public int score;
        public int lives = 3;
        string name;
        bool alive = true;
        bool moveRight;
        bool moveLeft;
        bool moveUp;
        bool moveDown;
        int delayTimer;
        
        public int direction = 1;
        
        Vector2 playerPosition;
        

        public AwesomeSquare()
        {
          
            playerPosition.X = col + 40;
            playerPosition.Y = row + 40;
            
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            image = theContentManager.Load<Texture2D>("greenSquare");
            eyeImage = theContentManager.Load<Texture2D>("eye image");
            
            
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch theSpriteBatch)
        {

            //Player changes colour depending on his lifes
            if(alive == true)
            { 
                 

                if(lives == 3)
                {
                    theSpriteBatch.Draw(image, playerPosition, Color.White);
                }

                if (lives == 2)
                {
                    theSpriteBatch.Draw(image, playerPosition, Color.Orange);
                }
                if(lives == 1)
                {

                    
                    
                        theSpriteBatch.Draw(image, playerPosition, Color.OrangeRed);
                    
                }
            }
            //Eye despawns with the player
            if(alive == true)
            {
                theSpriteBatch.Draw(eyeImage, eyePos, Color.White);
            }
            

        }
        //Logic for getting the player to move in rows and collums giving it an arcade feel
        public void MoveDown(WorldSquare[,] Blocks)
        {
               KeyboardState keyboard = Keyboard.GetState();
           

           if (keyboard.IsKeyUp(Keys.Down))
           {
               moveDown = true;
               
              
           }
           if (keyboard.IsKeyDown(Keys.Down) && (moveDown == true))
           {
               if(Blocks[row + 1,col].containsSquare == false)
               {
                   row++;
                   moveDown = false;
               }
               direction = 3;

           }
          

            playerPosition.X = col * 40;
            playerPosition.Y = row * 40;

        }

        public void MoveUp(WorldSquare[,] Blocks)
        {
            KeyboardState keyboard = Keyboard.GetState();


            if (keyboard.IsKeyUp(Keys.Up))
            {
                moveUp = true;


            }
            if (keyboard.IsKeyDown(Keys.Up) && (moveUp == true))
            {
                if(Blocks[row - 1,col].containsSquare == false)
                {
                    row--;
                    moveUp = false;
                    
                }

                direction = 1;

            }

            playerPosition.X = col * 40;
            playerPosition.Y = row * 40;

        }
        public void MoveRight(WorldSquare[,] Blocks)
        {
            KeyboardState keyboard = Keyboard.GetState();


            if (keyboard.IsKeyUp(Keys.Right))
            {
                moveRight = true;

            }
            if (keyboard.IsKeyDown(Keys.Right) && (moveRight == true))
            {
                if(Blocks[row,col + 1].containsSquare == false)
                {
                    col++;
                    moveRight = false;
                }


                direction = 2;   

            }


            playerPosition.X = col * 40;
            playerPosition.Y = row * 40;

        }
        public void MoveLeft(WorldSquare[,] Blocks)
        {
            KeyboardState keyboard = Keyboard.GetState();


            if (keyboard.IsKeyUp(Keys.Left))
            {
                moveLeft = true;

            }
            if (keyboard.IsKeyDown(Keys.Left) && (moveLeft== true))
            {
                if(Blocks[row,col - 1].containsSquare == false)
                {
                    col--;
                    moveLeft = false;
                }

                direction = 4;
            }


            playerPosition.X = col * 40;
            playerPosition.Y = row * 40;

        }

        public void BoundaryChecking()
        {
            playerPosition.X = MathHelper.Clamp(playerPosition.X, 0,  480 - image.Width);
            playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 0, 480 - image.Height);
        }
        
        public void MoveEye()
        {

            // TO Calculate the eye position depnding on which way the player is facing
            if(direction == 1)
            {
                eyePos.X = playerPosition.X + (image.Width / 2) - (eyeImage.Width / 2);
                eyePos.Y = playerPosition.Y + (eyeImage.Height/2);
            }
            if (direction == 2)
            {
                eyePos.X = playerPosition.X + (image.Width ) - (eyeImage.Width + eyeImage.Width/2 );
                eyePos.Y = playerPosition.Y + +(image.Width / 2) - (eyeImage.Width / 2);
            }
            if (direction == 3)
            {
                eyePos.X = playerPosition.X + (image.Width / 2) - (eyeImage.Width / 2);
                eyePos.Y = playerPosition.Y + image.Height - (eyeImage.Height + eyeImage.Height/2);
            }
            if (direction == 4)
            {
                eyePos.X = playerPosition.X + (eyeImage.Width / 2);
                eyePos.Y = playerPosition.Y + +(image.Width / 2) - (eyeImage.Width / 2);
            }
        }

        public void Kick(WorldSquare[,] maze)
        {
                
            KeyboardState keyboard = Keyboard.GetState();
            //Moves the block in the direction player is facing... however it cant move the outermost blocks
            if(direction == 1 && keyboard.IsKeyDown(Keys.Space) && row > 1)
            {
                maze[row - 1,col].containsSquare = false;
                maze[row - 2, col].containsSquare = true;
                
            }
            if (direction == 2 && keyboard.IsKeyDown(Keys.Space) && col < 10)
            {
                maze[row, col + 1].containsSquare = false;
                maze[row , col + 2].containsSquare = true;

            }
            if (direction == 3 && keyboard.IsKeyDown(Keys.Space) && row < 10)
            {
                maze[row + 1 , col].containsSquare = false;
                maze[row + 2, col].containsSquare = true;

            }
            if (direction == 4 && keyboard.IsKeyDown(Keys.Space) && col > 1)
            {
                maze[row , col - 1].containsSquare = false;
                maze[row, col - 2].containsSquare = true;

            }
        


        }

       
        public void Die(EvilSquares enemy, int round)
        {

            //Collision detection between the player and enemy
            if (enemy.row == row + 1 && enemy.col == col && enemy.direction == 1
                || enemy.row == row - 1 && enemy.col == col && enemy.direction == 3
                || enemy.row == row && enemy.col == col + 1 && enemy.direction == 4
                || enemy.row == row && enemy.col == col - 1 && enemy.direction == 2)
            {
                alive = false;
                if(lives>0)
                {
                    lives--; //Stops decremtnting at 0
                    if(score > 0)
                    {
                        score = score - 2;
                    }
                }
                
                
            }
        }

        public void Respawn( Random agen)
        {
            //Randomly respawns the player at a few pre set locations
            if(alive == false && lives > 0)
            {
               
                {
                    int respawn;

                    respawn = agen.Next(5);

                    if (respawn == 1)
                    {
                        row = 8;
                        col = 3;
                        

                    }
                    else if (respawn == 2)
                    {
                        row = 10;
                        col = 8;
                        
                    }
                    else if (respawn == 3)
                    {
                        row = 5;
                        col = 9;
                        

                    }
                    else if (respawn == 4)
                    {
                        row = 1;
                        col = 5;
                        
                    }
                    else 
                    {
                        row = 10;
                        col = 1;
                        

                    }
                    
                    alive = true;
                    //delayTimer = 0;
                }
            }
        }

        public void Save (object sender, EventArgs e)
        {
            try
            {
               // XmlTextWriter objXmlTextwriter =  new
            }
            catch
            {

            }
        }
        public void LoadFromFile(string aline)
        //Reloads data from file
        {
            string[] words;

            words = aline.Split(',');

            playerPosition.X = Convert.ToInt32(words[0]);
            playerPosition.Y = Convert.ToInt32(words[1]);

            name = Convert.ToString(words[3]);
            lives = Convert.ToInt32(words[4]);
            score = Convert.ToInt32(words[5]);

            moveUp = Convert.ToBoolean(words[6]);
            moveDown = Convert.ToBoolean(words[7]);
            moveLeft = Convert.ToBoolean(words[8]);
            moveRight = Convert.ToBoolean(words[9]);
            alive = Convert.ToBoolean(words[10]);
            direction = Convert.ToInt32(words[11]);



            
        }
    }
}
