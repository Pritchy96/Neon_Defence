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

namespace Tower_Defence
{
    public class GameClass : Microsoft.Xna.Framework.Game
    {
        
        #region Class Variables 

        //Input Devices
        MouseState MouseState;
        KeyboardState keyState;
        KeyboardState prevKeyState;

        //Game Constants
        public const int GAME_HEIGHT = 600;
        public const int GAME_WIDTH = 800;
        public const string GAME_NAME = "Neon Defence";
        public const Boolean MOUSE_VISABLE = true;

        //Rendering Stuff
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Objects
        Level level = new Level();
        WaveManager waveManager;
        Player player;

        //GUI
        Toolbar toolBar;
        Button MachineGunButton;
        Button slowButton;
        Button laserButton;

        bool paused;

        #endregion
     

        //Constructor.
        public GameClass()
        {
            graphics = new GraphicsDeviceManager(this);                      
            Content.RootDirectory = "Content";
        }
        

        //Initializing the game. Run only at start.
        protected override void Initialize()
        {
            //Game setup stuff.
            graphics.PreferredBackBufferHeight = GAME_HEIGHT + 100;
            graphics.PreferredBackBufferWidth = GAME_WIDTH;
            this.Window.Title = GAME_NAME;
            this.IsMouseVisible = MOUSE_VISABLE;

            graphics.ApplyChanges();

            base.Initialize();
        }


        //Loading all the content in the game.
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Textures.LoadContent(Content);

            #region GUI Content
            
            Texture2D ToolBar = Content.Load<Texture2D>("GUI\\Toolbar");
            SpriteFont font = Content.Load<SpriteFont>("GUI\\Arial");

            //GUI for MachineGun
            Texture2D MachineGunNormal = Content.Load<Texture2D>("GUI\\MachineGun\\MachineGunButtonNormal");
            Texture2D MachineGunHover = Content.Load<Texture2D>("GUI\\MachineGun\\MachineGunButtonHover");

            //GUI for Slow
            Texture2D SlowNormal = Content.Load<Texture2D>("GUI\\Slow\\SlowButtonNormal");
            Texture2D SlowHover = Content.Load<Texture2D>("GUI\\Slow\\SlowButtonHover");

            //GUI for Laser
            Texture2D LaserNormal = Content.Load<Texture2D>("GUI\\Laser\\LaserButtonNormal");
            Texture2D LaserHover = Content.Load<Texture2D>("GUI\\Laser\\LaserButtonHover");


            //Initalize Toolbar Buttons
            MachineGunButton = new Button(MachineGunNormal, MachineGunHover, new Vector2(20, level.Height * 43));
            MachineGunButton.Clicked += new EventHandler(MachineGunButton_Clicked);
            MachineGunButton.OnPress += new EventHandler(MachineGunButton_OnPress);

            slowButton = new Button(SlowNormal, SlowHover, new Vector2(80, level.Height * 43));
            slowButton.Clicked += new EventHandler(SlowButton_Clicked);
            slowButton.OnPress += new EventHandler(SlowButton_OnPress);

            laserButton = new Button(LaserNormal, LaserHover, new Vector2(140, level.Height * 43));
            laserButton.Clicked += new EventHandler(SniperButton_Clicked);
            laserButton.OnPress += new EventHandler(SniperButton_OnPress);

            //Radius texture
            Texture2D radiusTexture = Content.Load<Texture2D>("RadiusTexture");
            #endregion

            #region Enemy Content
            Texture2D healthBarTexture = Content.Load<Texture2D>("HealthBar");

            Texture2D enemyTexture = Content.Load<Texture2D>("BasicEnemy");
            #endregion

            #region Tower Content
            //Array of tower textures.
            Texture2D[,] towerTextures = new Texture2D[3,3];
               
            

            towerTextures[0, 0] = Content.Load<Texture2D>("Towers\\MachineGun\\MachineGun");
            towerTextures[0, 1] = Content.Load<Texture2D>("Towers\\MachineGun\\MachineGunUpgraded");
            Texture2D bulletTexture = Content.Load<Texture2D>("Towers\\MachineGun\\MachineGunBullet");

            towerTextures[1, 0] = Content.Load<Texture2D>("Towers\\Slow\\Slow");
            towerTextures[1, 1] = Content.Load<Texture2D>("Towers\\Slow\\SlowUpgraded");

            towerTextures[2, 0] = Content.Load<Texture2D>("Towers\\Laser\\Laser");
            towerTextures[2, 1] = Content.Load<Texture2D>("Towers\\Laser\\LaserUpgraded");
            Texture2D laserTexture = Content.Load<Texture2D>("Towers\\MachineGun\\MachineGunBullet");
            #endregion
            
            #region Initializing things needing these Textures.

            //Initalize player.
            player = new Player(level, towerTextures, bulletTexture, laserTexture, radiusTexture);
            //Initialize wave
            waveManager = new WaveManager(level, 10000, enemyTexture, healthBarTexture, player);
            //Initialize toolbar
            toolBar = new Toolbar(ToolBar, font, new Vector2(0, level.Height *40));
            #endregion

        }


        protected override void UnloadContent()
        {
            Textures.UnLoadContent();
        }


        #region Click and Drag events Events.

        //Click Events.
        //Telling player class we want to build a MachineGun.
        private void MachineGunButton_Clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "MachineGun";
            player.NewTowerIndex = 0;
        }

        private void SlowButton_Clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Slow";
            player.NewTowerIndex = 1;
        }

        private void SniperButton_Clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Sniper";
            player.NewTowerIndex = 2;
        }

        //Drag events
        private void MachineGunButton_OnPress(object sender, EventArgs e)
        {
            player.NewTowerType = "MachineGun";
            player.NewTowerIndex = 0;
        }

        private void SlowButton_OnPress(object sender, EventArgs e)
        {
            player.NewTowerType = "Slow";
            player.NewTowerIndex = 1;
        }

        private void SniperButton_OnPress(object sender, EventArgs e)
        {
            player.NewTowerType = "Sniper";
            player.NewTowerIndex = 2;
        }
        
        #endregion

        
        //Update loop.
        protected override void Update(GameTime gameTime)
        {

            //Update the Mouse and Keyboard states
            keyState = Keyboard.GetState();
            MouseState = Mouse.GetState();

            //Allows easy exiting during debug!
            if (keyState.IsKeyDown(Keys.Escape)) { this.Exit(); }

            //Update Game if it is not paused.
            if (paused == false)
            {
                    waveManager.Update(gameTime);
                    player.Update(gameTime, waveManager.Enemies);

                    //Button updates.
                    MachineGunButton.Update(gameTime);
                    slowButton.Update(gameTime);
                    laserButton.Update(gameTime);

                    base.Update(gameTime);
              }

            //If Space is pressed
            if (keyState.IsKeyUp(Keys.Space) && prevKeyState.IsKeyDown(Keys.Space))
            {
                // Toggle paused.
                paused = !paused;
            }

            //If Space is pressed
            if (keyState.IsKeyUp(Keys.M) && prevKeyState.IsKeyDown(Keys.M))
            {
                // Toggle paused.
                player.money += 100;
            }

            //Update prevKeyState
            prevKeyState = keyState;
            }


        //Drawing/Rendering.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Begins to draw the sprites drawn in the classes, Immediate means draw
            //as they are listed, and the AlphaBlend just means that Alpha is allowed.
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
  
            //draw Game here.
            level.Draw(spriteBatch);
            waveManager.Draw(spriteBatch);
            player.Draw(spriteBatch);

            //GUI
            toolBar.Draw(spriteBatch, player, waveManager);
            MachineGunButton.Draw(spriteBatch);
            slowButton.Draw(spriteBatch);
            laserButton.Draw(spriteBatch);

            player.DrawPreview(spriteBatch);

            spriteBatch.End();
      
            base.Draw(gameTime);
        }
  
    }
}
