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
    public static class  Globals
    {
        public  static int EnemyCount = 100;
        //game area is nine times that of 800x600 screen!
        public  static Vector2 Universe = new Vector2(2400,1800);  
        //centre of viewable screen 800x600
        public static Vector2 Centre = new Vector2(400, 300);
        public static Vector2 radar = new Vector2(96, 72);
        public static int grainSize =25;
        //current translation to apply to place player in screen centre!
        public static Vector2 translation = Vector2.Zero;
        //translation for radar
        public static Vector2 radarTranslation = Vector2.Zero;
        public static Random random = new Random();
        public static float maxSpeed = 30;
        public static float maxRotation = 10;
        public static float scale = 0.3f;

        //The red Pixel texture object used for radar
        public static Texture2D radarBackground;
        public static Texture2D Background;
        public static Texture2D redPixel;
        public static Texture2D bluePixel;
        public static Texture2D whitePixel;
        public static Texture2D yellowPixel;

        public static Vector2 wrapAround(Vector2 pos)
        {
            //do wrap-around if necessary
            if (pos.X > Globals.Universe.X) pos.X -= Globals.Universe.X;
            else if (pos.X < 0) pos.X += Globals.Universe.X;
            if (pos.Y > Globals.Universe.Y) pos.Y -= Globals.Universe.Y;
            else if (pos.Y < 0) pos.Y += Globals.Universe.Y;
            return pos;
        }

        public static Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
        /// <summary>
        /// adds 90% to answer because in XNA 0 is up :-(
        /// </summary>
        /// <param name="vector">vector we need to convert</param>
        /// <returns>angle of vector in radians</returns>
        public static float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X)+(float)Math.PI/2;
        }
        
    }
}
