using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GeometryWar
{
    class Rocket : Entity
    {
        public bool alive;
        public int duration;
        public int speed;

        public Rocket()
        {
            alive = false;
        }

        public void Fire(Vector2 position, Vector2 orientation)
        {
            alive = true;
            duration = 50;
            speed = 18;
            mPosition = position;
            mVelocity = (orientation * speed);
            mOrientation = orientation;
        }

        public void Update(GameTime theGameTime)
        {
            mPosition += mVelocity;
            if (mPosition.X > Globals.Universe.X) mPosition.X -= Globals.Universe.X;
            else if (mPosition.X < 0) mPosition.X += Globals.Universe.X;
            if (mPosition.Y > Globals.Universe.Y) mPosition.Y -= Globals.Universe.Y;
            else if (mPosition.Y < 0) mPosition.Y += Globals.Universe.Y;

            duration--;
            if (duration < 0)
            {
                alive = false;
                
            }
        }
    }
}
