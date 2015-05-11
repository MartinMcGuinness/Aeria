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

namespace GeometryWar
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        EnemyManager EnemyManager = new EnemyManager();

        //Create player and ships here
        Player thePlayer = new Player();

        Planet[] thePlanets = new Planet[50];
        //for score info
        SpriteFont font;
        public bool newWaveOne = false;
        public bool updateWaveOne = false;
        public bool newWaveTwo = false;
        public bool updateWaveTwo = false;
        public bool newWaveThree = false;
        public bool updateWaveThree = false;


        int twentySec =1200;
        int fortySec = 2400;
        int oneMinute = 3600;

        Texture2D healthBar;
        Rectangle healthRec;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //set preferred screen dimensions
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            //initialise all entities
            //thePlayer.mVelocity = new Vector2(2, 2);
            
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
            

            for (int i = 0; i < thePlanets.Length; i++)
            {
                thePlanets[i] = new Planet();
            }
            EnemyManager.Init();
            thePlayer.InitRocket();
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
            //load font
            font = Content.Load<SpriteFont>("MyFont");
            // TODO: use this.Content to load your game content here
            thePlayer.LoadContent(this.Content, "thumb_ship001", 0);
            Globals.radarBackground = Content.Load<Texture2D>("radarBackgroundLrg");
            Globals.Background = Content.Load<Texture2D>("star_field");
            Globals.redPixel=Content.Load<Texture2D>("redPixel");
            Globals.yellowPixel = Content.Load<Texture2D>("yellowPixel");
            Globals.bluePixel = Content.Load<Texture2D>("bluePixel");
            Globals.whitePixel = Content.Load<Texture2D>("whitePixel");

            healthBar = Content.Load<Texture2D>("PNG");


            for (int i = 0; i < EnemyManager.startingShips; i++)
            {
                EnemyManager.theShips[i].LoadContent(this.Content, "EnemyShip001", 0);
            }
            for (int i = 0; i < thePlanets.Length; i++)
            {
                thePlanets[i].LoadContent(this.Content, "Bubble", 0);
            }
            for (int i = 0; i < thePlayer.mRockets.Length; i++)
            {
                thePlayer.mRockets[i].LoadContent(this.Content, "new", 0);
            }
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
        int seconds;
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (seconds == twentySec)
            {
                newWaveOne = true;
                updateWaveOne = true;
            }
            if (seconds == fortySec)
            {
                newWaveTwo = true;
                updateWaveTwo = true;
            }
            if (seconds == oneMinute)
            {
                newWaveThree = true;
                updateWaveThree = true;
            }


            // TODO: Add your update logic here
            if (newWaveOne)
            {
                EnemyManager.InitWaveOne();
                for (int i = 0; i <  EnemyManager.waveOneCount; i++)
                {
                    int mRand;
                    mRand = (Globals.random.Next(0,3));
                    if (mRand == 0)
                        EnemyManager.waveOne[i].LoadContent(this.Content, "needler", mRand);
                    else if(mRand == 1)
                        EnemyManager.waveOne[i].LoadContent(this.Content, "alien-spaceship", mRand);
                    else if(mRand == 2)
                        EnemyManager.waveOne[i].LoadContent(this.Content, "viper", mRand);
                    else if (mRand == 3)
                        EnemyManager.waveOne[i].LoadContent(this.Content, "needler", mRand);
                }
            }

            if (newWaveTwo)
            {
                EnemyManager.InitWaveTwo();
                for (int i = 0; i < EnemyManager.waveTwoCount; i++)
                {
                    int mRand;
                    mRand = (Globals.random.Next(0, 3));
                    if (mRand == 0)
                        EnemyManager.waveTwo[i].LoadContent(this.Content, "needler", mRand);
                    else if (mRand == 1)
                        EnemyManager.waveTwo[i].LoadContent(this.Content, "alien-spaceship", mRand);
                    else if (mRand == 2)
                        EnemyManager.waveTwo[i].LoadContent(this.Content, "viper", mRand);
                    else if (mRand == 3)
                        EnemyManager.waveTwo[i].LoadContent(this.Content, "needler", mRand);
                }
            }

            EnemyManager.Update(gameTime,  thePlayer, updateWaveOne, updateWaveTwo);
            healthRec = new Rectangle(100, 5, thePlayer.mHealth, 20);

            thePlayer.Update(gameTime);

            newWaveOne = false;
            newWaveTwo = false;

            seconds++;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            //Calculate translation to centre screen
            Globals.translation = Globals.Centre - thePlayer.mPosition;
            spriteBatch.Begin();

            //Globals.radarTranslation = Globals.Universe / 2 - thePlayer.mPosition;
            spriteBatch.Draw(Globals.Background, Vector2.Zero, Color.White);

            if (thePlayer.mHealth < 0)
            {
                spriteBatch.DrawString(font, "GameOver", new Vector2(500, 500), Color.Red);
                //this.Exit();
            }

            thePlayer.Draw(spriteBatch);
            EnemyManager.Draw( gameTime,  spriteBatch, updateWaveOne, updateWaveTwo);

            for (int i = 0; i < thePlayer.mRockets.Length; i++)
            {
                if (thePlayer.mRockets[i].alive == true)
                {
                    thePlayer.mRockets[i].Draw(spriteBatch);
                }
            }

            for (int i = 0; i < thePlanets.Length; i++)
            {
                thePlanets[i].Draw(spriteBatch);
            }
            //spriteBatch.DrawString(font, thePlayer.mPosition.ToString(), new Vector2(20, 45), Color.White);
            
            //Draw Radar!!
            //one pixel per ship/planet
            //radar is in top left corner
            Globals.radarTranslation = Globals.Universe / 2 - thePlayer.mPosition;
            spriteBatch.Draw(Globals.radarBackground, Vector2.Zero, Color.White);

            for (int i = 0; i < EnemyManager.startingShips; i++)
            {
                if(EnemyManager.theShips[i].alive == true)
                {
                    Vector2 pos = EnemyManager.theShips[i].mPosition;
                    pos+=Globals.radarTranslation;
                    pos = Globals.wrapAround(pos);
                    pos = pos / Globals.grainSize;
                    spriteBatch.Draw(Globals.redPixel, pos,Color.White);
                }
            }
            if(updateWaveOne)
            {
                for (int i = 0; i < EnemyManager.waveOneCount; i++)
                {
                    if (EnemyManager.waveOne[i].alive == true)
                    {
                        Vector2 pos = EnemyManager.waveOne[i].mPosition;
                        pos += Globals.radarTranslation;
                        pos = Globals.wrapAround(pos);
                        pos = pos / Globals.grainSize;
                        spriteBatch.Draw(Globals.redPixel, pos, Color.White);
                    }
                }
            }

            if (updateWaveTwo)
            {
                for (int i = 0; i < EnemyManager.waveTwoCount; i++)
                {
                    if (EnemyManager.waveTwo[i].alive == true)
                    {
                        Vector2 pos = EnemyManager.waveTwo[i].mPosition;
                        pos += Globals.radarTranslation;
                        pos = Globals.wrapAround(pos);
                        pos = pos / Globals.grainSize;
                        spriteBatch.Draw(Globals.redPixel, pos, Color.White);
                    }
                }
            }

            for (int i = 0; i < thePlanets.Length; i++)
            {
                Vector2 pos = thePlanets[i].mPosition;
                pos += Globals.radarTranslation;
                pos = Globals.wrapAround(pos);
                pos = pos / Globals.grainSize; 
                spriteBatch.Draw(Globals.yellowPixel, pos, Color.White);
            }
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Draw(healthBar, healthRec, Color.White);


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
