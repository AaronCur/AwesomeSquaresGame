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
    class EvilSquares
    {
        public int row;
        public int col;
        public int direction = 1;
        Texture2D image;
        public bool alive = true;
        Vector2 position;
        int prevDirection = 2;

        public EvilSquares(Random agen, WorldSquare [,] block)
        {

            //Random generator for the initial spawn of the enemies
            int spawn = agen.Next(10);
            if (spawn == 1)
            {
                row = 1;
                col = 2;


            }
            else if (spawn == 2)
            {
                row = 10;
                col = 1;

            }
            else if (spawn == 3)
            {
                row = 10;
                col = 10;


            }
            else if (spawn == 4)
            {
                row = 2;
                col = 10;

            }
            else if (spawn == 5)
            {
                row = 8;
                col = 1;

            }
            else if (spawn == 6)
            {
                row = 4;
                col = 1;

            }
            else if (spawn == 7)
            {
                row = 1;
                col = 7;

            }
            else if (spawn == 8)
            {
                row = 6;
                col = 1;

            }
            else if (spawn == 9)
            {
                row = 7;
                col = 7;

            }
            else
            {
                row = 10;
                col = 5;


            }
           
                position.X = col * 40;
                position.Y = row * 40;
            
           
            
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            image = theContentManager.Load<Texture2D>("redSquare");
            
        }
        public void Draw(SpriteBatch theSpriteBatch)
        {
            if ( alive == true)
            {
                theSpriteBatch.Draw(image, position, Color.White);
            }
            
        }
        public void Move(WorldSquare[,] Blocks, Random aGen)
        {
            
           //Some crazy ai code ... basially a bunch of senarios so the enemie moves clockwise .. 
           // if it hits a wall it goes anti clockwise
        //also sets the direction the oppoiste way if hits a dead end .. it retreats
          
            if(Blocks[row +1,col].containsSquare == true && 
               Blocks[row - 1, col].containsSquare == true && Blocks[row,col-1].containsSquare == true )
            {
                direction = 4;
                prevDirection = 4;
            }
            else if(Blocks[row +1,col].containsSquare == true && 
               Blocks[row - 1, col].containsSquare == true && Blocks[row,col+1].containsSquare == true)
            {
                direction = 2;
                prevDirection = 2;
            }
            else if (Blocks[row -1, col].containsSquare == true &&
                     Blocks[row , col -1].containsSquare == true && Blocks[row, col + 1].containsSquare == true)
            {
                direction = 1;
                prevDirection = 1;
            }
            else if (Blocks[row, col - 1].containsSquare == true &&
                     Blocks[row +1, col].containsSquare == true && Blocks[row, col + 1].containsSquare == true )
            {
                direction = 3;
                prevDirection = 3;
            }

            else if(Blocks[row +1,col].containsSquare == false && prevDirection ==2
                || Blocks[row + 1, col].containsSquare == false && prevDirection == 4)
            {
                direction = 1;
                prevDirection = direction;
            }
            else if (Blocks[row, col + 1].containsSquare == false && prevDirection == 1
                     || Blocks[row, col + 1].containsSquare == false && Blocks[row - 1, col].containsSquare == true && prevDirection == 3)
            {
                direction = 4;
                prevDirection = direction;
            }
            else if (Blocks[row -1, col].containsSquare == false && prevDirection == 4
                     || Blocks[row - 1, col].containsSquare == false && prevDirection == 2)
            {
                direction = 3;
                prevDirection = direction;
            }
             else if (Blocks[row , col - 1].containsSquare == false && prevDirection == 3
                      || Blocks[row, col - 1].containsSquare == false && prevDirection == 1)
            {
                direction = 2;
                prevDirection = direction;
            }
            
            

            if (direction == 1)
            {
                row++;
            }
            if (direction == 2)
            {
                col--;

            }
            if (direction == 3)
            {
                row--;

            }
            if (direction == 4)
            {
                col++;
            }

            
          
           
           
            
            

            position.X = col * 40;
            position.Y = row * 40;
           
            
        }

        public void BoundaryChecking(WorldSquare[,] maze,AwesomeSquare player)
        {
            if(maze[row,col].containsSquare == true)
            {
                alive = false;
                player.score++;
            }
        }
        //Again my random but not so random spawning system that respawns the enemies at preset locations
        public void Die(Random agen, WorldSquare[,] maze)
        {
            if (alive == false)
            {

                int respawn;

                respawn = agen.Next(10);
                if (respawn == 1)
                {
                    if(maze[1,2].containsSquare == false)
                    {
                        row = 1;
                        col = 2;
                    }
                   
                }
                else if (respawn == 2)
                {
                    if(maze[10,1].containsSquare == false)
                    {
                        row = 10;
                        col = 1;
                    }

                }
                else if (respawn == 3)
                {
                    if (maze[10, 10].containsSquare == false)
                    {
                        row = 10;
                        col = 10;
                    }
                   


                }
                else if (respawn == 4)
                {
                    if (maze[2, 10].containsSquare == false)
                    {
                        row = 3;
                        col = 5;
                    }

                   

                }
                else if (respawn == 5)
                {
                    if (maze[8, 1].containsSquare == false)
                    {
                        row = 8;
                        col = 1;
                    }



                }
                else if (respawn == 6)
                {
                    if (maze[4, 1].containsSquare == false)
                    {
                        row = 4;
                        col = 1;
                    }



                }
                else if (respawn == 7)
                {
                    if (maze[1, 7].containsSquare == false)
                    {
                        row = 1;
                        col = 7;
                    }



                }
                else if (respawn == 8)
                {
                    if (maze[6, 1].containsSquare == false)
                    {
                        row = 6;
                        col = 1;
                    }



                }
                else if (respawn == 9)
                {
                    if (maze[7, 7].containsSquare == false)
                    {
                        row = 7;
                        col = 7;
                    }



                }
                else
                {
                    if (maze[10, 5].containsSquare == false)
                    {
                        row = 10;
                        col = 5;
                    }

                    


                }
                alive = true;
            }
        }
    }
}
