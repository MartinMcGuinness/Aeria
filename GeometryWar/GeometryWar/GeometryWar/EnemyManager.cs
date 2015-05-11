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

namespace GeometryWar
{
    class EnemyManager
    {

        public AIShip[] theShips = new AIShip[Globals.EnemyCount];
        public AIShip[] waveOne = new AIShip[Globals.EnemyCount];
        public AIShip[] waveTwo = new AIShip[Globals.EnemyCount];

        public int startingShips = 10;
        public int waveOneCount = 50;
        public int waveTwoCount = 50;


        public void Update(GameTime gameTime,   Player thePlayer, bool updateWaveOne, bool updateWaveTwo)
        {
            bool rocketCollideOne = false;
            bool rocketCollideTwo = false;
            bool rocketCollideThree = false;
            bool shipCollideOne = false;
            bool shipCollideTwo = false;
            bool shipCollideThree = false;




            for (int i = 0; i < startingShips; i++)
            {
                for (int j = 0; j < thePlayer.mRockets.Length; j++)
                {
                    rocketCollideOne = thePlayer.collisionsWithEnemy(thePlayer.mRockets[j], theShips[i]);
                    shipCollideOne = thePlayer.collisions(thePlayer, theShips[i]);

                    if (rocketCollideOne || shipCollideOne)
                    {
                        theShips[i].alive = false;
                        thePlayer.mRockets[i].alive = false;
                    }
                }
                if (!rocketCollideOne && theShips[i].alive == true)
                {
                    theShips[i].Update(gameTime, i, thePlayer);
                }
            }
            if (updateWaveOne)
            {
                for (int i = 0; i < waveOneCount; i++)
                {

                    for (int j = 0; j < thePlayer.mRockets.Length; j++)
                    {
                        rocketCollideTwo = thePlayer.collisionsWithEnemy(thePlayer.mRockets[j], waveOne[i]);
                        shipCollideTwo = thePlayer.collisions(thePlayer, waveOne[i]);

                        if (rocketCollideTwo || shipCollideTwo)
                        {
                            waveOne[i].alive = false;
                            thePlayer.mRockets[j].alive = false;
                        }
                    }
                    if (!rocketCollideTwo && waveOne[i].alive == true)
                    {
                        waveOne[i].Update(gameTime, i, thePlayer);
                        
                    }
                }
            }
            if (updateWaveTwo)
            {
                for (int i = 0; i < waveTwoCount; i++)
                {
                    for (int j = 0; j < thePlayer.mRockets.Length; j++)
                    {
                        rocketCollideThree = thePlayer.collisionsWithEnemy(thePlayer.mRockets[j], waveTwo[i]);


                        if (rocketCollideThree)
                        {
                            waveTwo[i].alive = false;
                            thePlayer.mRockets[j].alive = false;
                        }
                    }
                    if (!rocketCollideThree && waveTwo[i].alive == true)
                    {
                        waveTwo[i].Update(gameTime, i, thePlayer);
                        thePlayer.collisions(thePlayer, waveTwo[i]);
                    }
                }
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool updateWaveOne , bool updateWaveTwo)
        {
            for (int i = 0; i < startingShips; i++)
            {
                if (theShips[i].alive == true)
                {
                    theShips[i].Draw(spriteBatch);
                    Vector2 pos = new Vector2(theShips[i].mPosition.X + Globals.translation.X, theShips[i].mPosition.Y + Globals.translation.Y);
                    //spriteBatch.DrawString(font, theShips[i].mPosition.ToString(),pos, Color.White);
                }
            }

            if (updateWaveOne)
            {
                for (int i = 0; i < waveOneCount; i++)
                {
                    if (waveOne[i].alive == true)
                    {
                        waveOne[i].Draw(spriteBatch);
                        Vector2 pos = new Vector2(waveOne[i].mPosition.X + Globals.translation.X, waveOne[i].mPosition.Y + Globals.translation.Y);
                    }
                }
            }
            if (updateWaveTwo)
            {
                for (int i = 0; i < waveTwoCount; i++)
                {
                    if (waveTwo[i].alive == true)
                    {
                        waveTwo[i].Draw(spriteBatch);
                        Vector2 pos = new Vector2(waveTwo[i].mPosition.X + Globals.translation.X, waveTwo[i].mPosition.Y + Globals.translation.Y);
                    }
                }
            }
            

        }





        public void Init()
        {
            for (int i = 0; i < startingShips; i++)
            {
                theShips[i] = new AIShip();
            }
        }
        public void InitWaveOne()
        {
            for (int i = 0; i < waveOneCount; i++)
            {
                waveOne[i] = new AIShip();
            }
        }
        public void InitWaveTwo()
        {
            for (int i = 0; i < waveTwoCount; i++)
            {
                waveTwo[i] = new AIShip();
            }
        }

    }
}
