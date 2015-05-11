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
    class AIShip : NPC
    {
        

        public void Update(GameTime theGameTime, int i, Player thePlayer)
        {
          //Calculate position first
            mVelocity += mAcceleration * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            if (mVelocity.Length() > Globals.maxSpeed)
            {
                mVelocity.Normalize();
                mVelocity *= Globals.maxSpeed;
            }
            mPosition += mVelocity * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            mPosition = Globals.wrapAround(mPosition);

            //now calculate orientation
            mRotation += Globals.AngleToVector(mAngularAcceleration * (float)theGameTime.ElapsedGameTime.TotalSeconds);
            if (mRotation.Length() > Globals.maxRotation)
            {
                mRotation.Normalize();
                mRotation *= Globals.maxRotation;
            }
            mOrientation += mRotation * (float)theGameTime.ElapsedGameTime.TotalSeconds;

            if (mBehaviour == 0)
                Blocking(theGameTime, thePlayer);
            if (mBehaviour == 1)
                Flee(theGameTime, thePlayer);
            if (mBehaviour == 2)
                Arrive(theGameTime, thePlayer);
            if (mBehaviour == 3)
                Blocking(theGameTime, thePlayer);

            else
            {
                mVelocity += mAcceleration;

                Vector2 temp = new Vector2();
                temp = mOrientation;
                if (mVelocity.Length() > 0)
                mOrientation = Vector2.Normalize(mVelocity);

                mPosition += mVelocity * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            }
            //do wrap-around if necessary
            if (mPosition.X > Globals.Universe.X) mPosition.X -= Globals.Universe.X;
            else if (mPosition.X < 0) mPosition.X += Globals.Universe.X;
            if (mPosition.Y > Globals.Universe.Y) mPosition.Y -= Globals.Universe.Y;
            else if (mPosition.Y < 0) mPosition.Y += Globals.Universe.Y;
        }

        public void Seek(GameTime theGameTime, Player thePlayer)
        {

            //Code that works
            Vector2 trajectory = thePlayer.mPosition - mPosition;

            if (trajectory.Length() < thePlayer.mRadius / 20.0f)
            {
                if (mVelocity.Length() > 0)
                    mOrientation = Vector2.Normalize(mVelocity);
                trajectory = Vector2.Zero;
                mVelocity = Vector2.Zero;
                mAcceleration = Vector2.Zero;
                
            }
            else
            {
                if((trajectory.Y > Globals.Universe.Y/2) || (-trajectory.Y > Globals.Universe.Y/2))
                {
                    trajectory.Y *= -1;
                }
                if ((trajectory.X > Globals.Universe.X / 2) || (-trajectory.X > Globals.Universe.X / 2))
                {
                    trajectory.X *= -1;
                }

                trajectory.Normalize();
                mAcceleration = trajectory * 100;
            }
        }

        public void Flee(GameTime theGameTime, Player thePlayer)
        {
            Vector2 trajectory = mPosition -thePlayer.mPosition;

            if ((trajectory.Y > Globals.Universe.Y / 2) || (-trajectory.Y > Globals.Universe.Y / 2))
            {
                trajectory.Y *= -1;
            }
            if ((trajectory.X > Globals.Universe.X / 2) || (-trajectory.X > Globals.Universe.X / 2))
            {
                trajectory.X *= -1;
            }

            if (trajectory.Length() < thePlayer.mRadius )
            {
                trajectory.Normalize();
                mAcceleration = trajectory * 300;
                
            }
            else
            {
                trajectory.Normalize();
                mAcceleration = trajectory * 25;
            }
        }

        public void Arrive(GameTime theGameTime, Player thePlayer)
        {

            Vector2 trajectory = thePlayer.mPosition - mPosition;
            Vector2 timeToTarget = trajectory / mVelocity;
            //float x = trajectory.Length();
            
            if(trajectory.Length() < thePlayer.mRadius / 5.0f)
            {
                trajectory = Vector2.Zero;
                mVelocity = Vector2.Zero;
                mAcceleration = Vector2.Zero;
            }
            else if(trajectory.Length() < thePlayer.mRadius / 3.0f)
            {
                trajectory = trajectory / 2;
            }
            else if (trajectory.Length() < thePlayer.mRadius)
            {
                //Vector2 trajectory = thePlayer.mPosition - mPosition;

                if ((trajectory.Y > Globals.Universe.Y / 2) || (-trajectory.Y > Globals.Universe.Y / 2))
                {
                    trajectory.Y *= -1;
                }
                if ((trajectory.X > Globals.Universe.X / 2) || (-trajectory.X > Globals.Universe.X / 2))
                {
                    trajectory.X *= -1;
                }

                trajectory.Normalize();
                mAcceleration = trajectory * 1;
            }
            else
            {
                Seek(theGameTime, thePlayer);
                
            }


        }
        public void Wandering(GameTime theGameTime, Player thePlayer)
        {
            Vector2 trajectory = thePlayer.mPosition - mPosition;
            trajectory.Normalize();
                

            //velocity=target.position - my.position
            //velocity=normalize( velocity)
            //orientation=setOrientation( velocity, orientation)
            //orientation=orientation + MaxRotation*random(-1,+1)
            //velocity=(-sin( orientation), cos( orientation))*maxSpeed

        }
        public void Blocking(GameTime theGameTime, Player thePlayer)
        {
            Vector2 trajectory = thePlayer.mPosition - mPosition;
            Vector2 timeToTarget = trajectory / mVelocity;
            

            if (trajectory.Length() < thePlayer.mRadius /2.0f)
            {
                trajectory = Vector2.Zero;
                mVelocity = Vector2.Zero;
                mAcceleration = Vector2.Zero;
            }
            else if (trajectory.Length() < thePlayer.mRadius)
            {
                //Vector2 trajectory = thePlayer.mPosition - mPosition;

                if ((trajectory.Y > Globals.Universe.Y / 2) || (-trajectory.Y > Globals.Universe.Y / 2))
                {
                    trajectory.Y *= -1;
                }
                if ((trajectory.X > Globals.Universe.X / 2) || (-trajectory.X > Globals.Universe.X / 2))
                {
                    trajectory.X *= -1;
                }

                trajectory.Normalize();
                mAcceleration = trajectory * 100;
            }
            else
            {
                Seek(theGameTime, thePlayer);
            }

        }

    }
}
