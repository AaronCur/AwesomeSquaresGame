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
    class WorldSquare
    {
        public bool containsSquare;
        int row; 
        int col;
        Texture2D worldSqImage;
        Texture2D emptySqImage;
        Texture2D mSpriteTexture;
        SpriteFont font;
        Vector2 position = new Vector2();

        public WorldSquare(int row, int col)
        {
            position = new Vector2(row, col);
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            worldSqImage = theContentManager.Load<Texture2D>("blueSquare");
            emptySqImage= theContentManager.Load<Texture2D>("emptySquare");

        
        }

        public void Update()
        {
          // setting the wall texture
            if(containsSquare == true)
            {
                mSpriteTexture = worldSqImage;
            }
            else
            {
                mSpriteTexture = emptySqImage;
            }
            

        }

        public void Draw(SpriteBatch theSpriteBatch, AwesomeSquare player)
        {
            
           
            
               
            //Depending on the players score the map gets darker when the enemies become faster and more difficult to kill   
               
            if(  player.score >= 0 && player.score < 3)
            {
                theSpriteBatch.Draw(mSpriteTexture, position, Color.White);
            }


            else if ( player.score >= 3 && player.score < 5)
            {
                theSpriteBatch.Draw(mSpriteTexture, position, Color.LightSalmon);
            }
            else if ( player.score >= 5 && player.score < 7)
            {
                theSpriteBatch.Draw(mSpriteTexture, position, Color.Salmon);
            }
            else if(player.score >= 7 && player.score < 9)
            {
                theSpriteBatch.Draw(mSpriteTexture, position, Color.OrangeRed);
            }
            else if(player.score >= 9)
            {
                theSpriteBatch.Draw(mSpriteTexture, position, Color.Red);
            }
                   
             
           
                
            
             
           

        }

       

    }   
}
