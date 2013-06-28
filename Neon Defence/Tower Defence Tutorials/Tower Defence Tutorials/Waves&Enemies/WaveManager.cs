using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tower_Defence_Tutorials
{
    public class WaveManager
    {
        private int numberOfWaves; //How many waves in this level?
        private float timeSinceLastWave; //How long since last wave ended.

        private Queue<Wave> waves = new Queue<Wave>(); //A queue to hold our waves.

        private Texture2D enemyTexture; //Texture of enemy in the wave.
        private bool waveFinished = false; //Is the current wave over?
        private Level level; //Reference to level.
        private Player player;


        //Getter && Setters/Properties.
        public Wave CurrentWave    //Get current wave in Queue
        {
            get { return waves.Peek(); }
        }

        public List<Enemy> Enemies //Get current enemy list
        {
            get { return CurrentWave.Enemies; }
        }

        public int Round //Round/Wave number.
        {
            get { return CurrentWave.RoundNumber + 1; }
        }
        
        //Constructor
        public WaveManager(Level level, int numberOfWaves, Texture2D enemyTexture, 
            Texture2D healthTexture, Player player)
        {
            this.numberOfWaves = numberOfWaves;
            this.enemyTexture = enemyTexture;
            this.player = player;
            this.level = level;


            //Adding waves.
            for( int i = 0; i < numberOfWaves; i++)
            {
                int initalNumberOfEnemies = 6;
                int initalHealth = 3;
                int cashDrop = 2;

                //Modifier to add 6 enemies, Every 6 waves.
                //Is an integer so rounds down. (wave 1, 1/6 = 0 as it rounds down.)
                int enemyNumberModifier = (i / initalNumberOfEnemies) + 1;

                //Adds 3 to health every three waves.
                int healthHumberModifier = (i / 1) + 2;

                //Adds 2 to cashDrop every 6 waves.
                int cashDropModifier = (i / 6) + 1;


                //Initialising new wave.
                Wave wave = new Wave(i, initalNumberOfEnemies * enemyNumberModifier, initalHealth * healthHumberModifier,
                    cashDrop * cashDropModifier, level, enemyTexture, healthTexture, player);

                //Adding wave to Queue.
                waves.Enqueue(wave);

                StartNextWave();
            }
        }

        public void StartNextWave()
        {
            if (waves.Count > 0)    //If there are waves left.
            {
                waves.Peek().Start();   //Start the next wave.

                timeSinceLastWave = 0; //Reset timer,
                waveFinished = false;
            }
        }


        //Update loop.
        public void Update(GameTime gametime)
        {
            CurrentWave.Update(gametime);   //Update wave.

            if (CurrentWave.RoundOver)  //If the wave is over
                waveFinished = true;

            if (waveFinished)
                timeSinceLastWave += (float)gametime.ElapsedGameTime.TotalSeconds;     //Starts timer.

            if (timeSinceLastWave > 5.0f)  //If to seconds has passed since last wave
            {
                waves.Dequeue();    //Remove finished wave.
                StartNextWave();    //Start next wave.
            }
        }


        //Draw the wave
        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentWave.Draw(spriteBatch);
        }
    }
}
