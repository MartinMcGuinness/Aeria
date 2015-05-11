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
    class Player : Entity
    {
        public int mHealth = 500;
        int currentRocket = 0;
        int rocketWait = 0;
        public Rocket[] mRockets = new Rocket[10];
        protected GamePadState previosGamepadState;


        public void InitRocket()
        {
            for (int i = 0; i < mRockets.Length; i++)
            {
                mRockets[i] = new Rocket();
            }
        }

        public void FireRocket(Vector2 aim)
        {
            mRockets[currentRocket].Fire(mPosition, aim);
            currentRocket++;
            if (currentRocket == mRockets.Length)
            {
                currentRocket = 0;
            }
        }

        public bool collisions(Entity Obj1, Entity Obj2)
        {
            float distanceFrom = Vector2.Distance(Obj1.mPosition, Obj2.mPosition);

            if (distanceFrom < 100)
            {
                mHealth -= 1;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool collisionsWithEnemy(Entity rocket, Entity enemy)
        {
            float distanceFrom = Vector2.Distance(rocket.mPosition, enemy.mPosition);

            if (distanceFrom < 50)
            {
                //mHealth += 100;
                
                return true;
            }
            else
            {
                return false;
            }

        }

        public void Update(GameTime theGameTime)
        {
            mVelocity.X *= 0.9f;
            mVelocity.Y *= 0.9f;
            mAcceleration = Vector2.Zero;

            GamePadState currentGamepadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

                   
            if ((currentGamepadState.ThumbSticks.Left.X < 0) || aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
            {
                mAcceleration.X = -1f;
                //mOrientation.X = -10f;
            }
            if ((currentGamepadState.ThumbSticks.Left.X > 0) || aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
            {
                mAcceleration.X = 1f;
            }
            if ((currentGamepadState.ThumbSticks.Left.Y > 0) || aCurrentKeyboardState.IsKeyDown(Keys.Up) == true)
            {
                mAcceleration.Y = -1f;
            }
            if ((currentGamepadState.ThumbSticks.Left.Y < 0) || aCurrentKeyboardState.IsKeyDown(Keys.Down) == true)
            {
                mAcceleration.Y = 1f;
            }
            if (rocketWait > 0)
            {
                rocketWait--;
            }

            if ((currentGamepadState.ThumbSticks.Right.X < 0) && rocketWait <= 0 || (currentGamepadState.ThumbSticks.Right.X > 0) && rocketWait <= 0 ||
                (currentGamepadState.ThumbSticks.Right.Y > 0) && rocketWait <= 0 || (currentGamepadState.ThumbSticks.Right.Y < 0) && rocketWait <= 0 || 
                aCurrentKeyboardState.IsKeyDown(Keys.Space) == true && rocketWait <= 0)
            {
                float aimX = currentGamepadState.ThumbSticks.Right.X;
                float aimY = currentGamepadState.ThumbSticks.Right.Y;
                Vector2 aim = new Vector2();
                aim.X = aimX;
                aim.Y = -aimY;

                FireRocket(aim);
                rocketWait = 12;
            }

            //update Rockets
            for (int i = 0; i < mRockets.Length; i++)
            {
                if (mRockets[i].alive == true)
                {
                    mRockets[i].Update(theGameTime);
                }
            }  

            mVelocity += mAcceleration;
            if(mVelocity.Length() > 0)
            mOrientation = Vector2.Normalize(mVelocity);
                
            mPosition += mVelocity;
            //do wrap-around if necessary
            if (mPosition.X > Globals.Universe.X) mPosition.X -= Globals.Universe.X;
            else if (mPosition.X < 0) mPosition.X += Globals.Universe.X;
            if (mPosition.Y > Globals.Universe.Y) mPosition.Y -= Globals.Universe.Y;
            else if (mPosition.Y < 0) mPosition.Y += Globals.Universe.Y;
   
        }


    }
}
