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
    class Planet:Entity
    {
        public Planet()
        {
            mPosition = new Vector2(Globals.random.Next((int)Globals.Universe.X), Globals.random.Next((int)Globals.Universe.Y));
            mVelocity = Vector2.Zero;
            mOrientation = Vector2.Zero;
            mAcceleration = Vector2.Zero;
        }
    }
}
